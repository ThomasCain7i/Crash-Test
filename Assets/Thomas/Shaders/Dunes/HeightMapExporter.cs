using UnityEngine;

public class HeightMapExporter : MonoBehaviour
{
    public Shader shader; // Reference to the shader that contains your shader graph
    public RenderTexture renderTexture; // Reference to the Render Texture asset created in step 1

    void Start()
    {
        // Create a temporary material with the shader
        Material material = new Material(shader);

        // Set up the Render Texture as the render target
        RenderTexture.active = renderTexture;
        // Clear the Render Texture
        GL.Clear(true, true, Color.clear);

        // Render the shader graph to the Render Texture
        Graphics.Blit(null, renderTexture, material);

        // Read the Render Texture data into a Texture2D
        Texture2D texture = new Texture2D(renderTexture.width, renderTexture.height);
        texture.ReadPixels(new Rect(0, 0, renderTexture.width, renderTexture.height), 0, 0);
        texture.Apply();

        // Save the Texture2D as a PNG image file
        byte[] bytes = texture.EncodeToPNG();
        System.IO.File.WriteAllBytes("HeightMap.png", bytes);

        // Clean up
        Destroy(material);
        Destroy(texture);
    }
}