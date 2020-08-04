using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControllerX : MonoBehaviour
{
    public bool gameOver;
    public float floatForce= 7;
    private float gravityModifier = 1.5f;
    private Rigidbody playerRb;

    public ParticleSystem explosionParticle;
    public ParticleSystem fireworksParticle;

    private AudioSource playerAudio;
    public AudioClip moneySound;
    public AudioClip explodeSound;

    private bool isLowEnough;
    private float upperLimit = 14.5f;

    public AudioClip bounceSound;
    private float groundCollisionForce = 10f;

    // Start is called before the first frame update
    void Start()
    {
        playerRb = GetComponent<Rigidbody>();
        Physics.gravity *= gravityModifier;
        playerAudio = GetComponent<AudioSource>();


    }

    // Update is called once per frame
    void Update()
    {
        if (transform.position.y >= upperLimit)
        {
            isLowEnough = false;
        }
        else if (transform.position.y < upperLimit)
        {
            isLowEnough = true;
        }
        // While space is pressed and player is low enough, float up
        if (Input.GetKey(KeyCode.Space) && !gameOver && isLowEnough )
        {
            playerRb.AddForce(Vector3.up * floatForce, ForceMode.Impulse);

        }
        else if(Input.GetKey(KeyCode.Space) && !gameOver && !isLowEnough)
        {
            isLowEnough = false;
        }
        
    }

    private void OnCollisionEnter(Collision other)
    {
        // if player collides with bomb, explode and set gameOver to true
        if (other.gameObject.CompareTag("Bomb"))
        {
            explosionParticle.Play();
            playerAudio.PlayOneShot(explodeSound, 1.0f);
            gameOver = true;
            Debug.Log("Game Over!");
            Destroy(other.gameObject);
        } 

        // if player collides with money, fireworks
        else if (other.gameObject.CompareTag("Money"))
        {
            fireworksParticle.Play();
            playerAudio.PlayOneShot(moneySound, 1.0f);
            Destroy(other.gameObject);

        }

        else if (other.gameObject.CompareTag("Ground") && !gameOver)
        {
            playerRb.AddForce(Vector3.up * groundCollisionForce, ForceMode.VelocityChange);

            playerAudio.PlayOneShot(bounceSound, 1.0f);
        }

    }

}
