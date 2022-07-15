using System.Net;
using System.Text;

namespace IPv4.Console
{
    public class IPv4Extensions
    {
        public static bool ValidatePrefixLength(string prefixLength)
        {
            //TODO: Refactor this
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
            if (ip.Length < 7)
            {
                return false;
            }
            else
            {
                return IPAddress.TryParse(ip, out _);
            }
        }

        public static (IPAddress decimalMask, string binaryMask) GenerateNetworkMask(int networkBits)
        {
            StringBuilder builder = new(32);

            //Set network bits to ones
            builder.Append('1', networkBits);

            //Set host bits to zero
            builder.Append('0', 32 - networkBits);

            var octects = builder.ToString().Chunk(8).ToList();

            byte[] maskBytes = new byte[4];

            string binMask = new(new string(octects[0]) + "." + new string(octects[1]) + "." + new string(octects[2]) + "." + new string(octects[3]));

            for (int i = 0; i < 4; i++)
            {
                maskBytes[i] = (byte)Convert.ToInt32(new string(octects[i]), 2);
            }

            return (new IPAddress(maskBytes), binMask);
        }

        public static IPAddress GetWildcardMask(IPAddress networkMask)
        {
            byte[] maskBytes = networkMask.GetAddressBytes();

            byte[] wildcardBytes = new byte[4];

            for (int i = 0; i < maskBytes.Length; i++)
            {
                wildcardBytes[i] = (byte)~maskBytes[i];
            }

            return new IPAddress(wildcardBytes);
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

        public static List<PowerOfTwo> FindPowersOfTwo(List<int> hosts)
        {
            List<PowerOfTwo> ouput = new();

            for (int i = 0; i < hosts.Count; i++)
            {
                for (int j = 0; j < 32; j++)
                {
                    double power = Math.Pow(2, j);

                    if (power >= hosts[i])
                    {
                        PowerOfTwo pO2 = new((int)Math.Log2(power), (int)power);
                        ouput.Add(pO2);
                        break;
                    }
                }
            }

            return ouput;
        }

        public static IPAddress GetNextAvailableIP(IPAddress last)
        {
            var lastBytes = last.GetAddressBytes();
            lastBytes[3] += 0b1;

            return new(lastBytes);
        }
    }
}
