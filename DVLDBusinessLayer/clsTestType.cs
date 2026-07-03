using DVLDDataAccessLayer;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DVLDBusinessLayer
{
    public class clsTestType
    {
        public enum enMode { AddNew, Update }
        public enMode Mode;
        public enum enTestType { VisionTest =1,WrittenTest =2,StreetTest=3}

        public enTestType TestTypeID { get; private set; }
        public string TestTypeName { get; set; }
        public string TestTypeDescription { get; set; }
        public float TestTypeFees { get; set; }
        public clsTestType()
        {
            Mode = enMode.AddNew;
            TestTypeID = enTestType.VisionTest;
            TestTypeName = "";
            TestTypeDescription = "";
            TestTypeFees = 0f;
        }
        private clsTestType(enTestType TestTypeID, string TestTypeName,string TestTypeDescription, float TestTypeFees)
        {
            Mode = enMode.Update;
            this.TestTypeID = TestTypeID;
            this.TestTypeName = TestTypeName;
            this.TestTypeDescription = TestTypeDescription;
            this.TestTypeFees = TestTypeFees;
        }
        private bool _AddNew()
        {
            this.TestTypeID =(enTestType) clsTestTypesData.AddNewTestType(this.TestTypeName,this.TestTypeDescription, this.TestTypeFees);
            return this.TestTypeName != "";
        }
        private bool _Update()
        {
            return clsTestTypesData.UpdateTestTypes((int)this.TestTypeID, this.TestTypeName, this.TestTypeDescription, this.TestTypeFees);
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

        public static clsTestType Find(enTestType TestTypeID)
        {

            string TestTypeName = "";
            string TestTypeDescription = "";
            float TestTypeFees = 0f;
            if (clsTestTypesData.GetTestTypeByID((int)TestTypeID, ref TestTypeName,ref TestTypeDescription, ref TestTypeFees))
            {
                return new clsTestType(TestTypeID, TestTypeName, TestTypeDescription, TestTypeFees);
            }
            return null;
        }
        public static DataTable GetAllAplications()
        {
            return clsTestTypesData.GetAllTestTypes();
        }
    }
}
