using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using ip_alchemist.core;
using ip_alchemist.gui.Attributes;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace ip_alchemist.gui.ViewModels
{
    internal partial class NetworkOverviewViewModel : ObservableValidator
    {
        public NetworkOverviewViewModel()
        {
            
        }

        private static Block NetworkSegment { get; set; }

        [ObservableProperty]
        [NotifyPropertyChangedFor(nameof(CanGenerateNetworkInformation))]
        [IPAddress(ErrorMessage = "- This is not a vaild IPv4 address.")]
        private string ipAddress;

        [ObservableProperty]
        [NotifyPropertyChangedFor(nameof(CanGenerateNetworkInformation))]
        [PrefixLength(ErrorMessage = "- The prefix length must be between 0 & 33.")]
        private string prefixLength;

        public bool CanGenerateNetworkInformation => !string.IsNullOrWhiteSpace(IpAddress) && !string.IsNullOrWhiteSpace(PrefixLength);

        [RelayCommand]
        private async Task GenerateNetworkInformation()
        {
            ValidateAllProperties();
            if (HasErrors) { await ShowValidationErrorsAsync(GetErrors()); }
        }

        private static async Task ShowValidationErrorsAsync(IEnumerable<ValidationResult> results)
        {
            StringBuilder errorMessage = new();

            foreach (ValidationResult result in results)
            {
                errorMessage.AppendLine(result.ErrorMessage);
            }

            await Application.Current.MainPage.DisplayAlert("Invalid input", errorMessage.ToString(), "Cancel");
        }
    }
}
