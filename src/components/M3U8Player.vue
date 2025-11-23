<template>
  <div class="player-container">
    <h2>M3U8播放器</h2>
    
    <div class="input-section">
      <input 
        v-model="streamUrl" 
        type="text" 
        placeholder="请输入M3U8直播流地址，例如: https://example.com/playlist.m3u8"
        class="url-input"
      />
      <button @click="loadStream" class="load-btn">加载流</button>
    </div>
    
    <div class="player-wrapper" v-if="streamUrl">
      <video 
        ref="videoRef" 
        class="video-player" 
        controls 
        playsinline
        :poster="defaultPoster"
      >
        您的浏览器不支持视频播放
      </video>
    </div>
    
    <div class="presets-section">
      <h3>预设流地址</h3>
      <div class="preset-buttons">
        <button 
          v-for="preset in presets" 
          :key="preset.name"
          @click="loadPreset(preset.url)"
          class="preset-btn"
        >
          {{ preset.name }}
        </button>
      </div>
    </div>
    
    <div class="instructions">
      <h3>使用说明</h3>
      <ul>
        <li>输入有效的M3U8直播流地址</li>
        <li>点击"加载流"按钮开始播放</li>
        <li>点击预设按钮可快速加载常用流</li>
        <li>支持大部分HLS格式的直播流</li>
      </ul>
    </div>
  </div>
</template>

<script setup>
import { ref, onMounted, onUnmounted } from 'vue';
import Hls from 'hls.js';

const streamUrl = ref('');
const videoRef = ref(null);
let hls = null;

// 预设流地址
const presets = ref([
  { name: '测试流1', url: 'https://test-streams.mux.dev/x36xhzz/x36xhzz.m3u8' },
  { name: 'Apple测试流', url: 'https://devstreaming-cdn.apple.com/videos/streaming/examples/bipbop_4x3/bipbop_4x3_variant.m3u8' },
  { name: 'Apple测试流2', url: 'https://devstreaming-cdn.apple.com/videos/streaming/examples/bipbop_16x9/bipbop_16x9_variant.m3u8' },
  { name: 'Big Buck Bunny', url: 'https://commondatastorage.googleapis.com/gtv-videos-bucket/sample/AppleAndOranges.m3u8' }
]);

// 默认海报图片
const defaultPoster = 'https://via.placeholder.com/800x450/3498db/ffffff?text=在线M3U8播放器';

const loadStream = () => {
  if (!streamUrl.value) {
    alert('请输入M3U8流地址');
    return;
  }
  
  loadVideo(streamUrl.value);
};

const loadPreset = (url) => {
  streamUrl.value = url;
  loadVideo(url);
};

const loadVideo = (url) => {
  // 销毁之前的播放器实例
  if (hls) {
    hls.destroy();
  }
  
  const video = videoRef.value;
  if (!video) return;
  
  if (video.canPlayType('application/vnd.apple.mpegurl')) {
    // Safari原生支持HLS
    video.src = url;
  } else if (Hls.isSupported()) {
    // 使用hls.js
    hls = new Hls();
    hls.loadSource(url);
    hls.attachMedia(video);
    
    hls.on(Hls.Events.MANIFEST_PARSED, () => {
      video.play().catch(error => {
        console.error('播放失败:', error);
        alert('自动播放失败，请手动点击播放按钮');
      });
    });
    
    hls.on(Hls.Events.ERROR, (event, data) => {
      console.error('HLS错误:', event, data);
      if (data.fatal) {
        switch(data.type) {
          case Hls.ErrorTypes.NETWORK_ERROR:
            alert('网络错误，正在尝试重连...');
            hls.startLoad();
            break;
          case Hls.ErrorTypes.MEDIA_ERROR:
            alert('媒体错误，正在尝试恢复...');
            hls.recoverMediaError();
            break;
          default:
            console.error('未知错误类型:', data.type);
            alert('播放出错，请检查流地址是否有效');
            break;
        }
      }
    });
  } else {
    alert('您的浏览器不支持HLS播放，请尝试使用Chrome、Firefox或Edge浏览器');
  }
};

onUnmounted(() => {
  if (hls) {
    hls.destroy();
  }
});
</script>

<style scoped>
.player-container {
  max-width: 1000px;
  margin: 0 auto;
  padding: 20px;
}

.player-container h2 {
  text-align: center;
  color: #333;
  margin-bottom: 20px;
}

.input-section {
  display: flex;
  gap: 10px;
  margin-bottom: 30px;
  flex-wrap: wrap;
}

.url-input {
  flex: 1;
  padding: 12px;
  border: 2px solid #ddd;
  border-radius: 6px;
  font-size: 16px;
  min-width: 300px;
}

.load-btn {
  padding: 12px 24px;
  background: #3498db;
  color: white;
  border: none;
  border-radius: 6px;
  cursor: pointer;
  font-size: 16px;
  transition: background 0.3s;
}

.load-btn:hover {
  background: #2980b9;
}

.player-wrapper {
  margin: 20px 0;
  text-align: center;
}

.video-player {
  width: 100%;
  max-width: 800px;
  height: auto;
  border-radius: 8px;
  box-shadow: 0 4px 15px rgba(0, 0, 0, 0.2);
  background: #000;
}

.presets-section {
  margin: 30px 0;
}

.presets-section h3 {
  margin-bottom: 15px;
  color: #333;
}

.preset-buttons {
  display: flex;
  flex-wrap: wrap;
  gap: 10px;
  justify-content: center;
}

.preset-btn {
  padding: 10px 15px;
  background: #2ecc71;
  color: white;
  border: none;
  border-radius: 5px;
  cursor: pointer;
  transition: background 0.3s;
}

.preset-btn:hover {
  background: #27ae60;
}

.instructions {
  margin-top: 30px;
  padding: 20px;
  background: #f8f9fa;
  border-radius: 8px;
}

.instructions h3 {
  color: #333;
  margin-bottom: 10px;
}

.instructions ul {
  padding-left: 20px;
}

.instructions li {
  margin: 8px 0;
  color: #555;
}

@media (max-width: 768px) {
  .input-section {
    flex-direction: column;
  }
  
  .url-input {
    min-width: 100%;
  }
  
  .preset-buttons {
    flex-direction: column;
    align-items: center;
  }
  
  .preset-btn {
    width: 100%;
    max-width: 300px;
  }
}
</style>