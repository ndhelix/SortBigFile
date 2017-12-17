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
        static int _sortSizeMbLimit = 100;
        static int _linelimit = _sortSizeMbLimit * 10000;
                         //10000000
        internal void SortFile( string filename, string sortedfilename )
        {
            long sizeMb = new System.IO.FileInfo( filename ).Length / 1024 / 1024;

            if (sizeMb < _sortSizeMbLimit)
            {
                SortSmallFile( filename, sortedfilename );
                return;
            }

            List<string> smallfilelist = CreateSmallFiles( filename );
            List<string> smallfilelistsorted = SortSmallFiles( smallfilelist );
            //MergeFiles( smallfilelistsorted, sortedfilename );
        }

        private List<string> SortSmallFiles( List<string> smallfilelist )
        {
            List<string> sortedfilelist = new List<string>();
            var TaskList = new List<Task>();
            foreach (string file in smallfilelist)
            {
                string sortedfile = file + "_sorted";
                sortedfilelist.Add( sortedfile );
                Task task = Task.Run( () => SortSmallFile( file, sortedfile ) );
                TaskList.Add( task );
            }
            Task.WaitAll( TaskList.ToArray() );
            return sortedfilelist;
        }

        private List<string> CreateSmallFiles( string filename )
        {
            string sortareadir = Path.Combine( Path.GetDirectoryName( filename ), "sortarea" );
            if (!Directory.Exists( sortareadir ))
                Directory.CreateDirectory( sortareadir );
            List<string> files = new List<string>();
            StreamWriter sw;
            
            using (StreamReader sr = new StreamReader( filename ))
            {
                var sdict = new List<string>( 10000000 );
                int counter = 0;
                int filecounter = 0;
                string line;
                string smallfile = Path.Combine( sortareadir, filecounter.ToString( "000" ) );
                //sw = new StreamWriter( File.Open( smallfile, FileMode.Create ) );
                while ((line = sr.ReadLine()) != null)
                {
                    if (counter == _linelimit)
                    {
                        smallfile = Path.Combine( sortareadir, filecounter.ToString( "000" ) );
                        sw = new StreamWriter( File.Open( smallfile, FileMode.Create ) );
                        foreach (string item in sdict)
                        {
                            sw.WriteLine( item );
                        }
                        sw.Dispose();
                        filecounter++;
                        files.Add( smallfile );
                        sdict.Clear();
                        counter = 0;
                     }
                    sdict.Add( line );
                    counter++;
                }
                if (sdict.Count > 0)
                {
                    smallfile = Path.Combine( sortareadir, filecounter.ToString( "000" ) );
                    sw = new StreamWriter( File.Open( smallfile, FileMode.Create ) );
                    foreach (string item in sdict)
                    {
                        sw.WriteLine( item );
                    }
                    sw.Dispose();
                    files.Add( smallfile );
                }
            }

            return files;
        }

        private static void SortSmallFile( string filename, string sortedfilename )
        {
            var sdict = new List<string>( 10000000 );

            using (StreamReader sr = new StreamReader( filename ))
            {
                int i = 0;
                string line;
                while ((line = sr.ReadLine()) != null)
                {
                    sdict.Add( line );
                }
            }

            sdict.Sort();

            using (var bw = new StreamWriter( File.Open( sortedfilename, FileMode.Create ) ))
            {
                foreach (string item in sdict)
                {
                    bw.WriteLine( item );
                }
            }

            sdict = null;
            GC.Collect();
        }
    }
}
