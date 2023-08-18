import Vue from 'vue'
import Router from 'vue-router'
import HelloWorld from '@/components/HelloWorld'

// Vue.use(Router)

// export default new Router({
//   routes: [
//     {
//       path: '/',
//       name: 'HelloWorld',
//       component: HelloWorld
//     },
//     {
//       path: '/con',
//       name: 'con',
//       component: () => import('../components/caseController.vue')
//     }
//   ]
// })

Vue.use(Router)

// 导入要使用的组件

// 创建路由实例
const router = new Router({
  routes: [

    // {
    //   path: '/',
    //   name: 'HelloWorld',
    //   component: HelloWorld
    // },
    {
      path: '/',
      name: 'con',
      component: () => import('../Controller/caseController.vue')
    }
  ]
})

export default router
