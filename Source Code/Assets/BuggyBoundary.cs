using UnityEngine;

public class BuggyBoundary : MonoBehaviour
{
    public float minX = 615f;
    public float maxX = 977f;
    public float minZ = -104f;
    public float maxZ = 264f;
    public float minY = 103f; // Adjust based on track height
    public float maxY = 105f; // Adjust slightly above track

    void Update()
    {
        Vector3 pos = transform.position;
        pos.x = Mathf.Clamp(pos.x, minX, maxX);
        pos.z = Mathf.Clamp(pos.z, minZ, maxZ);
        pos.y = Mathf.Clamp(pos.y, minY, maxY); // Prevent floating

        transform.position = pos;
    }
}