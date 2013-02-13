using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class author_pageCreate : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack) { 
            AssignmentModelClass a = new AssignmentModelClass();
            System.Web.HttpApplication _context;
            _context = System.Web.HttpContext.Current.ApplicationInstance;
            string _root = "";
            _root = _context.Server.MapPath("~/");
            DropDownList1.DataSource = a.getfolders(_root);
            DropDownList1.DataBind();
        }
    }
    protected void Button1_Click(object sender, EventArgs e)
    {
        AssignmentModelClass b = new AssignmentModelClass();
        b.CreateNewEditablePage(TextBox1.Text, DropDownList1.SelectedItem.Text);
        Response.Redirect("~/"+ DropDownList1.SelectedItem.Text +"/"+ TextBox1.Text + ".aspx");
    }
}