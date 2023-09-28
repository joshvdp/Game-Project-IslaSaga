using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Interface;
using UnityEngine.Events;
using System;
using VFX;
using AudioSoundEvents;
public class HpBar : MonoBehaviour, IDamageable
{

    public float MaxHealth { get; set; } = 100f;
    public float CurrentHealth { get; set; }
    [field: SerializeField] public UnityEvent onDeath { get; set; }
    public bool IsDamageable { get; set; }

    public UnityEvent onHit;

    VFXManager vfxManager;
    [SerializeField] bool ShowDamageText;
    [SerializeField] float PopUpScale;
    [SerializeField] Color PopupTextColor;

    private void Awake()
    {
        CurrentHealth = MaxHealth;
        vfxManager = FindObjectOfType<VFXManager>();
    }

    public void Hit(float Damage)
    {
        if (!IsDamageable || CurrentHealth <= 0) return;
        if (ShowDamageText && vfxManager != null) vfxManager.SpawnDmgPopup(gameObject.transform.position, Damage, PopUpScale, PopupTextColor);

        CurrentHealth = Mathf.Clamp(CurrentHealth - Damage, 0, MaxHealth);
        onHit?.Invoke();
        
        if (CurrentHealth <= 0) onDeath?.Invoke();
    }

    
}
