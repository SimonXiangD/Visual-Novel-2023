using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Character;

namespace TEST
{
    public class Test_CharacterSystem : MonoBehaviour
    {
        void Start()
        {
            CharacterSystem.instance.createCharacter("Isary");
            CharacterSystem.instance.createCharacter("Dept");
		}

        void Update()
        {

        }
    }

}