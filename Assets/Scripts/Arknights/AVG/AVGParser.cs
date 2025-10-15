// Created by ChaomengOrion
// Create at 2022-04-29 18:56:05
// Last modified on 2022-08-01 19:07:50

using System;
using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;

namespace RhodeIsland.Arknights.AVG
{
	public class AVGParser : IAVGParser
	{
		public IAVGVariableConverter variableConverter { private get; set; }

		/// <summary>
		/// 将剧情原始字符串解析为命令数组
		/// </summary>
		/// <param name="content">原始字符串</param>
		/// <param name="commands">输出的命令数组</param>
		/// <returns>是否成功</returns>
		public bool TryParse(string content, out List<Command> commands)
		{
			m_errors.Clear();
			commands = new();
			StringReader reader = new(content);
			for (int i = 0; ; )
            {
				TextBlock text = _ReadNextBlock(reader);
				if (text.text == null)
					break;
				if (!CheckIfSkipLine(text.text))
                {
                    try
                    {
						Command command = _ParseCommand(text);
						command.lineNumber = i + 1;
						commands.Add(command);
					}
					catch (Exception ex)
                    {
						if (text.isConcat)
                        {
							_AppendError(string.Format("Error in line {0} to {1}: {2}", i, text.readLineCount, ex.Message));
                        }
						else
                        {
							_AppendError(string.Format("Error in line {0}: {1}", i, ex.Message + ex.StackTrace));
						}
                    }
                }
				i += text.readLineCount;
			}
			if (reader != null)
            {
				reader.Dispose();
			}
			return m_errors.Count == 0;
		}

		/// <summary>
		/// 将剧情原始字符串解析为Story对象
		/// </summary>
		/// <param name="content">原始字符串</param>
		/// <param name="story">输出的Story对象</param>
		/// <returns>是否成功</returns>
		public bool TryParse(string content, out Story story)
		{
			return TryParse(content, new Story.StoryParam(), out story);
		}

		/// <summary>
		/// 将剧情原始字符串解析为Story对象（带参数）
		/// </summary>
		/// <param name="content">原始字符串</param>
		/// <param name="param">参数</param>
		/// <param name="story">输出的Story对象</param>
		/// <returns>是否成功</returns>
		public bool TryParse(string content, Story.StoryParam param, out Story story)
		{
			story = new();
			if (!TryParse(content, out story.commands))
				return false;
			if (story.commands.Count > 0)
            {
				if (story.commands[0].command == "header")
                {
					Command first = story.commands[0];
					story.title = first.content;
					story.id = Command.GetOrDefault("key", string.Empty, first.TryGetParam);
					story.param = param;
					if (!string.IsNullOrEmpty(story.param.overrideId))
                    {
						story.id = story.param.overrideId;
					}
					story.isTutorial = Command.GetOrDefault("is_tutorial", false, first.TryGetParam);
					story.isSkippable = Command.GetOrDefault("is_skippable", story.isTutorial, first.TryGetParam);
					story.isVideoOnly = Command.GetOrDefault("is_video_only", false, first.TryGetParam);
					story.isAutoable = Command.GetOrDefault("is_autoable", !story.isTutorial && !story.isVideoOnly, first.TryGetParam);
					story.denyAutoSwitchScene = Command.GetOrDefault("deny_auto_switch_scene", false, first.TryGetParam);
					story.dontClearGameObjectPoolOnStart = Command.GetOrDefault("dont_clear_gameobjectpool_onstart", false, first.TryGetParam);
					story.fitMode = first.param.GetEnum("fit_mode", AVGController.FitMode.DEFAULT, true);
					story.characterSortType = first.param.GetEnum("char_sort_type", UI.CharacterSortType.BY_GAIN_TIME_UP, true);
				}
				return true;
			}
			return false;
		}

		/// <summary>
		/// 获取解析过程中的错误
		/// </summary>
		/// <returns></returns>
		public string GetErrorMessage()
		{
			System.Text.StringBuilder builder = new System.Text.StringBuilder();
			foreach (string error in m_errors)
            {
				builder.AppendLine(error);
            }
			return builder.ToString();
		}

		/// <summary>
		/// 判断命令是否为无意义行或注释
		/// </summary>
		/// <param name="str">行文本</param>
		/// <returns></returns>
		public static bool CheckIfSkipLine(string str)
		{
			if (string.IsNullOrEmpty(str))
            {
				return true;
            }
			if (!ONLY_SPACE_REGEX.Match(str).Success)
            {
				return COMMENT_REGEX.Match(str).Success;
            }
			return true;
		}

		/// <summary>
		/// 判断命令是否为注释
		/// </summary>
		/// <param name="str">行文本</param>
		/// <returns></returns>
		public static bool CheckIfComment(string str)
		{
			if (str == null)
			{
				return true;
			}
			return COMMENT_REGEX.Match(str).Success;
		}

		/// <summary>
		/// 添加解析时错误
		/// </summary>
		/// <param name="error"></param>
		private void _AppendError(string error)
		{
			m_errors.Add(error);
			UnityEngine.Debug.LogError(error);
		}

		/// <summary>
		/// 将原始命令解析
		/// </summary>
		/// <param name="block">原始命令</param>
		/// <returns>解析后的命令</returns>
		/// <exception cref="ArgumentNullException"></exception>
		private Command _ParseCommand(TextBlock block)
		{
			if (string.IsNullOrEmpty(block.text))
            {
				throw new ArgumentNullException("Line content is null or empty!"); //MODIFY
            }
			// 正则表达式: ^\[\s*(?:(.*?)\((.*)\)|(?:([\.|\w]*)|(.*)))\s*\]\s*(.*)
			// 例子1: [命令(参数A=X)]文字
			// 例子2: [只包括字母和'.'的命令]
			// 例子3: [参数B=X]
			// 捕获组0 - (全部)
			// 捕获组1 - "命令"
			// 捕获组2 - "参数A=X"
			// 捕获组3 - "只包括字母和'.'的命令"
			// 捕获组4 - "参数B=X"
			// 捕获组5 - "文字"
			Match match = COMMAND_REGEX.Match(block.text);
			Command command = new();
			if (!match.Success) //未匹配成功，为纯对话内容
            {
				command.command = "dialog";
				command.content = block.text;
				return command;
            }
			//读取命令
			if (match.Groups[3].Length > 0)
            {
				command.command = match.Groups[3].Value.ToLower();
			}
			else if (match.Groups[1].Length > 0)
			{
				command.command = match.Groups[1].Value.ToLower();
			}
            else
            {
				command.command = "dialog";
			}
			//读取文本
			if (match.Groups[5].Length > 0)
			{
				command.content = match.Groups[5].Value;
			}
			else
            {
				command.content = null;
			}
			//读取参数
			string param;
			if (match.Groups[2].Length <= 0)
			{
				param = match.Groups[4].Value;
			}
			else
			{
				param = match.Groups[2].Value;
			}
			if (!string.IsNullOrEmpty(param))
            {
				string paramFixed = _ReplaceEqualSignWithColon(param);
				command.param = Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(string.Format("{{{0}}}", paramFixed));
				if (command.param != null)
                {
					if (variableConverter != null)
                    {
						Dictionary<string, object> paramDict = command.param;
						List<string> keys = new(paramDict.Keys);
						if (keys.Count > 0)
                        {
							for (int i = 0; i < keys.Count; i++)
                            {
								string key = keys[i];
								paramDict[key] = variableConverter.Convert(paramDict[key]);
							}
                        }
					}
                }
            }
			return command;
		}

		/// <summary>
		/// 读取下一个文本块
		/// </summary>
		/// <param name="reader">StringReader</param>
		/// <returns>文本块</returns>
		private TextBlock _ReadNextBlock(StringReader reader)
		{
			string line = reader.ReadLine();
			if (line != null)
            {
				System.Text.StringBuilder builder = new();
				builder.Append(line);
				int readLineCount = 0;
				bool conact = false;
				while (builder.Length > 0)
                {
					if (builder[^1] != ESCAPE_CHAR)
                    {
						break;
                    }
					builder.Remove(builder.Length - 1, 1);
					string next = reader.ReadLine();
					conact = true;
					if (next == null)
                    {
						break;
                    }
					builder.Append(next);
					++readLineCount;
                }
				return new TextBlock { text = builder.ToString(), isConcat = conact, readLineCount = readLineCount + 1 };
            }
			else
            {
				return new TextBlock { text = null, isConcat = false, readLineCount = 0 };
			}
		}

		/// <summary>
		/// 将参数字符串转为json标准类型
		/// </summary>
		/// <param name="str">原始参数</param>
		/// <returns>转移后参数</returns>
		private string _ReplaceEqualSignWithColon(string str)
		{
			System.Text.StringBuilder builder = new();
			if (str.Length > 0)
			{

				bool inStr = false;
				for (int i = 0; i < str.Length; i++)
                {
					if (inStr || str[i] != '=')
					{
						if (str[i] == '"' && (i <= 0 || str[i - 1] != '\\'))
						{
							inStr = !inStr;
						}
						builder.Append(str[i]);
					}
					else
					{
						builder.Append(':');
					}
				}
            }
			return builder.ToString();
		}

		private const char ESCAPE_CHAR = '\\'; //换行连接符
		private static readonly Regex COMMAND_REGEX = new(@"^\[\s*(?:(.*?)\((.*)\)|(?:([\.|\w]*)|(.*)))\s*\]\s*(.*)", RegexOptions.ECMAScript); //匹配命令
		private static readonly Regex COMMENT_REGEX = new(@"^\s*//.*$"); //匹配注释
		private static readonly Regex ONLY_SPACE_REGEX = new(@"^\s+$");  //匹配空行
		private List<string> m_errors = new();

		/// <summary>
		/// 文本块
		/// </summary>
		private struct TextBlock
		{
			public bool IsEmpty()
			{
				return text.Length == 0;
			}

			public string text;
			public bool isConcat;
			public int readLineCount;
		}
	}
}
