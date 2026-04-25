import { createRouter, createWebHistory } from 'vue-router'
import HomeView from '../views/HomeView.vue'

const router = createRouter({
  history: createWebHistory(import.meta.env.BASE_URL),
  routes: [
    {
      path: '/',
      name: 'home',
      component: HomeView,
    },
    {
      path: '/autos',
      name: 'autos',
      component: () => import('../views/AutosView.vue'),
    },
    {
      path: '/autos/:id',
      name: 'auto-detail',
      component: () => import('../views/AutoDetailView.vue'),
    },
    {
      path: '/reservatie',
      name: 'reservatie',
      component: () => import('../views/ReservatieView.vue'),
    },
    {
      path: '/reservatie/bevestiging',
      name: 'reservatie-bevestiging',
      component: () => import('../views/ReservatieBevestigingView.vue'),
    },
    {
      path: '/login',
      name: 'login',
      component: () => import('../views/LoginView.vue'),
    },
  ],
})

export default router
