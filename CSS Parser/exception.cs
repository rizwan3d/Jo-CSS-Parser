using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JoCssParser
{
    public class NULL_TAG : Exception    {        public NULL_TAG() : base("Tag name is null.") { }    }
    public class NULL_PRORERTY_VALUE : Exception    {        public NULL_PRORERTY_VALUE() : base("Property value is null.") { }    }
}
