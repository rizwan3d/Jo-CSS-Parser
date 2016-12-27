using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text.RegularExpressions;

namespace JoCssParser
{
    /// <summary>
    /// Parser css code to add/remove or manage it.
    /// </summary>
    public class CssParser
    {
        //
        //
        // Private
        //
        //
        Dictionary<CssProperty, string> PrpertyValue;
        Dictionary<Tag, string> tag;

        List<TagWithCSS> TagWithCSSList;
        List<TagWithCSS> GetTagWithCSS(string input)
        {
            List<TagWithCSS> TagWithCSSList = new List<TagWithCSS>();
            List<string> IndivisualTag = IndivisualTags(input);
            foreach (string tag in IndivisualTag)
            {
                string[] tagname = Regex.Split(tag, @"[{]");
                TagWithCSS TWCSS = new TagWithCSS();
                if (RemoveWhitespace(tagname[0]) != "")
                {
                    TWCSS.TagName = RemoveWitespaceFormStartAndEnd(tagname[0]);
                    TWCSS.Properties = GetProperty(getBetween(tag, "{", "}"));
                    TagWithCSSList.Add(TWCSS);
                }
            }
            return TagWithCSSList;
        }
        string getBetween(string strSource, string strStart, string strEnd)
        {
            int Start, End;
            if (strSource.Contains(strStart) && strSource.Contains(strEnd))
            {
                Start = strSource.IndexOf(strStart, 0) + strStart.Length;
                End = strSource.IndexOf(strEnd, Start);
                return strSource.Substring(Start, End - Start);
            }
            else
            {
                return null;
            }
        }
        List<string> IndivisualTags(string input)
        {
            string pattern = @"(?<selector>(?:(?:[^,{]+),?)*?)\{(?:(?<name>[^}:]+):?(?<value>[^};]+);?)*?\}";

            List<string> b = new List<string>();

            foreach (Match m in Regex.Matches(input, pattern))
                b.Add(m.Value);

            return b;
        }
        List<Property> GetProperty(string input)
        {
            List<Property> p = new List<Property>();
            string[] s = Regex.Split(input, @"[;]");
            int i = 0;
            foreach (string b in s)
            {
                if (b != "")
                {
                    string[] t = Regex.Split(s[i], @"[:]");
                    Property g = new Property();
                    if (t.Length == 2)
                    {
                        if (t[0] != "")
                            g.PropertyName = RemoveWhitespace(t[0]);
                        if (t[1] != "")
                            g.PropertyValue = RemoveWitespaceFormStartAndEnd(t[1]);
                        p.Add(g);
                    }
                }
                i++;
            }
            return p;
        }
        string RemoveWhitespace(string input)
        {
            return new string(input.ToCharArray()
                .Where(c => !Char.IsWhiteSpace(c))
                .ToArray());
        }
        string RemoveWitespaceFormStartAndEnd(string input)
        {
            input = input.Trim();
            input = input.TrimEnd();
            return input;
        }
        void SetPrpertyValue()
        {
            PrpertyValue = new Dictionary<CssProperty, string>();
            string[] csskey = {  "font-weight", "border-radius", "color-stop", "alignment-adjust","alignment-baseline"
                ,"animation","animation-delay","animation-direction","animation-duration","animation-iteration-count"
                ,"animation-name","animation-play-state","animation-timing-function","appearance","azimuth"
                ,"backface-visibility","background","background-attachment","background-break","background-clip"
                ,"background-color","background-image","background-origin","background-position","background-repeat"
                ,"background-size","baseline-shift","binding","bleed","bookmark-label","bookmark-level","bookmark-state"
                ,"bookmark-target","border","border-bottom","border-bottom-color","border-bottom-left-radius"
                ,"border-bottom-right-radius","border-bottom-style","border-bottom-width","border-collapse"
                ,"border-color","border-image","border-image-outset","border-image-repeat","border-image-slice"
                ,"border-image-source","border-image-width","border-left","border-left-color","border-left-style"
                ,"border-left-width","border-right","border-right-color","border-right-style","border-right-width"
                ,"border-spacing","border-style","border-top","border-top-color","border-top-left-radius"
                ,"border-top-right-radius","border-top-style","border-top-width","border-width","bottom","box-align"
                ,"box-decoration-break","box-direction","box-flex","box-flex-group","box-lines","box-ordinal-group"
                ,"box-orient","box-pack","box-shadow","box-sizing","break-after","break-before","break-inside"
                ,"caption-side","clear","clip","color","color-profile","column-count","column-fill","column-gap"
                ,"column-rule","column-rule-color","column-rule-style","column-rule-width","column-span"
                ,"column-width","columns","content","counter-increment","counter-reset","crop","cue","cue-after"
                ,"cue-before","cursor","direction","display","dominant-baseline","drop-initial-after-adjust"
                ,"drop-initial-after-align","drop-initial-before-adjust","drop-initial-before-align"
                ,"drop-initial-size","drop-initial-value","elevation","empty-cells","filter","fit","fit-position"
                ,"float-offset","font","font-effect","font-emphasize","font-family","font-size"
                ,"font-size-adjust","font-stretch","font-style","font-variant","grid-columns","grid-rows"
                ,"hanging-punctuation","height","hyphenate-after","hyphenate-before","hyphenate-character"
                ,"hyphenate-lines","hyphenate-resource","hyphens","icon","image-orientation","image-rendering"
                ,"image-resolution","inline-box-align","left","letter-spacing","line-height","line-stacking"
                ,"line-stacking-ruby","line-stacking-shift","line-stacking-strategy","list-style","list-style-image"
                ,"list-style-position","list-style-type","margin","margin-bottom","margin-left","margin-right"
                ,"margin-top","mark","mark-after","mark-before","marker-offset","marks","marquee-direction"
                ,"marquee-play-count","marquee-speed","marquee-style","max-height","max-width","min-height"
                ,"min-width","move-to","nav-down","nav-index","nav-left","nav-right","nav-up","opacity","orphans"
                ,"outline","outline-color","outline-offset","outline-style","outline-width","overflow","overflow-style"
                ,"overflow-x","overflow-y","padding","padding-bottom","padding-left","padding-right","padding-top","page"
                ,"page-break-after","page-break-before","page-break-inside","page-policy","pause","pause-after"
                ,"pause-before","perspective","perspective-origin","phonemes","pitch","pitch-range","play-during"
                ,"position","presentation-level","punctuation-trim","quotes","rendering-intent","resize","rest"
                ,"rest-after","rest-before","richness","right","rotation","rotation-point","ruby-align","ruby-overhang"
                ,"ruby-position","ruby-span","size","speak","speak-header","speak-numeral","speak-punctuation","speech-rate"
                ,"stress","string-set","table-layout","target","target-name","target-new","target-position"
                ,"text-align","text-align-last","text-decoration","text-emphasis","text-height","text-indent"
                ,"text-justify","text-outline","text-overflow","text-shadow","text-transform","text-wrap","top"
                ,"transform","transform-origin","transform-style","transition","transition-delay","transition-duration"
                ,"transition-property","transition-timing-function","unicode-bidi","vertical-align","visibility"
                ,"voice-balance","voice-duration","voice-family","voice-pitch","voice-pitch-range","voice-rate"
                ,"voice-stress","voice-volume","volume","white-space","white-space-collapse","widows","width","word-break"
                ,"word-spacing","word-wrap","fixed","linear-gradient","color-dodge","center","content-box"
                ,"-webkit-flex","flex","row-reverse","space-around"
                ,"first","justify","inter-word","uppercase","lowercase","capitalize","nowrap","break-all","break-word","overline"
                ,"line-through","wavy","myFirstFont","sensation"};
            int i = 0;
            foreach(string T in csskey)
            {
                PrpertyValue.Add((CssProperty)i, T);
                i++;
            }
        }
        void setHTML()
        {
            tag = new Dictionary<Tag, string>();
            string[] tags = {"h1","h2","h3","h4","h5","h6","body","a","img","ol"," ul","li","table","tr","th","nav","heder","footer","form","option","select","button","textarea","input","audio","video","iframe","hr","em","div","pre","p" , "span" };
            int i = 0;
            foreach (string T in tags)
            {
                tag.Add((Tag)i, T);
                i++;
            }
        }
        //
        //
        //  Public 
        //
        //
        public override string ToString()
        {
            string ToRreturn = string.Empty;
            foreach (TagWithCSS T in TagWithCSSList)
            {
#if DEBUG
                ToRreturn += "//---------------------------" + T.TagName + "-----------------------------\n";
#endif
                ToRreturn += T.TagName;
                ToRreturn += "\n{\n";
                foreach (Property p in T.Properties)
                {
                    ToRreturn += "\t" + p.PropertyName + ":" + p.PropertyValue + ";\n";
                }
                ToRreturn += "}\n";

            }
            return ToRreturn;
        }
        /// <summary>
        /// Set and return Css Code.
        /// </summary>
        [DefaultValue(typeof (string), "")]
        [Description("Set and return Css Code.")]
        public string Css { set { TagWithCSSList = GetTagWithCSS(value); } get { return this.ToString(); } }
        /// <summary>
        /// Remove property form given tag.
        /// </summary>
        /// <param name="Tag">Tage to remove property.</param>
        /// <param name="Proverty">Property to remove.</param>
        /// <returns>True if removed.</returns>
        public bool RemoveProperty(string Tag, CssProperty Proverty)
        {
            if (Tag == "" || Tag == string.Empty || Tag == "")
                throw new NULL_TAG();
            int pointinTagWithCSSList = 0;
            int pointinProperties = 0;
            string ProvertyName = PrpertyValue[Proverty];
            foreach (TagWithCSS T in TagWithCSSList)
            {
                if (T.TagName.Equals(Tag, StringComparison.InvariantCultureIgnoreCase))
                {
                    foreach (Property p in T.Properties)
                    {
                        if (p.PropertyName.Equals(ProvertyName, StringComparison.InvariantCultureIgnoreCase))
                        {
                            TagWithCSSList[pointinTagWithCSSList].Properties.RemoveAt(pointinProperties);
                            return true;
                        }
                        pointinProperties++;
                    }

                }
                pointinTagWithCSSList++;
            }
            return false;
        }
        /// <summary>
        /// Remove property form given tag.
        /// </summary>
        /// <param name="Tag">Tage to remove property.</param>
        /// <param name="Proverty">Property to remove.</param>
        /// <returns>True if removed.</returns>
        public bool RemoveProperty(Tag tag, CssProperty Proverty)
        {
            string _ = this.tag[tag];
            return RemoveProperty(_, Proverty);
        }
        /// <summary>
        /// Remove Tag and it's properties.
        /// </summary>
        /// <param name="Tag">Tag to remove.</param>
        /// <returns>True if removed.</returns>
        public bool RemoveTag(string Tag)
        {
            if (Tag == "" || Tag == string.Empty || Tag == "")
                throw new NULL_TAG();
            int pointinTagWithCSSList = 0;

            foreach (TagWithCSS T in TagWithCSSList)
            {
                if (T.TagName.Equals(Tag, StringComparison.InvariantCultureIgnoreCase))
                {
                    TagWithCSSList.RemoveAt(pointinTagWithCSSList);
                    return true;
                }
                pointinTagWithCSSList++;
            }
            return false;
        }
        /// <summary>
        /// Remove Tag and it's properties.
        /// </summary>
        /// <param name="Tag">Tag to remove.</param>
        /// <returns>True if removed.</returns>
        public bool RemoveTag(Tag tag)
        {
            string _ = this.tag[tag];
            return RemoveTag(_);
        }
        /// <summary>
        /// Get list of properties on tag.
        /// </summary>
        /// <param name="Tag">Tag to get properties</param>
        /// <returns>Property List</returns>
        public List<Property> GetProperties(string Tag)
        {
            if (Tag == "" || Tag == string.Empty || Tag == "")
                throw new NULL_TAG();
            int pointinTagWithCSSList = 0;

            foreach (TagWithCSS T in TagWithCSSList)
            {
                if (T.TagName.Equals(Tag, StringComparison.InvariantCultureIgnoreCase))
                {
                    return T.Properties;
                }
                pointinTagWithCSSList++;
            }
            return new List<Property>();
        }
        /// <summary>
        /// Get list of properties on tag.
        /// </summary>
        /// <param name="Tag">Tag to get properties</param>
        /// <returns>Property List</returns>
        public List<Property> GetProperties(Tag tag)
        {
            string _ = this.tag[tag];
            return GetProperties(_);
        }
        /// <summary>
        /// Get property.
        /// </summary>
        /// <param name="Tag">Name of tag.</param>
        /// <param name="Property">Property to value.</param>
        /// <returns>Property</returns>
        public Property GetPropertie(string Tag, CssProperty Property)
        {
            if (Tag == "" || Tag == string.Empty || Tag == "")
                throw new NULL_TAG();
            int pointinTagWithCSSList = 0;

            foreach (TagWithCSS T in TagWithCSSList)
            {
                if (T.TagName.Equals(Tag, StringComparison.InvariantCultureIgnoreCase))
                {
                    foreach (Property p in T.Properties)
                    {
                        if (p.PropertyName.Equals(PrpertyValue[Property], StringComparison.InvariantCultureIgnoreCase))
                            return p;
                    }
                }
                pointinTagWithCSSList++;
            }
            return new Property();
        }
        /// <summary>
        /// Get property.
        /// </summary>
        /// <param name="Tag">Name of tag.</param>
        /// <param name="Property">Property to value.</param>
        /// <returns>Property</returns>
        public Property GetPropertie(Tag tag, CssProperty Property)
        {
            string _ = this.tag[tag];
            return GetPropertie(_, Property);
        }
        /// <summary>
        /// Add/Overwrite property or property's value of tag.  
        /// </summary>
        /// <param name="Tag">Tag name.</param>
        /// <param name="property">Property to add/overwrite</param>
        /// <param name="PropertValue">Property's valueto add/overwrite</param>
        /// <param name="Type">Type of tag (HTML tag ,Class or Id).Deffalt HTML Tag.</param>
        /// <returns>Added/Overwrited or not.</returns>
        public bool AddPropery(string Tag, CssProperty property, string PropertValue, TagType Type = TagType.Tag)
        {
            if (Tag == "" || Tag == string.Empty || Tag == "")
                throw new NULL_TAG();
            if (PropertValue == "" || PropertValue == string.Empty || PropertValue == "")
                throw new NULL_PRORERTY_VALUE();
            int pointinTagWithCSSList = 0;
            int pointinProperties = 0;
            bool notfound = false;
            bool added = false;

            bool tagcannotexist = true;
            bool tagExist = false;

            if (Type == TagType.Class) { Tag = "." + Tag; }
            if (Type == TagType.Id) { Tag = "#" + Tag; }

            string ProvertyName = PrpertyValue[property];

            foreach (TagWithCSS T in TagWithCSSList)
            {
                if (T.TagName.Equals(Tag, StringComparison.InvariantCultureIgnoreCase))
                {
                    foreach (Property p in T.Properties)
                    {
                        if (p.PropertyName.Equals(ProvertyName, StringComparison.InvariantCultureIgnoreCase))
                        {
                            Property np = new Property();
                            np.PropertyName = ProvertyName;
                            np.PropertyValue = PropertValue;
                            TagWithCSSList[pointinTagWithCSSList].Properties[pointinProperties] = np;
                            added = true;
                            return true;//break;
                        }
                        notfound = true;
                        pointinProperties++;
                    }

                    if (notfound && !added)
                    {
                        Property np = new Property();
                        np.PropertyName = ProvertyName;
                        np.PropertyValue = PropertValue;
                        TagWithCSSList[pointinTagWithCSSList].Properties.Add(np);
                        added = true;
                        return true;//break;                 
                    }
                    tagExist = true;
                    tagcannotexist = false;
                }
                else
                {
                    tagExist = false;
                    tagcannotexist = true;
                }
                pointinTagWithCSSList++;
            }
            if (tagcannotexist && !tagExist)
            {
                TagWithCSS t = new TagWithCSS();
                t.TagName = Tag;
                Property p = new Property();
                p.PropertyName = ProvertyName;
                p.PropertyValue = PropertValue;
                List<Property> pl = new List<Property>();
                pl.Add(p);
                t.Properties = pl;
                TagWithCSSList.Add(t);
                return true;//break;
            }
            return false;
        }
        /// <summary>
        /// Add/Overwrite property or property's value of tag.  
        /// </summary>
        /// <param name="Tag">Tag name.</param>
        /// <param name="property">Property to add/overwrite</param>
        /// <param name="PropertValue">Property's valueto add/overwrite</param>
        /// <param name="Type">Type of tag (HTML tag ,Class or Id).Deffalt HTML Tag.</param>
        /// <returns>Added/Overwrited or not.</returns>
        public bool AddPropery(Tag tag, CssProperty property, string PropertValue)
        {
            string _ = this.tag[tag];
            return AddPropery(_, property, PropertValue);
        }
        /// <summary>
        /// Initilise the pasrser with Css code.
        /// </summary>
        /// <param name="input">CSS code.</param>
        public CssParser(string input)
        {
            setHTML();
            SetPrpertyValue();
            Css = input;
        }
        /// <summary>
        /// Initilise the pasrser without Css code.
        /// </summary>
        public CssParser()
        {
            setHTML();
            SetPrpertyValue();
            TagWithCSSList = new List<TagWithCSS>();
        }
        /// <summary>
        /// Check the existance of tag.
        /// </summary>
        /// <param name="Tag">Name of tag.</param>
        /// <returns>True if exist.</returns>
        public bool TagExist(string Tag)
        {
            if (Tag == "" || Tag == string.Empty || Tag == "")
                throw new NULL_TAG();
            foreach (TagWithCSS T in TagWithCSSList)
            {
                if (T.TagName.Equals(Tag, StringComparison.InvariantCultureIgnoreCase))
                {
                    return true;
                }
            }
            return false;
        }   
        /// <summary>
            /// Check the existance of tag.
            /// </summary>
            /// <param name="Tag">Name of tag.</param>
            /// <returns>True if exist.</returns>
        public bool TagExist(Tag tag)
        {
            string _ = this.tag[tag];
            return TagExist(_);
        }
    }
}