<script setup>
defineProps({
  auto: {
    type: Object,
    required: true
  }
})
</script>

<template>
  <div class="auto-card">
    <div class="auto-card__image">
      <img
        v-if="auto.afbeeldingUrl"
        :src="auto.afbeeldingUrl"
        :alt="auto.merk + ' ' + auto.model"
      />
      <div v-else class="auto-card__image--placeholder">🚗</div>
    </div>

    <div class="auto-card__info">
      <h3>{{ auto.merk }} {{ auto.model }}</h3>
      <p class="auto-card__bouwjaar">Bouwjaar: {{ auto.bouwjaar }}</p>
      <p class="auto-card__prijs">€ {{ auto.prijsPerDag }} / dag</p>

      <span
        class="auto-card__beschikbaar"
        :class="auto.beschikbaar ? 'beschikbaar' : 'niet-beschikbaar'"
      >
        {{ auto.beschikbaar ? 'Beschikbaar' : 'Niet beschikbaar' }}
      </span>
    </div>

    <div class="auto-card__acties">
      <RouterLink :to="'/autos/' + auto.id" class="btn btn--details">
        Bekijk details
      </RouterLink>
    </div>
  </div>
</template>

<style scoped>
.auto-card {
  background-color: #fff;
  border: 1px solid #e0e0e0;
  border-radius: 10px;
  overflow: hidden;
  box-shadow: 0 2px 8px rgba(0, 0, 0, 0.08);
  display: flex;
  flex-direction: column;
  transition: transform 0.2s, box-shadow 0.2s;
}

.auto-card:hover {
  transform: translateY(-4px);
  box-shadow: 0 6px 16px rgba(0, 0, 0, 0.12);
}

.auto-card__image {
  width: 100%;
  height: 180px;
  background-color: #f5f5f5;
  display: flex;
  align-items: center;
  justify-content: center;
  overflow: hidden;
}

.auto-card__image img {
  width: 100%;
  height: 100%;
  object-fit: cover;
}

.auto-card__image--placeholder {
  font-size: 4rem;
}

.auto-card__info {
  padding: 1rem;
  flex: 1;
}

.auto-card__info h3 {
  margin: 0 0 0.4rem;
  font-size: 1.1rem;
  color: #222;
}

.auto-card__bouwjaar {
  color: #666;
  font-size: 0.9rem;
  margin: 0.2rem 0;
}

.auto-card__prijs {
  font-weight: bold;
  font-size: 1rem;
  color: #2c7be5;
  margin: 0.4rem 0;
}

.auto-card__beschikbaar {
  display: inline-block;
  padding: 0.2rem 0.6rem;
  border-radius: 20px;
  font-size: 0.8rem;
  font-weight: 600;
  margin-top: 0.4rem;
}

.beschikbaar {
  background-color: #d4edda;
  color: #155724;
}

.niet-beschikbaar {
  background-color: #f8d7da;
  color: #721c24;
}

.auto-card__acties {
  padding: 0.8rem 1rem;
  border-top: 1px solid #f0f0f0;
}

.btn {
  display: inline-block;
  padding: 0.5rem 1rem;
  border-radius: 6px;
  text-decoration: none;
  font-size: 0.9rem;
  font-weight: 600;
  cursor: pointer;
  transition: background-color 0.2s;
}

.btn--details {
  background-color: #2c7be5;
  color: white;
  width: 100%;
  text-align: center;
  box-sizing: border-box;
}

.btn--details:hover {
  background-color: #1a5fbb;
}
</style>
