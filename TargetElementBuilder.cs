using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace WebScraper {
    public class TargetElementBuilder {
        private string TargetRegex { get; set; }
        private RegexOptions RegexOption { get; set; }
        private List<TargetElement> Children { get; set; }

        public TargetElementBuilder() {
            TargetRegex = string.Empty;
            RegexOption = RegexOptions.Singleline;
            Children = new List<TargetElement>();
        }
        public TargetElementBuilder WithTargetRegex(string targetRegex) {
            TargetRegex = targetRegex;
            return this;
        }
        public TargetElementBuilder WithRegexOption(RegexOptions regexOption) {
            RegexOption = regexOption;
            return this;
        }
        public TargetElementBuilder WithChild(TargetElement child) {
            Children.Add(child);
            return this;
        }
        public TargetElement Build() {
            return new TargetElement() {
                TargetRegex = TargetRegex,
                RegexOption = RegexOption,
                Children = Children
            };
        }
    }
}
