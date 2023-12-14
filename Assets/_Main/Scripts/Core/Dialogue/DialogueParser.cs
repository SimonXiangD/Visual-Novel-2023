using Dialogue;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Text.RegularExpressions;
using System;
using UnityEngine.Windows;

public class DialogueParser : MonoBehaviour
{
	public static DialogueParser instance;
	private string dividePattern = @"^(?:([\w|\s]+)\s)?(?:""(.*?)""\s)?(\w+\(.*\)\s)?";

	public void Awake()
	{
		if (instance == null)
		{
			instance = this;
		}
		else DestroyImmediate(gameObject);
	}
	public DIALOGUE_LINE parse(string line)
	{
		(string speaker, string content, string command) = rigLine( line );
		return new DIALOGUE_LINE(speaker, content, command);
	}

	private (string speaker, string content, string command) rigLine(string line)
	{
		string speaker = "";
		string content = "";
		string command = "";

		Debug.Log($"Start Parsing: {line}");
		Match match = Regex.Match(line, dividePattern);

		if (match.Success)
		{
			speaker = match.Groups[1].Value;
			content = match.Groups[2].Value.Replace("\\\"", "\"");
			command = match.Groups[3].Value;
			Debug.Log("Success!");

			Debug.Log("Speaker: " + (string.IsNullOrEmpty(speaker) ? "None" : speaker));
			Debug.Log("Dialogue: " + (string.IsNullOrEmpty(content) ? "None" : content));
			Debug.Log("Command: " + (string.IsNullOrEmpty(command) ? "None" : command));
		}
		else
		{
			Debug.LogError("No match found.");
		}

		return (speaker, content, command);
	}
	
}
