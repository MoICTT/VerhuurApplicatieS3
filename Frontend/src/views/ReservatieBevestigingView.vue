<script setup>
import { useRoute, RouterLink } from 'vue-router'

const route = useRoute()

const merk       = route.query.merk       ?? 'Onbekend'
const model      = route.query.model      ?? ''
const startDatum = route.query.startDatum ?? ''
const eindDatum  = route.query.eindDatum  ?? ''
const totaal     = route.query.totaal     ?? 0
</script>

<template>
  <div class="bevestiging-pagina">
    <div class="bevestiging-kaart">
      <div class="bevestiging-kaart__icoon">✅</div>

      <h1>Reservatie bevestigd!</h1>
      <p class="bevestiging-kaart__subtitel">
        Uw reservatie voor de <strong>{{ merk }} {{ model }}</strong> is succesvol geplaatst.
      </p>

      <table class="overzicht-tabel">
        <tr>
          <th>Auto</th>
          <td>{{ merk }} {{ model }}</td>
        </tr>
        <tr>
          <th>Startdatum</th>
          <td>{{ startDatum }}</td>
        </tr>
        <tr>
          <th>Einddatum</th>
          <td>{{ eindDatum }}</td>
        </tr>
        <tr>
          <th>Totaalprijs</th>
          <td><strong>€ {{ totaal }}</strong></td>
        </tr>
      </table>

      <p class="bevestiging-kaart__email-info">
        📧 U ontvangt een bevestiging per e-mail.
      </p>

      <RouterLink to="/autos" class="btn btn--terug">Terug naar overzicht</RouterLink>
    </div>
  </div>
</template>

<style scoped>
.bevestiging-pagina {
  display: flex;
  justify-content: center;
  align-items: center;
  min-height: calc(100vh - 80px);
  padding: 2rem 1rem;
  background-color: #f7f8fa;
}

.bevestiging-kaart {
  background: white;
  border-radius: 16px;
  padding: 2.5rem;
  max-width: 500px;
  width: 100%;
  box-shadow: 0 4px 24px rgba(0,0,0,0.1);
  text-align: center;
}

.bevestiging-kaart__icoon {
  font-size: 3.5rem;
  margin-bottom: 0.5rem;
}

.bevestiging-kaart h1 {
  font-size: 1.8rem;
  color: #155724;
  margin-bottom: 0.6rem;
}

.bevestiging-kaart__subtitel {
  color: #555;
  margin-bottom: 1.5rem;
  font-size: 0.95rem;
}

.overzicht-tabel {
  width: 100%;
  border-collapse: collapse;
  margin-bottom: 1.5rem;
  text-align: left;
}

.overzicht-tabel th,
.overzicht-tabel td {
  padding: 0.6rem 0.5rem;
  border-bottom: 1px solid #f0f0f0;
  font-size: 0.95rem;
}

.overzicht-tabel th {
  color: #888;
  font-weight: 500;
  width: 40%;
}

.bevestiging-kaart__email-info {
  font-size: 0.9rem;
  color: #666;
  margin-bottom: 1.5rem;
}

.btn {
  display: inline-block;
  padding: 0.7rem 1.8rem;
  border-radius: 8px;
  font-size: 1rem;
  font-weight: 600;
  text-decoration: none;
  cursor: pointer;
  transition: background-color 0.2s;
}

.btn--terug {
  background-color: #2c7be5;
  color: white;
}

.btn--terug:hover {
  background-color: #1a5fbb;
}
</style>
