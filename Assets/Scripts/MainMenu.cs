using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Play : MonoBehaviour
{
  public void PlayGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1); // scene builder order (it is going to the next scene)
    }

    public void QuitMenu()
    {
        Application.Quit(); 
    }
}
