namespace RTCV.UI
{
    using System;
    using System.Windows.Forms;
    using RTCV.UI.Modular;

    public partial class SelectBoxForm : ComponentForm, IBlockable
    {
        private new void HandleMouseDown(object s, MouseEventArgs e) => base.HandleMouseDown(s, e);
        private new void HandleFormClosing(object s, FormClosingEventArgs e) => base.HandleFormClosing(s, e);

        private ComponentForm[] _childForms;

        public SelectBoxForm(ComponentForm[] childForms)
        {
            InitializeComponent();

            _childForms = childForms ?? throw new ArgumentNullException(nameof(childForms));

            cbSelectBox.DisplayMember = "text";
            cbSelectBox.ValueMember = "value";

            foreach (var item in _childForms)
            {
                cbSelectBox.Items.Add(new { text = item.Text, value = item });
            }
        }

        private void AnchorSelectedItemToPanel(object sender, EventArgs e)
        {
            ((cbSelectBox.SelectedItem as dynamic)?.value as ComponentForm)?.AnchorToPanel(pnComponentForm);
        }

        private void OnLoad(object sender, EventArgs e)
        {
            cbSelectBox.SelectedIndex = 0;
        }
    }
}
