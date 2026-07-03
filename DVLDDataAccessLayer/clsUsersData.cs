using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DVLDDataAccessLayer
{
    public class clsUsersData
    {
        public static bool GetUserInfoByUserID(int UserID, ref int PersonID, ref string UserName, ref string Password, ref bool IsActive)
        {
            bool IsFound = false;
            SqlConnection conn = new SqlConnection(clsDataAccessSettings.ConnectionString);
            string query = "SELECT * FROM Users WHERE UserID=@UserID ";
            SqlCommand command = new SqlCommand(query, conn);
            command.Parameters.AddWithValue("@UserID", UserID);
            try
            {
                conn.Open();
                SqlDataReader reader = command.ExecuteReader();
                if (reader.Read())
                {
                    IsFound = true;
                    PersonID = Convert.ToInt32(reader["PersonID"]);
                    UserName = reader["UserName"].ToString();
                    Password = reader["Password"].ToString();
                    IsActive = Convert.ToBoolean(reader["IsActive"]);
                }
            }
            catch (Exception ex)
            {
                IsFound = false;
            }
            finally
            {
                conn.Close();
            }
            return IsFound;
        }

        public static bool GetUserInfoPersonID(ref int UserID, int PersonID, ref string UserName, ref string Password, ref bool IsActive)
        {
            bool IsFound = false;
            SqlConnection conn = new SqlConnection(clsDataAccessSettings.ConnectionString);
            string query = "SELECT * FROM Users WHERE PersonID=@PersonID ";
            SqlCommand command = new SqlCommand(query, conn);
            command.Parameters.AddWithValue("@PersonID", PersonID);
            try
            {
                conn.Open();
                SqlDataReader reader = command.ExecuteReader();
                if (reader.Read())
                {
                    IsFound = true;
                    UserID = Convert.ToInt32(reader["UserID"]);
                    UserName = reader["UserName"].ToString();
                    Password = reader["Password"].ToString();
                    IsActive = Convert.ToBoolean(reader["IsActive"]);
                }
            }
            catch (Exception ex)
            {
                IsFound = false;
            }
            finally
            {
                conn.Close();
            }
            return IsFound;
        }

        public static bool GetUserInfoUserNameAndPassword(ref int UserID, ref int PersonID,  string UserName,  string Password, ref bool IsActive)
        {
            bool IsFound = false;
            SqlConnection conn = new SqlConnection(clsDataAccessSettings.ConnectionString);
            string query = "SELECT * FROM Users WHERE UserName=@UserName AND Password=@Password ";
            SqlCommand command = new SqlCommand(query, conn);
            command.Parameters.AddWithValue("@UserName", UserName);
            command.Parameters.AddWithValue("@Password", Password);
            try
            {
                conn.Open();
                SqlDataReader reader = command.ExecuteReader();
                if (reader.Read())
                {
                    IsFound = true;
                    UserID = Convert.ToInt32(reader["UserID"]);
                    PersonID = Convert.ToInt32(reader["PersonID"]);
                    IsActive = Convert.ToBoolean(reader["IsActive"]);
                }
            }
            catch (Exception ex)
            {
                IsFound = false;
            }
            finally
            {
                conn.Close();
            }
            return IsFound;
        }

        public static int AddNewUser(int PersonID, string UserName, string Password, bool IsActive)
        {
            int UserID = -1;
            SqlConnection conn = new SqlConnection(clsDataAccessSettings.ConnectionString);
            string query = @"INSERT INTO Users
                           (PersonID
                           ,UserName
                           ,Password
                           ,IsActive)
                     VALUES
                      (@PersonID,@UserName,@Password,@IsActive) 
                      SELECT SCOPE_IDENTITY();";
            SqlCommand command = new SqlCommand(query, conn);
            command.Parameters.AddWithValue("@PersonID", PersonID);
            command.Parameters.AddWithValue("@UserName", UserName);
            command.Parameters.AddWithValue("@Password", Password);
            command.Parameters.AddWithValue("@IsActive", IsActive);
            try
            {
                conn.Open();
                object result = command.ExecuteScalar();
                if (result != null && int.TryParse(result.ToString(), out int InsertedID))
                {
                    UserID = InsertedID;
                }
            }
            catch (Exception ex)
            {

            }
            finally
            {
                conn.Close(); 
            }
            return UserID;
        }

        public static bool UpdateUser(int UserID,int PersonID, string UserName, string Password, bool IsActive)
        {
            int rowsAffected = 0;
            SqlConnection conn = new SqlConnection(clsDataAccessSettings.ConnectionString);

            string query = @"UPDATE Users
                              SET PersonID = @PersonID, 
                                 UserName = @UserName,
                                 Password = @Password,
                                 IsActive = @IsActive
                            WHERE UserID =@UserID ";
            SqlCommand command = new SqlCommand(query, conn);
            command.Parameters.AddWithValue("@PersonID", PersonID);
            command.Parameters.AddWithValue("@UserName", UserName);
            command.Parameters.AddWithValue("@Password", Password);
            command.Parameters.AddWithValue("@IsActive", IsActive);
            command.Parameters.AddWithValue("@UserID", UserID);
            try
            {
                conn.Open();
                rowsAffected = command.ExecuteNonQuery();
                
            }
            catch (Exception ex)
            {
                return false;
            }
            finally
            {
                conn.Close();
            }
            return rowsAffected>0;
        }

        public static DataTable GetAllUsers()
        {
            DataTable dt = new DataTable();
            SqlConnection conn = new SqlConnection(clsDataAccessSettings.ConnectionString);
            string query = @"SELECT Users.UserID, Users.PersonID, FullName =People.FirstName +' '+ People.SecondName+' '+ ISNULL( People.ThirdName,'')+' '+ People.LastName, Users.UserName, Users.IsActive
                           FROM     People INNER JOIN
                            Users ON People.PersonID = Users.PersonID";
            SqlCommand command = new SqlCommand(query,conn);
            try
            {
                conn.Open();
                SqlDataReader reader = command.ExecuteReader();
                if (reader.HasRows)
                {
                    dt.Load(reader);
                }
                reader.Close();

            }
            catch (Exception ex)
            {
                return dt;
            }
            finally
            {
                conn.Close();
            }



            return dt;
        }
        public static bool  DeleteUser(int userID)
        {
            int rowsAffected = 0;
            SqlConnection conn = new SqlConnection(clsDataAccessSettings.ConnectionString);
            string query = "DELETE Users WHERE userID=@userID";
            SqlCommand command = new SqlCommand(query, conn);
            command.Parameters.AddWithValue("@userID", userID);

            try
            {
                conn.Open();
                rowsAffected = command.ExecuteNonQuery();

            }
            catch (Exception ex)
            {
                return false;
            }
            finally
            {
                conn.Close();
            }
            return rowsAffected > 0;
        }
        public static bool IsUserExistByUserID(int UserID)
        {
            bool found = false;
            SqlConnection conn = new SqlConnection(clsDataAccessSettings.ConnectionString);

            string query = "Select found=1 from Users WHERE UserID=@UserID";
            SqlCommand command = new SqlCommand(query, conn);
            command.Parameters.AddWithValue("@UserID", UserID);
            try
            {
                conn.Open();
                SqlDataReader reader = command.ExecuteReader();
                found = reader.HasRows;
                reader.Close();
            }
            catch (Exception ex)
            {
                return false;
            }
            finally
            { conn.Close(); }
            return found;
        }
        public static bool IsUserExistByUserName(string UserName)
        {
            bool found = false;
            SqlConnection conn = new SqlConnection(clsDataAccessSettings.ConnectionString);

            string query = "Select found=1 from Users WHERE UserName=@UserName";
            SqlCommand command = new SqlCommand(query, conn);
            command.Parameters.AddWithValue("@UserName", UserName);
            try
            {
                conn.Open();
                SqlDataReader reader = command.ExecuteReader();
                found = reader.HasRows;
                reader.Close();
            }
            catch (Exception ex)
            {
                return false;
            }
            finally
            { conn.Close(); }
            return found;
        }
        public static bool IsUserExistForPersonID(int PersonID)
        {
            bool found = false;
            SqlConnection conn = new SqlConnection(clsDataAccessSettings.ConnectionString);

            string query = "Select found=1 from Users WHERE PersonID=@PersonID";
            SqlCommand command = new SqlCommand(query, conn);
            command.Parameters.AddWithValue("@PersonID", PersonID);
            try
            {
                conn.Open();
                SqlDataReader reader = command.ExecuteReader();
                found = reader.HasRows;
                reader.Close();
            }
            catch (Exception ex)
            {
                return false;
            }
            finally
            { conn.Close(); }
            return found;
        }


        public static bool ChangePassword(int UserID, string NewPassword)
        {
            int rowsAffected = 0;
            SqlConnection conn = new SqlConnection(clsDataAccessSettings.ConnectionString);

            string query = @"UPDATE Users
                              SET  
                                 Password = @NewPassword
                            WHERE UserID =@UserID ;";
            SqlCommand command = new SqlCommand(query, conn);
            command.Parameters.AddWithValue("@NewPassword", NewPassword);
            command.Parameters.AddWithValue("@UserID", UserID);
            try
            {
                conn.Open();
                rowsAffected = command.ExecuteNonQuery();

            }
            catch (Exception ex)
            {
                return false;
            }
            finally
            {
                conn.Close();
            }
            return rowsAffected > 0;
        }

    }
}
