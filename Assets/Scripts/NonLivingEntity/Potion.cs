using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Potion : Item
{
    public float EffectDuration; //time the potion lasts if IsTimed
    public bool IsTimed; //toggle for potions with timed effects
}