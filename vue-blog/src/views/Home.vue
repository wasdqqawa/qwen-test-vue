<template>
  <div class="home">
    <section class="hero">
      <h1>Welcome to Vue Blog</h1>
      <p>A modern, responsive blog built with Vue.js and Vite</p>
    </section>
    
    <section class="blog-posts">
      <div class="post-card" v-for="post in posts" :key="post.id">
        <div class="post-header">
          <h2>{{ post.title }}</h2>
          <p class="post-meta">By {{ post.author }} on {{ formatDate(post.date) }}</p>
        </div>
        <p class="post-excerpt">{{ post.excerpt }}</p>
        <router-link :to="`/post/${post.id}`" class="read-more">Read More</router-link>
      </div>
    </section>
  </div>
</template>

<script setup>
import { ref, onMounted } from 'vue'

const posts = ref([])

// Sample blog posts data
const samplePosts = [
  {
    id: 1,
    title: 'Getting Started with Vue 3',
    excerpt: 'Learn the fundamentals of Vue 3 including Composition API, reactivity, and more.',
    content: `
      <h2>Introduction to Vue 3</h2>
      <p>Vue 3 is the latest major version of Vue.js, featuring improved performance, better TypeScript support, and new APIs.</p>
      <h3>Composition API</h3>
      <p>The Composition API is a set of additive, function-based APIs that allow flexible composition of component logic.</p>
      <h3>Reactivity System</h3>
      <p>Vue 3 uses a more efficient reactivity system based on ES6 Proxies.</p>
    `,
    author: 'Jane Doe',
    date: '2025-01-15',
    tags: ['Vue', 'JavaScript', 'Frontend']
  },
  {
    id: 2,
    title: 'Building Modern Web Applications',
    excerpt: 'Explore modern web development practices and tools that make building applications easier.',
    content: `
      <h2>Modern Web Development</h2>
      <p>Modern web development involves a combination of frameworks, build tools, and best practices.</p>
      <h3>Build Tools</h3>
      <p>Vite, Webpack, and Rollup are popular build tools that optimize your application for production.</p>
      <h3>Component Architecture</h3>
      <p>Building applications with reusable components improves maintainability and scalability.</p>
    `,
    author: 'John Smith',
    date: '2025-01-10',
    tags: ['Web Development', 'Tools', 'Architecture']
  },
  {
    id: 3,
    title: 'CSS Grid vs Flexbox',
    excerpt: 'Understanding when to use CSS Grid and Flexbox for different layout scenarios.',
    content: `
      <h2>CSS Layout Systems</h2>
      <p>CSS Grid and Flexbox are two powerful layout systems that serve different purposes.</p>
      <h3>CSS Grid</h3>
      <p>Grid is ideal for two-dimensional layouts - controlling both rows and columns.</p>
      <h3>Flexbox</h3>
      <p>Flexbox is perfect for one-dimensional layouts - either in a row or column.</p>
    `,
    author: 'Alice Johnson',
    date: '2025-01-05',
    tags: ['CSS', 'Layout', 'Frontend']
  }
]

onMounted(() => {
  // Simulate API call to fetch blog posts
  setTimeout(() => {
    posts.value = samplePosts
  }, 500)
})

const formatDate = (dateString) => {
  const options = { year: 'numeric', month: 'long', day: 'numeric' }
  return new Date(dateString).toLocaleDateString(undefined, options)
}
</script>

<style scoped>
.hero {
  text-align: center;
  padding: 3rem 1rem;
  background: linear-gradient(135deg, #667eea 0%, #764ba2 100%);
  color: white;
  margin-bottom: 2rem;
  border-radius: 8px;
}

.hero h1 {
  font-size: 2.5rem;
  margin-bottom: 1rem;
}

.hero p {
  font-size: 1.2rem;
  opacity: 0.9;
}

.blog-posts {
  display: grid;
  grid-template-columns: repeat(auto-fill, minmax(350px, 1fr));
  gap: 2rem;
  margin-top: 2rem;
}

.post-card {
  background: white;
  border-radius: 8px;
  box-shadow: 0 4px 6px rgba(0, 0, 0, 0.1);
  padding: 1.5rem;
  transition: transform 0.3s, box-shadow 0.3s;
}

.post-card:hover {
  transform: translateY(-5px);
  box-shadow: 0 10px 20px rgba(0, 0, 0, 0.15);
}

.post-header h2 {
  color: #333;
  margin-bottom: 0.5rem;
  font-size: 1.5rem;
}

.post-meta {
  color: #666;
  font-size: 0.9rem;
  margin-bottom: 1rem;
}

.post-excerpt {
  color: #555;
  line-height: 1.6;
  margin-bottom: 1rem;
}

.read-more {
  display: inline-block;
  color: #667eea;
  text-decoration: none;
  font-weight: 500;
}

.read-more:hover {
  text-decoration: underline;
}

@media (max-width: 768px) {
  .hero h1 {
    font-size: 2rem;
  }
  
  .blog-posts {
    grid-template-columns: 1fr;
  }
}
</style>