<script setup>
import { ref, onMounted } from 'vue'
import AutoCard from '@/components/AutoCard.vue'
import { autoService } from '@/services/api.js'

const autos = ref([])
const isLoading = ref(true)
const foutmelding = ref(null)

async function haalAutosOp() {
  isLoading.value = true
  foutmelding.value = null

  try {
    autos.value = await autoService.getAll()
  } catch (error) {
    foutmelding.value = 'Kon de auto\'s niet ophalen. Controleer of de backend actief is.'
    console.error(error)
  } finally {
    isLoading.value = false
  }
}

onMounted(() => {
  haalAutosOp()
})
</script>

<template>
  <div class="autos-pagina">
    <div class="autos-pagina__header">
      <h1>Beschikbare auto's</h1>
      <p>Kies een auto en maak eenvoudig een reservatie.</p>
    </div>

    <!-- Laden -->
    <div v-if="isLoading" class="autos-pagina__status">
      <p>⏳ Auto's worden geladen...</p>
    </div>

    <!-- Foutmelding -->
    <div v-else-if="foutmelding" class="autos-pagina__status autos-pagina__status--fout">
      <p>❌ {{ foutmelding }}</p>
      <button class="btn btn--opnieuw" @click="haalAutosOp">Opnieuw proberen</button>
    </div>

    <!-- Geen auto's gevonden -->
    <div v-else-if="autos.length === 0" class="autos-pagina__status">
      <p>😕 Er zijn momenteel geen auto's beschikbaar.</p>
    </div>

    <!-- Auto's grid -->
    <div v-else class="autos-grid">
      <AutoCard
        v-for="auto in autos"
        :key="auto.id"
        :auto="auto"
      />
    </div>
  </div>
</template>

<style scoped>
.autos-pagina {
  max-width: 1100px;
  margin: 0 auto;
  padding: 2rem 1rem;
}

.autos-pagina__header {
  margin-bottom: 2rem;
}

.autos-pagina__header h1 {
  font-size: 2rem;
  color: #222;
  margin-bottom: 0.4rem;
}

.autos-pagina__header p {
  color: #666;
  font-size: 1rem;
}

.autos-pagina__status {
  text-align: center;
  padding: 3rem;
  color: #555;
  font-size: 1.1rem;
}

.autos-pagina__status--fout {
  color: #721c24;
  background-color: #f8d7da;
  border-radius: 8px;
}

.autos-grid {
  display: grid;
  grid-template-columns: repeat(auto-fill, minmax(280px, 1fr));
  gap: 1.5rem;
}

.btn {
  display: inline-block;
  padding: 0.5rem 1.2rem;
  border-radius: 6px;
  font-size: 0.9rem;
  font-weight: 600;
  cursor: pointer;
  border: none;
  margin-top: 1rem;
}

.btn--opnieuw {
  background-color: #721c24;
  color: white;
}

.btn--opnieuw:hover {
  background-color: #4e1219;
}
</style>
