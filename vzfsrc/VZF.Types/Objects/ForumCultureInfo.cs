

namespace VZF.Types.Objects{
 
    public class ForumCultureInfo 
    {
        private string _cultureFile = "english.xml";

        public string CultureFile
        {
            get {  return _cultureFile; }
            set { _cultureFile = value; }
        }   
    
        public string IetfLanguageTag { get; set; }
        public string EnglishName { get; set; }
        public string NativeName { get; set; }
        public string DisplayName { get; set; }
    }
}
