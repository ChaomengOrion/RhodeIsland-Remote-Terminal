// Created by ChaomengOrion
// Create at 2022-04-29 18:56:05
// Last modified on 2022-08-01 19:07:50

using System;
using System.Collections.Generic;
//using XLua;

namespace RhodeIsland.Arknights.AVG
{
	public abstract class ExecutorComponent : AVGComponent
	{
		/// <summary>
		/// 是否在执行
		/// </summary>
		public bool isExecuting { get; private set; }

		/// <summary>
		/// 抽象方法: 获得该组件所有命令执行器委托
		/// </summary>
		/// <returns></returns>
		public abstract Dictionary<string, Executor> GetExecutors();

		public virtual Dictionary<string, Gather> GetGathers()
		{
			return new();
		}

		public virtual Dictionary<string, SignalReceiver> GetSignalReceivers()
		{
			return new();
		}

		public override void OnReset()
		{
			base.OnReset();
			isExecuting = false;
		}

		/// <summary>
		/// 获得包装后的剧情命令执行器列表
		/// </summary>
		/// <returns>剧情命令执行器列表</returns>
		public sealed override IList<ICommandExecutor> GetCommandExecutors()
		{
			m_executors = GetExecutors();
			m_signalReceivers = GetSignalReceivers();
			m_gathers = GetGathers();
			m_executorWrappers = new ICommandExecutor[m_executors.Count];
			int i = 0;
			foreach (KeyValuePair<string, Executor> executor in m_executors)
			{
				m_gathers.TryGetValue(executor.Key, out Gather gather);
				WrapperOptions options = new()
				{
					command = executor.Key,
					executor = Execute,
					gather = GatherRes,
					forceEnd = ForceCommandEnd,
					signalReceiver = RaiseSignal
				};
				m_executorWrappers[i++] = GenerateExecutorWrapper(options);
			}
			return m_executorWrappers;
		}

		/// <summary>
		/// 包装剧情命令执行器
		/// </summary>
		/// <param name="options">传入设置参数</param>
		/// <returns>包装后的剧情命令执行器</returns>
		protected virtual ICommandExecutor GenerateExecutorWrapper(WrapperOptions options)
		{
			return new CommandExecutorWrapper(options.command, options.executor, options.gather, options.forceEnd, options.signalReceiver);
		}

		/// <summary>
		/// 执行剧情命令上层函数
		/// </summary>
		/// <param name="command">剧情命令</param>
		/// <param name="finishCb">完成时回调</param>
		protected void Execute(Command command, Action finishCb)
		{
			m_finishCb = finishCb;
			isExecuting = true;
			m_executors.TryGetValue(command.command, out Executor executor);
			if (executor == null || !executor.Invoke(command))
            {
				FinishCommand();
            }
		}

		protected string GatherRes(Command command)
		{
			if (m_gathers.TryGetValue(command.command, out Gather gather))
			{
				return gather.Invoke(command);
			}
			return null;
		}

		protected void RaiseSignal(Command command)
		{
			if (m_signalReceivers.TryGetValue(tag, out SignalReceiver signalReceiver))
            {
				signalReceiver.Invoke(command);
			}
		}

		//EMPTY
		protected virtual void OnFinish() { }

		protected abstract void ForceCommandEnd();

		protected void FinishCommand()
		{
			OnFinish();
			if (m_finishCb != null)
            {
				m_finishCb.Invoke();
				m_finishCb = null;
            }
			isExecuting = false;
		}

		private ICommandExecutor[] m_executorWrappers;

		private Action m_finishCb;

		private Dictionary<string, Executor> m_executors = new();

		private Dictionary<string, SignalReceiver> m_signalReceivers = new();

		private Dictionary<string, Gather> m_gathers = new();

		public delegate bool Executor(Command command);

		public delegate string Gather(Command command);

		public delegate void SignalReceiver(Command command);

		protected struct WrapperOptions
		{
			public string command;

			public CommandExecutorWrapper.CommandExecuteDelegate executor;

			public CommandExecutorWrapper.CommandGatherDelegate gather;

			public Action forceEnd;

			public CommandExecutorWrapper.RaiseSignalDelegate signalReceiver;
		}
	}
}
