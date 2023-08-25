import Vue from 'vue'
import Router from 'vue-router'
import CaseStatistic from '../components/caseStatistics/caseStatisticsController.vue'

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
    {
      path: '/caseStatistic',
      name: 'CaseStatistic',
      component: CaseStatistic
    },
  ]
})

export default router
