using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SortBigFile
{
    public class Sorter
    {
        internal void SortFile( string filename, string sortedfilename )
        {
            //10 000 000
            var sdict = new List<string>(10000010);
            var sarray = new string[10000010] ;
            //var sdict = new SortedSet<string>();

            using (StreamReader sr = new StreamReader( filename ))
            {
                int i = 0;
                String line;
                while ((line = sr.ReadLine()) != null)
                {
                    sdict.Add( line );
                    //sarray[i++] = line;
                }
            }

            sdict.Sort();
            //Array.Sort( sarray );

            using (var bw = new StreamWriter( File.Open( sortedfilename, FileMode.Create ) ))
            {
                foreach(string item in sdict)
                {
                    bw.WriteLine( item );
                }
            }

        }
    }
}
