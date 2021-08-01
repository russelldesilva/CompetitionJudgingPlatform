using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using WEB_ASG.Models;

namespace WEB_ASG.DAL
{
    public class CompetitionScoreDAL
    {
        private IConfiguration Configuration { get; }
        private SqlConnection conn;
        public CompetitionScoreDAL()
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json");
            Configuration = builder.Build();
            string strConn = Configuration.GetConnectionString("CJPConnectionString");
            conn = new SqlConnection(strConn);
        }

        public List<CompetitionScoreViewModel> GetAllCompetitionScoreViewModel(int competitorID, int competitionID)
        {
            //Create a SqlCommand object from connection object
            SqlCommand cmd = conn.CreateCommand();
            //Specify the SQL statement that select all competition score
            cmd.CommandText = @"select CompetitionScore.CriteriaID, CriteriaName, CompetitionScore.CompetitorID, CompetitorName, CompetitionScore.CompetitionID, CompetitionName, Weightage, Score from
                                CompetitionScore INNER JOIN Criteria ON Criteria.CriteriaID = CompetitionScore.CriteriaID INNER JOIN
                                Competitor ON Competitor.CompetitorID = CompetitionScore.CompetitorID INNER JOIN Competition
                                ON Competition.CompetitionID = CompetitionScore.CompetitionID WHERE CompetitionScore.CompetitorID = @selectedCompetitor
                                AND CompetitionScore.CompetitionID = @selectedCompetition";
            cmd.Parameters.AddWithValue("@selectedCompetitor", competitorID);
            cmd.Parameters.AddWithValue("@selectedCompetition", competitionID);
            //Define the parameter used in SQL statement, value for the
            //Open a database connection
            conn.Open();
            //Execute SELCT SQL through a DataReader
            SqlDataReader reader = cmd.ExecuteReader();
            List<CompetitionScoreViewModel> competitionScores = new List<CompetitionScoreViewModel>();
            while (reader.Read())
            {
                competitionScores.Add(
                    new CompetitionScoreViewModel
                    {
                        CriteriaID = reader.GetInt32(0),
                        CriteriaName = reader.GetString(1),
                        CompetitorID = reader.GetInt32(2),
                        CompetitorName = reader.GetString(3),
                        CompetitionID = reader.GetInt32(4),
                        CompetitionName = reader.GetString(5),
                        Weightage = reader.GetInt32(6),
                        Score = reader.GetInt32(7)
                    }
                );
            }
            reader.Close();
            conn.Close();
            return competitionScores;
        }
        public CompetitionScoreViewModel GetScoreDetails(int criteriaID, int competitorID, int competitionID)
        {
            CompetitionScoreViewModel competitionscore = new CompetitionScoreViewModel();
            //Create a SqlCommand object from connection object
            SqlCommand cmd = conn.CreateCommand();
            //Specify the SELECT SQL statement that
            //retrieves all attributes of a criteria record.
            cmd.CommandText = @"SELECT CriteriaName, CompetitionName, CompetitorName, Score FROM CompetitionScore INNER JOIN Criteria ON CompetitionScore.CriteriaID = Criteria.CriteriaID 
                                INNER JOIN Competition ON CompetitionScore.CompetitionID = Competition.CompetitionID
                                INNER JOIN Competitor ON CompetitionScore.CompetitorID = Competitor.CompetitorID WHERE
                                CompetitionScore.CriteriaID = @selectedCriteriaID AND CompetitionScore.CompetitorID = @selectedCompetitorID AND CompetitionScore.CompetitionID = @selectedCompetitionID";
            //Define the parameter used in SQL statement, value for the
            //parameter is retrieved from the method parameter “criteriaID”.
            cmd.Parameters.AddWithValue("@selectedCriteriaID", criteriaID);
            cmd.Parameters.AddWithValue("@selectedCompetitorID", competitorID);
            cmd.Parameters.AddWithValue("@selectedCompetitionID", competitionID);
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
                    competitionscore.CriteriaName = !reader.IsDBNull(0) ?
                    reader.GetString(0) : null;
                    competitionscore.CompetitionName = !reader.IsDBNull(1) ?
                                        reader.GetString(1) : null;
                    competitionscore.CompetitorName = !reader.IsDBNull(2) ?
                    reader.GetString(2) : null;
                    competitionscore.Score = reader.GetInt32(3);
                    competitionscore.CriteriaID = criteriaID;
                    competitionscore.CompetitorID = competitorID;
                    competitionscore.CompetitionID = competitionID;
                }
            }
            //Close data reader
            reader.Close();
            //Close database connection
            conn.Close();
            return competitionscore;
        }

        public void AddCompetitionScore(CompetitionScore competitionscore)
        {
            //Create a SqlCommand object from connection object
            SqlCommand cmd = conn.CreateCommand();
            //Specify an INSERT SQL statement which will
            //return the auto-generated JudgeID after insertion 
            cmd.CommandText = @"INSERT INTO CompetitionScore (CriteriaID, CompetitorID, CompetitionID, Score)
                                VALUES (@criteriaid, @competitorid, @competitionid, @score)";
            //Define the parameters used in SQL statement, value for each parameter
            //is retrieved from respective class's property.
            cmd.Parameters.AddWithValue("@criteriaid", competitionscore.CriteriaID);
            cmd.Parameters.AddWithValue("@competitorid", competitionscore.CompetitorID);
            cmd.Parameters.AddWithValue("@competitionid", competitionscore.CompetitionID);
            cmd.Parameters.AddWithValue("@score", competitionscore.Score);
            //A connection to database must be opened before any operations made.
            conn.Open();
            cmd.ExecuteScalar();
            //A connection should be closed after operations.
            conn.Close();
        }
        public int UpdateScore(CompetitionScoreViewModel competitionscore)
        {
            //Create a SqlCommand object from connection object
            SqlCommand cmd = conn.CreateCommand();
            //Specify an UPDATE SQL statement
            cmd.CommandText = @"UPDATE CompetitionScore SET Score = @Score
                                 WHERE CriteriaID = @selectedCriteriaID AND CompetitorID = @selectedCompetitorID AND CompetitionID = @selectedCompetitionID";
            //Define the parameters used in SQL statement, value for each parameter
            //is retrieved from respective class's property.
            cmd.Parameters.AddWithValue("@Score", competitionscore.Score);
            cmd.Parameters.AddWithValue("@selectedCriteriaID", competitionscore.CriteriaID);
            cmd.Parameters.AddWithValue("@selectedCompetitorID", competitionscore.CompetitorID);
            cmd.Parameters.AddWithValue("@selectedCompetitionID", competitionscore.CompetitionID);
            //Open a database connection
            conn.Open();
            //ExecuteNonQuery is used for UPDATE and DELETE
            int count = cmd.ExecuteNonQuery();
            //Close the database connection
            conn.Close();
            return count;
        }
        public int DeleteScore(int criteriaID, int competitorID, int competitionID, int score)
        {
            //Instantiate a SqlCommand object, supply it with a DELETE SQL statement
            //to delete a staff record specified by a Staff ID
            SqlCommand cmd = conn.CreateCommand();
            cmd.CommandText = @"DELETE FROM CompetitionScore
                                WHERE CriteriaID = @selectCriteriaID AND CompetitorID = @selectedCompetitorID AND CompetitionID = @selectedCompetitionID AND Score = @selectedScore";
            cmd.Parameters.AddWithValue("@selectCriteriaID", criteriaID);
            cmd.Parameters.AddWithValue("@selectedCompetitorID", competitorID);
            cmd.Parameters.AddWithValue("@selectedCompetitionID", competitionID);
            cmd.Parameters.AddWithValue("@selectedScore", score);
            //Open a database connection
            conn.Open();
            int rowAffected = 0;
            //Execute the DELETE SQL to remove the staff record
            rowAffected += cmd.ExecuteNonQuery();
            //Close database connection
            conn.Close();//Return number of row of staff record updated or deleted
            return rowAffected;
        }
    }
}
