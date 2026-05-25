using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.SceneManagement;

public class MainMenuController : MonoBehaviour
{
    [SerializeField] GameObject ControlsPanel;
    [SerializeField] GameObject ControlsKeyboard;
    [SerializeField] GameObject ControlsController;
    [SerializeField] GameObject MainMenu;
    bool menuBeingUsed = false;
    [Header("UI Sound Settings")]
    [SerializeField] AudioClip uiSfx;
    [SerializeField][Range(0,1)] float uiVolume;

    void Awake()
    {
        ControlsPanel.SetActive(false);
        ControlsController.SetActive(false);
        ControlsKeyboard.SetActive(false);
    }

    public void StartGame()
    {
        SceneManager.LoadScene("GrassLand");
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    void CloseMenu(GameObject menu)
    {
        if (menuBeingUsed)
        {
            return;
        }
        menu.SetActive(false);
        PlayAudio(uiSfx, uiVolume);
    }

    void OpenMenu(GameObject menu)
    {
        menu.SetActive(true);
        PlayAudio(uiSfx, uiVolume);
    }

    public void OpenControls()
    {
        OpenMenu(ControlsPanel);
        CloseMenu(MainMenu);
        menuBeingUsed = true;
        PlayAudio(uiSfx, uiVolume);

    }

    public void CloseControls()
    {
        OpenMenu(MainMenu);
        CloseMenu(ControlsPanel);
        menuBeingUsed = false;
        PlayAudio(uiSfx, uiVolume);
    }

    public void OpenKeyboardControls()
    {
        OpenMenu(ControlsKeyboard);
        CloseMenu(ControlsPanel);
        menuBeingUsed = true;
        PlayAudio(uiSfx, uiVolume);
    }

    public void CloseKeyboardControls()
    {
        OpenMenu(ControlsPanel);
        CloseMenu(ControlsKeyboard);
        menuBeingUsed = true;
        PlayAudio(uiSfx, uiVolume);
    }

     public void OpenControllerControls()
    {
        OpenMenu(ControlsController);
        CloseMenu(ControlsPanel);
        menuBeingUsed = true;
        PlayAudio(uiSfx, uiVolume);
    }

    public void CloseControllerControls()
    {
        OpenMenu(ControlsPanel);
        CloseMenu(ControlsController);
        menuBeingUsed = true;
        PlayAudio(uiSfx, uiVolume);
    }

    void PlayAudio(AudioClip clip, float volume)
    {
        AudioSource.PlayClipAtPoint(clip, transform.position, volume);
    }
}
