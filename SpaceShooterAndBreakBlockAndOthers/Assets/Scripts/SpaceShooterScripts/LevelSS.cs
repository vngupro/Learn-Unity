using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelSS : MonoBehaviour
{
 // param
    [SerializeField] int enemyCount; //Serialize for debug purposes
    // cache
    SceneLoader sceneLoader;

    // Another way is to directly use UnityEngine.SceneManagement
    // But already have a script so better use it
    private void Start() {
        sceneLoader = GetComponent<SceneLoader>();    
    }


}
