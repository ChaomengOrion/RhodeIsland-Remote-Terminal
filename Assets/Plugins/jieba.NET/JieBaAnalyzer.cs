using System;
using Lucene.Net.Analysis;
using Lucene.Net.Analysis.Core;
using Lucene.Net.Analysis.TokenAttributes;
using Lucene.Net.Analysis.Util;
using System.IO;
using JiebaNet.Segmenter;


namespace Lucene.Net.Analysis.JieBa
{
    public class JieBaAnalyzer : Analyzer
    {
        public TokenizerMode mode;
        public JieBaAnalyzer(TokenizerMode Mode) : base()
        {
            this.mode = Mode;
        }

        protected internal override TokenStreamComponents CreateComponents(string filedName,TextReader reader)
        {
            var tokenizer = new JieBaTokenizer(reader,mode);

            var tokenstream = (TokenStream)new LowerCaseFilter(Lucene.Net.Util.LuceneVersion.LUCENE_48, tokenizer);

            tokenstream.AddAttribute<ICharTermAttribute>();
            tokenstream.AddAttribute<IOffsetAttribute>();

            return new TokenStreamComponents(tokenizer, tokenstream);
        }
    }
}
