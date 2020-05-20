using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockControl : MonoBehaviour
{
    [SerializeField] AudioClip breakSound;
    [SerializeField] GameObject blockSparklesVFX;
    [SerializeField] Sprite[] hitSprites;

    //Debug purpose
    [SerializeField] int timeHits;

    // One method to get a specific gameobject
    // [SerializeField] GameObject level;

    // other way need to create class in another file
    LevelBreakBlock level;
    void Start(){
        CountBreakBlock();
    }

    // CountBreakBlock : Count Block And Help for go next level
    private void CountBreakBlock(){
        level = FindObjectOfType<LevelBreakBlock>();
        if(tag == "breakable" || tag == "multihit"){
            level.CountBreakableBlocks();
        }
    }
    // Collision 2D give a bunch of parameters about the collision happening between 2 game object if there a collider
    private void OnCollisionEnter2D(Collision2D collision) {
        if(tag == "multihit"){
            HandleHit();
        }else if(tag == "breakable"){
            DestroyBlock();
        }
    }

    // HandleHit : decide if block is destroy or only damage 
    private void HandleHit(){
        
        timeHits++;
        int maxHits = hitSprites.Length + 1;
        if( timeHits >= maxHits){
                DestroyBlock();
        }else{
            ShowNextHitSprite();
        }
    }

    // ShowNextHitSprite : control sprite view depending the damage the block took
    private void ShowNextHitSprite(){
        int spriteIndex = timeHits - 1;
        if(hitSprites[spriteIndex] != null){
            GetComponent<SpriteRenderer>().sprite = hitSprites[spriteIndex];
        }else{
            Debug.LogError("BlockSprite missing from array" + gameObject.name);
        }
    }

    // DestroyBlock : Destroy block and add vfx
    private void DestroyBlock(){
        PlayBlockDestroySFX();
        Destroy(gameObject,0.2f);
        level.BlockDestroyed();
        TriggerSparklesVFX();
        
    }

    // PlayBlockDestroySFX : Control sfx of particle vfx 
    private void PlayBlockDestroySFX(){
        FindObjectOfType<GameStatus>().addToScore();
        AudioSource.PlayClipAtPoint(breakSound, transform.position, 0.1f);
    }

    // TriggerSparklesVFX : Add particle VFX on block
    private void TriggerSparklesVFX(){
        GameObject sparkles = Instantiate(blockSparklesVFX, transform.position, transform.rotation);
        Destroy(sparkles, 1f);
    }
}

        // For sound effect playing before destroy object add delay 1f
        // Destroy(gameObject, 1f);

        // For sound effect after destroy
        // Game object the script is on
        // You can play on camera or gameobject (less sound)
        // In 3D sound
        // AudioSource.PlayClipAtPoint(breakSound, Camera.main.transform.position);
        // public static void PlayClipAtPoint(AudioClip clip, Vector3 position, float volume = 1.0F); 
