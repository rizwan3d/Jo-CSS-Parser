using System;
using JoCssParser;

namespace Parser_Test
{
    class Program
    {
        static void Main(string[] args)
        {
            string s = @"h1
{
        color:#000;
        background-color:#ebebeb;
        sensation:aaa;
        color-stop:werghjk;
}
";
            CssParser p = new CssParser();
            p.Css = s;
            p.AddPropery(Tag.h1, CssProperty.Color, "#000");
            p.AddPropery(Tag.h1, CssProperty.BackgroundColor, "#fff");
            p.AddPropery("rfv", CssProperty.Size,"1589621478563");
            Console.WriteLine(p.Css);
            Console.Read();
        }
    }
}
