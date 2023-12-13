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

    //public string currentText => tmpro.text;

    // newly appended text
    public string targetText { get; private set; } = "";

    // already displayed text
    public string preText { get; private set; } = "";

    // all text
    public string fullTargetText => preText + targetText;

    // 3 choices
    public enum BuildMethod { instant, typeWriter, fade };
    public BuildMethod buildMethod = BuildMethod.typeWriter;

    public Color textColor { get { return tmpro.color; } set { tmpro.color = value; } }

    // speed of typewriter, where we truncate it to interger in the variable charPerCycle
    private float speed { get { return baseSpeed * speedMultiplier; } set { speedMultiplier = value; } }
    private float baseSpeed = 1;
    private float speedMultiplier = 1;

    // use this as true speed seems a better choice
    public float showSpeed = 0.5f;

    private int charPerCycle { get { return (baseCharNum) * (speed < 2.0f ? 1 : speed < 2.5 ? 2 : 3); } }
    private int baseCharNum = 1;

    public bool hurryUp = false;
    public int hurrySpeed = 5;

    // record the building status of text
    Coroutine buildingProcess;
    public bool isBuilding => buildingProcess != null;

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
        buildingProcess = tmpro.StartCoroutine(Building());
        return buildingProcess;
	}

    public Coroutine Append( string text )
    {
        preText = tmpro.text;
        targetText = text;
        Stop();
        buildingProcess = tmpro.StartCoroutine(Building());
        return buildingProcess;
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

    public void ForceComplete()
	{
        switch(buildMethod)
		{
            case BuildMethod.typeWriter:
                tmpro.maxVisibleCharacters = tmpro.textInfo.characterCount;
                break;
            case BuildMethod.fade:
                break; 
        }
        Stop();
        On_Complete();
    }

    void Prepare_Typewriter()
	{
        tmpro.color = tmpro.color;
        tmpro.text = preText;
        tmpro.maxVisibleCharacters = 0;
        tmpro.ForceMeshUpdate();

        if (preText.Length > 0)
		{
            tmpro.maxVisibleCharacters = tmpro.textInfo.characterCount;
		}
        tmpro.text = fullTargetText;
        // without force update, text will not be updated
        tmpro.ForceMeshUpdate();

    }
    void Prepare_Fade()
	{

	}

    IEnumerator Build_Typewriter()
	{
        while(tmpro.maxVisibleCharacters < tmpro.textInfo.characterCount)
		{
            
            tmpro.maxVisibleCharacters += charPerCycle * (hurryUp ? hurrySpeed : 1);
            yield return new WaitForSeconds(0.01f / showSpeed / speed);
        }
    }

    IEnumerator Build_Fade()
	{
        yield return null;
    }

    void On_Complete()
	{
        hurryUp = false;
        buildingProcess = null;
	}

}







