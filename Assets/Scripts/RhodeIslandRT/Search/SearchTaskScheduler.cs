// Created by ChaomengOrion
// Create at 2022-05-14 11:23:20
// Last modified on 2022-05-14 17:42:21

using System;
using System.Threading;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Collections.Concurrent;
using UnityEngine;

namespace RhodeIsland.RemoteTerminal.Search
{
    public class SearchTaskScheduler : TaskScheduler, IDisposable
    {
        public static new TaskScheduler Current { get; } = new SearchTaskScheduler();
        public static new TaskScheduler Default { get; } = Current;

        private readonly BlockingCollection<Task> m_queue = new();

        private Thread m_thread;

        public SearchTaskScheduler()
        {
            m_thread = new(Run);
            m_thread.IsBackground = true;
            m_thread.Start();
        }

        private void Run()
        {
            while (m_queue.TryTake(out Task t, Timeout.Infinite))
            {
                Thread thread = new(_RunTask);
                thread.IsBackground = true;
                thread.Start(t);
            }
        }

        private void _RunTask(object task)
        {
            DateTime time = DateTime.Now;
            Task t = (Task)task;
            TryExecuteTask(t);
            Debug.Log($"ThreadID: {Thread.CurrentThread.ManagedThreadId} done in {(DateTime.Now - time).TotalMilliseconds} ms with status {t.IsCompletedSuccessfully}");
        }

        protected override IEnumerable<Task> GetScheduledTasks()
        {
            return m_queue;
        }

        protected override void QueueTask(Task task)
        {
            m_queue.Add(task);
        }

        protected override bool TryExecuteTaskInline(Task task, bool taskWasPreviouslyQueued)
        {
            return false;
        }

        public void Dispose()
        {
            m_thread.Abort();
            GC.SuppressFinalize(this);
        }
    }
}