using DVLDDataAccessLayer;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DVLDBusinessLayer
{
    public class clsDriver
    {
        public enum enMode { AddNew,Update}
        public enMode Mode;
        public int DriverID {  get; set; }
        public int PersonID { get; set; }
        public clsPerson PersonInfo;
        public int CreatedByUser { get; set; }
        public clsUser CreatedByUserInfo;
        public DateTime CreatedDate { get; set; }
        public clsDriver()
        {
            Mode = enMode.AddNew;
            PersonID = -1;
            DriverID = -1;
            CreatedByUser = -1;
            CreatedDate = DateTime.Now;
            PersonInfo = new clsPerson();
            CreatedByUserInfo = new clsUser();
        }
        private clsDriver(int DriverID, int PersonID,int CreatedByUserID,DateTime CreatedDate )
        {
            this.DriverID = DriverID;
            this.PersonID = PersonID;
            this.PersonInfo = clsPerson.Find(PersonID);
            this.CreatedByUser = CreatedByUserID;
            this.CreatedByUserInfo = clsUser.FindByUserID(CreatedByUserID);
            this.CreatedDate = CreatedDate;
            this.Mode = enMode.Update;

        }
        public static clsDriver FindDriverByDriverID(int DriverID)
        {
            int PersonID = -1;
            int CreatedByUserID = -1;
            DateTime CreatedDate = DateTime.Now;
            if(clsDriverData.GetDriverInfoByDriverID(DriverID,ref PersonID,ref CreatedByUserID,ref CreatedDate))
            {
                return new clsDriver(DriverID,PersonID,CreatedByUserID,CreatedDate);
            }
            return null;
        }
        public static clsDriver FindDriverByPersonID(int PersonID)
        {
            int DriverID = -1;
            int CreatedByUserID = -1;
            DateTime CreatedDate = DateTime.Now;
            if (clsDriverData.GetDriverInfoByPersonID(PersonID, ref DriverID, ref CreatedByUserID, ref CreatedDate))
            {
                return new clsDriver(DriverID, PersonID, CreatedByUserID, CreatedDate);
            }
            return null;
        }
        bool _AddNew()
        {
            if (clsDriverData.IsPersonDriver(PersonID))
                return false;
            this.DriverID = clsDriverData.AddNewDriver(PersonID,CreatedByUser,CreatedDate);
            return DriverID != -1;
        }
        bool _Update()
        {
            return clsDriverData.UpdateDriver(DriverID, PersonID, CreatedByUser, CreatedDate);
        }
        public bool Save()
        {

            switch (Mode)
            {
                case enMode.AddNew:
                    if (_AddNew())
                    {

                        Mode = enMode.Update;
                        return true;
                    }
                    else
                    {
                        return false;
                    }

                case enMode.Update:

                    return _Update();

            }

            return false;
        }
        public bool Delete()
        {
            return clsDriverData.Delete(this.DriverID);
        }
        public static bool Delete(int DriverID)
        {
            return clsDriverData.Delete(DriverID);
        }
        public static bool IsPersonDriver(int PersonID)
        {
            return clsDriverData.IsPersonDriver(PersonID);
        }
        public static bool IsDriverExist(int DriverID)
        {
            return clsDriverData.IsDriverExist(DriverID);
        }
        public static DataTable GetAllDrivers()
        {
            return clsDriverData.GetAllDrivers(); 
        }
        public DataTable GetDriverLicenses()
        {
            return clsLicenseData.GetDriverLicenses(this.DriverID);
        }
    }
}
