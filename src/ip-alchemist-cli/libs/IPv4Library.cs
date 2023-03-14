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
    }
}