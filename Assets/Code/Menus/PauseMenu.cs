using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;

public class PauseMenu : MonoBehaviour
{
	protected PlayerInput playerInput;
	protected InputAction menu;

	[SerializeField] private GameObject PauseMenuWindow;
    public bool gameIsPaused;

	public void Awake()
	{
		playerInput = new PlayerInput();
	}

	public void Resume()
	{
		PauseMenuWindow.SetActive(false);
		Time.timeScale = 1f;
		gameIsPaused = false;
	}

	public void Pause(InputAction.CallbackContext context)
	{
		gameIsPaused = !gameIsPaused;

		if(gameIsPaused)
		{
			ActivateMenu();
		}
		else
		{
			DeactivateMenu();
		}
	}

	public void LoadMenu()
	{
		Time.timeScale = 1f;
		SceneManager.LoadScene("MainMenu");
	}

	public void QuitGame()
	{
		Application.Quit();
	}

	public void OnEnable()
	{
		menu = playerInput.Menu.Escape;
		menu.Enable();
		menu.performed += Pause;
	}

	public void OnDisable()
	{
		menu.Disable();
	}

	public void ActivateMenu()
	{
		Time.timeScale = 0;
		PauseMenuWindow.SetActive(true);
	}

	public void DeactivateMenu()
	{
		Time.timeScale = 1;
		PauseMenuWindow.SetActive(false);
		gameIsPaused = false;
	}

}