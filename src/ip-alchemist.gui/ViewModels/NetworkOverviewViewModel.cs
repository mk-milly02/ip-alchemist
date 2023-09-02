using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using ip_alchemist.core;
using ip_alchemist.gui.Attributes;
using System.ComponentModel.DataAnnotations;
using System.Net;
using System.Net.NetworkInformation;
using System.Text;

namespace ip_alchemist.gui.ViewModels
{
    internal partial class NetworkOverviewViewModel : ObservableValidator
    {
        public NetworkOverviewViewModel() { }

        public static Block Network { get; set; }

        [ObservableProperty]
        [NotifyPropertyChangedFor(nameof(CanGenerateNetworkInformation))]
        [IPAddress(ErrorMessage = "- This is not a vaild IPv4 address.")]
        private string address;

        [ObservableProperty]
        [NotifyPropertyChangedFor(nameof(CanGenerateNetworkInformation))]
        [PrefixLength(ErrorMessage = "- The prefix length must be between 0 & 33.")]
        private string prefixLength;

        [ObservableProperty]
        private string binaryNetworkMask;

        [ObservableProperty]
        private string decimalNetworkMask;

        [ObservableProperty]
        private string wildcardMask;

        [ObservableProperty]
        private string networkAddress;

        [ObservableProperty]
        private string broadcastAddress;

        [ObservableProperty]
        private string networkType;

        [ObservableProperty]
        private string range;

        [ObservableProperty]
        private string totalHosts;

        [ObservableProperty]
        private string totalValidHosts;

        public bool CanGenerateNetworkInformation => !string.IsNullOrWhiteSpace(Address) && !string.IsNullOrWhiteSpace(PrefixLength);

        [RelayCommand]
        private async Task GenerateNetworkInformation()
        {
            ValidateAllProperties();
            if (HasErrors) { await ShowValidationErrorsAsync(GetErrors()); return; }

            Network = new(Address, int.Parse(PrefixLength));

            BinaryNetworkMask = Network.NetworkMask.binaryMask;
            DecimalNetworkMask = Network.NetworkMask.decimalMask.ToString();
            WildcardMask = Network.WildcardMask.ToString();
            NetworkAddress = Network.NetworkAddress.ToString();
            BroadcastAddress = Network.BroadcastAddress.ToString();
            NetworkType = Network.NetworkType;
            TotalHosts = Network.TotalHosts.ToString();
            TotalValidHosts = Network.TotalValidHosts.ToString();
            Range = Network.AddressRange;
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
