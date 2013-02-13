using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Net.Mail;

public partial class ui_uc_EmailUserControl : System.Web.UI.UserControl
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }
    protected void Button1_Click(object sender, EventArgs e)
    {
       
        MailMessage mail = new MailMessage();

        mail.To.Add("bkumar3@learn.senecac.on.ca");
        mail.Subject = "Message from ICT Website: Contact Us";
        mail.Body = TextBox4.Text;

        SmtpClient mysmtpclient = new SmtpClient();
        if (RadioButtonList1.SelectedIndex == 0)
        {

            mail.CC.Add(TextBox3.Text);
            mysmtpclient.Send(mail);

        }
        else { mysmtpclient.Send(mail); }
    }
    protected void TextBox4_TextChanged(object sender, EventArgs e)
    {

    }
}