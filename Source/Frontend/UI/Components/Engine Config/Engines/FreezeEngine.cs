namespace RTCV.UI.Components.EngineConfig.Engines
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Data;
    using System.Drawing;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using System.Windows.Forms;

    internal partial class FreezeEngine : EngineConfigControl
    {
        internal FreezeEngine(CorruptionEngineForm parent)
        {
            InitializeComponent();

            btnClearAllFreezes.Click += parent.ClearCheats;
            cbClearFreezesOnRewind.CheckedChanged += parent.OnClearRewindToggle;
        }
    }
}
