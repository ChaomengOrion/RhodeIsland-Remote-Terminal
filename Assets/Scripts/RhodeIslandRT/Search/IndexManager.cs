// Created by ChaomengOrion
// Create at 2022-05-14 17:01:05
// Last modified on 2022-08-01 22:25:50

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Lucene.Net.Documents;
using Lucene.Net.Index;
using Lucene.Net.Store;
using Lucene.Net.Util;
using Lucene.Net.Analysis;
using Lucene.Net.Analysis.JieBa;
using JiebaNet.Segmenter;
using RhodeIsland.RemoteTerminal.AVG;
using RhodeIsland.Arknights.AVG;
using Cysharp.Threading.Tasks;

namespace RhodeIsland.RemoteTerminal.Search
{
    public class IndexManager : Singleton<IndexManager>
    {
        public IndexWriter IndexWriter => _GetIndexWriter();

        public string IndexPath { get; set; } = "D:/Index_Data";

        private IndexWriter m_indexWriter = null;
        private AVGParser m_parser = new();

        private IndexWriter _GetIndexWriter()
        {
            if (m_indexWriter == null || m_indexWriter.IsClosed)
            {
                Directory dir = FSDirectory.Open(IndexPath);
                if (IndexWriter.IsLocked(dir))
                {
                    IndexWriter.Unlock(dir);
                }
                Analyzer analyzer = new JieBaAnalyzer(TokenizerMode.Search);
                IndexWriterConfig indexConfig = new(LuceneVersion.LUCENE_48, analyzer);
                indexConfig.RAMBufferSizeMB = 64;
                m_indexWriter = new(dir, indexConfig);
            }
            return m_indexWriter;
        }

        public async UniTask CreateStoryIndexAsync(IEnumerable<Chapter> chapters)
        {
            await UniTask.SwitchToThreadPool();
            HashSet<Chapter> charStorys = new();
            HashSet<IEnumerable<IIndexableField>> docs = new();
            HashSet<UniTask> tasks = new();
            int taskCount = 0;
            object docsLock = new();
            foreach (Chapter chapter in chapters)
            {
                if (chapter.type != StoryReviewType.CHARACTER_STORY)
                {
                    taskCount++;
                    UniTaskCompletionSource source = new();
                    tasks.Add(source.Task);
                    UniTask.RunOnThreadPool(() => _ParseChapterToDocument(chapter, docs, docsLock, source)).Forget();
                }
                else
                {
                    charStorys.Add(chapter);
                    if (charStorys.Count >= 15)
                    {
                        charStorys.Add(chapter);
                        taskCount++;
                        UniTaskCompletionSource source = new();
                        tasks.Add(source.Task);
                        UniTask.RunOnThreadPool(() => _ParseChapterToDocument(charStorys, docs, docsLock, source)).Forget();
                        charStorys.Clear();
                    }
                }
            }
            if (charStorys.Count > 0)
            {
                taskCount++;
                UniTaskCompletionSource source = new();
                tasks.Add(source.Task);
                UniTask.RunOnThreadPool(() => _ParseChapterToDocument(charStorys, docs, docsLock, source)).Forget();
                charStorys.Clear();
            }
            await UniTask.SwitchToMainThread();
            DLog.Log("Have added tasks.");
            await UniTask.SwitchToThreadPool();
            IndexWriter writer = IndexWriter;
            while (taskCount > 0)
            {
                await UniTask.WhenAny(tasks);
                taskCount--;
                lock (docsLock)
                {
                    foreach (IEnumerable<IIndexableField> doc in docs)
                    {
                        writer.AddDocument(doc);
                    }
                    docs.Clear();
                }
            }
            await UniTask.SwitchToMainThread();
            DLog.Log("Have added all docs.");
            await UniTask.SwitchToThreadPool();
            writer.Commit();
            writer.Dispose();
        }

        private void _ParseChapterToDocument(HashSet<Chapter> chapterInfos, HashSet<IEnumerable<IIndexableField>> docs, object docsLock, UniTaskCompletionSource source)
        {
            HashSet<Document> m_docs = new();
            foreach (Chapter chapter in chapterInfos)
            {
                for (int i = 0; i < chapter.storyPaths.Length; i++)
                {
                    if (m_parser.TryParse(chapter.storyDatas[i], out Story story))
                    {
                        m_docs.Add(Utils.WriteStoryDocument(chapter.storyPaths[i], story.title, chapter.id, chapter.type, StoryUtil.ParseStoryToTxt(story, out _, true)));
                    }
                }
            }
            lock (docsLock)
            {
                foreach (Document item in m_docs)
                {
                    docs.Add(item);
                }
            }
            source.TrySetResult();
        }

        private void _ParseChapterToDocument(Chapter chapterInfo, HashSet<IEnumerable<IIndexableField>> docs, object docsLock, UniTaskCompletionSource source)
        {
            HashSet<Document> m_docs = new();
            for (int i = 0; i < chapterInfo.storyPaths.Length; i++)
            {
                if (m_parser.TryParse(chapterInfo.storyDatas[i], out Story story))
                {
                    m_docs.Add(Utils.WriteStoryDocument(chapterInfo.storyPaths[i], story.title, chapterInfo.id, chapterInfo.type, StoryUtil.ParseStoryToTxt(story, out _, true)));
                }
            }
            lock (docsLock)
            {
                foreach (Document item in m_docs)
                {
                    docs.Add(item);
                }
            }
            source.TrySetResult();
        }

        [System.Obsolete]
        public IEnumerator CreateIndex(HashSet<StoryGroupInfo> stories)
        {
            BuildIndexTaskManager taskManager = new();
            foreach (StoryGroupInfo storyInfo in stories)
            {
                taskManager.Add(_WriteStoryToIndex, storyInfo);
            }
            _GetIndexWriter();
            bool end = false;
            taskManager.RunAll(suc => end = true);
            yield return new WaitUntil(() => end);
            IndexWriter writer = _GetIndexWriter();
            writer.Flush(triggerMerge: false, applyAllDeletes: true);
            //writer.Commit();
            writer.Dispose();
            Debug.Log("finished");
        }

        [System.Obsolete]
        private bool _WriteStoryToIndex(StoryGroupInfo storyInfo)
        {
            bool error = false;
            IndexWriter writer = _GetIndexWriter();
            foreach (KeyValuePair<string, string> info in storyInfo.Groups)
            {
                if (m_parser.TryParse(info.Value, out Story story))
                {
                    //writer.AddDocument(Utils.WriteStoryDocument(info.Key, story.title, storyInfo.Name, storyInfo.Type, Utils.GetStoryContent(story)));
                }
                else
                {
                    error = true;
                }
            }
            return error;
        }
    }
}