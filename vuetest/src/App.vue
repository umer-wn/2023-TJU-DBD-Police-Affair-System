<template>
  <div>
    <button @click="displayContent">点击显示内容</button>
    <button @click="clearContent">清除姓名</button> <!-- 新增的清除按钮 -->
    <div class="box">{{ content }}</div>
    <ul v-if="content.items" class="name-list">
      <li v-for="(item) in content.items" :key="item.id" class="name-item">
        ID={{ item.id }}, name={{ item.name }}, deptname={{ item.deptname }}, tot_cred={{ item.tot_cred }} 
      </li>
    </ul>
   
  </div>
</template>

<script>
import axios from 'axios'

export default {
  data() {
    return {
      content: []
    }
  },
  methods: {
    displayContent() {
      axios.get('http://localhost:7078/api/data')
        .then(response => {
          this.content = {
            items: response.data
          };
        })
        .catch(error => {
          console.error(error);
        });
    },
    clearContent() {
      this.content = []; // 清空content数组
    }
  }
}
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
