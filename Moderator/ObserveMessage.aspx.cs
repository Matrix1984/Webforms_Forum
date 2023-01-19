using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Data;

public partial class Moderator_ObserveMessage : System.Web.UI.Page
{
    public string MessageTitles { get; set; }
    public string MessageContent { get; set; }
    public string MessageFrom { get; set; }
    protected void Page_Load(object sender, EventArgs e)
    {
        int messageID=int.Parse(Request["MessageID"].ToString());
        GetMessage(messageID);
        MessageTitleCell.Text = MessageTitles;
        MessageCell.Text ="<br/>"+ MessageContent;
        SenderCell.Text =  MessageFrom + ":" + "שולח" + "<br/>";
    }
    protected void Button1_Click(object sender, EventArgs e)
    {
        Server.Transfer("~/Moderator/NewMessage.aspx");
    }

    public void GetMessage(int messageID)
    {
        string myConnectionString = AllQuestionsPresented.connectionString;

        using (SqlConnection myConnection = new SqlConnection(myConnectionString))
        {
            SqlCommand sqlCommand = new SqlCommand("GetMessage", myConnection);
            sqlCommand.CommandType = CommandType.StoredProcedure;
            sqlCommand.Parameters.Add("@MessageID", SqlDbType.Int).Value = messageID;
            myConnection.Open();
            SqlDataReader reader = sqlCommand.ExecuteReader();
            if (reader.HasRows)
            {//Messages, MessageTitle, UsersID 
                reader.Read();
                MessageTitles = reader["MessageTitle"].ToString();
                MessageContent = reader["Messages"].ToString();
                MessageFrom= ModeratorUsefulFunctions.GetMessageUsersName((Guid)reader["UsersID"]);
            }
        }
    }



}