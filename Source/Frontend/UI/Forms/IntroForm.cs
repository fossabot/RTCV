﻿namespace RTCV.UI
{
    using System;
    using System.Windows.Forms;
    using RTCV.NetCore;
    using RTCV.Common;

    public partial class IntroForm : Form, IAutoColorize
    {
        public IntroAction selection { get; private set; } = IntroAction.EXIT;

        public IntroForm()
        {
            try
            {
                InitializeComponent();
            }
            catch (Exception ex)
            {
                if (CloudDebug.ShowErrorDialog(ex, true) == DialogResult.Abort)
                {
                    throw new AbortEverythingException();
                }
            }
        }

        private void OnLoad(object sender, EventArgs e)
        {
            Colors.SetRTCColor(Colors.GeneralColor, this);
        }

        private void OnFormClosing(object sender, FormClosingEventArgs e)
        {
            if (e.CloseReason != CloseReason.UserClosing)
            {
                return;
            }

            if (selection == IntroAction.EXIT)
            {
                if (VanguardImplementation.connector.netConn.status == NetCore.Enums.NetworkStatus.CONNECTED)
                {
                    LocalNetCoreRouter.Route(NetcoreCommands.VANGUARD, NetcoreCommands.REMOTE_EVENT_CLOSEEMULATOR);
                }

                Environment.Exit(0);
            }
        }

        private void Exit(object sender, EventArgs e)
        {
            selection = IntroAction.EXIT;
            this.Close();
        }

        private void SelectSimpleMode(object sender, EventArgs e)
        {
            selection = IntroAction.SIMPLEMODE;
            Close();
        }

        private void SelectNormalMode(object sender, EventArgs e)
        {
            selection = IntroAction.NORMALMODE;
            Close();
        }

        private void CheckDisclaimerAgreement(object sender, EventArgs e)
        {
            if (cbAgree.Checked)
            {
                btnSimpleMode.Visible = true;

                if (btnNormalMode.Text != "Continue")
                {
                    lbStartupMode.Visible = true;
                    btnNormalMode.Visible = true;
                }
            }
        }

        internal void DisplayRtcvDisclaimer(string disclaimer)
        {
            cbAgree.Checked = false;
            btnSimpleMode.Visible = false;
            lbStartupMode.Visible = false;
            btnNormalMode.Visible = false;

            this.Text = "Welcome to RTCV";

            tbDisclaimerText.Text = disclaimer;

            selection = IntroAction.EXIT;
            this.ShowDialog();
        }
    }

    public enum IntroAction { EXIT, SIMPLEMODE, NORMALMODE }
}
