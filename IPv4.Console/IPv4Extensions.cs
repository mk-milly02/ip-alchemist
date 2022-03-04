using System.Net;
using System.Text;
using Spectre.Console;

namespace IPv4.Console
{
    public class IPv4Extensions
    {
        public static bool ValidateMask(string ip)
        {
            if (ip.Contains('/'))
            {
                string mask = ip.Split('/')[1];

                bool output = int.TryParse(mask, out int nBits);

                if (output && nBits >= 1 && nBits < 32)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            else
            {
                return false;
            }
        }

        public static bool IsIPValid(string ip)
        {
            return IPAddress.TryParse(ip, out _);
        }

        public static string GenerateMask(int networkBits)
        {
            int hostBits = 32 - networkBits;

            StringBuilder builder = new(32);

            //Set network bits to ones
            for (int i = 0; i < networkBits; i++)
            {
                builder.Append('1');
            }

            //Set host bits to zeros
            for (int i = 0; i < hostBits; i++)
            {
                builder.Append('0');
            }

            string mask = builder.ToString();

            var octects = mask.Chunk(8).ToList();

            //Convert from binary to decimal
            string firstOctect = Convert.ToString(Convert.ToInt32(new string(octects[0]), 2), 10);
            string secondOctect = Convert.ToString(Convert.ToInt32(new string(octects[1]), 2), 10);
            string thirdOctect = Convert.ToString(Convert.ToInt32(new string(octects[2]), 2), 10);
            string fourthOctect = Convert.ToString(Convert.ToInt32(new string(octects[3]), 2), 10);

            string[] decOctects = new[] {firstOctect, secondOctect, thirdOctect, fourthOctect};

            //Separate octects with dots
            StringBuilder builder1 = new();
            builder1.AppendJoin('.', decOctects);

            return builder1.ToString();
        }

        public static string GetNetworkAddress(string ip, string mask)
        {
            IPAddress iPAddress = IPAddress.Parse(ip);
            IPAddress networkMask = IPAddress.Parse(mask);

            byte[] ipBytes = iPAddress.GetAddressBytes();
            byte[] maskBytes = networkMask.GetAddressBytes();

            if (ipBytes.Length != maskBytes.Length)
            {
                throw new ArgumentException("The IP address and the network mask does not match");
            }

            byte[] networkAddressBytes = new byte[ipBytes.Length];

            for (int i = 0; i < ipBytes.Length; i++)
            {
                networkAddressBytes[i] = (byte)(ipBytes[i] & maskBytes[i]);
            }

            IPAddress networkAddress = new(networkAddressBytes);

            return networkAddress.ToString();
        }

        public static string GetBroadcastAddress(string networkAddress, string mask)
        {
            IPAddress networkAd = IPAddress.Parse(networkAddress);
            IPAddress networkMask = IPAddress.Parse(mask);

            byte[] networkAdBytes = networkAd.GetAddressBytes();
            byte[] maskBytes = networkMask.GetAddressBytes();

            if (networkAdBytes.Length != maskBytes.Length)
            {
                throw new ArgumentException("The network address and the network mask does not match");
            }

            byte[] broadcastBytes = new byte[networkAdBytes.Length];

            for (int i = 0; i < networkAdBytes.Length; i++)
            {
                broadcastBytes[i] = (byte)(networkAdBytes[i] | ~maskBytes[i]);
            }

            IPAddress broadcastAddress = new(broadcastBytes);

            return broadcastAddress.ToString();
        }

        public static int TotalNumberOfAddresses(int networkBits)
        {
            return (int)Math.Pow(2, 32 - networkBits);
        }

        public static string GetRange(string networkAddress, string broadcastAddress)
        {
            IPAddress netAd = IPAddress.Parse(networkAddress);
            IPAddress broadcast = IPAddress.Parse(broadcastAddress);

            byte[] networkAdBytes = netAd.GetAddressBytes();
            byte[] broadcastBytes = broadcast.GetAddressBytes();

            networkAdBytes[3] += 0b1;
            broadcastBytes[3] -= 0b1;

            IPAddress first = new(networkAdBytes);
            IPAddress last = new(broadcastBytes);

            return first.ToString() + " ~ " + last.ToString();
        }

        public static void IPBasics(string ip, int networkBits)
        {
            string mask = GenerateMask(networkBits);
            string networkAddress = GetNetworkAddress(ip, mask);
            string broadcastAddress = GetBroadcastAddress(networkAddress, mask);
            int totalAddresses = TotalNumberOfAddresses(networkBits);
            int validHost = totalAddresses - 2;
            string range = GetRange(networkAddress, broadcastAddress);

            Table output = new();
            output.Width(60);
            output.Border(TableBorder.Rounded);
            output.BorderColor(Color.Gold1);
            output.AddColumns("", "[cyan]Information[/]");
            output.AddRow("Network mask", mask);
            output.AddRow("Network Address", networkAddress);
            output.AddRow("Broadcast Address", broadcastAddress);
            output.AddRow("Addressess(Total)", totalAddresses.ToString());
            output.AddRow("Valid Host", validHost.ToString());
            output.AddRow("Range", $"[yellow]{range}[/]");

            AnsiConsole.Write(output);
        }

        public static bool ValidateHostGroups(string groups)
        {
            bool output = true;
            var hosts = groups.Split(',');

            List<int> Hosts = new();

            for (int i = 0; i < hosts.Length; i++)
            {
                output = int.TryParse(hosts[i], out int host);

                if (output)
                {
                    Hosts.Add(host);
                }
                else
                {
                    return output;
                }
            }

            return output;
        }

        public static List<int> GetHostGroups(string hosts)
        {
            var _hosts = hosts.Split(',').ToList();

            List<int> output = new();

            foreach (var item in _hosts)
            {
                output.Add(int.Parse(item));
            }

            return output;
        }

        public static int FindPowerOfTwo(int host)
        {
            int output = 0;
            List<int> PowersOfTwo = new();

            for (int i = 0; i < 32; i++)
            {
                PowersOfTwo.Add((int)Math.Pow(2, i));
            }

            foreach (var power in PowersOfTwo)
            {
                if (power >= host)
                {
                    output = power;
                    break;
                }
            }

            return output;
        }
    }
}
