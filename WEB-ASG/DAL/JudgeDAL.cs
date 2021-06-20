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
        public List<Judge> GetJudges()
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
                        AreaIntrestID = reader.GetInt32(3),
                        EmailAddr = reader.GetString(4),
                        Password = reader.GetString(5),
                    }
                );
            }
            reader.Close();
            conn.Close();
            return judgeList;
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
                    if  (j.JudgeID == reader.GetInt32(1))
                    {
                        j.Selected = true;
                    }
                }
            }
            return judgeList;
        }
        public int Update(Judge judge)
        {
            //Create a SqlCommand object from connection object
            SqlCommand cmd = conn.CreateCommand();
            //Specify an UPDATE SQL statement
            cmd.CommandText = @"UPDATE Judge SET CompetitionID = @compID, AreaInterestID = @areaID
                            WHERE JudgeID = @judgeID";
            //Define the parameters used in SQL statement, value for each parameter
            //is retrieved from respective class's property.
            cmd.Parameters.AddWithValue("@compID", judge.CompetitionID);
            cmd.Parameters.AddWithValue("@areaID", judge.AreaIntrestID);
            cmd.Parameters.AddWithValue("@judgeID", judge.JudgeID);
            //Open a database connection
            conn.Open();
            //ExecuteNonQuery is used for UPDATE and DELETE
            int count = cmd.ExecuteNonQuery();
            //Close the database connection
            conn.Close();
            return count;
        }
    }
}
