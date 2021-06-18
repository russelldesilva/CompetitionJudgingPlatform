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
        public List<Competition> GetCompetitions()
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
                        AreaInterestID = reader.GetInt32(1),
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
    }
}
