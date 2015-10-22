using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;

namespace HostsFileEditorUI
{
    public class HostEntry
    {
        public int EntryNumber { get; set; }
        public bool IsActive { get; set; }

        private string _domainName;
        public string DomainName
        {
            get { return _domainName; }
            set
            {
                if (!HostRegEx.RegexHost.IsMatch(value))
                    throw new ArgumentException("Invalid Domain Name!");

                _domainName = value;
            }
        }

        private string _ip;
        public string Ip
        {
            get { return _ip; }
            set
            {
                if (!HostRegEx.RegexIp.IsMatch(value))
                    throw new ArgumentException("Invalid IP!");

                _ip = value;
            }
        }


        public string Comment { get; set; }

        public string ToHostString()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append(IsActive ? "  " : "# ");
            sb.Append(Ip);

            for (int i = 21 - Ip.Length; i > 0; i--)
                sb.Append(" ");

            sb.Append(DomainName);

            if (!string.IsNullOrEmpty(Comment))
            {
                for (int i = 34 - DomainName.Length; i > 0; i--)
                    sb.Append(" ");
                sb.Append("#");
                sb.Append(Comment);
            }
            return sb.ToString();
        }


    }
}
