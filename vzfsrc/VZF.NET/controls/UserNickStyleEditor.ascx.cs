namespace VZF.Controls
{
    using System;
    using System.Collections.Concurrent;

    using YAF.Core;
    using VZF.Utils;

    /// <summary>
    /// The user nick style editor.
    /// </summary>
    public partial class UserNickStyleEditor : BaseUserControl
    {
        /// <summary>
        /// Gets or sets the styles.
        /// </summary>
        public string Styles
        {
            get
            {
                if (this.ViewState["Styles"] == null)
                {
                    return null;
                }

                return (string)this.ViewState["Styles"];
            }

            set
            {
                this.ViewState["Styles"] = value;
            }
        }

        /// <summary>
        /// The page_ load.
        /// </summary>
        /// <param name="sender">
        /// The sender.
        /// </param>
        /// <param name="e">
        /// The e.
        /// </param>
        protected void Page_Load(object sender, EventArgs e)
        {
            this.BindData();
        }

        /// <summary>
        /// The bind data.
        /// </summary>
        private void BindData()
        {
            if (this.Styles.IsSet())
            {
                var values = new ConcurrentDictionary<string, string>();

                // default!font-size: 8pt; color: red/flatearth!font-size: 8pt; color:blue
                string[] allStyles = this.Styles.Split('/');
                foreach (var allStyle in allStyles)
                {
                   string[] thisStyle  = allStyle.Split('!');
                   values.AddOrUpdate(thisStyle[0], thisStyle[1], (key, oldValue) => thisStyle[1]);
                }

                this.StylesRepeater.DataSource = values;
                this.StylesRepeater.DataBind();
            }
        }
    }
}