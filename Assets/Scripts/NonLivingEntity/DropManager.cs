using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropManager : MonoBehaviour
{

    double dropChance;
    GameObject[] dropList;

    public DropManager(double dropChance, GameObject[] dropList)
    {
        this.dropChance = dropChance;
        this.dropList = dropList;
    }
    // Start is called before the first frame update
    void Start()
    {
        //this.dropChance;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
