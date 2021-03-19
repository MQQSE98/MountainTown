using UnityEngine;

public abstract class Combat : ScriptableObject
{
    public abstract void MeleeAttack(int attack);

    public abstract void RangedAttack(int attack);

    public abstract void SpellAttack(int attack);

    public abstract void Block(int defense);
}