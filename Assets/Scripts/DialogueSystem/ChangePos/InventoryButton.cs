using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace DialogueSystem
{
    public class InventoryButton : MonoBehaviour
    {
        int index;

        public Button InventoryBtn;


        private void Update()
        {
            Inventory();
        }

        public void Inventory()
        {
            InventoryBtn.onClick.AddListener(prompt);
        }

        private void prompt()
        {
            index = 7;
            DialogueHandler.Instance.EnableDialogue(index);
        }
    }
}

