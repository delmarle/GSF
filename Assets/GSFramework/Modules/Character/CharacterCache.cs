using System.Collections.Generic;
using UnityEngine;

namespace GSFramework
{
    [System.Serializable]
    public class CharacterEntry
    {
        public string CharacterName;
        public GameObject CharacterPrefab;
    }

    [CreateAssetMenu(fileName = "CharacterDatabase", menuName = "GSF/Character Database", order = 1)]
    public class CharacterCache : ScriptableObject
    {
        public CharacterEntry[] Characters;

        public string CurrentCharacterType;
        public float CurrentCharacterHeight = 1;

        private  Dictionary<string, GameObject> _characterDict = new Dictionary<string, GameObject>();

        public  Dictionary<string, GameObject> CharacterDict {
            get
            {
                if (_characterDict.Count == 0)
                {
                    foreach (var characterEntry in Characters)
                    {
                        _characterDict.Add(characterEntry.CharacterName,characterEntry.CharacterPrefab);
                    }
                }

                return _characterDict;
            }
        }

        
    }
}


