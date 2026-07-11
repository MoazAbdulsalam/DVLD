using Shared;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DVLDDataAccessLayer
{
    public class clsTestTypesData
    {
        public static bool GetTestTypeByID(int TestTypeID, ref string TestTypeTitle,ref string TestTypeDescription, ref float TestTypeFees)
        {
            bool IsFound = false;
            SqlConnection conn = new SqlConnection(clsDataAccessSettings.ConnectionString);
            string query = "SELECT * FROM TestTypes WHERE TestTypeID = @TestTypeID ;";
            SqlCommand command = new SqlCommand(query, conn);
            command.Parameters.AddWithValue("@TestTypeID", TestTypeID);
            try
            {
                conn.Open();
                SqlDataReader Reader = command.ExecuteReader();
                if (Reader.Read())
                {
                    IsFound = true;

                    TestTypeTitle = Reader["TestTypeTitle"].ToString();
                    TestTypeDescription = Reader["TestTypeDescription"].ToString();
                    TestTypeFees = Convert.ToSingle(Reader["TestTypeFees"]);


                }
                else
                {
                    IsFound = false;
                }
                Reader.Close();
            }
            catch (Exception ex)
            {

                string Location = "clsTestTypesData → GetTestTypeByID";
                clsEventLogger.LogEvent(ex, Location, System.Diagnostics.EventLogEntryType.Error);

                IsFound = false;
            }
            finally
            {
                conn.Close();
            }
            return IsFound;
        }
        public static bool UpdateTestTypes(int TestTypeID, string TestTypeTitle, string TestTypeDescription, float TestTypeFees)
        {
            int rowsAffected = 0;
            SqlConnection conn = new SqlConnection(clsDataAccessSettings.ConnectionString);

            string query = @"UPDATE TestTypes
                              SET 
                                 TestTypeTitle = @TestTypeTitle,
                                 TestTypeFees = @TestTypeFees,
                                 TestTypeDescription =@TestTypeDescription
                            WHERE TestTypeID =@TestTypeID ";
            SqlCommand command = new SqlCommand(query, conn);
            command.Parameters.AddWithValue("@TestTypeFees", TestTypeFees);
            command.Parameters.AddWithValue("@TestTypeTitle", TestTypeTitle);
            command.Parameters.AddWithValue("@TestTypeDescription", TestTypeDescription);
            command.Parameters.AddWithValue("@TestTypeID", TestTypeID);

            try
            {
                conn.Open();
                rowsAffected = command.ExecuteNonQuery();

            }
            catch (Exception ex)
            {

                string Location = "clsTestTypesData → UpdateTestTypes";
                clsEventLogger.LogEvent(ex, Location, System.Diagnostics.EventLogEntryType.Error);

                return false;
            }
            finally
            {
                conn.Close();
            }
            return rowsAffected > 0;
        }
        public static DataTable GetAllTestTypes()
        {
            DataTable dt = new DataTable();
            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

            string query = "SELECT * FROM TestTypes order by TestTypeTitle";

            SqlCommand command = new SqlCommand(query, connection);

            try
            {
                connection.Open();

                SqlDataReader reader = command.ExecuteReader();

                if (reader.HasRows)

                {
                    dt.Load(reader);
                }

                reader.Close();


            }

            catch (Exception ex)
            {

                string Location = "clsTestTypesData → GetAllTestTypes";
                clsEventLogger.LogEvent(ex, Location, System.Diagnostics.EventLogEntryType.Error);

            }
            finally
            {
                connection.Close();
            }

            return dt;
        }
        public static int AddNewTestType(string TestTypeTitle, string TestTypeDescription, float TestTypeFees)
        {
            int TestTypeID = -1;
            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);


            string query = @"Insert Into TestTypes (TestTypeTitle,TestTypeDescription,TestTypeFees)
                            Values (@TestTypeTitle,@TestTypeDescription,@TestTypeFees)
                            
                            SELECT SCOPE_IDENTITY();";

            SqlCommand command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue("@TestTypeTitle", TestTypeTitle);
            command.Parameters.AddWithValue("@TestTypeDescription", TestTypeDescription);
            command.Parameters.AddWithValue("@TestTypeFees", TestTypeFees);

            try
            {
                connection.Open();

                object result = command.ExecuteScalar();

                if (result != null && int.TryParse(result.ToString(), out int insertedID))
                {
                    TestTypeID = insertedID;
                }
            }

            catch (Exception ex)
            {

                string Location = "clsTestTypesData → AddNewTestType";
                clsEventLogger.LogEvent(ex, Location, System.Diagnostics.EventLogEntryType.Error);

            }

            finally
            {
                connection.Close();
            }


            return TestTypeID;

        }

    }
}
