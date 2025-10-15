// Created by ChaomengOrion
// Create at 2022-04-29 18:56:05
// Last modified on 2022-08-01 19:07:50

using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using DG.Tweening;
using UnityEngine;
//using XLua;

namespace RhodeIsland.Arknights.AVG
{
	public class CharacterPanel : ExecutorComponent, IContainsResRefs, IFadeTimeRatio
	{
		public override Dictionary<string, Executor> GetExecutors()
		{
			return new()
            {
				["character"] = _ExecuteCharacter,
				["characteraction"] = _ExecuteCharacterAction
            };
		}

		public override Dictionary<string, Gather> GetGathers()
		{
			return new()
			{
				["character"] = _GatherCharacter,
			};
		}

		public override void OnReset()
		{
			base.OnReset();
			_middleSlot.OnReset();
			_leftSlot.OnReset();
			_rightSlot.OnReset();
		}

		public AbstractResRefCollecter DontInvoke_PlzImplInternalResRefCollector()
		{
			throw new NotImplementedException();
		}

		private bool _ExecuteCharacter(Command command)
		{
			string name = Command.GetOrDefault<string>("name", null, command.TryGetParam);
			string name2 = Command.GetOrDefault<string>("name2", null, command.TryGetParam);
			string enter = Command.GetOrDefault("enter", string.Empty, command.TryGetParam);
			string enter2 = Command.GetOrDefault("enter2", string.Empty, command.TryGetParam);
			float blackstart = Command.GetOrDefault("blackstart", 0f, command.TryGetParam);
			float blackend = Command.GetOrDefault("blackend", 0f, command.TryGetParam);
			float blackstart2 = Command.GetOrDefault("blackstart2", 0f, command.TryGetParam);
			float blackend2 = Command.GetOrDefault("blackend2", 0f, command.TryGetParam);
			float fadetime = Command.GetOrDefault("fadetime", 0.15f, command.TryGetParam);
			int focus = Command.GetOrDefault("focus", 0, command.TryGetParam);
			fadetime = CalculateFadetime(fadetime);
			if (string.IsNullOrEmpty(name) && string.IsNullOrEmpty(name2))
            {
				_middleSlot.Clear(fadetime);
				_leftSlot.Clear(fadetime);
				_rightSlot.Clear(fadetime);
				return false;
            }
			if (string.IsNullOrEmpty(name2))
            {
				_leftSlot.Clear(fadetime);
				_rightSlot.Clear(fadetime);
				if (enter != null)
                {
					if (enter == "left" || enter == "right")
                    {
						_middleSlot.SetCharPos(enter == "left" ? -1152f : 1152f, 0f, 0f);
						Color colorEnter;
						if (focus > 1)
						{
							colorEnter = _unfocusColor;
						}
						else
						{
							colorEnter = _focusColor;
						}
						_middleSlot.Set(name, 0f, colorEnter, true, false, blackstart, blackend);
						_middleSlot.transform.SetAsLastSibling();
						_middleSlot.SetCharPos(0f, 0f, fadetime);
						return false;
					}
					if (enter == "up" || enter == "down")
					{
						_middleSlot.SetCharPos(0f, enter == "up" ? 1072f : -1072f, 0f);
						Color colorEnter;
						if (focus > 1)
						{
							colorEnter = _unfocusColor;
						}
						else
						{
							colorEnter = _focusColor;
						}
						_middleSlot.Set(name, 0f, colorEnter, true, false, blackstart, blackend);
						_middleSlot.transform.SetAsLastSibling();
						_middleSlot.SetCharPos(0f, 0f, fadetime);
						return false;
					}
				}
				Color color;
				if (focus > 1)
                {
					color = _unfocusColor;
                }
				else
                {
					color = _focusColor;
                }
				_middleSlot.Set(name, fadetime, color, true, true, blackstart, blackend);
				_middleSlot.transform.SetAsLastSibling();
				return false;
			}
			_middleSlot.Clear(fadetime);
			if (enter == null)
            {
				Color color;
				if (focus > 1)
				{
					color = _unfocusColor;
				}
				else
				{
					color = _focusColor;
				}
				_leftSlot.Set(name, fadetime, color, true, true, blackstart, blackend);
				goto ENTER2;
			}
			if (enter != "left")
			{
				if (enter == "right")
				{
					_leftSlot.SetCharPos(1352f, 0f, 0f);
					Color colorEnter;
					if (focus > 1)
					{
						colorEnter = _unfocusColor;
					}
					else
					{
						colorEnter = _focusColor;
					}
					_leftSlot.Set(name, 0f, colorEnter, true, false, blackstart, blackend);
					_leftSlot.SetCharPos(0f, 0f, fadetime);
					goto ENTER2;
				}
				if (enter == "up" || enter == "down")
				{
					_leftSlot.SetCharPos(0f, enter == "up" ? 1072f : -1072f, 0f);
					Color colorEnter;
					if (focus > 1)
					{
						colorEnter = _unfocusColor;
					}
					else
					{
						colorEnter = _focusColor;
					}
					_leftSlot.Set(name, 0f, colorEnter, true, false, blackstart, blackend);
					_leftSlot.SetCharPos(0f, 0f, fadetime);
					goto ENTER2;
				}
				Color color;
				if (focus > 1)
				{
					color = _unfocusColor;
				}
				else
				{
					color = _focusColor;
				}
				_leftSlot.Set(name, fadetime, color, true, true, blackstart, blackend);
				goto ENTER2;
			}
			_leftSlot.SetCharPos(-952f, 0f, 0f);
			Color colorLeft;
			if (focus > 1)
			{
				colorLeft = _unfocusColor;
			}
			else
			{
				colorLeft = _focusColor;
			}
			_leftSlot.Set(name, 0f, colorLeft, true, false, blackstart, blackend);
			_leftSlot.SetCharPos(0f, 0f, fadetime);
		ENTER2:
			if (enter2 == null)
            {
				Color colorEnter2;
				if ((focus | 2) == 2)
				{
					colorEnter2 = _focusColor;
				}
				else
				{
					colorEnter2 = _unfocusColor;
				}
				_rightSlot.Set(name2, fadetime, colorEnter2, true, true, blackstart, blackend);
				goto END;
			}
			if (enter2 == "left")
			{
				_rightSlot.SetCharPos(-1352f, 0f, 0f);
				Color colorEnter2;
				if ((focus | 2) == 2)
				{
					colorEnter2 = _focusColor;
				}
				else
				{
					colorEnter2 = _unfocusColor;
				}
				_rightSlot.Set(name2, 0f, colorEnter2, true, false, blackstart2, blackend2);
				_rightSlot.SetCharPos(0f, 0f, fadetime);
				goto END;
			}
			if (enter2 == "right")
			{
				_rightSlot.SetCharPos(952f, 0f, 0f);
				Color colorEnter2;
				if ((focus | 2) == 2)
				{
					colorEnter2 = _focusColor;
				}
				else
				{
					colorEnter2 = _unfocusColor;
				}
				_rightSlot.Set(name2, 0f, colorEnter2, true, false, blackstart2, blackend2);
				_rightSlot.SetCharPos(0f, 0f, fadetime);
				goto END;
			}
			if (enter2 == "up" || enter2 == "down")
			{
				_rightSlot.SetCharPos(0f, enter2 == "up" ? 1072f : -1072f, 0f);
				Color colorEnter2;
				if (focus > 1)
				{
					colorEnter2 = _unfocusColor;
				}
				else
				{
					colorEnter2 = _focusColor;
				}
				_rightSlot.Set(name2, 0f, colorEnter2, true, false, blackstart2, blackend2);
				_rightSlot.SetCharPos(0f, 0f, fadetime);
			}
			else
			{
				Color colorEnter2;
				if ((focus | 2) == 2)
				{
					colorEnter2 = _focusColor;
				}
				else
				{
					colorEnter2 = _unfocusColor;
				}
				_rightSlot.Set(name2, fadetime, colorEnter2, true, true, blackstart, blackend);
			}
			END:
			if (focus == 2)
			{
				_rightSlot.transform.SetAsLastSibling();
			}
			if (focus == 1)
			{
				_leftSlot.transform.SetAsLastSibling();
			}
			return false;
		}

		private string _GatherCharacter(Command command)
        {
			string name = Command.GetOrDefault<string>("name", null, command.TryGetParam);
			string name2 = Command.GetOrDefault<string>("name2", null, command.TryGetParam);
			if (!string.IsNullOrEmpty(name))
            {
				if (!string.IsNullOrEmpty(name2))
                {
					return AVGCharacterSlot.GetCharPath(name) + '|' + AVGCharacterSlot.GetCharPath(name2);
                }
				return AVGCharacterSlot.GetCharPath(name);
            }
			return null;
		}

		private bool _ExecuteCharacterAction(Command command)
		{
			string type = Command.GetOrDefault("type", string.Empty, command.TryGetParam);
			if (type == null)
            {
				goto Error;
			}
			if (type == "move")
			{
				return _ExecuteCharacterMove(command);
			}
			if (type == "jump")
			{
				return _ExecuteCharacterJump(command);
			}
			if (type == "exit")
			{
				return _ExecuteCharacterExit(command);
			}
			if (type == "zoom")
			{
				return _ExecuteCharacterZoom(command);
			}
		Error:
			Debug.LogError("[AVG] Unknown Character Action type");
			return false; 
		}

		private bool _ExecuteCharacterMove(Command command)
		{
			string name = Command.GetOrDefault("name", "left", command.TryGetParam);
			float xpos = Command.GetOrDefault("xpos", 0f, command.TryGetParam);
			float ypos = Command.GetOrDefault("ypos", 0f, command.TryGetParam);
			float fadetime = Command.GetOrDefault("fadetime", 0.5f, command.TryGetParam);
			bool isblock = Command.GetOrDefault("isblock", false, command.TryGetParam);
			fadetime = CalculateFadetime(fadetime);
			AVGCharacterSlot slot;
			if (name == null)
            {
				slot = _leftSlot;
				goto END;
			}
			else if (name != "right")
            {
				if (name == "middle")
                {
					slot = _middleSlot;
					goto END;
				}
				slot = _leftSlot;
				goto END;
			}
			slot = _rightSlot;
			END:
			return _AddFinishCommand(isblock, slot.MoveChar(xpos, ypos, fadetime));
		}

		private bool _ExecuteCharacterJump(Command command)
		{
			return false;
		}

		private bool _ExecuteCharacterZoom(Command command)
		{
			return false;
		}

		private bool _ExecuteCharacterExit(Command command)
		{
			string name = Command.GetOrDefault("name", "left", command.TryGetParam);
			string direction = Command.GetOrDefault("direction", "left", command.TryGetParam);
			float fadetime = Command.GetOrDefault("fadetime", 1f, command.TryGetParam);
			bool isblock = Command.GetOrDefault("isblock", false, command.TryGetParam);
			fadetime = CalculateFadetime(fadetime);
			Tween tween;
			if (name == "left")
            {
				if (direction == "left")
                {
					tween = _leftSlot.SetCharPos(-952f, 0f, fadetime);
				}
				else if (direction == "right")
				{
					tween = _leftSlot.SetCharPos(1352f, 0f, fadetime);
				}
				else if (direction == "up")
				{
					tween = _leftSlot.SetCharPos(0f, 1072f, fadetime);
				}
				else if(direction == "down")
				{
					tween = _leftSlot.SetCharPos(0f, -1072f, fadetime);
				}
				else
                {
                    return _AddFinishCommand(isblock, null);
                }
			}
			else
            {
				if (name == "right")
				{
					if (direction == "left")
					{
						tween = _rightSlot.SetCharPos(-1352f, 0f, fadetime);
					}
					else if (direction == "right")
					{
						tween = _rightSlot.SetCharPos(952f, 0f, fadetime);
					}
					else if (direction == "up")
					{
						tween = _rightSlot.SetCharPos(0f, 1072f, fadetime);
					}
					else if (direction == "down")
					{
						tween = _rightSlot.SetCharPos(0f, -1072f, fadetime);
					}
					else
                    {
                        return _AddFinishCommand(isblock, null);
                    }
				}
				else
                {
					if (direction == "left")
					{
						tween = _middleSlot.SetCharPos(-1152f, 0f, fadetime);
					}
					else if (direction == "right")
					{
						tween = _middleSlot.SetCharPos(1152f, 0f, fadetime);
					}
					else if (direction == "up")
					{
						tween = _middleSlot.SetCharPos(0f, 1072f, fadetime);
					}
					else if (direction == "down")
					{
						tween = _middleSlot.SetCharPos(0f, -1072f, fadetime);
					}
					else
                    {
                        return _AddFinishCommand(isblock, null);
                    }
				}
			}
			return _AddFinishCommand(isblock, tween);
		}

		protected override void ForceCommandEnd()
		{
		}

		public override void OnStoryEnd(Story story)
		{
			base.OnStoryEnd(story);
			_middleSlot.Clear();
			_leftSlot.Clear();
			_rightSlot.Clear();
		}

		private bool _AddFinishCommand(bool block, Tween tween)
		{
			if (tween != null && block)
            {
				tween.OnComplete(FinishCommand);
            }
			return block && tween != null;
		}

		public float CalculateFadetime(float initialFadetime)
		{
			return initialFadetime * AVGController.instance.animateRatio;
		}

		public bool NeedSkipAnimation(float fadetime)
		{
			return MathUtil.IsZero(fadetime);
		}

		private const float HORIZONTAL_OUTSCREEN_DELTA = 1152f;

		private const float VERTICAL_OUTSCREEN_DELTA = 1072f;

		private const float LEFT_CHAR_HORIZONAL_DELTA = 200f;

		private const float DEFAULT_FADE_TIME = 0.15f;

		[SerializeField]
		private AVGCharacterSlot _middleSlot;

		[SerializeField]
		private AVGCharacterSlot _leftSlot;

		[SerializeField]
		private AVGCharacterSlot _rightSlot;

		[SerializeField]
		private Color _focusColor = Color.white;

		[SerializeField]
		private Color _unfocusColor = Color.grey;

		private class InternalResRefCollector : AbstractResRefCollecter
		{
			public InternalResRefCollector()
			{
			}

			public override void GatherResRefs(Command command, HashSet<string> references)
			{
			}

			public override void GatherResFilenames(Command command, HashSet<string> filenames)
			{
			}

			private string _StripResPath(string name)
			{
				return null;
			}

			private Regex m_regex;
		}
	}
}