using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Security;
using System.Data.SqlClient;
using System.Text;
using System.Data;
using System.IO;
using System.Drawing;



public partial class CP : System.Web.UI.MasterPage
{
    Guid userid;
    string name;
    protected void Page_Load(object sender, EventArgs e)
    {
        //MembershipUser CurrentUser = Membership.GetUser();
        //userid = (Guid)CurrentUser.ProviderUserKey;
        try
        {
            name = Request.QueryString["x"];
            userid = UsefulStaticMethods.GetUserNameFromUserGuid(name);
        }
        catch (Exception ex)
        {
            if (Session["Name"] != null)
            {
                name = Session["Name"].ToString();
                userid = UsefulStaticMethods.GetUserNameFromUserGuid(name);
            }
        }
            HyperLink1.NavigateUrl = "~/UserControl.aspx?x=" + GetUsersName();
         if (Roles.IsUserInRole("Administrator"))
         {
             TableRow9.Visible = true;
             Inbox.Click += AdministratorInbox_Clicked;
             Spam.Click += AdministratorSpam_Clicked;
             ComposeNewMessage.Click += AdministratorNewMessage_Clicked;
             TableRow10.Visible = true;
             TableRow11.Visible = true;
         }
         else if (Roles.IsUserInRole("Moderator"))
         {
             TableRow9.Visible = true;
             Spam.Click += ModeratorSpam_Clicked;
             Inbox.Click += ModeratorInbox_Clicked;
             ComposeNewMessage.Click += ModeratorNewMessage_Clicked;
             TableRow10.Visible = true;
             TableRow11.Visible = true;
         }
         else
         {
             TableRow10.Visible = false;
             TableRow9.Visible = false;
             TableRow11.Visible = false;
         }

        GetUsersDetails(userid);
        GetUsersThreadCount(userid);
        CreateATable(Name, QA, Reputation);
        string imgSource=UsefulStaticMethods.GetAnImage(userid);
        if (imgSource!=null)
        {
            Image1.ImageUrl = ResolveUrl(imgSource);
        }
    }

    public void AdministratorNewMessage_Clicked(object sender, EventArgs e)
    {
        Server.Transfer("~/Administrator/NewMessage.aspx");
    }
    public void ModeratorNewMessage_Clicked(object sender, EventArgs e)
    {
        Server.Transfer("~/Moderator/NewMessage.aspx");
    }


    public void ModeratorInbox_Clicked(object sender, EventArgs e)
    {
        Server.Transfer("~/Moderator/ModeratorInbox.aspx");
    }

    public void ModeratorSpam_Clicked(object sender, EventArgs e)
    {
       
        Server.Transfer("~/Moderator/Spam.aspx");
    }

    public void AdministratorSpam_Clicked(object sender, EventArgs e)
    {
        Server.Transfer("~/Administrator/Spam.aspx");
    }

    public void AdministratorInbox_Clicked(object sender, EventArgs e)
    {
        Server.Transfer("~/Administrator/AdministratorInbox.aspx");
    }

    public void Stats_Clicked(object sender, EventArgs e)
    {
        Server.Transfer("~/Statistics.aspx?x=" + name);
    }
    public void CreateATable(string name, int questionsAnswered, int reputation)
    {
        Label usersName = new Label();
        usersName.Text = name;
        Label qa = new Label();
        qa.Text = questionsAnswered.ToString();
        Label reputtion = new Label();
        reputtion.Text = reputation.ToString();
      
        TableCell4.Controls.Add(usersName);
        TableCell8.Controls.Add(qa);
        TableCell10.Controls.Add(reputtion);     
    }
    public string GetUsersName()
    {
        MembershipUser CurrentUser = Membership.GetUser();
        Guid userid = (Guid)CurrentUser.ProviderUserKey;

        return UsefulStaticMethods.GetUsersControlPanelName(userid);
    }
    public string Name { get; set; }
    public int QA { get; set; }
    public int Reputation { get; set; }
    public string Img { get; set; }

    public void GetUsersDetails(Guid i)
    {
        StringBuilder sb=new StringBuilder();
        sb.Append("SELECT Name, Reputation, Avatar");
        sb.Append(" FROM Users");
        sb.Append(" WHERE UsersID=@UserID");
        int result3;
     
        using (SqlConnection conn = new SqlConnection(AllQuestionsPresented.connectionString))
        {
            SqlCommand cmd = new SqlCommand(sb.ToString(),conn);
            cmd.Parameters.Add("UserID", SqlDbType.UniqueIdentifier).Value = i;
            conn.Open();
            SqlDataReader dr = cmd.ExecuteReader();
            dr.Read();
            Name = dr["Name"].ToString();
            Reputation = (Int32.TryParse(dr["Reputation"].ToString(), out result3)) ? int.Parse(dr["Reputation"].ToString()) : 0;
            //Img = dr["Avatar"]; 
        }
    }
    public void GetUsersThreadCount(Guid i)
    {
        StringBuilder sb = new StringBuilder();
      
        sb.Append(" SELECT COUNT(t.threadsID) as ThreadCount");
        sb.Append(" FROM Threads AS t");
        sb.Append(" INNER JOIN Users AS u ON u.UsersID=t.UsersID");
        sb.Append(" WHERE u.UsersID=@UserID");
        int result3;
        string myConnectionString = AllQuestionsPresented.connectionString;
        using (SqlConnection conn = new SqlConnection(AllQuestionsPresented.connectionString))
        {
            conn.Open();
            SqlCommand cmd = new SqlCommand(sb.ToString(), conn);
            cmd.Parameters.Add("UserID", SqlDbType.UniqueIdentifier).Value = i;
            SqlDataReader dr = cmd.ExecuteReader();
            dr.Read();
            QA = (Int32.TryParse(dr["ThreadCount"].ToString(), out result3)) ? int.Parse(dr["ThreadCount"].ToString()) : 0;
        
        }
    }
   
    protected void Button1_Click(object sender, EventArgs e)
    {
          int size = FileUpload1.PostedFile.ContentLength/1024;
          if (FileUpload1.HasFile && size < 100)
          {


              string ImgName = FileUpload1.FileName;

              FileUpload1.SaveAs(Server.MapPath("~").ToString() + "\\" + ImgName);

              Stream imgStream = FileUpload1.PostedFile.InputStream;

              Bitmap bmThumb = new Bitmap(imgStream);

              System.Drawing.Image im = bmThumb.GetThumbnailImage(100, 100, null, IntPtr.Zero);

              im.Save(Server.MapPath("~").ToString() + "\\ThumbNail_" + ImgName);
              Img = ImgName;
              InsertAnImage(userid);
              Label lbl = new Label();
              lbl.Text += "הקובץ הועלה בהצלחה אנא רענן את הדף או המשך לגלוש";
              TableCell12.Controls.Add(lbl);
          }
          else
          {
              Label lbl = new Label();
              lbl.Text += "100 * ההלאה נכשלה!! בדוק אם הקובץ יותר קטן מ 100 קילו בייט. רק תמונות בגודל 100";
              lbl.Text +=  "פיקסל יראו היטב";
              TableCell12.Controls.Add(lbl);
              
          }
  
    }

    public void InsertAnImage(Guid i)
    {
        StringBuilder sb = new StringBuilder();

        sb.Append("UPDATE dbo.Users");
        sb.Append(" SET Avatar=@pic");
        sb.Append(" WHERE UsersID=@UserID");
        Stream stream = FileUpload1.FileContent;
        StreamReader reader = new StreamReader(stream);
       
        string myConnectionString = AllQuestionsPresented.connectionString;
        using (SqlConnection conn = new SqlConnection(AllQuestionsPresented.connectionString))
        {
            SqlCommand cmd = new SqlCommand(sb.ToString(), conn);
            cmd.Parameters.Add("@pic", SqlDbType.NVarChar).Value = Img;
            cmd.Parameters.Add("UserID", SqlDbType.UniqueIdentifier).Value = i;
            conn.Open();
            cmd.ExecuteNonQuery();
        }
    }




}
