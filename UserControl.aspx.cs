using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Net.Mail;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using System.Web.Security;

public partial class UserControl : System.Web.UI.Page
{
    List<ControlPanelMessages> allComments;
    string name;
    Guid userid;
    string currentUserPosting;
    protected void Page_Load(object sender, EventArgs e)
    {
        name=Request["x"];
        Guid id = UsefulStaticMethods.GetUserNameFromUserGuid(name);
        MembershipUser CurrentUser = Membership.GetUser();
        userid = (Guid)CurrentUser.ProviderUserKey;
        if (id != userid)
        {
            GridView1.Columns[0].Visible = false;
            TableCell fileuploadCell = (TableCell)Master.FindControl("TableCell6");
            fileuploadCell.Visible = false;
        }
        int controlPanelsID = UsefulStaticMethods.GetControlPanelID(name);
        allComments = ControlPanelMessages.GetAllControlPanelPosts(controlPanelsID);
        currentUserPosting = UsefulStaticMethods.ControlPanelGetName(userid);
    }


    protected void Button1_Click(object sender, EventArgs e)
    {
        StringBuilder sb = new StringBuilder();
        sb.Append("DECLARE @ControlPaneliD int");
        sb.Append(" SET @ControlPaneliD=(SELECT ControlPanelID");
        sb.Append(" FROM ControlPanel");
        sb.Append(" WHERE Name=@Name)");

        //sb.Append("DECLARE @Commentator NVARCHAR(50)");
        //sb.Append(" SET @Commentator=(SELECT Name");
        //sb.Append(" FROM Users");
        //sb.Append(" WHERE UsersID=@UserID)");
      
        sb.Append("INSERT INTO dbo.CP_Comments (ControlPanelID,Comments,Commentator)");
        sb.Append(" VALUES(@ControlPaneliD,@Comment,@Commentator)");
        MembershipUser CurrentUser = Membership.GetUser();
        Guid id = (Guid)CurrentUser.ProviderUserKey;

        string myConnectionString = AllQuestionsPresented.connectionString;
        using (SqlConnection conn = new SqlConnection(AllQuestionsPresented.connectionString))
        {
            conn.Open();
            SqlCommand cmd = new SqlCommand(sb.ToString(), conn);
            cmd.Parameters.Add("Commentator", SqlDbType.NVarChar).Value = currentUserPosting;
            cmd.Parameters.Add("Comment", SqlDbType.NVarChar).Value = TextBox1.Text;
            cmd.Parameters.Add("Name", SqlDbType.NVarChar).Value = name;
            cmd.ExecuteNonQuery();

        }
        TextBox1.Text = "";
        Server.Transfer("~/UserControl.aspx?x="+name);
    }
    protected void GridView1_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        //if (this.GridView1.Rows.Count < this.GridView1.PageSize)
        //{
            foreach (GridViewRow tt in GridView1.Rows)
            { tt.Height = Unit.Pixel(30); }

        //}
    }

    protected void GridView1_RowCreated(object sender, GridViewRowEventArgs e)
    {
     
            foreach (GridViewRow tt in GridView1.Rows)
            { tt.Height = Unit.Pixel(30); }

      
    }
    protected void GridView1_RowDeleted(object sender, GridViewDeletedEventArgs e)
    {
        Server.Transfer("~/UserControl.aspx?x=" + name);
    }
    protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        GridView1.DataBind();
    }
}

class ObjectDataSourceClass
{

    public static DataSet GetCustomerTable()
    {

        return new DataSet();
    }
}