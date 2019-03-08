using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TextureRevealer : MonoBehaviour
{
    public Texture2D background;
    private Color32[] PixelArray;
    public Texture2D SourceTexture2D;
    private int brushSize;

    public enum BrushShapEnum
    {

    }

    private Color32 caughtPixels;
    // Start is called before the first frame update
    void Start()
    {
        PixelArray = background.GetPixels32();
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void Paint()
    {


        //1. Grab the instance of the target texture (or instantiate it if necessary -- may need some fancy logic for this)
        //2. Grab the reference ixels from the source texture at the correct location
        //3. Apply the pixels from the source block to the correct location of the target texture

        //That's a 100% application. If the colors match (a conditional...) increase the alpha instead via lerping.


    }

    private int transformPositionToPixelSpace(Vector2 inputPoint)
    {

    }
    //This method assumes it's being fed from a pixel position where 0,0 is the bottom left corner of the screen (a la mouse position)
    private Color32[] GetPixelsFromSourceAtPosition(Vector2Int inputPixelPosition)
    {


        
        int height = SourceTexture2D.height;
        int width = SourceTexture2D.width;


        //Start from the lower left corner of the brush area or the texture... 
        int startingCol = Mathf.Max(inputPixelPosition.x - brushSize, 0);
        int startingRow = Mathf.Max(inputPixelPosition.y - brushSize, 0);
        
        //End at the top right of the brush or the texture...
        int endingCol = Mathf.Min(inputPixelPosition.x + brushSize, height);
        int endingRow = Mathf.Min(inputPixelPosition.y + brushSize, width);

        //Determine the size of the sample
        int sampleCols = (endingCol == width? endingCol - inputPixelPosition.x : brushSize / 2) 
                         + (startingCol > 0? brushSize / 2 : inputPixelPosition.x);

        int sampleRows = (endingRow == height ? endingRow - inputPixelPosition.y : brushSize / 2) 
                         + (startingRow > 0 ? brushSize / 2 : inputPixelPosition.y);

        // Create an array to receive the colors
        var Pixels = new Color32[brushSize * brushSize];
        
        //Retrieve the pixel data via the row major algorithm: row * numCol + column
        int indexer = 0;
        int currentRow = startingRow;
        int currentCol = startingCol;
        for (int i = 0; i < sampleRows; i++)
        {
            for (int j = 0; j < sampleCols; j++)
            {
                Pixels[indexer] = 
            }
        }



        return Pixels;
    }

}
