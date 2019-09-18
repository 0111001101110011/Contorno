using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using Emgu;
using Emgu.CV;
using Emgu.CV.Structure;

namespace Segmentacion
{
    public partial class Form1 : Form
    {
        Image<Bgr, byte> _imgInput;
        Image<Gray, byte> _imgGray;
        public Form1()
        {
            InitializeComponent();
        }

        private void CargarImagenToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();

            if (ofd.ShowDialog() == DialogResult.OK)
            {
                _imgInput = new Image<Bgr, byte>(ofd.FileName);
                pictureBox1.Image = _imgInput.Bitmap;
            }
        }

        private void detectarToolStripMenuItem_Click(object sender, EventArgs e)
        {

            //Binarización
            Image<Gray, byte> _imgOutput = _imgInput.Convert<Gray, byte>().ThresholdBinary(new Gray(50), new Gray(255));
            Emgu.CV.Util.VectorOfVectorOfPoint countours = new Emgu.CV.Util.VectorOfVectorOfPoint();

            Mat hier = new Mat();

            //Encontrar Contorno
            CvInvoke.FindContours(_imgOutput, countours, hier, Emgu.CV.CvEnum.RetrType.External, Emgu.CV.CvEnum.ChainApproxMethod.ChainApproxSimple);

            CvInvoke.DrawContours(_imgOutput, countours, -1, new MCvScalar(255,0,0));

            pictureBox2.Image = _imgOutput.Bitmap;

        }
    }
}
