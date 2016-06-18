using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Clinic
{
    
    public partial class Form1 : Form
    {
        public string CSVPath = "C:\\Users\\Gok\\Desktop\\clinic.csv";
        public string backupEmail;
        public Form1()
        {
            InitializeComponent();
        }

        private void openFileDialog1_FileOk(object sender, CancelEventArgs e)
        {

        }

        private void richTextBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void button2_Click_1(object sender, EventArgs e)
        {
            // Show the dialog and get result.
            DialogResult result = openFileDialog1.ShowDialog();
            if (result == DialogResult.OK) // Test result.
            {
                Console.WriteLine(openFileDialog1.FileName);
                CSVFiletextBox.Text = openFileDialog1.FileName;
                CSVPath = openFileDialog1.FileName;
            }
            
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {

                string[] lines = File.ReadAllLines(CSVPath);
                for (int i = 0; i < lines.Length; i++)
                {

                    if (lines[i].Contains(nameTextBox.Text))
                    {
                        string [] filtered = lines[i].Split(',');
                        resultRichTextBox.AppendText(filtered[0] + " " + filtered[1] + "\n");
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error is " + ex.ToString());
                throw;
            }
        }
    }
}
