using DVLDDataAccessLayer;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DVLDBusinessLayer
{
    public class clsTestAppointment
    {
        public enum enMode { AddNew,Update}
        public enMode Mode;
        public int TestAppointmentID { get; private set; }
        public clsTestType.enTestType TestTypeID { get; set; }

        public clsTestType TestTypeInfo;
        public int LocalDrivingLicenseApplicationID { get; set; }
        public clsLocalDrivingLicenseApplication LocalDrivingLicenseApplication;
        public DateTime AppointmentDate { get; set; }
        public float PaidFees { get; set; }
        public int CreatedByUserID { get; set; }
        public clsUser CreatedByUser { get; private set; }
        public bool IsLocked { get; set; }
        public int RetakeTestApplicationID { get; set; }
        public clsApplication RetakeTestApplication { get; set; }
        public clsTestAppointment()
        {
            Mode= enMode.AddNew;
            TestAppointmentID = -1;
            TestTypeID = clsTestType.enTestType.VisionTest;
            TestTypeInfo = new clsTestType();
            LocalDrivingLicenseApplicationID = -1;
            LocalDrivingLicenseApplication = new clsLocalDrivingLicenseApplication();
            AppointmentDate = DateTime.Now;
            PaidFees = 0;
            CreatedByUserID = -1;
            CreatedByUser = new clsUser();
            IsLocked = false;
            RetakeTestApplicationID = -1;
            RetakeTestApplication = null;
        }
        private clsTestAppointment(int TestAppointmentID, clsTestType.enTestType TestTypeID,int LocalDrivingLicenseApplicationID, DateTime AppointmentDate, 
            float PaidFees, int CreatedByUserID,bool IsLocked, int RetakeTestApplicationID)
        {
            Mode = enMode.Update;

            this.TestAppointmentID = TestAppointmentID;
            this.TestTypeInfo = clsTestType.Find(TestTypeID);
            this.LocalDrivingLicenseApplicationID = LocalDrivingLicenseApplicationID;
            this.LocalDrivingLicenseApplication =  clsLocalDrivingLicenseApplication.FindByLocalDrivingLicenseApplicationID(LocalDrivingLicenseApplicationID);
            this.AppointmentDate = AppointmentDate;
            this.PaidFees = PaidFees;
            this.CreatedByUserID = CreatedByUserID;
            this.CreatedByUser =  clsUser.FindByUserID(CreatedByUserID);
            this.IsLocked = IsLocked;
            this.RetakeTestApplicationID = RetakeTestApplicationID;
            this.RetakeTestApplication = clsApplication.FindBaseApplication(RetakeTestApplicationID);
        }
        public static clsTestAppointment GetLastTestAppointment(int LocalDrivingLicenseApplicationID, clsTestType.enTestType TestTypeID)
        {
            int TestAppointmentID = -1, CreatedByUserID = -1, RetakeTestApplicationID = -1;
            bool islocked = false;
            float PaidFees = 0;
            DateTime AppointmentDate = DateTime.Now;
            if (clsTestAppointmentData.GetLastTestAppointment(LocalDrivingLicenseApplicationID,(int)TestTypeID, ref TestAppointmentID,ref AppointmentDate,ref PaidFees,ref CreatedByUserID,ref islocked,ref RetakeTestApplicationID))
            {
                return new clsTestAppointment(TestAppointmentID,TestTypeID,LocalDrivingLicenseApplicationID,AppointmentDate,PaidFees,CreatedByUserID,islocked,RetakeTestApplicationID);
            }
            return null;
        }
        public static clsTestAppointment GetTestAppointmentByID(int TestAppointmentID)
        {
            int  CreatedByUserID = -1, RetakeTestApplicationID = -1, LocalDrivingLicenseApplicationID=-1,TestTypeID=-1;
            bool islocked = false;
            float PaidFees = 0;
            DateTime AppointmentDate = DateTime.Now;
            if (clsTestAppointmentData.GetTestAppointmentByID(TestAppointmentID, ref TestTypeID, ref LocalDrivingLicenseApplicationID, ref AppointmentDate, ref PaidFees, ref CreatedByUserID, ref islocked, ref RetakeTestApplicationID))
            {
                return new clsTestAppointment(TestAppointmentID, (clsTestType.enTestType)TestTypeID, LocalDrivingLicenseApplicationID, AppointmentDate, PaidFees, CreatedByUserID, islocked, RetakeTestApplicationID);
            }
            return null; 
        }
        bool _AddNew()
        {
            this.TestAppointmentID = clsTestAppointmentData.AddNewTestAppointment((int)TestTypeID, LocalDrivingLicenseApplicationID, AppointmentDate, PaidFees, CreatedByUserID, IsLocked, RetakeTestApplicationID);
            return TestAppointmentID != -1;
        }
        bool _Update()
        {
            return clsTestAppointmentData.UpdateTestAppointment(TestAppointmentID, (int)TestTypeID, LocalDrivingLicenseApplicationID, AppointmentDate, PaidFees, CreatedByUserID, IsLocked, RetakeTestApplicationID);
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

        public static DataTable GetAllTestAppointments()
        {
            return clsTestAppointmentData.GetAllTestAppointments();
        }
        public static DataTable GetAllTestAppointmentsForTestTypeForApplication(int LocalDrivingLicenseApplicationID, int TestTypeID)
        {
            return clsTestAppointmentData.GetAllTestAppointmentsForTestTypeForApplication(LocalDrivingLicenseApplicationID,  TestTypeID);
        }
        public DataTable GetAllTestAppointment()
        {
            return GetAllTestAppointmentsForTestTypeForApplication(LocalDrivingLicenseApplicationID, (int)TestTypeID);

        }
        public static int GetTestID(int TestAppointmentID)
        {
            return clsTestAppointmentData.GetTestID(TestAppointmentID);
        }
        public int GetTestID()
        {
            return GetTestID(TestAppointmentID);
        }
        public static bool LockTestAppointment(int TestAppointmentID)
        {
            return clsTestAppointmentData.LockTestAppointment(TestAppointmentID);
        }
        public bool LockTestAppointment()
        {
            return LockTestAppointment(TestAppointmentID);
        }
        public static bool IsTestAppintmentLocked(int TestAppointmentID)
        {
            return clsTestAppointmentData.IsTestAppintmentLocked(TestAppointmentID);
        }
        public  bool IsTestAppintmentLocked()
        {
            return IsTestAppintmentLocked(TestAppointmentID);
        }

    }
}
