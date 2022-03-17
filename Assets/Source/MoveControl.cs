using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveControl : MonoBehaviour
{
    public GameObject axisFrame;
    // Start is called before the first frame update
    void Start()
    {
        axisFrame.transform.parent = this.transform;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
