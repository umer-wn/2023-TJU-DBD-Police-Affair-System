import { createApp } from 'vue'
import ElementPlus from 'element-plus'
import 'element-plus/dist/index.css'
import router from './router/router'
import main from './components/MyMain.vue'
import Antd from 'ant-design-vue';

import zhCn from 'element-plus/dist/locale/zh-cn.mjs'

import 'ant-design-vue/dist/reset.css';

createApp(main).use(ElementPlus,{locale: zhCn}).use(Antd).use(router).mount('#app')
