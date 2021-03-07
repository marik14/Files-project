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

namespace WorkWithFiles
{
    public partial class Form1 : Form
    {
        string path = @"../../file.txt";
        string nameOfManager;
        string nameOfBussiness;
        int newEmploee;

        public Form1()
        {
            InitializeComponent();
        }

        private void btnShowFile_Click(object sender, EventArgs e)
        {
            using (StreamReader sr = new StreamReader(path))
            {
                string text = sr.ReadToEnd();
                richTextBox1.Text = text;
            }

        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            nameOfManager = txtOnerName.Text;
            nameOfBussiness = txtBusiName.Text;
            newEmploee = Convert.ToInt32(txtNumOfEmploy.Text);
            string AllInfo = "Owner Name:" + nameOfManager + "\n Bussiness He Own:," +
                nameOfBussiness + "\n Number Of Emploee Added:" + newEmploee + "\n";
            Console.WriteLine("**************************");
            File.AppendAllText(path, AllInfo);
            MessageBox.Show("All Been Added");
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            try
            {
                string find = txtOld.Text.Trim(); //txtFind is textbox and will have the text that we want to find and replace
                string replace = txtNew.Text.Trim(); //txtReplace is text and it will replace the find text with Replace text
                string newText = richTextBox1.Text.Replace(find, replace);
                richTextBox1.Text = newText;

                //Write the new contents of rich text box to file
                File.WriteAllText(path, richTextBox1.Text.Trim());
                MessageBox.Show(find + " Replaced with" + replace);

            }
            catch (Exception ex)
            {
                lblEror.Text = ex.Message;
            }

        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            string tempFile = Path.GetTempFileName();
            using (var sr = new StreamReader(path))
            using (var sw = new StreamWriter(tempFile))
            {
                string line;

                while ((line = sr.ReadLine()) != null)
                {
                    if (!line.Contains(this.txtDelete.Text))
                        sw.WriteLine(line);//remember the string in tempfile.
                    else
                    {
                        sw.WriteLine(line.Replace(this.txtDelete.Text, ""));//replace the string with empty string.
                        MessageBox.Show(this.txtDelete.Text + " Deleted");
                    }

                }
            }

            File.Delete(path);
            File.Move(tempFile, path);//write back to the file with new data.
        }

        private void btnDeleteAll_Click(object sender, EventArgs e)
        {
            File.WriteAllText(path, String.Empty);
            MessageBox.Show("ALL Been Deleted");
        }

       
    }
}
