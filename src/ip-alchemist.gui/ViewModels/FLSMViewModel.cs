using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using ip_alchemist.core;
using ip_alchemist.gui.Attributes;
using ip_alchemist.gui.Models;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;

namespace ip_alchemist.gui.ViewModels
{
    partial class FLSMViewModel : ObservableValidator
    {
        public FLSMViewModel() { }

        public static FBlock Network { get; set; }

        #region Observable Properties

        [ObservableProperty]
        [NotifyPropertyChangedFor(nameof(CanGenerateSubnets))]
        [IPAddress(ErrorMessage = "- This is not a vaild IPv4 address.")]
        private string address;

        [ObservableProperty]
        [NotifyPropertyChangedFor(nameof(CanGenerateSubnets))]
        [NotifyPropertyChangedFor(nameof(ValidSubnets))]
        private string prefixLength;

        [ObservableProperty]
        [NotifyPropertyChangedFor(nameof(CanGenerateSubnets))]
        private string numberOfSubnets;

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

        #endregion

        public ObservableCollection<string> PrefixLengths => new()
        {
            "1", "2", "3", "4", "5", "6", "7", "8", "9", "10",
            "11", "12", "13", "14", "15", "16", "17", "18", "19", "20",
            "21", "22", "23", "24", "25", "26", "27", "28", "29", "30"
        };

        public ObservableCollection<string> ValidSubnets => Subnetting.ValidSubnets(PrefixLength);

        [ObservableProperty]
        public ObservableCollection<SubnetModel> subnets;

        public bool CanGenerateSubnets => !string.IsNullOrWhiteSpace(Address) && !string.IsNullOrWhiteSpace(PrefixLength) && !string.IsNullOrWhiteSpace(NumberOfSubnets);

        [RelayCommand]
        private async Task GenerateSubnets()
        {
            ValidateAllProperties();

            if (HasErrors) { await ShowValidationErrorsAsync(GetErrors().FirstOrDefault()); return; }

            Network = new(Address, int.Parse(PrefixLength))
            {
                NumberOfSubnets = int.Parse(NumberOfSubnets)
            };

            BinaryNetworkMask = Network.NetworkMask.binaryMask;
            DecimalNetworkMask = Network.NetworkMask.decimalMask.ToString();
            WildcardMask = Network.WildcardMask.ToString();
            NetworkAddress = Network.NetworkAddress.ToString();
            BroadcastAddress = Network.BroadcastAddress.ToString();
            NetworkType = Network.NetworkType;
            TotalHosts = Network.TotalHosts.ToString();
            TotalValidHosts = Network.TotalValidHosts.ToString();
            Range = Network.AddressRange;

            GenerateFixedLengthSubnets();
        }

        private static async Task ShowValidationErrorsAsync(ValidationResult result)
        {
            await Application.Current.MainPage.DisplayAlert("Invalid input", result.ErrorMessage.ToString(), "Cancel");
        }

        private void GenerateFixedLengthSubnets()
        {
            Network.Subnets = new();
            Subnets = new();

            //create first subnet
            Subnet subnet = new()
            {
                Number = 1,
                Address = Network.Address,
                Hosts = Network.HostsPerSubnet,
                PrefixLength = 32 - (int)Math.Log2(Network.HostsPerSubnet)
            };

            Network.Subnets.Add(subnet);
            Subnets.Add(new(subnet));
            

            for (int i = 1; i < Network.NumberOfSubnets; i++)
            {
                Subnet subnet1 = new()
                {
                    Number = i + 1,
                    Address = Subnetting.GetNextAvailableIPAddress(Network.Subnets[i - 1].BroadcastAddress),
                    Hosts = Network.NumberOfSubnets,
                    PrefixLength = 32 - (int)Math.Log2(Network.HostsPerSubnet)
                };
                Network.Subnets.Add(subnet1);
                Subnets.Add(new(subnet1));
            }
        }
    }
}
