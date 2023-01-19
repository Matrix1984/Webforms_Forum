using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Data;
using System.Web.Configuration;
using System.Web.Security;
using System.Text;


public partial class AskQuestion : System.Web.UI.Page
{
    Dictionary<string, string[]> topic;

    public string Question { get { return QuestionTextBox1.Text; } private set { } }
    public string QDetails { get { return QuestionDetailsTextBox2.Text; } private set { } }
    public string Title { get; set; }
    protected void Page_Load(object sender, EventArgs e)
    {        
        //main criteria
        topic = new Dictionary<string,string[]>();

        topic["אומנות"] = new string[] { "ספרים", "ריקוד", "גינולוגיה", "היסטוריה", "פילוסופיה", "שירה", "תיאטרון ומשחק", "אומנות ויזואלית", "אחר" };
        topic["עסקים וכלכלה"] = new string[] { "משחק ושיווק", "קריירה", "עסקים", "מסים", "נדלן", "שכירות ומשכנתא", "ביטוח", "ניהול פיננסי אישי", "שוק ההון", "כלכלה", "ניהול פיננסי כלכלי", "משאבי אנוש", "חשבונאות", "אחר" };
        topic["תחבורה"] = new string[] { "מטוסים", "שייט", "מכירה וקנייה", "תיקונים ורכיבי מכוניות", "ביטוח", "רשיון", "חוקים", "רכבות", "סטטיסטיקה", "ביטחון", "אחר" };
        topic["מחשבים"] = new string[] { "רשתות", "חומרה", "אינטרנט", "פיתוח תוכנה", "מערכת הפעלה", "גרפיקה", "QA", "פיתוח למכשרים ניידים", "קניית ומכירת מחשבים", "ביטחון", "בתי ספר וקורסים", "מערכות מידע", "אחר" };
        topic["מוצרי חשמל"] = new string[] { "מצלמות", "מכשרים ניידים", "מכשרים למטבח", "מכשרים לבידור", "מכשרים לכביסה", "אחר" };
        topic["חינוך"] = new string[] { "השכלה יסודית", "השכלה חטיבת הבניים", "השכלה תיכונית", "אונברסיטה", "קורסים מקצועים", "חינוך מיוחד", "לימוד", "מבחנים", "לימודי חוץ", "לימוד פרטי", "אחר" };
        topic["בידור"] = new string[] { "טלויזיה", "אינטרנט", "שחקנים", "אנשים מפורסמים", "מוזיקה", "בילוים", "מופעים", "שעות פנאי", "אחר" };
        topic["משפחה ויחסים"] = new string[] { "ילדים", "הורים", "אחים", "סבים", "גיל הזהב", "חברים", "רומנטיקה", "אינטמיה", "התבגרות", "נישואים", "יחסים משפחתיים", "אחר" };
        topic["אוכל ושתייה"] = new string[] { "מתכונים", "אבזרי בישול", "מסעדות", "משקאות", "אוכל", "אוכל תרבותי", "אחר" };
        topic["משחקים וזמן פנוי"] = new string[] { "משחקי מחשב", "משחקי קונסולה", "משחקים לטלפונים ניידים", "משחקי ספורט", "משחקים חברותים", "אחר" };
        topic["בריאות"] = new string[] { "תרופות ", "מחלות", " רפואה", "אימונים", "יופי קוסמטי", "אחר" };
        topic["בית וגינון"] = new string[] { "גינה", "אביזרי בית", "דיקורציה", "שיפוצים", "קנייה ומכירת בית", "אחר" };
        topic["חדשות"] = new string[] { "אקטואליה", "תקשורת", "אחר" };
        topic["חיות בית"] = new string[] { "כלבים", "חתולים", "ציפורים", "מכרסמים", "דגים", "זוחלים", "סוסים וחמורים", "אחר" };
        topic["פוליטיקה"] = new string[] { "חקיקה", "משטרה", "ממשלה", "בתי משפט", "ראשי ממשלה", "שרים", "מפלגות", "הסטוריה פוליטית", "יחסי מדינות", "דיפלומטיה", "משטר", "צבא", "אסכולות פולטיות", "הסטוריה פוליטית", "החברה", "תרבות", "אחר" };
        topic["הריון והורות"] = new string[] { "הריון", "אימוץ", "גידול ילדים", "הורים", "אחר" };
        topic["מתמתיקה ומדע"] = new string[] { "אסטרונומיה", "פיזיקה", "גאוגרפיה", "כימיה", "ביולוגיה", "רפואה", "הנדסה", "מתמטיקה", "אחר" };
        topic["מדעי החברה"] = new string[] { "סוציולוגיה", "קרמינולוגיה", "פסיכולוגיה", "כלכלה", "פוליטיקה", "אנטרופולוגיה", "אחר" };
        topic["חברה ותרבות"] = new string[] { "תרבות", "חברה", "קהילה", "שפות", "דת", "אומנות", "מתולוגיה", "אתיקה", "אחר" };
        topic["ספורט"] = new string[] {"כדורסל","כדוריד","כדורגל","בסבול","קריקט","גולף","הוקי","מרוצים","אומניות לחימה","איגרוף","טריוויה","סנוקר","אולימפיה","שייט","גלישה","טניס","שחיה","ספורט מים",
        "ספורט חורף", "אחר"};
        topic["טיולים"] = new string[] { "טיולים", "חול", "כרטיסי טיסה", "מקומות", "אטרקציות", "אחר" };
        topic["צבא"] = new string[] { "יחידות","חוקים","גיוס","הסטוריה","נשקים","מטוסים","רכבי מלחמה","הסטוריה","אחר"};
    }

    protected void sendButton1_Click(object sender, EventArgs e)
    {
       
        if (QuestionDetailsTextBox2.Text.Length > 5000)
        {
            QuestionDetailsTextBox2.Text = "You cant enter more than 5000 characters";
        }
        else if(QuestionTextBox1.Text.Length > 100)
        {
            QuestionDetailsTextBox2.Text = "You cant enter more than 100 characters";
        }    
        else
        {
            if(Page.IsValid)
            {
              PutAllIntoDataBase();
              Title = QuestionTextBox1.Text;
              HttpContext.Current.Server.Transfer("~/AllQuestions.aspx");
            }
        
        }
    }

    string questionTitle;
    string questionParagraph;
    string mytopic;
    string subtopic;



    void PutAllIntoDataBase()
    {
     
        questionTitle = QuestionTextBox1.Text;
        questionParagraph = QuestionDetailsTextBox2.Text;

        mytopic = topicDropDownMenu.SelectedItem.Value;
        subtopic = SubTopicDropDownList.SelectedItem.Value;

        new AskQuestionAdo(questionTitle, questionParagraph, mytopic, subtopic).InsertUsers();
    }
  

 
    protected void topicDropDownMenu_SelectedIndexChanged1(object sender, EventArgs e)
    {
        SubTopicDropDownList.Items.Clear();

        string[] chosenItem = topic[topicDropDownMenu.SelectedItem.Value];

        foreach (string item in chosenItem)
        {
            SubTopicDropDownList.Items.Add(item);
        }
    }

class AskQuestionAdo
{
    public const string READERROR = "שגיאה 103" + ": " + "שגיאה בקריאת נתונים מהמערכת";
    string questionTitle;
    string questionParagraph;
    string topic;
    string subTopic;
 
   
    public AskQuestionAdo(string title, string details, string toc, string sub)
    {
        questionTitle = title;
        questionParagraph = details;
        topic = toc;
        subTopic = sub;   
    }


    public void InsertUsers()
    {
        SqlConnection sqlConnect = getSqlConnection();
        MembershipUser CurrentUser = Membership.GetUser();
        Guid id = (Guid)CurrentUser.ProviderUserKey;
        StringBuilder insertCommand = new StringBuilder();
 
        insertCommand.Append("DECLARE @TopicsId int; INSERT INTO Topics(Theme,Topics,Date)");
        insertCommand.Append("VALUES(@topic,@subTopic,GETDATE())");
        insertCommand.Append("SET @TopicsId = SCOPE_IDENTITY()");
     
        insertCommand.Append(" INSERT INTO Threads(UsersID,TopicsID,Date,ThreadTitle,ThreadParagraph,ThreadClosed,Views,Replies,PageNumber)");
        insertCommand.Append(" SELECT @uniqueIdentifier,@TopicsID,@Date,@questionTitle,@questionParagraph,0,0,0,FLOOR(Count(t.TopicsID)/20)");
        insertCommand.Append(" FROM Threads AS d INNER JOIN Topics AS t ON d.TopicsID=t.TopicsID");


        SqlCommand sqlCommand = new SqlCommand(insertCommand.ToString(), sqlConnect);

        sqlCommand.Parameters.Add("@uniqueIdentifier", SqlDbType.UniqueIdentifier, 30);
        sqlCommand.Parameters["@uniqueIdentifier"].Value = id;

        sqlCommand.Parameters.Add("@questionTitle", SqlDbType.NVarChar, 100);
        sqlCommand.Parameters["@questionTitle"].Value = questionTitle;


        sqlCommand.Parameters.Add("@questionParagraph", SqlDbType.NVarChar, 3000);
        sqlCommand.Parameters["@questionParagraph"].Value = questionParagraph;

        sqlCommand.Parameters.Add("@topic", SqlDbType.NVarChar, 50);
        sqlCommand.Parameters["@topic"].Value = topic;

        sqlCommand.Parameters.Add("@subTopic", SqlDbType.NVarChar, 50);
        sqlCommand.Parameters["@subTopic"].Value = subTopic;


        sqlCommand.Parameters.Add("@Date", SqlDbType.DateTime);
        sqlCommand.Parameters["@Date"].Value = DateTime.Now;

        try
        { 
            sqlConnect.Open();
            sqlCommand.ExecuteNonQuery();

  
        }
        catch (Exception x)
        {
            HttpContext.Current.Response.Redirect("~/ErrorPage.aspx?Error=" + READERROR+"&x="+x.StackTrace);
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
}
