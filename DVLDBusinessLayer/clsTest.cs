using DVLDDataAccessLayer;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DVLDBusinessLayer
{
    public class clsTest
    {
        public enum enMode { AddNew,Update}
        public enMode Mode;
        public int TestID { get; set; }
        public int TestAppointmentID { get; set; }
        public clsTestAppointment TestAppointmentInfo ;
        public bool TestResult { get; set; }
        public string Notes { get; set; }
        public int CreatedByUserID { get; set; }
        public clsUser CreatedByUserInfo;
        public clsTest()
        {
            Mode = enMode.AddNew;
            TestID = -1;
            TestAppointmentID = -1;
            TestAppointmentInfo = new clsTestAppointment();
            TestResult = false;
            Notes = "";
            CreatedByUserID = -1;
            CreatedByUserInfo = new clsUser();
        }
        private clsTest(int TestID,int TestAppointmentID,bool TestResult,string Notes,int CreatedByUserID)
        {
            Mode = enMode.Update;
            this.TestID = TestID;
            this.TestAppointmentID = TestAppointmentID;
            TestAppointmentInfo = clsTestAppointment.GetTestAppointmentByID(TestAppointmentID);
            this.TestResult = TestResult;
            this.Notes = Notes;
            this.CreatedByUserID = CreatedByUserID;
            CreatedByUserInfo = clsUser.FindByUserID(CreatedByUserID);
        }
        public static clsTest FindByTestID(int TestID)
        {
            int TestAppointmentID = -1,CreatedByUserID=-1;
            bool TestResult = false;
            string Notes = "";
            if(clsTestData.GetTestByTestID(TestID,ref TestAppointmentID,ref TestResult,ref Notes,ref CreatedByUserID))
            {
                return new clsTest(TestID,TestAppointmentID,TestResult,Notes,CreatedByUserID);
            }
            return null;
        }
        public static clsTest FindLastTestPerPersonAndLicenseClass(int PersonID,int LicenseClassID,clsTestType.enTestType TestTypeID)
        {
            int TestAppointmentID = -1, CreatedByUserID = -1,TestID=-1;
            bool TestResult = false;
            string Notes = "";
            if ( clsTestData.GetLastTestByPersonAndTestTypeAndLicenseClass(PersonID,LicenseClassID,(int)TestTypeID,ref TestID,ref TestAppointmentID,ref TestResult,ref Notes,ref CreatedByUserID))
                return new clsTest(TestID, TestAppointmentID, TestResult, Notes, CreatedByUserID);
            return null;
        }
        bool _AddNew()
        {
            this.TestID = clsTestData.AddNewTest(TestAppointmentID,TestResult,Notes,CreatedByUserID);
            return TestID != -1;
        }
        bool _Update()
        {
            return clsTestData.UpdateTest(TestID,TestAppointmentID,TestResult,Notes,CreatedByUserID);
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

        public static byte CountPassedTest(int LocalDrivingLicenseApplicationID)
        {
            return clsTestData.GetPassedTestCount(LocalDrivingLicenseApplicationID);
        }
        public static bool DoesPassedAllTest(int LocalDrivingLicenseApplicationID)
        {
            return CountPassedTest(LocalDrivingLicenseApplicationID) == 3;
        }
        public static DataTable GetAllTests()
        {
            return clsTestData.GetAllTests(); 
        }

    }
}
