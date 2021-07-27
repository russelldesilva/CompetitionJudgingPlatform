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
    public class JudgeDAL
    {
        private IConfiguration Configuration { get; }
        private SqlConnection conn;
        public JudgeDAL()
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json");
            Configuration = builder.Build();
            string strConn = Configuration.GetConnectionString("CJPConnectionString");
            conn = new SqlConnection(strConn);
        }
        public List<Judge> GetAllJudges()
        {
            SqlCommand cmd = conn.CreateCommand();
            cmd.CommandText = @"SELECT * FROM Judge ORDER BY JudgeID";
            conn.Open();
            SqlDataReader reader = cmd.ExecuteReader();
            List<Judge> judgeList = new List<Judge>();
            while (reader.Read())
            {
                judgeList.Add(
                    new Judge
                    {
                        JudgeID = reader.GetInt32(0),
                        JudgeName = reader.GetString(1),
                        Salutation = reader.GetString(2),
                        AreaInterestID = reader.GetInt32(3),
                        EmailAddr = reader.GetString(4),
                        Password = reader.GetString(5),
                    }
                );
            }
            reader.Close();
            conn.Close();
            return judgeList;
        }
        public List<Criteria> GetAllCriteria()
        {
            SqlCommand cmd = conn.CreateCommand();
            cmd.CommandText = @"SELECT * FROM Criteria ORDER BY CriteriaID";
            conn.Open();
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
                        Weightage = reader.GetInt32(3),

                    }
                );
            }
            reader.Close();
            conn.Close();
            return criteriaList;
        }
        public List<CriteriaViewModel> GetAllCriteriaViewModel()
        {
            //Create a SqlCommand object from connection object
            SqlCommand cmd = conn.CreateCommand();
            //Specify the SQL statement that select all judges
            cmd.CommandText = @"Select CriteriaID, CriteriaName, Criteria.CompetitionID, CompetitionName, Weightage from Criteria
                                INNER JOIN Competition on Criteria.CompetitionID = Competition.CompetitionID";
            //Define the parameter used in SQL statement, value for the
            //Open a database connection
            conn.Open();
            //Execute SELCT SQL through a DataReader
            SqlDataReader reader = cmd.ExecuteReader();
            List<CriteriaViewModel> criteriaList = new List<CriteriaViewModel>();
            while (reader.Read())
            {
                criteriaList.Add(
                    new CriteriaViewModel
                    {
                        CriteriaID = reader.GetInt32(0),
                        CriteriaName = reader.GetString(1),
                        CompetitionID = reader.GetInt32(2),
                        CompetitionName = reader.GetString(3),
                        Weightage = reader.GetInt32(4)
                    }
                );
            }
            reader.Close();
            conn.Close();
            return criteriaList;
        }
        public List<CompetitionScoreViewModel> GetAllCompetitionScoreViewModel(int competitorID, int competitionID)
        {
            //Create a SqlCommand object from connection object
            SqlCommand cmd = conn.CreateCommand();
            //Specify the SQL statement that select all judges
            cmd.CommandText = @"select CompetitionScore.CriteriaID, CriteriaName, CompetitionScore.CompetitorID, CompetitorName, CompetitionScore.CompetitionID, CompetitionName, Score from
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
                        Score = reader.GetInt32(6)
                    }
                );
            }
            reader.Close();
            conn.Close();
            return competitionScores;
        }

        public List<CompetitionScoreViewModel> GetAllCriteriaCompetition(int competitionID)
        {
            //Create a SqlCommand object from connection object
            SqlCommand cmd = conn.CreateCommand();
            //Specify the SQL statement that select all judges
            cmd.CommandText = @"select * from Criteria where CompetitionID = @selectedCompetition";
            cmd.Parameters.AddWithValue("@selectedCompetition", competitionID);
            //Define the parameter used in SQL statement, value for the
            //Open a database connection
            conn.Open();
            //Execute SELCT SQL through a DataReader
            SqlDataReader reader = cmd.ExecuteReader();
            List<CompetitionScoreViewModel> criteriaList = new List<CompetitionScoreViewModel>();
            while (reader.Read())
            {
                criteriaList.Add(
                    new CompetitionScoreViewModel
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

        public List<CompetitionJudgeViewModel> GetCompetitionAssigned(int judgeID)
        {
            SqlCommand cmd = conn.CreateCommand();
            //Specify the SQL statement that select all judges
            cmd.CommandText = @"select CompetitionJudge.CompetitionID, CompetitionName from CompetitionJudge 
                                INNER JOIN Competition ON Competition.CompetitionID = CompetitionJudge.CompetitionID
                                WHERE JudgeID = @selectedJudge";
            //Define the parameter used in SQL statement, value for the
            //parameter is retrieved from the method parameter “competitorID”.
            cmd.Parameters.AddWithValue("@selectedJudge", judgeID);
            //Open a database connection
            conn.Open();
            //Execute SELCT SQL through a DataReader
            SqlDataReader reader = cmd.ExecuteReader();
            List<CompetitionJudgeViewModel> competitionList = new List<CompetitionJudgeViewModel>();
            while (reader.Read())
            {
                competitionList.Add(
                    new CompetitionJudgeViewModel
                    {
                        CompetitionID = reader.GetInt32(0),
                        CompetitionName = reader.GetString(1),
                    }
                );
            }
            reader.Close();
            conn.Close();
            return competitionList;
        }
        public List<Competitor> GetAllCompetitors(int competitionID)
        {
            //Create a SqlCommand object from connection object
            SqlCommand cmd = conn.CreateCommand();
            //Specify the SELECT SQL statement
            cmd.CommandText = @"SELECT CompetitionSubmission.CompetitorID, CompetitorName from CompetitionSubmission INNER JOIN
                                Competitor ON Competitor.CompetitorID = CompetitionSubmission.CompetitorID where CompetitionID = @selectedCompetition";
            cmd.Parameters.AddWithValue("@selectedCompetition", competitionID);
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

                }
                );
            }
            //Close DataReader
            reader.Close();
            //Close the database connection
            conn.Close();
            return competitorList;
        }
        public Competition GetReleasedDate(int competitionID)
        {
            //Create a SqlCommand object from connection object
            SqlCommand cmd = conn.CreateCommand();
            //Specify the SELECT SQL statement
            cmd.CommandText = @"SELECT ResultReleasedDate FROM Competition
                                where CompetitionID = @selectedCompetition";
            cmd.Parameters.AddWithValue("@selectedCompetition", competitionID);
            //Open a database connection
            conn.Open();
            //Execute the SELECT SQL through a DataReader
            SqlDataReader reader = cmd.ExecuteReader();
            //Read all records until the end, save data into a competitor list
            Competition releaseDate = new Competition();
            if (reader.HasRows)
            {
                //Read the record from database
                while (reader.Read())
                {
                    // Fill competitor object with values from the data reader
                    releaseDate.ResultReleaseDate = reader.GetDateTime(0);
                }
            }
            //Close DataReader
            reader.Close();
            //Close the database connection
            conn.Close();
            return releaseDate;
        }
        public List<Competition> GetCompetitionName()
        {
            SqlCommand cmd = conn.CreateCommand();
            cmd.CommandText = @"SELECT * FROM Competition ORDER BY CompetitionID";
            conn.Open();
            SqlDataReader reader = cmd.ExecuteReader();
            List<Competition> competitionList = new List<Competition>();
            while (reader.Read())
            {
                competitionList.Add(
                    new Competition
                    {
                        CompetitionID = reader.GetInt32(0),
                        CompetitionName = reader.GetString(2)
                    }
                );
            }
            reader.Close();
            conn.Close();
            return competitionList;
        }
        public List<Judge> GetJudges(int areaID)
        {
            SqlCommand cmd = conn.CreateCommand();
            cmd.CommandText = @"SELECT * FROM Judge WHERE AreaInterestID = @areaID ORDER BY JudgeID";
            cmd.Parameters.AddWithValue("@areaID", areaID);
            conn.Open();
            SqlDataReader reader = cmd.ExecuteReader();
            List<Judge> judgeList = new List<Judge>();
            while (reader.Read())
            {
                judgeList.Add(
                    new Judge
                    {
                        JudgeID = reader.GetInt32(0),
                        JudgeName = reader.GetString(1),
                        Salutation = reader.GetString(2),
                        AreaInterestID = reader.GetInt32(3),
                        EmailAddr = reader.GetString(4),
                        Password = reader.GetString(5),
                    }
                );
            }
            reader.Close();
            conn.Close();
            return judgeList;
        }
        public bool IsEmailExist(string email, int JudgeID)
        {
            bool emailFound = false;
            //Create a SqlCommand object and specify the SQL statement
            //to get a staff record with the email address to be validated
            SqlCommand cmd = conn.CreateCommand();
            cmd.CommandText = @"SELECT JudgeID FROM Judge
 WHERE EmailAddr=@selectedEmail";
            cmd.Parameters.AddWithValue("@selectedEmail", email);
            //Open a database connection and execute the SQL statement
            conn.Open();
            SqlDataReader reader = cmd.ExecuteReader();
            if (reader.HasRows)
            { //Records found
                while (reader.Read())
                {
                    if (reader.GetInt32(0) != JudgeID)
                        //The email address is used by another staff
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

        public int EditWeightage(int competitionID, int criteriaID)
        {
            int Weightage = 0;
            SqlCommand cmd = conn.CreateCommand();
            cmd.CommandText = @"SELECT Weightage FROM Criteria
 WHERE CompetitionID=@selectedCompetition and CriteriaID = @selectedCriteriaID";
            cmd.Parameters.AddWithValue("@selectedCompetition", competitionID);
            cmd.Parameters.AddWithValue("@selectedCriteriaID", criteriaID);
            conn.Open();
            SqlDataReader reader = cmd.ExecuteReader();
            if (reader.HasRows)
            { //Records found
                while (reader.Read())
                {
                    Weightage = reader.GetInt32(0);

                }

            }
            reader.Close();
            conn.Close();
            return Weightage;
        }
        public bool IsWeightageRight(int competitionID, int weightage, int criteriaID)
        {
            bool weightageWrong = false;
            int count = 0;
            int Weightage = 0;
            //Create a SqlCommand object and specify the SQL statement
            //to get a staff record with the email address to be validated
            SqlCommand cmd = conn.CreateCommand();
            cmd.CommandText = @"SELECT Weightage FROM Criteria
 WHERE CompetitionID=@selectedCompetition";
            cmd.Parameters.AddWithValue("@selectedCompetition", competitionID);
            //Open a database connection and execute the SQL statement
            Weightage = EditWeightage(competitionID, criteriaID);
            conn.Open();
            SqlDataReader reader = cmd.ExecuteReader();
            if (reader.HasRows)
            { //Records found
                while (reader.Read())
                {
                    count = count + reader.GetInt32(0);

                }

            }

            if (count - Weightage + weightage > 100)
            {
                weightageWrong = true;
            }
            else if (count - Weightage + weightage == 100)
            {
                weightageWrong = false;
            }
            else if (count - Weightage + weightage < 100)
            {
                weightageWrong = false;
            }
            else if (count + weightage > 100)
            {
                weightageWrong = true;
            }
            else
            {
                weightageWrong = false;
            }
            reader.Close();
            conn.Close();

            return weightageWrong;
        }

        public int Add(Judge judge)
        {
            //Create a SqlCommand object from connection object
            SqlCommand cmd = conn.CreateCommand();
            //Specify an INSERT SQL statement which will
            //return the auto-generated JudgeID after insertion 
            cmd.CommandText = @"INSERT INTO Judge (JudgeName, Salutation, AreaInterestID, EmailAddr, Password)
                                OUTPUT INSERTED.JudgeID
                                VALUES(@judgeName, @salutation, @areaInterestID, @emailAddr, @password)";
            //Define the parameters used in SQL statement, value for each parameter
            //is retrieved from respective class's property.
            cmd.Parameters.AddWithValue("@judgeName", judge.JudgeName);
            cmd.Parameters.AddWithValue("@salutation", judge.Salutation);
            cmd.Parameters.AddWithValue("@areaInterestID", judge.AreaInterestID);
            cmd.Parameters.AddWithValue("@emailAddr", judge.EmailAddr);
            cmd.Parameters.AddWithValue("@password", judge.Password);
            //A connection to database must be opened before any operations made.
            conn.Open();
            //ExecuteScalar is used to retrieve the auto-generated
            //JudgeID after executing the INSERT SQL statement
            judge.JudgeID = (int)cmd.ExecuteScalar();
            //A connection should be closed after operations.
            conn.Close();
            //Return id when no error occurs.
            return judge.JudgeID;
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
        public int Update(Criteria criteria)
        {
            //Create a SqlCommand object from connection object
            SqlCommand cmd = conn.CreateCommand();
            //Specify an UPDATE SQL statement
            cmd.CommandText = @"UPDATE Criteria SET CriteriaName=@criteriaName,
                                Weightage = @weightage WHERE CriteriaID = @selectedCriteriaID";
            //Define the parameters used in SQL statement, value for each parameter
            //is retrieved from respective class's property.
            cmd.Parameters.AddWithValue("@criteriaName", criteria.CriteriaName);
            cmd.Parameters.AddWithValue("@weightage", criteria.Weightage);
            cmd.Parameters.AddWithValue("@selectedCriteriaID", criteria.CriteriaID);
            //Open a database connection
            conn.Open();
            //ExecuteNonQuery is used for UPDATE and DELETE
            int count = cmd.ExecuteNonQuery();
            //Close the database connection
            conn.Close();
            return count;
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
        public int Delete(int criteriaID)
        {
            //Instantiate a SqlCommand object, supply it with a DELETE SQL statement
            //to delete a staff record specified by a Staff ID
            SqlCommand cmd = conn.CreateCommand();
            cmd.CommandText = @"DELETE FROM Criteria
 WHERE CriteriaID = @selectCriteriaID";
            cmd.Parameters.AddWithValue("@selectCriteriaID", criteriaID);
            //Open a database connection
            conn.Open();
            int rowAffected = 0;
            //Execute the DELETE SQL to remove the staff record
            rowAffected += cmd.ExecuteNonQuery();
            //Close database connection
            conn.Close();//Return number of row of staff record updated or deleted
            return rowAffected;
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
        public Criteria GetDetails(int criteriaID)
        {
            Criteria criteria = new Criteria();
            //Create a SqlCommand object from connection object
            SqlCommand cmd = conn.CreateCommand();
            //Specify the SELECT SQL statement that
            //retrieves all attributes of a criteria record.
            cmd.CommandText = @"SELECT * FROM Criteria WHERE CriteriaID = @selectedCriteriaID";
            //Define the parameter used in SQL statement, value for the
            //parameter is retrieved from the method parameter “criteriaID”.
            cmd.Parameters.AddWithValue("@selectedCriteriaID", criteriaID);
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
                    criteria.CriteriaID = criteriaID;
                    criteria.CompetitionID = reader.GetInt32(1);
                    criteria.CriteriaName = !reader.IsDBNull(2) ?
                    reader.GetString(2) : null;
                    criteria.Weightage = reader.GetInt32(3);
                }
            }
            //Close data reader
            reader.Close();
            //Close database connection
            conn.Close();
            return criteria;
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

        public int AddCriteria(Criteria criteria)
        { //Create a SqlCommand object from connection object
            SqlCommand cmd = conn.CreateCommand();
            //Specify an INSERT SQL statement which will
            //return the auto-generated CriteriaID after insertion
            cmd.CommandText = @"INSERT INTO Criteria (CompetitionID, CriteriaName, Weightage)
                                OUTPUT INSERTED.CriteriaID
                                VALUES(@competitionID, @criteriaName, @weightage)";
            //Define the parameters used in SQL statement, value for each parameter
            //is retrieved from respective class's property.
            cmd.Parameters.AddWithValue("@competitionID", criteria.CompetitionID);
            cmd.Parameters.AddWithValue("@criteriaName", criteria.CriteriaName);
            cmd.Parameters.AddWithValue("@weightage", criteria.Weightage);
            //A connection to database must be opened before any operations made.
            conn.Open();
            //ExecuteScalar is used to retrieve the auto-generated
            //CriteriaID after executing the INSERT SQL statement
            criteria.CriteriaID = (int)cmd.ExecuteScalar();
            //A connection should be closed after operations.
            conn.Close();
            //Return id when no error occurs.
            return criteria.CriteriaID;
        }
        public List<Judge> GetCompetitionJudges(List<Judge> judgeList, int compID)
        {
            SqlCommand cmd = conn.CreateCommand();
            cmd.CommandText = @"SELECT * FROM CompetitionJudge WHERE CompetitionID = @compID ORDER BY CompetitionID";
            cmd.Parameters.AddWithValue("@compID", compID);
            conn.Open();
            SqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                foreach (Judge j in judgeList)
                {
                    if (j.JudgeID == reader.GetInt32(1))
                    {
                        j.Selected = true;
                    }
                }
            }
            reader.Close();
            conn.Close();
            return judgeList;
        }
        public int InsertCompetitionJudge(int compID, int judgeID)
        {
            SqlCommand cmd = conn.CreateCommand();
            cmd.CommandText = @"INSERT INTO CompetitionJudge (CompetitionID, JudgeID)
                                VALUES(@compID, @judgeID)";
            cmd.Parameters.AddWithValue("@compID", compID);
            cmd.Parameters.AddWithValue("@judgeID", judgeID);
            conn.Open();
            int count = cmd.ExecuteNonQuery();
            conn.Close();
            return count;
        }
        public int RemoveCompetitionJudge(int compID, int judgeID)
        {
            SqlCommand cmd = conn.CreateCommand();
            cmd.CommandText = @"DELETE FROM CompetitionJudge WHERE CompetitionID = @compID and JudgeID = @judgeID";
            cmd.Parameters.AddWithValue("@compID", compID);
            cmd.Parameters.AddWithValue("@judgeID", judgeID);
            conn.Open();
            int count = cmd.ExecuteNonQuery();
            conn.Close();
            return count;
        }
    }
}
