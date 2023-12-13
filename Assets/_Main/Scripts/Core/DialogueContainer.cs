using UnityEngine;
using TMPro;

[System.Serializable]
public class DialogueContainer 
{
    // root is parent of name, content, info
    public GameObject root;

    // character name
    public TMP_Text nameText;

    // dialogue content
    public TMP_Text contentText;

    // additional info(if exist)
    public TMP_Text infoText;
}
