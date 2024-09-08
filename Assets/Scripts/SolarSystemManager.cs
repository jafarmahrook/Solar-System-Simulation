using UnityEngine;
using System.Collections.Generic;

public class SolarSystemManager : MonoBehaviour
{
    [SerializeField] private float G = 1000f;
    [SerializeField] private bool IsEllipticalOrbit = false;

    private List<Planet> planets;
    private List<Asteroids> asteroids;


    void Start()
    {
        planets = PlanetManager.Instance.GetPlanets();
        asteroids = PlanetManager.Instance.GetAsteroids();
        PlanetManager.Instance.OnPlanetAdded += UpdatePlanetList;
        PlanetManager.Instance.OnPlanetRemoved += UpdatePlanetList;
        PlanetManager.Instance.OnAsteroidAdded += UpdateAsteroidList;
        PlanetManager.Instance.OnAsteroidRemoved += UpdateAsteroidList;

        SetInitialVelocity();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Gravity();

    }

    void SetInitialVelocity()
    {
        foreach (Planet a in planets)
        {
            foreach (Planet b in planets)
            {
                if (!a.Equals(b))
                {
                    float m2 = b.GetComponent<Rigidbody>().mass;
                    float r = Vector3.Distance(a.transform.position, b.transform.position);

                    a.transform.LookAt(b.transform);

                    if (IsEllipticalOrbit)
                    {
                        // Eliptic orbit = G * M  ( 2 / r + 1 / a) where G is the gravitational constant, M is the mass of the central object, r is the distance between the two bodies
                        // and a is the length of the semi major axis (!!! NOT GAMEOBJECT a !!!)
                        a.GetComponent<Rigidbody>().velocity += a.transform.right * Mathf.Sqrt((G * m2) * ((2 / r) - (1 / (r * 1.5f))));
                    }
                    else
                    {
                        //Circular Orbit = ((G * M) / r)^0.5, where G = gravitational constant, M is the mass of the central object and r is the distance between the two objects
                        //We ignore the mass of the orbiting object when the orbiting object's mass is negligible, like the mass of the earth vs. mass of the sun
                        a.GetComponent<Rigidbody>().velocity += a.transform.right * Mathf.Sqrt((G * m2) / r);
                    }
                }
            }
        }
    }

    void Gravity()
    {
        // Combine the lists of planets and asteroids
        var allObjects = new List<MonoBehaviour>();
        allObjects.AddRange(planets);
        allObjects.AddRange(asteroids);
       
        for (int i = 0; i < allObjects.Count; i++)
        {
            for (int j = i + 1; j < allObjects.Count; j++)
            {
                var a = allObjects[i];
                var b = allObjects[j];

                if (a != b)
                {
                    var aRigidbody = a.GetComponent<Rigidbody>();
                    var bRigidbody = b.GetComponent<Rigidbody>();

                    if (aRigidbody != null && bRigidbody != null)
                    {
                        float m1 = aRigidbody.mass;
                        float m2 = bRigidbody.mass;
                        float r = Vector3.Distance(a.transform.position, b.transform.position);

                        if (r > 0) // Prevent division by zero
                        {
                            Vector3 force = (b.transform.position - a.transform.position).normalized * (G * (m1 * m2) / (r * r));
                            aRigidbody.AddForce(force);
                            bRigidbody.AddForce(-force); // Apply equal and opposite force to the other object
                        }
                    }
                }
            }
        }
    }

    void UpdatePlanetList(Planet planet)
    {
        planets = PlanetManager.Instance.GetPlanets();
    }
    void UpdateAsteroidList(Asteroids asteroid)
    {
        asteroids = PlanetManager.Instance.GetAsteroids();
    }

}
