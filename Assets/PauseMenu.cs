using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public static bool GameIsPaused = false;
	public GameObject pauseMenuUI;
	protected PlayerInput pauseMenu;

	public void Resume()
	{
		pauseMenuUI.SetActive(false);
		Time.timeScale = 1f;
		GameIsPaused = false;
	}

	private void Pause()
	{
		pauseMenuUI.SetActive(true);
		Time.timeScale = 0f;
		GameIsPaused = true;
	}

	public void LoadMenu()
	{
		Time.timeScale = 1f;
		SceneManager.LoadScene("MainMenu");
	}

	public void QuitGame()
	{
		Debug.Log("Quiting game...");
		Application.Quit();
	}

	public void OnEnable()
	{
		pauseMenu.Enable();
		interaction = playerInput.Player.Interact;
		pauseMenu.performed += Interact;
	}

	public void OnDisable()
	{
		pauseMenu.Disable();
	}

}
