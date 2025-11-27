using UnityEngine;
using System.Collections.Generic;

public class NetworkPlayerManager : MonoBehaviour
{
    public static NetworkPlayerManager Instance;
    
    [Header("Player Prefabs")]
    public GameObject remotePlayerPrefab;
    public Material localPlayerMaterial;
    public Material remotePlayerMaterial;
    
    private Dictionary<string, GameObject> networkPlayers = new Dictionary<string, GameObject>();
    
    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
    
    void Start()
    {
        // 订阅网络事件 - 通过WebSocketNetworkManager的事件
        if (WebSocketNetworkManager.Instance != null)
        {
            WebSocketNetworkManager.Instance.OnNetworkMessageReceived += OnNetworkMessageReceived;
        }
    }
    
    void OnDestroy()
    {
        // 取消订阅网络事件
        if (WebSocketNetworkManager.Instance != null)
        {
            WebSocketNetworkManager.Instance.OnNetworkMessageReceived -= OnNetworkMessageReceived;
        }
        
        if (Instance == this)
        {
            Instance = null;
        }
        
        // 清理所有网络玩家
        foreach (GameObject player in networkPlayers.Values)
        {
            if (player != null)
            {
                DestroyImmediate(player);
            }
        }
        networkPlayers.Clear();
    }
    
    public void AddNetworkPlayer(string playerId, Vector3 position, Vector3 rotation)
    {
        if (networkPlayers.ContainsKey(playerId))
        {
            // 如果玩家已存在，更新其位置
            UpdateNetworkPlayerPosition(playerId, position, rotation);
            return;
        }
        
        if (remotePlayerPrefab != null)
        {
            GameObject playerObj = Instantiate(remotePlayerPrefab, position, Quaternion.Euler(rotation));
            NetworkPlayerController playerController = playerObj.GetComponent<NetworkPlayerController>();
            
            if (playerController != null)
            {
                playerController.SetPlayerId(playerId);
                playerController.playerId = playerId;
                
                // 设置远程玩家材质
                if (remotePlayerMaterial != null)
                {
                    Renderer playerRenderer = playerObj.GetComponent<Renderer>();
                    if (playerRenderer != null)
                    {
                        playerRenderer.material = remotePlayerMaterial;
                    }
                }
            }
            
            networkPlayers[playerId] = playerObj;
        }
    }
    
    public void RemoveNetworkPlayer(string playerId)
    {
        if (networkPlayers.ContainsKey(playerId))
        {
            GameObject playerObj = networkPlayers[playerId];
            if (playerObj != null)
            {
                DestroyImmediate(playerObj);
            }
            networkPlayers.Remove(playerId);
        }
    }
    
    public void UpdateNetworkPlayerPosition(string playerId, Vector3 position, Vector3 rotation)
    {
        if (networkPlayers.ContainsKey(playerId))
        {
            GameObject playerObj = networkPlayers[playerId];
            if (playerObj != null)
            {
                NetworkPlayerController controller = playerObj.GetComponent<NetworkPlayerController>();
                if (controller != null)
                {
                    controller.UpdatePlayerPosition(position, rotation);
                }
                else
                {
                    // 直接更新位置作为备选方案
                    playerObj.transform.position = position;
                    playerObj.transform.eulerAngles = rotation;
                }
            }
        }
    }
    
    public void OnNetworkMessageReceived(string jsonMessage)
    {
        try
        {
            // 处理玩家位置更新消息
            if (jsonMessage.Contains("\"playerId\"") && jsonMessage.Contains("\"position\"") && jsonMessage.Contains("\"rotation\""))
            {
                PlayerPositionMessage playerMsg = JsonUtility.FromJson<PlayerPositionMessage>(jsonMessage);
                
                // 忽略本地玩家的消息
                if (playerMsg.playerId != WebSocketNetworkManager.Instance?.GetLocalPlayerId())
                {
                    // 添加或更新网络玩家
                    if (!networkPlayers.ContainsKey(playerMsg.playerId))
                    {
                        AddNetworkPlayer(playerMsg.playerId, playerMsg.position, playerMsg.rotation);
                    }
                    else
                    {
                        UpdateNetworkPlayerPosition(playerMsg.playerId, playerMsg.position, playerMsg.rotation);
                    }
                }
            }
        }
        catch (System.Exception e)
        {
            Debug.LogError("Error processing network message: " + e.Message);
        }
    }
    
    void OnDestroy()
    {
        if (Instance == this)
        {
            Instance = null;
        }
        
        // 清理所有网络玩家
        foreach (GameObject player in networkPlayers.Values)
        {
            if (player != null)
            {
                DestroyImmediate(player);
            }
        }
        networkPlayers.Clear();
    }
}