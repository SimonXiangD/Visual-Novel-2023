using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using UnityEngine;
using System.Linq;

public class DL_SPEAKER
{
	public string name, castName;
	public Vector2 position;
	public List<(string layer, string expression)> expressions;

	string NAME_CAST = @" as ";
	string POS_CAST = @" at ";
	string EXPRESS_CAST = @" [";
	string dividePattern;

	char POS_SEP = ':';
	char EXPRESS_SEP = ',';
	char EXRPESS_DET_SEP = ':';

	public string speakerName => castName.Length > 0 ? castName : name;
	
	public DL_SPEAKER(string speaker)
	{
		dividePattern = $@"{NAME_CAST}|{POS_CAST}|{EXPRESS_CAST.Insert(1, @"\")}";

		name = "";
		castName = "";

		MatchCollection matches = Regex.Matches(speaker, dividePattern);

		if (matches.Count == 0)
		{
			name = speaker;
		}
		else
		{
			int ind = matches[0].Index;
			name = speaker.Substring(0, ind);
			for (int i = 0; i < matches.Count; i++)
			{
				Match match = matches[i];
				int startIndex = 0, endIndex = 0;
				if (match.Value == NAME_CAST)
				{
					startIndex = match.Index + match.Value.Length;
					endIndex = (i == matches.Count - 1) ? speaker.Length : matches[i + 1].Index;
					castName = speaker.Substring(startIndex, endIndex - startIndex);
				}
				else if (match.Value == POS_CAST)
				{
					startIndex = match.Index + match.Value.Length;
					endIndex = (i == matches.Count - 1) ? speaker.Length : matches[i + 1].Index;
					string posInfo = speaker.Substring(startIndex, endIndex - startIndex);
					string[] strs = posInfo.Split(POS_SEP, StringSplitOptions.RemoveEmptyEntries);
					position.x = float.Parse(strs[0]);
					if (strs.Length > 1)
					{
						position.y = float.Parse(strs[1]);
					}
				}
				else if (match.Value == EXPRESS_CAST)
				{
					startIndex = match.Index + 2;
					endIndex = (i == matches.Count - 1) ? speaker.Length : matches[i + 1].Index;
					endIndex -= 1;

					string expressInfo = speaker.Substring(startIndex, endIndex - startIndex);
					expressions = new List<(string layer, string expression)>();

					expressions = expressInfo.Split(EXPRESS_SEP, StringSplitOptions.RemoveEmptyEntries).Select(x =>
					{
						var parts = x.Trim().Split(EXRPESS_DET_SEP, StringSplitOptions.RemoveEmptyEntries);
						return (parts[0], parts[1]);
					}).ToList();
				}
			}
		}
	}
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
