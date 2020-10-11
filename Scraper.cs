using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text;
using System.Text.Json;
using System.Text.RegularExpressions;

namespace WebScraper {
    public class Scraper {
        public string BaseURL { get; set; }
        public List<TargetElement> Config { get; set; }

        public Scraper() {
            BaseURL = string.Empty;
            Config = new List<TargetElement>();
        }
        public Scraper(List<TargetElement> config, string baseURL) {
            Config = config;
            BaseURL = baseURL;
        }

        public string ScrapeSiteHTML(string pathToPage) {
            string content;
            using (WebClient client = new WebClient()) {
                byte[] contentBytes = client.DownloadData($"{BaseURL}{pathToPage}");
                content = Encoding.UTF8.GetString(contentBytes);
            }
            return content;
        }
        public List<string> Scrape(string pathToPage) {
            var content = ScrapeSiteHTML(pathToPage).ToLower();
            List<string> scrapedElements = new List<string>();
            foreach (var child in Config)
                ScrapeElements(content, child, scrapedElements);
            return scrapedElements;
        }
        private void ScrapeElements(string data, TargetElement element, List<string> scrapedElements) {
            MatchCollection matches = Regex.Matches(data, element.TargetRegex, element.RegexOption);
            foreach (Match match in matches) {
                if (match.Success) {
                    for (int idx = 1; idx < match.Groups.Count; idx++) {
                        var matchData = match.Groups[idx].Value;
                        if (element.HasChildren())
                            foreach (var child in element.Children)
                                ScrapeElements(matchData, child, scrapedElements);
                        else scrapedElements.Add(matchData);
                    }
                }
            }
        }

        public static Scraper DeserializeFromFile(string fileName) {
            var options = new JsonSerializerOptions() {
                WriteIndented = true
            };
            var jsonBytes = File.ReadAllBytes(fileName);
            return JsonSerializer.Deserialize<Scraper>(jsonBytes, options);
        }
        public void SerializeToFile(string fileName) {
            var options = new JsonSerializerOptions() {
                WriteIndented = true
            };
            var jsonBytes = JsonSerializer.SerializeToUtf8Bytes(this, options);
            File.WriteAllBytes(fileName, jsonBytes);
        }
    }
}
