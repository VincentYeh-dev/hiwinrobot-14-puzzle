using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ExclusiveProgram
{
    public class GlobalUtils
    {
        public static string FILTER_IMAGE = "Image file|*.jpg;*.bmp|Any file|*.*";
        public static string FILTER_CSV = "CSV file|*.csv";

        private GlobalUtils()
        {

        }

        public static string[] SelectFiles(string Filter)
        {
            var openFileDialog1 = new OpenFileDialog();
            openFileDialog1.InitialDirectory = Environment.CurrentDirectory;
            openFileDialog1.Filter = Filter;
            openFileDialog1.FilterIndex = 0;
            openFileDialog1.RestoreDirectory = true;
            openFileDialog1.Multiselect = true;

            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                return openFileDialog1.FileNames;
            }
            else
            {
                return new string[0];
            }
        }
        public static string SelectFile(string Filter)
        {
            var openFileDialog1 = new OpenFileDialog();
            openFileDialog1.InitialDirectory = Environment.CurrentDirectory;
            openFileDialog1.Filter = Filter;
            openFileDialog1.FilterIndex = 0;
            openFileDialog1.RestoreDirectory = true;

            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                return openFileDialog1.FileName;
            }
            else
            {
                return "";
            }
        }
    }
}
