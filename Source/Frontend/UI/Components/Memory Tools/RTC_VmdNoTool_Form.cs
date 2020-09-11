﻿namespace RTCV.UI
{
    using System.Windows.Forms;
    using RTCV.Common;
    using RTCV.UI.Modular;

    public partial class RTC_VmdNoTool_Form : ComponentForm, IAutoColorize, IBlockable
    {
        private new void HandleMouseDown(object s, MouseEventArgs e) => base.HandleMouseDown(s, e);
        private new void HandleFormClosing(object s, FormClosingEventArgs e) => base.HandleFormClosing(s, e);

        public RTC_VmdNoTool_Form()
        {
            InitializeComponent();

            popoutAllowed = false;
        }
    }
}
