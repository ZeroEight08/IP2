using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Parallax : MonoBehaviour
{
    public GameObject foreGround;
    public GameObject middleGround;
    public GameObject backGround;
    public float cycleSpeed;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey("d"))
        {
            foreGround.GetComponent<Rigidbody2D>().velocity = new Vector2(cycleSpeed* 1 / 3, 0);
            middleGround.GetComponent<Rigidbody2D>().velocity = new Vector2(cycleSpeed * 2 / 3, 0);
            backGround.GetComponent<Rigidbody2D>().velocity = new Vector2(cycleSpeed, 0);
        }

        if (Input.GetKeyUp("d"))
        {
            foreGround.GetComponent<Rigidbody2D>().velocity = new Vector2(0, 0);
            middleGround.GetComponent<Rigidbody2D>().velocity = new Vector2(0, 0);
            backGround.GetComponent<Rigidbody2D>().velocity = new Vector2(0, 0);
        }

        if (Input.GetKey("a"))
        {
            foreGround.GetComponent<Rigidbody2D>().velocity = new Vector2(-cycleSpeed * 1 / 3, 0);
            middleGround.GetComponent<Rigidbody2D>().velocity = new Vector2(-cycleSpeed * 2 / 3, 0);
            backGround.GetComponent<Rigidbody2D>().velocity = new Vector2(-cycleSpeed, 0);
        }

        if (Input.GetKeyUp("a"))
        {
            foreGround.GetComponent<Rigidbody2D>().velocity = new Vector2(0, 0);
            middleGround.GetComponent<Rigidbody2D>().velocity = new Vector2(0, 0);
            backGround.GetComponent<Rigidbody2D>().velocity = new Vector2(0, 0);
        }

    }
}
