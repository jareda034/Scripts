using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.SceneManagement;

public class PauseMenuController : MonoBehaviour
{
  [SerializeField] GameObject pauseMenu;
  [SerializeField] GameObject ControlsPanel;
    [SerializeField] GameObject ControlsKeyboard;
    [SerializeField] GameObject ControlsController;
    GameManager gameManager;
    PlayerMovement player;
    AudioSource audioSource;
    bool menuBeingUsed = false;
    [Header("UI Sound Settings")]
    [SerializeField] AudioClip uiSfx;
    [SerializeField][Range(0,1)] float uiVolume;


    void Awake()
    {
        gameManager = FindFirstObjectByType<GameManager>();
        player = FindFirstObjectByType<PlayerMovement>();
        audioSource = GetComponent<AudioSource>();
    }

  public void Resume()
    {
        Time.timeScale = 1f;
        pauseMenu.SetActive(false);
        PlayAudio(uiSfx, uiVolume);
    }

    public void Quit()
    {
        SceneManager.LoadScene("Main Menu");
        Destroy(gameManager.gameObject);
        Destroy(player.gameObject);
        Time.timeScale = 1f;
        PlayAudio(uiSfx, uiVolume);
    }

    void CloseMenu(GameObject menu)
    {
        menu.SetActive(false);
    }

    void OpenMenu(GameObject menu)
    {
        menu.SetActive(true);
        
    }

        public void OpenControls()
    {
        OpenMenu(ControlsPanel);
        CloseMenu(pauseMenu);
        menuBeingUsed = true;
        PlayAudio(uiSfx, uiVolume);
        
    }

    public void CloseControls()
    {
        OpenMenu(pauseMenu);
        CloseMenu(ControlsPanel);
        menuBeingUsed = false;
        PlayAudio(uiSfx, uiVolume);
    }

    public void OpenKeyboardControls()
    {
        OpenMenu(ControlsKeyboard);
        CloseMenu(ControlsPanel);
        PlayAudio(uiSfx, uiVolume);
        
    }

    public void CloseKeyboardControls()
    {
        OpenMenu(ControlsPanel);
        CloseMenu(ControlsKeyboard);
        PlayAudio(uiSfx, uiVolume);
        
    }

     public void OpenControllerControls()
    {
        OpenMenu(ControlsController);
        CloseMenu(ControlsPanel);
        PlayAudio(uiSfx, uiVolume);
        
    }

    public void CloseControllerControls()
    {
        OpenMenu(ControlsPanel);
        CloseMenu(ControlsController);
        PlayAudio(uiSfx, uiVolume);
       
    }

    void PlayAudio(AudioClip clip, float volume)
    {
        if(clip != null)
        {
          audioSource.PlayOneShot(clip, volume);
        }
    }

    public bool MenuBeingUsed()
    {
         return menuBeingUsed;
    }
}
