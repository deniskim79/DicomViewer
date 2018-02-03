using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CSTest
{
    public partial class FormMain : Form
    {
        public FormMain()
        {
            InitializeComponent();
        }

        private void buttonOpen_Click(object sender, EventArgs e)
        {
            myView.OpenDicom();
        }

        private void buttonRotateCW_Click(object sender, EventArgs e)
        {
            if (myView.IsImageOpen())
                myView.RotateFilp(CSCustomDisplay.CustomView.eRotateFlipDirection.eRotateCW);
        }

        private void buttonRotateCCW_Click(object sender, EventArgs e)
        {
            if (myView.IsImageOpen())
                myView.RotateFilp(CSCustomDisplay.CustomView.eRotateFlipDirection.eRotateCCW);
        }

        private void buttonRotate180_Click(object sender, EventArgs e)
        {
            if (myView.IsImageOpen())
                myView.RotateFilp(CSCustomDisplay.CustomView.eRotateFlipDirection.eRotate180);
        }

        private void buttonFlipTB_Click(object sender, EventArgs e)
        {
            if (myView.IsImageOpen())
                myView.RotateFilp(CSCustomDisplay.CustomView.eRotateFlipDirection.eFlipTB);
        }

        private void buttonFlipLR_Click(object sender, EventArgs e)
        {
            if (myView.IsImageOpen())
                myView.RotateFilp(CSCustomDisplay.CustomView.eRotateFlipDirection.eFlipLR);
        }

        private void buttonHeader_Click(object sender, EventArgs e)
        {
            if (myView.DcmFile == null)
                return;

            TagViewer allHeader = new TagViewer();
            allHeader.DcmFile = myView.DcmFile;

            allHeader.ShowDialog(this);
        }
    }
}
