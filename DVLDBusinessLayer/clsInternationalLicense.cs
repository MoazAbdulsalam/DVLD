using DVLDDataAccessLayer;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DVLDBusinessLayer
{
    public class clsInternationalLicense : clsApplication
    {
        public enum enMode { AddNew,Update}
        public enMode Mode;

        public int InternationalLicenseID { get;protected  set; }
        public int DriverID { get; set; }
        public clsDriver DriverInfo;
        public int IssuedUsingLocalLicenseID { get; set; }
        public DateTime IssueDate { get; set; }
        public DateTime ExpirationDate { get; set; }
        public bool IsActive { get; set; }


        public clsInternationalLicense()
        {
            Mode = enMode.AddNew;
            this.ApplicationTypeID = (int) clsApplication.enApplicationType.NewInternationalLicense;

            InternationalLicenseID = -1;
            ApplicationID = -1;
            DriverID = -1;
            DriverInfo = new clsDriver();
            IssuedUsingLocalLicenseID = -1;
            IssueDate = DateTime.Now;
            ExpirationDate = DateTime.Now;
            IsActive = false;
            CreatedByUserID = -1;
        }
        public clsInternationalLicense(int InternationalLicenseID, int DriverID, int IssuedUsingLocalLicenseID, DateTime IssueDate, DateTime ExpirationDate, bool IsActive,
            int ApplicationID, int ApplicantPersonID, DateTime ApplicationDate, int ApplicationTypeID,
             enApplicationStatus ApplicationStatus, DateTime LastStatusDate, float PaidFees, int CreatedByUserID) : base( ApplicationID,  ApplicantPersonID,  ApplicationDate,  ApplicationTypeID,
              ApplicationStatus,  LastStatusDate,  PaidFees,  CreatedByUserID)
        {
            Mode = enMode.Update;
            this.InternationalLicenseID = InternationalLicenseID;
            this.ApplicationID = ApplicationID;
            this.DriverID = DriverID;
            this.DriverInfo =  clsDriver.FindDriverByDriverID(DriverID);
            this.IssuedUsingLocalLicenseID = IssuedUsingLocalLicenseID;
            this.IssueDate = IssueDate;
            this.ExpirationDate = ExpirationDate;
            this.IsActive = IsActive;
            this.CreatedByUserID = CreatedByUserID;

        }
        public static clsInternationalLicense Find(int InternationalLicenseID)
        {

            int ApplicationID = -1, DriverID = -1, IssuedUsingLicenseID = -1, CreatedByUserID = -1;
            DateTime IssueDate = DateTime.Now, ExpirationDate= DateTime.Now;
            bool isActive = false;
            if(clsInternationalLicensesData.GetInternationalLicenseByID(InternationalLicenseID,ref ApplicationID,ref DriverID,ref IssuedUsingLicenseID,ref IssueDate,ref ExpirationDate,ref isActive,ref CreatedByUserID))
            {
                clsApplication Application = clsApplication.FindBaseApplication(ApplicationID);
                return new clsInternationalLicense(InternationalLicenseID,DriverID,IssuedUsingLicenseID,IssueDate,ExpirationDate,isActive,ApplicationID,Application.ApplicantPersonID,Application.ApplicationDate,Application.ApplicationTypeID,Application.ApplicationStatus,Application.LastStatusDate,Application.PaidFees,Application.CreatedByUserID);
            }

            return null;
        }
        bool _AddNew()
        {
            InternationalLicenseID = clsInternationalLicensesData.AddNewInternationalLicense(ApplicationID, DriverID, IssuedUsingLocalLicenseID, IssueDate, ExpirationDate, IsActive, CreatedByUserID);
            return InternationalLicenseID != -1;
        }
        bool _Update()
        {
            return clsInternationalLicensesData.UpdateInternationalLicense(InternationalLicenseID, ApplicationID, DriverID,IssuedUsingLocalLicenseID, IssueDate,ExpirationDate,IsActive, CreatedByUserID);
        }
        public new bool Save()
        {
            base.Mode = (clsApplication.enMode)Mode;
            if (!base.Save()) return false;
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
        public static DataTable GetAllInternationalLicenses()
        {
            return clsInternationalLicensesData.GetAllInternationalLicenses();
        }
        public static DataTable GetDriverlInternationalLicenses(int DriverID)
        {
            return clsInternationalLicensesData.GetAllDriverInternationalLicenses(DriverID);

        }
        public static int GetActiveInternationalLicense(int DriverID)
        {
            return clsInternationalLicensesData.GetDriverActiveInternationalLicenseID(DriverID) ;

        }


        public static bool DoesDriverHasActiveInternationalLicense(int DriverID)
        {
            return clsInternationalLicensesData.GetDriverActiveInternationalLicenseID(DriverID) != -1;
        }
        public bool IsLicenseIssued()
        {
            return DoesDriverHasActiveInternationalLicense(DriverID);
        }
        public bool DeActivate()
        {
            return clsInternationalLicensesData.DeActivate(InternationalLicenseID);
        }
    }
}
