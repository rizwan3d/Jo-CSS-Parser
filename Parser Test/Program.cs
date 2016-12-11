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
            p.AddPropery("h1", CssProperty.Color, "#fff");
            p.AddPropery("h1", CssProperty.Color, "#000");
            p.AddPropery("h1", CssProperty.BackgroundColor, "#fff");
            p.AddPropery("h1", CssProperty.BackgroundColor, "#ebebeb");
            p.AddPropery("h1", CssProperty.Sensation, "aaa");
            Console.WriteLine(p.ToString());
            Console.Read();
        }
    }
}
