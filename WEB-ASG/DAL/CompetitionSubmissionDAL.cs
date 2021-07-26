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
    public class CompetitionSubmissionDAL
    {
        private IConfiguration Configuration { get; }
        private SqlConnection conn;
        //Constructor
        public CompetitionSubmissionDAL()
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

        public List<CompetitionSubmission> GetAllCompetitionSubmission(int competitorID)
        {
            //Create a SqlCommand object from connection object
            SqlCommand cmd = conn.CreateCommand();
            //Specify the SELECT SQL statement to get all competition submmissions of a competitor
            cmd.CommandText = @"SELECT * FROM CompetitionSubmission 
                                WHERE CompetitorID = @selectedCompetitorID  
                                ORDER BY CompetitorID ";
            //Define the parameter used in SQL statement, value for the
            //parameter is retrieved from the method parameter “competitorID”.
            cmd.Parameters.AddWithValue("@selectedCompetitorID", competitorID);
            //Open a database connection
            conn.Open();
            //Execute the SELECT SQL through a DataReader
            SqlDataReader reader = cmd.ExecuteReader();
            //Read all records until the end, save data into a competitionSubmission list
            List<CompetitionSubmission> competitionSubmissionList = new List<CompetitionSubmission>();
            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    competitionSubmissionList.Add(
                    new CompetitionSubmission
                    {
                        CompetitionID = reader.GetInt32(0), //0: 1st column
                        CompetitorID = reader.GetInt32(1), //1: 2nd column
                        FileSubmitted = !reader.IsDBNull(2) ? reader.GetString(2) : null, //2: 3rd column
                        DateTimeFileUpload = !reader.IsDBNull(3) ? reader.GetDateTime(3) : (DateTime?)null, //3: 4th column
                        Appeal = !reader.IsDBNull(4) ? reader.GetString(4) : null, //4: 5th column
                        VoteCount = reader.GetInt32(5), //5: 6th column
                        Ranking = !reader.IsDBNull(6) ? reader.GetInt32(6) : (int?)null, //6: 7th column
                    }
                    ); ;
                }
            }
            
            //Close DataReader
            reader.Close();
            //Close the database connection
            conn.Close();
            return competitionSubmissionList;
        }

        public void Add(CompetitionSubmission competitionSubmissions)
        {
            //Create a SqlCommand object from connection object
            SqlCommand cmd = conn.CreateCommand();
            //Specify an INSERT SQL statement
            cmd.CommandText = @"INSERT INTO CompetitionSubmission (CompetitionID, CompetitorID, FileSubmitted, DateTimeFileUpload, VoteCount)
                            VALUES(@competitionID, @competitorID, @fileSubmitted, @dateTimeFileUpload, @voteCount)";
            //Define the parameters used in SQL statement, value for each parameter
            //is retrieved from respective class's property.
            cmd.Parameters.AddWithValue("@competitionID", competitionSubmissions.CompetitionID);
            cmd.Parameters.AddWithValue("@competitorID", competitionSubmissions.CompetitorID);
            cmd.Parameters.AddWithValue("@voteCount", competitionSubmissions.VoteCount);
            if(string.IsNullOrEmpty(competitionSubmissions.FileSubmitted))
            {
                cmd.Parameters.AddWithValue("@fileSubmitted", DBNull.Value);
                cmd.Parameters.AddWithValue("@dateTimeFileUpload", DBNull.Value);
            }
            else
            {
                cmd.Parameters.AddWithValue("@fileSubmitted", competitionSubmissions.FileSubmitted);
                cmd.Parameters.AddWithValue("@dateTimeFileUpload", competitionSubmissions.DateTimeFileUpload);
            }
            //A connection to database must be opened before any operations made.
            conn.Open();
            cmd.ExecuteScalar();
            //A connection should be closed after operations.
            conn.Close();
            //Return id when no error occurs.
        }

        public CompetitionSubmissionViewModel GetDetails(int competitionID, int competitorID)
        {
            CompetitionSubmissionViewModel competitionSubmissionVM = new CompetitionSubmissionViewModel();
            //Create a SqlCommand object from connection object
            SqlCommand cmd = conn.CreateCommand();
            //Specify the SELECT SQL statement that
            //retrieves all attributes of a competition submission record.
            cmd.CommandText = @"SELECT Competition.CompetitionID, Competition.CompetitionName, CompetitorID, FileSubmitted, DateTimeFileUpload 
                                FROM CompetitionSubmission 
                                INNER JOIN Competition ON Competition.CompetitionID = CompetitionSubmission.CompetitionID
                                WHERE Competition.CompetitionID = @selectedCompetitionID AND CompetitorID = @selectedCompetitorID";
            //Define the parameters used in SQL statement, value for the
            //parameter is retrieved from the method parameter “competitionID” and "competitorID".
            cmd.Parameters.AddWithValue("@selectedCompetitionID", competitionID);
            cmd.Parameters.AddWithValue("@selectedCompetitorID", competitorID);
            //Open a database connection
            conn.Open();
            //Execute SELCT SQL through a DataReader
            SqlDataReader reader = cmd.ExecuteReader();
            if (reader.HasRows)
            {
                //Read the record from database
                while (reader.Read())
                {
                    // Fill competitionSubmissions object with values from the data reader
                    competitionSubmissionVM.CompetitionID = competitionID;
                    competitionSubmissionVM.CompetitionName = reader.GetString(1);
                    competitionSubmissionVM.CompetitorID = competitorID;
                    competitionSubmissionVM.FileSubmitted = !reader.IsDBNull(3) ? reader.GetString(3) : null;
                    competitionSubmissionVM.DateTimeFileUpload = !reader.IsDBNull(4) ? reader.GetDateTime(4) : (DateTime?)null;
                }
            }
            //Close data reader
            reader.Close();
            //Close database connection
            conn.Close();
            return competitionSubmissionVM;
        }

        // Return number of row updated
        public int Update(CompetitionSubmission competitionSubmissions)
        {
            //Create a SqlCommand object from connection object
            SqlCommand cmd = conn.CreateCommand();
            //Specify an UPDATE SQL statement
            cmd.CommandText = @"UPDATE CompetitionSubmission SET FileSubmitted=@fileSubmitted,
                            DateTimeFileUpload=@dateTimeFileUpload, Appeal = @appeal
                            WHERE CompetitionID = @competitionID AND CompetitorID = @competitorID";
            //Define the parameters used in SQL statement, value for each parameter
            //is retrieved from respective class's property.
            cmd.Parameters.AddWithValue("@competitionID", competitionSubmissions.CompetitionID);
            cmd.Parameters.AddWithValue("@competitorID", competitionSubmissions.CompetitorID);
            cmd.Parameters.AddWithValue("@fileSubmitted", competitionSubmissions.FileSubmitted);
            cmd.Parameters.AddWithValue("@dateTimeFileUpload", competitionSubmissions.DateTimeFileUpload);
            if (string.IsNullOrEmpty(competitionSubmissions.Appeal))
            {
                cmd.Parameters.AddWithValue("@appeal", DBNull.Value);
            }
            else
            {
                cmd.Parameters.AddWithValue("@appeal", competitionSubmissions.Appeal);
            }
            cmd.Parameters.AddWithValue("@voteCount", competitionSubmissions.VoteCount);

            if (competitionSubmissions.Ranking == null)
            {
                cmd.Parameters.AddWithValue("@ranking", DBNull.Value);
            }
            else
            {
                cmd.Parameters.AddWithValue("@ranking", competitionSubmissions.Ranking);
            }
            //Open a database connection
            conn.Open();
            //ExecuteNonQuery is used for UPDATE and DELETE
            int count = cmd.ExecuteNonQuery();
            //Close the database connection
            conn.Close();
            return count;
        }
        public int Delete(int competitionID, int competitorID)
        {
            //Instantiate a SqlCommand object, supply it with a DELETE SQL statement
            //to delete a competitor record specified by a CompetitionSubmission ID
            int rowAffected = 0;
            SqlCommand cmd = conn.CreateCommand();
            cmd.CommandText = @"DELETE FROM CompetitionSubmission
                                WHERE CompetitionID = @selectedCompetitionID AND CompetitorID = @selectedCompetitorID";
            cmd.Parameters.AddWithValue("@selectedCompetitionID", competitionID);
            cmd.Parameters.AddWithValue("@selectedCompetitorID", competitorID);
            //Open a database connection
            conn.Open();
            //Execute the DELETE SQL to remove the competitor record
            rowAffected += cmd.ExecuteNonQuery();
            //Close database connection
            conn.Close();
            //Return number of row of competitor record updated or deleted
            return rowAffected;
        }
    }
}
