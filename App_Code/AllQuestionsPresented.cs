using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Text;
using System.Data.SqlClient;
using System.Web.Configuration;
using System.Data;
using System.Collections;
/// <summary>
/// Summary description for AllQuestionsPresented
/// </summary>
///   
/// 
[Serializable()]
public class AllQuestionsPresented
{
    public static readonly string connectionString = WebConfigurationManager.ConnectionStrings["CP_AllQuestionsAnswered"].ConnectionString;
    public static List<AllQuestionsPresented> GetAllThreads()
    {
        List<AllQuestionsPresented> allThreads = new List<AllQuestionsPresented>();
        string findUsersID = "SELECT ThreadsID FROM dbo.Threads ORDER BY ThreadsID DESC"; // "SELECT ThreadsID FROM Threads";
        string myConnectionString = AllQuestionsPresented.connectionString;

        using (SqlConnection myConnection = new SqlConnection(myConnectionString))
        {
            myConnection.Open();
            SqlCommand sqlCommand = new SqlCommand(findUsersID, myConnection);
            SqlDataReader reader = sqlCommand.ExecuteReader();
            while (reader.Read())
            {
                AllQuestionsPresented allQ = new AllQuestionsPresented((int)reader["ThreadsID"]);
                allThreads.Add(allQ);
            }
        }
        return allThreads;
    }

    public string Name{ get; set; }
    public string ThreadName { get; set; }
    public string Topic { get; set; }
    public string Subtopic { get; set; }
    public int Views { get; set; }
    public int Replies { get; set; }
    public int PageNumber { get; set; }
    public DateTime Time { get; set; }
    public int ThreadsID { get; set; }


	public AllQuestionsPresented()
	{
        Name = String.Empty;
        ThreadName = String.Empty;
        Views = 0;
        Replies = 0;
        Topic = String.Empty;
        Subtopic = String.Empty;
        Time = DateTime.MinValue;
	}

    public AllQuestionsPresented(int ThreadID)
    {
        StringBuilder sb = new StringBuilder();
        sb.Append("SELECT u.Name,t.ThreadTitle,t.Date, t.Views,t.Replies,p.Theme,p.Topics,t.PageNumber, t.ThreadsID");
        sb.Append("  FROM Threads AS t");
        sb.Append( " INNER JOIN Users AS u ON u.UsersID=t.UsersID");
        sb.Append( " INNER JOIN Topics AS p ON p.TopicsID=t.TopicsID");
        sb.Append(" WHERE t.ThreadsID=@ThreadsID");
    
        SqlConnection conn = new SqlConnection(AllQuestionsPresented.connectionString);
        SqlCommand cmd = new SqlCommand(sb.ToString(), conn);
        cmd.Parameters.Add("@ThreadsID", SqlDbType.Int).Value = ThreadID;
               int result3;
        try
        {
            conn.Open();
            SqlDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection);
     
            if (dr != null && dr.Read() && dr["Views"] != null && dr["PageNumber"] != null && dr["Replies"]!=null)
            {
                Name = dr["Name"].ToString();
                ThreadName = dr["ThreadTitle"].ToString();
                ThreadsID = (int.Parse(dr["ThreadsID"].ToString()));
                Views = (Int32.TryParse(dr["Views"].ToString(), out result3))?int.Parse(dr["Views"].ToString()):0;
                Replies = (Int32.TryParse(dr["Replies"].ToString(), out result3)) ? int.Parse(dr["Replies"].ToString()) : 0; 
                Topic = dr["Theme"].ToString();
                Subtopic = dr["Topics"].ToString();
                PageNumber = (Int32.TryParse(dr["PageNumber"].ToString(), out result3)) ? int.Parse(dr["PageNumber"].ToString())+1 : 0;
                Time =(DateTime) AllQuestionsPresented.TryParse(dr["Date"].ToString()); 
            }
            dr.Close();
        }
        finally
        {
            if (conn.State != ConnectionState.Closed)
            {
                conn.Close();
            }
        }

       
      
    }
    public static DateTime? TryParse(string text)
    {
        DateTime date;
        if (DateTime.TryParse(text, out date))
        {
            return date;
        }
        else
        {
            return null;
        }
    }
 
}