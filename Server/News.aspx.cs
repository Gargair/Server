using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Server.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using System.Data.Entity;

namespace Server
{
    public partial class News : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            var manager = Context.GetOwinContext().Get<ApplicationUserManager>();
            var db = Context.GetOwinContext().Get<ApplicationDbContext>();
            ApplicationUser user = manager.FindById(User.Identity.GetUserId());
            DbSet<Models.News> set = db.Set<Models.News>();
            set.Load();
            NewsGrid.DataSource = set.ToList<Models.News>();
            InsertView.DataSource = set.ToList<Models.News>();
            NewsGrid.PageIndexChanging += NewsGrid_PageIndexChanging;
            NewsGrid.PageIndexChanged += NewsGrid_PageIndexChanged;
            NewsGrid.SelectedIndexChanging += NewsGrid_SelectedIndexChanging;
            NewsGrid.SelectedIndexChanged += NewsGrid_SelectedIndexChanged;
            InsertView.ModeChanging += InsertView_ModeChanging;
            InsertView.ModeChanged += InsertView_ModeChanged;
            InsertView.ItemUpdating += InsertView_ItemUpdating;
            InsertView.ItemInserting += InsertView_ItemInserting;
            NewsGrid.DataBind();
            InsertView.DataBind();
            if (manager.IsInRole(User.Identity.GetUserId(), "Moderator"))
            {
                InsertView.Visible = true;
            }
        }

        private void InsertView_ItemInserting(object sender, DetailsViewInsertEventArgs e)
        {
            var manager = Context.GetOwinContext().Get<ApplicationUserManager>();
            var db = Context.GetOwinContext().Get<ApplicationDbContext>();
            ApplicationUser user = manager.FindById(User.Identity.GetUserId());
            DbSet<Models.News> set = db.Set<Models.News>();
            if (e.Values.Contains("Text"))
            {
                Models.News news = new Models.News(user);
                news.Text = (string)e.Values["Text"];
                set.Add(news);
                db.SaveChanges();
            }
            InsertView.ChangeMode(DetailsViewMode.ReadOnly);
            InsertView.DataBind();
        }

        private void InsertView_ItemUpdating(object sender, DetailsViewUpdateEventArgs e)
        {
            Guid newsId = (Guid)e.Keys["NewsId"];
            var db = Context.GetOwinContext().Get<ApplicationDbContext>();
            DbSet<Models.News> set = db.Set<Models.News>();
            //set.Load();
            Models.News news = set.FirstOrDefault(iNews => iNews.NewsId.Equals(newsId));
            if(news != null)
            {
                if(e.NewValues.Contains("Text"))
                {
                    news.Text = (string)e.NewValues["Text"];
                    db.SaveChanges();
                }
            }
            InsertView.ChangeMode(DetailsViewMode.ReadOnly);
            InsertView.DataBind();
        }

        private void InsertView_ModeChanged(object sender, EventArgs e)
        {
            InsertView.DataBind();
        }

        private void InsertView_ModeChanging(object sender, DetailsViewModeEventArgs e)
        {
            InsertView.ChangeMode(e.NewMode);
            InsertView.DataBind();
        }

        private void InsertView_Update()
        {
            Guid NewsId = (Guid)InsertView.SelectedValue;

        }

        private void NewsGrid_SelectedIndexChanged(object sender, EventArgs e)
        {
            InsertView.DataBind();
        }

        private void NewsGrid_SelectedIndexChanging(object sender, GridViewSelectEventArgs e)
        {
            int NewIndex = e.NewSelectedIndex + NewsGrid.PageIndex * NewsGrid.PageSize;
            if (NewIndex >= 0 && NewIndex < NewsGrid.PageCount)
            {
                InsertView.PageIndex = NewIndex;
                InsertView.ChangeMode(DetailsViewMode.ReadOnly);
            }
        }

        private void NewsGrid_PageIndexChanged(object sender, EventArgs e)
        {
            NewsGrid.DataBind();
        }

        private void NewsGrid_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            if (e.NewPageIndex >= 0 && e.NewPageIndex < NewsGrid.PageCount)
            {
                NewsGrid.PageIndex = e.NewPageIndex;
            }
            else
            {
                e.Cancel = true;
            }
        }
    }
}