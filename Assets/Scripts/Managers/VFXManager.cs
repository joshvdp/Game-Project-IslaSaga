using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
namespace VFX
{
    public class VFXManager : MonoBehaviour
    {
        [Header("-------VFX PREFABS-------")]
        [SerializeField] GameObject DamagePopup;

        public static VFXManager Instance;
        private void Awake()
        {
            if (Instance != null && Instance != this) Destroy(this);
            else Instance = this;
        }

        public void SpawnDmgPopup(Vector3 position, float Dmg)
        {
            GameObject popup = Instantiate(DamagePopup, position, Quaternion.identity);
            popup.GetComponent<TextMeshPro>().text = Dmg.ToString();
        }
    }
}
