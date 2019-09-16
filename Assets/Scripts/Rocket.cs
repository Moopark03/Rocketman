using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class Rocket : MonoBehaviour
{
    Rigidbody rigidBody;
    AudioSource audioSource;
    bool onGround = false;
    [SerializeField] float rotateThrust = 100f;
    [SerializeField] float heightThrust = 100f;

    enum State { Alive, Dead, Transcend };
    State state = State.Alive;
    [SerializeField] float stateTimer = 1f;

    [SerializeField] AudioClip engineSound;
    [SerializeField] AudioClip deathSound;
    [SerializeField] AudioClip transcendSound;

    [SerializeField] ParticleSystem engine;
    [SerializeField] ParticleSystem death;
    [SerializeField] ParticleSystem success;
    // Start is called before the first frame update
    void Start()
    {
        rigidBody = GetComponent<Rigidbody>();
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if (state == State.Alive)
        {
            Thrust();
            if (!onGround)
            {
                Rotate();
            }
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        if (state != State.Alive) { return; }
        onGround = true;
        switch (collision.gameObject.tag)
        {
            case "Friendly":
                break;

            case "Finish":
                state = State.Transcend;
                audioSource.Stop();
                engine.Stop();
                success.Play();
                audioSource.PlayOneShot(transcendSound);
                Invoke("LoadScene", stateTimer);
                break;
            default:
                state = State.Dead;
                audioSource.Stop();
                engine.Stop();
                death.Play();

                audioSource.PlayOneShot(deathSound);
                Invoke("LoadScene", stateTimer);
                break;
        }
    }

    void LoadScene()
    {
        if(state == State.Transcend)
        {
            success.Stop();
            SceneManager.LoadScene(1);
        }
        else if (state == State.Dead)
        {
            death.Stop();
            SceneManager.LoadScene(0);
        }
    }

    void OnCollisionExit(Collision collision)
    {
        onGround = false;
    }

    private void Rotate()
    {
        
        float rotateSpeed = rotateThrust * Time.deltaTime;


        if (Input.GetKey(KeyCode.D))
        {
            rigidBody.freezeRotation = true;
            transform.Rotate(-Vector3.forward * rotateSpeed);
            rigidBody.freezeRotation = false;
        }
        else if (Input.GetKey(KeyCode.A))
        {
            rigidBody.freezeRotation = true;
            transform.Rotate(Vector3.forward * rotateSpeed);
            rigidBody.freezeRotation = false;

        }

        rigidBody.constraints = RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationY;
    }

    private void Thrust()
    {
        if (Input.GetKey(KeyCode.Space))
        {
            rigidBody.AddRelativeForce(Vector3.up * heightThrust * Time.deltaTime);
            if (!audioSource.isPlaying)
            {
                audioSource.PlayOneShot(engineSound);
            }
            engine.Play();
        }
        else
        {
            audioSource.Stop();
            engine.Stop();
        }
    }
}
