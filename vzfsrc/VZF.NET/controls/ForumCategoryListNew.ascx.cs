using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using YAF.Core;

namespace YAF.Controls
{
    public partial class ForumCategoryListNew : BaseUserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            var lst = new List<Category>();
            lst.Add(new Category() { CategoryName = "Category1", Forums = GetForums() });
          //  Categories.DataSource = lst;
          //  Categories.DataBind();
        }

        private IEnumerable<Forum> GetForums()
        {
            yield return new Forum() { Name = "Category1Forum1" };
            yield return new Forum() { Name = "Category1Forum2" };
        }
    }

   
    public class Category
    {
        public string CategoryName { get; set; }
        public IEnumerable<Forum> Forums { get; set; }
    }
    public class Forum
    {
        public string Name { get; set; }
    }


}