using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using InterfaceAndInheritables;
using UnityEngine.Events;
using System;
using VFX;
using Player;
using AudioSoundEvents;
using StateMachine.Player;
using Random = UnityEngine.Random;

public class HpBar : MonoBehaviour, IDamageable
{
    [field: SerializeField] public float MaxHealth { get; set; } = 100f;
    [field: SerializeField] public float CurrentHealth { get; set; }
    [field: SerializeField] public bool IsDamageable { get; set; }
    [SerializeField] bool ShowDamageText;
    [SerializeField] float PopUpScale;
    [SerializeField] Color PopupTextColor;
    [SerializeField] List<Droppable> DroppableObjects;
    public UnityEvent onHit;
    [field: SerializeField] public UnityEvent onDeath { get; set; }
    VFXManager vfxManager;
    

    
    PlayerStats PlayerStatistics;
    public bool IsPlayer => PlayerStatistics != null;
    
    private void OnEnable()
    {
        if (!IsPlayer) return;
        PlayerStatistics.OnChangeHp += UpdateHp;
    }
    private void OnDisable()
    {
        if (!IsPlayer) return;
        PlayerStatistics.OnChangeHp -= UpdateHp;
    }
    private void Awake()
    {
        PlayerStatistics = GetComponent<PlayerMonoStateMachine>()?.PlayerStatsSCO;
        CurrentHealth = MaxHealth;
        vfxManager = FindObjectOfType<VFXManager>();
    }

    public void Hit(float Damage, DamageType Type)
    {
        float FinalDmg = Damage;
        if (!IsDamageable || CurrentHealth <= 0) return;

        if (ShowDamageText && vfxManager != null) vfxManager.SpawnDmgPopup(gameObject.transform.position, FinalDmg, PopUpScale, PopupTextColor);

        if (PlayerStatistics == null) CurrentHealth = Mathf.Clamp(CurrentHealth - FinalDmg, 0, MaxHealth);
        else PlayerStatistics.TakeDamage(FinalDmg);

        onHit?.Invoke();
        if (DroppableObjects.Count > 0) DropItems();
        if (CurrentHealth <= 0) onDeath?.Invoke();
    }

    void DropItems()
    {
        for (int i = 0; i < DroppableObjects.Count; i++)
        {
            InstantiateItemOrObject(DroppableObjects[i].Probability, DroppableObjects[i].ItemToDrop);
        }
    }    

    void UpdateHp()
    {
        CurrentHealth = PlayerStatistics.PlayerCurrentHealth;
    }


    #region OPTIONS OF FUNCTIONS TO CALL WHEN THIS DIES
    public void DestroyThis() => Destroy(gameObject);
    

    public void InstantiateItemOrObject(float PercentChance, GameObject ObjToDrop)
    {
        float DropChance = Random.Range(1, 100 + 1); // Added +1 because maxExclusive is like index range. 100 = 100%. Random number between 1 and 100.
        if (DropChance <= PercentChance) Instantiate(ObjToDrop, transform.position + Vector3.up * 1, Quaternion.identity);
        Debug.Log("PERCENT IS " + PercentChance + "  CHOSEN NUMBER IS " +DropChance);
    }
    #endregion

}
