using UnityEngine;

[CreateAssetMenu(menuName = "Player/Combat")]
public class PlayerCombat : Combat
{
    public override void MeleeAttack(int attack) { }

    public override void RangedAttack(int attack) { }

    public override void SpellAttack(int attack) { }

    public override void Block(int defense) { }
}