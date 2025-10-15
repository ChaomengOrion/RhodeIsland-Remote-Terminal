// Created by ChaomengOrion
// Create at 2022-05-14 16:58:53
// Last modified on 2022-08-01 22:18:34

using System.Collections.Generic;
using System.Text;
using Lucene.Net.Documents;
using Lucene.Net.Index;
using Lucene.Net.Search;
using Lucene.Net.Search.Highlight;
using Lucene.Net.Store;
using Lucene.Net.Util;
using RhodeIsland.Arknights.AVG;
using RhodeIsland.RemoteTerminal.AVG;

namespace RhodeIsland.RemoteTerminal.Search
{
    public class Utils
    {
        public static Document WriteStoryDocument(string path, string title, string chapter, StoryReviewType type, string content)
        {
            Document doc = new();
            StringField id = new("path", path ?? string.Empty, Field.Store.YES);
            StringField chapterField = new("chapter", chapter ?? string.Empty, Field.Store.YES);
            Int32Field typeField = new("type", (int)type, Field.Store.YES);
            TextField titleField = new("title", title ?? string.Empty, Field.Store.YES);
            titleField.Boost = 2f;
            TextField contentField = new("content", content ?? string.Empty, Field.Store.NO);
            contentField.Boost = 1f;

            doc.Add(id);
            doc.Add(chapterField);
            doc.Add(typeField);
            doc.Add(titleField);
            doc.Add(contentField);
            return doc;
        }

        /*public static string GetStoryContent(Story story)
        {
            StringBuilder sb = new();
            foreach (var command in story.commands)
            {
                if (!string.IsNullOrEmpty(command.content))
                {
                    sb.AppendLine(command.content);
                }
                else if (command.command.ToLower() == "decision")
                {
                    command.TryGetParam("options", out string s);
                    sb.AppendLine(s);
                }
            }
            return sb.ToString();
        }*/
    }
}