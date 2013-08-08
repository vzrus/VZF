namespace YAF
{
    #region Using

    using System;
    using System.Drawing;
    using System.Drawing.Drawing2D;
    using System.Drawing.Imaging;
    using System.Drawing.Text;
    using System.IO;

    using System.Web;

    using System.Web.SessionState;

    using YAF.Core;
  
    using YAF.Types;
    using YAF.Types.Interfaces;
    using VZF.Utils;

    #endregion

    /// <summary>
    /// Yaf Resource Handler for all kind of Stuff (Avatars, Attachments, Albums, etc.)
    /// </summary>
    public partial class YafResourceHandler : IHttpHandler, IReadOnlySessionState, IHaveServiceLocator
    {
        #region Methods

        /// <summary>
        /// Get the Album Or Image Attachement Preview
        /// </summary>
        /// <param name="data">The data.</param>
        /// <param name="previewWidth">The preview width.</param>
        /// <param name="previewHeight">The preview height.</param>
        /// <param name="previewCropped">The preview Cropped</param>
        /// <param name="downloads">The downloads.</param>
        /// <param name="localizationFile">The localization file.</param>
        /// <param name="localizationPage">The localization page.</param>
        /// <returns>
        /// Resized Image Stream
        /// </returns>
        [NotNull]
        private static MemoryStream GetAlbumOrAttachmentImageResized(
            [NotNull] Stream data,
            int previewWidth,
            int previewHeight,
            bool previewCropped,
            int downloads,
            [NotNull] string localizationFile,
            string localizationPage)
        {
            const int PixelPadding = 6;
            const int BottomSize = 26;

            var localization = new YafLocalization(localizationPage);
            localization.LoadTranslation(localizationFile);

            using (var src = new Bitmap(data))
            {
                var ms = new MemoryStream();

                // Cropped Image
                int size = previewWidth;

                var newImgSize = new Size(previewWidth, previewHeight);
                int x = 0;
                int y = 0;

                if (previewCropped)
                {
                    // Determine dimensions of resized version of the image 
                    if (newImgSize.Width > newImgSize.Height)
                    {
                        newImgSize.Width =
                            decimal.Round(
                                (size.ToType<decimal>()
                                 * (newImgSize.Width.ToType<decimal>() / newImgSize.Height.ToType<decimal>())).ToType<decimal>(),
                                0).ToType<int>();
                        newImgSize.Height = size;
                    }
                    else if (newImgSize.Height > newImgSize.Width)
                    {
                        newImgSize.Height =
                            decimal.Round(
                                (size.ToType<decimal>()
                                 * (newImgSize.Height.ToType<decimal>() / newImgSize.Width.ToType<decimal>())).ToType<decimal>(),
                                0).ToType<int>();
                        newImgSize.Width = size;
                    }
                    else
                    {
                        newImgSize.Width = size;
                        newImgSize.Height = size;
                    }

                    newImgSize.Width = newImgSize.Width - PixelPadding;
                    newImgSize.Height = newImgSize.Height - BottomSize - PixelPadding;

                    // moves cursor so that crop is more centered 
                    x = newImgSize.Width / 2;
                    y = newImgSize.Height / 2;
                }
                else
                {
                    var finalHeight = Math.Abs(src.Height * newImgSize.Width / src.Width);

                    // Height resize if necessary
                    if (finalHeight > newImgSize.Height)
                    {
                        newImgSize.Width = src.Width * newImgSize.Height / src.Height;
                        finalHeight = newImgSize.Height;
                    }

                    newImgSize.Height = finalHeight;
                    newImgSize.Width = newImgSize.Width - PixelPadding;
                    newImgSize.Height = newImgSize.Height - BottomSize - PixelPadding;

                    if (newImgSize.Height <= BottomSize + PixelPadding)
                    {
                        newImgSize.Height = finalHeight;
                    }
                }

                bool heightToSmallFix = newImgSize.Height <= BottomSize + PixelPadding;

                using (
                    var dst = new Bitmap(
                        newImgSize.Width + PixelPadding,
                        newImgSize.Height + BottomSize + PixelPadding,
                        PixelFormat.Format24bppRgb))
                {
                    var rSrcImg = new Rectangle(
                        0, 0, src.Width, src.Height + (heightToSmallFix ? BottomSize + PixelPadding : 0));

                    if (previewCropped)
                    {
                        rSrcImg = new Rectangle(x, y, newImgSize.Width, newImgSize.Height);
                    }

                    var rDstImg = new Rectangle(3, 3, dst.Width - PixelPadding, dst.Height - PixelPadding - BottomSize);
                    var rDstTxt1 = new Rectangle(3, rDstImg.Height + 3, newImgSize.Width, BottomSize - 13);
                    var rDstTxt2 = new Rectangle(3, rDstImg.Height + 16, newImgSize.Width, BottomSize - 13);

                    using (Graphics g = Graphics.FromImage(dst))
                    {
                        g.Clear(Color.FromArgb(64, 64, 64));
                        g.FillRectangle(Brushes.White, rDstImg);

                        g.CompositingMode = CompositingMode.SourceOver;
                        g.CompositingQuality = CompositingQuality.GammaCorrected;
                        g.SmoothingMode = SmoothingMode.HighQuality;
                        g.InterpolationMode = InterpolationMode.HighQualityBicubic;

                        g.TextRenderingHint = TextRenderingHint.ClearTypeGridFit;

                        g.DrawImage(src, rDstImg, rSrcImg, GraphicsUnit.Pixel);

                        using (var f = new Font("Arial", 10, FontStyle.Regular, GraphicsUnit.Pixel))
                        {
                            using (var brush = new SolidBrush(Color.FromArgb(191, 191, 191)))
                            {
                                var sf = new StringFormat { Alignment = StringAlignment.Near, LineAlignment = StringAlignment.Center };

                                g.DrawString(localization.GetText("IMAGE_RESIZE_ENLARGE"), f, brush, rDstTxt1, sf);

                                sf.Alignment = StringAlignment.Far;
                                g.DrawString(
                                    localization.GetText("IMAGE_RESIZE_VIEWS").FormatWith(downloads),
                                    f,
                                    brush,
                                    rDstTxt2,
                                    sf);
                            }
                        }
                    }

                    // save the bitmap to the stream...
                    dst.Save(ms, ImageFormat.Png);
                    ms.Position = 0;

                    return ms;
                }
            }
        }
#endregion
    }
}