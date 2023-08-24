using System.Diagnostics;
using System.Text;
using System.Xml.Linq;

namespace XMLTypeFinder
{
    internal class Program
    {
        private static Dictionary<ushort, ItemDesc> Type2Item = new Dictionary<ushort, ItemDesc>(); 

        static void Main(string[] args)
        {
            Console.WriteLine("Hello, World!");
            ParseXMLs();
            Console.WriteLine("You can close the app now. Files generated");
            Console.WriteLine("Press any key to close app.");
            Console.Read();
        }
        private static void ParseXMLs()
        {
            var watch = Stopwatch.StartNew();
            var paths = Directory.EnumerateFiles(Path.Combine(Directory.GetCurrentDirectory(), "xmls"), "*.xml", SearchOption.TopDirectoryOnly).ToArray();

            for (var i = 0; i < paths.Length; i++)
            {
            
#if DEBUG   
                Console.WriteLine($"Parsing XMLS <{paths[i].Split('/').Last()}>");
#endif      
                var data = XElement.Parse(File.ReadAllText(paths[i]));
            
                foreach (var item in data.Elements("Object")
                    .AsParallel().WithDegreeOfParallelism(4))
                {
                    string id = item.ParseString("@id");
                    ushort type = item.ParseUshort("@type");
                    Type2Item[type] = new ItemDesc(id, type);
                }
            }

            
            StringBuilder sb = new StringBuilder(); //Lists just the range


            bool hasValue = false;
            for(ushort i =0; i < ushort.MaxValue; i++)//loop through all ushort values
            {
                if (!hasValue && !Type2Item.ContainsKey(i))//find the first number for our range
                {
                    hasValue = true;
                    var value = Convert.ToString(i, 16);
                    sb.Append("[0x" + value + "-");
                } 

                if(hasValue && Type2Item.ContainsKey(i)) //finds the last range
                {
                    hasValue = false;
                    var value = Convert.ToString(i - 1, 16);
                    sb.Append("0x" + value + "]");
                    sb.AppendLine();
                }
            }

            if(hasValue)
            {
                var value = Convert.ToString(ushort.MaxValue, 16);
                sb.Append("0x" + value + "]");
            }

            File.WriteAllText(Path.Combine(Directory.GetCurrentDirectory(),"output-ranges.txt"), sb.ToString());

            Console.WriteLine("Usable Types:\n" + sb.ToString());

            watch.Stop();
            Console.WriteLine($"Time elapsed: {watch.ElapsedMilliseconds} ms");
        }
    }
}
