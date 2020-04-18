using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float jumpVelocity;
    public float gravityScale = 1.0f;
    public static float globalGravity = -9.81f;
    bool jumpRequest;
    float movement;
    public float speed; 
    
    void Start()
    {
        jumpRequest = true;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            jumpRequest = true;
        }

        movement = Input.GetAxis("Horizontal");
    }

    private void FixedUpdate()
    {
        GetComponent<Rigidbody>().MovePosition(transform.position + new Vector3(movement * speed * Time.deltaTime, 0, 0));

        if (jumpRequest)
        {
            GetComponent<Rigidbody>().AddForce(Vector3.up * jumpVelocity, ForceMode.Impulse);
            jumpRequest = false;
        }

        Vector3 gravity = globalGravity * gravityScale * Vector3.up;
        GetComponent<Rigidbody>().AddForce(gravity, ForceMode.Acceleration);
    }
}
