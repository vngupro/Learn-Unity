using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class laserScript : MonoBehaviour
{
    // Out of camera view
    private void OnBecameInvisible() {
        Destroy(gameObject);   
    }

    // // hit something
    private void OnTriggerEnter2D(Collider2D other) {
            Destroy(gameObject);   
    }
}
