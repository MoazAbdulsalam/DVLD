using System;
using System.Data;
using DVLDDataAccessLayer;

namespace DVLDBusinessLayer
{
    public class clsUser
    {
        public enum enMode { AddNew = 0, Update = 1 };
        public enMode Mode = enMode.AddNew;

        public int UserID {  get; private set; }
        public int PersonID {  get;  set; }
        public clsPerson PersonInfo { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public bool IsActive {  get; set; }
        public clsUser() 
        {
            UserID = -1;
            PersonID = -1;
            PersonInfo = null;
            UserName = "";
            Password = "";
            IsActive = true;
            Mode = enMode.AddNew;
        }
        private clsUser(int UserID,int PersonID,string UserName,string Password,bool IsActive)
        {
            this.UserID = UserID;
            this.PersonID = PersonID;
            this.UserName = UserName;
            this.Password = Password;
            this.IsActive = IsActive;
            this.PersonInfo = clsPerson.Find(PersonID);
            Mode = enMode.Update;
            
        }
        
        public static clsUser FindByUserID(int UserID)
        {
            int PersonID = -1;
            string UserName = "", Password = "";
            bool IsActive = false;
            if(clsUsersData.GetUserInfoByUserID(UserID,ref PersonID,ref UserName,ref Password,ref IsActive))
            {
                return new clsUser(UserID,PersonID,UserName,Password,IsActive);
            }
            return null;
        }
        public static clsUser FindByPersonID(int PersonID)
        {
            int UserID = -1;
            string UserName = "", Password = "";
            bool IsActive = false;
            if (clsUsersData.GetUserInfoPersonID(ref UserID,  PersonID, ref UserName, ref Password, ref IsActive))
            {
                return new clsUser(UserID, PersonID, UserName, Password, IsActive);
            }
            return null;
        }
        public static clsUser FindByUserNameAndPassword(string UserName,string Password)
        {
            int UserID = -1;
            int PersonID = -1;
            bool IsActive = false;
            if (clsUsersData.GetUserInfoUserNameAndPassword(ref UserID, ref PersonID, UserName,Password,ref IsActive))
            {
                return new clsUser(UserID, PersonID, UserName, Password, IsActive);
            }
            return null;
        }
        private bool _AddNewUser()
        {
            this.UserID = clsUsersData.AddNewUser(PersonID, UserName, Password,IsActive);
            return this.UserID != -1;
        }
        private bool _UpdateUser()
        {
            return clsUsersData.UpdateUser(UserID,PersonID,UserName,Password,IsActive);
        }
        public bool Save()
        {

            switch (Mode)
            {
                case enMode.AddNew:
                    if (_AddNewUser())
                    {

                        Mode = enMode.Update;
                        return true;
                    }
                    else
                    {
                        return false;
                    }

                case enMode.Update:

                    return _UpdateUser();

            }

            return false;
        }
        public bool changePassword(string NewPassword)
        {
            if (Mode == enMode.AddNew)
                return false;
            if(clsUsersData.ChangePassword(this.UserID, NewPassword))
            {
                this.Password = NewPassword;
                return true;
            }
            return false;
            
        }
    
        public static DataTable GetAllUsers()
        {
            return clsUsersData.GetAllUsers(); 
        }
        public static bool DeleteUser(int UserID)
        {
            return clsUsersData.DeleteUser(UserID);
        }
        public static bool IsUserExistByUserID(int UserID)
        {
            return clsUsersData.IsUserExistByUserID(UserID);
        }
        public static bool IsUserExistByUserName(string UserName)
        {
            return clsUsersData.IsUserExistByUserName(UserName);
        }
        public static bool IsUserExistForPersonID(int PersonID)
        {
            return clsUsersData.IsUserExistForPersonID(PersonID);
        }

    }
}
