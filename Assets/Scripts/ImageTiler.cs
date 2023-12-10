using System;
using UnityEngine;
using UnityEngine.Serialization;

[Serializable]
public class ImageTiler
{
    [FormerlySerializedAs("sheet16x16")] [SerializeField]
    private Texture2D sheet16X16;

    [FormerlySerializedAs("sheet8x8")] [SerializeField]
    private Texture2D sheet8X8;

    [FormerlySerializedAs("sheet4x4")] [SerializeField]
    private Texture2D sheet4X4;

    [Tooltip("The maximum zoom acceptable to use the 8x8 resolution")]
    [SerializeField] private float zoomFor8X8 = 3f;
    [Tooltip("The maximum zoom acceptable to use the 4x4 resolution")]
    [SerializeField] private float zoomFor4X4 = 1f;


    private Sprite[] _sprites16X16;
    private Sprite[] _sprites8X8;
    private Sprite[] _sprites4X4;
    
    public float GetResolution(float zoom)
    {
        if (zoom > zoomFor8X8)
        {
            return 0;
        }
        if (zoom > zoomFor4X4)
        {
            return 1;
        }

        return 2;
    }


    public void Initialize()
    {
#if UNITY_EDITOR
        //Check if any Texture2d is null
        if (sheet16X16 == null)
        {
            Debug.LogError("sheet16x16 is null");
        }

        if (sheet8X8 == null)
        {
            Debug.LogError("sheet8x8 is null");
        }

        if (sheet4X4 == null)
        {
            Debug.LogError("sheet4x4 is null");
        }
#endif


        _sprites16X16 = Resources.LoadAll<Sprite>(sheet16X16.name);
        _sprites8X8 = Resources.LoadAll<Sprite>(sheet8X8.name);
        _sprites4X4 = Resources.LoadAll<Sprite>(sheet4X4.name);
        
    }



    /// <summary>
    /// 
    /// </summary>
    /// <param name="x"></param>
    /// <param name="y"></param>
    /// <param name="resolution">Resolution 0 for 16x16 sheet,
    /// Resolution 1 for 8x8 sheet
    /// Resolution 2 for 4x4 sheet</param>
    /// <returns></returns>
    public Sprite GetSprite(int x, int y, int resolution)
    {
        Sprite[] sprites;
        int rowSize;

        //Assign sprite sheet to the right one depending on variable called resolution
        switch (resolution)
        {
            case 0:
                sprites = _sprites16X16;
                rowSize = 16;
                break;
            case 1:
                sprites = _sprites8X8;
                rowSize = 8;
                break;
            case 2:
                sprites = _sprites4X4;
                rowSize = 4;
                break;
            default:
                Debug.LogError("WRONG INPUT");
                sprites = _sprites16X16;
                rowSize = 16;
                break;
        }


        // Calculate the index based on x, y, and the number of sprites per row
        int index = y * rowSize + x;

        // Check if the index is within the bounds of the sprite array
        if (index >= 0 && index < sprites.Length)
        {
            return sprites[index];
        }

        Debug.LogError("Sprite index out of range.");
        return null;
    }
}