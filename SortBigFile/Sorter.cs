using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SortBigFile
{
    public class Sorter
    {
        static int _sortSizeLimit = 2 * 1024 * 1024;
        static int _mergepool = 25;
        static int _concurrentsortthreadcount = 10;
        static int _smallfilesize = 0;
        //static int _linelimit = _sortSizeMbLimit * 10000;
        //10000000
        internal void SortFile( string filename, string sortedfilename )
        {
            long size = new System.IO.FileInfo( filename ).Length;
            DateTime start = DateTime.Now;
            if (size < _sortSizeLimit)
            {
                SortSmallFile( filename, sortedfilename, true );
                return;
            }

            // this block was used to test on small files
            _smallfilesize = _sortSizeLimit / 2;
            if (_smallfilesize < size/500)
                _smallfilesize = (int)(size/200);

            List<string> smallfilelistsorted = CreateAndSortSmallFiles( filename );
            File.AppendAllText( "sorter.log", "CreateSmallFiles took " + (DateTime.Now - start).ToString( @"hh\:mm\:ss" ) + Environment.NewLine );
            start = DateTime.Now;

            //List<string> smallfilelistsorted = SortSmallFiles( smallfilelist );
            //File.AppendAllText( "sorter.log", "SortSmallFiles took " + ( DateTime.Now - start ).ToString( @"hh\:mm\:ss" ) + Environment.NewLine );
            //start = DateTime.Now;
            MergeFiles( smallfilelistsorted, sortedfilename+"merged", 0, true );
            File.AppendAllText( "sorter.log", "MergeFiles took " + ( DateTime.Now - start ).ToString( @"hh\:mm\:ss" )+Environment.NewLine );
        }

        private void MergeFiles( List<string> filelist, string outputfilename, int recursionlevel, bool isroot = false)
        {
            if (filelist.Count <= _mergepool)
            {
                MergePlain( filelist, outputfilename, isroot );
                return;
            }

            List<string> mergedfilelist = new List<string>();
            var TaskList = new List<Task>();
            var listlist = GetSubLists( filelist, _mergepool );
            int filecounter = 0;
            string dir = Path.GetDirectoryName( outputfilename );
            //if (recursionlevel == 0)
            dir = Path.Combine( dir, "sortarea" );
            foreach (var list in listlist)
            {
                string mergedfile = filecounter.ToString( "0000" ) + "_merged_rec" + recursionlevel;
                mergedfile = Path.Combine( dir, mergedfile );
                mergedfilelist.Add( mergedfile );
                Task task = Task.Run( () => MergeFiles( list, mergedfile, recursionlevel+2 ) );
                TaskList.Add( task );
                filecounter++;
            }
            Task.WaitAll( TaskList.ToArray() );
            MergeFiles( mergedfilelist, outputfilename, recursionlevel+1, isroot ) ;
        }

        private List<List<string>> GetSubLists( List<string> filelist, int maxcount )
        {
            List<List<string>> listlist = new List<List<string>>();
            for (int i = 0; i < filelist.Count; i += maxcount)
                listlist.Add( filelist.GetRange( i, Math.Min( maxcount, filelist.Count - i ) ) );
            return listlist;
        }
        private  void MergePlain( List<string> filelist, string outputfilename, bool unswap )
        {
            int buffersize = 50000;
            StreamWriter writer = new StreamWriter( File.Open( outputfilename, FileMode.Create ) );
            StreamReader[] srarr = new StreamReader[filelist.Count];
            Queue<string>[] q = new Queue<string>[filelist.Count];
            
            for (int i = 0; i < filelist.Count; i++)
                q[i] = new Queue<string>();

            for (int i = 0; i < filelist.Count; i++)
                srarr[i] = new StreamReader( filelist[i] );

            for (int i = 0; i < filelist.Count; i++)
            {
                int qlength = 0;
                while (!srarr[i].EndOfStream && qlength < buffersize)
                {
                    string line = srarr[i].ReadLine();
                    q[i].Enqueue( line );
                    qlength++;
                }
            }

            var sd = new SortedDictionary<string, int>();
            for (int i = 0; i < q.Length; i++)
                if (q[i].Count > 0)
                    sd.AddAnyway( q[i].Dequeue(), i);

            if (sd.Any())
            while (true)
            {
                if (!sd.Any())
                {
                    for (int i = 0; i < filelist.Count; i++)
                    {
                        int qlength = 0;
                        if (!q[i].Any())
                            while (!srarr[i].EndOfStream && qlength < buffersize)
                            {
                                string line = srarr[i].ReadLine();
                                q[i].Enqueue( line );
                                qlength++;
                            }
                    }

                    for (int i = 0; i < q.Length; i++)
                        if (q[i].Count > 0)
                            sd.AddAnyway( q[i].Dequeue(), i);
                }

                if (!sd.Any())
                    break;

                int filenum = sd.First().Value;
                string row = sd.First().Key.TrimEnd('\t');
                writer.WriteLine( unswap ? UnSwap( row ) : row );
                sd.Remove( sd.First().Key );
                if (!q[filenum].Any())
                {
                    int qlength = 0;
                    while (!srarr[filenum].EndOfStream && qlength < buffersize)
                    {
                        string line = srarr[filenum].ReadLine();
                        q[filenum].Enqueue( line );
                        qlength++;
                    }
                }

                if (q[filenum].Any())
                {
                    string key = q[filenum].Dequeue();
                    sd.AddAnyway( key, filenum );
                }

            }

            for (int i = 0; i < filelist.Count; i++)
                srarr[i].Dispose();

            foreach(string filename in filelist)
                if (File.Exists( filename ))
                    File.Delete( filename );

            writer.Dispose();
        }


        private List<string> SortSmallFiles( List<string> smallfilelist )
        {
            List<string> sortedfilelist = new List<string>();

            var listlist = GetSubLists( smallfilelist, _concurrentsortthreadcount );

            foreach (var list in listlist)
            {
                var TaskList = new List<Task>();
                foreach (string file in list)
                {
                    string sortedfile = file + "_sorted";
                    sortedfilelist.Add( sortedfile );
                    Task task = Task.Run( () => SortSmallFile( file, sortedfile, false ) );
                    TaskList.Add( task );
                }
                Task.WaitAll( TaskList.ToArray() );
            }
            return sortedfilelist;
        }

        private List<string> CreateAndSortSmallFiles( string filename )
        {
            string sortareadir = Path.Combine( Path.GetDirectoryName( filename ), "sortarea" );
            if (!Directory.Exists( sortareadir ))
                Directory.CreateDirectory( sortareadir );
            List<string> files = new List<string>();
            //StreamWriter sw;
            var TaskList = new List<Task>();
            Queue<string> q = new Queue<string>();

            using (StreamReader sr = new StreamReader( filename ))
            {
                var list = new List<string>( 10000000 );
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
                        q.Enqueue( smallfile );
                        WriteListToFile( list, smallfile );

                        if (q.Count >= 8)
                            while(q.Any())
                            {
                                string file = q.Dequeue();
                                string sortedfile = file + "_sorted";
                                Task task = Task.Run( () => SortSmallFile( file, sortedfile, false ) );
                                TaskList.Add( task );
                                files.Add( sortedfile );    
                            }

                        filecounter++;
                        list.Clear();
                        currentsize = 0;
                    }
                    list.Add( Swap(line) );
                    currentsize+=line.Length;
                }
                if (list.Count > 0)
                {
                    smallfile = Path.Combine( sortareadir, filecounter.ToString( "0000" ) );
                    WriteListToFile( list, smallfile );
                    q.Enqueue( smallfile );
                    //string sortedfile = smallfile + "_sorted";
                    //Task task = Task.Run( () => SortSmallFile( smallfile, sortedfile, false ) );
                    //TaskList.Add( task );
                    //files.Add( sortedfile );
                }
                list = null;
            }

            while (q.Any())
            {
                string file = q.Dequeue() ;
                string sortedfile = file + "_sorted";
                Task task = Task.Run( () => SortSmallFile( file, sortedfile, false ) );
                TaskList.Add( task );
                files.Add( sortedfile );
            }

            Task.WaitAll( TaskList.ToArray() );

            GC.Collect();

            return files;
        }

        private void WriteListToFile( List<string> list, string smallfile )
        {
            StreamWriter sw = new StreamWriter( File.Open( smallfile, FileMode.Create ) );
            foreach (string item in list)
            {
                sw.WriteLine( item );
            }
            sw.Dispose();
            list = null;
        }

        private void SortSmallFile( string filename, string sortedfilename, bool swap )
        {
            var list = new List<string>( 10000000 );

            using (StreamReader sr = new StreamReader( filename ))
            {
                int i = 0;
                string line;
                while ((line = sr.ReadLine()) != null)
                {
                    if (swap)
                        line = Swap( line );
                    list.Add( line );
                }
            }

            list.Sort( StringComparer.Ordinal );

            using (var bw = new StreamWriter( File.Open( sortedfilename, FileMode.Create ) ))
            {
                foreach (string item in list)
                {
                    bw.WriteLine( swap ? UnSwap( item ): item );
                }
            }
            if (File.Exists( filename ))
                File.Delete( filename );
            list = null;
            GC.Collect();
        }

        private string UnSwap( string item )
        {
            var arr = item.Split( '\t' );
            if (arr.Length < 2)
                return item;
            string num = arr[1].TrimStart( '0' );
            if (num == "")
                num = "0";
            return num + ". " + arr[0];
        }

        private string Swap( string line )
        {
            int pos = line.IndexOf( ". " );
            if (pos == -1)
                return line;
            string strnum = line.Substring( 0, pos );
            int num;
            int.TryParse( strnum, out num );
            string ret = line.Substring( pos + 2 ) + "\t" + num.ToString( "0000000000" );
            return ret;
        }
    }
}
