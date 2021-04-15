using System.Collections;
using UnityEngine;
 //test
public class EnemyCombat : Combat
{
    private SpriteRenderer sr;
    private PlayerManager playerManager;

    private void Start()
    {
        playerManager = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerManager>();
        sr = gameObject.GetComponent<Transform>().GetComponentInChildren<SpriteRenderer>();
    }


    public void MeleeAttack(GameObject enemy, GameObject player)
    {
       if ((player.transform.position - enemy.transform.position).magnitude < enemy.GetComponent<EnemyM>().HitRange)
        {
            playerManager.TakeDamage(1);
        }
    }
    public void TakeDamage()
    {
        Color takeDamageColor = new Color(0.7f, 1, 0.3f, 1);
        // Tints the sprite red and fades back to the origin color after a delay of 1 second
        StartCoroutine(DamageEffectSequence(sr, takeDamageColor , 1, 1));
    }

    IEnumerator DamageEffectSequence(SpriteRenderer sr, Color dmgColor, float duration, float delay)
    {
        // save origin color
        Color originColor = sr.color;

        // tint the sprite with damage color
        sr.color = dmgColor;

        // you can delay the animation
        yield return new WaitForSeconds(delay);

        // lerp animation with given duration in seconds
        for (float t = 0; t < 1.0f; t += Time.deltaTime / duration)
        {
            sr.color = Color.Lerp(dmgColor, originColor, t);

            yield return null;
        }

        // restore origin color
        sr.color = originColor;
    }
}
