namespace YAF.Pages.Admin
{
    #region Using

    using System;
    using System.Data;
    using System.Linq;

    using VZF.Data.Common;

    using YAF.Classes;
    
    using YAF.Core;
    using YAF.Types;
    using YAF.Types.Constants;
    using YAF.Types.Interfaces;
    using VZF.Utils;

    #endregion

    /// <summary>
    /// The Admin replacewords import page.
    /// </summary>
    public partial class replacewords_import : AdminPage
    {
        #region Methods

        /// <summary>
        /// Cancel Import and Return Back to Previous Page
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        protected void Cancel_OnClick([NotNull] object sender, [NotNull] EventArgs e)
        {
            YafBuildLink.Redirect(ForumPages.admin_replacewords);
        }

        /// <summary>
        /// Try to Import from selected File
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        protected void Import_OnClick([NotNull] object sender, [NotNull] EventArgs e)
        {
            // import selected file (if it's the proper format)...
            if (!this.importFile.PostedFile.ContentType.StartsWith("text"))
            {
                this.PageContext.AddLoadMessage(
                    this.GetText("ADMIN_REPLACEWORDS_IMPORT", "MSG_IMPORTED_FAILEDX").FormatWith(
                        "Invalid upload format specified: " + this.importFile.PostedFile.ContentType));

                return;
            }

            try
            {
                // import replace words...
                var dsReplaceWords = new DataSet();
                dsReplaceWords.ReadXml(this.importFile.PostedFile.InputStream);

                if (dsReplaceWords.Tables["YafReplaceWords"] != null
                    && dsReplaceWords.Tables["YafReplaceWords"].Columns["badword"] != null
                    && dsReplaceWords.Tables["YafReplaceWords"].Columns["goodword"] != null)
                {
                    int importedCount = 0;

                    DataTable replaceWordsList = CommonDb.replace_words_list(PageContext.PageModuleID, this.PageContext.PageBoardID, null);

                    // import any extensions that don't exist...
                    foreach (DataRow row in
                        dsReplaceWords.Tables["YafReplaceWords"].Rows.Cast<DataRow>().Where(
                            row =>
                            replaceWordsList.Select(
                                "badword = '{0}' AND goodword = '{1}'".FormatWith(row["badword"], row["goodword"])).Length == 0))
                    {
                        // add this...
                        CommonDb.replace_words_save(PageContext.PageModuleID, this.PageContext.PageBoardID, null, row["badword"], row["goodword"]);
                        importedCount++;
                    }

                    this.PageContext.LoadMessage.AddSession(
                        importedCount > 0
                            ? this.GetText("ADMIN_REPLACEWORDS_IMPORT", "MSG_IMPORTED").FormatWith(importedCount)
                            : this.GetText("ADMIN_REPLACEWORDS_IMPORT", "MSG_NOTHING"),
                        importedCount > 0 ? MessageTypes.Success : MessageTypes.Warning);

                    YafBuildLink.Redirect(ForumPages.admin_replacewords);
                }
                else
                {
                    this.PageContext.AddLoadMessage(this.GetText("ADMIN_REPLACEWORDS_IMPORT", "MSG_IMPORTED_FAILED"));
                }
            }
            catch (Exception x)
            {
                this.PageContext.AddLoadMessage(
                    this.GetText("ADMIN_REPLACEWORDS_IMPORT", "MSG_IMPORTED_FAILEDX").FormatWith(x.Message));
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
        protected void Page_Load([NotNull] object sender, [NotNull] EventArgs e)
        {
            if (this.IsPostBack)
            {
                return;
            }

            this.PageLinks.AddLink(this.Get<YafBoardSettings>().Name, YafBuildLink.GetLink(ForumPages.forum));
            this.PageLinks.AddLink(
                this.GetText("ADMIN_ADMIN", "Administration"), YafBuildLink.GetLink(ForumPages.admin_admin));
            this.PageLinks.AddLink(
                this.GetText("ADMIN_REPLACEWORDS", "TITLE"), YafBuildLink.GetLink(ForumPages.admin_replacewords));
            this.PageLinks.AddLink(this.GetText("ADMIN_REPLACEWORDS_IMPORT", "TITLE"), string.Empty);

            this.Page.Header.Title = "{0} - {1} - {2}".FormatWith(
                this.GetText("ADMIN_ADMIN", "Administration"),
                this.GetText("ADMIN_REPLACEWORDS", "TITLE"),
                this.GetText("ADMIN_REPLACEWORDS_IMPORT", "TITLE"));

            this.Import.Text = this.GetText("COMMON", "IMPORT");
            this.cancel.Text = this.GetText("COMMON", "CANCEL");
        }

        #endregion
    }
}