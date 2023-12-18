using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Character
{

    public abstract class Character
    {
        public string name = "";
        public RectTransform root = null;

        public enum CharacterType
        {
            Text,
            Sprite,
            SpriteSheet,
            Live2D,
            Model3D
        }

        public Character(string name)
        {
            this.name = name;
        }

    }

}