// Created by ChaomengOrion
// Create at 2022-07-22 12:09:37
// Last modified on 2022-08-01 19:07:51

using System.Text;
using System.Collections.Generic;
using UnityEngine;
using RhodeIsland.Arknights.AVG;

namespace RhodeIsland.RemoteTerminal
{
    public static class StoryUtil
    {
        public static (int, float) CalculateFontCountAndReadTime(Story story, int cgCount)
        {
            string txt = ParseStoryToTxt(story, out int lineCount, true);
            return (txt.Length, txt.Length / 10f + lineCount * 0.8f + cgCount * 10f); //TODO
        }

        public static int GetCGCount(IEnumerable<Story> storys)
        {
            List<string> names = new();
            foreach (Story story in storys)
            {
                foreach (Command command in story.commands)
                {
                    if (command.command == "image" && command.TryGetParam("image", out string image))
                    {
                        if (!names.Contains(image))
                        {
                            names.Add(image);
                        }
                    }
                }
            }
            return names.Count;
        }

        public static int GetCGCount(Story story)
        {
            int count = 0;
            foreach (Command command in story.commands)
            {
                if (command.command == "image" && command.TryGetParam("image", out string image))
                {
                    count++;
                }
            }
            return count;
        }

        public static string[] GetMainCharacters(Story story)
        {
            List<string> characters = new();
            List<(int, int)> counts = new();
            foreach (Command command in story.commands)
            {
                if (command.command == "dialog" && command.TryGetParam("name", out string name))
                {
                    if (characters.Contains(name))
                    {
                        (int, int) pair = counts[characters.IndexOf(name)];
                        pair.Item2++;
                        counts[characters.IndexOf(name)] = pair;
                    }
                    else
                    {
                        characters.Add(name);
                        counts.Add((characters.Count - 1, 1));
                    }
                }
            }
            counts.Sort((a, b) => b.Item2 - a.Item2);
            List<string> result = new();
            for (int i = 0; i < counts.Count; i++)
            {
                if (counts[i].Item2 >= 10 && result.Count < 6)
                {
                    result.Add(characters[counts[i].Item1]);
                }
                else
                    break;
            }
            return result.ToArray();
        }

        public static string ParseStoryToTxt(Story story, out int lineCount, bool withOutAddition = false)
        {
            lineCount = 0;
            StringBuilder sb = new();
            foreach (Command command in story.commands)
            {
                switch (command.command)
                {
                    case "dialog":
                        if (!string.IsNullOrEmpty(command.content))
                        {
                            if (!withOutAddition && command.TryGetParam("name", out string name))
                                sb.AppendLine($"{name}： {command.content}");
                            else
                                sb.AppendLine(command.content);
                            lineCount++;
                        }
                        break;
                    case "subtitle":
                        if (command.TryGetParam("text", out string text))
                        {
                            sb.AppendLine(text);
                            lineCount++;
                        }
                        break;
                    case "decision":
                        if (command.TryGetParam("options", out string optionsStr) && command.TryGetParam("values", out string valuesStr))
                        {
                            string[] values = valuesStr.Split(new char[] { ';' });
                            string[] options = optionsStr.Split(new char[] { ';' });
                            for (int i = 0; i < options.Length; i++)
                            {
                                string op = options[i];
                                if (op.StartsWith("&"))
                                    op = op[1..];
                                if (withOutAddition)
                                    sb.AppendLine(op);
                                else
                                    sb.AppendLine($"[选项{values[i]}]： {op}");
                                lineCount++;
                            }
                        }
                        break;
                    default:
                        break;
                }
            }
            return sb.ToString();
        }
    }

}