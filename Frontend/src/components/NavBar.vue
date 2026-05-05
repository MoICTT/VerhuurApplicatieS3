<script setup>
import { RouterLink } from 'vue-router'
</script>

<template>
  <nav class="navbar">
    <div class="navbar__logo">
      <RouterLink to="/">🚗 VerhuurApp</RouterLink>
    </div>

    <ul class="navbar__links">
      <li><RouterLink to="/">Home</RouterLink></li>
      <li><RouterLink to="/autos">Auto's</RouterLink></li>
      <li><RouterLink to="/login">Inloggen</RouterLink></li>
    </ul>
  </nav>
</template>

<style scoped>
.navbar {
  display: flex;
  align-items: center;
  justify-content: space-between;
  padding: 1rem 2rem;
  background-color: #1a1a2e;
  color: white;
  box-shadow: 0 2px 8px rgba(0,0,0,0.2);
}

.navbar__logo a {
  font-size: 1.3rem;
  font-weight: 700;
  color: white;
  text-decoration: none;
  letter-spacing: 0.5px;
}

.navbar__links {
  list-style: none;
  display: flex;
  gap: 1.5rem;
  margin: 0;
  padding: 0;
}

.navbar__links a {
  color: #ccc;
  text-decoration: none;
  font-size: 0.95rem;
  transition: color 0.2s;
}

.navbar__links a:hover,
.navbar__links a.router-link-active {
  color: white;
}
</style>
