using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ChromeRemoto.Utilities
{
    public class Utilidades
    {
        public string GetDomainFromUrl(string Url)
        {
            return GetDomainFromUrl(new Uri(Url));
        }

        public string GetDomainFromUrl(string Url, bool Strict)
        {
            return GetDomainFromUrl(new Uri(Url), Strict);
        }

        public string GetDomainFromUrl(Uri Url)
        {
            return GetDomainFromUrl(Url, false);
        }

        public string GetDomainFromUrl(Uri Url, bool Strict)
        {
            initializeTLD();
            if (Url == null) return null;
            var dotBits = Url.Host.Split('.');
            if (dotBits.Length == 1) return Url.Host; //eg http://localhost/blah.php = "localhost"
            if (dotBits.Length == 2) return Url.Host; //eg http://blah.co/blah.php = "localhost"
            string bestMatch = "";
            foreach (var tld in DOMAINS)
            {
                if (!Url.Host.EndsWith("." + tld, StringComparison.InvariantCultureIgnoreCase)) continue;

                if (Url.Host.EndsWith(tld, StringComparison.InvariantCultureIgnoreCase))
                {
                    if (tld.Length > bestMatch.Length) bestMatch = tld;
                }
            }
            if (string.IsNullOrEmpty(bestMatch))
                return Url.Host; //eg http://domain.com/blah = "domain.com"

            //add the domain name onto tld
            string[] bestBits = bestMatch.Split('.');
            string[] inputBits = Url.Host.Split('.');
            int getLastBits = bestBits.Length + 1;
            bestMatch = "";
            for (int c = inputBits.Length - getLastBits; c < inputBits.Length; c++)
            {
                if (bestMatch.Length > 0) bestMatch += ".";
                bestMatch += inputBits[c];
            }
            return bestMatch;
        }


        private void initializeTLD()
        {
            if (DOMAINS.Count > 0) return;

            string line;
            StreamReader reader = File.OpenText($"{Application.StartupPath}\\Assets\\effective_tld_names.dat");
            while ((line = reader.ReadLine()) != null)
            {
                if (!string.IsNullOrEmpty(line) && !line.StartsWith("//"))
                {
                    DOMAINS.Add(line);
                }
            }
            reader.Close();
        }


        // This file was taken from https://publicsuffix.org/list/effective_tld_names.dat

        public List<String> DOMAINS = new List<String>();
        

        public string GetDomainName(string url)
        {
            string domain = new Uri(url).DnsSafeHost.ToLower();
            var tokens = domain.Split('.');
            if (tokens.Length > 2)
            {
                //Add only second level exceptions to the < 3 rule here
                string[] exceptions = { "info", "firm", "name", "com", "biz", "gen", "ltd", "web", "net", "pro", "org" };
                var validTokens = 2 + ((tokens[tokens.Length - 2].Length < 3 || exceptions.Contains(tokens[tokens.Length - 2])) ? 1 : 0);
                domain = string.Join(".", tokens, tokens.Length - validTokens, validTokens);
            }

            return domain;
        }
    }
}
