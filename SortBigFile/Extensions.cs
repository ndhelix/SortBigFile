using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SortBigFile
{
    public static class Extensions
    {
        public static void AddAnyway( this SortedDictionary<string, int> sd, string key, int val )
        {
            if (sd.ContainsKey( key ))
                AddAnyway( sd, key + '\t', val );
            else
                sd.Add( key, val );
        }
    }
}
