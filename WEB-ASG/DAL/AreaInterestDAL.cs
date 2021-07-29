using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using System.IO;
using System.Data.SqlClient;
using WEB_ASG.Models;

namespace WEB_ASG.DAL
{
    public class AreaInterestDAL
    {
        private IConfiguration Configuration { get; }
        private SqlConnection conn;
        public AreaInterestDAL()
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json");
            Configuration = builder.Build();
            string strConn = Configuration.GetConnectionString("CJPConnectionString");
            conn = new SqlConnection(strConn);
        }
        public List<AreaInterest> GetAreaInterests()
        {
            SqlCommand cmd = conn.CreateCommand();
            cmd.CommandText = @"SELECT * FROM AreaInterest ORDER BY AreaInterestID";
            conn.Open();
            SqlDataReader reader = cmd.ExecuteReader();
            List<AreaInterest> areaInterestList = new List<AreaInterest>();
            while (reader.Read())
            {
                areaInterestList.Add(
                    new AreaInterest
                    {
                        AreaInterestID = reader.GetInt32(0),
                        Name = reader.GetString(1)
                    }
                );
            }
            reader.Close();
            conn.Close();
            return areaInterestList;
        }
        public AreaInterest GetDetails(int areaInterestID)
        {
            AreaInterest areaInterest = new AreaInterest();
            SqlCommand cmd = conn.CreateCommand();
            cmd.CommandText = @"SELECT * FROM AreaInterest WHERE AreaInterestID = @selectedID";
            cmd.Parameters.AddWithValue("@selectedID", areaInterestID);
            conn.Open();
            SqlDataReader reader = cmd.ExecuteReader();
            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    areaInterest.AreaInterestID = areaInterestID;
                    areaInterest.Name = reader.GetString(1);
                }
            }
            reader.Close();
            conn.Close();
            return areaInterest;
        }
        public int Add(AreaInterest areaInterest)
        {
            //Create a SqlCommand object from connection object
            SqlCommand cmd = conn.CreateCommand();
            //Specify an INSERT SQL statement which will
            //return the auto-generated StaffID after insertion
            cmd.CommandText = @"INSERT INTO AreaInterest (Name)
                                                OUTPUT INSERTED.AreaInterestID
                                                VALUES(@name)";
            //Define the parameters used in SQL statement, value for each parameter
            //is retrieved from respective class's property.
            cmd.Parameters.AddWithValue("@name", areaInterest.Name);
            //A connection to database must be opened before any operations made.
            conn.Open();
            //ExecuteScalar is used to retrieve the auto-generated
            //StaffID after executing the INSERT SQL statement
            areaInterest.AreaInterestID = (int)cmd.ExecuteScalar();
            //A connection should be closed after operations.
            conn.Close();
            //Return id when no error occurs.
            return areaInterest.AreaInterestID;
        }
        public int Delete(int areaInterestID)
        {
            //Instantiate a SqlCommand object, supply it with a DELETE SQL statement
            //to delete a staff record specified by a Staff ID
            SqlCommand cmd = conn.CreateCommand();
            SqlCommand cmd1 = conn.CreateCommand();
            SqlCommand cmd2 = conn.CreateCommand();
            cmd2.CommandText = @"DELETE FROM Competition
                                 WHERE AreaInterestID = @areaInterestID";
            cmd1.CommandText = @"DELETE FROM Judge
                                 WHERE AreaInterestID = @areaInterestID";
            cmd.CommandText = @"DELETE FROM AreaInterest
                                WHERE AreaInterestID = @areaInterestID";
            cmd.Parameters.AddWithValue("@areaInterestID", areaInterestID);
            cmd1.Parameters.AddWithValue("@areaInterestID", areaInterestID);
            cmd2.Parameters.AddWithValue("@areaInterestID", areaInterestID);
            //Open a database connection
            conn.Open();
            int rowAffected = 0;
            //Execute the DELETE SQL to remove the staff record
            rowAffected += cmd.ExecuteNonQuery();
            rowAffected += cmd1.ExecuteNonQuery();
            rowAffected += cmd2.ExecuteNonQuery();
            //Close database connection
            conn.Close();
            //Return number of row of staff record updated or deleted
            return rowAffected;
        }
    }
}
