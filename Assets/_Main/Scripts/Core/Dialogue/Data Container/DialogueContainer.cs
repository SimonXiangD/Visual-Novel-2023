using UnityEngine;
using TMPro;
using UnityEngine.TextCore.Text;

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

        public void setContentColor( Color c) => contentText.color  = c;
        public void setNameColor(Color c) => nameText.color  = c;
                    
        public void setContentFont(TMP_FontAsset fontAsset) => contentText.font = fontAsset;
        public void setNameFont(TMP_FontAsset fontAsset) => nameText.font = fontAsset;

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