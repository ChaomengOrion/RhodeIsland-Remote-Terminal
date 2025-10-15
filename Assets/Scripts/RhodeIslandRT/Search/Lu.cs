// Created by ChaomengOrion
// Create at 2022-04-29 18:56:05
// Last modified on 2022-08-01 22:29:02

using Lucene.Net.Analysis;
//using Lucene.Net.Analysis.Cn.Smart;
//using Lucene.Net.Analysis.PanGu;
using Lucene.Net.Analysis.JieBa;
using Lucene.Net.Documents;
using Lucene.Net.Index;
using Lucene.Net.Search;
using Lucene.Net.Search.Highlight;
using Lucene.Net.Store;
using Lucene.Net.Util;
using System;
using System.Threading;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using Sirenix.OdinInspector;
using RhodeIsland.RemoteTerminal.UI;
using RhodeIsland.Arknights.AVG;
using Cysharp.Threading.Tasks;

namespace RhodeIsland.RemoteTerminal.Search
{
    public class Lu : MonoBehaviour
    {
        public JiebaNet.ConfigManager manager;
        protected void Awake()
        {
            JiebaNet.ConfigManager.Init(manager);
        }

        [SerializeField]
        private string aa;

        [Button("s")]
        private void Search()
        {
            DateTime time = DateTime.Now;
            Search(aa);
            Debug.Log((DateTime.Now - time).TotalMilliseconds + "ms");
        }

        [Button("Write")]
        private void Write()
        {
            StartCoroutine(_Write());
        }

        [Button("WriteTest")]
        private void Test()
        {
            _WriteTest().Forget();
        }

        [Button("SeachTest")]
        private void SeachTestTest(string s)
        {
            _SearchTest(s).Forget();
        }

        private async UniTaskVoid _WriteTest()
        {
            DateTime time = DateTime.Now;
            await IndexManager.instance.CreateStoryIndexAsync(StorysManager.instance.Chapters.Values);
            await UniTask.SwitchToMainThread();
            Debug.Log((DateTime.Now - time).TotalMilliseconds + "ms");
        }

        private async UniTaskVoid _SearchTest(string q)
        {
            await UniTask.SwitchToThreadPool();
            IndexReader reader = DirectoryReader.Open(FSDirectory.Open("D:/Index_Data"));

            DateTime time = DateTime.Now;
            IndexSearcher searcher = new(reader);
            var keyWordQuery = new PhraseQuery();
            foreach (var item in GetKeyWords(q))
            {
                keyWordQuery.Add(new Term("content", item));
            }
            keyWordQuery.Slop = 5;
            var hits = searcher.Search(keyWordQuery, 1000).ScoreDocs;
            await DLog.LogAsync((DateTime.Now - time).TotalMilliseconds + "ms");
            SimpleFragmenter fragmenter = new(100);
            QueryScorer scorer = new(keyWordQuery);
            SimpleHTMLFormatter formatter = new("<color=#66CCFF>", "</color>");
            Highlighter highlighter = new(formatter, scorer);
            highlighter.TextFragmenter = fragmenter;
            Analyzer analyzer = new JieBaAnalyzer(JiebaNet.Segmenter.TokenizerMode.Default);
            await UniTask.SwitchToMainThread();
            foreach (var hit in hits)
            {
                var document = searcher.Doc(hit.Doc);
                //Debug.Log(string.Format("<color=#CCCC00>path: {0}</color>", document.Get("path")));
                if (StorysManager.instance.TryGetStoryText(document.Get("chapter"), document.Get("path"), out string content))
                {
                    var sss = highlighter.GetBestFragment(analyzer, "content", content);
                    Debug.Log(string.Format("<color=#CCCC00>content:</color> {0}", sss));
                }
            }
        }

        private IEnumerator _Write()
        {
            DateTime time = DateTime.Now;
            HashSet<StoryGroupInfo> stories = new();
            foreach (var items in TableManager.instance.StoryReviewGroupDatas.Values)
            {
                Dictionary<string, string> groups = new();
                foreach (var data in items.infoUnlockDatas)
                {
                    groups.Add(data.storyTxt, AVGController.instance.assetLoader.Load<TextAsset>(ResourceRouter.GetStoryPath(data.storyTxt)).text);
                }
                stories.Add(new() { Groups = groups, Type = StoryGroupType.Other, Name = items.name });
            }
            yield return IndexManager.instance.CreateIndex(stories);
            Debug.Log((DateTime.Now - time).TotalMilliseconds + "ms");
        }

        public static List<string> GetKeyWords(string q)
        {
            List<string> keyworkds = new();
            Analyzer analyzer = new JieBaAnalyzer(JiebaNet.Segmenter.TokenizerMode.Default);
            using (var ts = analyzer.GetTokenStream(null, q))
            {
                ts.Reset();
                var ct = ts.GetAttribute<Lucene.Net.Analysis.TokenAttributes.ICharTermAttribute>();

                while (ts.IncrementToken())
                {
                    StringBuilder keyword = new();
                    for (int i = 0; i < ct.Length; i++)
                    {
                        keyword.Append(ct.Buffer[i]);
                    }
                    string item = keyword.ToString();
                    if (!keyworkds.Contains(item))
                    {
                        keyworkds.Add(item);
                    }
                }
            }
            return keyworkds;
        }

        public static void Search(string q)
        {
            IndexReader reader = DirectoryReader.Open(FSDirectory.Open("D:/Index_Data"));
            SearchTaskScheduler taskScheduler = new();
            IndexSearcher searcher = new(reader, taskScheduler);
            Analyzer analyzer = new JieBaAnalyzer(JiebaNet.Segmenter.TokenizerMode.Default);

            var keyWordQuery = new PhraseQuery();
            Debug.Log("<color=#CCCCFF>" + q + "</color>");
            foreach (var item in GetKeyWords(q))
            {
                Debug.Log("<color=#CCCCFF>拆分：" + item + "</color>");
                keyWordQuery.Add(new Term("content", item));
            }
            keyWordQuery.Slop = 5;
            var hits = searcher.Search(keyWordQuery, 1000).ScoreDocs;
            /*SimpleFragmenter fragmenter = new(100);
            QueryScorer scorer = new(keyWordQuery);
            SimpleHTMLFormatter formatter = new("<color=#66CCFF>", "</color>");
            
            Highlighter highlighter = new(formatter, scorer);
            highlighter.TextFragmenter = fragmenter;*/
            Debug.Log($"finded {hits.Length}.");
            /*foreach (var hit in hits)
            {
                var document = searcher.Doc(hit.Doc);
                Debug.Log(string.Format("<color=#CCCC00>id: {0}</color>", document.Get("url")));
                //var sss = highlighter.GetBestFragment(TokenSources.GetAnyTokenStream(reader, hit.Doc, "content", analyzer), document.Get("content"));
                Debug.Log(string.Format("<color=#CCCC00>content:</color> {0}", document.Get("content")));
            }*/
        }
    }
}