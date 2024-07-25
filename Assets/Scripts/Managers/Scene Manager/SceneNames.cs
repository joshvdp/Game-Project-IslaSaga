using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Manager
{
    [CreateAssetMenu(fileName = "SceneNames", menuName = "Scene Names")]
    public class SceneNames : ScriptableObject
    {
        public string MainMenu;
        public string InGameUI;
        public string TutorialInGameUI;
        public string LoadingScreen;
        public string Prologue;
        public string Level1;
        public string Level2;
        public string AfterLevel;
        public string EndScene;

    }

}
