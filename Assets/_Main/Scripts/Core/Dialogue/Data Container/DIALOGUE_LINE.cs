using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using UnityEngine;


namespace Dialogue
{
    // contain a single line that is converted to current action
    public class DIALOGUE_LINE
    {
        public string speaker;
        public string content;
        public string command;
        public SpeakerInfo speakerInfo;

		string NAME_CAST = @" as ";
		string POS_CAST = @" at ";
		string EXPRESS_CAST = @" [";
        string dividePattern;

        char POS_SEP = ':';
        char EXPRESS_SEP = ',';
        char EXRPESS_DET_SEP = ':';

		public DIALOGUE_LINE( string speaker, string content, string command )
        {
            this.speaker = speaker;
            this.content = content;
            this.command = command;
			this.dividePattern = $@"{NAME_CAST}|{POS_CAST}|{EXPRESS_CAST.Insert(1, @"\")}";
			this.speakerInfo = getSpeakerInfo(speaker);
            show();
        }

        public bool hasContent => content.Trim().Length > 0;
        public bool hasCommand => command.Trim().Length > 0;
        public bool hasSpeaker => speaker.Trim().Length > 0;

        public void show()
        {
            Debug.Log(speaker);
            Debug.Log(content);
            Debug.Log(command);
            speakerInfo.show();
		}

		public SpeakerInfo getSpeakerInfo(string speaker)
		{
            SpeakerInfo speakerInfo = new SpeakerInfo();

			MatchCollection matches = Regex.Matches(speaker, dividePattern);

			if(matches.Count == 0)
            {
                speakerInfo.name = speaker;
            }
            else
            {
                int ind = matches[0].Index;
                speakerInfo.name = speaker.Substring(0, ind);
                for(int i = 0; i < matches.Count; i++)
                {
                    Match match = matches[i];
                    int startIndex = 0, endIndex = 0; 
                    if(match.Value == NAME_CAST)
                    {
                        startIndex = match.Index + match.Value.Length;
                        endIndex = (i == matches.Count-1) ? speaker.Length : matches[i + 1].Index;
                        speakerInfo.castName = speaker.Substring(startIndex, endIndex - startIndex);
                    }
                    else if(match.Value == POS_CAST)
                    {
                        startIndex = match.Index + match.Value.Length;
                        endIndex = (i == matches.Count-1) ? speaker.Length : matches[i + 1].Index;
                        string posInfo = speaker.Substring(startIndex, endIndex - startIndex);
                        string[] strs = posInfo.Split(POS_SEP, StringSplitOptions.RemoveEmptyEntries);
                        speakerInfo.position.x = float.Parse(strs[0]);
                        if(strs.Length > 1)
                        {
							speakerInfo.position.y = float.Parse(strs[1]);
						}
					}
                    else if(match.Value == EXPRESS_CAST)
                    {
						startIndex = match.Index + 2;
						endIndex = (i == matches.Count-1) ? speaker.Length : matches[i + 1].Index;
                        endIndex -= 1;
                        
						string expressInfo = speaker.Substring(startIndex, endIndex - startIndex);
                        speakerInfo.expressions = new List<(string layer, string expression)>();

						speakerInfo.expressions = expressInfo.Split(EXPRESS_SEP, StringSplitOptions.RemoveEmptyEntries).Select(x =>
                        {
                            var parts = x.Trim().Split(EXRPESS_DET_SEP, StringSplitOptions.RemoveEmptyEntries);
							return (parts[0], parts[1]);
                        }).ToList();
					}
				}
            }
			return speakerInfo;
		}

	}

    public struct SpeakerInfo
    {
        public string name, castName;
		public Vector2 position;
		public List<(string layer, string expression)> expressions;
        public void show()
        {
            Debug.Log(name);
            Debug.Log(castName);
            Debug.Log(position);
            if (expressions != null)
            {
                foreach (var expression in expressions)
                {
                    Debug.Log(expression);
                }
            }
            else Debug.Log(expressions);
		}
	}

}