using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Security;

public partial class Master : System.Web.UI.MasterPage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        studentbutton.BorderColor = System.Drawing.Color.Black;
        studentbutton.BackColor = System.Drawing.Color.FromArgb(204, 0, 0);
        studentbutton.ForeColor = System.Drawing.Color.White;
        staffbutton.BorderColor = System.Drawing.Color.Black;
        staffbutton.BackColor = System.Drawing.Color.FromArgb(204, 0, 0);
        staffbutton.ForeColor = System.Drawing.Color.White;
        publicbutton.BorderColor = System.Drawing.Color.Black;
        publicbutton.BackColor = System.Drawing.Color.FromArgb(204, 0, 0);
        publicbutton.ForeColor = System.Drawing.Color.White;
        communitybutton.BorderColor = System.Drawing.Color.Black;
        communitybutton.BackColor = System.Drawing.Color.FromArgb(204, 0, 0);
        communitybutton.ForeColor = System.Drawing.Color.White;

        
        if(HttpContext.Current.User.Identity.IsAuthenticated == true){

        if (Roles.IsUserInRole("Administrator"))
        {
            Response.Cookies["userType"].Expires = DateTime.Now.AddDays(-1D);
            SiteMapDataSource1.SiteMapProvider = "adminProvider";
        }

        else if (Roles.IsUserInRole("Author"))
        {
            Response.Cookies["userType"].Expires = DateTime.Now.AddDays(-1D);
            SiteMapDataSource1.SiteMapProvider = "authorProvider";
        }

        else if (Roles.IsUserInRole("Student"))
        {
            
            Response.Cookies["userType"].Expires = DateTime.Now.AddDays(-1D);
            SiteMapDataSource1.SiteMapProvider = "studentProvider";
            studentbutton.BorderColor = System.Drawing.Color.Red;
            studentbutton.BackColor = System.Drawing.Color.FromArgb(255, 255, 255);
            studentbutton.ForeColor = System.Drawing.Color.Black;
        }

        else if (Roles.IsUserInRole("Staff"))
        {
            Response.Cookies["userType"].Expires = DateTime.Now.AddDays(-1D);
            SiteMapDataSource1.SiteMapProvider = "staffProvider";
            staffbutton.BorderColor = System.Drawing.Color.Red;
            staffbutton.BackColor = System.Drawing.Color.FromArgb(255, 255, 255);
            staffbutton.ForeColor = System.Drawing.Color.Black;
        }

        else if (Roles.IsUserInRole("Community"))
        {
            Response.Cookies["userType"].Expires = DateTime.Now.AddDays(-1D);
            SiteMapDataSource1.SiteMapProvider = "communityProvider";
            communitybutton.BorderColor = System.Drawing.Color.Red;
            communitybutton.BackColor = System.Drawing.Color.FromArgb(255, 255, 255);
            communitybutton.ForeColor = System.Drawing.Color.Black;
        }
    }
        else if (HttpContext.Current.User.Identity.IsAuthenticated == false)
        {
            Response.Cookies["userType"].Expires = DateTime.Now.AddDays(-1D);
            SiteMapDataSource1.SiteMapProvider = "publicProvider";
            publicbutton.BorderColor = System.Drawing.Color.Red;
            publicbutton.BackColor = System.Drawing.Color.FromArgb(255, 255, 255);
            publicbutton.ForeColor = System.Drawing.Color.Black;
        }
    }

    protected void publicbutton_Click(object sender, EventArgs e)
    {

        Response.Redirect("~/Default.aspx");

    }

    protected void studentbutton_Click(object sender, EventArgs e)
    {

        Response.Redirect("~/studentDefault.aspx");

    }

    protected void staffbutton_Click(object sender, EventArgs e)
    {

        Response.Redirect("~/staffDefault.aspx");

    }
    protected void communitybutton_Click(object sender, EventArgs e)
    {

        Response.Redirect("~/communityDefault.aspx");

    }
    protected void LoginView2_ViewChanged(object sender, EventArgs e)
    {

    }
}
