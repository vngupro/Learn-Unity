using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundParallax : MonoBehaviour
{
    private float initialPos, pos, length;
    [SerializeField] float parallaxSpeed = 1f;


    void Start(){
        initialPos = transform.position.y;
        pos = initialPos;
        length = GetComponent<SpriteRenderer>().bounds.size.y;   
    }

    void Update(){
        pos -= parallaxSpeed;
        transform.position = new Vector3(transform.position.x, pos, transform.position.z);
        if(Mathf.Abs(pos) >= initialPos + length){
            transform.position = new Vector3(transform.position.x, initialPos, transform.position.z);
            pos = initialPos;
        }

    }

    // private float length, startpos;
    // public GameObject cam;
    // public float parallaxEffect;

    // // Start is called before the first frame update
    // void Start()
    // {
    //     
    //     length = GetComponent<SpriteRenderer>().bounds.size.y;   
    // }

    // // Update is called once per frame
    // void Update()
    // {
    //     float temp =(cam.transform.position.y * (1 - parallaxEffect));
    //     float dist = (cam.transform.position.y * parallaxEffect);
    //     transform.position = new Vector3(transform.position.x, startpos + dist, transform.position.z);
    //     if(temp > startpos + length){
    //         startpos += length;
    //     } else if (temp < startpos - length){
    //         startpos -= length;
    //     } 
    // }
}


