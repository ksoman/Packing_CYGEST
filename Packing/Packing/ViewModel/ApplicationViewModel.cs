using PackingCygest.Data;

namespace PackingCygest.ViewModel
{
    /// <summary>
    /// Application view model keep default value to launched when applciation is loaded 
    /// </summary>
    public class ApplicationViewModel
    {
       
        public string DefaultedCultureInfo { get; set; }
        private DatabaseAccess _db;

        /// <summary>
        /// Initializes a new instance of the <see cref="ApplicationViewModel"/> class.
        /// </summary>
        public ApplicationViewModel()
        {          
            PopulateDefaultCultureInfo();            
        }

        /// <summary>
        /// Populates the default culture information.
        /// </summary>
        /// <returns></returns>
        private void PopulateDefaultCultureInfo()
        {
            _db = new DatabaseAccess();
            var languages = _db.LanguageSelected();
            if(languages != null)
            {
                DefaultedCultureInfo = languages;
            }
            else
            {
                DefaultedCultureInfo = "en-US";
            }
            
        }

  

    }
}
