using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TexturePainter : MonoBehaviour
{
    public Texture2D SourceTexture;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    /// <summary>
    /// Caches the target texture and its pixels, to be used in all further texture operations in place of getting the pixels directly to
    /// dramatically improve performance. Call this before performing multiple consecutive texture operations on the same texture. Each call
    /// to BeginTextureOperation must follow the operations with a call to CompleteTextureOperation for the changes to be applied.
    /// 
    /// Setting overwrite to false will refrain from overwriting an entry for the target texture if one already exists. Use this when
    /// performing texture operations in Update on the same texture for further optimization.
    /// </summary>
    /// <param name="target"></param>
    /// <param name="overwrite"></param>
    public static void CacheTexture(Texture2D target, bool overwrite = true)
    {
        bool containsKey = TextureCache.Instance.textureCache.ContainsKey(target.GetInstanceID());
        if ((containsKey && overwrite) || !containsKey)
        {
            TextureCache.Instance.textureCache[target.GetInstanceID()] = new Tuple<Texture2D, Color32[]>(target, target.GetPixels32());
        }
    }

    public static void UpdateCachedTexture(Texture2D target, Color32[] pixels)
    {
        if (TextureCache.Instance.textureCache.ContainsKey(target.GetInstanceID()))
        {
            var instance = TextureCache.Instance.textureCache[target.GetInstanceID()];
            TextureCache.Instance.textureCache[target.GetInstanceID()] = new Tuple<Texture2D, Color32[]>(instance.Item1, pixels);

        }
    }
}
