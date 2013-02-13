using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
// more...
using System.Drawing;
using System.IO;

public partial class assets_images_Default : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        // Special situation... if the query string isn't there, 
        // return this page so that the students can see the code
        if (!Request.QueryString.HasKeys())
        {
            return;
        }

        Response.Clear();

        // Get the ItemID

        // For this "version 1", we will quickly check that...
        // 1) "id" is a query string name
        // 2) it is the right data type (i.e. it parses to an int)
        // 3) the requested ItemID exists
        // Later you can build in better error checking and responses

        // Parse the item ID, and make sure that it's the right data type
        // Declare an int
        int itemID;
        // Get the query string value for id as a string
        string idValue = Request.QueryString["id"];

        if (Int32.TryParse(idValue, out itemID))
        {
            using (var context = new AssignmentModelEntities())
            {
                // Fetch the matching item
                ictMedia m =
                    context
                    .ictMedias
                    .FirstOrDefault(i => i.Id == itemID);
                //DigitalContent dc = context.DigitalContents.FirstOrDefault(i => i.ItemID == itemID);

                if (m != null)
                {
                    // Set the content type
                    Response.ContentType = m.MIMEType;

                    // Was a thumbnail requested?
                    if (!String.IsNullOrEmpty(Request.QueryString["thumb"]))
                    {
                        int Length = 0;
                        Response.OutputStream.Write(this.GenerateThumbnail(m.Content, out Length), 0, Length);
                    }
                    else
                    {
                        Response.OutputStream.Write(m.Content, 0, m.Size);
                    }
                }
            }
        }
        else
        {
            // It's not a valid ID; fail gracefully
        }

        // Finish
        Response.End();
    }
    protected Byte[] GenerateThumbnail(byte[] Original, out int Length)
    {
        // Create an image from the original digital content
        // using a "byte array to memory stream" technique
        MemoryStream msOriginal = new MemoryStream(Original);
        System.Drawing.Image imOriginal = new Bitmap(msOriginal);

        // If the image size is less than 120 pixels wide, just return it
        // It's small enough that it doesn't need thumbnailing
        if (imOriginal.Width < 121)
        {
            Length = Original.Length;
            return Original;
        }
        else
        {
            // Scale it to be 120 pixels wide

            // Get the original width to height ratio
            decimal imageRatio = (decimal)imOriginal.Width / (decimal)imOriginal.Height;
            // Assume we want a thumbnail that has a 120 pixel width
            int myThumbWidth = 120;
            // Calculate the new height
            int myThumbHeight = Convert.ToInt32(120 / imageRatio);

            // Now, create the thumbnail image
            Bitmap tn = new Bitmap(myThumbWidth, myThumbHeight);
            // Create a graphics object to enable high-quality rendering
            Graphics g = Graphics.FromImage(tn);
            // Configure the settings of the object
            g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBilinear;
            // Make the thumbnail
            g.DrawImage
                (imOriginal, new Rectangle(0, 0, myThumbWidth, myThumbHeight),
                0, 0, imOriginal.Width, imOriginal.Height, GraphicsUnit.Pixel);
            // Release the resources
            g.Dispose();

            // Return the thumbnail using a "memory stream to byte array" technique
            MemoryStream msThumbnail = new MemoryStream();
            tn.Save(msThumbnail, System.Drawing.Imaging.ImageFormat.Png);
            Length = Convert.ToInt32(msThumbnail.Length);
            return msThumbnail.GetBuffer();
        }
        // if (imOriginal.Width < 121)

    } // GenerateThumbnail
}