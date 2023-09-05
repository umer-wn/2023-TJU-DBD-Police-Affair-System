import { createApp } from 'vue'
import ElementPlus from 'element-plus'
import 'element-plus/dist/index.css'
import router from './router/router'
import main from './components/MyMain.vue'
import Antd from 'ant-design-vue';





createApp(main).use(ElementPlus).use(Antd).use(router).mount('#app')

