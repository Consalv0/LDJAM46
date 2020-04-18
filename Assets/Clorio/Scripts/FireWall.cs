using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class FireWall : MonoBehaviour
{

    public float burnModifier;
    public float pacifyModifier;

    public bool burn;
    public bool pacify;
    void Start()
    {
        
        burn = false;
        pacify = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (burn)
        {
            transform.parent.localScale = new Vector3(transform.parent.localScale.x + burnModifier, transform.parent.localScale.y, transform.parent.localScale.z);
            burn = false;
        }

        if (pacify)
        {
            transform.parent.localScale = new Vector3(transform.parent.localScale.x - pacifyModifier, transform.parent.localScale.y, transform.parent.localScale.z);
            pacify = false;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            Destroy(other.gameObject);
            //add
        } 
        else if (other.tag == "Platform")
        {
            Debug.Log("Plat");
            if (other.GetComponent<Platform>().isIce)
            {
                pacify = true;
            }
            Destroy(other.gameObject.transform.parent.gameObject);
        }
        else if (other.tag == "Coal")
        {
            Destroy(other.gameObject);
            burn = true;
            
        }
    }
}
