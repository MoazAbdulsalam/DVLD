using Shared;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Text;
using System.Threading.Tasks;

namespace DVLDDataAccessLayer
{
    public class clsDetainedLicensesData
    {

        //Add new DetainLicense by License Id
        public static int AddNewDetainedLicense(int LicenseID, DateTime DetainDate ,float FineFees,int CreatedByUserID)
        {
            int DetainID = -1;
            SqlConnection conn = new SqlConnection( clsDataAccessSettings.ConnectionString);
            string query = @"INSERT INTO DetainedLicenses(LicenseID,DetainDate,FineFees,CreatedByUserID,IsReleased)
                             VALUES(@LicenseID,@DetainDate,@FineFees,@CreatedByUserID,0);
                             Select SCOPE_IDENTITY();";
            SqlCommand command = new SqlCommand(query, conn);
            command.Parameters.AddWithValue("@LicenseID", LicenseID); 
            command.Parameters.AddWithValue("@DetainDate", DetainDate);
            command.Parameters.AddWithValue("@FineFees", FineFees);
            command.Parameters.AddWithValue("@CreatedByUserID", CreatedByUserID);
            try
            {
                conn.Open();
                object Result = command.ExecuteScalar();
                if (Result != null && int.TryParse(Result.ToString(),out int ID) )
                {
                    DetainID = ID;
                }
            }
            catch (Exception ex) 
            {
                string Location = "clsDetainedLicensesData → AddNewDetainedLicense";
                clsEventLogger.LogEvent(ex, Location, System.Diagnostics.EventLogEntryType.Error);

            }
            finally
            {
                conn.Close();
            }
            return DetainID;
        }
        // List All Detained Licenses
        public static DataTable GetAllDetainedLicenses()
        {
            SqlConnection conn = new SqlConnection(clsDataAccessSettings.ConnectionString);
            DataTable table = new DataTable();
            string query = " SELECT * From detainedLicenses_View";
            SqlCommand command1 = new SqlCommand(query, conn);
            try
            {
                conn.Open();
                SqlDataReader reader = command1.ExecuteReader();
                if (reader.HasRows)
                {
                    table.Load(reader);
                }
                reader.Close();
            }
            catch (Exception ex)
            {

                string Location = "clsDetainedLicensesData → GetAllDetainedLicenses";
                clsEventLogger.LogEvent(ex, Location, System.Diagnostics.EventLogEntryType.Error);

            }
            finally { conn.Close(); }
            return table;
        }
        //is detained by license id
        public static bool IsLicenseDetained(int LicenseID)
        {
            bool isDetained = false;
            SqlConnection conn = new SqlConnection(clsDataAccessSettings.ConnectionString);
            string query = "Select found =1 from DetainedLicenses Where LicenseID = @LicenseID and IsReleased = 0;";
            SqlCommand command = new SqlCommand( query, conn);
            command.Parameters.AddWithValue("@LicenseID", LicenseID);

            try
            {
                conn.Open();
               object Result = command.ExecuteScalar();
                if (Result != null)
                {
                    isDetained = Convert.ToBoolean(Result);
                }
            }
            catch (Exception ex) {

                string Location = "clsDetainedLicensesData → IsLicenseDetained";
                clsEventLogger.LogEvent(ex, Location, System.Diagnostics.EventLogEntryType.Error);

            }
            finally { conn.Close(); }
            return isDetained;
        }
        //GetDetainedLicenseInfoByID
        public static bool GetDetainedLicenseInfoByID(int DetainID,
           ref int LicenseID, ref DateTime DetainDate,
           ref float FineFees, ref int CreatedByUserID,
           ref bool IsReleased, ref DateTime ReleaseDate,
           ref int ReleasedByUserID, ref int ReleaseApplicationID)
        {
            bool IsFound = false;
            SqlConnection conn = new SqlConnection( clsDataAccessSettings.ConnectionString);
            string query = "Select * From DetainedLicenses Where DetainID =@DetainID  ";
            SqlCommand command =new SqlCommand( query, conn);
            command.Parameters.AddWithValue("@DetainID", DetainID);
            try
            {
                conn.Open();
                SqlDataReader reader = command.ExecuteReader();
                if (reader.Read())
                {
                    IsFound = true;
                    LicenseID = (int)reader["LicenseID"];
                    DetainDate = (DateTime)reader["DetainDate"];
                    FineFees = (float)reader["FineFees"];
                    CreatedByUserID = (int)reader["CreatedByUserID"];
                    IsReleased = Convert.ToBoolean(reader["IsReleased"]);

                    if (reader["ReleaseDate"] == DBNull.Value)
                        ReleaseDate = DateTime.MaxValue;
                    else
                        ReleaseDate = (DateTime)reader["ReleaseDate"];

                    if (reader["ReleasedByUserID"] == DBNull.Value)
                        ReleasedByUserID = -1;
                    else
                        ReleasedByUserID = (int)reader["ReleasedByUserID"];

                    if (reader["ReleaseApplicationID"] == DBNull.Value)
                        ReleaseApplicationID = -1;
                    else
                        ReleaseApplicationID = (int)reader["ReleaseApplicationID"];
                }
                else 
                    IsFound = false;
                reader.Close();
            }
            catch(Exception ex) 
            {
                string Location = "clsDetainedLicensesData → GetDetainedLicenseInfoByID";
                clsEventLogger.LogEvent(ex, Location, System.Diagnostics.EventLogEntryType.Error);

                IsFound = false; 
            }
            finally
            {
                conn.Close();
            }




            return IsFound;
        }
        //GetDetainedLicenseInfoByLicenseID
        public static bool GetDetainedLicenseInfoByLicenseID(int LicenseID,
          ref int DetainID, ref DateTime DetainDate,
          ref float FineFees, ref int CreatedByUserID,
          ref bool IsReleased, ref DateTime ReleaseDate,
          ref int ReleasedByUserID, ref int ReleaseApplicationID)
        {
            bool isFound = false;

            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

            string query = "SELECT top 1 * FROM DetainedLicenses WHERE LicenseID = @LicenseID order by DetainID desc";

            SqlCommand command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue("@LicenseID", LicenseID);

            try
            {
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();

                if (reader.Read())
                {

                    // The record was found
                    isFound = true;

                    DetainID = (int)reader["DetainID"];
                    DetainDate = (DateTime)reader["DetainDate"];
                    FineFees = Convert.ToSingle(reader["FineFees"]);
                    CreatedByUserID = (int)reader["CreatedByUserID"];

                    IsReleased = (bool)reader["IsReleased"];

                    if (reader["ReleaseDate"] == DBNull.Value)

                        ReleaseDate = DateTime.MaxValue;
                    else
                        ReleaseDate = (DateTime)reader["ReleaseDate"];


                    if (reader["ReleasedByUserID"] == DBNull.Value)

                        ReleasedByUserID = -1;
                    else
                        ReleasedByUserID = (int)reader["ReleasedByUserID"];

                    if (reader["ReleaseApplicationID"] == DBNull.Value)

                        ReleaseApplicationID = -1;
                    else
                        ReleaseApplicationID = (int)reader["ReleaseApplicationID"];

                }
                else
                {
                    // The record was not found
                    isFound = false;
                }

                reader.Close();


            }
            catch (Exception ex)
            {

                string Location = "clsDetainedLicensesData → GetDetainedLicenseInfoByLicenseID";
                clsEventLogger.LogEvent(ex, Location, System.Diagnostics.EventLogEntryType.Error);

                isFound = false;
            }
            finally
            {
                connection.Close();
            }

            return isFound;
        }

        //UpdateDetainedLicense
        public static bool UpdateDetainedLicense(int DetainID,
           int LicenseID, DateTime DetainDate,
           float FineFees, int CreatedByUserID)
        {

            int rowsAffected = 0;
            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

            string query = @"UPDATE dbo.DetainedLicenses
                              SET LicenseID = @LicenseID, 
                              DetainDate = @DetainDate, 
                              FineFees = @FineFees,
                              CreatedByUserID = @CreatedByUserID,   
                              WHERE DetainID=@DetainID;";

            SqlCommand command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue("@DetainedLicenseID", DetainID);
            command.Parameters.AddWithValue("@LicenseID", LicenseID);
            command.Parameters.AddWithValue("@DetainDate", DetainDate);
            command.Parameters.AddWithValue("@FineFees", FineFees);
            command.Parameters.AddWithValue("@CreatedByUserID", CreatedByUserID);


            try
            {
                connection.Open();
                rowsAffected = command.ExecuteNonQuery();

            }
            catch (Exception ex)
            {

                string Location = "clsDetainedLicensesData → UpdateDetainedLicense";
                clsEventLogger.LogEvent(ex, Location, System.Diagnostics.EventLogEntryType.Error);

                return false;
            }

            finally
            {
                connection.Close();
            }

            return (rowsAffected > 0);
        }

        //Release
        public static bool ReleaseDetainedLicense(int DetainID,int ReleaseApplicationID,int ReleaseByUserID)
        {
            int rowsAffected = 0;
            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);
            string query = @"UPDATE DetainedLicenses 
                             SET ReleaseApplicationID =@ReleaseApplicationID
                             ,   ReleasedByUserID =@ReleaseByUserID
                             ,   ReleaseDate =@ReleaseDate
                             ,   IsReleased=1;";
            SqlCommand cmd = new SqlCommand(query, connection);
            cmd.Parameters.AddWithValue("@ReleaseApplicationID", ReleaseApplicationID);
            cmd.Parameters.AddWithValue("@ReleaseByUserID", ReleaseByUserID);
            cmd.Parameters.AddWithValue("@ReleaseDate", DateTime.Now);
            try
            {
                connection.Open();
                rowsAffected = cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {

                string Location = "clsDetainedLicensesData → ReleaseDetainedLicense";
                clsEventLogger.LogEvent(ex, Location, System.Diagnostics.EventLogEntryType.Error);

                return false;
            }
            finally
            {
                connection.Close();
            }
            return rowsAffected > 0;
        }
    }
}
