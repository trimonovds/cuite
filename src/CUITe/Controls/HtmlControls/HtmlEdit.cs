﻿using CUITControls = Microsoft.VisualStudio.TestTools.UITesting.HtmlControls;

namespace CUITe.Controls.HtmlControls
{
    public class HtmlEdit : HtmlControl<CUITControls.HtmlEdit>
    {
        public HtmlEdit() : base() { }
        public HtmlEdit(string searchParameters) : base(searchParameters) { }

        public void SetText(string sText)
        {
            this._control.WaitForControlReady();
            this._control.Text = sText;
        }

        public string GetText()
        {
            this._control.WaitForControlReady();
            return this._control.Text; 
        }

        public bool ReadOnly
        {
            get
            {
                this._control.WaitForControlReady();
                return this._control.ReadOnly;
            }
        }
    }
}