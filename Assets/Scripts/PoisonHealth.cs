using System;
using System.Collections;
using UnityEngine;

public class PoisonHealth : MonoBehaviour, IHealth
{
    [SerializeField] private int startingHealth = 100;

    private int currentHealth;
    private int amt;
    private int count = 1;

    public event Action<float> OnHPPctChanged = delegate { };
    public event Action OnDied = delegate { };

    private void Start()
    {
        currentHealth = startingHealth;
    }

    public float CurrentHpPct
    {
        get { return (float)currentHealth / (float)startingHealth; }
    }

    public void TakeDamage(int amount)
    {
        amt = amount;
        if (amount <= 0)
            throw new ArgumentOutOfRangeException("Invalid Damage amount specified: " + amount);

        InvokeRepeating("Poison", 0f, 0.5f);
    }

    private void Die()
    {
        OnDied();
        GameObject.Destroy(this.gameObject);
    }

    private void Poison()
    {
        currentHealth -= amt / 4;

        OnHPPctChanged(CurrentHpPct);

        if (CurrentHpPct <= 0)
            Die();
        count++;
        if (count >= 10)
        {
            CancelInvoke();
            count = 1;
        }
    }
}
