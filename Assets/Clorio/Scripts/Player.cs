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

    public bool isJumping;

    public float throwForce;

    public int wait;
    
    void Start()
    {
        jumpRequest = true;
        isJumping = false;
    }

    void Update()
    {
        if (wait == 2)
        {
            transform.Find("ShoveCollider").GetComponent<BoxCollider>().enabled = false;
        }

        if (Input.GetKeyDown(KeyCode.Space) && !isJumping)
        {
            jumpRequest = true;
            isJumping = true;
        }

        if (Input.GetKeyDown(KeyCode.W) && !transform.Find("ShoveCollider").GetComponent<BoxCollider>().enabled) {
            transform.Find("ShoveCollider").GetComponent<BoxCollider>().enabled = true;
            wait = 0;
        }

        movement = Input.GetAxis("Horizontal");

        if (movement < 0)
        {
            transform.Find("ShoveCollider").transform.localPosition = Vector3.left;
        }
        else if (movement > 0)
        {
            transform.Find("ShoveCollider").transform.localPosition = Vector3.right;
        }

        
    }

    private void FixedUpdate()
    {

        wait += 1;
        GetComponent<Rigidbody>().MovePosition(transform.position + new Vector3(movement * speed * Time.deltaTime, 0, 0));

        if (jumpRequest)
        {
            GetComponent<Rigidbody>().AddForce(Vector3.up * jumpVelocity, ForceMode.Impulse);
            jumpRequest = false;
        }

        Vector3 gravity = globalGravity * gravityScale * Vector3.up;
        GetComponent<Rigidbody>().AddForce(gravity, ForceMode.Acceleration);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Coal")
        {
            other.GetComponent<Rigidbody>().AddForce(Vector3.up * throwForce * 1.5f + Vector3.left * throwForce, ForceMode.Impulse);
            transform.Find("ShoveCollider").GetComponent<BoxCollider>().enabled = false;
        }
    }
}
