<template>
  <div class="about">
    <h1>About Vue Blog</h1>
    <div class="about-content">
      <p>Vue Blog is a modern, responsive blog platform built with Vue.js and Vite. It showcases the power and flexibility of the Vue ecosystem.</p>
      
      <h2>Features</h2>
      <ul>
        <li>Responsive design that works on all devices</li>
        <li>Modern Vue 3 with Composition API</li>
        <li>Vue Router for navigation</li>
        <li>Dynamic blog post rendering</li>
        <li>Tag-based content organization</li>
        <li>SEO-friendly structure</li>
      </ul>
      
      <h2>Technology Stack</h2>
      <div class="tech-stack">
        <div class="tech-item">
          <h3>Vue.js</h3>
          <p>The progressive JavaScript framework for building user interfaces.</p>
        </div>
        <div class="tech-item">
          <h3>Vite</h3>
          <p>Next-generation frontend tooling with fast hot module replacement.</p>
        </div>
        <div class="tech-item">
          <h3>Vue Router</h3>
          <p>The official router for Vue.js that enables SPA functionality.</p>
        </div>
      </div>
    </div>
  </div>
</template>

<script setup>
// About component logic can be added here if needed
</script>

<style scoped>
.about {
  max-width: 800px;
  margin: 0 auto;
}

.about h1 {
  text-align: center;
  margin-bottom: 2rem;
  color: #333;
}

.about-content {
  background: white;
  padding: 2rem;
  border-radius: 8px;
  box-shadow: 0 4px 6px rgba(0, 0, 0, 0.1);
}

.about-content p {
  margin-bottom: 1.5rem;
  line-height: 1.6;
  color: #555;
}

.about-content h2 {
  color: #333;
  margin: 1.5rem 0 1rem 0;
  border-bottom: 2px solid #eee;
  padding-bottom: 0.5rem;
}

.about-content ul {
  padding-left: 1.5rem;
  margin-bottom: 1.5rem;
}

.about-content li {
  margin-bottom: 0.5rem;
  line-height: 1.6;
}

.tech-stack {
  display: grid;
  grid-template-columns: repeat(auto-fit, minmax(250px, 1fr));
  gap: 1.5rem;
  margin-top: 1.5rem;
}

.tech-item {
  background: #f8f9fa;
  padding: 1.5rem;
  border-radius: 8px;
  border-left: 4px solid #667eea;
}

.tech-item h3 {
  color: #333;
  margin-bottom: 0.5rem;
}

.tech-item p {
  color: #666;
  margin: 0;
}

@media (max-width: 768px) {
  .about {
    padding: 1rem;
  }
  
  .about-content {
    padding: 1.5rem;
  }
}
</style>