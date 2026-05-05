// Pas deze URL aan naar jouw backend adres
const BASE_URL = 'http://localhost:5150/api'

async function request(endpoint, options = {}) {
  const response = await fetch(`${BASE_URL}${endpoint}`, {
    headers: { 'Content-Type': 'application/json' },
    ...options,
  })

  if (!response.ok) {
    const error = await response.text()
    throw new Error(error || `Fout ${response.status}`)
  }

  // 204 No Content heeft geen body
  if (response.status === 204) return null
  return response.json()
}

// ── Auto's ──────────────────────────────────────────
export const autoService = {
  getAll: () => request('/autos'),
  getById: (id) => request(`/autos/${id}`),
}

// ── Reservaties ─────────────────────────────────────
export const reservatieService = {
  maakReservatie: (data) =>
    request('/reservaties', {
      method: 'POST',
      body: JSON.stringify(data),
    }),
}

// ── Authenticatie ────────────────────────────────────
export const authService = {
  login: (email, wachtwoord) =>
    request('/auth/login', {
      method: 'POST',
      body: JSON.stringify({ email, wachtwoord }),
    }),
}
