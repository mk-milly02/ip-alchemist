using ip_alchemist.core;
using System.ComponentModel.DataAnnotations;

namespace ip_alchemist.gui.Attributes
{
    class IPAddressAttribute : ValidationAttribute
    {
        public override bool IsValid(object value)
        {
            return IPv4Library.ValidateIPAddress(value.ToString());
        }
    }
}
