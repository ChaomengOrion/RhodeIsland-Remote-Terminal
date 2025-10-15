// Created by ChaomengOrion
// Create at 2022-04-29 18:56:05
// Last modified on 2022-08-01 19:07:50

using System;
using System.Collections.Generic;
//using XLua;

namespace RhodeIsland.Arknights.AVG
{
	public class AVGGotoLabel : ExecutorComponent
	{
		public override Dictionary<string, Executor> GetExecutors()
		{
			return new()
            {
				[COMMAND_NAME_LABEL] = _ExecuteLabel,
				[COMMAND_NAME_GOTO] = _ExecuteGoto
            };
		}

		public override void OnReset()
		{
			m_gotoLabelController.Reset();
			AVGController.instance.SetCommandFlowController(m_gotoLabelController);
		}

		private bool _ExecuteLabel(Command command) { return false; }

		private bool _ExecuteGoto(Command command)
		{
			if (command.TryGetParam(PARAM_NAME_NAME, out string name))

			{
				m_gotoLabelController.TryGotoLabel(name);
				return false;
            }
			else
            {
				UnityEngine.Debug.LogWarning("[AVG] Missing parameter [name] for Goto command");
				return false;
			}
		}

		protected override void OnFinish()
		{
			base.OnFinish();
		}

		protected override void ForceCommandEnd() { }

		private const string PARAM_NAME_NAME = "name";
		private const string COMMAND_NAME_LABEL = "label";
		private const string COMMAND_NAME_GOTO = "warp";
		private GotoLabelController m_gotoLabelController = new();

		private class GotoLabelController : AVGController.ICommandFlowController
		{
			public void RegisterLabel(string label, int index)
			{
				if (m_labelMap.ContainsKey(label))
				{
					UnityEngine.Debug.LogWarning(string.Format("[AVG] Label duplicated : {0}, the old one will be overrided!", label));
				}
				m_labelMap[label] = index;
			}

			public void TryGotoLabel(string label)
			{
				if (m_labelMap.ContainsKey(label))
                {
					m_gotoIndex = m_labelMap[label] + 1;
                }
				else
                {
					UnityEngine.Debug.LogWarning(string.Format("[AVG] Trying to warp to the missing label : {0}", label));
                }
			}

			public void Reset()
			{
				m_gotoIndex = -1;
				m_labelMap.Clear();
			}

			public void PreprocessCommands(List<Command> commands)
			{
                for (int i = 0; i < commands.Count; i++)
                {
					if (commands[i].command == COMMAND_NAME_LABEL)
                    {
                        if (commands[i].TryGetParam(PARAM_NAME_NAME, out string name))
                        {
                            RegisterLabel(name, i);
                        }
						else
                        {
							UnityEngine.Debug.LogWarning("[AVG] Missing parameter [name] for Label command");
                        }
                    }
                }
			}

			public int GotoCommandIndex()
			{
				int result = m_gotoIndex;
				m_gotoIndex = -1;
				return result;
			}

			private Dictionary<string, int> m_labelMap = new();
			private int m_gotoIndex = -1;
		}
	}
}