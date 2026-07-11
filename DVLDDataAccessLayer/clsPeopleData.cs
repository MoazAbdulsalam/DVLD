using Shared;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace DVLDDataAccessLayer
{
    public class clsPeopleData
    {
        public static bool GetPersonInfoById(int PersonID ,ref string NationalNo,
            ref string FirstName, ref string SecondName, ref string ThirdName,
            ref string LastName,ref DateTime DateOfBirth, ref short Gendor,
            ref string Address, ref string Phone, ref string Email, 
            ref int NationalityCountryID, ref string ImagePath)
        {
            bool IsFound = false;
            SqlConnection conn = new SqlConnection(clsDataAccessSettings.ConnectionString);
            string query = "SELECT * FROM People WHERE PersonID = @PersonID ;";
            SqlCommand command = new SqlCommand(query, conn);
            command.Parameters.AddWithValue("@PersonID", PersonID);
            try
            {
                conn.Open();
                SqlDataReader Reader = command.ExecuteReader();
                if(Reader.Read())
                {
                    IsFound = true;

                    NationalNo = Reader["NationalNo"].ToString();
                    FirstName = Reader["FirstName"].ToString();
                    SecondName = Reader["SecondName"].ToString();
                    ThirdName = Reader["ThirdName"] == DBNull.Value? "":Reader["ThirdName"].ToString();
                    LastName = Reader["LastName"].ToString();

                    DateOfBirth = (DateTime)Reader["DateOfBirth"];
                    Gendor = Convert.ToInt16(Reader["Gendor"]);
                    Address = Reader["Address"].ToString();
                    Phone = Reader["Phone"].ToString();
                    Email = Reader["Email"] == DBNull.Value ? "" : Reader["Email"].ToString();

                    NationalityCountryID = (int)Reader["NationalityCountryID"];
                    ImagePath = Reader["ImagePath"] == DBNull.Value ? "" : Reader["ImagePath"].ToString();

                }
                else
                {
                    IsFound = false; 
                }
                    Reader.Close();
            }
            catch(Exception ex)
            {

                string Location = "clsPeopleData → GetPersonInfoById";
                clsEventLogger.LogEvent(ex, Location, System.Diagnostics.EventLogEntryType.Error);

                IsFound = false;
            }
            finally
            {
                conn.Close();
            }
            return IsFound;
        }


        public static bool GetPersonInfoByNationalNo(ref int PersonID, string NationalNo,
            ref string FirstName, ref string SecondName, ref string ThirdName,
            ref string LastName, ref DateTime DateOfBirth, ref short Gendor,
            ref string Address, ref string Phone, ref string Email,
            ref int NationalityCountryID, ref string ImagePath)
        {
            bool IsFound = false;
            SqlConnection conn = new SqlConnection(clsDataAccessSettings.ConnectionString);
            string query = "SELECT * FROM People WHERE NationalNo = @NationalNo ;";
            SqlCommand command = new SqlCommand(query, conn);
            command.Parameters.AddWithValue("@NationalNo", NationalNo);
            try
            {
                conn.Open();
                SqlDataReader Reader = command.ExecuteReader();
                if (Reader.Read())
                {
                    IsFound = true;

                    PersonID = (int)Reader["PersonID"];
                    FirstName = Reader["FirstName"].ToString();
                    SecondName = Reader["SecondName"].ToString();
                    ThirdName = Reader["ThirdName"] == DBNull.Value ? "" : Reader["ThirdName"].ToString();
                    LastName = Reader["LastName"].ToString();

                    DateOfBirth = (DateTime)Reader["DateOfBirth"];
                    Gendor = Convert.ToInt16(Reader["Gendor"]);

                    Address = Reader["Address"].ToString();
                    Phone = Reader["Phone"].ToString();
                    Email = Reader["Email"] == DBNull.Value ? "" : Reader["Email"].ToString();

                    NationalityCountryID = (int)Reader["NationalityCountryID"];
                    ImagePath = Reader["ImagePath"] == DBNull.Value ? "" : Reader["ImagePath"].ToString();

                }
                Reader.Close();
            }
            catch (Exception ex)
            {

                string Location = "clsPeopleData → GetPersonInfoByNationalNo";
                clsEventLogger.LogEvent(ex, Location, System.Diagnostics.EventLogEntryType.Error);

                IsFound = false;
            }
            finally
            {
                conn.Close();
            }
            return IsFound;
        }

        public static int AddnewPerson(string NationalNo,
            string FirstName,  string SecondName, string ThirdName,
            string LastName,  DateTime DateOfBirth,short Gendor,
            string Address,  string Phone,  string Email,
            int NationalityCountryID,  string ImagePath)
        {
            int PersonID = -1;

            SqlConnection conn = new SqlConnection(clsDataAccessSettings.ConnectionString);

            string query = @"INSERT INTO People (NationalNo,FirstName,SecondName,ThirdName,LastName,DateOfBirth,Gendor,Address,Phone,Email,NationalityCountryID,ImagePath)
                          VALUES (@NationalNo,@FirstName,@SecondName,@ThirdName,@LastName,@DateOfBirth,@Gendor,@Address,@Phone,@Email,@NationalityCountryID,@ImagePath);
                          SELECT SCOPE_IDENTITY();";
            SqlCommand command = new SqlCommand(query, conn);

            command.Parameters.AddWithValue("@NationalNo", NationalNo); 
            command.Parameters.AddWithValue("@FirstName", FirstName); 
            command.Parameters.AddWithValue("@SecondName", SecondName); 
            command.Parameters.AddWithValue("@LastName", LastName); 
            command.Parameters.AddWithValue("@DateOfBirth", DateOfBirth); 
            command.Parameters.AddWithValue("@Gendor", Gendor); 
            command.Parameters.AddWithValue("@Address", Address); 
            command.Parameters.AddWithValue("@Phone", Phone);
            command.Parameters.AddWithValue("@NationalityCountryID", NationalityCountryID);
            
            if(ThirdName != null && ThirdName != "")
                 command.Parameters.AddWithValue("@ThirdName", ThirdName);
            else
                command.Parameters.AddWithValue("@ThirdName", DBNull.Value);

            if (Email != null && Email != "")
                command.Parameters.AddWithValue("@Email", Email); 
            else
                command.Parameters.AddWithValue("@Email", DBNull.Value);

            if(ImagePath!=null && ImagePath!= "")
                 command.Parameters.AddWithValue("@ImagePath", ImagePath);
            else
                command.Parameters.AddWithValue("@ImagePath", DBNull.Value);


            try
            {
                conn.Open();
                object result = command.ExecuteScalar();
                if(result != null && int.TryParse(result.ToString(),out int InsertedID))
                {
                    PersonID = InsertedID;
                }
            }
            catch(Exception ex)
            {

                string Location = "clsPeopleData → AddnewPerson";
                clsEventLogger.LogEvent(ex, Location, System.Diagnostics.EventLogEntryType.Error);

            }
            finally
            {
                conn.Close();
            }

            return PersonID;

        }


        public static bool UpdatePerson(int PersonID,string NationalNo,
          string FirstName, string SecondName, string ThirdName,
          string LastName, DateTime DateOfBirth, short Gendor,
          string Address, string Phone, string Email,
          int NationalityCountryID, string ImagePath)
        {
            int rowsAffected =0;
            SqlConnection conn = new SqlConnection(clsDataAccessSettings.ConnectionString);
            string query = @"UPDATE People
                              SET NationalNo = @NationalNo, 
                                 FirstName = @FirstName,
                                 SecondName = @SecondName,
                                 ThirdName = @ThirdName, 
                                 LastName = @LastName, 
                                 DateOfBirth = @DateOfBirth, 
                                 Gendor = @Gendor,
                                 Address = @Address, 
                                 Phone = @Phone, 
                                 Email = @Email, 
                                 NationalityCountryID = @NationalityCountryID,
                                 ImagePath = @ImagePath
                            WHERE PersonID =@PersonID ;";
            SqlCommand command = new SqlCommand(query, conn);
            command.Parameters.AddWithValue("@PersonID", PersonID);

            command.Parameters.AddWithValue("@NationalNo", NationalNo);
            command.Parameters.AddWithValue("@FirstName", FirstName);
            command.Parameters.AddWithValue("@SecondName", SecondName);
            command.Parameters.AddWithValue("@LastName", LastName);
            command.Parameters.AddWithValue("@DateOfBirth", DateOfBirth);
            command.Parameters.AddWithValue("@Gendor", Gendor);
            command.Parameters.AddWithValue("@Address", Address);
            command.Parameters.AddWithValue("@Phone", Phone);        
            command.Parameters.AddWithValue("@NationalityCountryID", NationalityCountryID);

            if (ThirdName != null&& ThirdName != "")
                command.Parameters.AddWithValue("@ThirdName", ThirdName);
            else
                command.Parameters.AddWithValue("@ThirdName", DBNull.Value);

            if (Email != null && Email != "")
                command.Parameters.AddWithValue("@Email", Email);
            else
                command.Parameters.AddWithValue("@Email", DBNull.Value);

            if (ImagePath != null && ImagePath != "")
                command.Parameters.AddWithValue("@ImagePath", ImagePath);
            else
                command.Parameters.AddWithValue("@ImagePath", DBNull.Value);

            try
            {
                conn.Open();
                rowsAffected = command.ExecuteNonQuery();
            }
            catch( Exception ex)
            {

                string Location = "clsPeopleData → UpdatePerson";
                clsEventLogger.LogEvent(ex, Location, System.Diagnostics.EventLogEntryType.Error);

                return false;

            }
            finally
            {
                conn.Close(); 
            }


            return rowsAffected > 0;

        }

        public static DataTable GetAllPeople()
        {
            DataTable dt = new DataTable();
            SqlConnection conn = new SqlConnection(clsDataAccessSettings.ConnectionString);
            string query = @" SELECT People.PersonID, People.NationalNo, People.FirstName, People.SecondName, People.ThirdName, People.LastName, People.DateOfBirth, 
	                   People.Gendor,
	                   CASE
	                   When People.Gendor=0 THEN 'Male'
	                   ELSE 'Female'
	                   End as GendorCaption,
	                   People.Address, People.Phone, People.Email, People.NationalityCountryID, 
                                  Countries.CountryName, People.ImagePath
                       FROM  People INNER JOIN
                                  Countries ON People.NationalityCountryID = Countries.CountryID  ";
            SqlCommand command = new SqlCommand(query, conn);
            try
            {
                conn.Open();
                SqlDataReader reader = command.ExecuteReader();
                if(reader.HasRows)
                {
                    dt.Load(reader);
                }
                reader.Close();

            }
            catch ( Exception ex )
            {

                string Location = "clsPeopleData → GetAllPeople";
                clsEventLogger.LogEvent(ex, Location, System.Diagnostics.EventLogEntryType.Error);

                return dt; 
            }
            finally
            { 
                conn.Close(); 
            }
            return dt;

        }
        public static bool DeletePeron(int PersonID)
        {
            int rowsAffected =0;
            SqlConnection conn = new SqlConnection(clsDataAccessSettings.ConnectionString);
            string query = "DELETE People WHERE PersonID=@PersonID";
            SqlCommand command = new SqlCommand(query, conn);
            command.Parameters.AddWithValue("@PersonID", PersonID);

            try
            {
                conn.Open();
                rowsAffected = command.ExecuteNonQuery();

            }
            catch ( Exception ex )
            {

                string Location = "clsPeopleData → DeletePeron";
                clsEventLogger.LogEvent(ex, Location, System.Diagnostics.EventLogEntryType.Error);

                return false;
            }
            finally
            {
                conn.Close(); 
            }
            return rowsAffected > 0;

        }
        public static bool IsPersonExist(string NationalNo)
        {
            bool found = false;
            SqlConnection conn = new SqlConnection(clsDataAccessSettings.ConnectionString);

            string query = "Select found=1 from People WHERE NationalNo=@NationalNo";
            SqlCommand command = new SqlCommand(query, conn);
            command.Parameters.AddWithValue("@NationalNo", NationalNo);
            try
            {
                conn.Open();
                SqlDataReader reader = command.ExecuteReader();
                found = reader.HasRows;
                reader.Close();
            }
            catch( Exception ex )
            {

                string Location = "clsPeopleData → IsPersonExist(nationalNo)";
                clsEventLogger.LogEvent(ex, Location, System.Diagnostics.EventLogEntryType.Error);

                return false;
            }
            finally
            { conn.Close(); }
            return found;
        }
        public static bool IsPersonExist(int PersonID)
        {
            bool found = false;
            SqlConnection conn = new SqlConnection(clsDataAccessSettings.ConnectionString);

            string query = "Select found=1 from People WHERE PersonID=@PersonID";
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

                string Location = "clsPeopleData → IsPersonExist(PersonID)";
                clsEventLogger.LogEvent(ex, Location, System.Diagnostics.EventLogEntryType.Error);

                return false;
            }
            finally
            { conn.Close(); }
            return found;
        }
    }
}
