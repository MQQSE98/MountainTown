using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Transactions;
using UnityEngine;

public class HealthManger : MonoBehaviour
{

    private double health;
    private bool dead;
    // Start is called before the first frame update
    void Start()
    {
        this.health = 100;
        this.dead = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (this.health > 100) this.health = 100;
        if (this.health <= 0) this.dead = true;
    }

    public double getHealth()
    {
        return this.health;
    }

    public bool getDead()
    {
        return this.dead;
    }

    public void Heal(double x)
    {
        this.health += x;
    }

    public void takeDamage(double x)
    {
        this.health -= x;
    }
}
