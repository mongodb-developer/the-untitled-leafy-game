using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camera : MonoBehaviour
{

    public Transform player;

    void Start() { }

    void Update() {
        transform.position = new Vector3(player.position.x + 4, transform.position.y, transform.position.z);
    }
}
