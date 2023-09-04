import Vue from "vue";
import VueRouter from "vue-router";
import Login from "@/components/Login.vue";
import ChangePassword from '@/components/Password/ChangePassword.vue';
import ForPassword from '@/components/Password/ForPassword.vue';
import MainPage from '@/components/MainPage.vue';
import CPSuccess from '@/components/Password/CPSuccess.vue'  // 修改密码成功后的界面
import civilianInfoRequest from '@/components/CivilianInfo/civilianInfoRequest.vue'
import FamilybgCheck from '@/components/FamilybgCheck.vue'
import searchWagesRecord from '@/components/salary/searchWagesRecord.vue'
import newRecord from '@/components/salary/newRecord.vue'
import dataStatistics from '@/components/salary/dataStatistics.vue'
import register from '@/components/Register.vue'
Vue.use(VueRouter);

const routes = [
  {
    path:'/register',
    name:"register",
    component:register
  },
  {
    path: "/login",
    name: "login",
    component: Login
  },
  {
    path: '/main',
    name: 'main',
    component: MainPage
  },
  {
    path:'/',
    name:'FamilybgCheck',
    component:FamilybgCheck
  },
  {
    path: "/change-password",
    name: "change-password",
    component: ChangePassword
  },
  {
    path: "/for-password",
    name: "for-password",
    component: ForPassword
  },
  {
    path: "/for-password/success",
    name: "CPsSuccess",
    component: CPSuccess
  },
  {
    path: "/civilianInfo",
    name: "civilianInfo",
    component: civilianInfoRequest
  },
  {
    path: '/searchWagesRecord',
    name: 'searchWagesRecord',
    component: searchWagesRecord
  },
  {
    path: '/newRecord',
    name: 'newRecord',
    component: newRecord
  },
  {
    path: '/dataStatistics',
    name: 'dataStatistics',
    component: dataStatistics
  }

];

const router = new VueRouter({
  mode: "history",
  base: process.env.BASE_URL,
  routes
});

export default router;
