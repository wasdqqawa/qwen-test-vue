using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 10f;
    public float jumpForce = 10f;
    public Transform groundCheck;
    public LayerMask groundMask;
    public float positionSyncRate = 0.1f; // 位置同步频率

    private CharacterController characterController;
    private Vector3 moveDirection;
    private bool isGrounded;
    private float gravity = -9.81f;
    private float verticalVelocity;
    private float lastPositionSyncTime;
    private NetworkPlayerController networkPlayerController;

    void Start()
    {
        characterController = GetComponent<CharacterController>();
        moveDirection = Vector3.zero;
        
        // 获取NetworkPlayerController组件
        networkPlayerController = GetComponent<NetworkPlayerController>();
        if (networkPlayerController == null)
        {
            networkPlayerController = gameObject.AddComponent<NetworkPlayerController>();
        }
        
        // 设置为本地玩家
        networkPlayerController.SetAsLocalPlayer();
        networkPlayerController.SetPlayerId(WebSocketNetworkManager.Instance?.GetLocalPlayerId() ?? "local");
    }

    void Update()
    {
        // 检查是否在地面上
        isGrounded = Physics.CheckSphere(groundCheck.position, 0.1f, groundMask);
        
        if(isGrounded && verticalVelocity < 0)
        {
            verticalVelocity = -2f;
        }

        // 水平移动
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        Vector3 forward = transform.TransformDirection(Vector3.forward);
        Vector3 right = transform.TransformDirection(Vector3.right);

        moveDirection = (forward * vertical + right * horizontal).normalized * moveSpeed;

        // 跳跃
        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            verticalVelocity = Mathf.Sqrt(jumpForce * -2f * gravity);
        }

        // 应用重力
        verticalVelocity += gravity * Time.deltaTime;
        moveDirection.y = verticalVelocity;

        // 移动角色
        characterController.Move(moveDirection * Time.deltaTime);
        
        // 同步位置到网络
        if (WebSocketNetworkManager.Instance != null && WebSocketNetworkManager.Instance.IsConnected())
        {
            if (Time.time - lastPositionSyncTime >= positionSyncRate)
            {
                WebSocketNetworkManager.Instance.SendPlayerPosition(transform.position, transform.eulerAngles);
                lastPositionSyncTime = Time.time;
            }
        }
    }
}