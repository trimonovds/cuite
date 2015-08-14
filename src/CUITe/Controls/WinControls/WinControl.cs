﻿using System;
using CUITControls = Microsoft.VisualStudio.TestTools.UITesting.WinControls;

namespace CUITe.Controls.WinControls
{
    /// <summary>
    /// Base wrapper class for all CUITe WinForms controls, inherits from ControlBase
    /// </summary>
    /// <typeparam name="T">The Coded UI WinControl type</typeparam>
    public class WinControl<T> : ControlBase<T> where T : CUITControls.WinControl
    {
        public WinControl()
        {
        }

        public WinControl(string searchParameters)
            : base(searchParameters)
        {
        }

        /// <summary>
        /// Gets the parent of the current CUITe control.
        /// </summary>
        public override IControlBase Parent
        {
            get
            {
                SourceControl.WaitForControlReady();

                IControlBase ret = null;

                try
                {
                    ret = WinControlFactory.Create((CUITControls.WinControl)SourceControl.GetParent());
                }
                catch (ArgumentOutOfRangeException)
                {
                    throw new InvalidTraversalException(string.Format("({0}).Parent", SourceControl.GetType().Name));
                }
                return ret;
            }
        }
    }
}