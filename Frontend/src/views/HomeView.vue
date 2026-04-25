<script setup>
import { RouterLink } from 'vue-router'
</script>

<template>
  <div class="home">
    <section class="hero">
      <div class="hero__tekst">
        <h1>Huur eenvoudig een auto</h1>
        <p>Bekijk ons aanbod en maak direct een reservatie. Snel, eenvoudig en voordelig.</p>
        <RouterLink to="/autos" class="btn btn--primair">Bekijk auto's</RouterLink>
      </div>
      <div class="hero__afbeelding">🚘</div>
    </section>

    <section class="kenmerken">
      <div class="kenmerk">
        <span class="kenmerk__icoon">✅</span>
        <h3>Groot aanbod</h3>
        <p>Kies uit tientallen beschikbare voertuigen voor elk budget.</p>
      </div>
      <div class="kenmerk">
        <span class="kenmerk__icoon">📅</span>
        <h3>Eenvoudig boeken</h3>
        <p>Reserveer in enkele stappen, direct online.</p>
      </div>
      <div class="kenmerk">
        <span class="kenmerk__icoon">💶</span>
        <h3>Eerlijke prijzen</h3>
        <p>Transparante dagprijzen zonder verborgen kosten.</p>
      </div>
    </section>
  </div>
</template>

<style scoped>
.home {
  max-width: 1100px;
  margin: 0 auto;
  padding: 2rem 1rem;
}

.hero {
  display: flex;
  align-items: center;
  justify-content: space-between;
  gap: 2rem;
  background: linear-gradient(135deg, #1a1a2e, #2c7be5);
  color: white;
  border-radius: 16px;
  padding: 3rem 2.5rem;
  margin-bottom: 3rem;
}

.hero__tekst h1 {
  font-size: 2.2rem;
  margin-bottom: 1rem;
  line-height: 1.2;
}

.hero__tekst p {
  font-size: 1.05rem;
  color: #cde;
  margin-bottom: 1.5rem;
  max-width: 420px;
}

.hero__afbeelding {
  font-size: 8rem;
  flex-shrink: 0;
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

.btn--primair {
  background-color: white;
  color: #2c7be5;
}

.btn--primair:hover {
  background-color: #eef4ff;
}

.kenmerken {
  display: grid;
  grid-template-columns: repeat(auto-fill, minmax(280px, 1fr));
  gap: 1.5rem;
}

.kenmerk {
  background: white;
  border-radius: 12px;
  padding: 1.8rem;
  box-shadow: 0 2px 8px rgba(0,0,0,0.07);
  text-align: center;
}

.kenmerk__icoon {
  font-size: 2.5rem;
  display: block;
  margin-bottom: 0.8rem;
}

.kenmerk h3 {
  margin-bottom: 0.5rem;
  color: #222;
}

.kenmerk p {
  color: #666;
  font-size: 0.95rem;
}
</style>
