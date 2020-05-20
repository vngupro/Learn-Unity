using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PaddleMouseControl : MonoBehaviour
{
    [SerializeField] float screenWidthInUnits = 32f;
    [SerializeField] float minX = 1f;
    [SerializeField] float maxX = 31f;

    GameStatus theGameSession;
    BallControl theBall;
    // Start is called before the first frame update
    void Start()
    {
        theGameSession = FindObjectOfType<GameStatus>();
        theBall = FindObjectOfType<BallControl>();
    }

    // Update is called once per frame
    void Update()
    {
        // Working and better 
        float xMousePosition = GetXPos(); //get mouse position in world unit

        Vector3 currentPosition = transform.position; // Get current pos of padding

        currentPosition.x = xMousePosition; //change current position x of paddle to the current position x of mouse
        currentPosition.x = Mathf.Clamp(GetXPos(), minX, maxX); //Limit position of min x and max x so no off screen
        transform.position = currentPosition; //make the transform happen because need z axis in vector 3
    }

    // For Autoplay
    // GetXPos : Get the pos of ball and make paddle follow
    private float GetXPos(){
        if(theGameSession.IsAutoPlayOn()){
            return theBall.transform.position.x;
        } else {
            return Camera.main.ScreenToWorldPoint(Input.mousePosition).x;
        }
    }
}

        
        // Debug.Log(Input.mousePosition); // Mouse Position
        // Debug.Log(Input.mousePosition.x); //Mouse Position Only on X axis
        // Debug.Log(Input.mousePosition.x / Screen.width); //Mouse Relative Position Only on X axis

        // 6 camera size so 6 unit Unity Height so 12 in total height so 16 in width if aspect ratio  is 4:3
        // camera size = 6, aspect ration = 16:9, so for 9 height = 18 total unity units height we have 32 total unity unit width

        // Debug.Log(Input.mousePosition.x / Screen.width * 32); //Mouse Relative Position Only on X axis in Camera size Ratio 
        // Debug.Log(Input.mousePosition.x / Screen.width * screenWidthInUnits); //Mouse Relative Position Only on X axis in Camera size Ratio 
        
        // Working
        // float mousePosInUnits = Input.mousePosition.x / Screen.width * screenWidthInUnits; //Mouse Relative Position Only on X axis in Camera size Ratio 

        // Vector2 paddlePos = new Vector2(mousePosInUnits, transform.position.y);
        // transform.position = paddlePos;
