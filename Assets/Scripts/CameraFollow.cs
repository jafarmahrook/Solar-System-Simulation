using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{

    public Transform player;  // Reference to the player's transform
    public Vector3 offset;    // Offset between the camera and player

    void Start()
    {
        // Set the initial offset based on the current positions
        offset = transform.position - player.position;
    }

    void LateUpdate()
    {
        // Update the camera's position based on the player's position and the offset
        transform.position = player.position + player.rotation * offset;
        transform.LookAt(player, player.up);

    }
}
