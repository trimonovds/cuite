﻿using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UITesting;
using CUITControls = Microsoft.VisualStudio.TestTools.UITesting.WinControls;

namespace CUITe.Controls.WinControls
{
    /// <summary>
    /// Wrapper class for WinRow
    /// </summary>
    public class WinRow : WinControl<CUITControls.WinRow>
    {
        public WinRow()
        {
        }

        public WinRow(string searchParameters)
            : base(searchParameters)
        {
        }

        public UITestControlCollection Cells
        {
            get { return UnWrap().Cells; }
        }

        public List<WinCell> CellsAsCUITe
        {
            get
            {
                List<WinCell> list = new List<WinCell>();
                foreach (CUITControls.WinCell control in UnWrap().Cells)
                {
                    WinCell cell = new WinCell();
                    cell.WrapReady(control);
                    list.Add(cell);
                }
                return list;
            }
        }

        public int RowIndex
        {
            get { return UnWrap().RowIndex; }
        }

        public bool Selected
        {
            get { return UnWrap().Selected; }
        }

        public string Value
        {
            get { return UnWrap().Value; }
        }
    }
}