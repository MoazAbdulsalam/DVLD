using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DVLDDataAccessLayer;
namespace DVLDBusinessLayer
{
    public class clsDetainedLicense
    {
        public enum enMode { AddNew, Update }
        public enMode Mode;

        public int DetainID { get; set; }
        public int LicenseID { get; set; }
        public clsLicense License;
        public DateTime DetainDate { get; set; }
        public float FineFees { get; set; }
        public int CreatedByUserID { get; set; }
        public clsUser CreatedByUserInfo;
        public bool IsReleased { get; set; }
        public DateTime ReleaseDate { get; set; }
        public int ReleasedByUserID { get; set; }
        public clsUser ReleasedByUserInfo;
        public  int ReleaseApplicationID  { get; set; }
        public clsApplication ReleaseApplication;
        public clsDetainedLicense()
        {
            Mode = enMode.AddNew;
           
            LicenseID = -1;
            DetainDate = DateTime.Now;
            FineFees = 0;
            CreatedByUserID= -1;
            IsReleased = false;
            ReleaseDate = DateTime.MaxValue;
            ReleasedByUserID = 0;
            ReleaseApplicationID = -1;
        }
        private clsDetainedLicense(int DetainID,
            int LicenseID, DateTime DetainDate,
            float FineFees, int CreatedByUserID,
            bool IsReleased, DateTime ReleaseDate,
            int ReleasedByUserID, int ReleaseApplicationID)
        {
            this.DetainID = DetainID;
            this.LicenseID = LicenseID;
            this.DetainDate = DetainDate;
            this.CreatedByUserID = CreatedByUserID;
            this.IsReleased = IsReleased;
            this. ReleaseDate = ReleaseDate;
            this.ReleasedByUserID = ReleasedByUserID;
            this.ReleaseApplicationID = ReleaseApplicationID;
            this.FineFees = FineFees;
            this.ReleasedByUserInfo = clsUser.FindByPersonID(this.ReleasedByUserID);
            this.CreatedByUserInfo = clsUser.FindByUserID(this.CreatedByUserID);
            Mode = enMode.Update;
        }

        private bool _UpdateDetainedLicense()
        {
            return clsDetainedLicensesData.UpdateDetainedLicense(DetainID, LicenseID, DetainDate, FineFees, CreatedByUserID);
        }
        private bool _AddNewDetainedLicense()
        {
            DetainID = clsDetainedLicensesData.AddNewDetainedLicense(LicenseID, DetainDate, FineFees, CreatedByUserID);
            return DetainID != -1;
        }
        public bool Save()
        {
            switch (Mode)
            {
                case enMode.AddNew:
                    if (_AddNewDetainedLicense())
                    {
                        Mode = enMode.Update;
                        return true;
                    }
                    else
                        return false;
                case enMode.Update:
                    return _UpdateDetainedLicense();
                default:
                    return false;
                    
            }
        }
        public static clsDetainedLicense FindByDetainID(int DetainID)
        {
            int LicenseID = -1, CreatedByUserID = -1, ReleasedByUserID = -1, ReleaseApplocationID=-1;
            float FineFees = 0;
            bool IsReleased =false;
            DateTime DetainDate = DateTime.Now;
            DateTime ReleaseDate = DateTime.MaxValue;
            if (clsDetainedLicensesData.GetDetainedLicenseInfoByID(DetainID,ref LicenseID,ref DetainDate,ref FineFees,ref CreatedByUserID,ref IsReleased,ref ReleaseDate,ref ReleasedByUserID,ref ReleaseApplocationID))
            {
                return new clsDetainedLicense(DetainID, LicenseID, DetainDate, FineFees, CreatedByUserID, IsReleased, ReleaseDate, ReleasedByUserID, ReleaseApplocationID);
            }
            else
                return null;
        }
        public static clsDetainedLicense FindByLicenseID(int LicenseID)
        {
            int DetainID = -1, CreatedByUserID = -1, ReleasedByUserID = -1, ReleaseApplocationID = -1;
            float FineFees = 0;
            bool IsReleased = false;
            DateTime DetainDate = DateTime.Now;
            DateTime ReleaseDate = DateTime.MaxValue;
            if (clsDetainedLicensesData.GetDetainedLicenseInfoByLicenseID( LicenseID,ref DetainID, ref DetainDate, ref FineFees, ref CreatedByUserID, ref IsReleased, ref ReleaseDate, ref ReleasedByUserID, ref ReleaseApplocationID))
            {
                return new clsDetainedLicense(DetainID, LicenseID, DetainDate, FineFees, CreatedByUserID, IsReleased, ReleaseDate, ReleasedByUserID, ReleaseApplocationID);
            }
            else
                return null;
        }
        public static DataTable GetALlDetainedLicenses()
        {
            return clsDetainedLicensesData.GetAllDetainedLicenses();
        }
        public static bool IsLicenseDetained(int LicenseID)
        {
            return clsDetainedLicensesData.IsLicenseDetained(LicenseID);
        }
        public bool ReleaseDetainedLicense(int ReleasedByUserID, int ReleaseApplicationID)
        {
            return clsDetainedLicensesData.ReleaseDetainedLicense(this.DetainID, ReleaseApplicationID,
                   ReleasedByUserID);
        }


    }
}
