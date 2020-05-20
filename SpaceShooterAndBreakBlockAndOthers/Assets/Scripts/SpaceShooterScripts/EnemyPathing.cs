﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Move the enemy along current path
public class EnemyPathing : MonoBehaviour
{
    // config parameters
    [SerializeField] WaveConfig waveConfig;
     List<Transform> waypoints;
     int waypointIndex = 0;
     
    // Start is called before the first frame update
    void Start()
    {
        waypoints = waveConfig.GetWaypoints();
        transform.position = waypoints[waypointIndex].transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        Move();
    }
    
    // waveConfig of this function  = waveConfig from config parameters
    public void SetWaveConfig(WaveConfig waveConfig){
        this.waveConfig = waveConfig;
    }
    // Move with waypoint : careful waypoint reset transform necessary
    private void Move(){
        if (waypointIndex < waypoints.Count){
            var targetPosition = waypoints[waypointIndex].transform.position;
            var movementThisFrame = waveConfig.GetMoveSpeed() * Time.deltaTime;
            transform.position = Vector2.MoveTowards(transform.position, targetPosition, movementThisFrame);
            if(transform.position == targetPosition){
                waypointIndex++;
            }

        }else{
                Destroy(gameObject);
        }
    }
}
