<template>
    <div class="change-permission">
      <h1>Change Permission</h1>

      <div class="parameters">
        <p>警号: {{ parameter1 }}</p>
        <p>姓名: {{ parameter2 }}</p>
      </div>

      <div class="permission-level">
        <label for="permission-select">Select permission level:</label>
        <select id="permission-select" v-model="selectedLevel">
          <option value="0">0</option>
          <option value="1">1</option>
          <option value="2">2</option>
          <option value="3">3</option>
          <option value="4">4</option>
          <option value="5">5</option>
        </select>
      </div>

      <button @click="submitPermission">Submit</button>
    </div>
  </template>

<script>
import axios from 'axios'
export default {
  data() {
    return {
      parameter1: '', // 通过 props 或其他方式传入的参数1
      parameter2: '', // 通过 props 或其他方式传入的参数2
      my_number: '',
      selectedLevel: '0' // 默认选择的权限级别为0
    }
  },
  mounted() {
    // 访问id和name参数
    this.parameter1 = this.$route.params.id
    this.parameter2 = this.$route.params.name
    this.my_number = this.$route.params.my_number
  },
  methods: {
    submitPermission() {
      // 在这里处理提交权限的逻辑
      axios.post('http://localhost:7078/api/permit', {s_number: this.parameter1, h_number: this.my_number, level: this.selectedLevel})
        .then(response => {
          this.searchResult = response.data
        })
      // const permissionData = {
        // parameter1: this.parameter1,
        // parameter2: this.parameter2,
        // selectedLevel: this.selectedLevel
      // }
      // console.log(permissionData) // 这里仅打印到控制台作为示例
    }
  }
}
</script>
  <style>
  .change-permission {
    /* 添加样式以定义页面布局和外观 */
  }

  .parameters {
    /* 参数显示的样式 */
  }

  .permission-level {
    /* 权限级别下拉菜单的样式 */
  }
  </style>
