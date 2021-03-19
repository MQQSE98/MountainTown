///<summary>
/// Contains all persistent skills of the player.
///</summary>

// consider using RangeFloat and/or [MinMaxRange(x,y)] for certain values

using UnityEngine;

[CreateAssetMenu(menuName = "Player/Skills")]
public class Skills : ScriptableObject 
{
    public int SkillPoints;
    public int Awareness;
}