using UnityEngine;
using System.Collections.Generic;

public class PlanetManager : MonoBehaviour
{
    // Singleton instance
    public static PlanetManager Instance { get; private set; }

    // Lists of planets and asteroids
    private List<Planet> planets = new List<Planet>();
    private List<Asteroids> asteroids = new List<Asteroids>();

    // Events for planet and asteroid addition and removal
    public delegate void PlanetEventHandler(Planet planet);
    public delegate void AsteroidEventHandler(Asteroids asteroid);
    public event PlanetEventHandler OnPlanetAdded;
    public event PlanetEventHandler OnPlanetRemoved;
    public event AsteroidEventHandler OnAsteroidAdded;
    public event AsteroidEventHandler OnAsteroidRemoved;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void AddPlanet(Planet planet)
    {
        if (!planets.Contains(planet))
        {
            planets.Add(planet);
            OnPlanetAdded?.Invoke(planet);
        }
    }

    public void RemovePlanet(Planet planet)
    {
        if (planets.Contains(planet))
        {
            planets.Remove(planet);
            OnPlanetRemoved?.Invoke(planet);
        }
    }

    public void AddAsteroid(Asteroids asteroid)
    {
        if (!asteroids.Contains(asteroid))
        {
            asteroids.Add(asteroid);
            OnAsteroidAdded?.Invoke(asteroid);
        }
    }

    public void RemoveAsteroid(Asteroids asteroid)
    {
        if (asteroids.Contains(asteroid))
        {
            asteroids.Remove(asteroid);
            OnAsteroidRemoved?.Invoke(asteroid);
        }
    }

    public List<Planet> GetPlanets()
    {
        return planets;
    }

    public List<Asteroids> GetAsteroids()
    {
        return asteroids;
    }
}
