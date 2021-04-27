using UnityEngine;


public class PlayerCombat : Combat
{
    
    /// <summary>
    /// Takes in player and enemy info, detects if any enemies were hit. In future player will need a reference to its weapon to determine
    /// hitRange or the weapon itself will need to alter a hitRange value of the player
    /// </summary>
    /// <param name="player">player game object</param>
    /// <param name="enemies">enemy game object array</param>
    /// <param name="orientation">player hitting orientation</param>
    public void MeleeAttack(GameObject player, MeleeWeapon weapon, GameObject[] enemies, string orientation)
    {
        Debug.LogWarning("In Melee--------------------------------------");
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
            if((player.transform.position - e.transform.position).magnitude <weapon.Range)
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
                    
                    e.GetComponent<Transform>().parent.transform.gameObject.GetComponent<EnemyM>().health -= weapon.Damage;
                    if(e.name == "SanguineSludge_Boss")
                    {
                        e.GetComponent<Transform>().parent.gameObject.GetComponent<BossHeathController>().SetHealth(e.GetComponent<Transform>().parent.transform.gameObject.GetComponent<EnemyM>().health);
                    }
                    if(e.name == "Goop")
                    {
                        e.GetComponent<EnemyCombat>().TakeDamage(); 
                    }
                    Debug.LogWarning("HIT!!!!!!");
                }
            }
        }
        
    }

    public void RangedAttack(GameObject player, RangedWeapon weapon, GameObject[] enemies, string orientation, float holdTimeDelta)
    {
        Debug.Log("in the attack functiin!!!!!!!!!!!!!!!!!!!!");
        float dist = 0;
        if (holdTimeDelta >= 1)
            dist = weapon.Range;
        else
            dist = holdTimeDelta * weapon.Range;


        GameObject arrow = Instantiate(weapon.Projectile, player.transform.position, Quaternion.identity);
        arrow.layer = 1;
        float x = 0;
        float y = 0;
        switch (orientation)
        {
            case "Up":
                arrow.GetComponent<Arrow>().destination = new Vector2(player.transform.position.x, player.transform.position.y + dist);
                arrow.GetComponent<CircleCollider2D>().offset = new Vector2(-.01f, .37f);
                arrow.GetComponent<Arrow>().orientation = 0;
                break;
            case "Down":
                arrow.GetComponent<Arrow>().destination = new Vector2(player.transform.position.x, player.transform.position.y - dist);
                arrow.GetComponent<CircleCollider2D>().offset = new Vector2(.01f, -.37f);
                arrow.GetComponent<Arrow>().orientation = 4;
                break;
            case "Left":
                arrow.GetComponent<Arrow>().destination = new Vector2(player.transform.position.x - dist, player.transform.position.y);
                arrow.GetComponent<CircleCollider2D>().offset = new Vector2(-.3f, -.02f);
                arrow.GetComponent<Arrow>().orientation = 6;
                break;
            case "Right":
                arrow.GetComponent<Arrow>().destination = new Vector2(player.transform.position.x + dist, player.transform.position.y);
                arrow.GetComponent<CircleCollider2D>().offset = new Vector2(.37f, .01f);
                arrow.GetComponent<Arrow>().orientation = 2;
                break;
            case "UpRight":
                y = dist * Mathf.Sin(45 * Mathf.Deg2Rad);
                x = dist * Mathf.Cos(45 * Mathf.Deg2Rad);
                arrow.GetComponent<Arrow>().destination = new Vector2(player.transform.position.x + x, player.transform.position.y + y);
                arrow.GetComponent<CircleCollider2D>().offset = new Vector2(.23f, .25f);
                arrow.GetComponent<Arrow>().orientation = 1;
                break;
            case "DownRight":
                y = dist * Mathf.Sin(315 * Mathf.Deg2Rad);
                x = dist * Mathf.Cos(315 * Mathf.Deg2Rad);
                arrow.GetComponent<Arrow>().destination = new Vector2(player.transform.position.x + x, player.transform.position.y + y);
                arrow.GetComponent<CircleCollider2D>().offset = new Vector2(.27f, -.23f);
                arrow.GetComponent<Arrow>().orientation = 3;
                break;
            case "UpLeft":
                y = dist * Mathf.Sin(135 * Mathf.Deg2Rad);
                x = dist * Mathf.Cos(135 * Mathf.Deg2Rad);
                arrow.GetComponent<Arrow>().destination = new Vector2(player.transform.position.x + x, player.transform.position.y + y);
                arrow.GetComponent<CircleCollider2D>().offset = new Vector2(-.26f, .23f);
                arrow.GetComponent<Arrow>().orientation = 7;
                break;
            case "DownLeft":
                y = dist * Mathf.Sin(225 * Mathf.Deg2Rad);
                x = dist * Mathf.Cos(225 * Mathf.Deg2Rad);
                arrow.GetComponent<Arrow>().destination = new Vector2(player.transform.position.x + x, player.transform.position.y + y);
                arrow.GetComponent<CircleCollider2D>().offset = new Vector2(-.22f, -.26f);
                arrow.GetComponent<Arrow>().orientation = 5;
                break;
        }
    }


}
