using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class ErrorPage : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        Label1.Text = Request.QueryString["Error"] + "<br/><br/>";
        Label1.Text = Request.QueryString["x"] + "<br/><br/>";
        Label1.Text += "קרתה טעות אנא לחצו חזרה";

    }
}