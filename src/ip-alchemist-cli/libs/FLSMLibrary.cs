using System.Net;
using System.Numerics;

namespace ip_alchemist_cli.libs
{
    public static class FLSMLibrary
    {
        public static bool ValidatePrefixLength(string prefixLength)
        {
            //each subnet must have at least 2 ip addresses
            return int.TryParse(prefixLength, out int x) && x >= 1 && x <= 30;
        }

        public static bool ValidateNumberOfSubnets(string subnets, int prefixLength)
        {
            //number of subnets must be a power of 2
            return int.TryParse(subnets, out int x)
                    && BitOperations.IsPow2(x)
                    && x <= Math.Pow(2, 32 - prefixLength);
        }

        public static IPAddress GetNextAvailableIPAddress(IPAddress address)
        {
            var ipBytes = address.GetAddressBytes();

            //255.255.255.255
            if (ipBytes[0].Equals(255)
                && ipBytes[1].Equals(255)
                && ipBytes[2].Equals(255)
                && ipBytes[3].Equals(255))
            {
                ipBytes = IPAddress.Any.GetAddressBytes();
            }
            //x.255.255.255
            else if (ipBytes[1].Equals(255)
                && ipBytes[2].Equals(255)
                && ipBytes[3].Equals(255))
            {
                ipBytes[0] += 0b1;
                ipBytes[1] = 0b0000_0000;
                ipBytes[2] = 0b0000_0000;
                ipBytes[3] = 0b0000_0000;
            }
            //x.x.255.255
            else if (ipBytes[2].Equals(255)
                && ipBytes[3].Equals(255))
            {
                ipBytes[1] += 0b1;
                ipBytes[2] = 0b0000_0000;
                ipBytes[3] = 0b0000_0000;
            }
            //x.x.x.255
            else if (ipBytes[3].Equals(255))
            {
                ipBytes[2] += 0b1;
                ipBytes[3] = 0b0000_0000;
            }
            else
            {
                ipBytes[3] += 0b1;
            }
            return new(ipBytes);
        }
    }
}