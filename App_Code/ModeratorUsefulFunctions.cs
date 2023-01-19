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
/// Summary description for ModeratorUsefulFunctions
/// </summary>
public class ModeratorUsefulFunctions
{
	public ModeratorUsefulFunctions()
	{
	
	}
    //    @UserID uniqueIdentifier,
    //@Message nvarchar(Max),
    //@MessageTitle nvarchar(200)
    public static void InsertNewMessage(Guid g, string messageTitle, string messageText, string messageTo)
    {
        string myConnectionString = AllQuestionsPresented.connectionString;

        using (SqlConnection myConnection = new SqlConnection(myConnectionString))
        {
            SqlCommand sqlCommand = new SqlCommand("InsertInboxMessage", myConnection);
        sqlCommand.CommandType = CommandType.StoredProcedure;
            sqlCommand.Parameters.Add("@UserID", SqlDbType.UniqueIdentifier).Value = g;
            sqlCommand.Parameters.Add("@Message", SqlDbType.NVarChar).Value = messageText;
            sqlCommand.Parameters.Add("@MessageTitle", SqlDbType.NVarChar).Value = messageTitle;
            sqlCommand.Parameters.Add("@MessageTo", SqlDbType.NVarChar).Value = messageTo;
            myConnection.Open();
            sqlCommand.ExecuteNonQuery();
        }
    }

    public static string GetMessageUsersName(Guid g)
    {
        string myConnectionString = AllQuestionsPresented.connectionString;
        string name = "";
        using (SqlConnection myConnection = new SqlConnection(myConnectionString))
        {
       
            SqlCommand sqlCommand = new SqlCommand("GetSendersNameFromGuid", myConnection);
            sqlCommand.CommandType = CommandType.StoredProcedure;
            sqlCommand.Parameters.Add("@UsersGuid", SqlDbType.UniqueIdentifier).Value = g;
            myConnection.Open();
            SqlDataReader reader = sqlCommand.ExecuteReader();
            if (reader.HasRows)
            {
                reader.Read();
               name= reader["Name"].ToString();
            }
        }
        return name;
    }
}