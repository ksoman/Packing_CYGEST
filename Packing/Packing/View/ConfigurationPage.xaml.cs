using System;
using PackingCygest.Model;
using PackingCygest.ViewModel;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace PackingCygest.View
{
    /// <summary>
    /// class COnfigurationPage
    /// </summary>
    /// <seealso cref="Xamarin.Forms.ContentPage" />

    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ConfigurationPage : ContentPage
    {
        readonly ConfigurationViewModel _configViewModel = new ConfigurationViewModel(new PageService());

        public ConfigurationPage()
        {
            InitializeComponent();
            BindingContext = _configViewModel;

        }

     
        /// <summary>
        /// Saves the configuration.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="System.EventArgs" /> instance containing the event data.</param>
        private async void SaveConfig(object sender, EventArgs e)
        {
            await _configViewModel.SaveConfig();

        }


        /// <summary>
        /// Countries the selected index change.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="System.EventArgs" /> instance containing the event data.</param>
        private void CountryPicker_SelectedIndexChanged(object sender, EventArgs e)
        {
            var country = CountryPicker.SelectedItem as CountryModel;
            _configViewModel?.SelectedCountryDetails(country);
        }

        /// <summary>
        /// Languages the selected index change.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="System.EventArgs" /> instance containing the event data.</param>
        private void LanguagePicker_SelectedIndexChanged(object sender, EventArgs e)
        {
            var language = LanguagePicker.SelectedItem as LanguageModel;
            _configViewModel.SelectedLanguageDetails(language);
        }

        /// <summary>
        /// Handles the SelectedIndexChanged event of the BranchPicker control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs" /> instance containing the event data.</param>
        private void BranchPicker_SelectedIndexChanged(object sender, EventArgs e)
        {
            var branch = BranchPicker.SelectedItem as BranchModel;
            _configViewModel.SelectedBranch(branch);
        }

     


    }
}
