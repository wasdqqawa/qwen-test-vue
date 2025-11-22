<template>
  <div class="blog-post" v-if="post">
    <article class="post-content">
      <header class="post-header">
        <h1>{{ post.title }}</h1>
        <div class="post-meta">
          <span>By {{ post.author }}</span>
          <span>{{ formatDate(post.date) }}</span>
          <div class="post-tags">
            <span class="tag" v-for="tag in post.tags" :key="tag">{{ tag }}</span>
          </div>
        </div>
      </header>
      
      <div class="post-body" v-html="post.content"></div>
    </article>
    
    <div class="post-navigation">
      <router-link 
        v-if="prevPost" 
        :to="`/post/${prevPost.id}`" 
        class="nav-link prev"
      >
        ← {{ prevPost.title }}
      </router-link>
      
      <router-link to="/" class="nav-link home">Back to Blog</router-link>
      
      <router-link 
        v-if="nextPost" 
        :to="`/post/${nextPost.id}`" 
        class="nav-link next"
      >
        {{ nextPost.title }} →
      </router-link>
    </div>
  </div>
  
  <div class="not-found" v-else>
    <h2>Post not found</h2>
    <p>The blog post you're looking for doesn't exist.</p>
    <router-link to="/" class="back-link">Back to Blog</router-link>
  </div>
</template>

<script setup>
import { ref, onMounted, computed } from 'vue'
import { useRoute } from 'vue-router'

const route = useRoute()
const post = ref(null)

// Sample blog posts data (same as in Home.vue)
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
      <div class="code-block">
        <pre><code>// Example of Composition API
import { ref, reactive } from 'vue'

export default {
  setup() {
    const count = ref(0)
    const state = reactive({ name: 'Vue' })
    
    return {
      count,
      state
    }
  }
}</code></pre>
      </div>
      <h3>Reactivity System</h3>
      <p>Vue 3 uses a more efficient reactivity system based on ES6 Proxies, which provides better performance and more reliable change detection.</p>
      <h3>Teleport and Suspense</h3>
      <p>New features like Teleport and Suspense provide more flexibility in component design and asynchronous component handling.</p>
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
      <p>Modern web development involves a combination of frameworks, build tools, and best practices to create efficient, maintainable applications.</p>
      <h3>Build Tools</h3>
      <p>Modern build tools like Vite, Webpack, and Rollup optimize your application for production by handling asset processing, code splitting, and dependency management.</p>
      <h3>Component Architecture</h3>
      <p>Building applications with reusable components improves maintainability and scalability. Each component should have a single responsibility and clear interfaces.</p>
      <h3>State Management</h3>
      <p>For complex applications, proper state management is crucial. Vue provides reactivity out of the box, and for larger applications, tools like Pinia or Vuex can be used.</p>
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
      <p>CSS Grid and Flexbox are two powerful layout systems that serve different purposes and can often be used together.</p>
      <h3>CSS Grid</h3>
      <p>Grid is ideal for two-dimensional layouts - controlling both rows and columns. It's perfect for page layouts, complex component structures, and dashboard interfaces.</p>
      <div class="code-block">
        <pre><code>.grid-container {
  display: grid;
  grid-template-columns: repeat(3, 1fr);
  grid-gap: 20px;
}</code></pre>
      </div>
      <h3>Flexbox</h3>
      <p>Flexbox is perfect for one-dimensional layouts - either in a row or column. It excels at distributing space and aligning items within a container.</p>
      <h3>When to Use Which</h3>
      <p>Use Grid for overall page layout and complex component structures. Use Flexbox for aligning items within components, navigation bars, and other one-dimensional layouts.</p>
    `,
    author: 'Alice Johnson',
    date: '2025-01-05',
    tags: ['CSS', 'Layout', 'Frontend']
  }
]

onMounted(() => {
  const postId = parseInt(route.params.id)
  post.value = samplePosts.find(p => p.id === postId)
})

const formatDate = (dateString) => {
  const options = { year: 'numeric', month: 'long', day: 'numeric' }
  return new Date(dateString).toLocaleDateString(undefined, options)
}

// Compute previous and next posts for navigation
const prevPost = computed(() => {
  if (!post.value) return null
  const currentIndex = samplePosts.findIndex(p => p.id === post.value.id)
  return currentIndex > 0 ? samplePosts[currentIndex - 1] : null
})

const nextPost = computed(() => {
  if (!post.value) return null
  const currentIndex = samplePosts.findIndex(p => p.id === post.value.id)
  return currentIndex < samplePosts.length - 1 ? samplePosts[currentIndex + 1] : null
})
</script>

<style scoped>
.blog-post {
  max-width: 800px;
  margin: 0 auto;
}

.post-header {
  margin-bottom: 2rem;
  padding-bottom: 1rem;
  border-bottom: 1px solid #eee;
}

.post-header h1 {
  color: #333;
  margin-bottom: 1rem;
  font-size: 2.2rem;
}

.post-meta {
  display: flex;
  flex-wrap: wrap;
  gap: 1rem;
  color: #666;
  font-size: 0.9rem;
}

.post-tags {
  display: flex;
  gap: 0.5rem;
}

.tag {
  background-color: #f0f4f8;
  padding: 0.25rem 0.75rem;
  border-radius: 20px;
  font-size: 0.8rem;
}

.post-body {
  line-height: 1.8;
  color: #444;
}

.post-body h2 {
  color: #333;
  margin: 2rem 0 1rem 0;
  font-size: 1.8rem;
}

.post-body h3 {
  color: #444;
  margin: 1.5rem 0 0.75rem 0;
  font-size: 1.4rem;
}

.post-body p {
  margin-bottom: 1rem;
}

.code-block {
  background-color: #f8f9fa;
  border-radius: 4px;
  padding: 1rem;
  overflow-x: auto;
  margin: 1.5rem 0;
  border: 1px solid #eee;
}

.code-block pre {
  margin: 0;
  font-size: 0.9rem;
}

.not-found {
  text-align: center;
  padding: 3rem 1rem;
}

.not-found h2 {
  color: #333;
  margin-bottom: 1rem;
}

.back-link {
  display: inline-block;
  margin-top: 1rem;
  color: #667eea;
  text-decoration: none;
  padding: 0.5rem 1rem;
  border: 1px solid #667eea;
  border-radius: 4px;
}

.back-link:hover {
  background-color: #667eea;
  color: white;
}

.post-navigation {
  display: flex;
  justify-content: space-between;
  margin-top: 3rem;
  padding-top: 2rem;
  border-top: 1px solid #eee;
}

.nav-link {
  color: #667eea;
  text-decoration: none;
  padding: 0.5rem 1rem;
  border: 1px solid #667eea;
  border-radius: 4px;
  transition: all 0.3s;
}

.nav-link:hover {
  background-color: #667eea;
  color: white;
}

.nav-link.home {
  color: #333;
  border-color: #333;
}

.nav-link.home:hover {
  background-color: #333;
  color: white;
}

@media (max-width: 768px) {
  .post-navigation {
    flex-direction: column;
    gap: 1rem;
    align-items: center;
  }
  
  .blog-post {
    padding: 1rem;
  }
}
</style>