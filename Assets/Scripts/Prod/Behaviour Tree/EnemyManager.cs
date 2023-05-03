using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    private int hp;

    private void Awake()
    {
        hp = 30;
    }

    public bool TakeHit()
    {
        hp -= 30;
        bool isDead = hp <= 0;
        if (isDead)
        {
            Die();
        }
        return isDead;
    }

    private void Die()
    {
        Destroy(this.gameObject);
    }
}
