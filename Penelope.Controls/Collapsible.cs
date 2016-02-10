using System;
using System.Drawing;
using System.Windows.Forms;

namespace Penelope.Controls
{
    public partial class Collapsible : UserControl
    {
        #region Fields + Properties

        public Control Control
        {
            get
            {
                return control;
            }
            set
            {
                control = value;
            }
        }

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

        private Size SizeCollapsed;
        private Size SizeExpanded;

        #endregion Fields + Properties

        #region Constructors

        public Collapsible()
        {
            InitializeComponent();
        }

        #endregion Constructors

        #region Methods

        public void SetCollapsingParameters(Size size, bool collapsedByDefault)
        {
            Size sizeCollapsed = SizeExpanded = size;
            sizeCollapsed.Height = label.Height;
            SizeCollapsed = sizeCollapsed;

            // Requested collapsed by default
            if (collapsedByDefault)
            {
                control.Visible = true;
                label_DoubleClick(null, null);
            }
        }

        private void label_DoubleClick(object sender, EventArgs e)
        {
            control.Visible = !control.Visible;

            // Depending on visibility, the component is either collapsed or not
            Size = control.Visible ? SizeExpanded : SizeCollapsed;
        }

        #endregion Methods
    }
}
