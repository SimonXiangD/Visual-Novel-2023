using UnityEngine;
using TMPro;

namespace Dialogue
{

    [System.Serializable]
    public class DialogueContainer
    {
        // root is parent of name, content, info
        public GameObject root;

        // character name
        public TextMeshProUGUI nameText;

        // dialogue content
        public TextMeshProUGUI contentText;

        // additional info(if exist)
        public TextMeshProUGUI infoText;
    }
}