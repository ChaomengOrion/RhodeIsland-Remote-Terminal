// Created by ChaomengOrion
// Create at 2022-04-29 18:56:05
// Last modified on 2022-08-01 19:07:50

using System;

namespace RhodeIsland.Arknights.AVG
{
	public interface ICommandExecutor
	{
		string command { get; }

		void Execute(Command command, Action<ICommandExecutor> finishCb);

		string Gather(Command command);

		void RaiseSignal(Command command);

		void ForceEnd();
	}
}