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
    public class CompetitionDAL
    {
        private IConfiguration Configuration { get; }
        private SqlConnection conn;
        public CompetitionDAL()
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json");
            Configuration = builder.Build();
            string strConn = Configuration.GetConnectionString("CJPConnectionString");
            conn = new SqlConnection(strConn);
        }
        //Get all Competition, but AreaOfInterestID is changed to Area of Interest Name
        public List<CompetitionDetailsViewModel> GetAllCompetitions()
        {
            SqlCommand cmd = conn.CreateCommand();
            cmd.CommandText = @"SELECT CompetitionID, AreaInterest.Name, CompetitionName, StartDate, EndDate, ResultReleasedDate 
                                FROM Competition 
                                INNER JOIN AreaInterest ON Competition.AreaInterestID = AreaInterest.AreaInterestID";
            conn.Open();
            SqlDataReader reader = cmd.ExecuteReader();
            List<CompetitionDetailsViewModel> competitionList = new List<CompetitionDetailsViewModel>();
            while (reader.Read())
            {
                competitionList.Add(
                    new CompetitionDetailsViewModel
                    {
                        CompetitionID = reader.GetInt32(0),
                        AreaInterest = reader.GetString(1),
                        CompetitionName = reader.GetString(2),
                        StartDate = reader.GetDateTime(3),
                        EndDate = reader.GetDateTime(4),
                        ResultReleaseDate = reader.GetDateTime(5)
                    }
                );
            }
            reader.Close();
            conn.Close();
            return competitionList;
        }
        //Get a Competition using CompetitionID
        public Competition GetCompetition(int competiionID)
        {
            Competition competition = new Competition();
            SqlCommand cmd = conn.CreateCommand();
            string commandText = string.Format(@"SELECT * FROM Competition WHERE CompetitionID = {0}", competiionID);
            cmd.CommandText = commandText;
            conn.Open();
            SqlDataReader reader = cmd.ExecuteReader();
            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    competition.CompetitionID = reader.GetInt32(0);
                    competition.AreaInterestID = reader.GetInt32(1);
                    competition.CompetitionName = reader.GetString(2);
                    competition.StartDate = reader.GetDateTime(3);
                    competition.EndDate = reader.GetDateTime(4);
                    competition.ResultReleaseDate = reader.GetDateTime(5);
                }
            }
            reader.Close();
            conn.Close();
            return competition;
        }
        //Get a list of Competition based on attribute
        public List<Competition> GetDetails(string attribute, int attrID)
        {
            List<Competition> compList = new List<Competition>();
            SqlCommand cmd = conn.CreateCommand();
            string commandText = string.Format(@"SELECT * FROM Competition WHERE {0} = {1}", attribute, attrID);
            cmd.CommandText = commandText;
            conn.Open();
            SqlDataReader reader = cmd.ExecuteReader();
            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    compList.Add(
                    new Competition
                    {
                        CompetitionID = reader.GetInt32(0),
                        AreaInterestID = reader.GetInt32(1),
                        CompetitionName = reader.GetString(2),
                        StartDate = reader.GetDateTime(3),
                        EndDate = reader.GetDateTime(4),
                        ResultReleaseDate = reader.GetDateTime(5)
                    });
                }
            }
            reader.Close();
            conn.Close();
            return compList;
        }
        public List<CompetitionDetailsViewModel> GetCompetitorCompetition(int competitorID)
        {
            //Create a SqlCommand object from connection object
            SqlCommand cmd = conn.CreateCommand();
            //Specify the SQL statement that select all judges
            cmd.CommandText = @"SELECT Competition.CompetitionID, AreaInterest.Name, CompetitionName, StartDate, EndDate, ResultReleasedDate
                                FROM Competition
                                INNER JOIN AreaInterest ON Competition.AreaInterestID = AreaInterest.AreaInterestID
                                INNER JOIN CompetitionSubmission ON Competition.CompetitionID = CompetitionSubmission.CompetitionID
                                WHERE CompetitionSubmission.CompetitorID = @selectedCompetitor";
            //Define the parameter used in SQL statement, value for the
            //parameter is retrieved from the method parameter “competitorID”.
            cmd.Parameters.AddWithValue("@selectedCompetitor", competitorID);
            //Open a database connection
            conn.Open();
            //Execute SELCT SQL through a DataReader
            SqlDataReader reader = cmd.ExecuteReader();
            List<CompetitionDetailsViewModel> competitionList = new List<CompetitionDetailsViewModel>();
            while (reader.Read())
            {
                competitionList.Add(
                    new CompetitionDetailsViewModel
                    {
                        CompetitionID = reader.GetInt32(0),
                        AreaInterest = reader.GetString(1),
                        CompetitionName = reader.GetString(2),
                        StartDate = reader.GetDateTime(3),
                        EndDate = reader.GetDateTime(4),
                        ResultReleaseDate = reader.GetDateTime(5)
                    }
                );
            }
            reader.Close();
            conn.Close();
            return competitionList;
        }
        public List<JudgeViewModel> GetCompetitionJudge(int competitionID)
        {
            //Create a SqlCommand object from connection object
            SqlCommand cmd = conn.CreateCommand();
            //Specify the SQL statement that select all judges
            cmd.CommandText = @"SELECT Judge.JudgeID, JudgeName, Salutation, AreaInterest.Name , EmailAddr, CompetitionID 
                                FROM Judge 
                                INNER JOIN CompetitionJudge ON Judge.JudgeID = CompetitionJudge.JudgeID
                                INNER JOIN AreaInterest ON Judge.AreaInterestID = AreaInterest.AreaInterestID
                                WHERE CompetitionID = @selectedCompetition";
            //Define the parameter used in SQL statement, value for the
            //parameter is retrieved from the method parameter “competitionID”.
            cmd.Parameters.AddWithValue("@selectedCompetition", competitionID);

            //Open a database connection
            conn.Open();
            //Execute SELCT SQL through a DataReader
            SqlDataReader reader = cmd.ExecuteReader();
            List<JudgeViewModel> judgeVMList = new List<JudgeViewModel>();
            while (reader.Read())
            {
                judgeVMList.Add(
                new JudgeViewModel
                {
                    JudgeID = reader.GetInt32(0),
                    JudgeName = reader.GetString(1),
                    Salutation = reader.GetString(2),
                    EmailAddr = reader.GetString(4),
                    CompetitionID = reader.GetInt32(5)
                }
                );
            }
            //Close DataReader
            reader.Close();
            //Close database connection
            conn.Close();
            return judgeVMList;
        }
        public List<Criteria> GetCompetitionCriteria(int competitionId)
        {
            //Create a SqlCommand object from connection object
            SqlCommand cmd = conn.CreateCommand();
            //Specify the SQL statement that select all judges
            cmd.CommandText = @"SELECT *
                                FROM Criteria
                                WHERE CompetitionID = @selectedCompetition";
            //Define the parameter used in SQL statement, value for the
            //parameter is retrieved from the method parameter “competitorID”.
            cmd.Parameters.AddWithValue("@selectedCompetition", competitionId);
            //Open a database connection
            conn.Open();
            //Execute SELCT SQL through a DataReader
            SqlDataReader reader = cmd.ExecuteReader();
            List<Criteria> criteriaList = new List<Criteria>();
            while (reader.Read())
            {
                criteriaList.Add(
                    new Criteria
                    {
                        CriteriaID = reader.GetInt32(0),
                        CompetitionID = reader.GetInt32(1),
                        CriteriaName = reader.GetString(2),
                        Weightage = reader.GetInt32(3)
                    }
                );
            }
            reader.Close();
            conn.Close();
            return criteriaList;
        }

        public int Update(Competition comp)
        {
            //Create a SqlCommand object from connection object
            SqlCommand cmd = conn.CreateCommand();
            //Specify an UPDATE SQL statement
            cmd.CommandText = @"UPDATE Competition SET AreaInterestID = @areaID, CompetitionName=@name,
                                StartDate=@startDate, EndDate = @endDate, ResultReleasedDate = @resultDate
                            WHERE CompetitionID = @compID";
            //Define the parameters used in SQL statement, value for each parameter
            //is retrieved from respective class's property.
            cmd.Parameters.AddWithValue("@areaID", comp.AreaInterestID);
            cmd.Parameters.AddWithValue("@name", comp.CompetitionName);
            cmd.Parameters.AddWithValue("@startDate", comp.StartDate);
            cmd.Parameters.AddWithValue("@endDate", comp.EndDate);
            cmd.Parameters.AddWithValue("@resultDate", comp.ResultReleaseDate);
            cmd.Parameters.AddWithValue("@compID", comp.CompetitionID);
            //Open a database connection
            conn.Open();
            //ExecuteNonQuery is used for UPDATE and DELETE
            int count = cmd.ExecuteNonQuery();
            //Close the database connection
            conn.Close();
            return count;
        }
        public int Add(Competition comp)
        {
            //Create a SqlCommand object from connection object
            SqlCommand cmd = conn.CreateCommand();
            //Specify an INSERT SQL statement which will
            //return the auto-generated StaffID after insertion
            cmd.CommandText = @"INSERT INTO Competition (AreaInterestID, CompetitionName, StartDate, EndDate, ResultReleasedDate)
                                                OUTPUT INSERTED.CompetitionID
                                                VALUES(@areaID, @name, @startDate, @endDate, @resultDate)";
            //Define the parameters used in SQL statement, value for each parameter
            //is retrieved from respective class's property.
            cmd.Parameters.AddWithValue("@areaID", comp.AreaInterestID);
            cmd.Parameters.AddWithValue("@name", comp.CompetitionName);
            cmd.Parameters.AddWithValue("@startDate", comp.StartDate);
            cmd.Parameters.AddWithValue("@endDate", comp.EndDate);
            cmd.Parameters.AddWithValue("@resultDate", comp.ResultReleaseDate);
            //A connection to database must be opened before any operations made.
            conn.Open();
            //ExecuteScalar is used to retrieve the auto-generated
            //StaffID after executing the INSERT SQL statement
            comp.CompetitionID = (int)cmd.ExecuteScalar();
            //A connection should be closed after operations.
            conn.Close();
            //Return id when no error occurs.
            return comp.CompetitionID;
        }
        public int Delete(int compID)
        {
            //Instantiate a SqlCommand object, supply it with a DELETE SQL statement
            //to delete a staff record specified by a Staff ID
            SqlCommand cmd = conn.CreateCommand();
            SqlCommand cmd1 = conn.CreateCommand();
            SqlCommand cmd2 = conn.CreateCommand();
            SqlCommand cmd3 = conn.CreateCommand();
            SqlCommand cmd4 = conn.CreateCommand();
            SqlCommand cmd5 = conn.CreateCommand();
            cmd.CommandText = @"DELETE FROM Competition
                                WHERE CompetitionID = @compID";
            cmd.Parameters.AddWithValue("@compID", compID);
            cmd1.CommandText = @"DELETE FROM CompetitionJudge
                                 WHERE CompetitionID = @compID";
            cmd1.Parameters.AddWithValue("@compID", compID);
            cmd2.CommandText = @"DELETE FROM CompetitionSubmission
                                 WHERE CompetitionID = @compID";
            cmd2.Parameters.AddWithValue("@compID", compID);
            cmd3.CommandText = @"DELETE FROM Criteria
                                 WHERE CompetitionID = @compID";
            cmd3.Parameters.AddWithValue("@compID", compID);
            cmd4.CommandText = @"DELETE FROM Comment
                                 WHERE CompetitionID = @compID";
            cmd4.Parameters.AddWithValue("@compID", compID);
            cmd5.CommandText = @"DELETE FROM CompetitionScore
                                 WHERE CompetitionID = @compID";
            cmd5.Parameters.AddWithValue("@compID", compID);
            //Open a database connection
            conn.Open();
            int rowAffected = 0;
            //Execute the DELETE SQL to remove the staff record
            rowAffected += cmd1.ExecuteNonQuery();
            rowAffected += cmd2.ExecuteNonQuery();
            rowAffected += cmd3.ExecuteNonQuery();
            rowAffected += cmd4.ExecuteNonQuery();
            rowAffected += cmd5.ExecuteNonQuery();
            rowAffected += cmd.ExecuteNonQuery();
            //Close database connection
            conn.Close();
            //Return number of row of staff record updated or deleted
            return rowAffected;
        }
    }
}
