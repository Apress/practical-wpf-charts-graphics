using System;
using System.Collections.Generic;
using System.Windows;
using System.IO;
using System.Data;
using Microsoft.Win32;

namespace StockCharts
{
    public class TextFileReader
    {
        public string[,] LoadFile()
        {
            OpenFileDialog ofd = new OpenFileDialog();
            if (ofd.ShowDialog() == true)
            {
                string filePath = ofd.FileName;
                List<string> sc = new List<string>();
                FileStream fs = new FileStream(filePath, FileMode.Open, FileAccess.ReadWrite);
                StreamReader sr = new StreamReader(fs);

                // Read file into a string collection:
                int noBytesRead = 0;
                string oneLine;
                while ((oneLine = sr.ReadLine()) != null)
                {
                    noBytesRead += oneLine.Length;
                    sc.Add(oneLine);
                }
                sr.Close();
                
                // Extract the stock data from the file: 
                string[] sa = new string[sc.Count];
                sc.CopyTo(sa, 0);
                char[] splitter = { ' ', ',', ':', '\t' };
                string[] sa1 = sa[0].Split(splitter);
                string[,] result = new string[sa1.Length, sc.Count];

                for (int i = 0; i < sc.Count; i++)
                {
                    sa1 = sa[i].Split(splitter);
                    for (int j = 0; j < sa1.Length; j++)
                        result[j, i] = sa1[j];
                }

                return result;
            }
            else
                return null;
        }
    }
}
