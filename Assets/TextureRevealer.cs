using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TextureRevealer : MonoBehaviour
{
    public Texture2D background;
    private Color32[] PixelArray;
    public Texture2D SourceTexture2D;

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

    private Color32[] GetPixelsFromSource()
    {
        var offsetPosition = Input.mousePosition;
    }

}
