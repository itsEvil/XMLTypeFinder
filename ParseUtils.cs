using System.Globalization;
using System.Xml.Linq;

namespace XMLTypeFinder
{
    public static partial class ParseUtils
    {
        public static string ParseString(this XElement element, string name, string undefined = null)
        {
            var value = name[0].Equals('@') ? element.Attribute(name.Remove(0, 1))?.Value : element.Element(name)?.Value;
            if (string.IsNullOrWhiteSpace(value)) return undefined;
            return value;
        }
        public static ushort ParseUshort(this XElement element, string name, ushort undefined = 0)
        {
            var value = name[0].Equals('@') ? element.Attribute(name.Remove(0, 1))?.Value : element.Element(name)?.Value;
            if (string.IsNullOrWhiteSpace(value)) return undefined;
            return (ushort)(value.StartsWith("0x") ? Int32.Parse(value[2..], NumberStyles.HexNumber) : Int32.Parse(value));
        }
    }
}
