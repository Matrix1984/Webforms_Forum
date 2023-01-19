using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Net.Mail;
using System.Data.SqlClient;
using System.Data;
using System.Web.Configuration;
using System.Text;
using System.Web.Security;


public partial class AnswerQuestion : System.Web.UI.Page
{
    public bool ListAllPosts { get { return ViewState["ListAllTopics"] == null ? false : (bool)ViewState["ListAllTopics"]; } set { ViewState["ListAllTopics"] = value; } }
    public bool FirstListAllPosts { get { return ViewState["ListAllTopics2"] == null ? false : (bool)ViewState["ListAllTopics2"]; } set { ViewState["ListAllTopics2"] = value; } }
    public static readonly string connectionString = WebConfigurationManager.ConnectionStrings["YourGuruDB"].ConnectionString;
    List<Comments> commentList;
    public int ThreadID { get; set; }
    public string QuestionRequest { get; set; }
    public DateTime Time { get; set; }
    public string WiningComment { get; set; }
    public bool BestAnswerChosen { get; set; }


    protected void Page_Load(object sender, EventArgs e)
    {
        ThreadID = int.Parse(this.Request["x"]);
        try
        {

            Time = DateTime.Parse(this.Request["time"]);
        }
        catch (Exception efadsfasdfasdfasdfasd)
        {
        }
        QuestionRequest = this.Request["question"].ToString();


        if (!IsPostBack)
        {
            UsefulStaticMethods.UpdateThreadView(ThreadID);
        }
       
      
        title.Text = QuestionRequest;
        PlaceHolder2.Controls.Add(title);

            ExecuteAll(ThreadID);
   
    }
    Label title;
    void Page_PreInit(object sender, EventArgs e)
    {
        title = new Label();
        title.SkinID = "Blue";
    }
    ThreadTable thread;

    public void ExecuteAll(int threadID)
    {
        commentList = Comments.GetAllComments(threadID);
        CheckWiningComment();
        FirstTimeload();
        if (BestAnswerChosen)
        {
            PutWinningPost();
        }
        ListAllTopics();
    
    }

    int commentID;
    Comments cm;
    string winningRemark;
    void CheckWiningComment()
    {
        cm = new Comments(0);
        StringBuilder sb = new StringBuilder();
        sb.Append("SELECT WiningComment, WinningRemark");
        sb.Append(" FROM Threads");
        sb.Append(" WHERE ThreadsID=@ThreadID");
        int result3 = 0;
        using (SqlConnection conn = new SqlConnection(AllQuestionsPresented.connectionString))
        {
            SqlCommand cmd = new SqlCommand(sb.ToString(), conn);
            cmd.Parameters.Add("ThreadID", SqlDbType.Int).Value = ThreadID;
            conn.Open();
            SqlDataReader reader = cmd.ExecuteReader();
            reader.Read();
            if (reader.HasRows)
            {
                commentID = (Int32.TryParse(reader["WiningComment"].ToString(), out result3)) ? int.Parse(reader["WiningComment"].ToString()) : 0;
                winningRemark = reader["WinningRemark"].ToString();
                if (commentID != 0)
                {
                    BestAnswerChosen = true;
                    cm = new Comments(commentID);
                }
            }
        }
    }
    void PutWinningPost()
    {
   

        string posterImage = UsefulStaticMethods.GetAnImageToComment(cm.CommentID);
        posterImage = posterImage != null ? ResolveUrl(posterImage) : null;
        thread = new ThreadTable(this, cm.Comment, ThreadID, cm.UserName, cm.Reputation,posterImage, cm.Time, cm.CommentID, Time, BestAnswerChosen, winningRemark);
        thread.CommentaddTimeHeaderLabel();
        thread.WinnerAddComment();
        thread.addUserLogoComment();
        thread.AddCommentsButtons();
        thread.myTable1();
        PlaceHolder1.Controls.Add(thread.myTable());
    }

    bool checkIfUserSubmittedComment()
    {
        MembershipUser CurrentUser = Membership.GetUser();
        Guid i = (Guid)CurrentUser.ProviderUserKey;
        bool userDoesntExist = true;
        foreach (var item in commentList)
        {
            if (item.UserID == i)
            {
                userDoesntExist = false;
            }
        }
        return userDoesntExist;
    }

    void ListAllTopics()
    {
        foreach (var item in commentList)
        {
            if (cm.CommentID != item.CommentID)
            {
                string posterImage = UsefulStaticMethods.GetAnImageToComment(item.CommentID);
                
                if ((HttpContext.Current.User.IsInRole("Administrator") || HttpContext.Current.User.IsInRole("Moderator")))
                {
                    Button deleteBtn = new Button();
                    deleteBtn.Text += "מחק";
                    deleteBtn.CommandName = item.CommentID.ToString();
                    deleteBtn.Command += deleteBtn_Click;

                    Button banBtn= new Button();
                    banBtn.Text += "באן";
                    banBtn.CommandName = item.CommentID.ToString();
                    banBtn.Command += banBtnCommentator_Click;


                    posterImage = posterImage != null ? ResolveUrl(posterImage) : null;
                    thread = new ThreadTable(this, item.Comment, ThreadID, item.UserName, item.Reputation, posterImage, item.Time, item.CommentID, Time, BestAnswerChosen, winningRemark, deleteBtn, banBtn);
                }
                else
                {

                    Button spam = new Button();
                    spam.Text += "ספאם";
                    spam.CommandName = item.CommentID.ToString();


                    thread = new ThreadTable(this, item.Comment, ThreadID, item.UserName, item.Reputation, posterImage, item.Time, item.CommentID, Time, BestAnswerChosen, winningRemark, spam, QuestionRequest);
                }
                thread.CommentaddTimeHeaderLabel();
                thread.AddComment();
                thread.addUserLogoComment();
                thread.AddCommentsButtons();
                thread.myTable1();
                PlaceHolder1.Controls.Add(thread.myTable());
            }
        }
    }

    public void banBtnCommentator_Click(object sender, CommandEventArgs e)
    {
        //GetThe usersName
        //Roles.IsUserInRole();
        UsefulStaticMethods.BanUserCommentator(int.Parse(e.CommandName));
        Server.Transfer("~/AnswerQuestion.aspx?x=" + ThreadID + "&time=" + Time + "&question=" + QuestionRequest);
    }

    public void banBtnThreader_Click(object sender, CommandEventArgs e)
    {
        UsefulStaticMethods.BanUserThreader(int.Parse(e.CommandName));
        Server.Transfer("~/AnswerQuestion.aspx?x=" + ThreadID + "&time=" + Time + "&question=" + QuestionRequest);
    }

    public void deleteBtn_Click(object sender, CommandEventArgs e)
    {
        UsefulStaticMethods.UpdateThreadReplies(ThreadID);
        UsefulStaticMethods.DeleteComment(int.Parse(e.CommandName));
        Server.Transfer("~/AnswerQuestion.aspx?x=" + ThreadID + "&time=" + Time + "&question=" + QuestionRequest);
    }



    void FirstTimeload()
    {
        Button banBtn = new Button();
        banBtn.Text += "באן";
        banBtn.CommandName = ThreadID.ToString();
        banBtn.Command += banBtnThreader_Click;

        Button spam = new Button();
        spam.Text += "ספאם";
        spam.CommandName = ThreadID.ToString();

        int i = ThreadID;
        string userName = UsefulStaticMethods.FindUserNameOfThread(ThreadID);
        string imgURL = UsefulStaticMethods.GetAnImageToThread(ThreadID) == null ? null : ResolveUrl(UsefulStaticMethods.GetAnImageToThread(ThreadID));
        thread = new ThreadTable(this, ThreadID, PlaceHolder1, QuestionRequest, checkIfUserSubmittedComment(), BestAnswerChosen, imgURL, banBtn, userName, spam);
        thread.ExecuteContent();
        thread.addTimeHeaderLabel();
        thread.AddComment();
        thread.addUserLogoTitle();
        thread.addAllButtonsPost();
        thread.myTable1();
        PlaceHolder1.Controls.Add(thread.myTable());
    }


    public void addedResponse()
    {
        string messageFromPoster = "";
        EmailUser(messageFromPoster);
    }

    void EmailUser(string msg)
    {
        MailMessage message = new MailMessage()
        {
            Subject = "קיבלת הודעה חדשה מאת",
            Body = msg
        };
        message.To.Add(new MailAddress("makovetskiyd@yahoo.co.uk"));
        message.IsBodyHtml = true;
        SmtpClient client = new SmtpClient();
        client.EnableSsl = true;
        client.Send(message);
    }

}





    class ThreadTable
    {
        Table tabl = new Table();
        Label UsersMessage;
        DateTime now;
        TableRow tablRowTime;
        TableRow tablRowButtons;
        TableRow tablRowLabel;
        string threadTitle;
        string threadParagraph;
        AnswerQuestion page;
        public string UserName { get; set; }
        public Image Image { get; set; }
        public string ThreadTitle { get; set; }
        public string ThreadParagraph { get; set; }
        public DateTime CommentTime { get; set; }
        public int Reputation { get; set; }
        TableCell tablCellButtons;
        Button edit;
        Button reply;
        public int ThreadID { get; set; }
        PlaceHolder placeH;
        public string ThreadName { get; set; }
        public int CommentID { get; set; }
        public DateTime ThreadTime { get; set; }
        public bool UserDidntPost { get; set; }
        public static DateTime threadsTime;
        public bool BestAnswerChosen { get; set; }
        public string WinningRemark { get; set; }
        Button deleteButton;
        Image myimage = new System.Web.UI.WebControls.Image();
        Button ban;
        Button spam;

        public ThreadTable(AnswerQuestion AQ, string ThreadP, int threadD, string userName, int reputation, string m, DateTime time, int commentID, DateTime time2, bool bestAnswer, string winningRemark, Button button, Button banBtn)
        {
            tablRowLabel = new TableRow();
            page = AQ;
            ThreadID = threadD;
            UserName = userName;
            Reputation = reputation;
            ThreadParagraph = ThreadP;
            CommentTime = time;
            ThreadTime = time2;
            CommentID = commentID;
            if (m != null) { myimage.ImageUrl = m; }
            else
            {
                myimage.ImageUrl = "~/YourGuruAvatar.png";

            }
            BestAnswerChosen = bestAnswer;
            WinningRemark = winningRemark;
            deleteButton = button;
            ban = banBtn;
        
        }

        public ThreadTable(AnswerQuestion AQ, string ThreadP, int threadD, string userName, int reputation, string m, DateTime time, int commentID, DateTime time2, bool bestAnswer, string winningRemark, Button spamBtn, string title)
        {
         
            tablRowLabel = new TableRow();
            page = AQ;
            ThreadID = threadD;
            UserName = userName;
            Reputation = reputation;
            ThreadParagraph = ThreadP;
            CommentTime = time;
            ThreadTime = time2;
            CommentID = commentID;
            if (m != null) { myimage.ImageUrl = m; }
            else
            {
                myimage.ImageUrl = "~/YourGuruAvatar.png";
       
            }
            BestAnswerChosen = bestAnswer;
            WinningRemark = winningRemark;
            spam = spamBtn;
            spam.Command += spamCommentBtn_Click;
            ThreadName = title;
        }
        // thread = new ThreadTable(this, ThreadID, PlaceHolder1, QuestionRequest, checkIfUserSubmittedComment(), BestAnswerChosen, imgURL, banBtn, userName, spam);
        public ThreadTable(AnswerQuestion AQ, int threadD, PlaceHolder h, string title, bool UserExistsCantSeeAnswerButton, bool bestAnswer, string m, Button banButon, string userName, Button spamBtn)
        {
            tablRowLabel = new TableRow();
            page = AQ;
            ThreadID = threadD;
            placeH = h;
            ThreadName = title;
            UserDidntPost = UserExistsCantSeeAnswerButton;
            BestAnswerChosen = bestAnswer;
            if (m != null) { myimage.ImageUrl = m; }
           else
            {
                myimage.ImageUrl = "~/YourGuruAvatar.png";
            }
            ban = banButon;
            UserName = userName;
            spam = spamBtn;
            spam.Command += spamThreadBtn_Click;
        }

        public ThreadTable(AnswerQuestion AQ, string ThreadP, int threadD, string userName, int reputation, string m, DateTime time, int commentID, DateTime time2, bool bestAnswer, string winningRemark)
        {

            tablRowLabel = new TableRow();
            page = AQ;
            ThreadID = threadD;
            UserName = userName;
            Reputation = reputation;
            ThreadParagraph = ThreadP;
            CommentTime = time;
            ThreadTime = time2;
            CommentID = commentID;
            if (m != null) { myimage.ImageUrl = m; }
            else
            {
                myimage.ImageUrl = "~/YourGuruAvatar.png";

            }
            BestAnswerChosen = bestAnswer;
            WinningRemark = winningRemark;
      
        }




        public void myTable1()
        {
            tabl.Width = 1000;
            tabl.BorderWidth = 1;
            tabl.Rows.Add(tablRowTime);
            tabl.Rows.Add(tablRowLabel);
            tabl.Rows.Add(tablRowButtons);
        }
        public Table myTable()
        {
            return tabl;
            //   page.Form.Controls.Add(tabl);

        }

        public void ExecuteContent()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("SELECT TOP 1 u.Avatar,t.Date, u.Name, t.ThreadTitle, t.ThreadParagraph");
            sb.Append(" FROM Threads AS t");
            sb.Append(" INNER JOIN Users AS u ON u.UsersID=t.UsersID");
            sb.Append(" WHERE ThreadsID=@ThreadID");

            using (SqlConnection conn = new SqlConnection(AnswerQuestion.connectionString))
            {
                conn.Open();
                SqlCommand sqlComm = new SqlCommand(sb.ToString(), conn);
                sqlComm.Parameters.Add("ThreadID", SqlDbType.Int).Value = ThreadID;
                SqlDataReader dr = sqlComm.ExecuteReader();

                if (dr.Read())
                {
                    UserName = dr["Name"].ToString();
                 
                    ThreadTitle = dr["ThreadTitle"].ToString();
                    ThreadParagraph = dr["ThreadParagraph"].ToString();
                    ThreadTime = (DateTime)AllQuestionsPresented.TryParse(dr["Date"].ToString());
                    threadsTime = ThreadTime;
                }
            }

        }
        Guid g;
        bool IsCurrentUserComment()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("SELECT u.UsersID");
            sb.Append(" FROM Users AS u");
            sb.Append(" INNER JOIN Comments AS c ON c.UsersID=u.UsersID");
            sb.Append(" INNER JOIN Threads AS t ON c.ThreadsID=t.ThreadsID");
            sb.Append(" WHERE c.CommentsID=@CommentID AND c.ThreadsID=@ThreadID");
                       
            using (SqlConnection conn = new SqlConnection(AllQuestionsPresented.connectionString))
            {
                SqlCommand cmd = new SqlCommand(sb.ToString(), conn);
                cmd.Parameters.Add("@ThreadID", SqlDbType.Int).Value = ThreadID;
                cmd.Parameters.Add("@CommentID", SqlDbType.Int).Value = CommentID;
                conn.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                reader.Read();
                if (reader.HasRows)
                {
                    g =(Guid)reader["UsersID"];
                }
            }
            MembershipUser CurrentUser = Membership.GetUser();
            Guid i = (Guid)CurrentUser.ProviderUserKey;
            return (i==g)?true:false;
        }

        bool IsCurrentUserTitle()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("SELECT t.UsersID");
            sb.Append(" FROM Users AS u");
            sb.Append(" INNER JOIN Threads AS t ON t.UsersID=u.UsersID");
            sb.Append(" WHERE t.ThreadsID=@ThreadID");

            using (SqlConnection conn = new SqlConnection(AllQuestionsPresented.connectionString))
            {
                SqlCommand cmd = new SqlCommand(sb.ToString(), conn);
                cmd.Parameters.Add("@ThreadID", SqlDbType.Int).Value = ThreadID;
                conn.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                reader.Read();

                g = (Guid)reader["UsersID"];
            }
            MembershipUser CurrentUser = Membership.GetUser();
            Guid i = (Guid)CurrentUser.ProviderUserKey;
            return (i == g) ? true : false;
        }

        bool CalculateTime()
        {
            bool fourHoursPassed = threadsTime.AddHours(4) < DateTime.Now;
            return fourHoursPassed;
        }

        public void addTimeHeaderLabel()
        {
            tablRowTime = new TableRow();
            TableCell tablCellTime = new TableCell();
            Label timeNow = new Label();
            timeNow.Text = UsefulStaticMethods.TimeNow(ThreadTime);
            tablCellTime.Controls.Add(timeNow);
            tablCellTime.HorizontalAlign = System.Web.UI.WebControls.HorizontalAlign.Right;
            tablRowTime.BackColor = System.Drawing.Color.Black;
            tablRowTime.ForeColor = System.Drawing.Color.White;
            tablCellTime.ColumnSpan = 2;
            tablRowTime.Cells.Add(tablCellTime);
        }
        public void CommentaddTimeHeaderLabel()
        {
            tablRowTime = new TableRow();
            TableCell tablCellTime = new TableCell();
            Label timeNow = new Label();

            timeNow.Text = UsefulStaticMethods.TimeNow(CommentTime);
            tablCellTime.Controls.Add(timeNow);
            tablCellTime.HorizontalAlign = System.Web.UI.WebControls.HorizontalAlign.Right;
            tablRowTime.BackColor = System.Drawing.Color.Black;
            tablRowTime.ForeColor = System.Drawing.Color.White;
            tablCellTime.ColumnSpan = 2;
            tablRowTime.Cells.Add(tablCellTime);
        }

        public void addUserLogoTitle()
        {
            int trhead = ThreadID;
            TableCell tablCellUserLogo = new TableCell();
            Label emptylblSeperator = new Label();
            emptylblSeperator.Text = "<br/>";
            Guid titleGuid = UsefulStaticMethods.GetUsersControlPanelNameTitleGuid(ThreadID);
            HyperLink hp = new HyperLink();
            hp.NavigateUrl = "UserControl.aspx?x=" + UsefulStaticMethods.ControlPanelGetName(titleGuid);
            hp.Text = "<br/>" + UserName;//Users Name
            tablCellUserLogo.Controls.Add(myimage);
            tablCellUserLogo.Controls.Add(emptylblSeperator);
            tablCellUserLogo.Controls.Add(hp);
            tablCellUserLogo.VerticalAlign = System.Web.UI.WebControls.VerticalAlign.Top;
            tablRowLabel.Cells.Add(tablCellUserLogo);
            tablRowLabel.HorizontalAlign = System.Web.UI.WebControls.HorizontalAlign.Center;
        }

        public void addUserLogoComment()
        {

            TableCell tablCellUserLogo = new TableCell();
            Label emptylblSeperator = new Label();
            emptylblSeperator.Text = "<br/>";
            Guid commentGuid = UsefulStaticMethods.GetUsersControlPanelNameCommentGuid(CommentID);
            HyperLink hp = new HyperLink();
            hp.NavigateUrl = "UserControl.aspx?x=" + UsefulStaticMethods.ControlPanelGetName(commentGuid);
            hp.Text = "<br/>" + UserName;//Users Name
            tablCellUserLogo.Controls.Add(myimage);
            tablCellUserLogo.Controls.Add(emptylblSeperator);
            tablCellUserLogo.Controls.Add(hp);
            tablCellUserLogo.VerticalAlign = System.Web.UI.WebControls.VerticalAlign.Top;
            tablRowLabel.Cells.Add(tablCellUserLogo);
            tablRowLabel.HorizontalAlign = System.Web.UI.WebControls.HorizontalAlign.Center;
        }

        public void AddComment()
        {
            Label newMessage = new Label();
            newMessage.Text = ThreadParagraph;// "הודעה נתקבלה";threadParagraph
            TableCell tablCellLabel = new TableCell();
            tablCellLabel.Height = 80;
            tablCellLabel.Width = 900;
            tablCellLabel.Controls.Add(newMessage);
            tablCellLabel.HorizontalAlign = System.Web.UI.WebControls.HorizontalAlign.Right;
            tablCellLabel.VerticalAlign = System.Web.UI.WebControls.VerticalAlign.Top;
            tablRowLabel.Cells.Add(tablCellLabel);

        }

        public void WinnerAddComment()
        {

            Label newMessage = new Label();
            newMessage.Text = "Winner's Answer:<br/><br/>" + ThreadParagraph + "<br/><br/><br/><br/>" + WinningRemark;// "הודעה נתקבלה";threadParagraph
            TableCell tablCellLabel = new TableCell();          
            tablCellLabel.Height = 80;
            tablCellLabel.Width = 900;
            tablCellLabel.Controls.Add(newMessage);
            tablCellLabel.HorizontalAlign = System.Web.UI.WebControls.HorizontalAlign.Right;
            tablCellLabel.VerticalAlign = System.Web.UI.WebControls.VerticalAlign.Top;
            tablRowLabel.Cells.Add(tablCellLabel);

        }

        Button quote;
        //Author
        public void addAllButtonsPost()
        {
            tablRowButtons = new TableRow();
            tablCellButtons = new TableCell();
            reply = new Button();
            edit = new Button();

            if (UserDidntPost && !BestAnswerChosen && !IsCurrentUserTitle())
            {
                reply.Text = "ענה על השאלה";
                reply.Click += reply_Click;
                //Table Buttons
                tablCellButtons.Controls.Add(reply); 
                tablCellButtons.ColumnSpan = 2;
                tablRowButtons.Cells.Add(tablCellButtons);
                tablCellButtons.HorizontalAlign = System.Web.UI.WebControls.HorizontalAlign.Left;
            }

            if ( !BestAnswerChosen && !((HttpContext.Current.User.IsInRole("Administrator") || HttpContext.Current.User.IsInRole("Moderator"))))
            {
 
                if (!(Roles.IsUserInRole(UserName, "Moderator") || Roles.IsUserInRole(UserName, "Administrator")))
                {
                    tablCellButtons.Controls.Add(spam);
                    tablRowButtons.Cells.Add(tablCellButtons);
                    tablCellButtons.HorizontalAlign = System.Web.UI.WebControls.HorizontalAlign.Left;
                }
            }
           

            if (IsCurrentUserTitle() || ((HttpContext.Current.User.IsInRole("Administrator") || HttpContext.Current.User.IsInRole("Moderator"))))
            {
                if (!BestAnswerChosen)
                {
                    edit.Text = "ערוך";
                    edit.Click += editTitle_Click;
                    //Table Buttons
                    tablCellButtons.Controls.Add(edit);
                    tablCellButtons.ColumnSpan = 2;
                    tablRowButtons.Cells.Add(tablCellButtons);
                    tablCellButtons.HorizontalAlign = System.Web.UI.WebControls.HorizontalAlign.Left;
                }
            }
          

            //only for moderators
            if ((HttpContext.Current.User.IsInRole("Administrator") || HttpContext.Current.User.IsInRole("Moderator")))
            {
                string[] roles = Roles.GetRolesForUser(UserName);
                if (roles.Length == 0)
                {
                    tablCellButtons.Controls.Add(ban);
                }
                tablRowButtons.Cells.Add(tablCellButtons);
                tablCellButtons.HorizontalAlign = System.Web.UI.WebControls.HorizontalAlign.Left;
            }
          
          //  tablCellButtons.Controls.Add(spam);
        }
        //Users
        public void AddCommentsButtons()
        {
            tablRowButtons = new TableRow();
            tablCellButtons = new TableCell();
            edit = new Button();
            Button pickTheBestAnswer = new Button();
            //If 4 hours passed and it is the main user!!! IsCurrentUserTitle()
            if (CalculateTime() && IsCurrentUserTitle() && !BestAnswerChosen)
            {
                pickTheBestAnswer.Text = "בחר את התשובה";
                pickTheBestAnswer.Click += pickTheBestAnswer_Click;
                tablCellButtons.Controls.Add(pickTheBestAnswer);
                tablCellButtons.ColumnSpan = 2;
                tablRowButtons.Cells.Add(tablCellButtons);
                tablCellButtons.HorizontalAlign = System.Web.UI.WebControls.HorizontalAlign.Left;
            }

            if (IsCurrentUserComment() && !BestAnswerChosen || (HttpContext.Current.User.IsInRole("Administrator") || HttpContext.Current.User.IsInRole("Moderator")))
            {
                edit.Click += edit_Click;
                //Table Buttons
             
                tablCellButtons.Controls.Add(edit);
                tablCellButtons.ColumnSpan = 2;
                //Controls properties.
                edit.Text = "ערוך";
                tablRowButtons.Cells.Add(tablCellButtons);
                tablCellButtons.HorizontalAlign = System.Web.UI.WebControls.HorizontalAlign.Left;
            }

            if (!IsCurrentUserComment() && spam != null && !((HttpContext.Current.User.IsInRole("Administrator") || HttpContext.Current.User.IsInRole("Moderator"))))
            {
                tablCellButtons.Controls.Add(spam);
            }

            if (deleteButton != null && ban != null && ((HttpContext.Current.User.IsInRole("Administrator") || HttpContext.Current.User.IsInRole("Moderator")))) 
            {
               
                tablCellButtons.Controls.Add(deleteButton);
                string[] roles = Roles.GetRolesForUser(UserName);
                 if (roles.Length == 0)
                {
                    tablCellButtons.Controls.Add(ban);
                }
            }
            tablRowButtons.Cells.Add(tablCellButtons);
        }

        public void pickTheBestAnswer_Click(object sender, EventArgs e)
        {
            page.WiningComment = ThreadParagraph;//ThreadID
            HttpContext.Current.Server.Transfer("~/PickTheBestAnswer.aspx?x=" + ThreadID + "&question=" + ThreadName + "&commentID=" + CommentID);
        }

        public void editTitle_Click(object sender, EventArgs e)
        {
            HttpContext.Current.Server.Transfer("~/PostUpdateQuestionParagraph.aspx?x=" + ThreadID + "&question=" + ThreadName);
        }

        public void reply_Click(object sender, EventArgs e)
        {
            HttpContext.Current.Server.Transfer("~/PostEdit.aspx?x=" + ThreadID + "&question=" + ThreadName);
        }

        public void edit_Click(object sender, EventArgs e)
        {
            HttpContext.Current.Server.Transfer("~/PostEditReply.aspx?x=" + ThreadID + "&question=" + ThreadName + "&commentID=" + CommentID);
        }

        public void spamCommentBtn_Click(object sender, CommandEventArgs e)
        {
            string ss = ThreadName;
            UsefulStaticMethods.UpdateCommentSpam(CommentID);// "AnswerQuestion.aspx?x=" + ThreadID + "&question=" + ThreadName + "&time=" + Time;
            HttpContext.Current.Server.Transfer("AnswerQuestion.aspx?x=" + ThreadID + "&question=" + ThreadName + "&time=" + DateTime.Now);
        }
        public void spamThreadBtn_Click(object sender, CommandEventArgs e)
        {
            UsefulStaticMethods.UpdateThreadSpam(ThreadID);
            HttpContext.Current.Server.Transfer("AnswerQuestion.aspx?x=" + ThreadID + "&question=" + ThreadName + "&time=" + DateTime.Now);
        }
    }


