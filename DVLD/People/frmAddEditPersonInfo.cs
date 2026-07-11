using DVLD.Classes;
using DVLD.Properties;
using DVLDBusinessLayer;
using Shared;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DVLD.People
{
    public partial class frmAddEditPersonInfo : Form
    {
        public delegate void DataBackEventHandler(object sender, int PersonID);
        public event DataBackEventHandler DataBack;
        public enum enMode { AddNew,Update};
        enMode eMode;
        clsPerson _Person;
        int _PersonID =-1;
        
        public frmAddEditPersonInfo(int PersonID)
        {
            InitializeComponent();
            _PersonID = PersonID;
            eMode = enMode.Update;
            
        }
        public frmAddEditPersonInfo()
        {
            InitializeComponent();
            eMode = enMode.AddNew;
        }
        private void _FillCountries()
        {
            DataTable dt = clsCountry.GetAllCountries();
            foreach (DataRow dr in dt.Rows)
            {
                cbCountries.Items.Add(dr["CountryName"]);
            }
        }
        private void _ResetDefoaltValues()
        {
            _FillCountries();
            if(eMode== enMode.AddNew)
            {
                lblAddEdit.Text = "Add New Person";
                _Person = new clsPerson();
            }
            else
                lblAddEdit.Text = "    Edit Person";
            
            if(rbtnMale.Checked)
                pbPersonImage.Image = Resources.Male_512;
            else
                pbPersonImage.Image = Resources.Female_512;

            dtpDateOfBirth.MaxDate = DateTime.Now.AddYears(-18);
            lnklblRemove.Visible=pbPersonImage.ImageLocation!= null;
            cbCountries.SelectedIndex = cbCountries.FindString("Egypt");
            txtBxFirstName.Text = "";
            txtBxSecondName.Text = "";
            txtbxThirdName.Text = "";
            txtbxLastName.Text = "";
            txtbxNationalNo.Text = "";
            txtbxPhone.Text = "";
            txtbxEmail.Text = "";
            txtbxAddress.Text = "";
            rbtnMale.Checked = true;

        }
        private void _LoadData()
        {

            _Person = clsPerson.Find(_PersonID);
            if (_Person == null)
            {
                MessageBox.Show("Person not Found");
                this.Close();
                return;
            }

            lblPersonID.Text = _PersonID.ToString();
            txtBxFirstName.Text = _Person.FirstName;
            txtBxSecondName.Text = _Person.SecondName;
            txtbxThirdName.Text = _Person.ThirdName;
            txtbxLastName.Text = _Person.LastName;
            txtbxNationalNo.Text = _Person.NationalNo;
            txtbxEmail.Text = _Person.Email;
            txtbxPhone.Text = _Person.Phone;
            txtbxAddress.Text = _Person.Address;
            dtpDateOfBirth.Value = _Person.DateOfBirth;
            cbCountries.SelectedIndex = cbCountries.Items.IndexOf(_Person.CountryInfo.CountryName);

            if (_Person.Gendor == 0)
                rbtnMale.Checked=true;
            else
                rbtnFemale.Checked=true;


            if (!string.IsNullOrWhiteSpace(_Person.ImagePath))
            {
                    pbPersonImage.ImageLocation = _Person.ImagePath;
            }
            lnklblRemove.Visible = _Person.ImagePath != "";
        }
        private void frmAddEditPersonInfo_Load(object sender, EventArgs e)
        {
            _ResetDefoaltValues();

            if(eMode ==enMode.Update )
                _LoadData();
        }
        private void btnClose_Click(object sender, EventArgs e)
        {
            Close();
        }
        private bool _HandlePersonImage()
        {

            //this procedure will handle the person image,
            //it will take care of deleting the old image from the folder
            //in case the image changed. and it will rename the new image with guid and 
            // place it in the images folder.


            //_Person.ImagePath contains the old Image, we check if it changed then we copy the new image
            if (_Person.ImagePath != pbPersonImage.ImageLocation)
            {
                if (_Person.ImagePath != "")
                {
                    //first we delete the old image from the folder in case there is any.

                    try
                    {
                        File.Delete(_Person.ImagePath);
                    }
                    catch (IOException iox)
                    {
                        // We could not delete the file.
                        //log it later
                        string Location = "frmAddEditPersonInfo → _HandlePersonImage";
                        clsEventLogger.LogEvent(iox, Location, System.Diagnostics.EventLogEntryType.Error);
                    }
                }

                if (pbPersonImage.ImageLocation != null)
                {
                    //then we copy the new image to the image folder after we rename it
                    string SourceImageFile = pbPersonImage.ImageLocation.ToString();

                    if (clsUtil.CopyImageToProjectImagesFolder(ref SourceImageFile))
                    {
                        pbPersonImage.ImageLocation = SourceImageFile;
                        return true;
                    }
                    else
                    {
                        MessageBox.Show("Error Copying Image File", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return false;
                    }
                }

            }
            return true;
        }
        private void btnSave_Click(object sender, EventArgs e)
        {
            if (!this.ValidateChildren())
            {
                MessageBox.Show("Complete Requaired Fields");
                return;
            }
            if(!_HandlePersonImage())
            {
                return;
            }

            _Person.FirstName = txtBxFirstName.Text.Trim();
            _Person.SecondName = txtBxSecondName.Text.Trim();
            _Person.ThirdName = txtbxThirdName.Text.Trim(); 
            _Person.LastName = txtbxLastName.Text.Trim();
            _Person.NationalNo = txtbxNationalNo.Text.Trim();
            _Person.Address = txtbxAddress.Text.Trim();
            _Person.Phone = txtbxPhone.Text.Trim();
            _Person.Email =txtbxEmail.Text.Trim();

            _Person.Gendor = (short)(rbtnMale.Checked == true ? 0 : 1);
            _Person.DateOfBirth = dtpDateOfBirth.Value ;

            _Person.CountryInfo = clsCountry.Find(cbCountries.SelectedItem.ToString());
            _Person.NationalityCountryID =_Person.CountryInfo.CountryID;
            
            if(pbPersonImage.ImageLocation!=null)
            {
                _Person.ImagePath = pbPersonImage.ImageLocation;
            }
            else
            {
                _Person.ImagePath = "";
            }

            if (_Person.Save())
            {
                MessageBox.Show("Saved Succefully", "Save", MessageBoxButtons.OK, MessageBoxIcon.Information);
                eMode = enMode.Update;
                lblAddEdit.Text = "    Edit Person";
                lblPersonID.Text = _Person.PersonID.ToString();
                DataBack?.Invoke(this, _Person.PersonID);
            }
            else
                MessageBox.Show("Save Failed", "Save Error", MessageBoxButtons.OK, MessageBoxIcon.Error);      
        }
        private void lnklblSetImage_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            openFileDialog1.Filter = "Image Files|*.jpg;*.jpeg;*.png;*.gif;*.bmp";
            openFileDialog1.FilterIndex = 1;
            openFileDialog1.RestoreDirectory = true;

            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                // Process the selected file
                string selectedFilePath = openFileDialog1.FileName;
                //MessageBox.Show("Selected Image is:" + selectedFilePath);

                pbPersonImage.Load(selectedFilePath);
                lnklblRemove.Visible = true;
                // ...
            }
        }
        private void lnklblRemove_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            pbPersonImage.ImageLocation = null;
            pbPersonImage.Image = rbtnMale.Checked ? Resources.Male_512 : Resources.Female_512;
            lnklblRemove.Visible = false;

        }


        //valedating
        private void ClearErrorOnTextChange(object sender, EventArgs e)
        {
            if(sender is TextBox txt)
            {
                errorProvider1.SetError(txt, "");
            }
        }

        private void ValedateNotEmpty(object sender, CancelEventArgs e)
        {
            if (sender is TextBox txt)
            {
                if (string.IsNullOrEmpty(txt.Text.Trim()))
                {
                    e.Cancel = true;
                    txt.Focus();
                    errorProvider1.SetError(txt, "Required Feild");
                }
                else
                {
                    e.Cancel = false;
                    errorProvider1.SetError(txt, "");
                }
            }
        }

        private void txtbxNationalNo_Validating(object sender, CancelEventArgs e)
        {
           

            if (string.IsNullOrEmpty(txtbxNationalNo.Text.Trim()))
            {
                e.Cancel = true;
                txtbxNationalNo.Focus();
                errorProvider1.SetError(txtbxNationalNo, "Required Feild");
                return;

            }
            else
            {
                e.Cancel= false;
                errorProvider1.SetError(txtbxNationalNo, "");
            }
            if (clsPerson.isPersonExist(txtbxNationalNo.Text) && eMode == enMode.AddNew)
            {
                e.Cancel = true;
                txtbxNationalNo.Focus();
                errorProvider1.SetError(txtbxNationalNo, "National Number Allready Exists");
            }
            else
            {
                e.Cancel = false;
                errorProvider1.SetError(txtbxNationalNo, "");
            }
        }

        private void txtbxPhone_Validating(object sender, CancelEventArgs e)
        {
           if(string.IsNullOrEmpty(txtbxPhone.Text) || txtbxPhone.Text.Length<10)
           {
               e.Cancel = true;
               txtbxPhone.Focus();
               errorProvider1.SetError(txtbxPhone, "Required");
           }
           else
            {
                e.Cancel= false;
                errorProvider1.SetError(txtbxPhone, "");
            }
        }

        private void txtbxPhone_KeyPress(object sender, KeyPressEventArgs e)
        {
            if(!char.IsDigit(e.KeyChar) && e.KeyChar!=8)
            {
                e.Handled = true;
                return;
            }

        }

        private void txtbxEmail_Validating(object sender, CancelEventArgs e)
        {
            if(txtbxEmail.Text.Length==0)
            {
                return;
            }
            if (!Regex.IsMatch(txtbxEmail.Text, @"^[^@\s]+@[^@\s]+\.[^@\s]+$"))
            {
                e.Cancel = true;
                errorProvider1.SetError(txtbxEmail, "Invalid Email");
            }


        }

        private void CheckedChanged(object sender, EventArgs e)
        {
            if (pbPersonImage.ImageLocation == null)
                pbPersonImage.Image = rbtnMale.Checked ? Resources.Male_512 : Resources.Female_512;
        }

        private void cbCountries_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}
