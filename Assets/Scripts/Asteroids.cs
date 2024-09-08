using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Asteroids : MonoBehaviour
{
    // Start is called before the first frame update
    private void Start()
    {
        PlanetManager.Instance.AddAsteroid(this);
    }

    private void OnDestroy()
    {
        PlanetManager.Instance.RemoveAsteroid(this);
    }
    private void OnCollisionEnter(Collision collision)
    {
        // Destroy the asteroid when it collides with another object
        Destroy(gameObject);
    }
}
