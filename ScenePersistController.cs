using JetBrains.Annotations;
using UnityEngine;

public class ScenePersistController : MonoBehaviour
{
    void Awake()
    {
        int numOfScenePersists = FindObjectsByType<ScenePersistController>(FindObjectsSortMode.None).Length;
        if (numOfScenePersists > 1)
        {
            Destroy(gameObject);
        }
        else
        {
            DontDestroyOnLoad(gameObject);
        }

    }

    public void ResetScene()
    {
        Destroy(gameObject);
    }
}

        

