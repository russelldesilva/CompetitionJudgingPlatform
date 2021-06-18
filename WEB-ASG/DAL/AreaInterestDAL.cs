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
    }
}
