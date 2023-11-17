using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using InterfaceAndInheritables;
using UnityEngine.Events;

[RequireComponent(typeof(Collider))]
public class AreaDamageOverTime : MonoBehaviour
{
    public UnityEvent OnInflictDamage;
    [SerializeField] float Damage;
    [SerializeField] float Interval;
    float CurrentTimeInterval;
    private void Update()
    {
        CurrentTimeInterval += CurrentTimeInterval > 0 ? -Time.deltaTime : Interval;
    }

    private void OnTriggerEnter(Collider other)
    {
        other.transform.root.GetComponent<HpBar>()?.Hit(Damage, DamageType.AREA);
    }
    private void OnTriggerStay(Collider other)
    {
        if (CurrentTimeInterval > 0) return;
        other.transform.root.GetComponent<HpBar>()?.Hit(Damage, DamageType.AREA);
        OnInflictDamage?.Invoke();
    }
}
