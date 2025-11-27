using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainMenuUI : MonoBehaviour
{
    [Header("Main Menu UI Elements")]
    public GameObject mainMenuPanel;
    public Button singlePlayerButton;
    public Button multiplayerButton;
    public Button quitButton;
    
    [Header("Multiplayer Menu UI Elements")]
    public GameObject multiplayerMenuPanel;
    public Button hostButton;
    public Button joinButton;
    public Button backButton;
    public InputField roomIdInput;
    
    [Header("Game Scene")]
    public string gameSceneName = "SampleScene";

    void Start()
    {
        InitializeUI();
        SetupButtonCallbacks();
    }

    void InitializeUI()
    {
        // 确保所有面板初始状态正确
        if (mainMenuPanel != null) mainMenuPanel.SetActive(true);
        if (multiplayerMenuPanel != null) multiplayerMenuPanel.SetActive(false);
    }

    void SetupButtonCallbacks()
    {
        // 主菜单按钮事件
        if (singlePlayerButton != null)
        {
            singlePlayerButton.onClick.AddListener(OnSinglePlayerButtonClicked);
        }
        
        if (multiplayerButton != null)
        {
            multiplayerButton.onClick.AddListener(OnMultiplayerButtonClicked);
        }
        
        if (quitButton != null)
        {
            quitButton.onClick.AddListener(OnQuitButtonClicked);
        }
        
        // 多人游戏菜单按钮事件
        if (hostButton != null)
        {
            hostButton.onClick.AddListener(OnHostButtonClicked);
        }
        
        if (joinButton != null && roomIdInput != null)
        {
            joinButton.onClick.AddListener(OnJoinButtonClicked);
        }
        
        if (backButton != null)
        {
            backButton.onClick.AddListener(OnBackButtonClicked);
        }
    }

    void OnSinglePlayerButtonClicked()
    {
        // 设置网络管理器为单人模式
        WebSocketNetworkManager.Instance?.StartSinglePlayerMode();
        
        // 加载游戏场景
        SceneManager.LoadScene(gameSceneName);
    }

    void OnMultiplayerButtonClicked()
    {
        // 切换到多人游戏菜单
        if (mainMenuPanel != null) mainMenuPanel.SetActive(false);
        if (multiplayerMenuPanel != null) multiplayerMenuPanel.SetActive(true);
    }

    void OnHostButtonClicked()
    {
        // 设置网络管理器为主机模式
        WebSocketNetworkManager.Instance?.StartHost();
        
        // 加载游戏场景
        SceneManager.LoadScene(gameSceneName);
    }

    void OnJoinButtonClicked()
    {
        if (!string.IsNullOrEmpty(roomIdInput.text))
        {
            // 设置网络管理器为加入游戏模式
            WebSocketNetworkManager.Instance?.JoinGame(roomIdInput.text);
            
            // 加载游戏场景
            SceneManager.LoadScene(gameSceneName);
        }
    }

    void OnBackButtonClicked()
    {
        // 返回主菜单
        if (multiplayerMenuPanel != null) multiplayerMenuPanel.SetActive(false);
        if (mainMenuPanel != null) mainMenuPanel.SetActive(true);
    }

    void OnQuitButtonClicked()
    {
#if UNITY_WEBGL && !UNITY_EDITOR
        // 在WebGL中，关闭标签页
        UnityEngine.WebGLTemplates.WebGLUI.CloseWindow();
#else
        // 在编辑器或其他平台中退出应用
        Application.Quit();
#endif
    }
}