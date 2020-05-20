using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallControl : MonoBehaviour
{
    [SerializeField] GameObject paddle1;
    [SerializeField] float xPush = 2f;
    [SerializeField] float yPush = 15f;
    [SerializeField] float randomFactor = 0.2f; // vs infinite horizontal ball mvt

    [SerializeField] AudioClip[] ballSound;
    //state
    Vector2 paddleToBallVector;
    bool hasStarted = false;
    Rigidbody2D ballRigidBody;
    AudioSource ballAudioSource;
    // Start is called before the first frame update
    void Start()
    {
        paddleToBallVector = transform.position - paddle1.transform.position;
        ballRigidBody = GetComponent<Rigidbody2D>();
        ballAudioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if(!hasStarted){
            LockBallToPaddle();
            LauchOnMouseClick();
        }
        
    }

    // LauchOnMouseClick : On mouse left click lauch the ball once
    private void LauchOnMouseClick(){
        if(Input.GetMouseButtonDown(0)){
            ballRigidBody.velocity = new Vector2 (xPush, yPush);
            hasStarted = true;
        }
    }

    // LockBallToPaddle : Lock to paddle at start of game until lauch
    private void LockBallToPaddle(){
        Vector2 paddlePos = new Vector2(paddle1.transform.position.x, paddle1.transform.position.y);
        transform.position = paddlePos + paddleToBallVector;
    }

    private void OnCollisionEnter2D(Collision2D other) {
        // Vector2 velocityTweak = new Vector2(Random.Range(0f, randomFactor), Random.Range(0f, randomFactor));
        if(hasStarted){
            // GetComponent<AudioSource>().Play();
            AudioClip clip = ballSound[UnityEngine.Random.Range(0, ballSound.Length)];
            ballAudioSource.PlayOneShot(clip); //Make the sound play all the way through
            // ballRigidBody.velocity += velocityTweak;
        }
        
    }
}
