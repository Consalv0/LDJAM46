using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coal : MonoBehaviour
{
    GameObject fireWall;
    // Start is called before the first frame update
    void Start()
    {
        fireWall = GameObject.Find("FireWall").transform.Find("Wall").gameObject;
    }

    // 7 unidades de distancia
    void Update()
    {
        
    }
}
