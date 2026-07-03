using DVLDDataAccessLayer;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DVLDBusinessLayer
{
    public class clsLocalDrivingLicenseApplication : clsApplication
    {
        public enum enMode { AddNew = 0, Update = 1 };
        public enMode Mode { get; set; }
        public int LocalDrivingLicenseApplicationID { get; set; }
        public int LicenseClassID { get; set; }
        public clsLicenseClass LicenseClassInfo { get; set; }

        public clsLocalDrivingLicenseApplication()
        {
            LocalDrivingLicenseApplicationID = -1;
            LicenseClassID = -1;
            Mode = enMode.AddNew;
        }
        private clsLocalDrivingLicenseApplication(int LocalDrivingLicenseApplicationID,int ApplicationID,int LicenseClassID, int ApplicantPersonID, DateTime ApplicationDate, int ApplicationTypeID,
             enApplicationStatus ApplicationStatus, DateTime LastStatusDate, float PaidFees, int CreatedByUserID)
        {
            Mode = enMode.Update;
            this.LocalDrivingLicenseApplicationID= LocalDrivingLicenseApplicationID;
            this.LicenseClassID= LicenseClassID;
            this.ApplicationID = ApplicationID;
            this.ApplicantPersonID = ApplicantPersonID;
            this.ApplicationDate = ApplicationDate;
            this.ApplicationTypeID = ApplicationTypeID;
            this.LastStatusDate = LastStatusDate;
            this.PaidFees = PaidFees;
            this.CreatedByUserID = CreatedByUserID;
            this.ApplicationStatus = ApplicationStatus;
            this.ApplicationTypeInfo = clsApplicationType.Find(ApplicationTypeID);
            this.CreatedByUserInfo = clsUser.FindByUserID(CreatedByUserID);
            this.PersonInfo = clsPerson.Find(ApplicantPersonID);
            this.LicenseClassInfo = clsLicenseClass.Find(LicenseClassID);
        }
        bool _AddNew()
        {
            this.LocalDrivingLicenseApplicationID = clsLocalDrivingLicenseApplicationData.AddNewLocalDrivingLicenseApplication(ApplicationID, LicenseClassID);
            return this.LocalDrivingLicenseApplicationID != -1;
        }
        bool _Update()
        {
            return clsLocalDrivingLicenseApplicationData.UpdateLocalDrivingLicenseApplication(LocalDrivingLicenseApplicationID, ApplicationID, LicenseClassID);
        }
        public  new bool Save()
        {
            base.Mode = (clsApplication.enMode)Mode;
            if(!base.Save()) return false;

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
        public new  bool Delete()
        {
            if(clsLocalDrivingLicenseApplicationData.DeleteLocalDrivingLicenseApplication(LocalDrivingLicenseApplicationID))
            {
               if( base.Delete())
                 return true;
            }
            return false;
        }
        public static bool DeleteLocalDrivingLicenseApplication( int LocalDrivingLicenseApplicationID)
        {
            return clsLocalDrivingLicenseApplicationData.DeleteLocalDrivingLicenseApplication(LocalDrivingLicenseApplicationID);
        }
        public static clsLocalDrivingLicenseApplication FindByLocalDrivingLicenseApplicationID(int LocalDrivingLicenseApplicationID)
        {
            int ApplicationID = -1;
            int LicenseClassID = -1;
            if(clsLocalDrivingLicenseApplicationData.GetLocalDrivingLicenseApplicationInfoByID(LocalDrivingLicenseApplicationID ,ref ApplicationID ,ref LicenseClassID))
            {
                clsApplication Application = clsApplication.FindBaseApplication(ApplicationID);
                return new clsLocalDrivingLicenseApplication(LocalDrivingLicenseApplicationID, ApplicationID, LicenseClassID, Application.ApplicantPersonID, Application.ApplicationDate, Application.ApplicationTypeID, Application.ApplicationStatus, Application.LastStatusDate, Application.PaidFees, Application.CreatedByUserID);
            }
            return null;

        }
        public static clsLocalDrivingLicenseApplication FindByApplicationID(int ApplicationID)
        {
            int LocalDrivingLicenseApplicationID = -1;
            int LicenseClassID = -1;
            if (clsLocalDrivingLicenseApplicationData.GetLocalDrivingLicenseApplicationInfoByApplicationID(ApplicationID, ref LocalDrivingLicenseApplicationID, ref LicenseClassID))
            {
                clsApplication Application = clsApplication.FindBaseApplication(ApplicationID);
                return new clsLocalDrivingLicenseApplication(LocalDrivingLicenseApplicationID, ApplicationID, LicenseClassID, Application.ApplicantPersonID, Application.ApplicationDate, Application.ApplicationTypeID, Application.ApplicationStatus, Application.LastStatusDate, Application.PaidFees, Application.CreatedByUserID);
            }
            return null;

        }
        public static DataTable GetAllLocalDrivingLicenseApplications()
        {
            return clsLocalDrivingLicenseApplicationData.GetAllLocalDrivingLicenseApplications();
        }

        public static bool DoesPassTest(int LocalDrivingLicenseApplicationID,clsTestType.enTestType TestType)
        {
            return clsLocalDrivingLicenseApplicationData.DoesPassTest(LocalDrivingLicenseApplicationID, (int)TestType);
        }
        public bool DoesPassTest(clsTestType.enTestType TestType)
        {
            return clsLocalDrivingLicenseApplication.DoesPassTest(this.LocalDrivingLicenseApplicationID, TestType);
        }
        public static bool DoesAttendTest(int LocalDrivingLicenseApplicationID, clsTestType.enTestType TestType)
        {
            return clsLocalDrivingLicenseApplicationData.DoesAttendTest(LocalDrivingLicenseApplicationID, (int)TestType);
        }
        public bool DoesAttendTest(clsTestType.enTestType TestType)
        {
            return clsLocalDrivingLicenseApplication.DoesAttendTest(this.LocalDrivingLicenseApplicationID, TestType);
        }

        public byte TotalTrialsPerTest(clsTestType.enTestType TestTypeID)
        {
            return clsLocalDrivingLicenseApplicationData.TotalTrialsPerTest(this.LocalDrivingLicenseApplicationID, (int)TestTypeID);
        }

        public static byte TotalTrialsPerTest(int LocalDrivingLicenseApplicationID, clsTestType.enTestType TestTypeID)

        {
            return clsLocalDrivingLicenseApplicationData.TotalTrialsPerTest(LocalDrivingLicenseApplicationID, (int)TestTypeID);
        }
        public static bool IsThereAnActiveScheduledTest(int LocalDrivingLicenseApplicationID, clsTestType.enTestType TestType)
        {
            return clsLocalDrivingLicenseApplicationData.IsThereAnActiveScheduledTest(LocalDrivingLicenseApplicationID, (int)TestType);
        }
        public bool IsThereAnActiveScheduledTest(clsTestType.enTestType TestType)
        {
            return clsLocalDrivingLicenseApplication.IsThereAnActiveScheduledTest(this.LocalDrivingLicenseApplicationID, TestType);
        }
        public  static clsTest GetLastTestPerTestType(int PersonID,int LicenseClassID,clsTestType.enTestType TestTypeID)
        {
            return clsTest.FindLastTestPerPersonAndLicenseClass(PersonID, LicenseClassID, TestTypeID);

        }
        public clsTest GetLastTestPerTestType(clsTestType.enTestType TestTypeID)
        {
            return GetLastTestPerTestType(this.ApplicantPersonID,this.LicenseClassID, TestTypeID);
        }
        public static byte GetPassedTestsCount(int LocalDrivingLicenseApplicationID)
        {
            return clsTest.CountPassedTest(LocalDrivingLicenseApplicationID);
        }
        public byte GetPassedTestCount()
        {
            return GetPassedTestsCount(this.LocalDrivingLicenseApplicationID);
        }
        public static bool DoesPassedAllTests(int LocalDrivingLicenseApplicationID)
        {
            return clsTest.DoesPassedAllTest(LocalDrivingLicenseApplicationID);
        }
        public bool DoesPassedAllTests()
        {
            return DoesPassedAllTests(this.LocalDrivingLicenseApplicationID);
        }
        public int IssueLicenseForTheFirstTime(string Notes,int CreatedByUserID)
        {
            int DriverID = -1;
            clsDriver Driver = clsDriver.FindDriverByPersonID(ApplicantPersonID);
            if(Driver == null)
            {
                Driver = new clsDriver();
                Driver.Mode = clsDriver.enMode.AddNew;
                Driver.PersonID = ApplicantPersonID;
                Driver.CreatedByUser=CreatedByUserID;
                Driver.CreatedDate = DateTime.Now;
                if(Driver.Save())
                {
                    DriverID = Driver.DriverID;

                }
                else
                {
                    return -1;
                    
                }
            }
            DriverID = Driver.DriverID;

            clsLicense license = new clsLicense();
            license.DriverID = DriverID;
            license.DriverInfo = Driver;
            license.ApplicationID = this.ApplicationID;
            license.IssueReason = clsLicense.enIssueReason.FirstTime;
            license.IssueDate = DateTime.Now;
            license.ExpireDate = DateTime.Now.AddYears(this.LicenseClassInfo.DefaultValidityLength);
            license.Notes = Notes;
            license.PaidFees = this.LicenseClassInfo.ClassFees;
            license.CreatedByUserID = CreatedByUserID;
            license.IsActive = true;
            
            license.LicenseClassID = this.LicenseClassID;
            license.LicenseClassInfo = this.LicenseClassInfo;
            if (license.Save())
            {
                SetComplete();
                return license.LicenseID;
            }
            else
                return -1;
        }
        public bool IsLicenseIssued()
        {
            return GetActiveLicenseID() != -1;
        }
        public int GetActiveLicenseID()
        {
            return clsLicenseData.GetActiveLicenseIDByPersonID(this.ApplicantPersonID, this.LicenseClassID);
        }

    }
}
