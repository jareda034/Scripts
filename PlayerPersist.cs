using UnityEngine;

public class PlayerPersist : MonoBehaviour
{
    public static GameObject instance;
  void Awake()
    {
        if (instance == null)
        {
            instance = gameObject;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
}
