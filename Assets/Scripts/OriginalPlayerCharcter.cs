using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OriginalPlayerCharcter : MonoBehaviour
{
    //private = camelCase public = PascalCase
    [SerializeField]
    private int lives = 3;

    [SerializeField]
    private string name = "Hector";
    //if they both are the same type use commas
    [SerializeField]
    private float jumpHeight = 5, speed = 5;

    private bool hasKey;

    private bool isOnGround;

    private Rigidbody2D rigidbody2DInstance;
    private float horizontalInput;

    // Use this for initialization
    void Start()
    {
        //have to initialize our component
        rigidbody2DInstance = GetComponent<Rigidbody2D>(); //Get component get the first object in that name and place it in there to use     
        rigidbody2DInstance.gravityScale = 5;
    }

    // Update is called once per frame
    private void Update()
    {
        //transform.Translate(0,-.02f,0); don't use translate 'cause we using physics
        GetInput();
        Move();
    }

    private void GetInput()
    {
        horizontalInput = Input.GetAxis("Horizontal");
    }
    private void Move()
    {
        rigidbody2DInstance.velocity = new Vector2(horizontalInput, 0);
    }
}
