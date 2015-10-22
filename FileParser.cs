using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace HostsFileEditorUI
{
    public class FileParser
    {
        static readonly string Path = Environment.SystemDirectory + @"\drivers\etc\hosts";

        public List<HostEntry> Parse()
        {
            var result = new List<HostEntry>();

            string fileContent;

            if (ReadFile(out fileContent))
            {
                var lines = fileContent.Split('\n');
                int i = 0;

                foreach (var line in lines)
                {
                    HostEntry host;
                    if (!TryParseHost(line, out host)) continue;
                    host.EntryNumber = ++i;
                    result.Add(host);
                }
            }

            return result;
        }

        public bool Save(List<HostEntry> entries)
        {

            try
            {
                System.IO.File.WriteAllLines(Path, entries.Select(e => e.ToHostString()), Encoding.ASCII);
                return true;
            }
            catch (Exception ex)
            {
                System.Diagnostics.EventLog.WriteEntry("HostsFileUIEditor", ex.ToString());
                return false;
            }

        }

        private bool TryParseHost(string line, out HostEntry host)
        {
            host = null;
            var isactive = !HostRegEx.RegexActive.IsMatch(line);

            Match matchip = HostRegEx.RegexIp.Match(line);
            var ip = matchip.Success ? matchip.Value : string.Empty;

            Match matchdomain = HostRegEx.RegexHost.Match(line);
            var domain = matchdomain.Success ? matchdomain.Value : string.Empty;

            Match matchcomment = HostRegEx.RegexComment.Match(line);
            var comment = (matchcomment.Success && matchcomment.Groups.Count == 2) ? matchcomment.Groups[1].Value.Trim() : string.Empty;

            if (matchip.Success && matchdomain.Success)
            {
                host = new HostEntry() { IsActive = isactive, DomainName = domain, Ip = ip, Comment = comment };
                return true;
            }

            return false;
        }

        private bool ReadFile(out string fileContent)
        {
            try
            {
                fileContent = System.IO.File.ReadAllText(Path, Encoding.ASCII);
                return true;
            }
            catch (Exception ex)
            {
                fileContent = string.Empty;
                System.Diagnostics.EventLog.WriteEntry("HostsFileUIEditor", ex.ToString());
                return false;
            }

        }

    }
}
