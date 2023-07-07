<template>
    <div>
      <!-- 搜索框 -->
      <input type="text" v-model="searchKeyword" placeholder="请输入搜索关键字" />

      <!-- 确认按钮 -->
      <button @click="search">确认</button>

      <!-- 信息显示表格 -->
      <table class="custom-table" v-if="searchResult">
        <thead>
          <tr>
            <th>警号</th>
            <th>姓名</th>
            <th>身份证号码</th>
            <th>电话号码</th>
          </tr>
        </thead>
        <tbody>
          <tr>
            <td class="cell-spacing">{{ searchResult.police_number }}</td>
            <td class="cell-spacing">{{ searchResult.police_name }}</td>
            <td class="cell-spacing">{{ searchResult.iD_number }}</td>
            <td class="cell-spacing">{{ searchResult.phone_number }}</td>
          </tr>
        </tbody>
      </table>
      <!-- ... -->
      <div class="center-align">
        <div class="middle-align">
          <button @click="modifyPermissions">修改其权限</button>
        </div>
      </div>

      <!-- 错误提示 -->
      <div v-if="errorMessage">{{ errorMessage }}</div>
    </div>
  </template>
<style>
.custom-table {
  border-collapse: collapse;
  width: 100%;
}

.custom-table th,
.custom-table td {
  border: 1px solid black;
  padding: 8px;
  height: 50px;
  text-align: middle;
}

.custom-table th {
  background-color: #f2f2f2;
}

.cell-spacing {
  padding: 4px;
}

.custom-table td {
  border: 1px solid black;
  padding: 8px;
  height: 50px;
  text-align: center;
}

</style>

<script>
import axios from 'axios'

export default {
  data() {
    return {
      searchKeyword: '',
      searchResult: {},
      selectedPermission: 'read'
    }
  },
  methods: {
    search() {
      this.searchResult = {}
      // 根据搜索关键字执行相应的逻辑，比如发送请求获取数据并更新 searchResult
      axios.post('http://localhost:7078/api/data', { temp: this.searchKeyword })
        .then(response => {
          this.searchResult = response.data
        })
        .catch(error => {
          console.error(error)
          this.searchResult = {}
        })
    },
    modifyPermissions() {
      this.$router.push({name: 'ChangePermission', params: {id: this.searchResult.police_number, name: this.searchResult.police_name, my_number: '9726541'}})
    }
  }
}

</script>
