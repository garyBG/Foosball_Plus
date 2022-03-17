using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CullFix : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    { 
        Mesh mesh = gameObject.GetComponent<MeshFilter>().mesh;
        Bounds meshBounds = new Bounds(Vector3.zero, new Vector3(100f, 100f, 100f));
        mesh.bounds = meshBounds;

        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
