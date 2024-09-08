using System.Collections.Generic;
using UnityEngine;

public class PlanetRotation : MonoBehaviour
{
    [SerializeField] private float gravitationalConstant = 0.01f; // Adjust this value based on your simulation scale
    private List<Planet> planets;
    void Start()
    {
        // Find all planets and apply the rotation calculation
        planets = PlanetManager.Instance.GetPlanets();

    }



    void FixedUpdate()
    {

        foreach (Planet planet in planets)
        {
            ApplyRotation(planet);
        }
    }
    void ApplyRotation(Planet planet)
    {
        // Debug log to verify planet details
        Debug.Log($"Planet: {planet.name}, Scale: {planet.transform.localScale}, Mass: {planet.GetComponent<Rigidbody>().mass}");

        float radius = planet.transform.localScale.x / 2f;
        float mass = planet.GetComponent<Rigidbody>().mass;

        float rotationPeriod = 2f * Mathf.PI * Mathf.Sqrt(Mathf.Pow(radius, 3) / (gravitationalConstant * mass));
        float rotationSpeed = 360f / rotationPeriod * Time.deltaTime;

        // Rotate the planet around its own axis
        planet.transform.Rotate(Vector3.up, rotationSpeed);
    }
}
