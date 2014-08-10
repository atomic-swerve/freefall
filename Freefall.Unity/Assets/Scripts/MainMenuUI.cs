using UnityEngine;
using System.Collections;

public class MainMenuUI : MonoBehaviour {

	public MenuUI Menu;

	public CreditsUI Credits;

	void Start () {
		if ((Menu == null) || (Credits == null)) {
			throw new MissingReferenceException("must have both Menu and Credits");
		}
	}
	
	public void ShowCredits() {
		Credits.gameObject.SetActive(true);
		Menu.gameObject.SetActive(false);
	}

	public void ShowMenu() {
		Credits.gameObject.SetActive(false);
		Menu.gameObject.SetActive(true);
	}
}
