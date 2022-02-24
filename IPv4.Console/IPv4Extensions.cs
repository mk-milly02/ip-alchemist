using System.Net;
using System.Text;

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
    }
}
