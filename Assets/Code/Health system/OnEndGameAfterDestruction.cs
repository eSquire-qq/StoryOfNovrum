using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class OnEndGameAfterDestruction : AOnDestruction
{
    protected override void OnDestruction()
    {
        Time.timeScale = 1f;
		SceneManager.LoadScene("MainMenu");
    }
}
