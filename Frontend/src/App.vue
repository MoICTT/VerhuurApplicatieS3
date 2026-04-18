<script setup>
import { RouterView } from 'vue-router'
import NavBar from '@/components/NavBar.vue'
</script>

<template>
  <NavBar />
  <main class="pagina-inhoud">
    <RouterView />
  </main>
</template>

<style scoped>
.pagina-inhoud {
  min-height: calc(100vh - 60px);
  background-color: #f7f8fa;
}
</style>
