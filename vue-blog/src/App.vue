<script setup>
import { RouterView } from 'vue-router'
</script>

<template>
  <div id="app">
    <header>
      <nav class="navbar">
        <div class="nav-container">
          <router-link to="/" class="nav-logo">Vue Blog</router-link>
          <ul class="nav-menu">
            <li class="nav-item">
              <router-link to="/" class="nav-link">Home</router-link>
            </li>
            <li class="nav-item">
              <router-link to="/about" class="nav-link">About</router-link>
            </li>
            <li class="nav-item">
              <router-link to="/contact" class="nav-link">Contact</router-link>
            </li>
          </ul>
        </div>
      </nav>
    </header>
    
    <main>
      <RouterView />
    </main>
    
    <footer>
      <div class="footer-content">
        <p>&copy; 2025 Vue Blog. All rights reserved.</p>
      </div>
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
  line-height: 1.6;
  color: #333;
}

#app {
  min-height: 100vh;
  display: flex;
  flex-direction: column;
}

.navbar {
  background-color: #333;
  padding: 1rem 0;
  position: sticky;
  top: 0;
  z-index: 100;
  box-shadow: 0 2px 4px rgba(0,0,0,0.1);
}

.nav-container {
  max-width: 1200px;
  margin: 0 auto;
  display: flex;
  justify-content: space-between;
  align-items: center;
  padding: 0 2rem;
}

.nav-logo {
  color: #fff;
  font-size: 1.5rem;
  font-weight: bold;
  text-decoration: none;
}

.nav-menu {
  display: flex;
  list-style: none;
}

.nav-link {
  color: #fff;
  text-decoration: none;
  padding: 0.5rem 1rem;
  margin: 0 0.5rem;
  border-radius: 4px;
  transition: background-color 0.3s;
}

.nav-link:hover, .nav-link.router-link-active {
  background-color: #555;
}

main {
  flex: 1;
  max-width: 1200px;
  margin: 0 auto;
  padding: 2rem;
  width: 100%;
}

footer {
  background-color: #333;
  color: #fff;
  text-align: center;
  padding: 1.5rem;
  margin-top: auto;
}

@media (max-width: 768px) {
  .nav-container {
    flex-direction: column;
    padding: 0 1rem;
  }
  
  .nav-menu {
    margin-top: 1rem;
  }
  
  main {
    padding: 1rem;
  }
}
</style>
