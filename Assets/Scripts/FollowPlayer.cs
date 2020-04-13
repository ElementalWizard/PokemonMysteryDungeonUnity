using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPlayer : MonoBehaviour
{

    public GameObject player;
    public Vector3 offSet;
    public FloorCreator fc;
    public int moveX, moveY,Pspeed=1;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(player != null)
        {

            transform.position = new Vector3(player.transform.position.x + offSet.x, player.transform.position.y + offSet.y, offSet.z);
            fc = GameObject.FindGameObjectWithTag("Dungeon").GetComponent<FloorCreator>();
          

        }
    }
}
