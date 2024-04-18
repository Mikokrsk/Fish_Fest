using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class UIHandler : MonoBehaviour
{
    [SerializeField] public UIDocument _uiDocument;
    [SerializeField] public GameMode _gameMode = GameMode.MainMenu;
    [SerializeField] private GameObject _MainMenuUIObject;
    [SerializeField] private GameObject _GameUIObject;
    [SerializeField] private Button _ToPortButton;
    public static UIHandler Instance { get; private set; }

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(this.gameObject);
        }
        //  DontDestroyOnLoad(Instance.gameObject);
    }

    private void Start()
    {
        _ToPortButton = _uiDocument.rootVisualElement.Q<Button>("ToPortButton");

        if (SceneManager.GetActiveScene().name == "MainMenu")
        {
            ChangeGameMode(GameMode.MainMenu);
        }
        else
        {
            ChangeGameMode(GameMode.Game);
        }
        if (SceneManager.GetActiveScene().name == "Ocean")
        {
            _ToPortButton.clicked += () => LoadLevelManager.Instance.LoadLevel(1);
            _ToPortButton.style.display = DisplayStyle.Flex;
        }
        else
        {
            _ToPortButton.style.display = DisplayStyle.None;
        }
    }

    public void ChangeGameMode(GameMode gameMode)
    {
        _gameMode = gameMode;

        if (_gameMode == GameMode.MainMenu)
        {
            SetMainMenuActive(true);
            SetGameMenuActive(false);
        }
        else
        {
            SetMainMenuActive(false);
            SetGameMenuActive(true);
        }
    }

    public void SetMainMenuActive(bool active)
    {
        _MainMenuUIObject.SetActive(active);
    }

    public void SetGameMenuActive(bool active)
    {
        _GameUIObject.SetActive(active);
    }
}

public enum GameMode
{
    Game,
    MainMenu
}