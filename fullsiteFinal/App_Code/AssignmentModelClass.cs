using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.IO;
using System.Text;

public class AssignmentModelClass
{
    System.Web.HttpApplication _app;
    public string search = "";

    public AssignmentModelClass()
    {
        _app = System.Web.HttpContext.Current.ApplicationInstance;
    }

    public List<ict_Staff> GetALLStaff()
    {
        using (var context = new AssignmentModelEntities())
        {
            return context.ict_Staff.ToList();
        }
    }

    public List<ict_Staff> SearchStaff()
    {
        using (var context = new AssignmentModelEntities())
        {
            return context.ict_Staff.Where(s => s.lastName == search).ToList();
        }
    }


    public List<ict_Student> GetALLStudents()
    {
        using (var context = new AssignmentModelEntities())
        {
            return context.ict_Student.ToList();
        }
    }

    public List<ict_Degree> GetDegreePrograms()
    {
        using (var context = new AssignmentModelEntities())
        {
            return context.ict_Degree.ToList();
        }
    }
    public List<ict_Diploma> GetDiplomaPrograms()
    {
        using (var context = new AssignmentModelEntities())
        {
            return context.ict_Diploma.ToList();
        }
    }
    public List<ict_Graduate> GetGraduatePrograms()
    {
        using (var context = new AssignmentModelEntities())
        {
            return context.ict_Graduate.ToList();
        }
    }

    public List<ict_ProgramOverview> GetOverview()
    {
        using (var context = new AssignmentModelEntities())
        {
            return context.ict_ProgramOverview.ToList();
        }
    }

    public ictPage GetPageByID(int pageID)
    {
        using (var context = new AssignmentModelEntities())
        {
            return context.ictPages.FirstOrDefault(od => od.Id == pageID);
        }
    }


    public void UpdatePageContentByID(int pageID, string Title, string Content)
	    {
	        using (var context = new AssignmentModelEntities())
	        {
                ictPage p = context.ictPages.Single(h => h.Id == pageID);
                p.Title = Title.Trim();
                p.Content = Content.Trim();
                p.DateCreated = DateTime.Now;
                p.DateModified = DateTime.Now;
                context.SaveChanges();

	        }
	    }

    public string[] getfolders(string folder)
    {
        string[] folders = Directory.GetDirectories(folder);
        for (int i = 0; i < folders.Length; i++)
            folders[i] = new DirectoryInfo(folders[i]).Name;
        return folders;
    }

    public List<ictMedia> GetAllMedia()
    {
        using (var context = new AssignmentModelEntities())
        {
            return context.ictMedias.ToList();
        }
    }

    public ictMedia AddNewMediaFile(string title, string desc, ref System.Web.UI.WebControls.FileUpload fu)
    {
        using (var context = new AssignmentModelEntities())
        {
            try
            {
                var p = new ictMedia();
                p.Title = title;
                p.Description = desc;
                p.MIMEType = fu.PostedFile.ContentType;
                p.Size = fu.PostedFile.ContentLength;
                p.DateCreated = DateTime.Now;
                p.DateModified = DateTime.Now;
                p.Content = fu.FileBytes;
                context.ictMedias.AddObject(p);
                context.SaveChanges();
                return p;
            }
            catch (Exception ex)
            {
                return new ictMedia();
            }
        }
    }

    public bool CreateNewEditablePage(string name, string inFolder)
    {
        // This version does not permit overwrites
        // It could, if the UI had a check box to permit overwrite/replace
        // Then add a parameter to the method to handle and process it

        // This method depends upon the following:
        // editablepage.aspx in the ~/author folder 
        // An entity named ictPage, with the properties referenced below 
        // A "new page creator" web form, which calls this method
        // The web form that calls this method must have the 
        // correctly-configured <location> block in Web.config 

        // Clean up the arguments which were passed in
        name = name.Trim().ToLower();
        inFolder = inFolder.Trim().ToLower();

        // Check whether file "name" already exists "inFolder"...

        // Create new proposed folder
        string folder = "";
        string newMarkup = "";
        string newCsharp ="";
        //if(_app != null){
        folder = string.Format("{0}\\{1}", _app.Server.MapPath("~"), inFolder);
            // Create new proposed file names
            newMarkup = Path.Combine(folder, name + ".aspx");
            newCsharp = Path.Combine(folder, name + ".aspx.cs");
    //}
        //else{
          //  folder = string.Format("{0}/{1}", "~/", inFolder);
            // Create new proposed file names
           // newMarkup = Path.Combine("~/", name + ".aspx");
            //newCsharp = Path.Combine("~/", name + ".aspx.cs");
        //}

        if (File.Exists(newMarkup))
        {
            return false;
        }
        else
        {
            // Get references to the editablepage.aspx and .cs
            // Use the technique that you used in your Lab 3 code
            // to get access to the http context
            string markup = _app.Server.MapPath("~/author/editablepage.aspx");
            string csharp = _app.Server.MapPath("~/author/editablepage.aspx.cs");

            // Copy the editable page template files
            File.Copy(markup, newMarkup, true);
            File.Copy(csharp, newCsharp, true);

            // Open the text of the aspx page
            StreamReader sr = File.OpenText(newMarkup);
            string markupText = sr.ReadToEnd();
            sr.Close();

            // Change the CodeFile attribute in the Page directive to the new file name
            markupText = markupText.Replace("editablepage.aspx", name + ".aspx");
            StreamWriter sw = File.CreateText(newMarkup);
            sw.Write(markupText);
            sw.Close();

            // Create a new ictPage content item
            ictPage page = null;
            using (var context = new AssignmentModelEntities())
            {
                page = new ictPage();
                page.DateCreated = DateTime.Now;
                page.DateModified = DateTime.Now;
                page.Title = "New page - " + name;
                page.Content = string.Format("<h3>New page - {0}</h3><p>New Page</p>", name);

                context.ictPages.AddObject(page);
                context.SaveChanges();
            }

            // Open the text of the C# code file
            sr = null;
            sr = File.OpenText(newCsharp);
            string csharpText = sr.ReadToEnd();
            sr.Close();

            // Change the placeholder "pageID" value
            csharpText = csharpText.Replace("pageID = 0", "pageID = " + page.Id.ToString());
            sw = null;
            sw = File.CreateText(newCsharp);
            sw.Write(csharpText);
            sw.Close();

            return true;
        }
    }
}

