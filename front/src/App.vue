<template>
  <div>
    <input type="text" v-model="inputText" placeholder="输入文本">
    <div class="box">{{ boxContent }}</div>
  </div>
</template>

<script>
import axios from 'axios';

export default {
  data() {
    return {
      inputText: '', // 存储输入框的值
      boxContent: '' // 存储box的内容
    };
  },
  watch: {
    inputText(newValue) {
      // 每当inputText的值发生变化时，执行相应的操作
      axios.post('http://localhost:7078/api/query', { inputText: newValue })
        .then(response => {
          // 请求成功的处理逻辑
          this.boxContent = response.data;
          console.log(response.data);
        })
        .catch(error => {
          // 请求失败的处理逻辑
          console.error(error);
        });
    }
  }
};
</script>

<style>
.box {
  width: 700px;
  height: 2000px;
  border: 1px solid #000;
  padding: 10px;
}

.name-list {
  display: grid;
  grid-template-columns: repeat(3, 1fr);
  gap: 10px;
}

.name-item {
  list-style: none;
}
</style>
