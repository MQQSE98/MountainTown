using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gold : MonoBehaviour
{
    public ParticleSystem goldSparkle;
    private GameObject player;

    public int amount;
    public bool magnetIsOn = false;

    public void Start()
    {
        if(this.gameObject.CompareTag("Gold"))
        {
            Instantiate(goldSparkle, transform.position, Quaternion.identity, transform);
        }

        player = GameObject.FindGameObjectWithTag("Player");


        
    }
    public void Update()
    {
        if(magnetIsOn)
        {
            magnet();     

        }
    }

    private void magnet()
    {

        transform.position = Vector3.Lerp(this.transform.position, player.transform.position, 3f * Time.deltaTime);
    }
}
