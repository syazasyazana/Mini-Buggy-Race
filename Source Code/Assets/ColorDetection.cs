using UnityEngine;
using Vuforia;

public class ColorDetection : MonoBehaviour
{
    public ImageTargetBehaviour imageTarget; // Assign Vuforia Image Target in Inspector
    public Renderer carRenderer; // Assign the 3D Car Model’s Renderer
    private WebCamTexture webcamTexture;
    private Texture2D cameraTexture;

    void Start()
    {
        // Initialize Webcam
        webcamTexture = new WebCamTexture();
        webcamTexture.Play();

        // Auto-detect car renderer if not assigned
        if (carRenderer == null)
        {
            carRenderer = GetComponentInChildren<MeshRenderer>();
        }
    }

    void Update()
    {
        if (imageTarget != null && imageTarget.TargetStatus.Status == Status.TRACKED)
        {
            DetectColorFromImageTarget();
        }
    }

    void DetectColorFromImageTarget()
    {
        if (webcamTexture.width < 100) return; // Ensure camera is running

        // Convert WebCamTexture to Texture2D
        cameraTexture = new Texture2D(webcamTexture.width, webcamTexture.height);
        cameraTexture.SetPixels32(webcamTexture.GetPixels32());
        cameraTexture.Apply();

        // Extract color from the central part of the image
        Color avgColor = GetDominantColor(cameraTexture);

        // Apply the detected color to the 3D Car Model
        ApplyColorToCar(avgColor);
    }

    Color GetDominantColor(Texture2D texture)
{
    int centerX = texture.width / 2;
    int centerY = texture.height / 2;
    int sampleSize = 50; // Adjust to focus on the car region

    float r = 0, g = 0, b = 0;
    int count = 0;

    // Sample a smaller area around the car (central region)
    for (int x = centerX - sampleSize / 2; x < centerX + sampleSize / 2; x++)
    {
        for (int y = centerY - sampleSize / 2; y < centerY + sampleSize / 2; y++)
        {
            Color pixel = texture.GetPixel(x, y);
            r += pixel.r;
            g += pixel.g;
            b += pixel.b;
            count++;
        }
    }

    return new Color(r / count, g / count, b / count);
}

    void ApplyColorToCar(Color detectedColor)
    {
        if (carRenderer != null && carRenderer.material != null)
        {
            carRenderer.material.color = detectedColor;
        }
        else
        {
            Debug.LogWarning("Car Renderer not found!");
        }
    }
}