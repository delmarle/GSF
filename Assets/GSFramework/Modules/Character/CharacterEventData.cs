using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GSFramework
{
    public class CharacterEventData 
    {
        public static string CharacterKey = "current_character";
        public static string CharacterHeightKey = "current_character_height";

        public class CharacterData
        {
            public string Id;
            public string DisplayName;
            public string BodyType;
            public float Height;
            public int Level;
            public int XpCurrent;
            public int XpMax;
        }

        #region Events

        public struct LoadLocalCharacter
        {
            public float Height;
            public string Body;
            public Vector3 Position;
            public LoadLocalCharacter(float height,string body, Vector3 position)
            {
                Height = height;
                Body = body;
                Position = position;
            }
        }

        public struct OnLocalCharacterInstantiated
        {
            public Transform PlayerTransform;

            public OnLocalCharacterInstantiated(Transform playerTransform)
            {
                PlayerTransform = playerTransform;
            }
        }

        public struct OpenRemotePanel
        {
            
        }

        public struct LoadRemoteCharacter
        {
            public CharacterData Data;
           
            public LoadRemoteCharacter(CharacterData data)
            {
                Data = data;
            }
        }

        public struct CharacterFindRemoteRequest
        {
            
        }

        public struct CharacterFindRemoteResponse
        {
            public CharacterData[] Characters;

            public CharacterFindRemoteResponse(CharacterData[] chars)
            {
                Characters = chars;
            }
        }

        public struct CharacterCreationRequest
        {
            public float Height;
            public string Body;
            public CharacterCreationRequest(float height,string body)
            {
                Height = height;
                Body = body;
            }
        }

        public struct GetCharacterRequest
        {
			
        }

        public struct GetCharacterResponse
        {
            public bool HasCharacter;
            public string BodyType;
            public float BodyHeight;

            public GetCharacterResponse(bool hasCharacter, string bodyType, float bodyHeight)
            {
                HasCharacter = hasCharacter;
                BodyType = bodyType;
                BodyHeight =bodyHeight;
            }
        }

        public struct CharacterDeleteRequest
        {
			
        }

        public struct CharacterActionRequest
        {
            public string ActionName;

            public CharacterActionRequest(string actionName)
            {
                ActionName = actionName;
            }
        }

        public struct CharacterActionResponse
        {
            public int Level;
            public int CurrentXp;
            public int MaxXp;

            public CharacterActionResponse(int level, int currentXp, int maxXp)
            {
                Level = level;
                CurrentXp = currentXp;
                MaxXp = maxXp;
            }
        }

        public struct CharacterNotCreated
        {
			
        }
        #endregion
	
    }
}


