using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Security;

public partial class Moderator_ModeratorInbox : System.Web.UI.Page
{
   
    protected void Page_Load(object sender, EventArgs e)
    {
        MembershipUser user = Membership.GetUser();
        Guid g = (Guid)user.ProviderUserKey;
        string name = ModeratorUsefulFunctions.GetMessageUsersName(g);
        //SqlDataSource1.SelectParameters.Add("@Name", name);
        SqlDataSource1.SelectParameters["Name"].DefaultValue = name;
        Session["Name"] = name;

    }
    protected void ListView1_ItemCommand(object sender, ListViewCommandEventArgs e)
    {
        ListViewDataItem item = (ListViewDataItem)e.Item;

        string recordID = ListView1.DataKeys[item.DataItemIndex]["MessageID"].ToString();
        Server.Transfer("~/Moderator/ObserveMessage.aspx?MessageID=" + recordID);
    }
}