# Minecraft-Style 3D WebGL Game (重构版)

这是一个使用Unity开发的Minecraft风格3D WebGL游戏，支持单人和多人模式，使用WebSocket进行网络通信。

## 特性

- **Minecraft风格的3D世界** - 可以破坏和放置方块
- **多人游戏支持** - 通过WebSocket实现的实时多人游戏
- **Firefox兼容** - 使用WebSocket替代WebRTC，确保在Firefox等浏览器中正常运行
- **主菜单界面** - 类似Minecraft的主菜单，可以选择单人或多人模式

## 技术架构

- **前端**: Unity 3D游戏引擎，导出为WebGL
- **网络**: WebSocket协议进行实时通信
- **后端**: Node.js WebSocket服务器

## 运行说明

### 1. 启动WebSocket服务器

```bash
npm install
npm start
```

或者

```bash
./start_server.sh
```

服务器将运行在 `ws://localhost:8080`

### 2. 在浏览器中打开游戏

打开 `index.html` 文件即可开始游戏。

### 3. 游戏模式

- **单人模式**: 直接在本地玩，无需网络连接
- **多人模式**: 
  - **创建游戏**: 作为主机创建一个房间
  - **加入游戏**: 输入房间ID加入其他玩家的房间

## 控制方式

- **WASD/方向键**: 移动
- **鼠标**: 视角控制
- **空格键**: 跳跃
- **左键**: 破坏方块
- **右键**: 放置方块

## 网络通信

游戏使用WebSocket进行网络通信，支持以下消息类型：

- 方块更新消息 (BlockUpdateMessage)
- 玩家位置消息 (PlayerPositionMessage)

## Firefox兼容性

通过使用WebSocket替代WebRTC，确保了在Firefox浏览器中的兼容性。WebSocket是标准的Web API，在所有现代浏览器中都有良好支持。

## 项目结构

- `index.html`: 主HTML页面，包含Minecraft风格的主菜单
- `WebSocketServer.js`: WebSocket服务器实现
- `start_server.sh`: 服务器启动脚本
- `Assets/Scripts/`: Unity C#脚本
  - `WebSocketNetworkManager.cs`: WebSocket网络管理器
  - `NetworkUIManager.cs`: 网络UI管理器
  - `MainMenuUI.cs`: 主菜单UI管理器
  - `PlayerController.cs`: 玩家控制器
  - `BlockInteraction.cs`: 方块交互逻辑
  - `World.cs`: 世界生成和管理
  - `BlockType.cs`: 方块类型定义

## 开发说明

此项目已重构为使用WebSocket替代WebRTC，以提供更好的浏览器兼容性（特别是Firefox）。网络架构设计为客户端-服务器模式，所有玩家通过中央WebSocket服务器进行通信。
