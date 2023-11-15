using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace VFX
{
    public class PlayerVFXCaller : MonoBehaviour
    {
        public void Heal()
        {
            Debug.Log("WORK");
            PlayerVFXHandler.Regen?.Invoke();
        }
    }
}

