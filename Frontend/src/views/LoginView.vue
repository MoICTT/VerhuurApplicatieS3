<script setup>
import { ref } from 'vue'
import { useRouter } from 'vue-router'
import { authService } from '@/services/api.js'

const router = useRouter()

const email      = ref('')
const wachtwoord = ref('')
const foutmelding= ref(null)
const isLoading  = ref(false)

async function login() {
  foutmelding.value = null

  if (!email.value || !wachtwoord.value) {
    foutmelding.value = 'Vul uw e-mailadres en wachtwoord in.'
    return
  }

  isLoading.value = true

  try {
    const resultaat = await authService.login(email.value, wachtwoord.value)

    // Token opslaan in localStorage
    if (resultaat?.token) {
      localStorage.setItem('token', resultaat.token)
    }

    router.push('/')
  } catch {
    foutmelding.value = 'Ongeldig e-mailadres of wachtwoord.'
  } finally {
    isLoading.value = false
  }
}
</script>

<template>
  <div class="login-pagina">
    <div class="login-kaart">
      <h1>Inloggen</h1>
      <p class="login-kaart__subtitel">Log in om reservaties te beheren.</p>

      <form @submit.prevent="login" novalidate>
        <div class="form-groep">
          <label for="email">E-mailadres</label>
          <input
            id="email"
            v-model="email"
            type="email"
            placeholder="jan@voorbeeld.nl"
            required
            autocomplete="email"
          />
        </div>

        <div class="form-groep">
          <label for="wachtwoord">Wachtwoord</label>
          <input
            id="wachtwoord"
            v-model="wachtwoord"
            type="password"
            placeholder="••••••••"
            required
            autocomplete="current-password"
          />
        </div>

        <p v-if="foutmelding" class="foutmelding">❌ {{ foutmelding }}</p>

        <button type="submit" class="btn btn--inloggen" :disabled="isLoading">
          {{ isLoading ? 'Bezig...' : 'Inloggen' }}
        </button>
      </form>
    </div>
  </div>
</template>

<style scoped>
.login-pagina {
  display: flex;
  justify-content: center;
  align-items: center;
  min-height: calc(100vh - 80px);
  padding: 2rem 1rem;
  background-color: #f7f8fa;
}

.login-kaart {
  background: white;
  border-radius: 16px;
  padding: 2.5rem;
  max-width: 420px;
  width: 100%;
  box-shadow: 0 4px 24px rgba(0,0,0,0.1);
}

.login-kaart h1 {
  font-size: 1.8rem;
  margin-bottom: 0.4rem;
  color: #222;
}

.login-kaart__subtitel {
  color: #666;
  font-size: 0.95rem;
  margin-bottom: 1.8rem;
}

.form-groep {
  display: flex;
  flex-direction: column;
  gap: 0.3rem;
  margin-bottom: 1.2rem;
}

.form-groep label {
  font-size: 0.9rem;
  font-weight: 500;
  color: #444;
}

.form-groep input {
  padding: 0.6rem 0.9rem;
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
  padding: 0.75rem;
  border: none;
  border-radius: 8px;
  font-size: 1rem;
  font-weight: 600;
  cursor: pointer;
  transition: background-color 0.2s;
}

.btn--inloggen {
  background-color: #1a1a2e;
  color: white;
}

.btn--inloggen:hover:not(:disabled) {
  background-color: #2c2c50;
}

.btn--inloggen:disabled {
  background-color: #aaa;
  cursor: not-allowed;
}
</style>
