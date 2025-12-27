using UnityEngine;
using System.Collections.Generic;

public class WeaponDamage : MonoBehaviour
{
    public int damage = 10;

    Health owner;
    Collider2D col;

    HashSet<Health> hitThisSwing = new HashSet<Health>();
    public Health getOwner() => owner;

    void Awake()
    {
        col = GetComponent<Collider2D>();
        col.enabled = false;
    }

    public void SetOwner(Health newOwner)
    {
        owner = newOwner;
    }

    public void BeginAttack()
    {
        hitThisSwing.Clear();
        col.enabled = true;

    }

    public void EndAttack()
    {
        col.enabled = false;
    }


    void OnTriggerEnter2D(Collider2D other)
    {
        {
            
        }
        if (owner == null)
        return;
        Health target =
            other.GetComponentInParent<Health>();

        if (!target) return;
        if (target == owner) return;
        if (target.GetTeam() == owner.GetTeam()) return;
        if (hitThisSwing.Contains(target)) return;

        hitThisSwing.Add(target);
        target.TakeDamage(damage);
        Debug.Log($"{target.GetComponent<Health>().GetTeam()} curretn health: {target.GetComponent<Health>().GetCurrent()}");
    }
}
