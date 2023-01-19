using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Security;

public partial class Moderator_NewMessage : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        MessageSent.Visible = false;
          string[] moderatorRoles = Roles.GetUsersInRole("Moderator");
          string[] administratorRoles = Roles.GetUsersInRole("Administrator");
          foreach (var item in moderatorRoles)
          {
              DropDownList1.Items.Add(item);
          }

          foreach (var item in administratorRoles)
          {
              DropDownList1.Items.Add(item);
          }
    }

    protected void btnSubmit_Click(object sender, EventArgs e)
    {
       MembershipUser CurrentUser = Membership.GetUser();
       Guid i = (Guid)CurrentUser.ProviderUserKey;
       ModeratorUsefulFunctions.InsertNewMessage(i, TextBox1.Text, Editor1.Content, DropDownList1.SelectedValue);
       MessageSent.Visible = true;
    }
}