using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Character
{

    [CreateAssetMenu(fileName = "", menuName = "dirnotSystem/Character config")]
    public class CharacterConfigSO : ScriptableObject
    {
        public CharacterConfigData[] characters;

        public CharacterConfigData GetConfig(string name )
        {
            name = name.ToLower();
            for (int i = 0; i < characters.Length; i++)
            {
                CharacterConfigData character = characters[i];
                if(string.Equals(name, character.name.ToLower())) return character.copy();
            }
            return CharacterConfigData.Default;
        }
    }

}