using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using System.Text;
using System.Data;
using System.Drawing;

/// <summary>
/// Summary description for Comments
/// </summary>
public class Comments
{
    public DateTime Time { get; set; }
    public string Comment { get; set; }
    public  string UserName { get; set; }
    public int Reputation { get; set; }
    public Guid UserID { get; set; }
    public string Image { get; set; }
    public int CommentID { get; set; }

    public static List<Comments> GetAllComments(int threadID)
    {
        List<Comments> allComments = new List<Comments>();
        StringBuilder sb = new StringBuilder();
        //Gets all the comments that are inside the thread!
        sb.Append("SELECT CommentsID ");
        sb.Append(" FROM Comments");
        sb.Append(" WHERE ThreadsID=@ThreadsID");
        string myConnectionString = AllQuestionsPresented.connectionString;
        
        using (SqlConnection myConnection = new SqlConnection(myConnectionString))
        {
            myConnection.Open();
            SqlCommand sqlCommand = new SqlCommand(sb.ToString(), myConnection);
            sqlCommand.Parameters.Add("@ThreadsID", SqlDbType.Int);
            sqlCommand.Parameters["@ThreadsID"].Value = threadID;
            SqlDataReader reader = sqlCommand.ExecuteReader();

            while (reader.Read())
            {
                Comments allQ = new Comments((int)reader["CommentsID"]);
                
                allComments.Add(allQ);
            }
        }
        return allComments;
    }
    StringBuilder sb;
    int commentIDe;

    public Comments(int commentID)
    {
        CommentID = commentID;
       //Read one comment..
        commentIDe = commentID;
        sb = new StringBuilder();
        sb.Append("SELECT u.Name, u.UsersID, c.Comments, u.Reputation, c.Date, u.Avatar");
        sb.Append("  FROM Comments AS c");
        sb.Append(" INNER JOIN Users AS u ON u.UsersID=c.UsersID");
        sb.Append(" INNER JOIN Threads AS t ON t.ThreadsID=c.ThreadsID");
        sb.Append(" WHERE c.CommentsID=@CommentsID");
       

        ExecuteAllComments();
    }


    void ExecuteAllComments()
    {
        SqlConnection conn = new SqlConnection(AllQuestionsPresented.connectionString);
       
        SqlCommand cmd = new SqlCommand(sb.ToString(), conn);
        cmd.Parameters.Add("@ThreadsID", SqlDbType.Int);
        cmd.Parameters["@ThreadsID"].Value = commentIDe;
        int result3;
        cmd.Parameters.Add("@CommentsID", SqlDbType.Int);
        cmd.Parameters["@CommentsID"].Value = commentIDe;
        
        try
        {
            conn.Open();
            SqlDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection);
            if (dr.HasRows)
            {
                    dr.Read();
                    Comment = dr["Comments"].ToString();
                    UserName = dr["Name"].ToString();
                    Reputation = (Int32.TryParse(dr["Reputation"].ToString(), out result3)) ? int.Parse(dr["Reputation"].ToString()) : 0;
                    Time = (DateTime)AllQuestionsPresented.TryParse(dr["Date"].ToString());
                    UserID = (Guid)dr["UsersID"];
                    Image = dr["Avatar"].ToString();
                
            }
            dr.Close();
        }
        finally
        {
            if (conn.State != ConnectionState.Closed)
            {
                conn.Close();
            }
        }
    }
}