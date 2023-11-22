using UnityEngine;

public class ImageTiler
{
    // Method to get the texture by name
    // Method to get the texture by name
    public static Texture2D GetTextureByName(string imageName)
    {
        imageName = "pngs/" + imageName;
        
        Texture2D texture = Resources.Load<Texture2D>(imageName);

#if UNITY_EDITOR
        
        // Check if the texture was loaded successfully
        if (texture == null)
        {
            Debug.LogError("Texture not found: " + imageName);
            return null;
        }
#endif

        return texture;
    }


    public static Texture2D GetTileTexture(int x, int y)
    {
        string imageName = GetTileImage(x, y);
        
        var texture = GetTextureByName(imageName);
        Debug.Log(texture);
        
        return GetTextureByName(imageName);
    }


    private static string GetTileImage(int x, int y)
    {
        // Calculate the total number of tiles per row/column
        int gridSize = 21;

        // Check if the coordinates are for the center image
        if (x == gridSize / 2 && y == gridSize / 2)
        {
            return "0"; //".png";
        }

        // Calculate the index of the image
        int index = x * gridSize + y + 1;

        // Adjust the index if it's after the center image
        if (index >= (gridSize * gridSize / 2) + 1)
        {
            index--;
        }

        // Return the image file name
        return index + "";// + ".png";
    }
}