using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    Rigidbody rb;

    public float boost = 100;
    public float rotationSpeed = 1;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        ProcessBoost();
        ProcessRotation();
    }

    private void ProcessBoost()
    {
        if (Input.GetKey(KeyCode.Space))
        {
            rb.AddRelativeForce(Vector3.up * boost * Time.deltaTime);
        }
    }

    private void ProcessRotation()
    {
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            Rotate(1);
        }
        else if (Input.GetKey(KeyCode.RightArrow))
        {
            Rotate(-1);
        }
    }

    private void Rotate(int dir)
    {
        rb.freezeRotation = true;
        transform.Rotate(dir * Vector3.forward * rotationSpeed * Time.deltaTime);
        rb.freezeRotation = false;
    }
}
