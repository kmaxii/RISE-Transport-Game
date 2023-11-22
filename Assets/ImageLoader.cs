using UnityEngine;
using System.Collections.Generic;

public class ImageLoader : MonoBehaviour
{
    private static Dictionary<string, Texture2D> textures;

    void Awake()
    {
        // Load all textures in the "pngs" folder at the start
        LoadAllTextures();
        Debug.Log("Images: " + textures.Count);
    }

    private static void LoadAllTextures()
    {
        textures = new Dictionary<string, Texture2D>();
        Texture2D[] loadedTextures = Resources.LoadAll<Texture2D>("pngs");
        foreach (var texture in loadedTextures)
        {
            textures[texture.name] = texture;
        }
    }

    // Method to get the texture by name
    public static Texture2D GetTextureByName(string imageName)
    {
        if (textures.TryGetValue(imageName, out Texture2D texture))
        {
            return texture;
        }
        else
        {
            Debug.LogError("Texture not found: " + imageName);
            return null;
        }
    }
}