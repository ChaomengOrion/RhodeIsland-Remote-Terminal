// Created by ChaomengOrion
// Create at 2022-07-21 20:24:16
// Last modified on 2022-08-01 19:12:37

using System;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;

namespace RhodeIsland.RemoteTerminal.TaskSystem
{
    public static class StoryParseTaskManager
    {
        private static readonly Dictionary<string, UniTask<Chapter>> m_normalPiorityParseTasksDict = new();
        private static readonly HashSet<UniTask<Chapter>> m_normalPiorityParseTasks = new();
        private static readonly HashSet<UniTask<Chapter>> m_highPiorityParseTasks = new();
        private static readonly HashSet<string> m_requestedParseId = new();

        public static int m_maxActiveTaskCount = 11;

        public static async UniTask<Chapter> RunParseTasksLazy(string id, Func<Chapter> func)
        {
            await UniTask.WaitUntil(() => _CheckIfAddTask(id));
            UniTask<Chapter> task = UniTask.RunOnThreadPool(func);
            m_normalPiorityParseTasksDict.Add(id, task);
            m_normalPiorityParseTasks.Add(task);
            Chapter chapter = await task;
            m_normalPiorityParseTasksDict.Remove(id);
            m_normalPiorityParseTasks.Remove(task);
            return chapter;
        }

        public static void RequestParseTask(string id)
        {
            if (m_normalPiorityParseTasksDict.TryGetValue(id, out UniTask<Chapter> task))
            {
                m_normalPiorityParseTasksDict.Remove(id);
                if (!m_normalPiorityParseTasks.Contains(task))
                    m_requestedParseId.Add(id);
            }
        }

        public static async UniTask<Chapter> RunParseTasksAsync(Func<Chapter> func)
        {
            UniTask<Chapter> task = UniTask.RunOnThreadPool(func);
            m_highPiorityParseTasks.Add(task);
            Chapter chapter = await task;
            m_highPiorityParseTasks.Remove(task);
            return chapter;
        }

        private static bool _CheckIfAddTask(string id)
        {
            return m_normalPiorityParseTasks.Count + m_highPiorityParseTasks.Count < m_maxActiveTaskCount || m_requestedParseId.Contains(id);
        }
    }
}
