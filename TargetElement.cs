using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace WebScraper {
    public class TargetElement {
        public string TargetRegex { get; set; }
        public RegexOptions RegexOption { get; set; }
        public List<TargetElement> Children { get; set; }

        public TargetElement() {
            TargetRegex = string.Empty;
            RegexOption = RegexOptions.None;
            Children = new List<TargetElement>();
        }
        public bool HasChildren() {
            return Children.Any();
        }
    }
}
