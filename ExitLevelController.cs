using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class ExitLevelController : MonoBehaviour
{
    
 float waitTime = 2f;

    void OnTriggerEnter2D(Collider2D other)
    {

        StartCoroutine(LoadNextScene());
        
    }

    IEnumerator LoadNextScene()
    {
        yield return new WaitForSeconds(waitTime);
        PlayerMovement player = FindAnyObjectByType<PlayerMovement>();
        if (player != null)
        {
         int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
         int nextSceneIndex = currentSceneIndex + 1;
         if (nextSceneIndex == SceneManager.sceneCountInBuildSettings)
            {
                nextSceneIndex = 0;
            }
            FindFirstObjectByType<ScenePersistController>().ResetScene();
            SceneManager.LoadScene(nextSceneIndex);
        }
    }
}
