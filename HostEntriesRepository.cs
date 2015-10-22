using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HostsFileEditorUI
{
    public class HostEntriesRepository
    {
        private static readonly object Locker = new object();

        private static List<HostEntry> _entries;

        public List<HostEntry> GetHostEntries()
        {
            lock (Locker)
                _entries = new FileParser().Parse();
            return _entries;
        }

        public void SaveHostEntries()
        {
            lock (Locker)
                new FileParser().Save(_entries);
        }
    }
}
