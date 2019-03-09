using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using Object = System.Object;

public class TextureRevealer : MonoBehaviour
{
    public Texture2D background;
    private Color32[] PixelArray;
    public Texture2D SourceTexture2D;
    public Texture2D TargetTexture2D;
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




    //This method assumes it's being fed from a pixel position where 0,0 is the bottom left corner of the screen (a la mouse position)
    private void Paint(Vector2Int inputPixelPosition)
    {

        // Create an array to receive the colors from the source texture
        var sample = ReadFromFlattenedArray(SourceTexture2D.GetPixels32(), SourceTexture2D.height, SourceTexture2D.width, brushSize, inputPixelPosition);
        
        //Modify the sampled pixels according to brush settings

        //Write the final result
        TargetTexture2D.SetPixels32(WriteToFlattenedArray(sample, TargetTexture2D.GetPixels32(), TargetTexture2D.height,
            TargetTexture2D.height, inputPixelPosition));
    }


    //Captures a square sample of the flattened source array.
    private static Color32[] ReadFromFlattenedArray(Color32[] sourceArray, int height, int width, int areaSize,
        Vector2Int startingPosition)
    {
        var targetArray = new Color32[areaSize * areaSize];
        int areaSizeSide = Mathf.RoundToInt((float)areaSize / 2);

        //Start from the lower left corner of the targetted area of the array, controlled by boundaries... 
        int startingCol = Mathf.Max(startingPosition.x - areaSizeSide, 0);
        int startingRow = Mathf.Max(startingPosition.y - areaSizeSide, 0);

        //End at the top right.
        int endingCol = Mathf.Min(startingPosition.x + areaSizeSide, height);
        int endingRow = Mathf.Min(startingPosition.y + areaSizeSide, width);

        //Determine the size of the sample
        int sampleCols = (endingCol == width ? endingCol - startingPosition.x : areaSizeSide)
                         + (startingCol > 0 ? areaSizeSide / 2 : startingPosition.x);

        int sampleRows = (endingRow == height ? endingRow - startingPosition.y : areaSizeSide)
                         + (startingRow > 0 ? areaSizeSide / 2 : startingPosition.y);
        
        //Retrieve the data via the row/column major algorithm: row * numCol + column
        int indexer = 0;
        int majorIndex = height >= width ? sampleCols : sampleRows;
        int minorIndex = majorIndex == sampleCols ? sampleRows : sampleCols;
        int transversalAxis = minorIndex == sampleCols ? width : height;
        int majorCurrentIndex = transversalAxis == width? startingRow : startingCol;
        int minorCurrentIndex = majorCurrentIndex == startingRow? startingCol : startingRow;
        for (int i = 0; i < majorIndex; i++)
        {

            for (int j = 0; j < minorIndex; j++)
            {
                targetArray[indexer] = sourceArray[majorCurrentIndex * transversalAxis + minorCurrentIndex];
                indexer++;
                minorCurrentIndex++;
            }
            //Minor index has to be reset to the starting column of the sample rather than to 0;
            minorCurrentIndex = transversalAxis == width? startingCol : startingRow;
            majorCurrentIndex++;
        }

        return targetArray;
    }

    //This method will always be row-major.
    private static Color32[] WriteToFlattenedArray(Color32[] sourceArray, Color32[] targetArray, int targetHeight, int targetWidth, Vector2Int startingPosition)
    {

        int areaSizeSide = Mathf.RoundToInt((float)sourceArray.Length / 2);

        //Start from the lower left corner of the targetted area of the array, controlled by boundaries... 
        int startingCol = Mathf.Max(startingPosition.x - areaSizeSide, 0);
        int startingRow = Mathf.Max(startingPosition.y - areaSizeSide, 0);

        //End at the top right.
        int endingCol = Mathf.Min(startingPosition.x + areaSizeSide, targetHeight);
        int endingRow = Mathf.Min(startingPosition.y + areaSizeSide, targetWidth);

        //Determine the size of the sample


        //Retrieve the data via the row/column major algorithm: row * numCol + column
        int indexer = 0;
        int majorCurrentIndex = startingRow;
        int minorCurrentIndex = startingCol;
        for (int i = 0; i < endingRow; i++)
        {

            for (int j = 0; j < endingCol; j++)
            {
                targetArray[indexer] = sourceArray[majorCurrentIndex * targetWidth + minorCurrentIndex];
                indexer++;
                minorCurrentIndex++;
            }
            //Column has to be reset to the starting column of the sample rather than to 0;
            minorCurrentIndex = startingCol;
            majorCurrentIndex++;
        }

        return targetArray;
    }

}
}

