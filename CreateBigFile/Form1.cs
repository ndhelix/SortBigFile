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
        int _minTextLength = 0;
        int _maxTextLength = 0;
        int _maxInt = 0;
        public Form1()
        {
            InitializeComponent();
        }

        FastRandom _random = new FastRandom();

        private void button1_Click( object sender, EventArgs e )
        {
            _random.Next( 0, 100 );
            lblStatus.Text = "Generating...";
            Application.DoEvents();
            string filename = txtFile.Text;
            long filesize = long.Parse( txtLineCount.Text )*1024*1024;

            _maxInt = int.Parse( txtMaxNum.Text );
            _minTextLength = int.Parse( txtMinLen.Text );
            _maxTextLength = int.Parse( txtMaxLen.Text );
            
            int lineblocksize = 10000;
            DateTime start = DateTime.Now;
            long size = 0;

            using (var bw = new StreamWriter( File.Open( filename, FileMode.Create ) ))
            {
                while (size < filesize)
                {
                    string block = GetRandomBlock( lineblocksize );
                    bw.Write( block );
                    size += block.Length;
                }
            }
            var timepass = DateTime.Now - start;
            lblStatus.Text = string.Format("{0} Mb generated in {1}. ", txtLineCount.Text, timepass.ToString( @"hh\:mm\:ss" ) );
        }

        
        private string GetRandomBlock( int linecount )
        {
            int averagenumberlength = 10;
            int averagelinelength = (_maxTextLength + _minTextLength)/2 + averagenumberlength + 2;
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
            int rnd = _random.Next( _maxInt );
            string rndstring = GetRandomString();
            //string rndstring = "0123456123123123123";
            return string.Format( "{0}. {1}", rnd, rndstring );
        }

        private string GetRandomString()
        {
            int rnd = _random.Next( _minTextLength, _maxTextLength );
            return CreateRandomString( rnd );
        }

        string duplicate;
        int duplicatecandidatepercent = 10;
        int useduplicatepercent = 10;

        string CreateRandomString( int length )
        {
            int prob = _random.Next( 100 );
            if (prob <= useduplicatepercent)
                if (duplicate != null)
                    return duplicate;

            //if (prob==20)
              //  return "KKKKKKKKKKKK";

            const string valid = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz 123456789,.:;-'\"";
            StringBuilder res = new StringBuilder();

            int firstlettercount = 16;
            byte[] bytearray = null;
            if (length >= firstlettercount)
            {
                 bytearray = new byte[length - firstlettercount];
                _random.NextBytes( bytearray );
            }

            for (int i =0; i < length; i++)
            {
                if (i< firstlettercount)
                    res.Append( valid[_random.Next( 26 )] ); // пусть сначала идут буквы, так удобнее
                else
                    res.Append( valid[bytearray[i- firstlettercount] % valid.Length] );
            }

            prob = _random.Next( 100 );
            if (prob <= duplicatecandidatepercent)
                duplicate = res.ToString();

            return res.ToString();
        }

        private void txtLineCount_KeyPress( object sender, KeyPressEventArgs e )
        {
            e.Handled = !char.IsDigit( e.KeyChar ) && !char.IsControl( e.KeyChar );
        }
    }
}
