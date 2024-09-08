using UnityEngine;

public class ShootObject : MonoBehaviour
{
    public GameObject[] objectPrefabs; // Array of prefabs to instantiate
    public float shootForce = 1000f; // The force to apply when shooting
    public Transform spawnPoint; // The transform indicating where to spawn the object

    private GameObject instantiatedObject;
    private Rigidbody rb;
    private TrailRenderer trail;
    private float holdTime;
    private bool isHoldingRightButton;
    [SerializeField] private float massFactor = 0.00001f;
    [SerializeField] private float scaleFactor = 0.00001f;
    private Collider objectCollider;

    void Update()
    {
        HandleInput();
    }

    private void HandleInput()
    {
        // Detect right mouse button press
        if (Input.GetMouseButtonDown(1))
        {
            StartHoldingRightButton();
        }

        // Increase hold time and scale if holding the right mouse button
        if (Input.GetMouseButton(1) && isHoldingRightButton)
        {
            ScaleObject();
            UpdateObjectPosition();
        }

        // If right mouse button is released
        if (Input.GetMouseButtonUp(1) && isHoldingRightButton)
        {
            EndHoldingRightButton();
        }

        // If left mouse button is pressed and holding right button, shoot the object
        if (Input.GetMouseButtonDown(0) && isHoldingRightButton)
        {
            ShootTheObject();
            StartHoldingRightButton(); // Ensure new object is instantiated
        }
    }

    private void UpdateObjectPosition()
    {
        if (instantiatedObject != null && isHoldingRightButton)
        {
            // Update the object's position to follow the camera's position
            instantiatedObject.transform.position = Camera.main.transform.position + Camera.main.transform.forward * 2f; // Adjust the offset as needed
        }
    }

    private void StartHoldingRightButton()
    {
        // Start holding the right mouse button
        isHoldingRightButton = true;
        holdTime = 0f;

        // Randomly choose a prefab from the array
        GameObject prefabToInstantiate = objectPrefabs[Random.Range(0, objectPrefabs.Length)];

        // Instantiate the chosen prefab at the spawn point
        instantiatedObject = Instantiate(prefabToInstantiate, spawnPoint.position, spawnPoint.rotation);
        instantiatedObject.SetActive(true);

        trail = instantiatedObject.GetComponent<TrailRenderer>();
        rb = instantiatedObject.GetComponent<Rigidbody>();
        objectCollider = instantiatedObject.GetComponent<Collider>();

        // Temporarily disable the collider to prevent collisions
        if (objectCollider != null)
        {
            objectCollider.enabled = false;
        }

        // Temporarily disable gravity and set velocity to zero
        if (rb != null)
        {
            rb.useGravity = false;
            rb.velocity = Vector3.zero; // Stop any movement
        }

        // Set initial scale to zero
        instantiatedObject.transform.localScale = Vector3.zero;

        // Disable the TrailRenderer initially
        if (trail != null)
        {
            trail.enabled = false;
        }
    }

    private void ScaleObject()
    {
        holdTime += Time.deltaTime;

        // Increase scale uniformly on all three axes
        float scaleIncrease = holdTime * scaleFactor; // Adjust scale factor as needed
        instantiatedObject.transform.localScale = new Vector3(scaleIncrease, scaleIncrease, scaleIncrease);

        if (rb != null)
        {
            // Increase mass as the object scales, keeping it relative to Earth's mass
            rb.mass = holdTime * massFactor; // Increase mass over time while holding the button
        }
    }

    private void EndHoldingRightButton()
    {
        // If no left click to shoot, cancel the instantiation
        if (!Input.GetMouseButton(0))
        {
            Destroy(instantiatedObject);
        }
        // Reset holding state
        isHoldingRightButton = false;
    }

    private void ShootTheObject()
    {
        if (rb != null)
        {
            float forceFactor = 1000f; // Adjust this based on desired acceleration
            rb.AddForce(Camera.main.transform.forward * shootForce * forceFactor * rb.mass, ForceMode.Impulse);

            // Enable the TrailRenderer when shooting
            if (trail != null)
            {
                trail.enabled = true;
            }

            // Re-enable the collider when shooting
            if (objectCollider != null)
            {
                objectCollider.enabled = true;
            }
        }

        isHoldingRightButton = false;
    }
}
