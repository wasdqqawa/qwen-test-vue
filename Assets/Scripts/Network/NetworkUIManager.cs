using UnityEngine;
using UnityEngine.UI;

public class NetworkUIManager : MonoBehaviour
{
    [Header("UI Elements")]
    public GameObject connectionPanel;  // 现在由HTML主菜单处理，但仍保留以兼容性
    public Button hostButton;
    public Button joinButton;
    public Button singlePlayerButton;  // 添加单人模式按钮
    public InputField roomIdInput;
    public Text statusText;
    public Text playerCountText;
    
    [Header("Game UI")]
    public GameObject gameUI;
    
    private bool isConnected = false;
    private bool isSinglePlayerMode = false;  // 添加单人模式标志

    void Start()
    {
        // 现在连接面板由HTML主菜单处理，所以默认隐藏Unity中的连接面板
        if (connectionPanel != null) connectionPanel.SetActive(false);
        
        // 初始化游戏UI
        if (gameUI != null) gameUI.SetActive(true);
        if (statusText != null) statusText.text = "Connected";
        if (playerCountText != null) playerCountText.text = "Players: 1";
        
        // 初始化按钮回调（保留以支持直接从场景启动）
        SetupButtonCallbacks();
    }

    void SetupButtonCallbacks()
    {
        if (hostButton != null)
        {
            hostButton.onClick.AddListener(OnHostButtonClicked);
        }
        
        if (joinButton != null && roomIdInput != null)
        {
            joinButton.onClick.AddListener(OnJoinButtonClicked);
        }
        
        if (singlePlayerButton != null)
        {
            singlePlayerButton.onClick.AddListener(OnSinglePlayerButtonClicked);
        }
    }

    void OnHostButtonClicked()
    {
        if (WebSocketNetworkManager.Instance != null)
        {
            WebSocketNetworkManager.Instance.StartHost();
            isConnected = true;
            
            if (statusText != null) statusText.text = "Hosting Game";
            if (connectionPanel != null) connectionPanel.SetActive(false);
            if (gameUI != null) gameUI.SetActive(true);
            
            InvokeRepeating("UpdatePlayerCount", 0f, 1f); // 每秒更新玩家数量
        }
    }

    void OnSinglePlayerButtonClicked()
    {
        // 直接进入单人模式，不启动网络连接
        isSinglePlayerMode = true;
        isConnected = true;
        
        // 启动WebSocketNetworkManager的单人模式
        if (WebSocketNetworkManager.Instance != null)
        {
            WebSocketNetworkManager.Instance.StartSinglePlayerMode();
        }
        
        if (statusText != null) statusText.text = "Single Player Mode";
        if (connectionPanel != null) connectionPanel.SetActive(false);
        if (gameUI != null) gameUI.SetActive(true);
        
        // 在单人模式下，不需要更新玩家数量
    }
    
    // 供JavaScript调用的静态方法
    public static void StartSinglePlayerFromJS()
    {
        if (Instance != null)
        {
            Instance.OnSinglePlayerButtonClicked();
        }
    }

    void OnJoinButtonClicked()
    {
        if (WebSocketNetworkManager.Instance != null && !string.IsNullOrEmpty(roomIdInput.text))
        {
            WebSocketNetworkManager.Instance.JoinGame(roomIdInput.text);
            isConnected = true;
            
            if (statusText != null) statusText.text = "Joined Game: " + roomIdInput.text;
            if (connectionPanel != null) connectionPanel.SetActive(false);
            if (gameUI != null) gameUI.SetActive(true);
            
            InvokeRepeating("UpdatePlayerCount", 0f, 1f); // 每秒更新玩家数量
        }
    }

    void UpdatePlayerCount()
    {
        if (playerCountText != null && WebSocketNetworkManager.Instance != null)
        {
            int playerCount = WebSocketNetworkManager.Instance.GetPlayerCount();
            playerCountText.text = "Players: " + playerCount;
        }
    }

    void Update()
    {
        // 更新连接状态
        if (!isSinglePlayerMode && WebSocketNetworkManager.Instance != null)
        {
            if (!WebSocketNetworkManager.Instance.IsConnected() && isConnected)
            {
                // 连接断开
                isConnected = false;
                CancelInvoke("UpdatePlayerCount");
                
                if (statusText != null) statusText.text = "Connection Lost";
                if (connectionPanel != null) connectionPanel.SetActive(true);
                if (gameUI != null) gameUI.SetActive(false);
            }
        }
    }

    void OnDestroy()
    {
        CancelInvoke("UpdatePlayerCount");
    }
}