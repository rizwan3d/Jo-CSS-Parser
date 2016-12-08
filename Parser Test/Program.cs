using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JoCSSParser;

namespace Parser_Test
{
    class Program
    {
        static void Main(string[] args)
        {
            string s = "";
            Parser p = new Parser();
            p.SetCSS(s);
            p.AddPropery("h1", "color", "#fff");
            p.AddPropery("h1", "a", "#000");
            p.AddPropery("h1", "background-color", "#fff");
            p.AddPropery("h1", "background-color", "#ebebeb");
            p.AddPropery("h4", "background-color", "#fff");
            p.AddPropery("h4", "color", "#000");
            p.AddPropery("abc", "color", "#000", TagType.Class);
            p.AddPropery("qwe", "color", "#000", TagType.Id);
            Console.WriteLine(p.ToString());
            Console.Read();
        }
    }
}
