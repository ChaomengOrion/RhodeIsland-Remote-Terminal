// Created by ChaomengOrion
// Create at 2022-06-03 10:32:36
// Last modified on 2022-08-01 19:07:51

using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;
using UnityEngine.UI;
//using XLua;

namespace RhodeIsland.Arknights.AVG
{
	public class DecisionPanel : ExecutorComponent
	{
		protected override ICommandExecutor GenerateExecutorWrapper(WrapperOptions options)
		{
			return new InternalCommand(options.command, options.executor, options.forceEnd, options.signalReceiver);
		}

		public override Dictionary<string, Executor> GetExecutors()
		{
			return new()
            {
				[COMMAND_NAME_DECISION] = _ExecuteDecision,
				[COMMAND_NAME_PREDICATE] = _ExecutePredicate
            };
		}

		public override void OnReset()
		{
			gameObject.SetActive(false);
		}

		public void OnOptionButtonPressed(int index)
		{
			_SetOptionButtonSelect(index, true);
		}

		private void _SetOptionButtonSelect(int index, bool needFinishCommand = true)
		{
			if (m_command != null)
            {
				if (m_command.TryGetParam(PARAM_NAME_VALUES, out string values))
                {
					m_decisionCommandPredicator.decisionValue = _GetOptionValue(values, index);
                    if (AVGController.hasInstance)
                    {
                        AVGController.instance.decisionIndex = index;
                    }
                }
				if (needFinishCommand)
                {
					FinishCommand();
                }
            }
		}

		private bool _ExecuteDecision(Command command)
		{
			m_command = command;
			gameObject.SetActive(true);
			m_decisionCommandPredicator.decisionValue = 0;
			m_decisionCommandPredicator.referenceValues = null;
			AVGController.instance.SetCommandPredicator(m_decisionCommandPredicator);
			if (command.TryGetParam(PARAM_NAME_OPTIONS, out string options))
            {
				string[] splits = options.Split(new char[] { ';' });
				if (m_optionTexts.Length < splits.Length)
                {
					DLog.LogError(string.Format("[AVG] Too many decision options. Some will be ignored!", splits));
                }
				//MODIFY - AVG Auto
				_SetupOptionText(splits);
			}
			else
            {
				DLog.LogError("[AVG] No options parameter for Decision command");
            }
			return true;
		}

		private bool _ExecutePredicate(Command command)
		{
			DLog.Log("_ExecutePredicate Called");
			if (command.TryGetParam(PARAM_NAME_REFERENCES, out string references))
			{
				m_decisionCommandPredicator.referenceValues = _GetReferenceValue(references);
            }
			else
            {
				m_decisionCommandPredicator.referenceValues = null;
            }
			return false;
		}

		private void _SetupOptionText(string[] optionString)
		{
			if (m_optionTexts.Length > 0)
            {
                for (int i = 0; i < m_optionTexts.Length; i++)
                {
					if (i >= optionString.Length)
                    {
						_optionRoots[i].SetActive(false);
                    }
					else
                    {
						string op = optionString[i], content;
						bool interactable;
						if (op.StartsWith("&"))
                        {
							content = op[1..];
							interactable = false;
						}
						else
                        {
							content = op;
							interactable = true;
						}
						m_optionTexts[i].text = AVGTextManager.instance.Translate(content);
						m_optionButtons[i].interactable = interactable;
						_optionRoots[i].SetActive(true);
					}
                }
            }
		}

		private int _GetOptionValue(string valueString, int index)
		{
			string[] splits = valueString.Split(new char[] { ';' });
			if (index < 0 || splits.Length < index)
            {
				DLog.LogError("[AVG] Decision value index out of range");
            }
			else
            {
				if (int.TryParse(splits[index], out index))
                {
					return index;
                }
				else
                {
					DLog.LogError("[AVG] Decision value format error");
                }
            }
			return 0;
		}

		private int[] _GetReferenceValue(string valueString)
		{
			string[] splits = valueString.Split(new char[] { ';' });
			int[] values = new int[splits.Length];
			if (splits.Length > 0)
            {
                for (int i = 0; i < splits.Length; i++)
                {
					if (!int.TryParse(splits[i], out values[i]))
					{
						values[i] = 0;
					}
                }
            }
			return values;
		}

		protected override void OnFinish()
		{
			gameObject.SetActive(false);
			m_command = null;
			base.OnFinish();
		}

		protected override void ForceCommandEnd() { }

		private void Awake()
		{
			m_optionTexts = new Text[_optionRoots.Length];
			m_optionButtons = new Button[_optionRoots.Length];
			if (m_optionTexts.Length > 0)
            {
                for (int i = 0; i < m_optionTexts.Length; i++)
                {
					m_optionTexts[i] = _optionRoots[i].GetComponentInChildren<Text>(true);
					m_optionButtons[i] = _optionRoots[i].GetComponentInChildren<Button>(true);
                }
            }
		}

		private const string PARAM_NAME_OPTIONS = "options";
		private const string PARAM_NAME_VALUES = "values";
		private const string PARAM_NAME_REFERENCES = "references";
		private const string COMMAND_NAME_DECISION = "decision";
		private const string COMMAND_NAME_PREDICATE = "predicate";
		[SerializeField]
		private GameObject[] _optionRoots;
		private Text[] m_optionTexts;
		private Button[] m_optionButtons;
		private Command m_command;
		private DesicionCommandPrecidator m_decisionCommandPredicator = new();

		private class InternalCommand : CommandExecutorWrapper, IUIInputCommand
		{
			public InternalCommand(string command, CommandExecuteDelegate executor, [Optional] Action forceEnd, [Optional] RaiseSignalDelegate signalReceiver)
				: base(command, executor, null, forceEnd, signalReceiver)
			{ }
		}

		private class DesicionCommandPrecidator : AVGController.ICommandPredicator
		{
			public bool NeedToExecuteCommand(Command command)
			{
				if (referenceValues == null || referenceValues.Length == 0 || decisionValue == 0 || command.command == COMMAND_NAME_PREDICATE)
				{
					return true;
				}
				int lenght = referenceValues.Length;
				if (lenght > 0)
                {
					int i = 0;
					while (true)
                    {
						if (decisionValue == referenceValues[i])
                        {
							break;
                        }
						if (++i >= lenght)
                        {
							return false;
                        }
                    }
					return true;
				}
				return false;
			}
			public int decisionValue;
			public int[] referenceValues;
		}
	}
}