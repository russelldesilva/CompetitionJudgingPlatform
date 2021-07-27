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
