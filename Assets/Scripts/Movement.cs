using UnityEngine.InputSystem;
using UnityEngine;
using System;

public class Movement : MonoBehaviour
{
    [SerializeField] InputAction thrust;
    [SerializeField] InputAction rotation;
    [SerializeField] float rotationForce = 10f;
    [SerializeField] float thrustForce = 10f;
    [SerializeField] AudioClip mainEngine;
    [SerializeField] ParticleSystem mainEngineParticles;
    [SerializeField] ParticleSystem leftThrusterParticles;
    [SerializeField] ParticleSystem rightThrusterParticles;

    AudioSource audioSource;
    Rigidbody rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        audioSource = GetComponent<AudioSource>();
    }

    private void OnEnable()
    {
        thrust.Enable();
        rotation.Enable();
    }

    private void FixedUpdate()
    {
        ApplyThrust();
        ApplyRotation();
    }

    private void ApplyThrust()
    {
        if (thrust.IsPressed())
        {
            rb.AddRelativeForce(Vector3.up * thrustForce * Time.fixedDeltaTime);

            if (!audioSource.isPlaying)
            {
                audioSource.PlayOneShot(mainEngine);
            }
            if (!mainEngineParticles.isPlaying)
            {
                mainEngineParticles.Play();
            }    
        }
        else
        {
            if (audioSource.isPlaying)
            {
                audioSource.Stop();
            }
            if (mainEngineParticles.isPlaying)
            {
                mainEngineParticles.Stop();
            }
        }
    }
    private void ApplyRotation()
    {
        float rotationInput = rotation.ReadValue<float>();
        if (rotationInput < 0)
        {
            RotationForce(rotationForce);
            if (!rightThrusterParticles.isPlaying)
            {
                leftThrusterParticles.Stop();
                rightThrusterParticles.Play();
            }
        }
        else if (rotationInput > 0)
        {
            RotationForce(-rotationForce);
            if (!leftThrusterParticles.isPlaying)
            {
                rightThrusterParticles.Stop();
                leftThrusterParticles.Play();
            }
        }
        else
        {
            rightThrusterParticles.Stop();
            leftThrusterParticles.Stop();
        }
    }

    private void RotationForce(float rotationSpeed)
    {
        rb.freezeRotation = true;
        transform.Rotate(Vector3.forward * rotationSpeed * Time.fixedDeltaTime);
        rb.freezeRotation = false;
    }
}
