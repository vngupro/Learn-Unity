using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
    Enemy Sprite
    Enemy hit points
    Shooting behaviour
    Score for kill
    Enemy death FXs
*/

public class EnemySS : MonoBehaviour
{   
    [Header("Stat")]
    [SerializeField] float health = 100;
    [SerializeField] int scoreValue = 150;

    [Header("Laser")]
    [SerializeField] float shotCounter; //timer before next shot
    [SerializeField] float minTimeBetweenShots = 0.2f; // randomize shot time
    [SerializeField] float maxTimeBetweenShots = 3f; //randomize shot time
    [SerializeField] GameObject laserEnemy;
    [SerializeField] float laserEnemySpeed;
    [SerializeField] AudioClip laserSound;
    [SerializeField] [Range(0,1)] float laserSoundVolume = 0.5f;

    [Header("Death")]
    [SerializeField] GameObject deathVFX;
    [SerializeField] float durationOfExplosion;

    [SerializeField] AudioClip deathSound;

    [SerializeField] [Range(0,1)] float deathSoundVolume = 0.5f;
    // Start is called before the first frame update
    void Start()
    {
        shotCounter = Random.Range(minTimeBetweenShots, maxTimeBetweenShots);
    }

    // Update is called once per frame
    void Update()
    {
        CountDownAndShoot();
    }

    private void CountDownAndShoot(){
        shotCounter -= Time.deltaTime;
        if(shotCounter <= 0f){
            FireEnemy();
            shotCounter = Random.Range(minTimeBetweenShots, maxTimeBetweenShots);
        }
    }

    private void FireEnemy(){
        GameObject laser = Instantiate(
            laserEnemy,
            transform.position,
            Quaternion.identity) as GameObject;
            laser.GetComponent<Rigidbody2D>().velocity = new Vector2(0, -laserEnemySpeed);
            AudioSource.PlayClipAtPoint(laserSound, Camera.main.transform.position, laserSoundVolume);
    }

    private void OnTriggerEnter2D (Collider2D other){
            DamageDealer damageDealer = other.gameObject.GetComponent<DamageDealer>();
            if(!damageDealer){return;}
            ProcessHit(damageDealer);
    }

    private void ProcessHit(DamageDealer damageDealer){
        health -= damageDealer.GetDamage();
        // damageDealer.Hit();
        if(health <= 0){
            DieEnemy();
        }
    }

    private void DieEnemy(){
        Destroy(gameObject);
        GameObject explosion = Instantiate(deathVFX, transform.position, transform.rotation);
        Destroy(explosion, durationOfExplosion);
        AudioSource.PlayClipAtPoint(deathSound, Camera.main.transform.position, deathSoundVolume);
        FindObjectOfType<GameStatusSS>().addToScore(scoreValue);
    }
}
