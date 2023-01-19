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
using System.Security.Principal;
using System.Collections;
using System.Web.UI.HtmlControls;
using System.Runtime.Serialization;

public partial class AllQuestions : System.Web.UI.Page
{
    Dictionary<string, string[]> topic;
    DropDownList ModeratorSubTopicDLL;
    DropDownList ModeratorTopicDLL;
    Dictionary<DisplayAllQuestionsTable, int> forumsPages = new Dictionary<DisplayAllQuestionsTable, int>();
    ILookup<string, AllQuestionsPresented> groupedByTopic;
    List<AllQuestionsPresented> forumData;
    public bool SearchClicked { get { return ViewState["bool"] == null ? false : (bool)ViewState["bool"]; } set { ViewState["bool"] = value; } }
    public bool AnswerClicked { get { return ViewState["Bool"] == null ? false : (bool)ViewState["Bool"]; } set { ViewState["Bool"] = value; } }
    public bool FirstList { get { return ViewState["first"] == null ? false : (bool)ViewState["first"]; } set { ViewState["first"] = value; } }
    public string SubTopic { get { return ViewState["PreviousSubTopic"] == null ? null : (string)ViewState["PreviousSubTopic"]; } set { ViewState["PreviousSubTopic"] = value; } }
    public int NumberOfButtons { get { return ViewState["Pages"] == null ? 0 : (int)ViewState["Pages"]; } set { ViewState["Pages"] = value; } }
    public Dictionary<int, List<AllQuestionsPresented>> PostsDic { get { return ViewState["Posts"] == null ? null : (Dictionary<int, List<AllQuestionsPresented>>)ViewState["Posts"]; } set { ViewState["Posts"] = value; } }
    public string Topic_DLL_VState { get { return ViewState["Subtopic"]==null?null:ViewState["Subtopic"].ToString(); } set { ViewState["Subtopic"] = value; } }
    public int PageNumber { get { return ViewState["Number"] == null ? 0 : int.Parse(ViewState["Number"].ToString()); } set { ViewState["Number"] = value; } }
    public bool DeleteButtonClicked { get { return ViewState["Delete"]==null?false:(bool)ViewState["Delete"]; } set { ViewState["Delete"] = value; } }
    List<Button> buttons = new List<Button>();
    public List<int> ThreadID2Treat { get { return ViewState["Checked"] == null ? null : (List<int>)ViewState["Checked"]; } set { ViewState["Checked"] = value; } }
    protected void Page_Load(object sender, EventArgs e)
    {

      
        ThreadID2Treat = new List<int>();
        PlaceHolder1.Controls.Clear();
        forumData = AllQuestionsPresented.GetAllThreads();
        groupedByTopic = forumData.ToLookup(x => x.Subtopic);  
        topic = AllTopics();
        Control c = DisplayAllQuestionsTable.GetPostBackControl(this);
        if ( (HttpContext.Current.User.IsInRole("Administrator") || HttpContext.Current.User.IsInRole("Moderator")))
        {
            //ModeratorDropDownListSubtopic
            //ModeratorDropDownListTopic

           ModeratorTopicDLL = (DropDownList)LoginView2.FindControl("ModeratorDropDownListTopic");
           ModeratorSubTopicDLL = (DropDownList)LoginView2.FindControl("ModeratorDropDownListSubtopic");
           ModeratorTopicDLL.SelectedIndexChanged += topicDropDownMenuModerator_SelectedIndexChanged;
        }
        if (!IsPostBack)
        {
            ListFirst20Posts();
            SearchClicked = false;
        }
        else
        {
        
           

            if (SearchClicked && PostsDic!=null && !(c==topicDropDownMenu))
            {
                PlaceHolder1.Controls.Clear();
                for (int j = 0; j < PostsDic.Count; j++)
                {
                    PutInAllButtons(j);
                }

                if (FirstList && !(c==topicDropDownMenu))
                {
                    ListFirstPage();
                }
            
            }
            //Login Control
            if (HttpContext.Current.User.IsInRole("Administrator") || HttpContext.Current.User.IsInRole("Moderator"))
            {
              
                Button button = (Button)LoginView1.FindControl("DeleteThread");
            
                if (button == c)
                {
                   
                    button.Click += DeleteButton_Clicked;
                    if (PostsDic != null)
                    {
                        PlaceHolder1.Controls.Clear();
                        for (int j = 0; j < PostsDic.Count; j++)
                        {
                            PutInAllButtons(j);
                        }

                        ListFullPageByButton(PageNumber);
                    }
                    else
                    {
                        ListFirst20Posts();
                    }
                 
                 //   ThreadID2Treat.Clear();
                }
               
                Button button2 = (Button)LoginView2.FindControl("PassThreadTo");
                if (button2 == c)
                {
                    button2.Click += PassTo_Clicked;
                    if (PostsDic != null)
                    {
                        PlaceHolder1.Controls.Clear();
                        for (int j = 0; j < PostsDic.Count; j++)
                        {
                            PutInAllButtons(j);
                        }

                        ListFullPageByButton(PageNumber);
                    }
                    else
                    {
                        ListFirst20Posts();
                    }
                  
                  //  ThreadID2Treat.Clear();
                }
            }
        }
        Label labelToclreateSpace = new Label();
        labelToclreateSpace.Text = "<br/><br/><br/><br/><br/><br/><br/><br/><br/><br/><br/>";
        PlaceHolder3.Controls.Add(labelToclreateSpace);
     
    }


    void PutInAllButtons(int j)
    {
        Button pgs2 = new Button();//Create New Topic
        pgs2.Width = 20;
        pgs2.ID = "Button_Control" + j.ToString();
        pgs2.Command += obtainTopicsPerPage_Click;
        pgs2.EnableViewState = false;
        pgs2.CommandName = j.ToString();
        pgs2.Text = j.ToString();
        buttons.Add(pgs2);
        PlaceHolder1.Controls.Add(pgs2);
    }

    void ListFullPageByButton(int d)
    {
        foreach (var apply in PostsDic[d])
        {
            RadioButton ch = new RadioButton();
            ch.CheckedChanged += CheckBox1_CheckedChanged;
            ch.ID = apply.ThreadsID.ToString();
            ch.Attributes["onclick"] = "radioClicked(this)";
            ch.Attributes["how_many_clicked"] = "0";
            DisplayAllQuestionsTable objectToList = new DisplayAllQuestionsTable(this, apply.Name, apply.ThreadName, apply.Topic, apply.Subtopic, apply.Views, apply.Replies, apply.PageNumber, apply.Time, PlaceHolder2, apply.ThreadsID, ch);       
            objectToList.ExecuteAll();
        }
    }
    CheckBox checkbox;
    void ListFirst20Posts()
    {
        checkbox = new CheckBox();
        foreach (var item in groupedByTopic)
        {
            List<DisplayAllQuestionsTable> posts = new List<DisplayAllQuestionsTable>();
            IEnumerable<AllQuestionsPresented> ff = groupedByTopic[item.Key];
            foreach (var postInfo in ff)
            {
                RadioButton ch = new RadioButton();
                ch.CheckedChanged += CheckBox1_CheckedChanged;
                ch.ID = postInfo.ThreadsID.ToString();
                DisplayAllQuestionsTable objectToList = new DisplayAllQuestionsTable(this, postInfo.Name, postInfo.ThreadName, postInfo.Topic, postInfo.Subtopic, postInfo.Views, postInfo.Replies, postInfo.PageNumber, postInfo.Time, PlaceHolder2, postInfo.ThreadsID,ch);
                posts.Add(objectToList);//Adds post objects to a list
                forumsPages.Add(objectToList, postInfo.PageNumber);//Adds post objects and number to a page.
            
            }
            
        }
        int num=0;
        foreach (var item in forumsPages)
        {
            num++;
           item.Key.ExecuteAll();
         
            //Needs To Add All The dropDownlists & buttons here and put them into a placeholder.
           if (num>200)
           {
               break;
           }
        }
    }


    protected void topicDropDownMenu_SelectedIndexChanged(object sender, EventArgs e)
    {
        SubTopicDropDownList.Items.Clear();
        string[] chosenItem = topic[topicDropDownMenu.SelectedItem.Value];

        foreach (string item in chosenItem)
        {
            SubTopicDropDownList.Items.Add(item);
        }
    }

    protected void topicDropDownMenuModerator_SelectedIndexChanged(object sender, EventArgs e)
    {
        //DropDownList ModeratorSubTopicDLL;
        //DropDownList ModeratorTopicDLL;
        ModeratorSubTopicDLL.Items.Clear();
        string[] chosenItem = topic[ModeratorTopicDLL.SelectedItem.Value];

        foreach (string item in chosenItem)
        {
            ModeratorSubTopicDLL.Items.Add(item);
        }
    }

    public static Dictionary<string, string[]> AllTopics()
    {
        Dictionary<string, string[]> topic = new Dictionary<string, string[]>();

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
        topic["צבא"] = new string[] { "יחידות", "חוקים", "גיוס", "הסטוריה", "נשקים", "מטוסים", "רכבי מלחמה", "הסטוריה", "אחר" };

        return topic;
    }


  
    protected void Answer_Click(object sender, EventArgs e)
    {
        PostsDic = null;
        
       // ILookup<string, AllQuestionsPresented> groupedByTopic;
        IEnumerable<AllQuestionsPresented> allRelevantPosts;
        foreach (var item in groupedByTopic)
        {
            if (item.Key == SubTopicDropDownList.SelectedItem.Value)
            {
                allRelevantPosts= groupedByTopic[item.Key];
                SortAllReleventPostsByPageNumbers(allRelevantPosts);
            }
        }

        FirstList = true;
 
        PlaceHolder1.Controls.Clear();
     
        if (PostsDic != null)
        {
            PlaceHolder1.Controls.Clear();
            for (int j = 0; j < PostsDic.Count; j++)
            {
                Button pgs2 = new Button();//Create New Topic
                pgs2.Width = 20;
                pgs2.ID = "Button_Control" + j.ToString();
                pgs2.Command += obtainTopicsPerPage_Click;
                pgs2.EnableViewState = false;
                pgs2.CommandName = j.ToString();
                pgs2.Text = j.ToString();
                buttons.Add(pgs2);
                PlaceHolder1.Controls.Add(pgs2);
            }

            if (FirstList)
            {
                ListFirstPage();
                FirstList = false;
            }
        }
   
    }

    Dictionary<int, List<AllQuestionsPresented>> posts = new Dictionary<int, List<AllQuestionsPresented>>();
    int pageNumber;
    void SortAllReleventPostsByPageNumbers(IEnumerable<AllQuestionsPresented> postsToSort)
    {
        int pageNum=0;
       
        List<AllQuestionsPresented> tempList = new List<AllQuestionsPresented>();
        foreach (var item in postsToSort)
        {
            tempList.Add(item);
            pageNum++;
            if (pageNum == 20)
            {
                posts.Add(pageNumber, tempList);
                tempList = new List<AllQuestionsPresented>();
                pageNum = 0;
                pageNumber++;
            }
        }
        if (!posts.ContainsKey(pageNumber))
        {
            posts.Add(pageNumber, tempList);
        }
     
        putAllInViewState(posts);

    }

    void putAllInViewState(Dictionary<int, List<AllQuestionsPresented>> postsToViewState)
    {
        PostsDic = postsToViewState;
        NumberOfButtons = pageNumber;
        SearchClicked = true;
    }

    void obtainTopicsPerPage_Click(Object sender, CommandEventArgs e)
    {
    
        //Dictionary<int, List<AllQuestionsPresented>>
        foreach (var item in PostsDic)
        {
            if (item.Key.ToString() == e.CommandName)
            {
                PageNumber = int.Parse(e.CommandName);
                
                foreach (var apply in PostsDic[item.Key])
                {
                    RadioButton ch = new RadioButton();
                    ch.CheckedChanged += CheckBox1_CheckedChanged;
                    ch.ID = apply.ThreadsID.ToString();
                    ch.Attributes["onclick"] = "radioClicked(this)";
                    ch.Attributes["how_many_clicked"] = "0";
                    DisplayAllQuestionsTable objectToList = new DisplayAllQuestionsTable(this, apply.Name, apply.ThreadName, apply.Topic, apply.Subtopic, apply.Views, apply.Replies, apply.PageNumber, apply.Time, PlaceHolder2, apply.ThreadsID,ch);
                    objectToList.ExecuteAll();
                  
                }
            }
        }
    }

    void ListFirstPage()
    {
 
        //Dictionary<int, List<AllQuestionsPresented>>
        foreach (var item in PostsDic)
        {
           
           foreach (var apply in PostsDic[item.Key])
           {
               RadioButton ch = new RadioButton();
               ch.CheckedChanged += CheckBox1_CheckedChanged;
               ch.ID = apply.ThreadsID.ToString();
               ch.Attributes["onclick"] = "radioClicked(this)";
               ch.Attributes["how_many_clicked"] = "0";
               DisplayAllQuestionsTable objectToList = new DisplayAllQuestionsTable(this, apply.Name, apply.ThreadName, apply.Topic, apply.Subtopic, apply.Views, apply.Replies, apply.PageNumber, apply.Time, PlaceHolder2, apply.ThreadsID, ch);
              objectToList.ExecuteAll();
           } 
        }
    }
    protected void CheckBox1_CheckedChanged(object sender, EventArgs e)
    {
        var rad = (CheckBox)sender;
        if (rad.Checked)
        {
            int threadID = int.Parse(rad.ID.ToString());
            ThreadID2Treat.Add(threadID);
        }
    }
    public void DeleteButton_Clicked(object sender, EventArgs e)
    {
        foreach (var item in ThreadID2Treat)
        {
            UsefulStaticMethods.DeleteComments(item);
            UsefulStaticMethods.DeleteThreads(item);
        }
        Server.Transfer("~/AllQuestions.aspx");
    }

    public void PassTo_Clicked(object sender, EventArgs e)
    {  //DropDownList ModeratorSubTopicDLL;
        //DropDownList ModeratorTopicDLL;
        SubTopic = ModeratorSubTopicDLL.SelectedItem.Value;
        Topic_DLL_VState = ModeratorTopicDLL.SelectedItem.Value;
        if (SubTopic[0] != '-' && Topic_DLL_VState != null && ThreadID2Treat.Count!=0)
        {
            foreach (var item in ThreadID2Treat)
            {
                UsefulStaticMethods.UpdateTopic(SubTopic, Topic_DLL_VState, item);
            }
        }
      Server.Transfer("~/AllQuestions.aspx");
    }
}




public class DisplayAllQuestionsTable
{
        Table Tmain = new Table();
        TableRow threadRow;
        Label viewsLabel;
        Label repliesLabel;
        TableCell qTitle;
        TableCell qView;
        TableCell qReplies;
        TableCell qAvatar;
        TableCell moderatorDelete;
        HyperLink q;
        Label lastPostedBy;
        TableCell tCellLastPostedBy;
        TableCell tCellmoderaterControls;
        Dictionary<string, string[]> allTopics;
        AllQuestions page;
        string Name { get; set; }
        string ThreadName { get; set; }
        string Topic { get; set; }
        string Subtopic { get; set; }
        int Views { get; set; }
        int Replies { get; set; }
        int PageNumber { get; set; }  
        public int ThreadID { get; set; }
        PlaceHolder holder2;
        public DateTime Time{ get; set; }
        CheckBox checkbox;
        public DisplayAllQuestionsTable(AllQuestions formPage, string name, string ThreadTitle, string topic, string subtopics, int views, int replies, int pageNumber, DateTime time, PlaceHolder pholder, int threadID,RadioButton checkbo)
        {
            page = formPage;
            Name = name;
            ThreadName = ThreadTitle;
            Topic = topic;
            Subtopic = subtopics;
            Views = views;
            Replies = replies;
            PageNumber = pageNumber;
            Time = time;
            ThreadID = threadID;
            holder2 = pholder;
            checkbox = checkbo;
        }



        public void ExecuteAll()
        {
            CreateTable();

            feedInformation();
            CreateLabels();
            InitializeCells();
            AddControlsToCells();
            if (HttpContext.Current.User.IsInRole("Administrator") || HttpContext.Current.User.IsInRole("Moderator"))
            {

                AddModeratorControls();
                initializeManagerControls();
                AddModeraterCells();
                ModeraterAddCellsToRows();
            }


            AddCellsToRows();

            if (HttpContext.Current.User.IsInRole("Administrator") || HttpContext.Current.User.IsInRole("Moderator"))
            {

                ModeraterAddCellsToRowsDelete();
            }
            CreateTable();
            holder2.Controls.Add(Tmain);
          
        }

        public void AddModeratorControls()
        {

        }
        void feedInformation()
        {     
            q = new HyperLink();
            q.Text = ThreadName;
            q.NavigateUrl = "AnswerQuestion.aspx?x=" + ThreadID + "&question=" + ThreadName + "&time=" + Time;
            allTopics = new Dictionary<string, string[]>();
            allTopics = AllQuestions.AllTopics();
        }


     
       


        void CreateLabels()
        {
            //Call everytime a question is inserted.
            viewsLabel = new Label();
            repliesLabel = new Label();
            lastPostedBy = new Label();

            viewsLabel.Text = "צפיות" + "<br/>" + Views;
            repliesLabel.Text = "תגובות" + "<br/>" + Replies;
            lastPostedBy.Text = "בעל אשכול" + "<br/>" + Name;
        }

        void initializeManagerControls()
        {
            tCellmoderaterControls = new TableCell();
            moderatorDelete = new TableCell();

            tCellmoderaterControls.HorizontalAlign = System.Web.UI.WebControls.HorizontalAlign.Right;
        }

        void InitializeCells()
        {
            //Initialize all controls
            threadRow = new TableRow();
            qTitle = new TableCell();
            qView = new TableCell();
            qReplies = new TableCell();
            qAvatar = new TableCell();
            tCellLastPostedBy = new TableCell();

            qView.Text = Views.ToString();
            qReplies.Text = Replies.ToString();

            //Adding all controls
            qTitle.Width = 470;
            qTitle.HorizontalAlign = System.Web.UI.WebControls.HorizontalAlign.Right;
            tCellLastPostedBy.HorizontalAlign = System.Web.UI.WebControls.HorizontalAlign.Center;
            qView.HorizontalAlign = System.Web.UI.WebControls.HorizontalAlign.Center;
            qReplies.HorizontalAlign = System.Web.UI.WebControls.HorizontalAlign.Center;

            qTitle.VerticalAlign = System.Web.UI.WebControls.VerticalAlign.Middle;
            tCellLastPostedBy.VerticalAlign = System.Web.UI.WebControls.VerticalAlign.Middle;
            qView.VerticalAlign = System.Web.UI.WebControls.VerticalAlign.Middle;
            qReplies.VerticalAlign = System.Web.UI.WebControls.VerticalAlign.Middle;
            qTitle.Controls.Add(q);
        }

        void AddControlsToCells()
        {
            //Call everytime a question in inserted.

            qReplies.Controls.Add(repliesLabel);
            qView.Controls.Add(viewsLabel);
            tCellLastPostedBy.Controls.Add(lastPostedBy);
        }
        public CheckBox GetCheckBx()
        {
            return checkbox;
        }
        void AddModeraterCells()
        {
            tCellmoderaterControls.Controls.Add(checkbox);
        }

        void ModeraterAddCellsToRows()
        {
            threadRow.Cells.Add(tCellmoderaterControls);
        }
   
        void AddCellsToRows()
        {
            //Call everytime a question is inserted
            threadRow.Cells.Add(qView);
            threadRow.Cells.Add(qReplies);
            threadRow.Cells.Add(tCellLastPostedBy);
            threadRow.Cells.Add(qAvatar);
            threadRow.Cells.Add(qTitle);
            Tmain.Rows.Add(threadRow);
        }

        void ModeraterAddCellsToRowsDelete()
        {
            threadRow.Cells.Add(moderatorDelete);
        }

        void CreateTable()
        {
            //Call only once
            Tmain.Width = 1000;
            Tmain.BorderColor = System.Drawing.Color.Black;
            Tmain.BorderWidth = 1;
            Tmain.CellPadding = 3;
        }

        public static System.Web.UI.Control GetPostBackControl(System.Web.UI.Page page)
        {
            Control control = null;
            string ctrlname = page.Request.Params["__EVENTTARGET"];
            if (ctrlname != null && ctrlname != String.Empty)
            {
                control = page.FindControl(ctrlname);
            }
            // if __EVENTTARGET is null, the control is a button type and we need to 
            // iterate over the form collection to find it
            else
            {
                string ctrlStr = String.Empty;
                Control c = null;
                foreach (string ctl in page.Request.Form)
                {
                    // handle ImageButton controls ...
                    if (ctl.EndsWith(".x") || ctl.EndsWith(".y"))
                    {
                        ctrlStr = ctl.Substring(0, ctl.Length - 2);
                        c = page.FindControl(ctrlStr);
                    }
                    else
                    {
                        c = page.FindControl(ctl);
                    }
                    if (c is System.Web.UI.WebControls.Button ||
                                c is System.Web.UI.WebControls.ImageButton)
                    {
                        control = c;
                        break;
                    }
                }
            }
            return control;
        }

    }

