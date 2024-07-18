using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Player.Controls;
using Manager;
public class PlayerParticlesHandler : MonoBehaviour
{
    [SerializeField] ParticleSystem Heal;

    private void OnEnable()
    {
        StartCoroutine(LateSubscribe(0.2f));
       
    }

    private void OnDisable()
    {
        MainManager.Instance.PlayerStatsSCO.OnHpPotionUsed -= PlayHealVFX;
    }


    IEnumerator LateSubscribe(float Delay)
    {
        yield return new WaitForSeconds(Delay);
        MainManager.Instance.PlayerStatsSCO.OnHpPotionUsed += PlayHealVFX;
    }

    public void PlayHealVFX() => Heal.Play();

}
