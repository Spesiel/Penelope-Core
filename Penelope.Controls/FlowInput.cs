using System;
using System.Windows.Forms;

namespace Penelope.Controls
{
    public partial class FlowInput : UserControl
    {
        #region Fields + Properties

        private bool HasTextBox = false;

        #endregion Fields + Properties

        #region Constructors

        public FlowInput()
        {
            InitializeComponent();
        }

        #endregion Constructors

        #region Methods

        private void OnDoubleClick(object sender, EventArgs e)
        {
            if (!HasTextBox)
            {
                TextBox textBox = new TextBox();
                textBox.KeyPress += (s, ek) =>
                {
                    if (','.Equals(ek.KeyChar)) ek.Handled = true;

                    // Finished input
                    if ((((char)Keys.Return).Equals(ek.KeyChar) || (',').Equals(ek.KeyChar))
                        && !string.IsNullOrEmpty(textBox.Text))
                    {
                        flow.Controls.Remove(textBox);

                        Label newLabel = new Label();
                        newLabel.AutoSize = true;
                        newLabel.Margin = new Padding(3);
                        newLabel.BorderStyle = BorderStyle.FixedSingle;
                        newLabel.Cursor = Cursors.Hand;
                        newLabel.Text = textBox.Text;

                        flow.Controls.Add(newLabel);
                        flow.Controls[flow.Controls.Count - 1].DoubleClick += (se, edc) =>
                              flow.Controls.Remove(newLabel);
                        HasTextBox = false;
                    }
                };

                HasTextBox = true;
                flow.Controls.Add(textBox);
                flow.Controls[flow.Controls.Count - 1].Focus();
            }
        }

        #endregion Methods
    }
}
