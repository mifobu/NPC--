using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.Random;

public class NumberOfHits : MonoBehaviour, IHealth
{
    [SerializeField]
    private int healthInHits = 5;

    [SerializeField]
    private float invulnTimeAfterHit = 5f;

    private int hitsRemaining;
    private bool canTakeDmg = true;



    public event Action<float> OnHPPctChanged = delegate (float f) { };
    public event Action OnDied = delegate { };

    public float CurrentHPPct
    {
        get { return (float)hitsRemaining / (float)healthInHits; }
    }

    private void Start()
    {
        hitsRemaining = healthInHits;
    }

    public void TakeDamage(int amt)
    {
        
        if (canTakeDmg)
        {
            //StartCoroutine(Poison());
            StartCoroutine(InvulnTimer());
            

            for (int j = 0; j < 1; j++)
            {
                //Console.WriteLine(rnd.Next(10));//returns random integers < 10
                //var rand = new Random();
                int hits = Range(0, 5);
                hitsRemaining = hitsRemaining - hits;
            }
            hitsRemaining--;
            OnHPPctChanged(CurrentHPPct);
            if (hitsRemaining == 2) {
                hitsRemaining += 3;
            }
            if (hitsRemaining == 4) {
                hitsRemaining += 3;
            }
            if (hitsRemaining <= 0)
                OnDied();
        }
    }

    private IEnumerator InvulnTimer()
    {
        canTakeDmg = false;
        yield return new WaitForSeconds(invulnTimeAfterHit);
        canTakeDmg = true;
    }
    //private IEnumerator Poison() {
        //canTakeDmg = false;
        //hitsRemaining--;
        //yield return new WaitForSeconds(invulnTimeAfterHit);
        //canTakeDmg = true;
    //}
}
