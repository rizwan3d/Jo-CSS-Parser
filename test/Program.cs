using System;
using CSS_Parser;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace test
{
    class Program
    {
        static void Main(string[] args)
        {
            string s = "";           
            CSSPARSER p = new CSSPARSER();
            p.SetCSS(s);
            p.AddPropery(".h1", "color", "#fff");
            p.AddPropery(".h1", "a", "#000");
            p.AddPropery(".h1", "background-color", "#fff");
            p.AddPropery(".h1", "f-color", "#ebebeb");
            p.AddPropery(".h4", "t-color", "#fff");
            p.AddPropery(".h4", "color", "#000");
            Console.WriteLine(p.ToString());
            Console.Read();
        }
    }
}
