// Created by ChaomengOrion
// Create at 2022-07-21 22:19:44
// Last modified on 2022-08-01 22:19:21

using System.Collections.Generic;
using UnityEngine;
using RhodeIsland.RemoteTerminal.AVG;
using RhodeIsland.Arknights.AVG;
using RhodeIsland.Arknights.Resource;
using RhodeIsland.RemoteTerminal.TaskSystem;
using Cysharp.Threading.Tasks;

namespace RhodeIsland.RemoteTerminal
{
    public struct Chapter
    {
        //Base Data
        public string id;
        public StoryReviewType type;
        public string[] storyPaths;
        public string[] storyDatas;
        //Infomation
        public int fontCount;
        public int cgCount;
        public string lastReadId;
        public StoryInfo[] storyInfos;
        //State
        public bool isDone;
    }

    public struct StoryInfo
    {
        public string[] mainCharacters;
        public int fontCount;
        public float predictReadTime;
    }

    public class StorysManager : PersistentSingleton<StorysManager>
    {
        public Dictionary<string, Chapter> Chapters => m_chapters;

        private AVGParser m_parser = new();
        [Sirenix.OdinInspector.ShowInInspector]
        private Dictionary<string, Chapter> m_chapters = new();

        #region RefenceMethod
        protected override void OnInit()
        {
            base.OnInit();
            Init().Forget();
        }

        protected async UniTaskVoid Init()
        {
            await UniTask.Yield();
            await CreateChapters();
            string[] keys = new string[m_chapters.Count];
            m_chapters.Keys.CopyTo(keys, 0);
            foreach (string key in keys)
                m_chapters[key] = _LoadChapterDatas(m_chapters[key]);
            UniTask<Chapter>[] tasks = new UniTask<Chapter>[m_chapters.Count];
            for (int i = 0; i < keys.Length; i++)
            {
                string key = keys[i];
                tasks[i] = StoryParseTaskManager.RunParseTasksLazy(key, () =>
                {
                    try
                    {
                        Chapter chapter = _ParseChapterDatas(m_chapters[key]);
                        chapter.isDone = true;
                        m_chapters[key] = chapter;
                        return chapter;
                    }
                    catch
                    {
                        return m_chapters[key];
                    }
                });
            }
            await UniTask.WhenAll(tasks);
            Debug.Log("DONE");
        }
        #endregion

        public bool TryGetStoryText(string chapterId, string path, out string content)
        {
            for (int i = 0; i < m_chapters[chapterId].storyPaths.Length; i++)
            {
                if (m_chapters[chapterId].storyPaths[i] == path)
                {
                    content = m_chapters[chapterId].storyDatas[i];
                    return true;
                }
            }
            content = null;
            return false;
        }

        public void SetParserVariableConfig(AVGVariableConfig config)
        {
            m_parser.variableConverter = config;
        }

        public bool TryParseStory(string path, out Story story)
        {
            if (ResourceManager.TryLoadAsset(path, out TextAsset textAsset) && m_parser.TryParse(textAsset.text, out story))
                return true;
            story = null;
            return false;
        }

        public bool IsChapterDone(string id)
        {
            return m_chapters.ContainsKey(id) && m_chapters[id].isDone;
        }

        public async UniTask<Chapter> LoadChapterAsync(string id)
        {
            if (!m_chapters.ContainsKey(id))
            {
                await UniTask.WaitUntil(() => m_chapters.ContainsKey(id));
            }
            if (m_chapters[id].isDone)
            {
                return m_chapters[id];
            }
            else
            {
                StoryParseTaskManager.RequestParseTask(id);
                await UniTask.WaitUntil(() => m_chapters[id].isDone);
                return m_chapters[id];
            }
        }

        public async UniTask CreateChapters()
        {
            await UniTask.RunOnThreadPool(_CreateChapters);
        }

        private void _CreateChapters()
        {
            foreach (KeyValuePair<string, StoryReviewGroupClientData> pair in TableManager.instance.StoryReviewGroupDatas)
            {
                m_chapters.Add(pair.Key, _CreateChapter(pair.Value));
            }
        }

        private Chapter _CreateChapter(StoryReviewGroupClientData data)
        {
            Chapter chapter = new();
            chapter.id = data.id;
            chapter.type = data.actType;
            chapter.storyPaths = new string[data.infoUnlockDatas.Count];
            for (int i = 0; i < data.infoUnlockDatas.Count; i++)
            {
                chapter.storyPaths[i] = data.infoUnlockDatas[i].storyTxt;
            }
            chapter.isDone = false;
            return chapter;
        }

        private Chapter _LoadChapterDatas(Chapter chapter)
        {
            chapter.storyDatas = new string[chapter.storyPaths.Length];
            for (int i = 0; i < chapter.storyPaths.Length; i++)
            {
                chapter.storyDatas[i] = ResourceManager.Load<TextAsset>(ResourceRouter.GetStoryPath(chapter.storyPaths[i])).text;
            }
            return chapter;
        }

        private async UniTask<Chapter> _LoadChapterDatasAsync(Chapter chapter)
        {
            chapter.storyDatas = new string[chapter.storyPaths.Length];
            for (int i = 0; i < chapter.storyPaths.Length; i++)
            {
                AsyncResource res = ResourceManager.LoadAsync<TextAsset>(ResourceRouter.GetStoryPath(chapter.storyPaths[i]));
                await res;
                chapter.storyDatas[i] = res.GetAsset<TextAsset>().text;
            }
            return chapter;
        }

        private Chapter _ParseChapterDatas(Chapter chapter)
        {
            chapter.storyInfos = new StoryInfo[chapter.storyDatas.Length];
            int count = 0;
            HashSet<Story> stories = new();
            for (int i = 0; i < chapter.storyDatas.Length; i++)
            {
                if (m_parser.TryParse(chapter.storyDatas[i], out Story story))
                {
                    stories.Add(story);
                    chapter.storyInfos[i] = _GetStoryInfo(story);
                    count += chapter.storyInfos[i].fontCount;
                }
            }
            chapter.fontCount = count;
            chapter.cgCount = StoryUtil.GetCGCount(stories);
            return chapter;
        }

        private StoryInfo _GetStoryInfo(Story story)
        {
            StoryInfo info = new();
            (info.fontCount, info.predictReadTime) = StoryUtil.CalculateFontCountAndReadTime(story, StoryUtil.GetCGCount(story));
            info.mainCharacters = StoryUtil.GetMainCharacters(story);
            return info;
        }
    }
}