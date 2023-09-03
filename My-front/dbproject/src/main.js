import { createApp } from 'vue'
import ElementPlus from 'element-plus'
import 'element-plus/dist/index.css'
import router from './router/router'
import main from './components/MyMain.vue'
import Antd from 'ant-design-vue';

import zhCn from 'element-plus/dist/locale/zh-cn.mjs'
import 'ant-design-vue/dist/reset.css';

const debounce = (fn, delay) => {
    let timer = null;
    return function () {
      let context = this;
      let args = arguments;
      clearTimeout(timer);
      timer = setTimeout(function () {
        fn.apply(context, args);
      }, delay);
    }
  }
  
  const _ResizeObserver = window.ResizeObserver;
  window.ResizeObserver = class ResizeObserver extends _ResizeObserver {
    constructor(callback) {
      callback = debounce(callback, 16);
      super(callback);
    }
  }
 
createApp(main).use(ElementPlus,{locale: zhCn}).use(Antd).use(router).mount('#app')
