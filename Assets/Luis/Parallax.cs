using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Parallax : MonoBehaviour
{
    private float length, pos0;
    public GameObject cam;
    public float parallaxEffect;

    void Start()
    {
        pos0 = transform.position.x;
        length = GetComponent<SpriteRenderer>().bounds.size.x;
    }

    void FixedUpdate()
    {
        float temp = cam.transform.position.x * (1-parallaxEffect);
        float dist = cam.transform.position.x * parallaxEffect;

        transform.position = new Vector3(pos0 + dist, transform.position.y, transform.position.z);

        if (temp > pos0 + length) pos0 += length;
        else if (temp < pos0 - length) pos0 -= length;
    }
}
