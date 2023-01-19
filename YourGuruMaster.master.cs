using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Security;
using System.Security.Authentication;

public partial class YourGuruMaster : System.Web.UI.MasterPage
{
    protected void Page_Load(object sender, EventArgs e)
    {

        HyperLink1.NavigateUrl = "UserControl.aspx?x="+GetUsersName();
        
    }

    public string GetUsersName()
    {
        MembershipUser CurrentUser = Membership.GetUser();
        Guid userid = (Guid)CurrentUser.ProviderUserKey;

        return UsefulStaticMethods.GetUsersControlPanelName(userid);
    }

  
}
