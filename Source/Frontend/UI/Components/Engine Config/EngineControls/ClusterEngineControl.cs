namespace RTCV.UI.Components.EngineConfig.EngineControls
{
    using System;
    using System.Windows.Forms;
    using RTCV.CorruptCore;

    internal partial class ClusterEngineControl : EngineConfigControl
    {
        internal ClusterEngineControl()
        {
            InitializeComponent();
        }

        private void UpdateClusterLimiterList(object sender, EventArgs e)
        {
            ComboBoxItem<string> item = (ComboBoxItem<string>)((ComboBox)sender).SelectedItem;
            if (item != null)
            {
                CorruptCore.ClusterEngine.LimiterListHash = item.Value;
            }
        }

        private void UpdateClusterChunkSize(object sender, EventArgs e)
        {
            CorruptCore.ClusterEngine.ChunkSize = (int)clusterChunkSize.Value;
        }
    }
}