using JiebaNet.Segmenter.Common;
using System.Collections.Generic;
using System.Linq;

namespace JiebaNet.Analyser
{
    public class IdfLoader
    {
        internal IDictionary<string, double> IdfFreq { get; set; }
        internal double MedianIdf { get; set; }

        public IdfLoader()
        {
            IdfFreq = new Dictionary<string, double>();
            MedianIdf = 0.0;
            var lines = FileExtension.ReadEmbeddedAllLines(ConfigManager.IdfLoader);
            IdfFreq = new Dictionary<string, double>();
            foreach (var line in lines)
            {
                var parts = line.Trim().Split(' ');
                var word = parts[0];
                var freq = double.Parse(parts[1]);
                IdfFreq[word] = freq;
            }
            MedianIdf = IdfFreq.Values.OrderBy(v => v).ToList()[IdfFreq.Count / 2];
        }
    }
}