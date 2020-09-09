namespace RTCV.UI
{
    using System;
    using System.Windows.Forms;
    using RTCV.CorruptCore;
    using RTCV.Common;
    using RTCV.UI.Modular;

    public partial class RTC_SettingsCorrupt_Form : ComponentForm, IAutoColorize, IBlockable
    {
        public new void HandleMouseDown(object s, MouseEventArgs e) => base.HandleMouseDown(s, e);
        public new void HandleFormClosing(object s, FormClosingEventArgs e) => base.HandleFormClosing(s, e);

        public RTC_SettingsCorrupt_Form()
        {
            InitializeComponent();

            Colors.SetRTCColor(Colors.GeneralColor, this);

            Load += RTC_SettingRerollForm_Load;

            var handler = new EventHandler<Components.Controls.ValueUpdateEventArgs<decimal>>(nmMaxInfiniteStepUnits_ValueChanged);
            nmMaxInfiniteStepUnits.ValueChanged += handler;
            nmMaxInfiniteStepUnits.registerSlave(S.GET<CorruptionEngineForm>().updownMaxCheats, handler);
            nmMaxInfiniteStepUnits.registerSlave(S.GET<CorruptionEngineForm>().updownMaxFreeze, handler);
            nmMaxInfiniteStepUnits.registerSlave(S.GET<CorruptionEngineForm>().updownMaxPipes, handler);
            nmMaxInfiniteStepUnits.registerSlave(S.GET<RTC_CustomEngineConfig_Form>().updownMaxInfiniteUnits, handler);
            nmMaxInfiniteStepUnits.registerSlave(S.GET<SimpleModeForm>().updownMaxInfiniteUnits, handler);

            cbRerollAddress.Checked = RtcCore.RerollAddress;
            cbRerollSourceAddress.Checked = RtcCore.RerollSourceAddress;

            cbRerollDomain.Checked = RtcCore.RerollDomain;
            cbRerollSourceDomain.Checked = RtcCore.RerollSourceDomain;

            cbRerollFollowsCustom.Checked = RtcCore.RerollFollowsCustomEngine;
            cbIgnoreUnitOrigin.Checked = RtcCore.RerollIgnoresOriginalSource;
        }

        private void nmMaxInfiniteStepUnits_ValueChanged(object sender, EventArgs e)
        {
            StepActions.MaxInfiniteBlastUnits = Convert.ToInt32(nmMaxInfiniteStepUnits.Value);
        }

        private void RTC_SettingRerollForm_Load(object sender, EventArgs e)
        {
        }

        private void cbRerollSourceAddress_CheckedChanged(object sender, EventArgs e)
        {
            RtcCore.RerollSourceAddress = cbRerollSourceAddress.Checked;
            if (!cbRerollSourceAddress.Checked)
            {
                cbRerollSourceDomain.Checked = false;
                cbRerollSourceDomain.Enabled = false;
            }
            else
            {
                cbRerollSourceDomain.Enabled = true;
            }
        }

        private void cbRerollDomain_CheckedChanged(object sender, EventArgs e)
        {
            RtcCore.RerollDomain = cbRerollDomain.Checked;
        }

        private void cbRerollSourceDomain_CheckedChanged(object sender, EventArgs e)
        {
            RtcCore.RerollSourceDomain = cbRerollSourceDomain.Checked;
        }

        private void cbRerollAddress_CheckedChanged(object sender, EventArgs e)
        {
            RtcCore.RerollAddress = cbRerollAddress.Checked;
            if (!cbRerollAddress.Checked)
            {
                cbRerollDomain.Checked = false;
                cbRerollDomain.Enabled = false;
            }
            else
            {
                cbRerollDomain.Enabled = true;
            }
        }

        private void CbRerollFollowsCustom_CheckedChanged(object sender, EventArgs e)
        {
            RtcCore.RerollFollowsCustomEngine = cbRerollFollowsCustom.Checked;
        }

        private void CBRerollIgnoresOriginalSource(object sender, EventArgs e)
        {
            RtcCore.RerollIgnoresOriginalSource = cbIgnoreUnitOrigin.Checked;
        }

        public void SetRewindBoxes(bool enabled)
        {
            DontUpdateSpec = true;
            cbClearStepUnitsOnRewind.Checked = enabled;
            DontUpdateSpec = false;
        }

        public void SetLockBoxes(bool enabled)
        {
            DontUpdateSpec = true;
            cbLockUnits.Checked = enabled;
            DontUpdateSpec = false;
        }

        private bool DontUpdateSpec;

        private void CbClearStepUnitsOnRewind_CheckedChanged(object sender, EventArgs e)
        {
            if (DontUpdateSpec)
            {
                return;
            }

            S.GET<CorruptionEngineForm>().SetRewindBoxes(cbClearStepUnitsOnRewind.Checked);
            S.GET<RTC_CustomEngineConfig_Form>().SetRewindBoxes(cbClearStepUnitsOnRewind.Checked);
            S.GET<SimpleModeForm>().SetRewindBoxes(cbClearStepUnitsOnRewind.Checked);

            StepActions.ClearStepActionsOnRewind = cbClearStepUnitsOnRewind.Checked;
        }

        private void CbLockUnits_CheckedChanged(object sender, EventArgs e)
        {
            if (DontUpdateSpec)
            {
                return;
            }

            S.GET<CorruptionEngineForm>().SetLockBoxes(cbLockUnits.Checked);

            StepActions.LockExecution = cbLockUnits.Checked;
        }
    }
}
