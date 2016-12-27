using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JoCssParser
{
    /// <summary>
    /// Property Info (Property Name and Property Value_.
    /// </summary>
    public struct Property
    {
        public string PropertyName;
        public string PropertyValue;
    };
    struct TagWithCSS
    {
        public string TagName;
        public List<Property> Properties;
    };
}
