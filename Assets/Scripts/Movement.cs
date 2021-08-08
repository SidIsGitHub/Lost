using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    [SerializeField] float mainThrust = 100f;
    [SerializeField] float rotateThrust = 1f;
    [SerializeField] AudioClip mainEngine;
    [SerializeField] ParticleSystem mainEngineParticles;
    
    [SerializeField] ParticleSystem leftThrustParticles;
    [SerializeField] ParticleSystem rightThrustParticles;
    Rigidbody rb;
    AudioSource audioSource;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        ProcessThrust();
        ProcessRotation();

    }

    void ProcessThrust()
    {
        if (Input.GetKey(KeyCode.Space))
        {
            StartThrusting();

        }
        else
        {
            StopThrusting();
        }

    }
     void ProcessRotation()
    {
         if(Input.GetKey(KeyCode.A))
        {
            RotateLeft();
        }
        else if(Input.GetKey(KeyCode.D))
        {
            RotateRight();
        }
        else
        {
            StopRotating();
        }
    }
    void StartThrusting()
    {
        if (!audioSource.isPlaying)
        {
            audioSource.PlayOneShot(mainEngine);
        }
        if (!mainEngineParticles.isPlaying)
        {
            mainEngineParticles.Play();
        }
        rb.AddRelativeForce(Vector3.up * mainThrust * Time.deltaTime);
    }
    void StopThrusting()
    {
        audioSource.Stop();
        mainEngineParticles.Stop();
    }
   
    void RotateLeft()
    {
        ApplyRotation(-rotateThrust); //reversed because window was hidden
        if (!leftThrustParticles.isPlaying)
        {
            leftThrustParticles.Play();
        }
    }
    void RotateRight()
    {
        ApplyRotation(rotateThrust); //reversed because window was hidden
        if (!rightThrustParticles.isPlaying)
        {
            rightThrustParticles.Play();
        }
    }
    void StopRotating()
    {
        rightThrustParticles.Stop();
        leftThrustParticles.Stop();
    }
    

    void ApplyRotation(float rotationThisFrame)
    {
        rb.freezeRotation = true; //freezing rotation so we can manually rotate
        transform.Rotate(Vector3.forward * rotationThisFrame * Time.deltaTime);
        rb.freezeRotation = false; //unfreezing rotation so the physics system can take over
    }
}

