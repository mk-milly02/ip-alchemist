namespace IPv4.Console
{
    public class SubNetwork : Network
    {
        public int DesiredHost { get; set; } 

        public int SpareHostAddresses  => TotalHosts - DesiredHost; 
    }
}