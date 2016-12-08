using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace JoCSSParser
{
    public class Parser
    {
        Dictionary<CssProperty, string> PrpertyValue;
        public override string ToString()
        {
            string ToRreturn = string.Empty;
            foreach(TagWithCSS T in TagWithCSSList)
            {
#if DEBUG
                ToRreturn += "//---------------------------" + T.TagName + "-----------------------------\n";
#endif
                ToRreturn += T.TagName;
                ToRreturn += "\n{\n";
                foreach(Property p in T.Properties)
                {
                    ToRreturn += "\t" + p.PropertyName + ":" + p.PropertyValue + ";\n";
                }
                ToRreturn += "}\n";

            }
            return ToRreturn;
        }
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
            string pattern = @"(?<selector>(?:(?:[^,{]+),?)*?)\{(?:(?<name>[^}:]+):?(?<value>[^};]+);?)*?\}"/*@"(?<=\}\s*)(?<selector>[^\{\}]+?)(?:\s*\{(?<style>[^\{\}]+)\})"*/;

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
        public bool RemoveProperty(string Tag, string ProvertyName)
        {
            int pointinTagWithCSSList = 0;
            int pointinProperties = 0;

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
        public bool RemoveTag(string Tag)
        {
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
        public List<Property> GetProperties(string Tag)
        {
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
        public Property GetPropertie(string Tag,string PropertyName)
        {
            int pointinTagWithCSSList = 0;

            foreach (TagWithCSS T in TagWithCSSList)
            {
                if (T.TagName.Equals(Tag, StringComparison.InvariantCultureIgnoreCase))
                {
                   foreach(Property p in T.Properties)
                    {
                        if (p.PropertyName.Equals(PropertyName, StringComparison.InvariantCultureIgnoreCase))
                            return p;
                    }
                }
                pointinTagWithCSSList++;
            }
            return new Property();
        }
        public void SetCSS(string input)
        {
            TagWithCSSList = GetTagWithCSS(input);
        }
        public bool AddPropery(string Tag, string ProvertyName, string PropertValue, TagType Type = TagType.Tag)
        {
            int pointinTagWithCSSList = 0;
            int pointinProperties = 0;
            bool notfound = false;
            bool added = false;

            bool tagcannotexist = true;
            bool tagExist = false;

            if (Type == TagType.Class) { Tag = "." + Tag; }
            if (Type == TagType.Id) { Tag = "#" + Tag; }

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
        public Parser(string input)
        {
            SetPrpertyValue();
            SetCSS(input);
        }
        public Parser()
        {
            SetPrpertyValue();
            TagWithCSSList = new List<TagWithCSS>();
        }
        public bool TagExist(string Tag)
        {    
            foreach (TagWithCSS T in TagWithCSSList)
            {
                if (T.TagName.Equals(Tag, StringComparison.InvariantCultureIgnoreCase))
                {
                    return true;
                }
               
            }
            return false;
        }

        void SetPrpertyValue()
        {
            PrpertyValue = new Dictionary<CssProperty, string>();
            string[] csskey = { "font-weight:", "border-radius:", "color-stop:", "alignment-adjust:","alignment-baseline"
                ,"animation:","animation-delay:","animation-direction:","animation-duration:","animation-iteration-count"
                ,"animation-name:","animation-play-state:","animation-timing-function:","appearance:","azimuth"
                ,"backface-visibility:","background:","background-attachment:","background-break:","background-clip"
                ,"background-color:","background-image:","background-origin:","background-position:","background-repeat"
                ,"background-size:","baseline-shift:","binding:","bleed:","bookmark-label:","bookmark-level:","bookmark-state"
                ,"bookmark-target:","border:","border:","border-bottom:","border-bottom-color:","border-bottom-left-radius"
                ,"border-bottom-right-radius:","border-bottom-style:","border-bottom-width:","border-collapse"
                ,"border-color:","border-image:","border-image-outset:","border-image-repeat:","border-image-slice"
                ,"border-image-source:","border-image-width:","border-left:","border-left-color:","border-left-style"
                ,"border-left-width:","border-right:","border-right-color:","border-right-style:","border-right-width"
                ,"border-spacing:","border-style:","border-top:","border-top-color:","border-top-left-radius"
                ,"border-top-right-radius:","border-top-style:","border-top-width:","border-width:","bottom:","box-align"
                ,"box-decoration-break:","box-direction:","box-flex:","box-flex-group:","box-lines:","box-ordinal-group"
                ,"box-orient:","box-pack:","box-shadow:","box-sizing:","break-after:","break-before:","break-inside"
                ,"caption-side:","clear:","clip:","color:","color-profile:","column-count:","column-fill:","column-gap"
                ,"column-rule:","column-rule-color:","column-rule-style:","column-rule-width:","column-span"
                ,"column-width:","columns:","content:","counter-increment:","counter-reset:","crop:","cue:","cue-after"
                ,"cue-before:","cursor:","direction:","display:","dominant-baseline:","drop-initial-after-adjust"
                ,"drop-initial-after-align:","drop-initial-before-adjust:","drop-initial-before-align"
                ,"drop-initial-size:","drop-initial-value:","elevation:","empty-cells:","filter:","fit:","fit-position"
                ,"float:","float-offset:","font:","font-effect:","font-emphasize:","font-family:","font-size"
                ,"font-size-adjust:","font-stretch:","font-style:","font-variant:","grid-columns:","grid-rows"
                ,"hanging-punctuation:","height:","hyphenate-after:","hyphenate-before:","hyphenate-character"
                ,"hyphenate-lines:","hyphenate-resource:","hyphens:","icon:","image-orientation:","image-rendering"
                ,"image-resolution:","inline-box-align:","left:","letter-spacing:","line-height:","line-stacking"
                ,"line-stacking-ruby:","line-stacking-shift:","line-stacking-strategy:","list-style:","list-style-image"
                ,"list-style-position:","list-style-type:","margin:","margin-bottom:","margin-left:","margin-right"
                ,"margin-top:","mark:","mark-after:","mark-before:","marker-offset:","marks:","marquee-direction"
                ,"marquee-play-count:","marquee-speed:","marquee-style:","max-height:","max-width:","min-height"
                ,"min-width:","move-to:","nav-down:","nav-index:","nav-left:","nav-right:","nav-up:","opacity:","orphans"
                ,"outline:","outline-color:","outline-offset:","outline-style:","outline-width:","overflow:","overflow-style"
                ,"overflow-x:","overflow-y:","padding:","padding-bottom:","padding-left:","padding-right:","padding-top:","page"
                ,"page-break-after:","page-break-before:","page-break-inside:","page-policy:","pause:","pause-after"
                ,"pause-before:","perspective:","perspective-origin:","phonemes:","pitch:","pitch-range:","play-during"
                ,"position:","presentation-level:","punctuation-trim:","quotes:","rendering-intent:","resize:","rest"
                ,"rest-after:","rest-before:","richness:","right:","rotation:","rotation-point:","ruby-align:","ruby-overhang"
                ,"ruby-position:","ruby-span:","size:","speak:","speak-header:","speak-numeral:","speak-punctuation:","speech-rate"
                ,"stress:","string-set:","table-layout:","target:","target-name:","target-new:","target-position"
                ,"text-align:","text-align-last:","text-decoration:","text-emphasis:","text-height:","text-indent"
                ,"text-justify:","text-outline:","text-overflow:","text-shadow:","text-transform:","text-wrap:","top"
                ,"transform:","transform-origin:","transform-style:","transition:","transition-delay:","transition-duration"
                ,"transition-property:","transition-timing-function:","unicode-bidi:","vertical-align:","visibility"
                ,"voice-balance:","voice-duration:","voice-family:","voice-pitch:","voice-pitch-range:","voice-rate"
                ,"voice-stress:","voice-volume:","volume:","white-space:","white-space-collapse:","widows:","width:","word-break"
                ,"word-spacing:","word-wrap:","z-index)","no-repeat","fixed","linear-gradient","color-dodge","center","content-box"
                ,"solid","thick","dotted","2em","(border)","thick","double","absolute","left","both","rect","inline"
                ,"right","scroll","hidden","-webkit-flex","wrap","row-reverse","flex","row-reverse wrap","space-around"
                ,"first","justify","inter-word","uppercase","lowercase","capitalize","nowrap","break-all","break-word","overline"
                ,"line-through", "underline","wavy","myFirstFont","sensation","light.woff","arial","sans-serif","italic","bold"
                ,"Georgia","serif","expanded","normal","oblique","italic","small-caps","bold","rtl","bidi-override","collapse"
                ,"separate","hide","fixed","section","subsection","Section", "counter(section)","counter(subsection)","square","sqpurple"
                ,"circle","upper-roman","lower-alpha","infinite","mymove","alternate","forwards","paused","linear","rotate","preserve"
                ,"width","crosshair","help","wait","auto","outset","all","always","avoid","grayscale","font-family"};
        }
    }
}
