using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text;
using System.Data.SqlClient;
using System.Data;

public partial class PickTheBestAnswer : System.Web.UI.Page
{
    int threadID;
    int commentID;
    string threadName;

    protected void Page_Load(object sender, EventArgs e)
    {
        threadID = int.Parse(Request["x"].ToString());
        commentID = int.Parse(Request["CommentID"].ToString());
        threadName = Request["question"].ToString();
        Label1.Text = "("+"סיבה לתשובה הטובה ביותר:(פרט כאן";
        Label title = new Label();
        title.Text = threadName + "<br/><br/>";
        Label answer = new Label();
        answer.Text = GetInfoFromPreviousPage();
        PlaceHolder1.Controls.Add(answer);
        PlaceHolder1.Controls.Add(title);
    }

    string GetInfoFromPreviousPage()
    {
        return PreviousPage!= null?PreviousPage.WiningComment:"";
    }
    int RadioButtonSelected()
    {
        int itemselected = int.Parse(RadioButtonList1.SelectedItem.Value);
        return itemselected;
    }


    public void CheckReputationIfNull()
    {
        StringBuilder sb = new StringBuilder();
        sb.Append("DECLARE @isReputationNull int");
        sb.Append(" SET @isReputationNull=( SELECT Reputation");
        sb.Append(" FROM Users u");
        sb.Append(" INNER JOIN Comments c ON c.UsersID = u.UsersID");
        sb.Append(" WHERE c.CommentsID = @CommentsID)");

        sb.Append(" BEGIN IF ( @isReputationNull IS NULL)");
        sb.Append("UPDATE u ");
        sb.Append(" SET Reputation = 0");
        sb.Append(" FROM Users u");
        sb.Append(" END");

        using (SqlConnection conn = new SqlConnection(AllQuestionsPresented.connectionString))
        {
            SqlCommand cmd = new SqlCommand(sb.ToString(), conn);
            cmd.Parameters.Add("@CommentsID", SqlDbType.Int).Value = commentID;
            conn.Open();
            cmd.ExecuteNonQuery();
        }
    }

    public void InsertUserReputation()
    {
        CheckReputationIfNull();
        StringBuilder sb = new StringBuilder();
        sb.Append("UPDATE u ");
        sb.Append(" SET Reputation = (u.Reputation + @Reputation)");
        sb.Append(" FROM Users u");
        sb.Append(" INNER JOIN Comments c ON c.UsersID = u.UsersID");
        sb.Append(" WHERE c.CommentsID = @CommentsID");

        using (SqlConnection conn = new SqlConnection(AllQuestionsPresented.connectionString))
        {
            SqlCommand cmd = new SqlCommand(sb.ToString(), conn);
            cmd.Parameters.Add("@Reputation", SqlDbType.Int).Value = RadioButtonSelected();
            cmd.Parameters.Add("@CommentsID", SqlDbType.Int).Value = commentID;  
            conn.Open();
            cmd.ExecuteNonQuery();
        }
    }
    public void InsertComment()
    {
        StringBuilder sb = new StringBuilder();
        sb.Append("UPDATE dbo.Threads");
        sb.Append(" SET WiningComment=@WiningComment");
        sb.Append(" WHERE ThreadsID=@ThreadID"); ;
        //sb.Append("UPDATE dbo.Threads");
        //sb.Append(" SET ThreadParagraph=@ThreadParagraph");
        //sb.Append(" WHERE ThreadsID=@ThreadID");
        using (SqlConnection conn = new SqlConnection(AllQuestionsPresented.connectionString))
        {
            SqlCommand cmd = new SqlCommand(sb.ToString(), conn);
            cmd.Parameters.Add("@WiningComment", SqlDbType.Int).Value = commentID;
            cmd.Parameters.Add("@ThreadID", SqlDbType.Int).Value = threadID;
            conn.Open();
            cmd.ExecuteNonQuery();
        }
    }

    public void CloseThread()
    {
        StringBuilder sb = new StringBuilder();
        sb.Append("UPDATE dbo.Threads");
        sb.Append(" SET ThreadClosed=@ThreadClosed");
        sb.Append(" WHERE ThreadsID=@ThreadID"); ;
        //sb.Append("UPDATE dbo.Threads");
        //sb.Append(" SET ThreadParagraph=@ThreadParagraph");
        //sb.Append(" WHERE ThreadsID=@ThreadID");
        using (SqlConnection conn = new SqlConnection(AllQuestionsPresented.connectionString))
        {
            SqlCommand cmd = new SqlCommand(sb.ToString(), conn);
            cmd.Parameters.Add("@ThreadClosed", SqlDbType.Int).Value =1;
            cmd.Parameters.Add("@ThreadID", SqlDbType.Int).Value = threadID;
            conn.Open();
            cmd.ExecuteNonQuery();
        }
    }

    public void UpdateWinningRemark()
    {
        StringBuilder sb = new StringBuilder();
        sb.Append("UPDATE dbo.Threads");
        sb.Append(" SET WinningRemark=@WinningRemark");
        sb.Append(" WHERE ThreadsID=@ThreadID"); ;
        //sb.Append("UPDATE dbo.Threads");
        //sb.Append(" SET ThreadParagraph=@ThreadParagraph");
        //sb.Append(" WHERE ThreadsID=@ThreadID");
        using (SqlConnection conn = new SqlConnection(AllQuestionsPresented.connectionString))
        {
            SqlCommand cmd = new SqlCommand(sb.ToString(), conn);
            cmd.Parameters.Add("@WinningRemark", SqlDbType.NVarChar).Value = TextBox1.Text;
            cmd.Parameters.Add("@ThreadID", SqlDbType.Int).Value = threadID;
            conn.Open();
            cmd.ExecuteNonQuery();
        }
    }

    protected void Button1_Click(object sender, EventArgs e)
    {
        InsertUserReputation();
        InsertComment();
        CloseThread();
        UpdateWinningRemark();
        HttpContext.Current.Server.Transfer("~/AnswerQuestion.aspx?x=" + threadID + "&question=" + threadName + "&time=" + DateTime.Now);
    }
}