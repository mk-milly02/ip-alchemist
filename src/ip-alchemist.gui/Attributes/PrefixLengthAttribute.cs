using ip_alchemist.core;
using System.ComponentModel.DataAnnotations;

namespace ip_alchemist.gui.Attributes
{
    internal class PrefixLengthAttribute : ValidationAttribute
    {
        public override bool IsValid(object value)
        {
            return IPv4Library.ValidatePrefixLength(value.ToString());
        }
    }
}
