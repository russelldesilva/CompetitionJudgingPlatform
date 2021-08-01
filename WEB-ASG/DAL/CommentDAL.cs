using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WEB_ASG.Models;
using System.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System.IO;

namespace WEB_ASG.DAL
{
    public class CommentDAL
    {
        private IConfiguration Configuration { get; }
        private SqlConnection conn;
        public CommentDAL()
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json");
            Configuration = builder.Build();
            string strConn = Configuration.GetConnectionString("CJPConnectionString");
            conn = new SqlConnection(strConn);
        }
        public List<Comment> Comments()
        {
            SqlCommand cmd = conn.CreateCommand();
            cmd.CommandText = @"SELECT * FROM Comment";
            conn.Open();
            SqlDataReader reader = cmd.ExecuteReader();
            List<Comment> commentList = new List<Comment>();
            while (reader.Read())
            {
                commentList.Add(
                    new Comment
                    {
                        CommentID = reader.GetInt32(0),
                        CompetitionID = reader.GetInt32(1),                        
                        Description = reader.GetString(2),
                        DateTimePosted = reader.GetDateTime(3),
                    }
                );
            }
            reader.Close();
            conn.Close();
            return commentList;
        }
        public List<Comment> GetComment(int competiionID)
        {
            List<Comment> commentList = new List<Comment>();
            SqlCommand cmd = conn.CreateCommand();
            string commandText = string.Format(@"SELECT * FROM Comment WHERE CompetitionID = {0}", competiionID);
            cmd.CommandText = commandText;
            conn.Open();
            SqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                commentList.Add(
                    new Comment
                    {
                        CommentID = reader.GetInt32(0),
                        CompetitionID = reader.GetInt32(1),
                        Description = reader.GetString(2),
                        DateTimePosted = reader.GetDateTime(3),
                    }
                );
            }
            reader.Close();
            conn.Close();
            return commentList;
        }
        public int Add(Comment com)
        {
            //Create a SqlCommand object from connection object
            SqlCommand cmd = conn.CreateCommand();
            //Specify an INSERT SQL statement which will
            //return the auto-generated StaffID after insertion
            cmd.CommandText = @"INSERT INTO Comment (CommentID, CompetitionID, Description, DateTimePosted)
                                                OUTPUT INSERTED.CommentID
                                                VALUES(@comID, @compID, @desc, @dateTimePosted)";
            //Define the parameters used in SQL statement, value for each parameter
            //is retrieved from respective class's property.
            cmd.Parameters.AddWithValue("@comID", com.CommentID);
            cmd.Parameters.AddWithValue("@compID", com.CompetitionID);
            cmd.Parameters.AddWithValue("@desc", com.Description);
            cmd.Parameters.AddWithValue("@dateTimePosted", com.DateTimePosted);
            //A connection to database must be opened before any operations made.
            conn.Open();
            //ExecuteScalar is used to retrieve the auto-generated
            //StaffID after executing the INSERT SQL statement
            com.CommentID = (int)cmd.ExecuteScalar();
            //A connection should be closed after operations.
            conn.Close();
            //Return id when no error occurs.
            return com.CommentID;
        }
    }

}
