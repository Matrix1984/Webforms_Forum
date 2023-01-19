using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text;
using System.Data.SqlClient;
using System.Web.Security;
using System.Data;

public partial class PostUpdateQuestionParagraph : System.Web.UI.Page
{
    int threadID;
    string threadTitle;

    protected void Page_Load(object sender, EventArgs e)
    {
     
        threadID = int.Parse(this.Request["x"]);
        threadTitle = this.Request["question"].ToString();
        Label title = new Label();
        title.Text = threadTitle;
        PlaceHolder1.Controls.Add(title);
    }
    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        StringBuilder sb = new StringBuilder();
        sb.Append("UPDATE dbo.Threads");
        sb.Append(" SET ThreadParagraph=@ThreadParagraph");
        sb.Append(" WHERE ThreadsID=@ThreadID");


        using (SqlConnection conn = new SqlConnection(AllQuestionsPresented.connectionString))
        {
            SqlCommand cmd = new SqlCommand(sb.ToString(), conn);
            cmd.Parameters.Add("@ThreadID", SqlDbType.Int);
            cmd.Parameters["@ThreadID"].Value = threadID;
            cmd.Parameters.Add("@ThreadParagraph", SqlDbType.NVarChar);
            cmd.Parameters["@ThreadParagraph"].Value = Editor1.Content;
      
            conn.Open();
            cmd.ExecuteNonQuery();
        }

        HttpContext.Current.Server.Transfer("~/AnswerQuestion.aspx?x=" + threadID + "&question=" + threadTitle + "&time=" + DateTime.Now);
    }
}