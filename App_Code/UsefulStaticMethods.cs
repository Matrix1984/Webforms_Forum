using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Text;
using System.Data.SqlClient;
using System.Data;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Data;
using System.Web.Configuration;
using System.Web.Security;
using System.Security.Principal;
using System.Collections;
using System.Web.UI.HtmlControls;
using System.Runtime.Serialization;
using System.IO;

/// <summary>
/// Summary description for UsefulStaticMethods
/// </summary>
public class UsefulStaticMethods
{
	public UsefulStaticMethods()
	{

	}
    public static string TimeNow(DateTime time)
    { 
        StringBuilder sb = new StringBuilder();
        sb.Append(time.Hour.ToString() + ":");
        sb.Append(time.Minute.ToString() + ":");
        sb.Append(time.Second.ToString());
        sb.Append(" ");
        sb.Append(" ");
        sb.Append(" ");
        sb.Append(time.Day.ToString() + "/");
        sb.Append(time.Month.ToString() + "/");
        sb.Append(time.Year.ToString());
        return sb.ToString();
    }

    public static void DeleteThreads(int threadID)
    {
        StringBuilder sb = new StringBuilder();
        sb.Append("DELETE FROM dbo.Threads");
        sb.Append(" WHERE ThreadsID=@ThreadsID");

        string myConnectionString = AllQuestionsPresented.connectionString;

        using (SqlConnection myConnection = new SqlConnection(myConnectionString))
        {
            myConnection.Open();
            SqlCommand sqlCommand = new SqlCommand(sb.ToString(), myConnection);
            sqlCommand.Parameters.Add("@ThreadsID", SqlDbType.Int);
            sqlCommand.Parameters["@ThreadsID"].Value = threadID;
            sqlCommand.ExecuteNonQuery();

        }
    }

    public static void DeleteComments(int threadID)
    {
        StringBuilder sb = new StringBuilder();
        sb.Append("DELETE FROM dbo.Comments");
        sb.Append(" WHERE ThreadsID=@ThreadsID");

        string myConnectionString = AllQuestionsPresented.connectionString;

        using (SqlConnection myConnection = new SqlConnection(myConnectionString))
        {
            myConnection.Open();
            SqlCommand sqlCommand = new SqlCommand(sb.ToString(), myConnection);
            sqlCommand.Parameters.Add("@ThreadsID", SqlDbType.Int);
            sqlCommand.Parameters["@ThreadsID"].Value = threadID;
            sqlCommand.ExecuteNonQuery();

        }
    }

    public static void UpdateTopic(string topic, string subtopic, int threadID)
    {
        StringBuilder sb = new StringBuilder();
        sb.Append("UPDATE p");
        sb.Append(" SET p.Theme=@Theme, p.Topics=@Topics");
        sb.Append(" FROM dbo.topics AS p");
        sb.Append(" INNER JOIN Threads AS t ON p.TopicsID=t.TopicsID");
        sb.Append(" WHERE t.ThreadsID=@ThreadID");

        string myConnectionString = AllQuestionsPresented.connectionString;

        using (SqlConnection myConnection = new SqlConnection(myConnectionString))
        {
            myConnection.Open();
            SqlCommand sqlCommand = new SqlCommand(sb.ToString(), myConnection);
            sqlCommand.Parameters.Add("@Theme", SqlDbType.NVarChar);
            sqlCommand.Parameters["@Theme"].Value = topic;
            sqlCommand.Parameters.Add("@Topics", SqlDbType.NVarChar);
            sqlCommand.Parameters["@Topics"].Value = topic;
            sqlCommand.Parameters.Add("@ThreadID", SqlDbType.Int);
            sqlCommand.Parameters["@ThreadID"].Value = threadID;
            sqlCommand.ExecuteNonQuery();
        }
    }

    public static void UpdateThreadView(int threadID)
    {
        StringBuilder sb = new StringBuilder();
        sb.Append("UPDATE  dbo.Threads");
        sb.Append(" SET Views=(Views+1)");
        sb.Append(" WHERE ThreadsID=@ThreadID");

        string myConnectionString = AllQuestionsPresented.connectionString;

        using (SqlConnection myConnection = new SqlConnection(myConnectionString))
        {
            myConnection.Open();
            SqlCommand sqlCommand = new SqlCommand(sb.ToString(), myConnection);
            sqlCommand.Parameters.Add("@ThreadID", SqlDbType.Int);
            sqlCommand.Parameters["@ThreadID"].Value = threadID;
            sqlCommand.ExecuteNonQuery();

        }
    }


    public static void DeleteComment(int commentID)
    {
        StringBuilder sb = new StringBuilder();
        sb.Append("DELETE FROM dbo.Comments");
        sb.Append(" WHERE CommentsID=@CommentID");

        string myConnectionString = AllQuestionsPresented.connectionString;

        using (SqlConnection myConnection = new SqlConnection(myConnectionString))
        {
            myConnection.Open();
            SqlCommand sqlCommand = new SqlCommand(sb.ToString(), myConnection);
            sqlCommand.Parameters.Add("@CommentID", SqlDbType.Int);
            sqlCommand.Parameters["@CommentID"].Value = commentID;
            sqlCommand.ExecuteNonQuery();

        }
    }
    public static void UpdateThreadReplies(int threadID)
    {
        StringBuilder sb = new StringBuilder();
        sb.Append("UPDATE  dbo.Threads");
        sb.Append(" SET Replies=(Replies-1)");
        sb.Append(" WHERE ThreadsID=@ThreadID");

        string myConnectionString = AllQuestionsPresented.connectionString;

        using (SqlConnection myConnection = new SqlConnection(myConnectionString))
        {
            myConnection.Open();
            SqlCommand sqlCommand = new SqlCommand(sb.ToString(), myConnection);
            sqlCommand.Parameters.Add("@ThreadID", SqlDbType.Int);
            sqlCommand.Parameters["@ThreadID"].Value = threadID;
            sqlCommand.ExecuteNonQuery();

        }
    }
    public static System.Web.UI.Control GetPostBackControl(System.Web.UI.Page page)
    {
        Control control = null;
        string ctrlname = page.Request.Params["__EVENTTARGET"];
        if (ctrlname != null && ctrlname != String.Empty)
        {
            control = page.FindControl(ctrlname);
        }
        // if __EVENTTARGET is null, the control is a button type and we need to 
        // iterate over the form collection to find it
        else
        {
            string ctrlStr = String.Empty;
            Control c = null;
            foreach (string ctl in page.Request.Form)
            {
                // handle ImageButton controls ...
                if (ctl.EndsWith(".x") || ctl.EndsWith(".y"))
                {
                    ctrlStr = ctl.Substring(0, ctl.Length - 2);
                    c = page.FindControl(ctrlStr);
                }
                else
                {
                    c = page.FindControl(ctl);
                }
                if (c is System.Web.UI.WebControls.Button ||
                            c is System.Web.UI.WebControls.ImageButton)
                {
                    control = c;
                    break;
                }
            }
        }
        return control;
    }

    public static string GetAnImage(Guid i)
    {
        StringBuilder sb = new StringBuilder();

        sb.Append("SELECT Avatar");
        sb.Append(" FROM Users");
        sb.Append(" WHERE UsersID=@UserID");

        string myConnectionString = AllQuestionsPresented.connectionString;
        using (SqlConnection conn = new SqlConnection(AllQuestionsPresented.connectionString))
        {
            conn.Open();
            SqlCommand cmd = new SqlCommand(sb.ToString(), conn);
            cmd.Parameters.Add("UserID", SqlDbType.UniqueIdentifier).Value = i;

            object img = cmd.ExecuteScalar();
            try
            {//ImageControl1.ImageUrl = ResolveUrl("~/ThumbNail_" + ImgName)
                return "~/ThumbNail_" + img.ToString();
            }
            catch (Exception ex)
            {
                return null;
            }

        }
    }


    public static string GetAnImageToComment(int commentID)
    {
        StringBuilder sb = new StringBuilder();

        sb.Append("SELECT u.Avatar");
        sb.Append(" FROM Users AS u");
        sb.Append(" INNER JOIN Comments AS c ON c.UsersID=u.UsersID");
        sb.Append(" WHERE c.commentsID=@commentID");

        string myConnectionString = AllQuestionsPresented.connectionString;
        using (SqlConnection conn = new SqlConnection(AllQuestionsPresented.connectionString))
        {
            conn.Open();
            SqlCommand cmd = new SqlCommand(sb.ToString(), conn);
            cmd.Parameters.Add("commentID", SqlDbType.Int).Value = commentID;

            object img = cmd.ExecuteScalar();
            string theImage = img.ToString();
            try
            {
                if (theImage=="")
                {
                    return null;
                }
                else
                {
                    return "~/ThumbNail_" + img.ToString();
                }
            }
            catch (Exception ex)
            {
                return null;
            }

        }
    }

    public static string GetAnImageToThread(int threadID)
    {
        StringBuilder sb = new StringBuilder();

        sb.Append("SELECT u.Avatar");
        sb.Append(" FROM Users AS u");
        sb.Append(" INNER JOIN Threads AS t ON t.UsersID=u.UsersID");
        sb.Append(" WHERE t.threadsID=@ThreadID");

        string myConnectionString = AllQuestionsPresented.connectionString;
        using (SqlConnection conn = new SqlConnection(AllQuestionsPresented.connectionString))
        {
            conn.Open();
            SqlCommand cmd = new SqlCommand(sb.ToString(), conn);
            cmd.Parameters.Add("ThreadID", SqlDbType.Int).Value = threadID;

            object img = cmd.ExecuteScalar();

            if (img.ToString() == "")
            {
                return null;
            }
            else
            {
                return "~/ThumbNail_" + img.ToString();
            }
          

        }
    }


    public static string ControlPanelGetName(Guid userID)
    {
        StringBuilder sb = new StringBuilder();
        sb.Append("SELECT Name");
        sb.Append(" FROM Users");
        sb.Append(" WHERE UsersID=@UserID");
        

        string getName = "";
        string myConnectionString = AllQuestionsPresented.connectionString;
        using (SqlConnection conn = new SqlConnection(AllQuestionsPresented.connectionString))
        {
            SqlCommand cmd = new SqlCommand(sb.ToString(), conn);
            conn.Open();
            cmd.Parameters.Add("@UserID", SqlDbType.UniqueIdentifier).Value = userID;
            
            getName = cmd.ExecuteScalar().ToString();
        }
        return getName;
    }

    public static Guid GetUserNameFromUserGuid(string userName)
    {
        StringBuilder sb = new StringBuilder();
        sb.Append("SELECT UsersID");
        sb.Append(" FROM ControlPanel");
        sb.Append(" WHERE Name=@Name");


        Guid getGuid = Guid.Empty;
        string myConnectionString = AllQuestionsPresented.connectionString;
        using (SqlConnection conn = new SqlConnection(AllQuestionsPresented.connectionString))
        {
            SqlCommand cmd = new SqlCommand(sb.ToString(), conn);
            conn.Open();
            cmd.Parameters.Add("@Name", SqlDbType.NVarChar).Value = userName;
            getGuid = (Guid)cmd.ExecuteScalar();
        }
        return getGuid;
    }

    public static void InsertControlPanel(Guid userGuid, string userName )
    {
        StringBuilder sb = new StringBuilder();
        sb.Append("INSERT INTO dbo.ControlPanel(UsersID,Name)");
        sb.Append(" Values(@UserID,@UserName)");

        string myConnectionString = AllQuestionsPresented.connectionString;
        using (SqlConnection conn = new SqlConnection(AllQuestionsPresented.connectionString))
        {
            SqlCommand cmd = new SqlCommand(sb.ToString(), conn);
            conn.Open();
            cmd.Parameters.Add("@UserID", SqlDbType.UniqueIdentifier).Value = userGuid;
            cmd.Parameters.Add("@UserName", SqlDbType.NVarChar).Value = userName;
            cmd.ExecuteNonQuery();
           
        }
      
    }


    public static int GetControlPanelID(string userName)
    {
        StringBuilder sb = new StringBuilder();
        sb.Append("SELECT ControlPanelID");
        sb.Append(" FROM ControlPanel");
        sb.Append(" WHERE Name=@UserName");

        int controlPanelID = 0;
        string myConnectionString = AllQuestionsPresented.connectionString;
        using (SqlConnection conn = new SqlConnection(AllQuestionsPresented.connectionString))
        {
            SqlCommand cmd = new SqlCommand(sb.ToString(), conn);
            conn.Open();
            cmd.Parameters.Add("@UserName", SqlDbType.NVarChar).Value = userName;
            controlPanelID = int.Parse(cmd.ExecuteScalar().ToString());

        }
        return controlPanelID;
    }

    public static Guid GetUsersControlPanelNameTitleGuid(int titleID)//GetUsersControlPanelNameTitleGuid
    {
        StringBuilder sb = new StringBuilder();
        sb.Append("SELECT UsersID");
        sb.Append(" FROM Threads");
        sb.Append(" WHERE ThreadsID=@ThreadID");

        Guid controlPanelName = Guid.Empty;
        string myConnectionString = AllQuestionsPresented.connectionString;
        using (SqlConnection conn = new SqlConnection(AllQuestionsPresented.connectionString))
        {
            SqlCommand cmd = new SqlCommand(sb.ToString(), conn);
            conn.Open();
            cmd.Parameters.Add("@ThreadID", SqlDbType.Int).Value = titleID;
            controlPanelName = (Guid)cmd.ExecuteScalar();

        }
        return controlPanelName;
    }


    public static Guid GetUsersControlPanelNameCommentGuid(int commentID)
    {
        StringBuilder sb = new StringBuilder();
        sb.Append("SELECT UsersID");
        sb.Append(" FROM Comments");
        sb.Append(" WHERE CommentsID=@ComemntID");

        Guid controlPanelName = Guid.Empty;
        string myConnectionString = AllQuestionsPresented.connectionString;
        using (SqlConnection conn = new SqlConnection(AllQuestionsPresented.connectionString))
        {
            SqlCommand cmd = new SqlCommand(sb.ToString(), conn);
            conn.Open();
            cmd.Parameters.Add("@ComemntID", SqlDbType.Int).Value = commentID;
            controlPanelName = (Guid)cmd.ExecuteScalar();
        }
        return controlPanelName;
    }
    

    public static string GetUsersControlPanelName(Guid userID)
    {
        StringBuilder sb = new StringBuilder();
        sb.Append("SELECT Name");
        sb.Append(" FROM Users");
        sb.Append(" WHERE UsersID=@UserID");

        string controlPanelName = "";
        string myConnectionString = AllQuestionsPresented.connectionString;
        using (SqlConnection conn = new SqlConnection(AllQuestionsPresented.connectionString))
        {
            SqlCommand cmd = new SqlCommand(sb.ToString(), conn);
            conn.Open();
            cmd.Parameters.Add("@UserID", SqlDbType.UniqueIdentifier).Value = userID;
            controlPanelName = cmd.ExecuteScalar().ToString();
        }
        return controlPanelName;
    }

    public static void BanUserThreader(int threadID)
    {

        StringBuilder sb = new StringBuilder();
        sb.Append("DECLARE  @UserID uniqueidentifier");
        sb.Append(" SELECT @UserID=UsersID");
        sb.Append(" FROM dbo.Threads");
        sb.Append(" WHERE ThreadsID=@ThreadID");

        sb.Append(" UPDATE  dbo.Users");
        sb.Append(" SET Banned=1");
        sb.Append(" WHERE UsersID=@UserID");


        string myConnectionString = AllQuestionsPresented.connectionString;

        using (SqlConnection myConnection = new SqlConnection(myConnectionString))
        {
            myConnection.Open();
            SqlCommand sqlCommand = new SqlCommand(sb.ToString(), myConnection);
            sqlCommand.Parameters.Add("@ThreadID", SqlDbType.Int);
            sqlCommand.Parameters["@ThreadID"].Value = threadID;
            sqlCommand.ExecuteNonQuery();

        }
    }

    public static void BanUserCommentator(int CommentID)
    {
        StringBuilder sb = new StringBuilder();
        sb.Append("DECLARE  @UserID uniqueidentifier");
        sb.Append(" SELECT @UserID=UsersID");
        sb.Append(" FROM dbo.Comments");
        sb.Append(" WHERE CommentsID=@CommentID");

        sb.Append(" UPDATE  dbo.Users");
        sb.Append(" SET Banned=1");
        sb.Append(" WHERE UsersID=@UserID");

    
        string myConnectionString = AllQuestionsPresented.connectionString;

        using (SqlConnection myConnection = new SqlConnection(myConnectionString))
        {
            myConnection.Open();
            SqlCommand sqlCommand = new SqlCommand(sb.ToString(), myConnection);
            sqlCommand.Parameters.Add("@CommentID", SqlDbType.Int);
            sqlCommand.Parameters["@CommentID"].Value = CommentID;
            sqlCommand.ExecuteNonQuery();

        }
    }


    public static bool CheckIfUserISbanned(string name)
    {
        StringBuilder sb = new StringBuilder();
        sb.Append("SELECT Banned");
        sb.Append(" FROM dbo.Users");
        sb.Append(" WHERE Name=@Name");
        object o;
        bool isBanned = false;
        string myConnectionString = AllQuestionsPresented.connectionString;
        using (SqlConnection conn = new SqlConnection(AllQuestionsPresented.connectionString))
        {
            SqlCommand cmd = new SqlCommand(sb.ToString(), conn);
            conn.Open();
            cmd.Parameters.Add("@Name", SqlDbType.NVarChar).Value = name;
            o = cmd.ExecuteScalar();
        }
        isBanned = o == DBNull.Value ? false : true;
        return isBanned;
    }

    public static string FindCommentatorToDeleteIsModerator(int CommentID)
    {
        string myConnectionString = AllQuestionsPresented.connectionString;
        StringBuilder sb = new StringBuilder();
        using (SqlConnection myConnection = new SqlConnection(myConnectionString))
        {
            myConnection.Open();
            SqlCommand sqlCommand = new SqlCommand(sb.ToString(), myConnection);
            sqlCommand.Parameters.Add("@CommentID", SqlDbType.Int);
            sqlCommand.Parameters["@CommentID"].Value = CommentID;
            sqlCommand.ExecuteScalar();

        }
        return "UsersNameToReturn";
    }

    public static string FindUserNameOfThread(int threadID)
    {
        string myConnectionString = AllQuestionsPresented.connectionString;
        StringBuilder sb = new StringBuilder();
        sb.Append("SELECT u.Name");
        sb.Append(" FROM Users as u ");
        sb.Append(" INNER JOIN Threads as t ON t.UsersID=u.UsersID");
        sb.Append(" WHERE t.ThreadsID=@threadID");

        string name = "";
        using (SqlConnection myConnection = new SqlConnection(myConnectionString))
        {
            myConnection.Open();
            SqlCommand sqlCommand = new SqlCommand(sb.ToString(), myConnection);
            sqlCommand.Parameters.Add("@threadID", SqlDbType.Int);
            sqlCommand.Parameters["@threadID"].Value = threadID;
            name = sqlCommand.ExecuteScalar().ToString();

        }
        return name;
    }

    public static void UpdateCommentSpam(int commentID)
    {
        string myConnectionString = AllQuestionsPresented.connectionString;
        using (SqlConnection myConnection = new SqlConnection(myConnectionString))
        {
            SqlCommand sqlCommand = new SqlCommand("Spam", myConnection);
            sqlCommand.CommandType = CommandType.StoredProcedure;
            sqlCommand.Parameters.Add("@CommentID", DbType.Int32).Value = commentID;
            myConnection.Open();
            sqlCommand.ExecuteNonQuery(); 
        }
   
    }


    public static void UpdateThreadSpam(int threadID)
    {
        string myConnectionString = AllQuestionsPresented.connectionString;

      
        using (SqlConnection myConnection = new SqlConnection(myConnectionString))
        {
            SqlCommand sqlCommand = new SqlCommand("ThreadSpam", myConnection);
            sqlCommand.CommandType = CommandType.StoredProcedure;
            sqlCommand.Parameters.Add("@ThreadID", DbType.Int32).Value = threadID;
            myConnection.Open();
            sqlCommand.ExecuteNonQuery(); 
        }
     
    }

}