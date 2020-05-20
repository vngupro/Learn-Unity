using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using  UnityEngine.UI;

public class PlayerSS : MonoBehaviour
{
    // configurations parameters
    [Header("Movement")]
    [SerializeField] float mvtSpeed = 10f;
    [SerializeField] float padding = 1f;

    [Header("Laser")]
    [SerializeField] GameObject laserPrefab;
    [SerializeField] float laserSpeed = 10f;
    [SerializeField] float laserTime = 0.1f;
    [SerializeField] AudioClip laserSound;
    [SerializeField] [Range(0,1)] float laserSoundVolume = 0.5f;

    [Header("Stat")]
    [SerializeField] float healthPlayer = 1000;
    [SerializeField] Scrollbar healthBar;
    [SerializeField] Color healthColor1;
    [SerializeField] Color healthColor2;
    [SerializeField] Color healthColor3;

    float healthDamage = 0.1f;
    ColorBlock cb;


    [Header("Death")]
    [SerializeField] GameObject deathVFX;
    [SerializeField] float durationOfExplosion;

    [SerializeField] AudioClip deathSound;
    [SerializeField] [Range(0,1)] float deathSoundVolume = 0.5f;

    bool isFiring = false;
    Coroutine laserCoroutine;
    float xMin;
    float xMax;
    float yMin;
    float yMax;
    // Start is called before the first frame update
    void Start()
    {
        SetUpMoveBoundaries();
    }

    // Update is called once per frame
    void Update()
    {
        Move();
        Fire();
    }

    // laserCoroutine == null for bug simultaneouse coroutine when 2 fire input at same time
    // private void Fire(){
    //     if(Input.GetButtonDown("Fire1") && laserCoroutine == null){
    //         laserCoroutine = StartCoroutine(FireContinuously());     
    //     }
    //     if(Input.GetButtonUp("Fire1")){
    //         StopCoroutine(FireContinuously());
    //         laserCoroutine = null;
    //     }
    // }

    // IEnumerator FireContinuously(){
    //     while(true){
    //         GameObject laser = Instantiate(
    //             laserPrefab, 
    //             transform.position, 
    //             Quaternion.identity) as GameObject;
    //         laser.GetComponent<Rigidbody2D>().velocity = new Vector2 (0, laserSpeed);
    //         yield return new WaitForSeconds(laserTime);   
    //     }
    // }

    // another solution
        private void Fire(){
        if (Input.GetButtonDown("Fire1") && !isFiring)
        {
            isFiring = true;
            StartCoroutine(FireContinuously());
        }

        if (Input.GetButtonUp("Fire1") && isFiring){
            isFiring = false;
        }
    }
     
    private IEnumerator FireContinuously()
    {
        while (isFiring)
        {
            GameObject laser = Instantiate(laserPrefab, transform.position, Quaternion.identity) as GameObject;
            laser.GetComponent<Rigidbody2D>().velocity = new Vector2(0f, laserSpeed);
            AudioSource.PlayClipAtPoint(laserSound, Camera.main.transform.position, laserSoundVolume);
            yield return new WaitForSeconds(laserTime);
        }
    }

    private void Move(){
        //Unity BuildIn Input
        var deltaX = Input.GetAxis("Horizontal") * Time.deltaTime * mvtSpeed; 
        var deltaY = Input.GetAxis("Vertical") * Time.deltaTime *mvtSpeed;
        var newXPos = Mathf.Clamp(transform.position.x + deltaX, xMin, xMax);
        var newYPos = Mathf.Clamp(transform.position.y + deltaY, yMin, yMax);
        transform.position = new Vector3(newXPos, newYPos, transform.position.z);
    }

        // SetUpMovBoundaries : limit play space
    private void SetUpMoveBoundaries(){
        Camera gameCamera = Camera.main;
        xMin = gameCamera.ViewportToWorldPoint(new Vector3(0,0,0)).x + padding;
        xMax = gameCamera.ViewportToWorldPoint(new Vector3(1,0,0)).x - padding;
        yMin = gameCamera.ViewportToWorldPoint(new Vector3(0,0,0)).y + padding;
        yMax = gameCamera.ViewportToWorldPoint(new Vector3(0,1,0)).y - padding;
    }

    private void OnCollisionEnter2D (Collision2D other){

            DamageDealer damageDealer = other.gameObject.GetComponent<DamageDealer>();
            if(!damageDealer){return;}
            TakeDamage(damageDealer);
    }

    private void OnTriggerEnter2D (Collider2D other){

            DamageDealer damageDealer = other.gameObject.GetComponent<DamageDealer>();
            if(!damageDealer){return;}
            TakeDamage(damageDealer);
    }

    private void TakeDamage(DamageDealer damageDealer){
        healthPlayer -= damageDealer.GetDamage();
        healthBar.size = healthPlayer/1000;

        if(healthPlayer <= 500 && healthPlayer > 300){
            cb = healthBar.colors;
            cb.normalColor = healthColor1;
            healthBar.colors = cb;
        }else if(healthPlayer <= 300){
            cb = healthBar.colors;
            cb.normalColor = healthColor2;
            healthBar.colors = cb;
        }
        // damageDealer.Hit();
        if(healthPlayer <= 0){
            cb = healthBar.colors;
            cb.normalColor = healthColor3;
            healthBar.colors = cb;
            DeathPlayer();
        }
    }

    private void DeathPlayer(){
        Destroy(gameObject);
        GameObject explosion = Instantiate(deathVFX, transform.position, transform.rotation);
        Destroy(explosion, durationOfExplosion);
        AudioSource.PlayClipAtPoint(deathSound, Camera.main.transform.position, deathSoundVolume);
        FindObjectOfType<SceneLoader>().LoadGameOver();
    }
}
