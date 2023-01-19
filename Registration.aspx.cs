using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Net.Mail;
using System.Configuration;
using System.Net;
using System.Data.SqlClient;
using System.Data;
using System.Web.Configuration;
using System.Web.Security;
using System.Web.Management; 

public partial class Registration : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        Image1.ImageUrl = "~/header.png";
       string s= Request.QueryString["d"];
    }

    
    protected void Page_Init(object sender, EventArgs e)
    {


    }

    protected void entry_Click(object sender, EventArgs e)
    {
   
    }
    static readonly string subject="אימות אימייל";
    static readonly string body = "בכדי לאמת את האמייל. לחץ על הקישור הבא";

    protected void submitButton_Click(object sender, EventArgs e)
    {

    }



    protected void CreateUserWizard1_CreatedUser(object sender, EventArgs e)
    {

        MembershipUser CurrentUser = Membership.GetUser(CreateUserWizard1.UserName, true);
       
        Guid i = (Guid)CurrentUser.ProviderUserKey;
        //System.Guid.NewGuid()
        RegisterAdo.SearchUser(i, CreateUserWizard1.UserName);  
    }

    protected void Login1_LoggedIn(object sender, EventArgs e)
    {
 
    }




class RegisterAdo
{
    public const string READERROR = "שגיאה 101" + ": " + "שגיאה בהזנת נתונים למערכת";
    public const string WRITE_ERROR = "שגיאה 102" + ": " + "שגיאה בקריאת נתונים מהמערכת";
    
    public static void InsertUsers(Guid UsersIDentity, string userName)
    {
       
              using (SqlConnection connection = getSqlConnection())
              {
                  try
                  {
                      DataSet userDataset = new DataSet();

                      string insertCommand = "INSERT INTO Users (UsersID, Name) VALUES" + "(CONVERT(uniqueidentifier, @uniqueIdentifier), @userName )";
                      SqlDataAdapter myCommand = new SqlDataAdapter(insertCommand, connection);

                      myCommand.SelectCommand.Parameters.Add("@uniqueIdentifier", SqlDbType.UniqueIdentifier, 30);
                      myCommand.SelectCommand.Parameters["@uniqueIdentifier"].Value = UsersIDentity;
                      myCommand.SelectCommand.Parameters.Add("@userName", SqlDbType.VarChar, 30);
                      myCommand.SelectCommand.Parameters["@userName"].Value = userName;
                      myCommand.Fill(userDataset);                
                  }
                  catch (Exception x)
                  {
                      HttpContext.Current.Response.Redirect("~/ErrorPage.aspx?Error=" + WRITE_ERROR+"<br/>"+x);

                  }
              }
              UsefulStaticMethods.InsertControlPanel(UsersIDentity, userName);
    }

 

    public static void SearchUser(Guid UsersIDentity, string userName)
    {
        SqlConnection sqlConnect = getSqlConnection();
        string searchAllUsersID = "SELECT Users.UsersID FROM Users WHERE Users.UsersID=@uniqueIdentifier";
        SqlCommand checkExistingUsersID = new SqlCommand(searchAllUsersID , sqlConnect);
        checkExistingUsersID.Parameters.Add("@uniqueIdentifier", SqlDbType.UniqueIdentifier, 30);
        checkExistingUsersID.Parameters["@uniqueIdentifier"].Value = UsersIDentity;

        SqlDataReader reader;
   
            sqlConnect.Open();
            reader = checkExistingUsersID.ExecuteReader();
          
            if (!reader.HasRows)
            {
                InsertUsers(UsersIDentity,userName);
            }
            reader.Close();
            try
            {
        }
        catch (Exception ex)
        {
            HttpContext.Current.Response.Redirect("~/ErrorPage.aspx?Error=" + READERROR);
        }
        finally
        {
            sqlConnect.Close();
        }
    }


    public static SqlConnection getSqlConnection()
    {
        string myConnectionString = WebConfigurationManager.ConnectionStrings["YourGuruDB"].ConnectionString;
        return new SqlConnection(myConnectionString);
    }
}



    protected void Login1_Authenticate(object sender, AuthenticateEventArgs e)//Login1_Authenticate
    {

        if (UsefulStaticMethods.CheckIfUserISbanned(Login1.UserName))
        {
            Server.Transfer("~/Banned.aspx");
        }
        else
        {
            //authenticate...
            e.Authenticated = true;
        }

    }
}