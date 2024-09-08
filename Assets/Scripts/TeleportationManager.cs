using UnityEngine;
using System.Collections.Generic;
using UnityEngine.EventSystems;

public class TeleportationController : MonoBehaviour
{
    public GameObject player;
    private List<Planet> planets = new List<Planet>();
    public Camera mainCamera;

    private void Start()
    {
        planets = PlanetManager.Instance.GetPlanets();
    }

    public void FindAndStoreGameObject()
    {
        // Get the name of the GameObject (button) that triggered this method
        string buttonName = EventSystem.current.currentSelectedGameObject.name;

        Debug.Log("Button Name: " + buttonName);

        // Initialize targetPlanet to null
        Planet targetPlanet = null;

        // Find the planet with the matching name
        foreach (Planet planet in planets)
        {
            if (planet.gameObject.name == buttonName)
            {
                targetPlanet = planet;
                break; // Exit the loop once the planet is found
            }
        }

        if (targetPlanet != null)
        {
            Debug.Log("Found GameObject: " + targetPlanet.name);
            Debug.Log("Found GameObject Position: " + targetPlanet.transform.position);

            Vector3 playerPosition = Vector3.zero;

            switch (targetPlanet.name)
            {
                case "Sun":
                    playerPosition = targetPlanet.transform.position + new Vector3(0, 0, 1450f);
                    break;
                case "Mercury":
                case "Venus":
                case "Earth":
                    playerPosition = targetPlanet.transform.position + new Vector3(0, 0, 200);
                    break;
                case "Moon":
                    playerPosition = targetPlanet.transform.position + new Vector3(0, 0, 100f);
                    break;
                case "Mars":
                    playerPosition = targetPlanet.transform.position + new Vector3(0, 0, 250f);
                    break;
                case "Jupiter":
                case "Saturn":
                case "Uranus":
                case "Neptune":
                    playerPosition = targetPlanet.transform.position + new Vector3(2f, 0, 400f);
                    break;
                default:
                    Debug.LogWarning("Unknown planet: " + targetPlanet.name);
                    break;
            }

            player.transform.position = playerPosition;
            player.transform.LookAt(targetPlanet.transform.position);
            mainCamera.transform.LookAt(targetPlanet.transform.position);

        }
        else
        {
            Debug.LogWarning("Planet with name " + buttonName + " not found.");
        }
    }
}
