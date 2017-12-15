using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SortBigFile
{
    public partial class Form1 : Form
    {
        const int MinTextLength = 3;
        const int MaxTextLength = 18;

        public Form1()
        {
            InitializeComponent();
        }

        Random _random = new Random();

        private void button1_Click( object sender, EventArgs e )
        {
            string filename = txtFile.Text;
            int linecount = int.Parse( txtLineCount.Text );
            int lineblocksize = 10000;
            DateTime start = DateTime.Now;
            using (var bw = new StreamWriter( File.Open( filename, FileMode.Create ) ))
            {
                for (int i=0; i< linecount; i += lineblocksize)
                {
                    string block = GetRandomBlock(Math.Min( linecount - i , lineblocksize ) );
                    
                    bw.WriteLine( block );
                }
            }
            var timepass = DateTime.Now - start;
            lblStatus.Text = string.Format("{0} lines generated in {1}. ", linecount, timepass.ToString( @"hh\:mm\:ss" ) );
        }

        private string GetRandomBlock( int linecount )
        {
            int averagenumberlength = 10;
            int averagelinelength = (MaxTextLength + MinTextLength)/2 + averagenumberlength + 2;
            StringBuilder sb = new StringBuilder( averagelinelength*linecount );
            for (int i = 0; i < linecount; i++)
                sb.Append( GetRandomLine() + Environment.NewLine ) ;
            return sb.ToString();
        }

        private string GetRandomLine()
        {
            int rnd = _random.Next( 100000 );
            string rndstring = GetRandomString();
            return string.Format( "{0}. {1}", rnd, rndstring );
        }

        private string GetRandomString()
        {
            int rnd = _random.Next( MinTextLength, MaxTextLength );
            return CreatePassword( rnd );
        }

        string CreatePassword( int length )
        {
            const string valid = "abcdefghijklmnprstuvwxyzABCDEFGHIJKLMNPRSTUVWXYZ123456789 ,.:;";
            StringBuilder res = new StringBuilder();
            while (0 < length--)
            {
                res.Append( valid[_random.Next( valid.Length )] );
            }
            return res.ToString();
        }

    }
}
