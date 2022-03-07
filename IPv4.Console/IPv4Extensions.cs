using System.Net;
using System.Text;

namespace IPv4.Console
{
    public class IPv4Extensions
    {
        public static bool ValidatePrefixLength(string prefixLength)
        {
            if (int.TryParse(prefixLength, out _))
            {
                var x = int.Parse(prefixLength);
                return x > 1 && x < 32;
            }
            else
            {
                return false;
            }
        }

        public static bool IsIPAddress(string ip)
        {
            if (ip.Length < 8)
            {
                return false;
            }
            else    
            {
                return IPAddress.TryParse(ip, out _);
            }
        }

        public static IPAddress GenerateNetworkMask(int networkBits)
        {
            StringBuilder builder = new(32);

            //Set network bits to ones
            builder.Append('1', networkBits);

            //Set host bits to zero
            builder.Append('0', 32 - networkBits);
            
            var octects = builder.ToString().Chunk(8).ToList();

            byte[] maskBytes = new byte[4];

            for (int i = 0; i < 4; i++)
            {
                maskBytes[i] = (byte)Convert.ToInt32(new string(octects[i]), 2);
            }

            return new IPAddress(maskBytes);
        }

        public static IPAddress GetNetworkAddress(IPAddress ip, IPAddress mask)
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

        public static IPAddress GetBroadcastAddress(IPAddress networkAddress, IPAddress mask)
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

        public static int TotalNumberOfAddresses(int networkBits)
        {
            return (int)Math.Pow(2, 32 - networkBits);
        }

        public static string GetRange(IPAddress networkAddress, IPAddress broadcastAddress)
        {
            byte[] networkAdBytes = networkAddress.GetAddressBytes();
            byte[] broadcastBytes = broadcastAddress.GetAddressBytes();

            networkAdBytes[3] += 0b1;
            broadcastBytes[3] -= 0b1;

            IPAddress first = new(networkAdBytes);
            IPAddress last = new(broadcastBytes);

            return first.ToString() + " ~ " + last.ToString();
        }

        public static bool ValidateNumberOfSubnets(string number)
        {
            if (int.TryParse(number, out int x))
            {
                return x > 1;
            }
            else
            {
                return false;
            }
        }

        public static bool ValidateNumberOfHosts(string number)
        {
            if (int.TryParse(number, out int x))
            {
                return x > 0;
            }
            else
            {
                return false;
            }
        }
    }
}
