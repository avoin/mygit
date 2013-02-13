using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Security;

public partial class ui_uc_UploadMedia : System.Web.UI.UserControl
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }
    protected void btnUpload_Click(object sender, EventArgs e)
    {
        if (FileUpload1.HasFile)
        {
            if (Membership.GetUser() != null)
            {
                try
                {
                    AssignmentModelClass a = new AssignmentModelClass();
                    a.AddNewMediaFile(tbTItle.Text, tbDescription.Text, ref FileUpload1);

                        lblResult.ForeColor = System.Drawing.Color.Green;
                        lblResult.Text = "The file has been added to the object store";
                        tbDescription.Text = "";
                        tbTItle.Text = "";
                        //Response.Redirect("http://warp.senecac.on.ca/bti420_121a29/Media/Default.aspx");

                }
                catch (Exception ex)
                {
                    lblResult.ForeColor = System.Drawing.Color.Red;
                    lblResult.Text = "Error! " + ex.Message;

                }
            }

        }
        else
        {
            lblResult.Text = "Error! Please Uplaod a file";
            lblResult.ForeColor = System.Drawing.Color.Red;

        }
    }
}