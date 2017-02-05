using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace QzoneAlbumDownloader
{
    public partial class Form_Debug : Form
    {
        public Form_Debug()
        {
            InitializeComponent();
            button1.Text = Process.GetCurrentProcess().Threads.Count.ToString();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            MaterialSkin.Controls.MaterialProcessBar ctl = new MaterialSkin.Controls.MaterialProcessBar();
            ctl.LengthValue = 100;
            ctl.StepValue = 7;
            ctl.Width = 300;
            ctl.Processing = true;
            flowLayoutPanel1.Controls.Add(ctl);
            button1.Text = Process.GetCurrentProcess().Threads.Count.ToString();
        }
    }
}
