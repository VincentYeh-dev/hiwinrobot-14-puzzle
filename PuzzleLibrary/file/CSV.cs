using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PuzzleLibrary.file
{
    public class CSV
    {
        private CSV() { }

        public static List<List<string>> Read(string filename, char separator=',')
        {
            var reader = new StreamReader(File.OpenRead(filename));
            var rows = new List<List<string>>();
            while (!reader.EndOfStream)
            {
                var line = reader.ReadLine();
                var values = line.Split(separator);
                var columns= new List<string>();
                foreach(var value in values)
                {
                    columns.Add(value);
                }
                rows.Add(columns);
            }
            reader.Close();
            return rows;
        }

        public static void Write(string filename,List<List<string>> rows, char separator=',')
        {
            var writer = new StreamWriter(File.OpenWrite(filename));
            
            foreach (var row in rows)
            {
                var columns = row;
                writer.Write(columns[0]);
                for(int i = 1; i < columns.Count; i++)
                {
                    writer.Write(separator);
                    writer.Write(columns[i]);
                }
            }
            writer.Close();
        }
        
    }
}
