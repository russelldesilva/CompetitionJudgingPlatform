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
    public class CompetitorDAL
    {
        private IConfiguration Configuration { get; }
        private SqlConnection conn;
        //Constructor
        public CompetitorDAL()
        {
            //Read ConnectionString from appsettings.json file
            var builder = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json");
            Configuration = builder.Build();
            string strConn = Configuration.GetConnectionString(
            "CJPConnectionString");
            //Instantiate a SqlConnection object with the
            //Connection String read.
            conn = new SqlConnection(strConn);
        }
        public List<Competitor> GetAllCompetitor()
        {
            //Create a SqlCommand object from connection object
            SqlCommand cmd = conn.CreateCommand();
            //Specify the SELECT SQL statement
            cmd.CommandText = @"SELECT * FROM Competitor ORDER BY CompetitorID";
            //Open a database connection
            conn.Open();
            //Execute the SELECT SQL through a DataReader
            SqlDataReader reader = cmd.ExecuteReader();
            //Read all records until the end, save data into a competitor list
            List<Competitor> competitorList = new List<Competitor>();
            while (reader.Read())
            {
                competitorList.Add(
                new Competitor
                {
                    CompetitorID = reader.GetInt32(0), //0: 1st column
                    CompetitorName = reader.GetString(1), //1: 2nd column
                                                          //Get the first character of a string
                    Salutation = reader.GetString(2), //2: 3rd column
                    EmailAddr = reader.GetString(3), //3: 4th column
                    Password = reader.GetString(4), //4: 5th column
                }
                );
            }
            //Close DataReader
            reader.Close();
            //Close the database connection
            conn.Close();
            return competitorList;
        }
        public bool IsEmailExist(string email, int competitorID)
        {
            bool emailFound = false;
            //Create a SqlCommand object and specify the SQL statement
            //to get a competitor record with the email address to be validated
            SqlCommand cmd = conn.CreateCommand();
            cmd.CommandText = @"SELECT CompetitorID FROM Competitor 
                               WHERE EmailAddr=@selectedEmail";
            cmd.Parameters.AddWithValue("@selectedEmail", email);
            //Open a database connection and execute the SQL statement
            conn.Open();
            SqlDataReader reader = cmd.ExecuteReader();
            if (reader.HasRows)
            { //Records found
                while (reader.Read())
                {
                    if (reader.GetInt32(0) != competitorID)
                        //The email address is used by another competitor
                        emailFound = true;
                }
            }
            else
            { //No record
                emailFound = false; // The email address given does not exist
            }
            reader.Close();
            conn.Close();

            return emailFound;
        }

        public int Add(Competitor competitor)
        {
            //Create a SqlCommand object from connection object
            SqlCommand cmd = conn.CreateCommand();
            //Specify an INSERT SQL statement which will
            //return the auto-generated CompetitorID after insertion
            cmd.CommandText = @"INSERT INTO Competitor (CompetitorName, Salutation, EmailAddr, Password)
                                OUTPUT INSERTED.CompetitorID
                                VALUES(@competitorName, @salutation, @emailAddr, @password)";
            //Define the parameters used in SQL statement, value for each parameter
            //is retrieved from respective class's property.
            cmd.Parameters.AddWithValue("@competitorName", competitor.CompetitorName);
            cmd.Parameters.AddWithValue("@salutation", competitor.Salutation);
            cmd.Parameters.AddWithValue("@emailAddr", competitor.EmailAddr);
            cmd.Parameters.AddWithValue("@password", competitor.Password);
            //A connection to database must be opened before any operations made.
            conn.Open();
            //ExecuteScalar is used to retrieve the auto-generated
            //CompetitorID after executing the INSERT SQL statement
            competitor.CompetitorID = (int)cmd.ExecuteScalar();
            //A connection should be closed after operations.
            conn.Close();
            //Return id when no error occurs.
            return competitor.CompetitorID;
        }

        public Competitor GetDetails(int competitorId)
        {
            Competitor competitor = new Competitor();
            //Create a SqlCommand object from connection object
            SqlCommand cmd = conn.CreateCommand();
            //Specify the SELECT SQL statement that
            //retrieves all attributes of a competitor record.
            cmd.CommandText = @"SELECT * FROM Competitor WHERE CompetitorID = @selectedCompetitorID";
            //Define the parameter used in SQL statement, value for the
            //parameter is retrieved from the method parameter “competitorId”.
            cmd.Parameters.AddWithValue("@selectedCompetitorID", competitorId);
            //Open a database connection
            conn.Open();
            //Execute SELCT SQL through a DataReader
            SqlDataReader reader = cmd.ExecuteReader();
            if (reader.HasRows)
            {
                //Read the record from database
                while (reader.Read())
                {
                    // Fill competitor object with values from the data reader
                    competitor.CompetitorID = competitorId;
                    competitor.CompetitorName = !reader.IsDBNull(1) ? reader.GetString(1) : null;
                    // (char) 0 - ASCII Code 0 - null value
                    competitor.Salutation = !reader.IsDBNull(2) ?
                    reader.GetString(2) : null;
                    competitor.EmailAddr = !reader.IsDBNull(3) ?
                    reader.GetString(3) : null;
                    competitor.Password = !reader.IsDBNull(5) ?
                    reader.GetString(5) : null;
                }
            }
            //Close data reader
            reader.Close();
            //Close database connection
            conn.Close();
            return competitor;
        }

        // Return number of row updated
        public int Update(Competitor competitor)
        {
            //Create a SqlCommand object from connection object
            SqlCommand cmd = conn.CreateCommand();
            //Specify an UPDATE SQL statement
            cmd.CommandText = @"UPDATE Competitor SET CompetitorName=@competitorName,
                                EmailAddr=@emailAddr, Password = @password
                                WHERE CompetitorID = @selectedCompetitorID";
            //Define the parameters used in SQL statement, value for each parameter
            //is retrieved from respective class's property.
            cmd.Parameters.AddWithValue("@competitorName", competitor.CompetitorName);
            cmd.Parameters.AddWithValue("@emailAddr", competitor.EmailAddr);
            cmd.Parameters.AddWithValue("@password", competitor.Password);
            cmd.Parameters.AddWithValue("@selectedCompetitorID", competitor.Password);
            //Open a database connection
            conn.Open();
            //ExecuteNonQuery is used for UPDATE and DELETE
            int count = cmd.ExecuteNonQuery();
            //Close the database connection
            conn.Close();
            return count;
        }
        public int Delete(int competitorID)
        {
            //Instantiate a SqlCommand object, supply it with a DELETE SQL statement
            //to delete a competitor record specified by a Competitor ID
            int rowAffected = 0;

            SqlCommand cmd = conn.CreateCommand();
            cmd.CommandText = @"DELETE FROM CompetitionSubmission
                                    WHERE CompetitorID = @selectedCompetitorID";
            cmd.Parameters.AddWithValue("@selectedCompetitorID", competitorID);

            SqlCommand cmd2 = conn.CreateCommand();
            cmd2.CommandText = @"DELETE FROM Competitor
                                WHERE CompetitorID = @selectedCompetitorID";
            cmd2.Parameters.AddWithValue("@selectedCompetitorID", competitorID);

            //Open a database connection
            conn.Open();
            //Execute the DELETE SQL to remove the competitor record
            rowAffected += cmd.ExecuteNonQuery();
            rowAffected += cmd2.ExecuteNonQuery();
            //Close database connection
            conn.Close();
            //Return number of row of competitor record updated or deleted
            return rowAffected;
        }
    }
}
