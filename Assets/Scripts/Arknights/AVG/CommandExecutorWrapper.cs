// Created by ChaomengOrion
// Create at 2022-04-29 18:56:05
// Last modified on 2022-08-01 19:07:49

using System;
using System.Runtime.InteropServices;

namespace RhodeIsland.Arknights.AVG
{
	/// <summary>
	/// AVG命令执行包装器
	/// </summary>
	public class CommandExecutorWrapper : ICommandExecutor
	{
		public CommandExecutorWrapper(string command, CommandExecuteDelegate executor, CommandGatherDelegate gather, [Optional] Action forceEnd, [Optional] RaiseSignalDelegate signalReceiver)
		{
			m_executor = executor;
			m_gather = gather;
			m_forceEnd = forceEnd;
			m_signalReceiver = signalReceiver;
			this.command = command;
		}

		public string command { get; private set; }

		public void Execute(Command command, Action<ICommandExecutor> finishCb)
		{
			m_finishCb = finishCb;
			m_executor.Invoke(command, _OnFinish);
		}

		public string Gather(Command command)
        {
			return m_gather?.Invoke(command);
        }

		public void RaiseSignal(Command command)
		{
			m_signalReceiver.Invoke(command);
		}

		public void ForceEnd()
		{
			m_forceEnd?.Invoke();
			_OnFinish();
		}

		private void _OnFinish()
		{
			m_finishCb?.Invoke(this);
		}

		private CommandExecuteDelegate m_executor;

		private CommandGatherDelegate m_gather;

		private RaiseSignalDelegate m_signalReceiver;

		private Action m_forceEnd;

		private Action<ICommandExecutor> m_finishCb;

		public delegate void CommandExecuteDelegate(Command command, Action finishCb);

		public delegate string CommandGatherDelegate(Command command);

		public delegate void RaiseSignalDelegate(Command command);
	}
}
