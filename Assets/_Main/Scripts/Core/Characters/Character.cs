using Dialogue;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Character
{

    public abstract class Character
    {
        const string defaultName = "ÎÞÃû";
        public string name = "";
        public string displayName = "";
        public RectTransform root = null;

        public CharacterConfigData config = null;

        private DialogueSystem dialogueSystem => DialogueSystem.instance;

        public enum CharacterType
        {
            Text,
            Sprite,
            SpriteSheet,
            Live2D,
            Model3D
        }

        public Character( string name )
        {
            this.name = name;
            this.displayName = name;
            this.config = CharacterSystem.instance.config.GetConfig( name );
        }

        public Coroutine Say( string d ) => Say(new List<string>() { d });

        public Coroutine Say(List<string> d)
        {
            dialogueSystem.ApplyCharacterConfig(name, config);
            dialogueSystem.showName(displayName);
            return dialogueSystem.Say( d );

		}

    }

}