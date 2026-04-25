<script setup>
import { ref, computed } from 'vue'
import { useRoute, useRouter } from 'vue-router'
import { reservatieService } from '@/services/api.js'

const route = useRoute()
const router = useRouter()

// Info uit de URL query (meegegeven vanuit AutoDetailView)
const autoId   = route.query.autoId  ?? ''
const autoMerk = route.query.merk    ?? ''
const autoModel= route.query.model   ?? ''
const prijs    = Number(route.query.prijs ?? 0)

// Formuliervelden
const voornaam   = ref('')
const achternaam = ref('')
const email      = ref('')
const startDatum = ref('')
const eindDatum  = ref('')

// Status
const isVerzonden  = ref(false)
const foutmelding  = ref(null)
const isLoading    = ref(false)

// Totaalprijs berekenen
const aantalDagen = computed(() => {
  if (!startDatum.value || !eindDatum.value) return 0
  const start = new Date(startDatum.value)
  const eind  = new Date(eindDatum.value)
  const diff  = Math.ceil((eind - start) / (1000 * 60 * 60 * 24))
  return diff > 0 ? diff : 0
})

const totaalPrijs = computed(() => aantalDagen.value * prijs)

// Minimale datum = vandaag
const vandaag = new Date().toISOString().split('T')[0]

async function verstuurReservatie() {
  foutmelding.value = null

  if (!voornaam.value || !achternaam.value || !email.value || !startDatum.value || !eindDatum.value) {
    foutmelding.value = 'Vul alle velden in.'
    return
  }

  if (aantalDagen.value <= 0) {
    foutmelding.value = 'De einddatum moet na de startdatum liggen.'
    return
  }

  isLoading.value = true

  try {
    await reservatieService.maakReservatie({
      autoId,
      voornaam: voornaam.value,
      achternaam: achternaam.value,
      email: email.value,
      startDatum: startDatum.value,
      eindDatum: eindDatum.value,
    })

    router.push({
      path: '/reservatie/bevestiging',
      query: {
        merk: autoMerk,
        model: autoModel,
        startDatum: startDatum.value,
        eindDatum: eindDatum.value,
        totaal: totaalPrijs.value,
      }
    })
  } catch (error) {
    foutmelding.value = 'Er ging iets mis bij het versturen. Probeer opnieuw.'
    console.error(error)
  } finally {
    isLoading.value = false
  }
}
</script>

<template>
  <div class="reservatie-pagina">
    <h1>Reservatie maken</h1>

    <!-- Auto info -->
    <div v-if="autoMerk" class="auto-info">
      <span>🚗</span>
      <span>{{ autoMerk }} {{ autoModel }} — <strong>€ {{ prijs }} / dag</strong></span>
    </div>

    <form class="reservatie-form" @submit.prevent="verstuurReservatie" novalidate>

      <!-- Persoonlijke gegevens -->
      <fieldset>
        <legend>Uw gegevens</legend>

        <div class="form-rij">
          <div class="form-groep">
            <label for="voornaam">Voornaam</label>
            <input id="voornaam" v-model="voornaam" type="text" placeholder="Jan" required />
          </div>
          <div class="form-groep">
            <label for="achternaam">Achternaam</label>
            <input id="achternaam" v-model="achternaam" type="text" placeholder="Janssen" required />
          </div>
        </div>

        <div class="form-groep">
          <label for="email">E-mailadres</label>
          <input id="email" v-model="email" type="email" placeholder="jan@voorbeeld.nl" required />
        </div>
      </fieldset>

      <!-- Periode -->
      <fieldset>
        <legend>Huurperiode</legend>

        <div class="form-rij">
          <div class="form-groep">
            <label for="startDatum">Startdatum</label>
            <input id="startDatum" v-model="startDatum" type="date" :min="vandaag" required />
          </div>
          <div class="form-groep">
            <label for="eindDatum">Einddatum</label>
            <input id="eindDatum" v-model="eindDatum" type="date" :min="startDatum || vandaag" required />
          </div>
        </div>

        <!-- Prijsoverzicht -->
        <div v-if="aantalDagen > 0" class="prijs-overzicht">
          <span>{{ aantalDagen }} dag{{ aantalDagen !== 1 ? 'en' : '' }} × € {{ prijs }}</span>
          <strong>Totaal: € {{ totaalPrijs }}</strong>
        </div>
      </fieldset>

      <!-- Foutmelding -->
      <p v-if="foutmelding" class="foutmelding">❌ {{ foutmelding }}</p>

      <!-- Verstuur knop -->
      <button type="submit" class="btn btn--reserveer" :disabled="isLoading">
        {{ isLoading ? 'Bezig met versturen...' : 'Bevestig reservatie' }}
      </button>
    </form>
  </div>
</template>

<style scoped>
.reservatie-pagina {
  max-width: 680px;
  margin: 0 auto;
  padding: 2rem 1rem;
}

.reservatie-pagina h1 {
  font-size: 1.9rem;
  margin-bottom: 1rem;
  color: #222;
}

.auto-info {
  display: flex;
  align-items: center;
  gap: 0.6rem;
  background: #eef4ff;
  border: 1px solid #bee3f8;
  border-radius: 8px;
  padding: 0.8rem 1rem;
  margin-bottom: 1.5rem;
  font-size: 1rem;
  color: #1a5fbb;
}

.reservatie-form fieldset {
  border: 1px solid #e0e0e0;
  border-radius: 10px;
  padding: 1.2rem 1.5rem;
  margin-bottom: 1.5rem;
  background: white;
}

.reservatie-form legend {
  font-weight: 600;
  color: #333;
  padding: 0 0.5rem;
}

.form-rij {
  display: grid;
  grid-template-columns: 1fr 1fr;
  gap: 1rem;
}

.form-groep {
  display: flex;
  flex-direction: column;
  gap: 0.3rem;
  margin-bottom: 1rem;
}

.form-groep label {
  font-size: 0.9rem;
  font-weight: 500;
  color: #444;
}

.form-groep input {
  padding: 0.55rem 0.8rem;
  border: 1px solid #ccc;
  border-radius: 6px;
  font-size: 0.95rem;
  transition: border-color 0.2s;
}

.form-groep input:focus {
  outline: none;
  border-color: #2c7be5;
  box-shadow: 0 0 0 3px rgba(44,123,229,0.15);
}

.prijs-overzicht {
  display: flex;
  justify-content: space-between;
  align-items: center;
  background: #f0f7ff;
  border-radius: 8px;
  padding: 0.8rem 1rem;
  font-size: 0.95rem;
  color: #1a3a6b;
  margin-top: 0.5rem;
}

.foutmelding {
  background: #f8d7da;
  color: #721c24;
  border-radius: 8px;
  padding: 0.7rem 1rem;
  margin-bottom: 1rem;
  font-size: 0.9rem;
}

.btn {
  width: 100%;
  padding: 0.8rem;
  border: none;
  border-radius: 8px;
  font-size: 1rem;
  font-weight: 600;
  cursor: pointer;
  transition: background-color 0.2s;
}

.btn--reserveer {
  background-color: #2c7be5;
  color: white;
}

.btn--reserveer:hover:not(:disabled) {
  background-color: #1a5fbb;
}

.btn--reserveer:disabled {
  background-color: #aaa;
  cursor: not-allowed;
}

@media (max-width: 500px) {
  .form-rij {
    grid-template-columns: 1fr;
  }
}
</style>
