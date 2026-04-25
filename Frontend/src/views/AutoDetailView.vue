<script setup>
import { ref, onMounted } from 'vue'
import { useRoute, RouterLink } from 'vue-router'
import { autoService } from '@/services/api.js'

const route = useRoute()
const auto = ref(null)
const isLoading = ref(true)
const foutmelding = ref(null)

onMounted(async () => {
  try {
    auto.value = await autoService.getById(route.params.id)
  } catch (error) {
    foutmelding.value = 'Kon de auto niet ophalen.'
    console.error(error)
  } finally {
    isLoading.value = false
  }
})
</script>

<template>
  <div class="detail-pagina">

    <!-- Terug knop -->
    <RouterLink to="/autos" class="terug-link">← Terug naar overzicht</RouterLink>

    <!-- Laden -->
    <div v-if="isLoading" class="status">⏳ Auto wordt geladen...</div>

    <!-- Fout -->
    <div v-else-if="foutmelding" class="status status--fout">❌ {{ foutmelding }}</div>

    <!-- Detail kaart -->
    <div v-else-if="auto" class="detail-kaart">
      <div class="detail-kaart__afbeelding">
        <img v-if="auto.afbeeldingUrl" :src="auto.afbeeldingUrl" :alt="auto.merk + ' ' + auto.model" />
        <div v-else class="placeholder-afbeelding">🚗</div>
      </div>

      <div class="detail-kaart__inhoud">
        <h1>{{ auto.merk }} {{ auto.model }}</h1>

        <span class="badge" :class="auto.beschikbaar ? 'badge--groen' : 'badge--rood'">
          {{ auto.beschikbaar ? 'Beschikbaar' : 'Niet beschikbaar' }}
        </span>

        <table class="specs-tabel">
          <tr>
            <th>Bouwjaar</th>
            <td>{{ auto.bouwjaar }}</td>
          </tr>
          <tr>
            <th>Brandstof</th>
            <td>{{ auto.brandstof ?? '—' }}</td>
          </tr>
          <tr>
            <th>Zitplaatsen</th>
            <td>{{ auto.aantalZitplaatsen ?? '—' }}</td>
          </tr>
          <tr>
            <th>Kenteken</th>
            <td>{{ auto.kenteken ?? '—' }}</td>
          </tr>
          <tr>
            <th>Prijs per dag</th>
            <td><strong>€ {{ auto.prijsPerDag }}</strong></td>
          </tr>
        </table>

        <RouterLink
          v-if="auto.beschikbaar"
          :to="{ path: '/reservatie', query: { autoId: auto.id, merk: auto.merk, model: auto.model, prijs: auto.prijsPerDag } }"
          class="btn btn--reserveer"
        >
          Reserveer nu
        </RouterLink>

        <p v-else class="niet-beschikbaar-tekst">
          Deze auto is momenteel niet beschikbaar voor verhuur.
        </p>
      </div>
    </div>
  </div>
</template>

<style scoped>
.detail-pagina {
  max-width: 900px;
  margin: 0 auto;
  padding: 2rem 1rem;
}

.terug-link {
  display: inline-block;
  margin-bottom: 1.5rem;
  color: #2c7be5;
  text-decoration: none;
  font-size: 0.95rem;
}

.terug-link:hover {
  text-decoration: underline;
}

.status {
  text-align: center;
  padding: 3rem;
  font-size: 1.1rem;
  color: #555;
}

.status--fout {
  background-color: #f8d7da;
  color: #721c24;
  border-radius: 8px;
}

.detail-kaart {
  display: grid;
  grid-template-columns: 1fr 1fr;
  gap: 2rem;
  background: white;
  border-radius: 12px;
  overflow: hidden;
  box-shadow: 0 4px 16px rgba(0,0,0,0.1);
}

.detail-kaart__afbeelding {
  background: #f5f5f5;
  min-height: 300px;
  display: flex;
  align-items: center;
  justify-content: center;
  overflow: hidden;
}

.detail-kaart__afbeelding img {
  width: 100%;
  height: 100%;
  object-fit: cover;
}

.placeholder-afbeelding {
  font-size: 6rem;
}

.detail-kaart__inhoud {
  padding: 2rem;
}

.detail-kaart__inhoud h1 {
  font-size: 1.8rem;
  margin-bottom: 0.8rem;
  color: #222;
}

.badge {
  display: inline-block;
  padding: 0.3rem 0.8rem;
  border-radius: 20px;
  font-size: 0.85rem;
  font-weight: 600;
  margin-bottom: 1.5rem;
}

.badge--groen { background: #d4edda; color: #155724; }
.badge--rood  { background: #f8d7da; color: #721c24; }

.specs-tabel {
  width: 100%;
  border-collapse: collapse;
  margin-bottom: 1.5rem;
}

.specs-tabel th,
.specs-tabel td {
  padding: 0.6rem 0.5rem;
  border-bottom: 1px solid #f0f0f0;
  text-align: left;
  font-size: 0.95rem;
}

.specs-tabel th {
  color: #888;
  font-weight: 500;
  width: 40%;
}

.btn {
  display: inline-block;
  padding: 0.7rem 1.6rem;
  border-radius: 8px;
  font-size: 1rem;
  font-weight: 600;
  text-decoration: none;
  cursor: pointer;
  transition: background-color 0.2s;
}

.btn--reserveer {
  background-color: #2c7be5;
  color: white;
  width: 100%;
  text-align: center;
  box-sizing: border-box;
}

.btn--reserveer:hover {
  background-color: #1a5fbb;
}

.niet-beschikbaar-tekst {
  color: #721c24;
  font-style: italic;
  font-size: 0.95rem;
}

@media (max-width: 700px) {
  .detail-kaart {
    grid-template-columns: 1fr;
  }
}
</style>
