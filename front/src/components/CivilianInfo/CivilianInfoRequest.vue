<template>
  <div>
    <!-- 根据选择的搜索方式显示不同的输入框和选项 -->
    <div v-if="searchBy === 'name'">
      <input v-model="name" type="text" placeholder="姓名" />
      <select v-model="gender">
        <option value="">性别(可选)</option>
        <option value="male">男</option>
        <option value="female">女</option>
      </select>
    </div>
    <div v-else>
      <input v-model="id" type="text" placeholder="ID" />
    </div>
    
    <!-- 选择搜索方式 -->
    <div>
      <label>
        <input type="radio" v-model="searchBy" value="name" /> 按姓名查找
      </label>
      <label>
        <input type="radio" v-model="searchBy" value="id" /> 按ID查找
      </label>
    </div>

     <!-- 表格显示搜索结果 -->
    <table v-if="searchResults.length > 0" >
      <thead>
        <tr>
          <th>姓名</th>
          <th>性别</th>
          <th>身份证号</th>
          
        </tr>
      </thead>
      <tbody>
        <tr v-for="(result, index) in searchResults" :key="index">
          <td>{{ result.name }}</td>
          <td>{{ result.gender }}</td>
          <td>{{ result.idCard }}</td>
          
        </tr>
      </tbody>
    </table>

    <!-- 显示未检索到相关人士信息 -->
    <p v-else-if="searchCompleted">未检索到相关人士。</p>
    
    <!-- 提交按钮 -->
    <button @click="submitSearch">搜索</button>
  </div>
</template>

<script>
import axios from 'axios';

export default {
  data() {
    return {
      searchBy: 'name', // 默认按姓名查找
      name: '',
      gender: 'male', // 默认选中男性
      id: '',
      searchResults: [], // 初始化为空数组
      searchCompleted: false // 初始化为false
    };
  },
  methods: {
    async submitSearch() {
      try {
        let response;

        const token = localStorage.getItem("token");

        const headers = {
            Authorization: `Bearer ${token}`
        };

        if (this.searchBy === 'name') {
          // 构建请求参数
          const params = {
            name: this.name,
            gender: this.gender
          };

          // 发送POST请求
          response = await axios.post('http://localhost:7078/searchCivilian/ByName', params,{headers});
        } else {
          // 构建请求参数
          const params = {
            id: this.id
          };

          // 发送POST请求
          response = await axios.post('http://localhost:7078/searchCivilian/ById', params,{headers});
        }



        if (response.data.length > 0) {
          // 更新搜索结果并标记搜索完成
          this.searchResults = response.data;
          this.searchCompleted = true;
        } else {
          // 标记搜索未完成，未检索到相关人士
          this.searchResults = [];
          this.searchCompleted = true;
        }

        // 处理响应结果，例如展示搜索结果
        console.log(response.data);
      } catch (error) {
        console.error('请求失败:', error);
      }
    }
  },
  mounted() {
      // 从 sessionStorage 中获取令牌
      const token = localStorage.getItem("token");

      if (!token) {
        // 令牌不存在，说明未经过身份验证，跳转回登录页面或其他处理
        this.$router.push("/fail");
        return;
      }
    }
};
</script>
