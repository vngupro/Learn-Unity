using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/* LevelBreakBlock : counting number of block in level and 
    see when you destroy all blocks 
    load next scene 
*/
public class LevelBreakBlock : MonoBehaviour
{
    // param
    [SerializeField] int breakableBlocks; //Serialize for debug purposes
    // cache
    SceneLoader sceneLoader;

    // Another way is to directly use UnityEngine.SceneManagement
    // But already have a script so better use it
    private void Start() {
        sceneLoader = FindObjectOfType<SceneLoader>();    
    }

    // CountBreakeableBlocks : Count blocks, each block run this function once
    public void CountBreakableBlocks(){
        breakableBlocks++;
    }

    // BlockDestoyed : decrease number of block to destroy before next level
    public void BlockDestroyed(){
        breakableBlocks--;
        if(breakableBlocks <= 0){
            sceneLoader.LoadNextScene();    // Go to next level when last block is hit
        }
    }
}
