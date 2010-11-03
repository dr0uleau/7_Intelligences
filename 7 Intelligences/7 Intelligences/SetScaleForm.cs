using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace _7_Intelligences
{
    public partial class SetScaleForm : Form
    {
        private int timeScale;

        public int TimeScale
        {
            get { return timeScale; }
            set { timeScale = value; }
        }

        public SetScaleForm()
        {
            InitializeComponent();
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            string timeScaleStr = txtTimeScale.Text;
            // Set time scale to timeScale
            if (int.TryParse(timeScaleStr, out timeScale))
            {
                Close();
            }
            else
            {
                MessageBox.Show("Input must be a valid integer", "Input Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }
    }
}
