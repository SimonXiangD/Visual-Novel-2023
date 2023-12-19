using Dialogue;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Character
{

    public abstract class Character
    {
        public string name = "";
        public string displayName = "";
        public RectTransform root = null;
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
        }

        public Coroutine Say( string d ) => Say(new List<string>() { d });

        public Coroutine Say(List<string> d)
        {
            dialogueSystem.showName(name);
            return dialogueSystem.Say( d );

		}

    }

}