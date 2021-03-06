﻿using System;
using System.IO;
using CassiniDev;
using CUITe.Controls;
using CUITe.Controls.SilverlightControls;
using CUITe.PageObjects;
using CUITe.SearchConfigurations;
using Microsoft.VisualStudio.TestTools.UITesting;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Sut.SilverlightTest.PageObjects;

namespace Sut.SilverlightTest
{
    [CodedUITest]
    [DeploymentItem("Sut.Silverlight.html")]
#if DEBUG
    [DeploymentItem(@"..\..\..\Sut.Silverlight\Bin\Debug\Sut.Silverlight.xap")]
#else
    [DeploymentItem(@"..\..\..\Sut.Silverlight\Bin\Release\Sut.Silverlight.xap")]
#endif
    public class SilverlightControlTests
    {
        //TODO: the silverlight control must be hosted on a page served through a web server (ex. iis, cassini \ web dev server) because IE 9 may
        //treat web pages differently between http://localhost and file:// (compatibility mode\view)

        private static readonly CassiniDevServer WebServer = new CassiniDevServer();
        private static readonly string HostName = "localhost";
        private static readonly int Port = 8080;
        private static readonly string PageUrl = string.Format("http://{0}:{1}/Sut.Silverlight.html", HostName, Port);
        
        [ClassInitialize]
        public static void ClassInitialize(TestContext testContext)
        {
            WebServer.StartServer(Directory.GetCurrentDirectory(), Port, "/", HostName);
        }

        [ClassCleanup]
        public static void ClassCleanup()
        {
            WebServer.StopServer();
        }

        [TestMethod]
        public void SlButtonAndEditAndDTP_ClickAndSetTextAndSelectedDateAsString_Succeeds()
        {
            BrowserWindow b = BrowserWindow.Launch(PageUrl);
            b.SetFocus();
            b.Find<SilverlightButton>(By.AutomationId("button1")).Click();
            SilverlightEdit oEdit = b.Find<SilverlightEdit>(By.AutomationId("textBox1"));
            oEdit.Text = "asddasdasdasdadasdadasdadadadasd";
            SilverlightDatePicker dp = b.Find<SilverlightDatePicker>(By.AutomationId("datePicker1"));
            dp.SourceControl.SelectedDate = new DateTime(2011, 5, 11);
            b.Close();
        }

        [TestMethod]
        public void SlList_PageObjects_Succeeds()
        {
            var page = Page.Launch<TestPage>(PageUrl);
            page.List.SelectedIndices = new[] { 2 };
            Assert.IsTrue(page.List.SelectedItemsAsString == "Coded UI Test");
        }

        [TestMethod]
        public void SlList_DynamicObjectRecognition_Succeeds()
        {
            BrowserWindow b = BrowserWindow.Launch(PageUrl);
            b.SetFocus();
            SilverlightList oList = b.Find<SilverlightList>(By.AutomationId("listBox1"));
            oList.SelectedIndices = new[] { 2 };
            Assert.IsTrue(oList.SelectedItemsAsString == "Coded UI Test");
            b.Close();
        }

        [TestMethod]
        public void SlComboBox_SelectItem_Succeeds()
        {
            var browserWindow = BrowserWindow.Launch(PageUrl);
            browserWindow.SetFocus();
            var oCombo = browserWindow.Find<SilverlightComboBox>(By.AutomationId("comboBox1"));
            oCombo.SelectIndex(3);
            foreach (UITestControl temp in oCombo.Items)
            {
                Console.WriteLine(temp.Name);
            }
            browserWindow.Close();
        }

        [TestMethod]
        public void SlTab_SelectedIndex_Succeeds()
        {
            BrowserWindow b = BrowserWindow.Launch(PageUrl);
            b.SetFocus();
            SilverlightTab oTab = b.Find<SilverlightTab>(By.AutomationId("tabControl1"));
            oTab.SelectedIndex= 1;
            Assert.IsTrue(oTab.SourceControl.Items[0].Name == "tabItem1");
            b.Close();
        }

        [TestMethod]
        public void SlTab_TraverseSiblingsAndChildren_Succeeds()
        {
            BrowserWindow b = BrowserWindow.Launch(PageUrl);
            b.SetFocus();
            SilverlightTab oTab = b.Find<SilverlightTab>(By.AutomationId("tabControl1"));
            oTab.SelectedIndex = 0;
            var btnOK = b.Find<SilverlightButton>(By.AutomationId("OKButtonInTabItem1"));
            ((SilverlightEdit)(btnOK.PreviousSibling)).Text = "blah blah hurray";
            foreach (ControlBase control in oTab.GetChildren())
            {
                if (control.GetType() == typeof(SilverlightEdit))
                {
                    ((SilverlightEdit)control).Text = "Text Changed";
                    break;
                }
            }
            Assert.IsTrue(((SilverlightTab)btnOK.Parent).SelectedItem == "tabItem1");
            b.Close();
        }
    }
}

