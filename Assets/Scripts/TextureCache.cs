using UnityEngine;
using System.Collections.Generic;
using System;

public class TextureCache : MonoBehaviour
{
	#region Singleton
	private static TextureCache _instance = null;
	public static TextureCache Instance
	{
		get
		{
			if (_instance == null)
			{
				_instance = FindObjectOfType<TextureCache>();

				if (_instance == null)
				{
					GameObject obj = new GameObject("TextureCache");
					_instance = obj.AddComponent<TextureCache>();
					_instance._Initialize();
				}
			}

			return _instance;
		}
	}
	#endregion

	#region Public Variables
	public Dictionary<int, Tuple<Texture2D, Color32[]>> textureCache = new Dictionary<int, Tuple<Texture2D, Color32[]>>();
	#endregion

	#region Private Methods
	private void _Initialize() { }
	#endregion
}
