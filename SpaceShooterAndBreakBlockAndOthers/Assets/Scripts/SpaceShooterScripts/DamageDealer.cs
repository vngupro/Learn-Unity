using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
    Set Damage
    Trigger VFX
    Other damage related stuff
    Enemy Missile
    Enemy Bomb
    Enemy
    Player Laser
*/
public class DamageDealer : MonoBehaviour
{
    [SerializeField] int damage = 100;

    public int GetDamage(){
        return damage;
    }

    // Disapear on hit (did it on laserScript)
    // public void Hit(){
    //     Destroy(gameObject);
    // }
}
