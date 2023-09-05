using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using ip_alchemist.core;
using ip_alchemist.gui.Attributes;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace ip_alchemist.gui.ViewModels
{
    partial  class FLSMViewModel : ObservableValidator
    {
        public FLSMViewModel() { }

        public static FBlock Network { get; set; }

        [ObservableProperty]
        [NotifyPropertyChangedFor(nameof(CanSubnet))]
        [IPAddress(ErrorMessage = "- This is not a vaild IPv4 address.")]
        private string address;

        [ObservableProperty]
        [NotifyPropertyChangedFor(nameof(CanSubnet))]
        private string prefixLength;

        [ObservableProperty]
        [NotifyPropertyChangedFor(nameof(CanSubnet))]
        //[NotifyPropertyChangedFor(nameof(Subnets))]
        private int numberOfSubnets;

        public ObservableCollection<string> PrefixLengths => new() 
        { 
            "1", "2", "3", "4", "5", "6", "7", "8", "9", "10", 
            "11", "12", "13", "14", "15", "16", "17", "18", "19", "20", 
            "21", "22", "23", "24", "25", "26", "27", "28", "29", "30" 
        };

        //public ObservableCollection<double> Subnets => Subnetting.ValidSubnets(PrefixLength);
        
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

        public bool CanSubnet => !string.IsNullOrWhiteSpace(Address);

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
