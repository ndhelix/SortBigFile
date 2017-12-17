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
        static int _smallfilesize = 100*1024*1024;
        //static int _linelimit = _sortSizeMbLimit * 10000;
                         //10000000
        internal void SortFile( string filename, string sortedfilename )
        {
            long sizeMb = new System.IO.FileInfo( filename ).Length / 1024 / 1024;

            if (sizeMb < _sortSizeMbLimit)
            {
                SortSmallFile( filename, sortedfilename, true );
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
                Task task = Task.Run( () => SortSmallFile( file, sortedfile, false ) );
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
            //StreamWriter sw;
            var TaskList = new List<Task>();
            using (StreamReader sr = new StreamReader( filename ))
            {
                
                var listlist = new List<List<string>>( );
                listlist.Add( new List<string>( 10000000 ) );
                //int counter = 0;
                int filecounter = 0;
                long currentsize = 0;
                string line;
                string smallfile = "";
                //sw = new StreamWriter( File.Open( smallfile, FileMode.Create ) );
                while ((line = sr.ReadLine()) != null)
                {
                    if (currentsize >= _smallfilesize)
                    {
                        smallfile = Path.Combine( sortareadir, filecounter.ToString( "0000" ) );

                        //WriteListToFile( list, smallfile );
                        filecounter++;
                        Task task = Task.Run( () => WriteListToFile( listlist[filecounter-1], smallfile ) );
                        TaskList.Add( task );

                        listlist.Add( new List<string>( 10000000 ) );
                        files.Add( smallfile );
                        currentsize = 0;
                    }
                    listlist[filecounter].Add( SwapLine(line) );
                    currentsize+=line.Length;
                }
                if (listlist[filecounter].Count > 0)
                {
                    smallfile = Path.Combine( sortareadir, filecounter.ToString( "0000" ) );
                    WriteListToFile( listlist[filecounter], smallfile );
                    files.Add( smallfile );
                }
                listlist[filecounter] = null;
                listlist = null;
            }
            
            Task.WaitAll( TaskList.ToArray() );

            GC.Collect();

            return files;
        }

        private StreamWriter WriteListToFile( List<string> list, string smallfile )
        {
            StreamWriter sw = new StreamWriter( File.Open( smallfile, FileMode.Create ) );
            foreach (string item in list)
            {
                sw.WriteLine( item );
            }
            sw.Dispose();
            list = null;
            return sw;
        }

        private void SortSmallFile( string filename, string sortedfilename, bool swap )
        {
            var sdict = new List<string>( 10000000 );

            using (StreamReader sr = new StreamReader( filename ))
            {
                int i = 0;
                string line;
                while ((line = sr.ReadLine()) != null)
                {
                    if (swap)
                        line = SwapLine( line );
                    sdict.Add( line );
                }
            }

            sdict.Sort();

            using (var bw = new StreamWriter( File.Open( sortedfilename, FileMode.Create ) ))
            {
                foreach (string item in sdict)
                {
                    bw.WriteLine( swap ? UnSwapLine( item ): item );
                }
            }

            sdict = null;
            GC.Collect();
        }

        private string UnSwapLine( string item )
        {
            var arr = item.Split( '\t' );
            if (arr.Length < 1)
                return item;
            return arr[1].TrimStart( '0' ) + ". " + arr[0];
        }

        private string SwapLine( string line )
        {
            int pos = line.IndexOf( ". " );
            if (pos == -1)
                return line;
            string strnum = line.Substring( 0, pos );
            int num = int.Parse( strnum );
            string ret = line.Substring( pos + 2 ) + "\t" + num.ToString( "0000000000" );
            return ret;
        }
    }
}
