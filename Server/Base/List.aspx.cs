using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Server.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Validation;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Server.Base
{
    public partial class List : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            var manager = Context.GetOwinContext().GetUserManager<ApplicationUserManager>();
            var user = manager.FindById(User.Identity.GetUserId());
            var db = Context.GetOwinContext().Get<ApplicationDbContext>();
            var set = db.Set<Models.Base>();
            set.Load();
            BaseGrid.DataSource = user.UserBases;
            DetailsView.DataSource = user.UserBases;
            BaseGrid.PageIndexChanging += BaseGrid_PageIndexChanging;
            BaseGrid.PageIndexChanged += BaseGrid_PageIndexChanged;
            BaseGrid.SelectedIndexChanging += BaseGrid_SelectedIndexChanging;
            BaseGrid.SelectedIndexChanged += BaseGrid_SelectedIndexChanged;
            BaseGrid.DataBind();
            DetailsView.DataBind();
        }

        private void BaseGrid_SelectedIndexChanged(object sender, EventArgs e)
        {
            DetailsView.DataBind();
        }

        private void BaseGrid_SelectedIndexChanging(object sender, GridViewSelectEventArgs e)
        {
            int NewIndex = e.NewSelectedIndex + BaseGrid.PageSize * BaseGrid.PageIndex;
            if (NewIndex >= 0 && NewIndex < DetailsView.DataItemCount)
            {
                DetailsView.PageIndex = NewIndex;
                DetailsView.Visible = true;
            }
        }

        private void BaseGrid_PageIndexChanged(object sender, EventArgs e)
        {
            BaseGrid.DataBind();
        }

        private void BaseGrid_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            if (e.NewPageIndex >= 0 && e.NewPageIndex < BaseGrid.PageCount && e.NewPageIndex != BaseGrid.PageIndex)
            {
                BaseGrid.PageIndex = e.NewPageIndex;
            }
        }
    }
}