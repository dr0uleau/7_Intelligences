using System;
using System.Timers;
using System.IO;
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
        private int timeScale;
        private DateTime oldTime;
        System.Timers.Timer timer = new System.Timers.Timer();

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            // Set the bar values based on the data file
            readDataFile();
            setBarValues(calculateHourDifference());

            setupTimer();
        }

        // This method is set to trigger after the amount of time required to decrease the bar control by 1
        private void UpdateTimeEvent(object source, ElapsedEventArgs e)
        {
            setBarValues(calculateHourDifference());
        }

        private void setupTimer()
        {
            // Remove the event handler so an updated one can be created
            timer.Elapsed -= UpdateTimeEvent;

            // Setup the timer to decrease the bar control values in real-time
            timer.Elapsed += new ElapsedEventHandler(UpdateTimeEvent);
            double tickToScaleRatio = barSpatial.Maximum / timeScale;
            // interval is the number of hours required to subtract one tick
            double interval = 1 / tickToScaleRatio;
            // This converts the hour unit into milliseconds
            timer.Interval = interval * 60 * 60 * 1000;
            timer.Start();
        }

        private double calculateHourDifference()
        {
            DateTime nowTime = DateTime.Now;
            double hours = ((nowTime.DayOfYear - oldTime.DayOfYear) * 24) +
                (nowTime.Hour - oldTime.Hour) + 
                (((double)nowTime.Minute - (double)oldTime.Minute) / 60) +
                (((double)nowTime.Second - (double)oldTime.Second) / 3600);

            oldTime = DateTime.Now;
            return hours;
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

            // If the time scale changes, set the new time scale, reset the time, and reset the timer
            if (timeScale != setScale.TimeScale && setScale.TimeScale != 0)
            {
                timeScale = setScale.TimeScale;
                oldTime = DateTime.Now;
                setupTimer();
            }
        }

        private void Form1_Closing(object sender, FormClosingEventArgs e)
        {
            StreamWriter sw = null;
            try
            {
                string path = Path.Combine(Application.StartupPath, "7Intelligences.dat");
                sw = new StreamWriter(path);

                // Write the date and each value of each bar control
                sw.WriteLine(timeScale + "\t" + DateTime.Now + "\t" + barSpatial.Value.ToString() + "\t" + 
                    barLinguistic.Value.ToString() +"\t" + barLogical.Value.ToString() + "\t" + 
                    barKinesthetic.Value.ToString() + "\t" + barMusical.Value.ToString() + "\t" + 
                    barInterpersonal.Value.ToString() + "\t" + barIntrapersonal.Value.ToString());
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Runtime Exception", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                if(sw != null) {
                    sw.Close();
                }
            }
        }

        private void readDataFile()
        {
            StreamReader sr = null;
            try
            {
                string path = Path.Combine(Application.StartupPath, "7Intelligences.dat");
                if (File.Exists(path))
                {
                    sr = new StreamReader(path);

                    string line = sr.ReadLine();

                    while (line != null)
                    {
                        string[] data = line.Split('\t');
                        timeScale = int.Parse(data[0]);
                        oldTime = DateTime.Parse(data[1]);
                        barSpatial.Value = int.Parse(data[2]);
                        barLinguistic.Value = int.Parse(data[3]);
                        barLogical.Value = int.Parse(data[4]);
                        barKinesthetic.Value = int.Parse(data[5]);
                        barMusical.Value = int.Parse(data[6]);
                        barInterpersonal.Value = int.Parse(data[7]);
                        barIntrapersonal.Value = int.Parse(data[8]);

                        line = sr.ReadLine();
                    }
                }
                else
                {
                    // If this is the first time running the program, set the default values
                    timeScale = 24;
                    oldTime = DateTime.Now;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Runtime Exception", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                if (sr != null)
                {
                    sr.Close();
                }
            }
        }

        private void setBarValues(double hourDifference)
        {
            int numberTicks = barSpatial.Maximum;
            double tickToScaleRatio = numberTicks / timeScale;
            int ticksToSubtract = (int)Math.Round(tickToScaleRatio * hourDifference);

            // An invoker is needed here because the timer event handler causes
            // this method to be called from another thread
            MethodInvoker invoker = new MethodInvoker(delegate()
            {

                if ((barSpatial.Value - ticksToSubtract) < 0)
                {
                    barSpatial.Value = 0;
                }
                else
                {
                    barSpatial.Value = barSpatial.Value - ticksToSubtract;
                }
                if ((barLinguistic.Value - ticksToSubtract) < 0)
                {
                    barLinguistic.Value = 0;
                }
                else
                {
                    barLinguistic.Value = barLinguistic.Value - ticksToSubtract;
                }
                if ((barLogical.Value - ticksToSubtract) < 0)
                {
                    barLogical.Value = 0;
                }
                else
                {
                    barLogical.Value = barLogical.Value - ticksToSubtract;
                }
                if ((barKinesthetic.Value - ticksToSubtract) < 0)
                {
                    barKinesthetic.Value = 0;
                }
                else
                {
                    barKinesthetic.Value = barKinesthetic.Value - ticksToSubtract;
                }
                if ((barMusical.Value - ticksToSubtract) < 0)
                {
                    barMusical.Value = 0;
                }
                else
                {
                    barMusical.Value = barMusical.Value - ticksToSubtract;
                }
                if ((barInterpersonal.Value - ticksToSubtract) < 0)
                {
                    barInterpersonal.Value = 0;
                }
                else
                {
                    barInterpersonal.Value = barInterpersonal.Value - ticksToSubtract;
                }
                if ((barIntrapersonal.Value - ticksToSubtract) < 0)
                {
                    barIntrapersonal.Value = 0;
                }
                else
                {
                    barIntrapersonal.Value = barIntrapersonal.Value - ticksToSubtract;
                }

            }
            );
            this.BeginInvoke(invoker);
        }
    }
}
