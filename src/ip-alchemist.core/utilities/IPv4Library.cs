using System.Net;
using System.Text;

namespace ip_alchemist.core;

public static class IPv4Library
{
    public static bool ValidateIPAddress(string ip)
    {
        string[] octets = ip.Split('.');

        if (octets.Length == 4)
        {
            for (int i = 0; i < octets.Length; i++)
            {
                if (!int.TryParse(octets[i], out int octect) || octect < 0 || octect > 255)
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

        //Set host bits to zeros
        builder.Append('0', 32 - prefixLength);

        var octets = builder.ToString().Chunk(8).ToList();

        byte[] maskBytes = new byte[4];

        string binaryMask = new(new string(octets[0]) + "." + new string(octets[1]) + "." + new string(octets[2]) + "." + new string(octets[3]));

        for (int i = 0; i < 4; i++)
        {
            maskBytes[i] = (byte)Convert.ToInt32(new string(octets[i]), 2);
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

    public static string GetNetworkType(IPAddress ip)
    {
        byte[] ipBytes = ip.GetAddressBytes();
        int[] octets = new int[4];

        for (int i = 0; i < ipBytes.Length; i++)
        {
            octets[i] = Convert.ToInt32(ipBytes[i]);
        }

        return octets[0] switch
        {
            10 => "Private",
            172 when octets[1] >= 16 && octets[1] <= 31 => "Private",
            192 when octets[1] == 168 => "Private",
            127 => "Loopback",
            169 when octets[1] == 254 => "Link-Local",
            _ => "Public",
        };
    }

    public static string GetAddressClass(IPAddress ip)
    {
        byte[] ipBytes = ip.GetAddressBytes();
        int[] octets = new int[4];

        for (int i = 0; i < ipBytes.Length; i++)
        {
            octets[i] = Convert.ToInt32(ipBytes[i]);
        }

        return octets[0] switch
        {
            >= 1 and <= 126 => "Class A",
            >= 128 and <= 191 => "Class B",
            >= 192 and <= 223 => "Class C",
            >= 224 and <= 239 => "Class D",
            >= 240 and <= 255 => "Class E",
            _ => "N/A",
        };
    }
}