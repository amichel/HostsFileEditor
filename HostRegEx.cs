using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace HostsFileEditorUI
{
    public static class HostRegEx
    {
        public static readonly Regex RegexActive = new Regex(@"^\s*\#+", RegexOptions.IgnoreCase | RegexOptions.Compiled);
        public static readonly Regex RegexIp = new Regex(@"(\b(?:(?:25[0-5]|2[0-4][0-9]|1[0-9][0-9]|[1-9]?[0-9])\.){3}(?:25[0-5]|2[0-4][0-9]|1[0-9][0-9]|[1-9]?[0-9])\b)|((?<![:.\w])(?:[A-F0-9]{1,4}:){7}[A-F0-9]{1,4}(?![:.\w]))", RegexOptions.IgnoreCase | RegexOptions.Compiled);
        public static readonly Regex RegexHost = new Regex(@"(\b((xn--)?[a-z0-9]+(-[a-z0-9]+)*\.)+[a-z]{2,}\b)|(localhost)", RegexOptions.IgnoreCase | RegexOptions.Compiled);
        public static readonly Regex RegexComment = new Regex(@"^.+\w+\s+\#+(.+)", RegexOptions.IgnoreCase | RegexOptions.Compiled);

    }
}
