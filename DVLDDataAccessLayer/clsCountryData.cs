using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;

namespace DVLDDataAccessLayer
{
    public class clsCountryData
    {
        public static bool GetCountryInfoById(int CountryID,ref string CountryName)
        {
            bool IsFound = false;
            SqlConnection conn = new SqlConnection(clsDataAccessSettings.ConnectionString);
            string query = "SELECT * FROM Countries WHERE CountryID = @CountryID ;";
            SqlCommand command = new SqlCommand(query, conn);
            command.Parameters.AddWithValue("@CountryID", CountryID);
            try
            {
                conn.Open();
                SqlDataReader Reader = command.ExecuteReader();
                if (Reader.Read())
                {
                    IsFound = true;
                    CountryName = (string)Reader["CountryName"];

                }
                Reader.Close();
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
        public static bool GetCountryInfoByName(ref int CountryID,  string CountryName)
        {
            bool IsFound = false;
            SqlConnection conn = new SqlConnection(clsDataAccessSettings.ConnectionString);
            string query = "SELECT * FROM Countries WHERE CountryName = @CountryName ;";
            SqlCommand command = new SqlCommand(query, conn);
            command.Parameters.AddWithValue("@CountryName", CountryName);
            try
            {
                conn.Open();
                SqlDataReader Reader = command.ExecuteReader();
                if (Reader.Read())
                {
                    IsFound = true;
                    CountryID = Convert.ToInt32(Reader["CountryID"]);

                }
                Reader.Close();
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

        public static DataTable GetAllCountries()
        {
            DataTable dt = new DataTable();
            SqlConnection conn = new SqlConnection(clsDataAccessSettings.ConnectionString);
            string query = " SELECT * FROM Countries";
            SqlCommand command = new SqlCommand(query, conn);
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
    }
}
