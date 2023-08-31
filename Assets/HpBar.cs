using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Interface;
using UnityEngine.Events;
using System;
using VFX;
public class HpBar : MonoBehaviour, IDamageable
{

    public float MaxHealth { get; set; } = 100f;
    public float CurrentHealth { get; set; }
    [field: SerializeField] public UnityEvent onDeath { get; set; }
    public bool IsDamageable { get; set; }

    public bool ShowDamageText;
    VFXManager vfxManager;

    public UnityEvent onHit;

    private void Awake()
    {
        CurrentHealth = MaxHealth;
        vfxManager = FindObjectOfType<VFXManager>();
    }
    public void Death()
    {
        onDeath?.Invoke();
    }

    public void Hit(float Damage)
    {
        if (!IsDamageable) return;

        if (ShowDamageText && vfxManager != null) vfxManager.SpawnDmgPopup(transform.position, Damage);
        CurrentHealth = Mathf.Clamp(CurrentHealth - Damage, 0, MaxHealth);
        onHit?.Invoke();
        Debug.Log("FIERY DAMAGED " + Damage + " CURRENT HP: " + CurrentHealth);

        if (CurrentHealth <= 0) Death();
    }

    
}
