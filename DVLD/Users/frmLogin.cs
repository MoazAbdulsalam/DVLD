using DVLD.Classes;
using DVLDBusinessLayer;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DVLD.Users
{
    public partial class frmLogin : Form
    {
    
        
        clsUser _User;
        string _FilePath = @"D:\source\repos\c# course 19\DVLD\Login.txt";
        public frmLogin()
        {
            InitializeComponent();

        }
        
        private string Encrypt(string txt,char key='k')
        {
            StringBuilder Result = new StringBuilder();
            foreach (char c in txt)
            {
                Result.Append((char)( c ^ key));
            }
            return Result.ToString();
        }
        private string Decrypt(string txt ,char key='k')
        {
            return Encrypt(txt, key);
        }
        private void RememberMe()
        {
            string Data = txtbxUserName.Text + "#//#" + Encrypt(txtbxPassword.Text) + "#//#";
            if (chbRememberMe.Checked)
            {
                Data += "1" + "#//#" + DateTime.Now;

            }
            else
            {
                Data += "0" + "#//#" + DateTime.Now;

            }
            File.AppendAllText(_FilePath, Data + Environment.NewLine);

        }

        private void btnLogIn_Click(object sender, EventArgs e)
        {
            _User = clsUser.FindByUserNameAndPassword(txtbxUserName.Text, txtbxPassword.Text);
            if (_User == null)
            {
                MessageBox.Show("UserName Or Password Is wrong","Error",MessageBoxButtons.OK,MessageBoxIcon.Error);
                return;
            }
            if(!_User.IsActive)
            {
                MessageBox.Show("User IS Not Active ,Contact Your Admin", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

                RememberMe();
            
            clsGlobals.CurrentUser = _User;
            Form frm = new MainForm(this);
            frm.Show();
            this.Hide();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void frmLogin_Load(object sender, EventArgs e)
        {

            
            if (File.Exists(_FilePath))
            {
                string[] lines = File.ReadAllLines(_FilePath);
                if (lines.Length > 0)
                {
                    string lastLine = lines[lines.Length - 1]; 
                    string[] Data = lastLine.Split(new string[] { "#//#" }, StringSplitOptions.None);

                    if (Data.Length >= 3 && Data[2] == "1")
                    {
                        txtbxUserName.Text = Data[0];
                        txtbxPassword.Text = Decrypt(Data[1]);
                        chbRememberMe.Checked = true;
                    }
                }
            }

        }
        public  void Clear()
        {
            txtbxPassword.Text = "";
            txtbxUserName.Text = "";
            chbRememberMe.Checked= false;
        }
    }
}
