using UnityEngine;

public class BuggySpawner : MonoBehaviour
{
    public Transform trackStartPosition; // Assign in Inspector

    void Start()
    {
        // Move buggy to track start position after scanning
        transform.position = trackStartPosition.position + new Vector3(0, -0.5f, 0);
    }
}