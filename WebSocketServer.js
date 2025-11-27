// 简单的WebSocket服务器，用于处理多人游戏连接
const WebSocket = require('ws');

// 创建WebSocket服务器
const wss = new WebSocket.Server({ port: 8080 });

console.log('WebSocket服务器已启动，端口 8080');

wss.on('connection', (ws, req) => {
    console.log('新客户端连接');
    
    // 解析URL参数
    const url = new URL(req.url, `http://${req.headers.host}`);
    const roomId = url.searchParams.get('roomId') || 'default';
    const playerId = url.searchParams.get('playerId');
    const mode = url.searchParams.get('mode');
    
    console.log(`客户端信息 - Room: ${roomId}, Player: ${playerId}, Mode: ${mode}`);
    
    // 存储客户端信息
    ws.roomId = roomId;
    ws.playerId = playerId;
    ws.mode = mode;
    
    // 将客户端添加到房间
    if (!wss.rooms) wss.rooms = {};
    if (!wss.rooms[roomId]) wss.rooms[roomId] = new Set();
    wss.rooms[roomId].add(ws);
    
    // 向房间内其他客户端广播新玩家加入
    wss.rooms[roomId].forEach(client => {
        if (client !== ws && client.readyState === WebSocket.OPEN) {
            client.send(JSON.stringify({
                type: 'player_joined',
                playerId: playerId,
                roomId: roomId
            }));
        }
    });
    
    // 处理接收到的消息
    ws.on('message', (message) => {
        console.log(`收到消息: ${message}`);
        
        // 将消息转发到同房间的其他客户端
        if (wss.rooms && wss.rooms[roomId]) {
            wss.rooms[roomId].forEach(client => {
                if (client !== ws && client.readyState === WebSocket.OPEN) {
                    client.send(message);
                }
            });
        }
    });
    
    // 处理连接关闭
    ws.on('close', () => {
        console.log(`客户端断开连接 - Player: ${playerId}`);
        
        // 从房间中移除客户端
        if (wss.rooms && wss.rooms[roomId]) {
            wss.rooms[roomId].delete(ws);
            
            // 如果房间为空，删除房间
            if (wss.rooms[roomId].size === 0) {
                delete wss.rooms[roomId];
            } else {
                // 向房间内其他客户端广播玩家离开
                wss.rooms[roomId].forEach(client => {
                    if (client.readyState === WebSocket.OPEN) {
                        client.send(JSON.stringify({
                            type: 'player_left',
                            playerId: playerId,
                            roomId: roomId
                        }));
                    }
                });
            }
        }
    });
    
    // 处理错误
    ws.on('error', (error) => {
        console.error('WebSocket错误:', error);
    });
});

console.log('WebSocket服务器监听中...');