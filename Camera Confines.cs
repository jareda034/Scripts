using Unity.Cinemachine;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CameraConfines : MonoBehaviour
{
  CinemachineConfiner2D confiner;

  void Awake()
    {
        confiner = GetComponent<CinemachineConfiner2D>();
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    void OnDestroy()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        GameObject bounds = GameObject.FindGameObjectWithTag("Bounds");
        if (bounds != null)
        {
            confiner.BoundingShape2D = bounds.GetComponent<Collider2D>();
        }
        else
        {
            confiner.InvalidateBoundingShapeCache();
        }
    }
}
