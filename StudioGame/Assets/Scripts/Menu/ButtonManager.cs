using UnityEngine;
// to use SceneManager
using System.Collections; 
using UnityEngine.SceneManagement;

public class ButtonManager : MonoBehaviour
{
	// Button name and level to load
	public void PlayBtn(string Level1) 
    {
        SceneManager.LoadScene(Level1);
    }

    public void OptionBtn(string Option)
    {
		SceneManager.LoadScene(Option);
    }

    public void CreditsBtn(string newGameLevel)
    {
        SceneManager.LoadScene(newGameLevel);
    }

	public void BackBtn(string newGameLevel)
	{
		SceneManager.LoadScene(newGameLevel);
	}

    /*public void ExitBtn(string newGameLevel)
    {
        SceneManager.LoadScene(newGameLevel); //we dont load exit?
    }
    */
}
