using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BladeSpin : MonoBehaviour {

    public const float ROTATION_SPEED = 300.0f;
    
    void Update()
    {
        transform.Rotate(Vector3.forward, ROTATION_SPEED * Time.deltaTime);
    }
}
