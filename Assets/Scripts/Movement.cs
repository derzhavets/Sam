using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    Rigidbody rb;
    AudioSource audioSource;

    public ParticleSystem thrustParticle;
    public ParticleSystem leftThrustParticles;
    public ParticleSystem rightThrestParticles;

    public float boost = 100;
    public float rotationSpeed = 1;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        audioSource = GetComponent<AudioSource>();
    }

    void Update()
    {
        ProcessThrust();
        ProcessRotation();
    }

    private void ProcessThrust()
    {
        if (Input.GetKey(KeyCode.Space))
            StartThrusting();
        else
            StopThrusting();
    }

    private void StartThrusting()
    {
        rb.AddRelativeForce(Vector3.up * boost * Time.deltaTime);

        if (!audioSource.isPlaying)
            audioSource.Play();

        if (!thrustParticle.isPlaying)
            thrustParticle.Play();
    }

    private void StopThrusting()
    {
        if (audioSource.isPlaying)
            audioSource.Stop();

        if (thrustParticle.isPlaying)
            thrustParticle.Stop();
    }

    private void ProcessRotation()
    {
        if (Input.GetKey(KeyCode.LeftArrow))
            RotateLeft();
        else if (Input.GetKey(KeyCode.RightArrow))
            RotateRight();
        else
            StopRotation();
    }

    private void RotateLeft()
    {
        Rotate(1);

        if (!rightThrestParticles.isPlaying)
            rightThrestParticles.Play();
    }

    private void RotateRight()
    {
        Rotate(-1);

        if (!leftThrustParticles.isPlaying)
            leftThrustParticles.Play();
    }

    private void StopRotation()
    {
        leftThrustParticles.Stop();
        rightThrestParticles.Stop();
    }

    private void Rotate(int dir)
    {
        rb.freezeRotation = true;
        transform.Rotate(dir * Vector3.forward * rotationSpeed * Time.deltaTime);
        rb.freezeRotation = false;
    }
}
