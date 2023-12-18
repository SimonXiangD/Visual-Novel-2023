using Dialogue;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace Character {
    [System.Serializable]
    public class CharacterConfigData 
    {
        public string name;
        public string alias;

        public Character.CharacterType characterType;

        public Color nameColor;
        public Color dialogueColor;

        public TMP_FontAsset nameFont;
        public TMP_FontAsset dialogueFont;

        public CharacterConfigData copy()
        {
            CharacterConfigData copyData = new CharacterConfigData();
            copyData.name = name;
            copyData.alias = alias;
            copyData.characterType = characterType;
            copyData.nameColor = copyColor(nameColor);
            copyData.dialogueColor = copyColor(dialogueColor);
            copyData.nameFont = nameFont;
            copyData.dialogueFont = dialogueFont;
            return copyData;
        }

        private Color copyColor(Color ori)
        {
            return new Color(ori.r, ori.g, ori.b, ori.a);
        }

        private static Color defaultColor => DialogueSystem.instance.config.defaultColor;
        private static TMP_FontAsset defaultFont => DialogueSystem.instance.config.defaultFont;

        public static CharacterConfigData Default
        {
            get
            {
                CharacterConfigData config = new CharacterConfigData();

                config.name = "None";
                config.alias = "Wuming";
                config.characterType = Character.CharacterType.Text;
                config.nameColor = defaultColor;
                config.dialogueColor = defaultColor;
                config.nameFont = DialogueSystem.instance.config.defaultFont;
				config.dialogueFont = DialogueSystem.instance.config.defaultFont;

				return config; 
			}
        }

	}
}