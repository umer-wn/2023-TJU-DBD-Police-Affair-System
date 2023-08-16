<template>
  <div>
    <input type="text" v-model="inputText" placeholder="输入文本">
    <button @click="submitText">提交</button>
    <div class="box">{{ boxContent }}</div>
  </div>
</template>

<script>
import axios from 'axios'

export default {
  data () {
    return {
      inputText: '', // 存储输入框的值
      boxContent: '' // 存储box的内容
    }
  },
  methods: {
    submitText () {
      axios.post('http://localhost:7078/api/query', { inputText: this.inputText })
        .then(response => {
          // 请求成功的处理逻辑
          this.boxContent = response.data
          console.log(response.data)
        })
        .catch(error => {
          // 请求失败的处理逻辑
          console.error(error)
        })
    }
  }
}
</script>

<style>
.box {
  width: 700px;
  height: 200px;
  border: 1px solid #000;
  padding: 10px;
}

/* 省略了其它样式 */
</style>
