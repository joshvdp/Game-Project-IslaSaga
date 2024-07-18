using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Core
{
    [CreateAssetMenu(fileName = "Dialogue", menuName = "Dialogue")]
    public class DialogueSCO : ScriptableObject
    {
        public NameStringAndDialogueString[] DialogueLines;
    }

    [Serializable]
    public class NameStringAndDialogueString
    {
        public string Name;
        [TextAreaAttribute] public string Dialogue;
    }
}
