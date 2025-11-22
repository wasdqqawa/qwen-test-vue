<template>
  <div class="contact">
    <h1>Contact Us</h1>
    <div class="contact-content">
      <div class="contact-info">
        <h2>Get in Touch</h2>
        <p>Have questions or feedback? We'd love to hear from you!</p>
        
        <div class="contact-details">
          <div class="contact-item">
            <h3>Email</h3>
            <p>contact@vueblog.com</p>
          </div>
          <div class="contact-item">
            <h3>Location</h3>
            <p>San Francisco, CA</p>
          </div>
        </div>
      </div>
      
      <form class="contact-form" @submit.prevent="submitForm">
        <div class="form-group">
          <label for="name">Name</label>
          <input type="text" id="name" v-model="form.name" required>
        </div>
        
        <div class="form-group">
          <label for="email">Email</label>
          <input type="email" id="email" v-model="form.email" required>
        </div>
        
        <div class="form-group">
          <label for="subject">Subject</label>
          <input type="text" id="subject" v-model="form.subject" required>
        </div>
        
        <div class="form-group">
          <label for="message">Message</label>
          <textarea id="message" v-model="form.message" rows="5" required></textarea>
        </div>
        
        <button type="submit" class="submit-btn">Send Message</button>
      </form>
    </div>
  </div>
</template>

<script setup>
import { ref } from 'vue'

const form = ref({
  name: '',
  email: '',
  subject: '',
  message: ''
})

const submitForm = () => {
  // In a real application, this would send the form data to a server
  alert('Thank you for your message! (This is a demo - no actual submission occurred)')
  form.value = {
    name: '',
    email: '',
    subject: '',
    message: ''
  }
}
</script>

<style scoped>
.contact {
  max-width: 1000px;
  margin: 0 auto;
}

.contact h1 {
  text-align: center;
  margin-bottom: 2rem;
  color: #333;
}

.contact-content {
  display: grid;
  grid-template-columns: 1fr 1fr;
  gap: 2rem;
  background: white;
  padding: 2rem;
  border-radius: 8px;
  box-shadow: 0 4px 6px rgba(0, 0, 0, 0.1);
}

.contact-info h2 {
  color: #333;
  margin-bottom: 1rem;
}

.contact-info p {
  color: #666;
  margin-bottom: 1.5rem;
  line-height: 1.6;
}

.contact-details {
  margin-top: 1.5rem;
}

.contact-item {
  margin-bottom: 1.5rem;
}

.contact-item h3 {
  color: #333;
  margin-bottom: 0.25rem;
}

.contact-item p {
  color: #666;
  margin: 0;
}

.contact-form {
  display: flex;
  flex-direction: column;
}

.form-group {
  margin-bottom: 1.5rem;
}

.form-group label {
  display: block;
  margin-bottom: 0.5rem;
  color: #333;
  font-weight: 500;
}

.form-group input,
.form-group textarea {
  width: 100%;
  padding: 0.75rem;
  border: 1px solid #ddd;
  border-radius: 4px;
  font-size: 1rem;
  transition: border-color 0.3s;
}

.form-group input:focus,
.form-group textarea:focus {
  outline: none;
  border-color: #667eea;
}

.submit-btn {
  background-color: #667eea;
  color: white;
  padding: 0.75rem 1.5rem;
  border: none;
  border-radius: 4px;
  font-size: 1rem;
  cursor: pointer;
  transition: background-color 0.3s;
}

.submit-btn:hover {
  background-color: #5a6fd8;
}

@media (max-width: 768px) {
  .contact-content {
    grid-template-columns: 1fr;
    padding: 1.5rem;
  }
  
  .contact {
    padding: 1rem;
  }
}
</style>