using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RopeScript : MonoBehaviour
{
    public Player1Code player;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey("d") && player.onRope == true)
        {
            this.gameObject.GetComponent<Rigidbody2D>().angularVelocity += 5;
        }
        if (Input.GetKey("a") && player.onRope == true)
        {
            this.gameObject.GetComponent<Rigidbody2D>().angularVelocity -= 5;
        }
    }
}
