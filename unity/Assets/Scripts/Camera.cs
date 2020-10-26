using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camera : MonoBehaviour
{

    public Transform player;

    void Start() { }

    // Update camera X position to match the attached player
    void Update() {
        transform.position = new Vector3(player.position.x, transform.position.y, transform.position.z);
    }
}
