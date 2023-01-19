using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Text;
using System.Data.SqlClient;
using System.Web.UI.DataVisualization.Charting;

public partial class Statistics : System.Web.UI.Page
{
    public int pageNumber { get { return ViewState["Page"] == null ? 0 : int.Parse(ViewState["Page"].ToString()); } set { ViewState["Page"] = value; } }
    Guid userid;
    protected void Page_Load(object sender, EventArgs e)//-System.Math.Abs(-1)
    {
        if (!IsPostBack)
        {
            MultiView1.ActiveViewIndex = 0;
        }
        string name = Request["x"];
        userid =(Guid) UsefulStaticMethods.GetUserNameFromUserGuid(name);
        CreateChartPie();
        SqlDataSource1.SelectParameters["Name"].DefaultValue = name;
    }
    protected void Next1_Click(object sender, EventArgs e)
    {
        if (MultiView1.ActiveViewIndex < 3)
        {
            pageNumber += 1;
            MultiView1.ActiveViewIndex = pageNumber;
        }
        else
        {
            MultiView1.ActiveViewIndex = 3;
        }
    }
    protected void Previous2_Click(object sender, EventArgs e)
    {
        if (pageNumber >= 0)
        {
            pageNumber -= 1;
            MultiView1.ActiveViewIndex = pageNumber;
        }
        else
        {
            MultiView1.ActiveViewIndex = 0;
        }

    }
    protected void AllQuestionAskedDataSource_Selecting(object sender, SqlDataSourceSelectingEventArgs e)
    {
       e.Command.Parameters["@UserID"].Value = userid;
       
    }

    public void CreateChartPie()
    {
        Chart1.Width = 500;
        Chart1.Height = 500;
        // Display 3D Pie Chart
        Chart1.Series[0].ChartType = SeriesChartType.Pie;
      //  Chart1.Series[0]["PieLabelStyle"] = "Inside";
        Chart1.ChartAreas[0].Area3DStyle.Enable3D = true;

        // Add Data to Display
        string[] xValues ={ "תשובות נכונות", "תשובות שניתנו"};
        int[] yValues = GetFiguresToSQL();
        Chart1.Series[0].Points.DataBindXY(xValues, yValues);
        // Call Out The Letter "D"
        // Display a Legend

        Chart1.Legends.Add(new Legend("Alphabet"));
        Chart1.Legends["Alphabet"].Title = "Index";
        Chart1.Series[0].Legend = "Alphabet";
        Chart1.Series[0]["PieLabelStyle"] = "Disabled";

        Chart1.Series[0].IsValueShownAsLabel = false;
    }

    public int[] GetFiguresToSQL()
    {

        StringBuilder sb = new StringBuilder();
        sb.Append("DECLARE @TotalQuestions int;");
        sb.Append(" DECLARE @CorrectQuestions int;");
        sb.Append(" DECLARE @IncorrectQuestions int;");

          sb.Append("SELECT @CorrectQuestions = COUNT( WiningComment) ");
          sb.Append(" FROM Threads");
          sb.Append(" WHERE WiningComment IN (SELECT CommentsID");
          sb.Append(" FROM Comments");
          sb.Append(" WHERE  UsersID=@UserID)");

        sb.Append(" SELECT @TotalQuestions =  COUNT(CommentsID)");
        sb.Append(" FROM  Comments");
        sb.Append(" WHERE  UsersID=@UserID");

        sb.Append("  SELECT  @IncorrectQuestions = (@TotalQuestions-@CorrectQuestions ) ");
        sb.Append(" Select @CorrectQuestions as 'WinningAnswers',");
        sb.Append(" @TotalQuestions as 'TotalAnswers',");
        sb.Append(" @IncorrectQuestions as 'IncorrectQuestions'");


    
        int winningAnswers =0 ;
        int nonWiningAnswers = 0;
        string myConnectionString = AllQuestionsPresented.connectionString;
        using (SqlConnection conn = new SqlConnection(AllQuestionsPresented.connectionString))
        {
            SqlCommand cmd = new SqlCommand(sb.ToString(), conn);
            conn.Open();
            cmd.Parameters.Add("@UserID", SqlDbType.UniqueIdentifier).Value = userid;
            SqlDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection);
            if (dr.HasRows)
            {
                dr.Read();
                winningAnswers = int.Parse(dr["WinningAnswers"].ToString());
                nonWiningAnswers = int.Parse(dr["IncorrectQuestions"].ToString());
            }
        }
        return new int[] { winningAnswers, nonWiningAnswers }; 
    }

  
     //   Server.Transfer("AnswerQuestion.aspx?x=" + int.Parse(DataList2.DataKeyField) + "&question=" + DataList2.SelectedValue.ToString() + "&time=" + DateTime.Now);
   


    protected void DataList1_ItemCommand(object source, DataListCommandEventArgs e)
    {
        LinkButton link = (LinkButton)e.Item.FindControl("LinkButton1");
        string threadTitle = link.Text;
    
        string recordID = (DataList1.DataKeys[e.Item.ItemIndex]).ToString();
        Server.Transfer("AnswerQuestion.aspx?x=" + recordID + "&question=" + threadTitle + "&time=" + DateTime.Now);
    }
    protected void DataList2_ItemCommand(object source, DataListCommandEventArgs e)
    {
        LinkButton link = (LinkButton)e.Item.FindControl("LinkButton2");
        string threadTitle = link.Text;

        string recordID = (DataList2.DataKeys[e.Item.ItemIndex]).ToString();
        Server.Transfer("AnswerQuestion.aspx?x=" + recordID + "&question=" + threadTitle + "&time=" + DateTime.Now);
    }


    protected void DetailsView1_ItemCommand(object sender, DetailsViewCommandEventArgs e)
    {
        DetailsView dv = (DetailsView)sender;
        LinkButton linkbtn = (LinkButton)dv.FindControl("LinkButton3");
        string threadID = e.CommandArgument.ToString();
        string threadName = linkbtn.Text;

        if (e.CommandName == "Select")
        {

            Server.Transfer("~/AnswerQuestion.aspx?x=" + threadID + "&question=" + threadName + "&time=" + DateTime.Now);
        }
    }

}