import Vue from 'vue'
import Router from 'vue-router'
import FamilyCrime from '@/components/FamilyCrime'
Vue.use(Router)

const routes = [
  {
    path: '/',
    name: 'FamilyCrime',
    component: FamilyCrime,
    props: true
  }
]

const router = new Router({
  mode: 'history',
  base: process.env.BASE_URL,
  routes
})

export default router
