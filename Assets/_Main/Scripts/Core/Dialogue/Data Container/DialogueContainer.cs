using UnityEngine;
using TMPro;

namespace Dialogue
{
    // contain dialogue text boxes
    [System.Serializable]
    public class DialogueContainer
    {
        // root is parent of name, content, info
        public GameObject root;

        public GameObject nameBox;

        // character name
        public TextMeshProUGUI nameText;

        // dialogue content
        public TextMeshProUGUI contentText;

        // additional info(if exist)
        public TextMeshProUGUI infoText;

		public void hideName()
		{
            nameBox.SetActive(false);
		}

        public void showName(string text = "")
        {
            nameBox.SetActive(true);
            if(text.Trim() != string.Empty) nameText.text = text;
            else nameBox.SetActive(false);
        }

	}
}