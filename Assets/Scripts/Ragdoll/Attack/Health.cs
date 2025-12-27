using UnityEngine;
using System;

public class Health : MonoBehaviour, IDamageable
{
    public Team team;
    public int maxHealth = 100;
    int current;

    public event Action<int, int> OnHealthChanged;

    void Awake()
    {
        current = maxHealth;
        OnHealthChanged?.Invoke(current, maxHealth);
    }

    public Team GetTeam() => team;
    public int GetCurrent() => current;

    public void TakeDamage(int dmg)
    {
        current -= dmg;
        current = Mathf.Clamp(current, 0, maxHealth);

        OnHealthChanged?.Invoke(current, maxHealth);

        Debug.Log($"{name} took {dmg} damage");

        if (current <= 0)
            Die();
    }

    public void Heal(int heal)
    {
        
        current += heal;
        if (current > maxHealth)
        {
            current = maxHealth;
        }
    }

    void Die()
    {
        if(team == Team.Enemy)
        {
            Destroy(gameObject);
        }else
        {
            gameObject.SetActive(false);
            // GameObject.FindGameObjectWithTag("GameOver").SetActive(true);
        }
    }
}
