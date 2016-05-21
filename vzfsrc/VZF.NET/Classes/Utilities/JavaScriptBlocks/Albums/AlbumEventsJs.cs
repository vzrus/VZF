namespace VZF.Utilities
{
    #region Using

    using YAF.Types;

    using VZF.Utils;

    #endregion

    /// <summary>
    /// Contains the Java Script Blocks
    /// </summary>
    public static partial class JavaScriptBlocks
    {
        /// <summary>
        /// Javascript events for Album pages.
        /// </summary>
        /// <param name="albumEmptyTitle">
        /// The Album Empty Title.
        /// </param>
        /// <param name="imageEmptyCaption">
        /// The Image Empty Caption.
        /// </param>
        /// <returns>
        /// The album events js.
        /// </returns>
        public static string AlbumEventsJs([NotNull] string albumEmptyTitle, [NotNull] string imageEmptyCaption)
        {
            return
                ("function showTexBox(spnTitleId){{"
                 + "spnTitleVar = document.getElementById('spnTitle' + spnTitleId.substring(8));"
                 + "txtTitleVar = document.getElementById('txtTitle'+spnTitleId.substring(8));"
                 +
                 "if (spnTitleVar.firstChild != null) txtTitleVar.setAttribute('value',spnTitleVar.firstChild.nodeValue);"
                 +
                 "if(spnTitleVar.firstChild.nodeValue == '{0}' || spnTitleVar.firstChild.nodeValue == '{1}'){{txtTitleVar.value='';spnTitleVar.firstChild.nodeValue='';}}"
                 + "txtTitleVar.style.display = 'inline'; spnTitleVar.style.display = 'none'; txtTitleVar.focus();}}"
                 +
                 "function resetBox(txtTitleId, isAlbum) {{spnTitleVar = document.getElementById('spnTitle'+txtTitleId.substring(8));txtTitleVar = document.getElementById(txtTitleId);"
                 +
                 "txtTitleVar.style.display = 'none';txtTitleVar.disabled = false;spnTitleVar.style.display = 'inline';"
                 +
                 "if (spnTitleVar.firstChild != null)txtTitleVar.value = spnTitleVar.firstChild.nodeValue;if (spnTitleVar.firstChild.nodeValue==''){{txtTitleVar.value='';if (isAlbum) spnTitleVar.firstChild.nodeValue='{0}';else spnTitleVar.firstChild.nodeValue='{1}';}}}}"
                 + "function checkKey(event, handler, id, isAlbum){{"
                 + "if ((event.keyCode == 13) || (event.which == 13)){{"
                 + "if (event.preventDefault) event.preventDefault(); event.cancel=true; event.returnValue=false; "
                 + "if(spnTitleVar.firstChild.nodeValue != txtTitleVar.value){{"
                 +
                 "handler.disabled = true; if (isAlbum == true)changeAlbumTitle(id, handler.id); else changeImageCaption(id,handler.id);}}"
                 + "else resetBox(handler.id, isAlbum);}}"
                 + "else if ((event.keyCode == 27) || (event.which == 27))resetBox(handler.id, isAlbum);}}"
                 +
                 "function blurTextBox(txtTitleId, id , isAlbum){{spnTitleVar = document.getElementById('spnTitle'+txtTitleId.substring(8));txtTitleVar = document.getElementById(txtTitleId);"
                 + "if (spnTitleVar.firstChild != null){{"
                 + "if(spnTitleVar.firstChild.nodeValue != txtTitleVar.value){{"
                 +
                 "txtTitleVar.disabled = true; if (isAlbum == true)changeAlbumTitle(id, txtTitleId); else changeImageCaption(id,txtTitleId);}}"
                 + "else resetBox(txtTitleId, isAlbum);}}" + "else resetBox(txtTitleId, isAlbum);}}").FormatWith(
                     albumEmptyTitle, imageEmptyCaption);
        }
    }
}