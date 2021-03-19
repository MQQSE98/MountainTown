using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class AStarManager : MonoBehaviour
{

    GameObject[] NPCEntity;
    Bounds[] NPCBounds;
    GameObject player;
    Bounds p;

    // Start is called before the first frame update
    void Start() {
        NPCEntity = GameObject.FindGameObjectsWithTag("NPC");
        NPCBounds = new Bounds[NPCEntity.Length];
        player = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void Update() {
        
        p = player.GetComponent<Collider2D>().bounds;
        var guo = new GraphUpdateObject(p);
        AstarPath.active.UpdateGraphs(guo);
        
        for (int i = 0; i < NPCEntity.Length; i++) {
            NPCBounds[i] = NPCEntity[i].GetComponent<Collider2D>().bounds;
            guo = new GraphUpdateObject(NPCBounds[i]);
            AstarPath.active.UpdateGraphs(guo);
        }
        
    }
}
