using System;
using System.IO;
using System.Windows.Forms;

namespace Clinic
{

    public partial class Form1 : Form
    {
        public string CSVPath = "C:\\Users\\Gok\\Desktop\\clinic.csv";
        public string backupEmail;
        public string configPath = "config.txt";
        public Form1()
        {
            InitializeComponent();
            readConfigFile();
        }

        private void csvBrowseButtonClick(object sender, EventArgs e)
        {
            // Show the dialog and get result.
            DialogResult result = openFileDialog1.ShowDialog();
            if (result == DialogResult.OK) // Test result.
            {
                Console.WriteLine(openFileDialog1.FileName);
                CSVFiletextBox.Text = openFileDialog1.FileName;
                CSVPath = openFileDialog1.FileName;
                using (StreamWriter sw = File.CreateText(configPath))
                {
                    sw.WriteLine("csvpath=" + CSVPath + "\n");
                    sw.WriteLine("email=" + backupEmail + "\n");
                }
            }

        }

        private void searchButtonClick(object sender, EventArgs e)
        {
            if (CSVPath.Length == 0)
            {
                MessageBox.Show("CSV path is not set!");
                return;
            }
            try
            {
                resultRichTextBox.Text = "";
                string[] lines = File.ReadAllLines(CSVPath);
                for (int i = 0; i < lines.Length; i++)
                {

                    if (lines[i].Contains(nameTextBox.Text))
                    {
                        string[] filtered = lines[i].Split(',');
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

        private void registerButtonClick(object sender, EventArgs e)
        {
            if (registerName.Text.Length == 0 || registerNumber.Text.Length == 0)
            {
                MessageBox.Show("Please input both Name and Number!");
                return;
            }

            register(registerName.Text, registerNumber.Text);
        }

        private string previousNumber = "";
        public void register(string name, string number)
        {
            if (previousNumber == number)
            {
                registerSuccessLabel.Text = "duplicated entry, number already registered.";
                registerSuccessLabel.Visible = true;
                return;
            }
            using (StreamWriter sw = File.AppendText(CSVPath))
            {
                sw.WriteLine(name + ',' + number + "," + DateTime.Now.GetDateTimeFormats()[103]);
            }
            registerSuccessLabel.Text = "Register Success";
            registerSuccessLabel.Visible = true;
            previousNumber = number;
        }

        private void readConfigFile()
        {
            string[] lines = File.ReadAllLines("config.txt");
            if (lines[0].Split('=').Length > 1)
                CSVPath = lines[0].Split('=')[1];
            if (lines[1].Split('=').Length > 1)
                backupEmail = lines[1].Split('=')[1];
            CSVFiletextBox.Text = CSVPath;
            emailTextBox.Text = backupEmail;
            Console.WriteLine("csv=" + CSVPath + " email=" + backupEmail);
        }
    }
}
