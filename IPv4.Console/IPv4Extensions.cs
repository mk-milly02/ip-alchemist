using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

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
            return IPAddress.TryParse(ip, out IPAddress? address);
        }
    }
}
