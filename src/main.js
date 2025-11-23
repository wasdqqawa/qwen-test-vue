import { createApp } from 'vue'
import { createRouter, createWebHistory } from 'vue-router'
import App from './App.vue'

// Import M3U8 Player component
import M3U8Player from './components/M3U8Player.vue'

const routes = [
  { path: '/', component: M3U8Player },
]

const router = createRouter({
  history: createWebHistory('./'), // 使用相对路径用于GitHub Pages部署
  routes
})

const app = createApp(App)

app.use(router)

app.mount('#app')