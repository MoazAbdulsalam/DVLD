using DVLDDataAccessLayer;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace DVLDBusinessLayer
{
    public class clsApplicationType
    {
        public enum enMode { AddNew,Update}
        public enMode Mode;
        public int ApplicationTypeID { get; private set; }
        public string ApplicationTypeName { get;  set; }
        public float ApplicationTypeFees { get;  set; }
        public clsApplicationType()
        {
            Mode = enMode.AddNew;
            ApplicationTypeID = -1;
            ApplicationTypeName = "";
            ApplicationTypeFees = 0f;
        }
        private clsApplicationType(int ApplicationTypeID,string ApplicationTypeName, float ApplicationTypeFees)
        {
            Mode = enMode.Update;
            this.ApplicationTypeID = ApplicationTypeID;
            this.ApplicationTypeName = ApplicationTypeName;
            this.ApplicationTypeFees = ApplicationTypeFees;
        }
        private bool _AddNew()
        {
            this.ApplicationTypeID = clsApplicationTypesData.AddNewApplication(this.ApplicationTypeName, this.ApplicationTypeFees);
            return this.ApplicationTypeID!=-1;
        }
        private bool _Update()
        {
            return clsApplicationTypesData.UpdateAplicationTypes(this.ApplicationTypeID,this.ApplicationTypeName,this.ApplicationTypeFees);
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

        public static clsApplicationType Find(int ApplicationTypeID)
        {
            
            string ApplicationTypeName = "";
            float ApplicationTypeFees = 0f;
            if(clsApplicationTypesData.GetApplicationByID(ApplicationTypeID,ref ApplicationTypeName, ref ApplicationTypeFees))
            {
                return new clsApplicationType(ApplicationTypeID, ApplicationTypeName, ApplicationTypeFees);
            }
            return null;
        }
        public static DataTable GetAllAplications()
        {
            return clsApplicationTypesData.GetAllApplecations();
        }
    }
}
