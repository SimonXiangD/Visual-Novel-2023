using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TextArchitect
{
    public TextMeshProUGUI tmpro_ui;
    public TextMeshPro tmpro_world;

    // the tmp text we have, maybe from TextMeshProUGUI or TextMeshPro format
    public TMP_Text tmpro => tmpro_ui != null ? tmpro_ui : tmpro_world;

    public string currentText => tmpro.text;

    public string targetText { get; private set; } = "";
    public string preText { get; private set; } = "";

    public string fullTargetText => preText + targetText;

    // 3 choices
    public enum BuildMethod { instant, typeWriter, fade };
    public BuildMethod buildMethod = BuildMethod.typeWriter;

    public Color textColor { get { return tmpro.color; } set { tmpro.color = value; } }

    // speed of typewriter, where we truncate it to interger in the variable charPerCycle
    public float speed { get { return baseSpeed * speedMultiplier; } set { speedMultiplier = value; } }
    private float baseSpeed = 1;
    private float speedMultiplier = 1;

    public int charPerCycle { get { return (baseCharNum) * (speed < 2.0f ? 1 : speed < 2.5 ? 2 : 3); } }
    private int baseCharNum = 1;

    Coroutine buildingProcess;
    bool isBuilding => buildingProcess != null;

    public TextArchitect(TextMeshPro textMeshPro)
	{
        tmpro_world = textMeshPro;
	}

    public TextArchitect( TextMeshProUGUI textMeshPro )
    {
        tmpro_ui = textMeshPro;
    }

    public Coroutine Build(string text)
	{
        preText = "";
        targetText = text;
        Stop();
        return tmpro.StartCoroutine(Building());
	}

    public Coroutine Append( string text )
    {
        preText = tmpro.text;
        targetText = text;
        Stop();
        return tmpro.StartCoroutine(Building());
    }

    void Stop()
	{
        if (!isBuilding) return;
        tmpro.StopCoroutine(buildingProcess);
        buildingProcess = null;
	}

    IEnumerator Building()
	{
        Prepare_Build();
        switch(buildMethod)
		{
            case BuildMethod.typeWriter:
                yield return Build_Typewriter();
                break;
            case BuildMethod.fade:
                yield return Build_Fade();
                break;
		}
        On_Complete();
	}

    void Prepare_Build()
	{
        switch(buildMethod)
		{
            case BuildMethod.instant:
                Prepare_Instant();
                break;
            case BuildMethod.typeWriter:
                Prepare_Typewriter();
                break;
            case BuildMethod.fade:
                Prepare_Fade();
                break;
        }
	}

    void Prepare_Instant()
	{
        tmpro.color = tmpro.color;
        tmpro.text = fullTargetText;
        tmpro.ForceMeshUpdate();
        tmpro.maxVisibleCharacters = tmpro.textInfo.characterCount;
	}
    void Prepare_Typewriter()
	{

	}
    void Prepare_Fade()
	{

	}

    IEnumerator Build_Typewriter()
	{
        yield return null;
	}

    IEnumerator Build_Fade()
	{
        yield return null;
    }

    void On_Complete()
	{
        buildingProcess = null;
	}

}







