using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text;
using System.Data.SqlClient;
using System.Data;
using System.Web.Security;

public partial class PostEdit : System.Web.UI.Page
{
    int threadID;
    string threadTitle;
    protected void Page_Load(object sender, EventArgs e)
    {
        threadID = int.Parse(this.Request["x"]);
        threadTitle = this.Request["question"].ToString();
        Label title=new Label();
        title.Text=threadTitle;
        PlaceHolder1.Controls.Add(title);
    }

    public void UpdateThreadView(int threadID)
    {
        StringBuilder sb = new StringBuilder();
        sb.Append("UPDATE  dbo.Threads");
        sb.Append(" SET Replies=Replies+1");
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

    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        UpdateThreadView(threadID);
        StringBuilder sb = new StringBuilder();
        sb.Append("INSERT INTO Comments(UsersID, ThreadsID, Date, Comments)");
        sb.Append(" SELECT u.UsersID,@ThreadID,GETDATE(),@Comment");
        sb.Append(" FROM Users AS u");
        sb.Append(" WHERE u.UsersID=@UserID ");


        using (SqlConnection conn = new SqlConnection(AllQuestionsPresented.connectionString))
        {
            SqlCommand cmd = new SqlCommand(sb.ToString(), conn);
            cmd.Parameters.Add("@ThreadID", SqlDbType.Int);
            cmd.Parameters["@ThreadID"].Value = threadID;// ThreadID;
            cmd.Parameters.Add("@Comment", SqlDbType.NVarChar);
            cmd.Parameters["@Comment"].Value = Editor1.Content;
            MembershipUser CurrentUser = Membership.GetUser();
            Guid i = (Guid)CurrentUser.ProviderUserKey;

            cmd.Parameters.Add("@UserID", SqlDbType.UniqueIdentifier);
            cmd.Parameters["@UserID"].Value = i;
            conn.Open();
            cmd.ExecuteNonQuery();
        }

        HttpContext.Current.Server.Transfer("~/AnswerQuestion.aspx?x=" + threadID + "&question=" + threadTitle+"&time="+DateTime.Now);
    }
}