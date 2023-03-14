using System.Net;
using System.Text;

namespace ip_alchemist_cli.libs
{
    public static class IPv4Library
    {
        public static bool ValidateIPAddress(string ip)
        {
            string[] octects = ip.Split('.');

            if (octects.Length == 4)
            {
                for (int i = 0; i < octects.Length; i++)
                {
                    if (!int.TryParse(octects[i], out int octect) || octect < 0 || octect > 255)
                    {
                        return false;
                    }
                }

                return true;
            }

            return false;
        }

        public static bool ValidatePrefixLength(string prefixLength)
        {
            return int.TryParse(prefixLength, out int x) && x >= 0 && x < 33;
        }

        public static (IPAddress decimalMask, string binaryMask) GenerateNetworkMask(int prefixLength)
        {
            StringBuilder builder = new(32);

            //Set network bits to ones
            builder.Append('1', prefixLength);

            //Set host bits to zero
            builder.Append('0', 32 - prefixLength);

            var octects = builder.ToString().Chunk(8).ToList();

            byte[] maskBytes = new byte[4];

            string binaryMask = new(new string(octects[0]) + "." + new string(octects[1]) + "." + new string(octects[2]) + "." + new string(octects[3]));

            for (int i = 0; i < 4; i++)
            {
                maskBytes[i] = (byte)Convert.ToInt32(new string(octects[i]), 2);
            }

            return (new IPAddress(maskBytes), binaryMask);
        }

        public static IPAddress GenerateWildcardMask(IPAddress networkMask)
        {
            byte[] maskBytes = networkMask.GetAddressBytes();

            byte[] wildcardBytes = new byte[4];

            for (int i = 0; i < maskBytes.Length; i++)
            {
                wildcardBytes[i] = (byte)~maskBytes[i];
            }

            return new IPAddress(wildcardBytes);
        }

        public static IPAddress GenerateNetworkAddress(IPAddress ip, IPAddress mask)
        {
            byte[] ipBytes = ip.GetAddressBytes();
            byte[] maskBytes = mask.GetAddressBytes();

            if (ipBytes.Length != maskBytes.Length)
            {
                throw new ArgumentException("The IP address and the network mask does not match");
            }

            byte[] networkAddressBytes = new byte[ipBytes.Length];

            //Perform 'and' operation
            for (int i = 0; i < ipBytes.Length; i++)
            {
                networkAddressBytes[i] = (byte)(ipBytes[i] & maskBytes[i]);
            }

            IPAddress networkAddress = new(networkAddressBytes);

            return networkAddress;
        }

        public static IPAddress GenerateBroadcastAddress(IPAddress networkAddress, IPAddress mask)
        {
            byte[] networkAdBytes = networkAddress.GetAddressBytes();
            byte[] maskBytes = mask.GetAddressBytes();

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

            return broadcastAddress;
        }

        public static long TotalNumberOfAddresses(int prefixLength)
        {
            return (long)Math.Pow(2, 32 - prefixLength);
        }

        public static string GenerateAddressRange(IPAddress networkAddress, IPAddress broadcastAddress, long validHosts)
        {
            if (validHosts > 0)
            {
                byte[] networkAdBytes = networkAddress.GetAddressBytes();
                byte[] broadcastBytes = broadcastAddress.GetAddressBytes();

                networkAdBytes[3] += 0b1;
                broadcastBytes[3] -= 0b1;

                IPAddress first = new(networkAdBytes);
                IPAddress last = new(broadcastBytes);

                return first.ToString() + " ~ " + last.ToString();
            }

            return "There are no valid addresses.";
        }
    }
}