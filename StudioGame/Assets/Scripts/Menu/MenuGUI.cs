using UnityEngine;
// For Namespacing
using UnityEngine.UI;
using System.Collections;
//to load scenes
using UnityEngine.SceneManagement;

public class MenuGUI : MonoBehaviour 
{
	//Buttons 
	public Canvas QuitMenu;
	public Button PlayBtn;
	public Button OptionBtn;
	public Button CreditsBtn;
	public Button ExitBtn;
	public Button MainMenuBtn;


	// Use this for initialization
	void Start () 
	{
		QuitMenu = QuitMenu.GetComponent<Canvas> ();
		PlayBtn = PlayBtn.GetComponent<Button> ();
		OptionBtn = OptionBtn.GetComponent<Button> ();
		CreditsBtn = CreditsBtn.GetComponent<Button> ();
		ExitBtn = ExitBtn.GetComponent<Button> ();
		MainMenuBtn = MainMenuBtn.GetComponent<Button> ();
		// so Quit menu is hidden by default
		QuitMenu.enabled = false;

	}
	
	// exit button
	public void ExitPress() 
	{
		QuitMenu.enabled = true;
		PlayBtn.enabled = false;
		OptionBtn.enabled = false;
		CreditsBtn.enabled = false;
		ExitBtn.enabled = false;
		MainMenuBtn.enabled = false;

	}

	public void NoPress() 
	{
		QuitMenu.enabled = false;
		PlayBtn.enabled = true;
		OptionBtn.enabled = true;
		CreditsBtn.enabled = true;
		ExitBtn.enabled = true;
		MainMenuBtn.enabled = true;
	}

	public void LoadScene()//	public void LoadScene("Play")
	{
		SceneManager.LoadScene ("Level1");
	}

	public void LoadScene("Options")
	{
		SceneManager.LoadScene ("Options");
	}

	public void LoadScene()
	{
		SceneManager.LoadScene ("Credits");
	}

	public void LoadScene()
	{
		SceneManager.LoadScene ("MainMenu");
	}

	public void ExitGame()
	{
		Appelication.Quit();
	}


}
