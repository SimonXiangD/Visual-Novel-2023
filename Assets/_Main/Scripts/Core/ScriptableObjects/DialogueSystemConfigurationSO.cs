using Character;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

[CreateAssetMenu(fileName = "Dialogue config", menuName = "dirnotSystem/Dialogue config")]
public class DialogueSystemConfigurationSO : ScriptableObject
{
	public CharacterConfigSO characterConfig;
    public TMP_FontAsset defaultFont;
	public Color defaultColor;

}
