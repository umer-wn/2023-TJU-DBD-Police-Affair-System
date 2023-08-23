import Vue from 'vue'
import Router from 'vue-router'
import FamilyCrime from '@/components/FamilyCrime'
import Investigation from '@/components/Investigation'
import AddNewUser from '@/components/AddNewUser'
Vue.use(Router)

const routes = [
    {
        path: '/familyCrime',
        name: 'FamilyCrime',
        component: FamilyCrime,
        props: true
    },
    {
        path: '/investigation',
        name: 'Investigation',
        component: Investigation,
        props: true
    },
    {
        path:'/',
        name:'AddNewUser',
        component: AddNewUser,
        props: true
    }
]

const router = new Router({
    mode: 'history',
    base: process.env.BASE_URL,
    routes
})

export default router
