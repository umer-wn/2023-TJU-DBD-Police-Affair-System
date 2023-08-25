import Vue from 'vue'
import Router from 'vue-router'
import FamilybgCheck from '@/components/FamilybgCheck'
import Register from '@/components/Register'
Vue.use(Router)

const routes = [
    // {
    //     path: '/',
    //     name: 'FamilyCrime',
    //     component: FamilyCrime,
    //     props: true
    // },
    {
        path: '/FamilybgCheck',
        name: 'FamilybgCheck',
        component: FamilybgCheck,
        props: true
    },
    {
        path:'/',
        name:'Register',
        component: Register,
        path: '/',
        name: 'Investigation',
        component: Investigation,
        props: true
    }
]

const router = new Router({
    mode: 'history',
    base: process.env.BASE_URL,
    routes
})

export default router
