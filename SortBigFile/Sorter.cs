﻿using System;
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
        static int _sortSizeLimit = 40000000;//10 * 1024 * 1024;
        static int _smallfilesize = 10 * 1024 * 1024;
        static int _mergepool = 10;
        //static int _linelimit = _sortSizeMbLimit * 10000;
        //10000000
        internal void SortFile( string filename, string sortedfilename )
        {
            long sizeMb = new System.IO.FileInfo( filename ).Length;

            if (sizeMb < _sortSizeLimit)
            {
                SortSmallFile( filename, sortedfilename, true );
                return;
            }

            List<string> smallfilelist = CreateSmallFiles( filename );
            List<string> smallfilelistsorted = SortSmallFiles( smallfilelist );
            MergeFiles( smallfilelistsorted, sortedfilename+"merged", 0 );
        }

        private void MergeFiles( List<string> filelist, string outputfilename, int recursionlevel )
        {
            if (filelist.Count <= _mergepool)
            {
                MergePlain( filelist, outputfilename, recursionlevel );
            }
        }

        private  void MergePlain( List<string> filelist, string outputfilename, int recursionlevel )
        {
            int buffersize = 10000;
            StreamWriter writer = new StreamWriter( File.Open( outputfilename, FileMode.Create ) );
            StreamReader[] srarr = new StreamReader[filelist.Count];
            //int[] pointers = new int[filelist.Count];
            //string[][] arr = new string[filelist.Count][];
            Queue<string>[] q = new Queue<string>[filelist.Count];
            var sd = new SortedList<string, int>();

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

            bool queuesareempty = true;
            for (int i = 0; i < q.Length; i++)
                if (q[i].Count > 0)
                {
                    queuesareempty = false;
                    sd[q[i].Dequeue()] = i;
                }
            bool unswap = recursionlevel == 0;
            while (!queuesareempty)
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
                            sd[q[i].Dequeue()] = i;
                }

                if (!sd.Any())
                    break;

                int filenum = sd.First().Value;
                writer.WriteLine( unswap ? UnSwap(sd.First().Key) : sd.First().Key );
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
                    sd[q[filenum].Dequeue()] = filenum;


                //queuesareempty = true;
                //for (int i = 0; i < q.Length; i++)
                //    if (q[i].Count > 0)
                //    {
                //        queuesareempty = false;
                //        break;
                //    }
            }

            for (int i = 0; i < filelist.Count; i++)
                srarr[i].Dispose();
            writer.Dispose();
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
            //var TaskList = new List<Task>();
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

                         WriteListToFile( list, smallfile );
                        //Task task = Task.Run( () => WriteListToFile( list, smallfile ) );
                        //TaskList.Add( task );

                        filecounter++;
                        files.Add( smallfile );
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
                    files.Add( smallfile );
                }
                list = null;
            }

            //Task.WaitAll( TaskList.ToArray() );

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
                        line = Swap( line );
                    sdict.Add( line );
                }
            }

            sdict.Sort( StringComparer.Ordinal );

            using (var bw = new StreamWriter( File.Open( sortedfilename, FileMode.Create ) ))
            {
                foreach (string item in sdict)
                {
                    bw.WriteLine( swap ? UnSwap( item ): item );
                }
            }

            sdict = null;
            GC.Collect();
        }

        private string UnSwap( string item )
        {
            var arr = item.Split( '\t' );
            if (arr.Length < 1)
                return item;
            return arr[1].TrimStart( '0' ) + ". " + arr[0];
        }

        private string Swap( string line )
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
