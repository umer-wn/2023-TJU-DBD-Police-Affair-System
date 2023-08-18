import Vue from "vue";
import VueRouter from "vue-router";
import Login from "@/components/Login.vue";
import LoginSuccess from "@/components/LoginSuccess.vue";
import ChangePassword from '@/components/Password/ChangePassword.vue';
import ForPassword from '@/components/Password/ForPassword.vue';
import MainPage from '@/components/MainPage.vue';
import CPSuccess from '@/components/Password/CPSuccess.vue'  // 修改密码成功后的界面
import civilianInfoRequest from '@/components/CivilianInfo/civilianInfoRequest.vue'
Vue.use(VueRouter);

const routes = [
  {
    path: "/login",
    name: "login",
    component: Login
  },
  {
    path: '/',
    name: 'main',
    component: MainPage
  },
  {
    path: "/succuss",
    name: "success",
    component: LoginSuccess,
    props: true
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
  }
];

const router = new VueRouter({
  mode: "history",
  base: process.env.BASE_URL,
  routes
});

export default router;
