namespace RTCV.UI.Components.EngineConfig.EngineControls
{
    using RTCV.CorruptCore;

    internal partial class VectorEngineControl : EngineConfigControl
    {
        internal VectorEngineControl(CorruptionEngineForm parent)
        {
            InitializeComponent();

            cbVectorValueList.DataSource = null;
            cbVectorLimiterList.DataSource = null;

            cbVectorValueList.DisplayMember = "Name";
            cbVectorLimiterList.DisplayMember = "Name";
            cbVectorValueList.ValueMember = "Value";
            cbVectorLimiterList.ValueMember = "Value";

            cbVectorLimiterList.SelectedIndexChanged += parent.UpdateVectorLimiterList;
            cbVectorValueList.SelectedIndexChanged += parent.UpdateVectorValueList;
            cbVectorUnlockPrecision.CheckedChanged += parent.UpdateVectorUnlockPrecision;

            //Do this here as if it's stuck into the designer, it keeps defaulting out
            cbVectorValueList.DataSource = RtcCore.ValueListBindingSource;
            cbVectorLimiterList.DataSource = RtcCore.LimiterListBindingSource;
        }
    }
}
