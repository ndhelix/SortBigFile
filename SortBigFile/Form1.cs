using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SortBigFile
{
    public partial class Form1 : Form
    {
        const int MinTextLength = 3;
        const int MaxTextLength = 20;

        public Form1()
        {
            InitializeComponent();
        }

        static Random _random = new Random();

        private void button1_Click( object sender, EventArgs e )
        {
            _random.Next( 0, 100 );
            lblStatus.Text = "Generating...";
            Application.DoEvents();
            string filename = txtFile.Text;
            int linecount = int.Parse( txtLineCount.Text );
            int lineblocksize = 10000;
            DateTime start = DateTime.Now;
            object locker = new object();
            //Parallel.For( 0, linecount / lineblocksize + 1, index =>
            //    {
            //        string block = GetRandomBlock( lineblocksize  );

            //        new Thread( () =>
            //        {

            //            lock (locker)
            //            {
            //                File.AppendAllText( filename, block );
            //            }
            //        } ).Start();
            //        //
            //        //lock (o)
            //        //{
            //        //  bw.WriteLine( block );
            //        //}
            //    } );


            //var TaskList = new List<Task<string>>();
            //for (int i = 0; i < linecount; i += lineblocksize)
            //{
            //    int rowcount = Math.Min( linecount - i, lineblocksize );
            //    Task<string> task = Task.Run( async () => await GetRandomBlockAsync( rowcount ) );
            //    TaskList.Add( task );
            //}

            //Task.WhenAll( TaskList.ToArray() );

            //using (var bw = new StreamWriter( File.Open( filename, FileMode.Create ) ))
            //{
            //    foreach (var task in TaskList)
            //    {
            //        bw.WriteLine( task.Result );
            //    }
            //}

            using (var bw = new StreamWriter( File.Open( filename, FileMode.Create ) ))
            {
                for (int i = 0; i < linecount; i += lineblocksize)
                {
                    string block = GetRandomBlock( Math.Min( linecount - i, lineblocksize ) );
                    bw.Write( block );
                }
            }
            var timepass = DateTime.Now - start;
            lblStatus.Text = string.Format("{0} lines generated in {1}. ", linecount, timepass.ToString( @"hh\:mm\:ss" ) );
        }

        private async Task<string> GetRandomBlockAsync( int linecount )
        {
            return await Task.Run( () => GetRandomBlock( linecount ) );
        }

        private string GetRandomBlock( int linecount )
        {
            int averagenumberlength = 10;
            int averagelinelength = (MaxTextLength + MinTextLength)/2 + averagenumberlength + 2;
            StringBuilder sb = new StringBuilder( averagelinelength*linecount );
            for (int i = 0; i < linecount; i++)
                sb.Append( GetRandomLine() + Environment.NewLine ) ;
                //sb.Append( "10540 JBGKJHGIJKGLKHJIHH:HIJKU" + Environment.NewLine );
            string ret =  sb.ToString();
            return ret;
        }



        private string GetRandomLine()
        {
            //var random = new Random( new DateTime().Millisecond );
            int rnd = _random.Next( 100000 );
            string rndstring = GetRandomString();
            //string rndstring = "0123456123123123123";
            return string.Format( "{0}. {1}", rnd, rndstring );
        }

        private string GetRandomString()
        {
            int rnd = _random.Next( MinTextLength, MaxTextLength );
            return CreateRandomString( rnd );
        }

        string CreateRandomString( int length )
        {
            const string valid = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz123456789 ,.:;-'\"";
            StringBuilder res = new StringBuilder();
            res.Append( valid[_random.Next( 26 )] );
            while (1 < length--)
            {
                res.Append( valid[_random.Next( 26 )] );
            }
            return res.ToString();
        }

        private void btnSort_Click( object sender, EventArgs e )
        {
            lblStatus.Text = "Sorting...";
            Application.DoEvents();
            DateTime start = DateTime.Now;

            string filename = txtFile.Text;
            if (!File.Exists( filename ))
            {
                MessageBox.Show("File does not exist: " + filename );
                return;
            }

            Sorter sorter = new Sorter();
            long size = new System.IO.FileInfo( filename ).Length;
            string sortedfilename = Path.Combine( Path.GetDirectoryName( filename ),
                Path.GetFileNameWithoutExtension( filename ) + "_sorted" + Path.GetExtension( filename ) );
            sorter.SortFile( filename, sortedfilename );

            var timepass = DateTime.Now - start;
            lblStatus.Text = string.Format( "{0:0.0} GB file sorted in {1}. ", (decimal)size/1024/1024/1024, timepass.ToString( @"hh\:mm\:ss" ) );
        }
    }
}
