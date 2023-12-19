using Dialogue;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace Character
{

    public class CharacterSystem : MonoBehaviour
    {
        public static CharacterSystem instance;
        private Dictionary<string, Character> characters = new Dictionary<string, Character>();
        public CharacterConfigSO config => DialogueSystem.instance.config.characterConfig ;

        private void Awake()
        {
            if (instance == null)
            {
                instance = this;
            }
            else DestroyImmediate(instance);
        }

        public Character GetCharacter( string name )
        {
            if (characters.ContainsKey(name)) { return characters[name]; }
            return null;
        }

        public CharacterConfigData GetCharacterConfigData( string name )
        {
            return config.GetConfig(name);
        }
       
        public Character createCharacter(string name )
        {
            if( characters.ContainsKey(name) )
            {
                Debug.LogError($"Character {name} already exists!");
                return null;
            }

            CHARACTER_INFO character_info = GetCHARACTER_INFO(name);

            characters[name] = createCharacterFromInfo(character_info);
            Debug.Log("afas");
            return characters[name];

        }

        private CHARACTER_INFO GetCHARACTER_INFO(string name)
        {
            CHARACTER_INFO info = new CHARACTER_INFO();
            info.name = name;
            info.config = config.GetConfig(name);

            return info; 
        }

        private class CHARACTER_INFO
        {
            public string name = "";
            public CharacterConfigData config = null;
        }

        private Character createCharacterFromInfo(CHARACTER_INFO characterInfo)
        {
            CharacterConfigData config = characterInfo.config;
            if(config.characterType == Character.CharacterType.Text)
            {
                return new Character_Text(characterInfo.name);
            }
			if (config.characterType == Character.CharacterType.Sprite || config.characterType == Character.CharacterType.SpriteSheet)
			{
				return new Character_Sprite(characterInfo.name);
			}
			if (config.characterType == Character.CharacterType.Model3D)
			{
				return new Character_Model3D(characterInfo.name);
			}
			if (config.characterType == Character.CharacterType.Live2D)
			{
				return new Character_Live2D(characterInfo.name);
			}
            return null;
		}




	}

}