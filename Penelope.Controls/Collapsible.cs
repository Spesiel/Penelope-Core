using System;
using System.Drawing;
using System.Windows.Forms;

namespace Penelope.Controls
{
    public partial class Collapsible : UserControl
    {
        #region Fields + Properties

        public string Title
        {
            get
            {
                return label.Text;
            }

            set
            {
                label.Text = value;
            }
        }

        private bool _AreParametersSet;
        private Size _SizeCollapsed;
        private Size _SizeExpanded;

        #endregion Fields + Properties

        #region Methods

        private Control _Control;

        public Collapsible()
        {
            InitializeComponent();
        }

        public void SetCollapsingParameters(Size size, Control control, bool collapsedByDefault)
        {
            if (control == null)
            {
                throw new ArgumentNullException(nameof(control));
            }
            _AreParametersSet = true;

            _Control = control;
            Size sizeCollapsed = _SizeExpanded = size;
            sizeCollapsed.Height = label.Height;
            _SizeCollapsed = sizeCollapsed;

            // Requested collapsed by default
            if (collapsedByDefault)
            {
                control.Visible = true;
                label_DoubleClick(null, null);
            }
        }

        private void label_DoubleClick(object sender, EventArgs e)
        {
            if (!_AreParametersSet)
            {
                throw new MissingFieldException(nameof(_Control));
            }

            _Control.Visible = !_Control.Visible;

            // Depending on visibility, the component is either collapsed or not
            Size = _Control.Visible ? _SizeExpanded : _SizeCollapsed;
        }

        #endregion Methods
    }
}
