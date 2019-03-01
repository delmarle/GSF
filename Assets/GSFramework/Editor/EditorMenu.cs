using UnityEditor;
using UnityEngine;

namespace GSFramework.Editor
{
    public class EditorMenu
    {

        public static void OpenAssetDocumentation()
        {
            Application.OpenURL("https://tinytreegames.com/gsf_documentation/");
        }
        
        public static void OpenDeveloperWebsite()
        {
            Application.OpenURL("https://tinytreegames.com/");
        }
        
        public static void AskQuestionByEmail()
        {
            Application.OpenURL("mailto:delmarle.damien@gmail.com?subject=[GSF-Framework]Question");
        }

        
        public static void PingInProject(string path, string asset = "")
        {

            if (path[path.Length - 1] == '/') path = path.Substring(0, path.Length - 1);
            
            var obj = AssetDatabase.LoadAssetAtPath(path, typeof(Object));
            Selection.activeObject = obj;
            EditorGUIUtility.PingObject(obj);
        }


    }
}


