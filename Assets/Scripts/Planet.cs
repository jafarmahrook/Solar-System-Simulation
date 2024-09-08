using UnityEngine;

public class Planet : MonoBehaviour
{
    
    private void Awake()
    {
        PlanetManager.Instance.AddPlanet(this);
    }

    private void OnDestroy()
    {
        PlanetManager.Instance.RemovePlanet(this);
    }

  
}
