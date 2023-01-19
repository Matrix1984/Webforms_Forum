using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using System.Data;
using System.Text;

/// <summary>
/// Summary description for ControlPanelMessages
/// </summary>
public class ControlPanelMessages
{
    public static List<ControlPanelMessages> CommentsList { get; set; }
    public string Name { get; set; }
    public string Img { get; set; }
    public string Comment { get; set; }
    public int CommentID { get; set; }
    public static List<ControlPanelMessages> GetAllControlPanelPosts(int controlPanelsID)
    {
        List<ControlPanelMessages> allComments = new List<ControlPanelMessages>();
        string findUsersID = "SELECT CommentsID FROM dbo.CP_Comments WHERE ControlPanelID=@ControlPanelID ORDER BY CommentsID DESC";
        string myConnectionString = AllQuestionsPresented.connectionString;

        using (SqlConnection myConnection = new SqlConnection(myConnectionString))
        {
            myConnection.Open();
            SqlCommand sqlCommand = new SqlCommand(findUsersID, myConnection);
            sqlCommand.Parameters.Add("ControlPanelID", DbType.Int16).Value = controlPanelsID;
            SqlDataReader reader = sqlCommand.ExecuteReader();
            while (reader.Read())
            {
                ControlPanelMessages allQ = new ControlPanelMessages((int)reader["CommentsID"]);
                allComments.Add(allQ);
            }
            CommentsList = allComments;
        }
        return allComments;
    }
    //  dt.Rows.Add(new object[] { item.Img, item.Name, item.Comment });

    public DataSet GetObjects()
    {
        DataSet ds = new DataSet();
        DataTable dt = new DataTable();
        var source = from p in CommentsList
                     select new { p.CommentID, p.Img, p.Name, p.Comment };

        dt.Columns.Add("CommentsID");
        dt.Columns.Add("Img");
        dt.Columns.Add("Name");
        dt.Columns.Add("Comment");

        foreach (var item in source)
        {
            DataRow userDetailsRow=dt.NewRow();
            userDetailsRow["Img"] = item.Img;
            userDetailsRow["Name"] = item.Name;

            DataRow commentsID = dt.NewRow();
            userDetailsRow["CommentsID"] = item.CommentID;

            DataRow comments = dt.NewRow();
            userDetailsRow["Comment"] = item.Comment;
            dt.Rows.Add(userDetailsRow);
            //dt.Rows.Add(comments);
        }
        ds.Tables.Add(dt);
        return ds;
    }

    public void RemoveComment(int CommentsID)//
    {
        int comment = CommentsID;
         StringBuilder sb = new StringBuilder();
         sb.Append("DELETE dbo.CP_Comments");
         sb.Append(" WHERE CommentsID=@CommentID");
    
        string myConnectionString = AllQuestionsPresented.connectionString;
        using (SqlConnection myConnection = new SqlConnection(myConnectionString))
        {
            myConnection.Open();
            SqlCommand sqlCommand = new SqlCommand(sb.ToString(), myConnection);
            sqlCommand.Parameters.Add("@CommentID", SqlDbType.Int);
            sqlCommand.Parameters["@CommentID"].Value = CommentsID;
            sqlCommand.ExecuteNonQuery();
        }
   
    }

    public ControlPanelMessages()
    {
    }

    public ControlPanelMessages(int cpCommentID)
    {
        CommentID = cpCommentID;
        StringBuilder sb = new StringBuilder();
        sb.Append(" SELECT Avatar");
        sb.Append(" FROM Users");
        sb.Append(" WHERE Name=(Select Commentator");
        sb.Append(" FROM CP_Comments");
        sb.Append(" WHERE CommentsID=@CPCommentsID)");

        SqlConnection conn = new SqlConnection(AllQuestionsPresented.connectionString);
        SqlCommand cmd = new SqlCommand(sb.ToString(), conn);
        cmd.Parameters.Add("@CPCommentsID", SqlDbType.Int).Value = cpCommentID;

        try
        {
            conn.Open();
            SqlDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection);
            dr.Read();
            if (dr.HasRows)
            {
                //Name = dr["Commentator"].ToString();
                //Comment = dr["Comments"].ToString();
                string img = dr["Avatar"].ToString();
                if (img == "")
                {
                    Img = "~/YourGuruAvatar.png";
                }
                else
                {
                    Img = "~/ThumbNail_" + img;
                }
            }
            dr.Close();
        }
        finally
        {
            if (conn.State != ConnectionState.Closed)
            {
                conn.Close();
            }
            ControlPanelMessages2(cpCommentID);
        }

      

    }


    public void ControlPanelMessages2(int cpCommentID)
    {
        StringBuilder sb = new StringBuilder();
        sb.Append("SELECT c.Comments, c.Commentator");
        sb.Append(" FROM CP_Comments AS c  ");
        sb.Append(" INNER JOIN ControlPanel AS cp ON c.ControlPanelID=cp.ControlPanelID");
        sb.Append(" WHERE c.CommentsID=@CPCommentsID");




        SqlConnection conn = new SqlConnection(AllQuestionsPresented.connectionString);
        SqlCommand cmd = new SqlCommand(sb.ToString(), conn);
        cmd.Parameters.Add("@CPCommentsID", SqlDbType.Int).Value = cpCommentID;

        try
        {
            conn.Open();
            SqlDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection);
            dr.Read();
            if (dr.HasRows)
            {
                Name = dr["Commentator"].ToString();
                Comment = dr["Comments"].ToString();
                //string img = dr["Avatar"].ToString();
                //if (img == "")
                //{
                //    Img = null;
                //}
                //else
                //{
                //    Img = "~/ThumbNail_" + img;
                //}
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