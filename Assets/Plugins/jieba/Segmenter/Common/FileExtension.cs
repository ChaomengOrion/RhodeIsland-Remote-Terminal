using System;
using System.Collections.Generic;
using System.IO;

namespace JiebaNet.Segmenter.Common
{
    public static class FileExtension
    {
        public static List<string> ReadEmbeddedAllLines(string str)
        {
            List<string> list = new();
            using (StringReader sr = new(str))
            {
                string item;
                while ((item = sr.ReadLine()) != null)
                {
                    list.Add(item);
                }
            }
            return list;
        }
    }
}
