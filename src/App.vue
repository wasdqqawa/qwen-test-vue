<script setup>
import { RouterView } from 'vue-router'
</script>

<template>
  <div id="app">
    <header class="header">
      <div class="header-container">
        <h1 class="logo">在线M3U8播放器</h1>
      </div>
    </header>

    <main>
      <RouterView />
    </main>

    <footer class="footer">
      <p>&copy; 2025 在线M3U8播放器. 版权所有.</p>
    </footer>
  </div>
</template>

<style>
* {
  margin: 0;
  padding: 0;
  box-sizing: border-box;
}

body {
  font-family: 'Segoe UI', Tahoma, Geneva, Verdana, sans-serif;
  background: linear-gradient(135deg, #f5f7fa 0%, #c3cfe2 100%);
  min-height: 100vh;
}

#app {
  min-height: 100vh;
  display: flex;
  flex-direction: column;
}

.header {
  background: linear-gradient(90deg, #343a40 0%, #495057 100%);
  padding: 1rem 0;
  box-shadow: 0 4px 6px rgba(0,0,0,0.1);
  position: sticky;
  top: 0;
  z-index: 100;
}

.header-container {
  max-width: 1200px;
  margin: 0 auto;
  display: flex;
  justify-content: center;
  align-items: center;
  padding: 0 2rem;
}

.logo {
  color: #ffffff;
  font-size: 1.8rem;
  font-weight: bold;
  text-align: center;
}

main {
  flex: 1;
  max-width: 1200px;
  margin: 2rem auto;
  padding: 0 2rem;
  width: 100%;
  background-color: rgba(255, 255, 255, 0.9);
  border-radius: 10px;
  box-shadow: 0 5px 15px rgba(0,0,0,0.08);
  padding: 2rem;
}

.footer {
  background: linear-gradient(90deg, #343a40 0%, #495057 100%);
  color: #ffffff;
  text-align: center;
  padding: 1.5rem;
  margin-top: auto;
}

/* 响应式设计 */
@media (max-width: 768px) {
  .header-container {
    padding: 1rem;
  }
  
  main {
    margin: 1rem auto;
    padding: 1rem;
  }
}
</style>