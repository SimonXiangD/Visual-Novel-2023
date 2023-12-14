using System.Collections;
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
    private int preTextLength => preText.Length;

    // all text
    public string fullTargetText => preText + targetText;
    private int fullTargetTextLength => fullTargetText.Length;


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
    public int hurrySpeed = 8;

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

    public void Stop()
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
                tmpro.ForceMeshUpdate();
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
        tmpro.text = preText;
        if(preTextLength > 0)
		{
            tmpro.ForceMeshUpdate();
        }
        tmpro.text += targetText;
        tmpro.maxVisibleCharacters = int.MaxValue;

        tmpro.ForceMeshUpdate();
        Canvas.ForceUpdateCanvases();

        TMP_TextInfo textInfo = tmpro.textInfo;

        for (int i = 0; i < fullTargetTextLength; i++)
		{
            TMP_CharacterInfo charInfo = textInfo.characterInfo[i];
            if (!charInfo.isVisible) continue;

            int vertexIndex = charInfo.vertexIndex;
            for(int k = 0; k < 4; k++)
			{
                textInfo.meshInfo[charInfo.materialReferenceIndex].colors32[vertexIndex + k].a = i < preTextLength ? byte.MaxValue : byte.MinValue;
			}
        }
        tmpro.UpdateVertexData(TMP_VertexDataUpdateFlags.Colors32);

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
        
        int minRange = preTextLength;
        int maxRange = minRange + 1;
        float fadeSpeed = (hurryUp ? hurrySpeed : 1) * 32;
        TMP_TextInfo textInfo = tmpro.textInfo;
        //Debug.Log("start building!");
        //Debug.Log("text is : " );
        //Debug.Log(tmpro.text);
        while (true)
		{
            for(int i = minRange; i < maxRange; i++)
			{
                TMP_CharacterInfo charInfo = textInfo.characterInfo[i];
                if (!charInfo.isVisible)
                {
                    minRange++;
                    continue;
                }

                int vertexIndex = charInfo.vertexIndex;
                bool flag = true;
                for (int k = 0; k < 4; k++)
                {
                    float val = textInfo.meshInfo[0].colors32[vertexIndex + k].a + fadeSpeed;
                    textInfo.meshInfo[0].colors32[vertexIndex + k].a = (
                        val >= 255 ? byte.MaxValue : ((byte)val));
                    if(val < 255)
					{
                        flag = false;
					}
                }
                if (flag) minRange++;
            }
            tmpro.UpdateVertexData(TMP_VertexDataUpdateFlags.Colors32);

            if (minRange >= maxRange)
			{
                maxRange++;
			}
            if (maxRange > textInfo.characterCount)
			{
                break;
			}
            yield return new WaitForEndOfFrame();
            //yield return new WaitForSeconds(0.0001f / showSpeed  / speed);
            //yield return new WaitForSeconds(100 * 0.01f / showSpeed / (256 / fadeSpeed) / speed);
        }
    }

    void On_Complete()
	{
        hurryUp = false;
        buildingProcess = null;
	}

}







