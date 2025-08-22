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
            StartThrusting();
        }
        else
        {
            StopThrust();
        }
    }

    private void StartThrusting()
    {
        rb.AddRelativeForce(thrustForce * Time.fixedDeltaTime * Vector3.up);

        if (!audioSource.isPlaying)
        {
            audioSource.PlayOneShot(mainEngine);
        }
        if (!mainEngineParticles.isPlaying)
        {
            mainEngineParticles.Play();
        }
    }
    private void StopThrust()
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

    private void ApplyRotation()
    {
        float rotationInput = rotation.ReadValue<float>();
        if (rotationInput < 0)
        {
            RightThrust();
        }
        else if (rotationInput > 0)
        {
            LeftThrust();
        }
        else
        {
            rightThrusterParticles.Stop();
            leftThrusterParticles.Stop();
        }
    }

    private void RightThrust()
    {
        RotationForce(rotationForce);
        if (!rightThrusterParticles.isPlaying)
        {
            leftThrusterParticles.Stop();
            rightThrusterParticles.Play();
        }
    }

    private void LeftThrust()
    {
        RotationForce(-rotationForce);
        if (!leftThrusterParticles.isPlaying)
        {
            rightThrusterParticles.Stop();
            leftThrusterParticles.Play();
        }
    }

    private void RotationForce(float rotationSpeed)
    {
        rb.freezeRotation = true;
        transform.Rotate(rotationSpeed * Time.fixedDeltaTime * Vector3.forward);
        rb.freezeRotation = false;
    }
}
