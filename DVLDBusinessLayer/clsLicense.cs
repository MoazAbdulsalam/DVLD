using DVLDDataAccessLayer;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace DVLDBusinessLayer
{
    public class clsLicense
    {
        public enum enMode { AddNew,Update}
        public enMode Mode;
        public enum enIssueReason { FirstTime=1, Renew, DamagedReplacement, LostReplacement }
        public enIssueReason IssueReason { get; set; }
        public string IssueReasonText
        {
            get
            {
                return GetIssueReasonText(this.IssueReason);
            }
        }
        public int LicenseID { get; protected set; }
        public int ApplicationID { get; set; }
        public clsApplication ApplicationInfo;
        public int DriverID { get; set; }
        public clsDriver DriverInfo;
        public int LicenseClassID { get; set; }
        public clsLicenseClass LicenseClassInfo;
        public DateTime IssueDate { get; set; }
        public DateTime ExpireDate { get; set; }
        public string Notes { get; set; }
        public float PaidFees { get; set; }
        public bool IsActive { get; set; }
        public int CreatedByUserID { get; set; }
        public clsUser CreatedByUserInfo;
        public bool IsDetained 
        { 
            get { return clsDetainedLicense.IsLicenseDetained(LicenseID); }
        }
        public clsDetainedLicense DetainedInfo { get; set; }
        public static string GetIssueReasonText(enIssueReason IssueReason)
        {

            switch (IssueReason)
            {
                case enIssueReason.FirstTime:
                    return "First Time";
                case enIssueReason.Renew:
                    return "Renew";
                case enIssueReason.DamagedReplacement:
                    return "Replacement for Damaged";
                case enIssueReason.LostReplacement:
                    return "Replacement for Lost";
                default:
                    return "First Time";
            }
        }
        public clsLicense()
        {
            this.Mode = enMode.AddNew;
            this.IssueReason =enIssueReason.FirstTime;
            this.LicenseID =-1;
            this.DriverID = -1;
            this.DriverInfo = new clsDriver();
            this.ApplicationID =-1;
            this.ApplicationInfo=new clsApplication();
            this.LicenseClassID =-1;
            this.LicenseClassInfo =new clsLicenseClass();
            this.IssueDate = DateTime.Now;
            this.ExpireDate = DateTime.Now;
            this.Notes = "";
            this.PaidFees = 0f;
            this.IsActive = false;
            this.CreatedByUserID = -1;
            this.CreatedByUserInfo = new clsUser();
        }
        private clsLicense(int LicenseID,int ApplicationID, int DriverID, int LicenseClass,
                  DateTime IssueDate, DateTime ExpirationDate, string Notes,
                   float PaidFees, bool IsActive, enIssueReason IssueReason, int CreatedByUserID)
        {
            this.Mode = enMode.Update;
            this.IssueReason = IssueReason;
            this.LicenseID = LicenseID;
            this.DriverID = DriverID;
            this.DriverInfo = clsDriver.FindDriverByDriverID(DriverID);
            this.ApplicationID = ApplicationID;
            this.ApplicationInfo =clsApplication.FindBaseApplication(ApplicationID);
            this.LicenseClassID = LicenseClass;
            this.LicenseClassInfo =  clsLicenseClass.Find(LicenseClass);
            this.IssueDate = IssueDate;
            this.ExpireDate = ExpirationDate;
            this.Notes = Notes;
            this.PaidFees = PaidFees;
            this.IsActive = IsActive;
            this.CreatedByUserID = CreatedByUserID;
            this.CreatedByUserInfo = clsUser.FindByUserID(CreatedByUserID);
            this.DetainedInfo = clsDetainedLicense.FindByLicenseID(this.LicenseID);

        }
        public static clsLicense GetLicenseByLicenseID(int LicenseID)
        {
            int ApplicationID=-1, DriverID=-1, LicenseClass=-1, CreatedByUserID=-1;
            DateTime IssueDate= DateTime.Now, ExpirationDate= DateTime.Now;
            string Notes = "";
            float PaidFees = 0f;
            bool IsActive = false;
            byte IssueReason = (byte)enIssueReason.FirstTime;
            if(clsLicenseData.GetLicenseInfoByID(LicenseID,ref ApplicationID,ref DriverID,ref LicenseClass,ref IssueDate,ref ExpirationDate,ref Notes,ref PaidFees,ref IsActive,ref IssueReason,ref CreatedByUserID))
            {
                return new clsLicense(LicenseID,ApplicationID,DriverID,LicenseClass,IssueDate,ExpirationDate,Notes,PaidFees,IsActive,(enIssueReason)IssueReason,CreatedByUserID);
            }
            return null;
        }
        bool _AddNew()
        {
            this.LicenseID = clsLicenseData.AddNewLicense(ApplicationID,DriverID,LicenseClassID,IssueDate,ExpireDate,Notes,PaidFees,IsActive,(byte)IssueReason, CreatedByUserID);
            return LicenseID != -1;
        }
        bool _Update()
        {
            return clsLicenseData.UpdateLicense(LicenseID, ApplicationID, DriverID, LicenseClassID, IssueDate, ExpireDate, Notes, PaidFees, IsActive, (byte)IssueReason, CreatedByUserID);
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
        public bool DeActivate()
        {
            return clsLicenseData.DeActivateLicense(this.LicenseID);
        }
        public clsLicense RenewLicense(string Notes,int UserID)
        {
            clsApplication application = new clsApplication();
            application.ApplicantPersonID = DriverInfo.PersonID;
            application.ApplicationTypeID =(int)clsApplication.enApplicationType.RenewDrivingLicense;
            application.ApplicationDate = DateTime.Now;
            application.ApplicationStatus = clsApplication.enApplicationStatus.Completed;
            application.LastStatusDate = DateTime.Now;
            application.CreatedByUserID = UserID;
            application.PaidFees = clsApplicationType.Find((int)clsApplication.enApplicationType.RenewDrivingLicense).ApplicationTypeFees;
            if(!application.Save())
            {
                return null;
            }
           clsLicense NewLicense = new clsLicense();
            NewLicense.ApplicationID = application.ApplicationID;
            NewLicense.CreatedByUserID = UserID;
            NewLicense.DriverID = DriverID;
            NewLicense.IssueDate = DateTime.Now;
            NewLicense.Notes = Notes;
            NewLicense.IssueReason = enIssueReason.Renew;
            NewLicense.ExpireDate = DateTime.Now.AddYears(LicenseClassInfo.DefaultValidityLength);
            NewLicense.IsActive = true;
            NewLicense.PaidFees = LicenseClassInfo.ClassFees;
            NewLicense.LicenseClassID = LicenseClassID;
            NewLicense.LicenseClassInfo = LicenseClassInfo;
            if(!NewLicense.Save())
            {
                return null; 
            }
            DeActivate();
            return NewLicense;
            
        }
        public clsLicense Replace(enIssueReason enIssueReason,int CreatedByUserID)
        {
            clsApplication Application = new clsApplication();
            Application.ApplicantPersonID = DriverInfo.PersonID;
            Application.ApplicationDate = DateTime.Now;
            Application.ApplicationTypeID = enIssueReason == enIssueReason.DamagedReplacement ? (int)clsApplication.enApplicationType.ReplaceDamagedDrivingLicense : (int)clsApplication.enApplicationType.ReplaceLostDrivingLicense;
            Application.CreatedByUserID = CreatedByUserID;
            Application.ApplicationStatus = clsApplication.enApplicationStatus.Completed;
            Application.LastStatusDate = DateTime.Now;
            Application.PaidFees = clsApplicationType.Find(Application.ApplicationTypeID).ApplicationTypeFees;
            if(!Application.Save())
                return null; 
            clsLicense NewLicense = new clsLicense();
            NewLicense.ApplicationID = Application.ApplicationID;
            NewLicense.CreatedByUserID = CreatedByUserID;
            NewLicense.DriverID = DriverID;
            NewLicense.IssueDate = DateTime.Now;
            NewLicense.Notes = Notes;
            NewLicense.IssueReason = enIssueReason;
            NewLicense.ExpireDate = DateTime.Now.AddYears(LicenseClassInfo.DefaultValidityLength);
            NewLicense.IsActive = true;
            NewLicense.PaidFees = LicenseClassInfo.ClassFees;
            NewLicense.LicenseClassID = LicenseClassID;
            NewLicense.LicenseClassInfo = LicenseClassInfo;

            if (!NewLicense.Save())
                return null;
            DeActivate();
            return NewLicense;
        }
        public static bool DeActivateLicense(int LicenseID)
        {
            return clsLicenseData.DeActivateLicense(LicenseID);

        }
        public static bool IsLicenseExistByPersonID(int PersonID,int LicenseClassID)
        {
            return clsLicenseData.GetActiveLicenseIDByPersonID(PersonID, LicenseClassID)!=-1;
        }
        public static DataTable GetAllLicense()
        {
            return clsLicenseData.GetAllLicenses();
        }
        public static DataTable GetDriverLicenses(int DriverID)
        {
            return clsLicenseData.GetDriverLicenses(DriverID);  
        }

        //Detain
        public int Detain(float FineFees,int CreatedByUserID)
        {
            clsDetainedLicense DetainedLicense = new clsDetainedLicense();
            DetainedLicense.FineFees = FineFees;
            DetainedLicense.LicenseID = this.LicenseID;
            DetainedLicense.CreatedByUserID = CreatedByUserID;
            DetainedLicense.DetainDate = DateTime.Now;
            if(!DetainedLicense.Save()) return -1;
            DetainedInfo = DetainedLicense;
            return DetainedLicense.DetainID;

        }
        //ReleaseDetain
        public bool ReleaseDetainedLicense(int ReleasedByUserID, ref int ApplicationID)
        {
            clsApplication ReleaseApplication = new clsApplication();
            ReleaseApplication.ApplicantPersonID=this.DriverInfo.PersonID;
            ReleaseApplication.ApplicationDate = DateTime.Now;
            ReleaseApplication.ApplicationTypeID = (int)clsApplication.enApplicationType.ReleaseDetainedDrivingLicsense;
            ReleaseApplication.CreatedByUserID = ReleasedByUserID;
            ReleaseApplication.ApplicationStatus = clsApplication.enApplicationStatus.Completed;
            ReleaseApplication.LastStatusDate = DateTime.Now;
            ReleaseApplication.PaidFees = clsApplicationType.Find((int)clsApplication.enApplicationType.ReleaseDetainedDrivingLicsense).ApplicationTypeFees;
            if(!ReleaseApplication.Save())
            {
                ApplicationID = -1;
                return false; 
            }
            ApplicationID = ReleaseApplication.ApplicationID;
            return DetainedInfo.ReleaseDetainedLicense(ReleasedByUserID, ApplicationID);
        }
    }
}
