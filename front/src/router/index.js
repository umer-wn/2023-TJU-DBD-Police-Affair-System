import Vue from 'vue'
import Router from 'vue-router'
import searchWagesRecord from '@/components/searchWagesRecord.vue'
import newRecord from '@/components/newRecord.vue'
import detailPage from '@/components/detailPage.vue'
import dataStatistics from '@/components/dataStatistics.vue'

Vue.use(Router)

export default new Router({
  routes: [
    {
      path: '/',
      name: 'searchWagesRecord',
      component: searchWagesRecord
    },
    {
      path: '/newRecord',
      name: 'newRecord',
      component: newRecord
    },
    {
      path: '/detailPage/:id',
      name: 'detailPage',
      component: detailPage
    },
    {
      path: '/dataStatistics',
      name: 'dataStatistics',
      component: dataStatistics
    }
  ]
})
