// Created by ChaomengOrion
// Create at 2022-05-15 13:39:13
// Last modified on 2022-08-01 19:15:10

using System;
using System.Threading;
using System.Collections.Generic;
using System.Collections.Concurrent;
using UnityEngine;

namespace RhodeIsland.RemoteTerminal.Search
{
    [Obsolete]
    public class BuildIndexTaskManager
    {
        private List<Func<StoryGroupInfo, bool>> m_tasks = new();
        private List<StoryGroupInfo> m_args = new();
        private int m_threadCount;
        private object m_lock = new();
        private int m_unFinishCount = 0;
        private bool hasError = false;

        private struct TaskInfo
        {
            public List<Func<StoryGroupInfo, bool>> tasks;
            public List<StoryGroupInfo> args;
        }

        public BuildIndexTaskManager(int threadCount = -1)
        {
            if (threadCount <= 0)
            {
                m_threadCount = Environment.ProcessorCount;
            }
            else
            {
                m_threadCount = threadCount;
            }
        }

        public void Add(Func<StoryGroupInfo, bool> task, StoryGroupInfo arg)
        {
            m_tasks.Add(task);
            m_args.Add(arg);
        }

        public void RunAll(Action<bool> onEnd)
        {
            hasError = false;
            int index = 0, lenght = m_tasks.Count, each = lenght / m_threadCount;
            m_unFinishCount = m_threadCount;
            for (int i = 0; i < m_threadCount; i++)
            {
                Thread thread = new(_Run);
                thread.IsBackground = true;
                int count = i + 1 != m_threadCount ? each : lenght - index;
                thread.Start(new TaskInfo { tasks = m_tasks.GetRange(index, count), args = m_args.GetRange(index, count) });
            }
            Thread wait = new(() =>
            {
                while (m_unFinishCount > 0);
                onEnd.Invoke(hasError);
            });
            wait.IsBackground = true;
            wait.Start();
        }

        private void _Run(object tasks)
        {
            TaskInfo taskInfo = (TaskInfo)tasks;
            for (int i = 0; i < taskInfo.tasks.Count; i++)
            {
                if (!taskInfo.tasks[i].Invoke(taskInfo.args[i]))
                {
                    hasError = true;
                }
            }
            lock (m_lock)
            {
                m_unFinishCount--;
            }
        }
    }
}