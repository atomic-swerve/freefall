using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text;

public class TextBoxUI : UIComponent {

    public delegate void OnCloseCallback();
    private OnCloseCallback onCloseCallbacks;

	public int fontColorKey;
	public Rect textLineRelativeBounds;

	public string[] text;
	private int currentTextIndex;

	public int CurrentTextIndex {
		get {
			if ((currentTextIndex < text.Length) && (currentTextIndex >= 0)) {
				return currentTextIndex;
			} else if (currentTextIndex >= 0) {
				return text.Length - 1;
			} else if (currentTextIndex < (-text.Length)) { // negative index wraparound
				return text.Length + currentTextIndex;
			} else {
				return 0;
			}
		}

		set {
			if ((value < -text.Length) || (value >= text.Length)) {
				throw new IndexOutOfRangeException("CurrentTextIndex must be between -text.length and text.length - 1");
			}
			currentTextIndex = value;
		}
	}

	public int FontColorKey {
		get {
			return fontColorKey;
		}
		set {
			if (value > 3) {
				value = 3;
			} else if (value < 0) {
				value = 0;
			}
		}
	}

	private IList<string> readSourceFile(string filename) {
		IList<string> lines = new List<string>();
		try {
			string line;
			StreamReader reader = new StreamReader(filename, Encoding.Default);
			using (reader) {
				do {
					line = reader.ReadLine();
					if (line != null) {
						line = line.Trim();
						if (line.Length == 0) {
							continue;
						}
						lines.Add(line);
					}
				} while (line != null);
			}

		} catch (Exception e) {
			Debug.LogException(e);
		}
		return lines;
	}

    public void AddOnCloseCallback(OnCloseCallback callback) {
        onCloseCallbacks += callback;
    }
    
    public void RemoveOnCloseCallback(OnCloseCallback callback) {
        onCloseCallbacks -= callback;
    }
    
	public void SetTextSource(string textSource) {
		string filename = "Assets/Data/" + textSource;
		IList<string> lines = readSourceFile(filename);
		int count = lines.Count;
		if (count < 1) {
			Debug.LogWarning("empty or non-existent text file specified");
		}
		text = new string[count];
		lines.CopyTo(text, 0);
		currentTextIndex = 0;
	}

	public void Show(string textSource = null) {
		if (textSource != null) {
			SetTextSource(textSource);
		}
		gameObject.SetActive(true);
	}

	public void Hide() {
		gameObject.SetActive(false);
	}

	public void Update() {
		if (Input.GetButtonDown("A")) {
			if (currentTextIndex < (text.Length - 2)) {
				currentTextIndex += 2;
			} else {
				currentTextIndex = 0;
                if (onCloseCallbacks != null) {
                    onCloseCallbacks();
                }
				Hide();
			}
		}
	}

	void OnGUI() {
		Rect context = new Rect(0, 0, Screen.width, Screen.height);

		if (spriteRenderer == null) {
			Debug.LogWarning("TextBoxUI expected local SpriteRenderer; none was found");
		} else {
			Sprite sprite = spriteRenderer.sprite;
			if (sprite == null){
				Debug.LogWarning("TextBoxUI expected Sprite in SpriteRenderer; none was found");
			}
			context = sprite.rect;
			float posX = (Screen.width / 6) + (transform.position.x + sprite.bounds.center.x - sprite.bounds.extents.x);
			float posY = (Screen.height / 6) + ((transform.position.y * -1) - sprite.bounds.center.y - sprite.bounds.extents.y);
			context.x = posX;
			context.y = posY;
		}

		Rect shape1 = new Rect((context.x + textLineRelativeBounds.x) * 3, (context.y + textLineRelativeBounds.y) * 3,
		                       textLineRelativeBounds.width * 3, textLineRelativeBounds.height * 3);
		Rect shape2 = new Rect(shape1.x, shape1.y + shape1.height, shape1.width, shape1.height);

		GUIStyle style = guiSkin.FindStyle("TextArea");
		style.normal.textColor = GetPaletteColor(fontColorKey);
		string textLine1 =  "";
		string textLine2 =  "";
		if (text.Length > 0) {
			textLine1 = text[CurrentTextIndex];
			if (text.Length > (CurrentTextIndex + 1)) {
				textLine2 = text[CurrentTextIndex + 1];
			}
		}
		GUI.TextArea(shape1, textLine1, guiSkin.FindStyle("TextArea"));
		GUI.TextArea(shape2, textLine2, guiSkin.FindStyle("TextArea"));
	}
}
