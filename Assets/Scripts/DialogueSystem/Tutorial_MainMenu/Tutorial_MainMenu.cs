using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace DialogueSystem
{
    public enum TutorialPlatformType
    {
        PC,
        Mobile
    }
    public class Tutorial_MainMenu : MonoBehaviour
    {

        int tutorialIndex;
        

        public TutorialPlatformType PlatformType;
        public Button Tutorial;


        private void Update()
        {
            TutorialButton();
        }

        public void TutorialButton()
        {
            Tutorial.onClick.AddListener(prompt);
        }

        private void prompt()
        {
            switch (PlatformType)
            {
                case TutorialPlatformType.PC:
                    tutorialIndex = 0;
                    TutorialHandler.Instance.EnableDialogue(tutorialIndex);
                    break;
                case TutorialPlatformType.Mobile:
                    tutorialIndex = 1;
                    TutorialHandler.Instance.EnableDialogue(tutorialIndex);
                    break;
            }
        }
    }
}
