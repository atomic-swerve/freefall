using UnityEngine;
using System.Collections;
using System;

public class UIComponent : MonoBehaviour {

	private Color[] colors = new Color[4];

	public GUISkin guiSkin;

	protected SpriteRenderer spriteRenderer;
	private bool hasPalette = false;

	void Start() {
		spriteRenderer = GetComponent<SpriteRenderer>();
		if (spriteRenderer == null) {
			Debug.LogWarning("UIComponent expected local SpriteRenderer; none was found");
		}
		Texture paletteTex = spriteRenderer.material.GetTexture("_PaletteTex");
		if (typeof(Texture2D).IsAssignableFrom(paletteTex.GetType())) {
			Texture2D palette = (Texture2D)paletteTex;
			colors[0] = palette.GetPixel(0, 1);
			colors[1] = palette.GetPixel(1, 1);
			colors[2] = palette.GetPixel(1, 0);
			colors[3] = palette.GetPixel(0, 0);
			hasPalette = true;
		}
	}

	public Color GetPaletteColor(int index, GUIStyleState styleBackup = null) {
		if (!hasPalette) {
			if (styleBackup != null) {
				return styleBackup.textColor;
			}
			return guiSkin.FindStyle("TextArea").normal.textColor;
		}
		if ((index >= 0) && (index < 4)) {
			return colors[index];
		}
		else {
			throw new IndexOutOfRangeException("index must be between " + 0 + " and " + 4);
		}
	}
}
