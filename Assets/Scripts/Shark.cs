using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shark : MonoBehaviour
{
    public int health = 100;
    public void TakeDamage(int damage)
    {
        Debug.Log(health);
        Debug.Log(damage);
        health -= damage;
        if (health <= 0)
            Die();
    }
    public void Die()
    {
        if (Audio.Instance != null)
        Audio.Instance.KillSound();
        Destroy(gameObject);
    }

}