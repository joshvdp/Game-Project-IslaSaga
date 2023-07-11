using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Player
{
    public class PlayerPickUp : MonoBehaviour
    {
        [SerializeField] ControlBindings Controls;
        [SerializeField] PlayerDetectObjects PlayerDetectObjCS;
        [SerializeField] Transform HoldSpot;
        [SerializeField] GameObject ItemBeingHeld;
        bool ItemPickedUp = false;

        [SerializeField] float ItemFollowSpeed;
        // Update is called once per frame
        void Update()
        {
            GetInput();

            if (ItemPickedUp) ItemBeingHeld.transform.position = Vector3.Lerp(ItemBeingHeld.transform.position, HoldSpot.position, ItemFollowSpeed * Time.deltaTime);

        }

        void GetInput()
        {
            if (Input.GetKeyDown(Controls.PickUpKey) && PlayerDetectObjCS.MoveableObjectInRange != null && !ItemPickedUp)
            {
                ItemBeingHeld = PlayerDetectObjCS.MoveableObjectInRange;
                ItemPickedUp = true;
            }
            else if (Input.GetKeyDown(Controls.PickUpKey) && ItemPickedUp)
            {
                ItemPickedUp = false;
                ItemBeingHeld = null;
            }
        }

        public void DropItem()
        {
            ItemPickedUp = false;
            ItemBeingHeld = null;
        }
    }

}
