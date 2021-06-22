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
            reader.Close();
            conn.Close();
            return judgeList;
        }
        public int InsertCompetitionJudge (int compID, int judgeID)
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
        public int RemoveCompetitionJudge (int compID, int judgeID)
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
