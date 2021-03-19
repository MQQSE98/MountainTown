using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Combat/PlayerCombat")]
public class PlayerCombat : Combat
{
    
    /// <summary>
    /// Takes in player and enemy info, detects if any enemies were hit. In future player will need a reference to its weapon to determine
    /// hitRange or the weapon itself will need to alter a hitRange value of the player
    /// </summary>
    /// <param name="player">player game object</param>
    /// <param name="enemies">enemy game object array</param>
    /// <param name="orientation">player hitting orientation</param>
    public void MeleeAttack(GameObject player, GameObject[] enemies, string orientation)
    {
        foreach(GameObject e in enemies)
        {
            float angle = Mathf.Rad2Deg * Mathf.Atan2((e.transform.position.y - player.transform.position.y)
                    ,(e.transform.position.x - player.transform.position.x));
            //float angle = Vector2.Angle(player.transform.position, enemy.transform.position);
            if (angle < 0)
            {
                angle += Mathf.Abs(angle) * 2;
                angle = 180-angle+180;
            }    
            if((player.transform.position - e.transform.position).magnitude < player.GetComponent<PlayerM>().HitRange)
            {
            bool hit = false;
                switch(orientation)
                {
                    case "Up":
                        if (angle > 55f && angle < 125f)
                            hit = true;  
                        break;
                    case "Down":
                        if (angle > 235f && angle < 305f)
                            hit = true;
                        break;
                    case "Left":
                        if (angle > 145f && angle < 215f)
                            hit = true;
                        break;
                    case "Right":
                        if ((angle > 325f && angle < 360f) || (angle > 0 && angle < 35))
                            hit = true;
                        break;
                    case "UpRight":
                        if (angle > 10f && angle < 80f)
                            hit = true;
                        break;
                    case "DownRight":
                        if (angle > 280f && angle < 350f)
                            hit = true;
                        break;
                    case "UpLeft":
                        if (angle > 100f && angle < 170f)
                            hit = true;
                        break;
                    case "DownLeft":
                        if (angle > 190f && angle < 260f)
                            hit = true;
                        break;
                }
                if (hit == true)
                {
                    e.GetComponent<EnemyM>().health -= 10;
                }
            }
        }
        
    }

    public void RangedAttack(GameObject arrow, GameObject[] enemies)
    {

    }


}
