using System;
using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using UnityEngine;

public class DL_COMMAND
{
	public struct cmdStruct
	{
		public string name;
		public string[] args;
		public string originStr;
		public bool wait; // whether async , if wait is true then add yield return before it when running command
	}

	string command;
	public cmdStruct[] cmdStructArr;

	void showCmdInfo(cmdStruct cmdInfo)
	{
		Debug.Log(cmdInfo.name);
		foreach (string s in cmdInfo.args)
		{
			Debug.Log(s);
		}
		Debug.Log(cmdInfo.originStr);
		Debug.Log(cmdInfo.wait);
	}

	public void show()
	{
		Debug.Log(command);
		foreach (var cmdInfo in cmdStructArr)
		{
			showCmdInfo(cmdInfo);
		}
	}


	//private string dividePattern = @"^(?:\[.*\])?(?:[^\(\)]*)\((?:.*)\)";
	//private string dividePattern = @"^(?:\[.*\])?(?:[^\(\)]+)\((?:.*)\)\s*";
	private string dividePattern = @"^(\[(?<prefix>.*?)\])?(?<name>.+?)\((?<arguments>.*?)\)$";
	private string WAIT_ID = "wait";


	const char CMD_SEP = ',';
	const char ARG_SEP = '-';

	public DL_COMMAND(string command)
	{
		command = command.Trim();
		this.command = command;
		//Debug.Log(command);
		List<cmdStruct> list = new List<cmdStruct>();
		string[] cmds = command.Split(CMD_SEP, StringSplitOptions.RemoveEmptyEntries);
		foreach (string s in cmds)
		{
			//Debug.Log(s);
			string cmd = s.Trim();

			if (cmd.Length <= 0) continue;

			cmdStruct cmdInfo = new cmdStruct();

			cmdInfo.originStr = cmd;
			cmdInfo.wait = false;

			string prefix = "";
			string cmdName = "";
			string cmdArgs = "";

			//Debug.Log($"Start Parsing: {line}");
			Match match = Regex.Match(cmd, dividePattern);

			if (match.Success)
			{
				prefix = match.Groups["prefix"].Value;
				cmdName = match.Groups["name"].Value;
				cmdArgs = match.Groups["arguments"].Value.Replace("\\\"", "\"");

				//Debug.Log(prefix);
				//Debug.Log(cmdName);
				//Debug.Log(cmdArgs);

				if (prefix.Length > 0)
				{
					if (prefix.ToLower().StartsWith(WAIT_ID))
					{
						cmdInfo.wait = true;
					}
				}

				cmdInfo.name = cmdName;

				string[] cmdArgStrs = cmdArgs.Split(ARG_SEP, StringSplitOptions.RemoveEmptyEntries);
				for (int i = 0; i < cmdArgStrs.Length; i++)
				{
					cmdArgStrs[i] = cmdArgStrs[i].Trim();
				}

				cmdInfo.args = cmdArgStrs;

			}
			else
			{
				Debug.LogError($"No match found for command {s}");
			}

			list.Add(cmdInfo);
		}

		this.cmdStructArr = list.ToArray();

	}

}
