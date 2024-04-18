using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class MainMenu : MonoBehaviour
{

    [SerializeField] private VisualElement _mainMenuUI;
    //  [SerializeField] private SettingsMenu _settingsMenu;
    //   [SerializeField] private LoadLevelsMenu _loadLevelsMenu;
    public static MainMenu Instance { get; private set; }

    private void OnEnable()
    {
        UIHandler.Instance._uiDocument.rootVisualElement.Q<Button>("StartButton").clicked += () => LoadLevelManager.Instance.LoadLevel(1);
        /* if (Application.platform != RuntimePlatform.WebGLPlayer)
         {
             UIHandler.Instance._uiDocument.rootVisualElement.Q<Button>("ExitTheGameButton").clicked += ExitTheGame;
         }
         else
         {
             UIHandler.Instance._uiDocument.rootVisualElement.Q<Button>("ExitTheGameButton").style.display = DisplayStyle.None;
         }*/
        _mainMenuUI = UIHandler.Instance._uiDocument.rootVisualElement.Q<VisualElement>("MainMenuUI");
        _mainMenuUI.style.display = DisplayStyle.None;
        OpenMainMenu();
    }

    private void OnDisable()
    {
        CloseMainMenuUI();
    }

    private void Awake()
    {
        Instance = this;
    }

    public void OpenMainMenu()
    {
        _mainMenuUI.style.display = DisplayStyle.Flex;
    }

    /*    private void OpenLoadLevelsMenu()
        {
            _loadLevelsMenu.OpenLoadLevelsMenuUI();
        }*/

    private void CloseMainMenuUI()
    {
        //  _settingsMenu.CloseSettingsMenuUI();
        //   _loadLevelsMenu.CloseLoadLevelsMenuUI();
        _mainMenuUI.style.display = DisplayStyle.None;
        UIHandler.Instance._uiDocument.rootVisualElement.Q<Button>("StartButton").clicked -= () => LoadLevelManager.Instance.LoadLevel(1);
    }

    /*    private void ExitTheGame()
        {
    #if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
    #else
            Application.Quit();
    #endif
        }*/
}
