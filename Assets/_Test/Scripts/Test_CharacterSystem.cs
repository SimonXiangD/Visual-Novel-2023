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
            //CharacterSystem.instance.createCharacter("Isary");
            //CharacterSystem.instance.createCharacter("Dept");


            StartCoroutine(Test());

		}

        IEnumerator Test()
        {
			List<string> list = new List<string> {
				"Hello",
				"This is a test",
				"Have a good day!"
			};

            Character.Character Dirnot = CharacterSystem.instance.createCharacter("Dirnot");
            Character.Character Rita = CharacterSystem.instance.createCharacter("Rita");

            yield return Dirnot.Say(list);
            yield return Rita.Say(list);

		}

        void Update()
        {

        }
    }

}