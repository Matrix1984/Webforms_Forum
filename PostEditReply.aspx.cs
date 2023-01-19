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

public partial class PostEditReply : System.Web.UI.Page
{
    int threadID;
    string threadTitle;
    int commentID;
    protected void Page_Load(object sender, EventArgs e)
    {
        commentID=int.Parse(this.Request["commentID"]);
        threadID = int.Parse(this.Request["x"]);
        threadTitle = this.Request["question"].ToString();
        Label title = new Label();
        title.Text = threadTitle;
        PlaceHolder1.Controls.Add(title);
    }



    protected void btnSubmit_Click(object sender, EventArgs e)
    {
     
        StringBuilder sb = new StringBuilder();
        sb.Append("UPDATE dbo.Comments");
        sb.Append(" SET Comments=@Comment");
        sb.Append(" WHERE CommentsID=@CommentID");


        using (SqlConnection conn = new SqlConnection(AllQuestionsPresented.connectionString))
        {
            SqlCommand cmd = new SqlCommand(sb.ToString(), conn);
            cmd.Parameters.Add("@CommentID", SqlDbType.Int);
            cmd.Parameters["@CommentID"].Value = commentID;

            cmd.Parameters.Add("@ThreadID", SqlDbType.Int);
            cmd.Parameters["@ThreadID"].Value = threadID;
            cmd.Parameters.Add("@Comment", SqlDbType.NVarChar);
            cmd.Parameters["@Comment"].Value = Editor1.Content;
            MembershipUser CurrentUser = Membership.GetUser();
            Guid i = (Guid)CurrentUser.ProviderUserKey;
            cmd.Parameters.Add("@UserID", SqlDbType.UniqueIdentifier);
            cmd.Parameters["@UserID"].Value = i;
            conn.Open();
            cmd.ExecuteNonQuery();
        }

        HttpContext.Current.Server.Transfer("~/AnswerQuestion.aspx?x=" + threadID + "&question=" + threadTitle + "&time=" + DateTime.Now);
    }
}