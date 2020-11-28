using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Rocket : MonoBehaviour
{
    enum State { ALIVE,DYING, TRANSCENDING};

    State state = State.ALIVE;
    Rigidbody rigidBody;
    AudioSource audioSource;
    [SerializeField] float rotateSpeed = 30f;
    [SerializeField] float thrustSpeed = 10f;
    [SerializeField] AudioClip thrustSound;
    [SerializeField] AudioClip dieSound;
    [SerializeField] AudioClip successSound;

    // Start is called before the first frame update
    void Start()
    {
        rigidBody = GetComponent<Rigidbody>();
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        HandleInput();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (state != State.ALIVE) return;

        switch (collision.gameObject.tag)
        {
            case "Friendly":
                break;
            case "Finish":
                state = State.TRANSCENDING;
                audioSource.Stop();
                audioSource.PlayOneShot(successSound);
                Invoke(nameof(LoadNextLevel), 1f);
                break;
            default:
                state = State.DYING;
                audioSource.Stop();
                audioSource.PlayOneShot(dieSound);
                Invoke(nameof(ResetLevel), 1f);
                break;
        }
    }

    private void LoadNextLevel()
    {
        SceneManager.LoadScene(1);
    }

    private void ResetLevel()
    {
        SceneManager.LoadScene(0);
    }

    private void HandleInput()
    {
        if (state == State.ALIVE)
        {
            HandleThrust();
            HandleRotate();
        }
    }

    private void HandleRotate()
    {
        if (Input.GetKey(KeyCode.A))
        {
            // Rotate the object around its local X axis at rotateSpeed degree/s
            transform.Rotate(Vector3.left * Time.deltaTime * rotateSpeed);
        }
        else if (Input.GetKey(KeyCode.D))
        {
            transform.Rotate(Vector3.right * Time.deltaTime * rotateSpeed);
        }
    }

    private void HandleThrust()
    {
        if (Input.GetKey(KeyCode.Space))
        {
            rigidBody.AddRelativeForce(Vector3.up * Time.deltaTime * thrustSpeed);
            if (!audioSource.isPlaying)
                audioSource.PlayOneShot(thrustSound);
        }
        else
        {
            audioSource.Stop();
        }
    }
}
