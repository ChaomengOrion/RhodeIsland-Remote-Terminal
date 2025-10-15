// Created by ChaomengOrion
// Create at 2022-04-29 18:56:05
// Last modified on 2022-08-01 19:07:50

using System;
using System.Collections.Generic;

namespace RhodeIsland.Arknights.AVG
{
	public interface IAVGParser
	{
		string GetErrorMessage();
		bool TryParse(string content, out List<Command> commands);
		bool TryParse(string content, Story.StoryParam param, out Story story);
	}
}
