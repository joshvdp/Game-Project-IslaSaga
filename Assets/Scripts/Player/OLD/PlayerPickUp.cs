using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Manager;
namespace Player
{
    public class PlayerPickUp : MonoBehaviour
    {
        [SerializeField] PlayerReferences References;
        [SerializeField] GameObject ItemBeingHeld;
        bool ItemPickedUp = false;

        [SerializeField] float ItemFollowSpeed;
        // Update is called once per frame
        void Update()
        {
                GetInput();

                if (ItemPickedUp)
                {
                    HoldItem();
                }
        }

        void GetInput()
        {
            
            if (Input.GetKeyDown(References.Controls.PickUpKey) && References.PlayerDetectObjCS.MoveableObjectInRange != null && !ItemPickedUp)
            {
                PickUpItem();
            }
            else if (Input.GetKeyDown(References.Controls.PickUpKey) && ItemPickedUp)
            {
                DropItem();
            }
        }

        void PickUpItem()
        {
            ItemBeingHeld = References.PlayerDetectObjCS.MoveableObjectInRange;
            ItemPickedUp = true;
        }
        void HoldItem()
        {
            ItemBeingHeld.transform.position = Vector3.Lerp(ItemBeingHeld.transform.position, References.HoldSpot.position, ItemFollowSpeed * Time.deltaTime);
        }
        public void DropItem()
        {
            ItemPickedUp = false;
            ItemBeingHeld = null;
        }
    }

}
