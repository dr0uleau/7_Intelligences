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
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            // Setup track bar data to update every x minutes
        }

        private void ResetToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("Are you sure you want to reset the bar values to 0?", "Reset bar values", 
                MessageBoxButtons.YesNo, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button1);

            if (result.ToString() == "Yes")
            {
                // Set all the bar values to 0
                barSpatial.Value = 0;
                barLinguistic.Value = 0;
                barLogical.Value = 0;
                barKinesthetic.Value = 0;
                barMusical.Value = 0;
                barInterpersonal.Value = 0;
                barIntrapersonal.Value = 0;
            }
        }

        private void AboutToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            AboutBox1 aboutBox = new AboutBox1();
            aboutBox.ShowDialog();
        }

        private void setTimeScaleToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SetScaleForm setScale = new SetScaleForm();
            setScale.ShowDialog();
        }

        private void Form1_Closing(object sender, FormClosingEventArgs e)
        {
            // Save track bar data to file
        }
    }
}
