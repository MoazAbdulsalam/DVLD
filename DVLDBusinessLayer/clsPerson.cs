using DVLDDataAccessLayer;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DVLDBusinessLayer
{
    public class clsPerson
    {
        public enum enMode { AddNew = 0, Update = 1 };
        public enMode Mode = enMode.AddNew;

        public int PersonID { get; private set; }
        public string FirstName { get; set; }
        public string SecondName { get; set; }
        public string ThirdName { get; set; }
        public string LastName { get; set; }
        public string FullName()
        {
            return FirstName + " " + SecondName + " " + ThirdName + " " + LastName;
        }
        public string NationalNo { get; set; }
        public DateTime DateOfBirth { get; set; }
        public short Gendor {  get; set; }// 0 = male 1= female
        public string Address { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public int NationalityCountryID { get; set; }
        public clsCountry CountryInfo;
        public string ImagePath { get; set; }

        public clsPerson()
        {
            PersonID = -1;
            FirstName = "";
            SecondName = "";
            ThirdName = "";
            LastName = "";
            NationalNo = "";
            DateOfBirth = DateTime.Now;
            Gendor = 0;
            Address = "";
            Phone = "";
            Email = "";
            NationalityCountryID = 0;
            ImagePath = "";
        }
        private clsPerson(int PersonID, string NationalNo,
            string FirstName, string SecondName, string ThirdName,
            string LastName, DateTime DateOfBirth, short Gendor,
            string Address, string Phone, string Email,
            int NationalityCountryID, string ImagePath)
        {
            this.PersonID=PersonID;
            this.NationalNo=NationalNo;
            this.FirstName=FirstName;
            this.SecondName=SecondName;
            this.ThirdName=ThirdName;
            this.LastName=LastName;
            this.DateOfBirth=DateOfBirth;
            this.Gendor=Gendor;
            this.Address=Address;
            this.Phone=Phone;
            this.Email=Email;
            this.NationalityCountryID = NationalityCountryID;
            this.ImagePath=ImagePath;
            this.Mode = enMode.Update;
            this.CountryInfo= clsCountry.Find(NationalityCountryID);


        }
        public static clsPerson Find(int PersonID)
        {
            int  NationalityCountryID = 0;
            string FirstName = "",SecondName = "",ThirdName = "",LastName = "",NationalNo = "", Address = "", Phone = "",Email="",ImagePath="" ;
            DateTime DateOfBirth = DateTime.Now;
            short Gendor = 0;
            
            if(clsPeopleData.GetPersonInfoById(PersonID,ref NationalNo,ref FirstName,ref SecondName,ref ThirdName,ref LastName,ref DateOfBirth,ref Gendor,ref Address,ref Phone,ref Email,ref NationalityCountryID,ref ImagePath))
            {
                return new clsPerson(PersonID,NationalNo,FirstName,SecondName,ThirdName,LastName,DateOfBirth,Gendor,Address,Phone,Email,NationalityCountryID,ImagePath);
            }

            return null;
        }
        public static clsPerson Find(string NationalNo)
        {
            int PersonID = -1, NationalityCountryID = 0;
            string FirstName = "", SecondName = "", ThirdName = "", LastName = "", Address = "", Phone = "", Email = "", ImagePath = "";
            DateTime DateOfBirth = DateTime.Now;
            short Gendor = 0;

            if (clsPeopleData.GetPersonInfoByNationalNo( ref PersonID,NationalNo, ref FirstName, ref SecondName, ref ThirdName, ref LastName, ref DateOfBirth, ref Gendor, ref Address, ref Phone, ref Email, ref NationalityCountryID, ref ImagePath))
            {
                return new clsPerson(PersonID, NationalNo, FirstName, SecondName, ThirdName, LastName, DateOfBirth, Gendor, Address, Phone, Email, NationalityCountryID, ImagePath);
            }

            return null;
        }

        private bool _AddNewPerson()
        {
            this.PersonID = clsPeopleData.AddnewPerson(this.NationalNo, this.FirstName, this.SecondName, this.ThirdName, this.LastName, this.DateOfBirth, this.Gendor, this.Address, this.Phone, this.Email, this.NationalityCountryID, this.ImagePath);
            return this.PersonID!=-1;
        }
        private bool _UpdatePerson()
        {
            return clsPeopleData.UpdatePerson(this.PersonID,this.NationalNo, this.FirstName, this.SecondName, this.ThirdName, this.LastName, this.DateOfBirth, this.Gendor, this.Address, this.Phone, this.Email, this.NationalityCountryID, this.ImagePath);
        }

        public bool Save()
        {


            switch (Mode)
            {
                case enMode.AddNew:
                    if (_AddNewPerson())
                    {

                        Mode = enMode.Update;
                        return true;
                    }
                    else
                    {
                        return false;
                    }

                case enMode.Update:

                    return _UpdatePerson();

            }

            return false;
        }
        public static DataTable GetAllPeople()
        {
            return clsPeopleData.GetAllPeople();

        }

        public static bool DeletePerson(int ID)
        {
            return clsPeopleData.DeletePeron(ID);
        }

        public static bool isPersonExist(int ID)
        {
            return clsPeopleData.IsPersonExist(ID);
        }
        public static bool isPersonExist(string NationalNo)
        {
            return clsPeopleData.IsPersonExist(NationalNo);
        }
    }
}
