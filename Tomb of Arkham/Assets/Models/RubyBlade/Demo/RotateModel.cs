using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateModel : MonoBehaviour
{
    public float speed = 1;
    // Update is called once per frame
    void Update()
    {
        gameObject.transform.Rotate(0, 1 * speed, 0);
    }
}
