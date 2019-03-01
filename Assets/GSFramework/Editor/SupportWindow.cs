//
//Icons from: http://www.fatcow.com/free-icons
//
using UnityEngine;
using UnityEditor;

namespace GSFramework.Editor
{
    public class SupportWindow : EditorWindow
    {
        private const string Version = "GSF Version 0.82.1";
        private const string EditorAssetsPath = "Assets/GSFramework/Editor/Assets/";
        private const string StoreAdress = "content/99401";
        private const string WindowName = "Toolbar";
        private const string MenuPath = "Tools/Gamespark Framework/Open Toolbar";
       
        
        
        GUIStyle _publisherNameStyle;

        GUIStyle _toolBarStyle;
        GUILayoutOption _toolbarHeight;

       
        GUIContent[] _toolbarOptions;
        int _toolBarIndex;
        GUIStyle _greyText;
        GUIStyle _centeredVersionLabel;
        GUIStyle _reviewBanner;
        GUILayoutOption _bannerHeight;

        bool _stylesNotLoaded = true;

        [MenuItem(MenuPath)]
        static void ShowWindow()
        {
            SupportWindow myWindow = GetWindow<SupportWindow>(WindowName);
            myWindow.minSize = new Vector2(300, 400);
            myWindow.maxSize = myWindow.minSize;
            myWindow.titleContent = new GUIContent(WindowName);
            myWindow.Show();
        }

        private void OnEnable()
        {
            _toolbarOptions = new GUIContent[2];
            _toolbarOptions[0] = new GUIContent("<size=14><b> Tools</b></size>\n <size=11>Utilities to simplify \n your workflow.</size>",GetEditorTexture("cog"), "");
            _toolbarOptions[1] = new GUIContent("<size=14><b> Links</b></size>\n <size=11>Documentation \n and support.</size>", GetEditorTexture("link"), "");

            _toolbarHeight = GUILayout.Height(50);

            _bannerHeight = GUILayout.Height(30);
        }

        void LoadStyles()
        {
            _publisherNameStyle = new GUIStyle(EditorStyles.label)
            {
                alignment = TextAnchor.MiddleLeft,
                richText = true
            };

            _toolBarStyle = new GUIStyle("LargeButtonMid")
            {
                alignment = TextAnchor.MiddleLeft,
                richText = true
            };

            _greyText = new GUIStyle(EditorStyles.centeredGreyMiniLabel)
            {
                alignment = TextAnchor.MiddleLeft
            };

            _centeredVersionLabel = new GUIStyle(EditorStyles.centeredGreyMiniLabel)
            {
                alignment = TextAnchor.MiddleCenter,
            };

            _reviewBanner = new GUIStyle("TL SelectionButton")
            {
                alignment = TextAnchor.MiddleCenter,
                richText = true
            };

            _stylesNotLoaded = false;
        }

        void OnGUI()
        {
            if (_stylesNotLoaded) LoadStyles();

            EditorGUILayout.Space();
            GUILayout.Label(new GUIContent("<size=20><b><color=#666666>GSF Framework</color></b></size>"), _publisherNameStyle);
            EditorGUILayout.Space();

            _toolBarIndex = GUILayout.Toolbar(_toolBarIndex, _toolbarOptions, _toolBarStyle, _toolbarHeight);
           
            switch (_toolBarIndex)
            {
                case 0:
                    EditorGUILayout.Space();
                    if (ButtonPressed("Configure Settings","open_save"))
                        EditorMenu.PingInProject("Assets/GameSparks/Resources");
                    EditorGUILayout.LabelField(" Add api key here.", _greyText);

                    EditorGUILayout.Space();
                    if (ButtonPressed("Go to Scenes","unity"))
                        EditorMenu.PingInProject("Assets/GSFramework/_Demo/Scenes");
                    EditorGUILayout.LabelField(" Select the scenes folder.", _greyText);
                    break;

                case 1:
                    EditorGUILayout.Space();
                    if (ButtonPressed("Send an email","mail"))
                        EditorMenu.AskQuestionByEmail();
                    EditorGUILayout.LabelField(" Ask any question here.", _greyText);

                    EditorGUILayout.Space();
                    if (ButtonPressed("Documentation","docs"))
                        EditorMenu.OpenAssetDocumentation();
                    EditorGUILayout.LabelField(" All modules explained.", _greyText);

                    EditorGUILayout.Space();
                    if (ButtonPressed("Developer Website","tiny_tree"))
                        EditorMenu.OpenDeveloperWebsite();
                    EditorGUILayout.LabelField(" Games & assets made.", _greyText);
                    break;
            }
     

           

            GUILayout.FlexibleSpace();
            EditorGUILayout.LabelField(new GUIContent(Version), _centeredVersionLabel);
            EditorGUILayout.Space();
            if (GUILayout.Button(new GUIContent("<size=11> Please consider leaving us a review.</size>",GetEditorTexture("star"), ""), _reviewBanner, _bannerHeight))
                UnityEditorInternal.AssetStore.Open(StoreAdress);
        }
        
        #region Utilities

        private Texture2D GetEditorTexture(string textureName)
        {
            return EditorGUIUtility.Load(EditorAssetsPath + textureName + ".png") as Texture2D;
        }

        private bool ButtonPressed(string buttonName, string buttonIcon = null)
        {
            var buttonContent = new GUIContent("   "+buttonName,GetEditorTexture(buttonIcon));
            
            return GUILayout.Button(buttonContent, EditorStyles.label);
        }

        #endregion
    }
}