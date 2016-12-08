using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JoCSSParser
{
    public struct Property
    {
        public string PropertyName;
        public string PropertyValue;
    };
    public struct TagWithCSS
    {
        public string TagName;
        public List<Property> Properties;
    };
}
