using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Core
{
    public class DialogueCollider : MonoBehaviour
    {
        [SerializeField] DialogueSCO DialogueToPlay;


        private void OnTriggerEnter(Collider other)
        {

            DialogueUIHandler.Instance.StartDialogue(DialogueToPlay, 0.1f);
            Debug.Log("COLLIDED WITH PLAEYR");
        }

    }
}

