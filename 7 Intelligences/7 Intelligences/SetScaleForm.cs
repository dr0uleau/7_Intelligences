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
        public SetScaleForm()
        {
            InitializeComponent();
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            int timeScaleInt = 0;
            string timeScale = txtTimeScale.Text;
            if (int.TryParse(timeScale, out timeScaleInt))
            {
                // Set time scale to timeScaleInt
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
