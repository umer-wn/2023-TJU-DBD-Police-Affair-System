<template>
    <div>
    <h1>权限申请审批</h1>
    <!-- 权限申请列表 -->
    <table>
      <thead>
      <tr>
        <th>
        <input type="checkbox" v-model="selectAll" @change="toggleSelectAll">
        </th>
        <th>申请人警号</th>
        <th>被修改人警号</th>
        <th>原权限等级</th>
        <th>修改权限等级</th>
        <th>申请状态</th>
        <th>申请原因</th>
        <th>操作</th>
      </tr>
      </thead>
      <tbody>
      <tr v-for="request in pendingRequests" :key="request.h_number + '-' + request.s_number">
        <td>
        <input type="checkbox" v-model="request.selected" v-if="request.status === '待处理'">
        </td>
        <td>{{ request.h_number }}</td>
        <td>{{ request.s_number }}</td>
        <td>{{ request.f_level }}</td>
        <td>{{ request.l_level }}</td>
        <td>{{ request.status }}</td>
        <td>{{ request.reason }}</td>
        <td>
        <button @click="approveRequest(request)" v-if="request.status === '待处理'">同意</button>
        <button @click="rejectRequest(request)" v-if="request.status === '待处理'">拒绝</button>
        </td>
      </tr>
      </tbody>
    </table>
    <!-- 批量审批操作区域 -->
    <div>
      <button @click="batchApprove">批量同意</button>
      <button @click="batchReject">批量拒绝</button>
    </div>
    <!-- 已同意的申请 -->
    <h2>已同意的申请</h2>
    <table>
      <!-- 表头 -->
      <thead>
      <tr>
        <th>申请人警号</th>
        <th>被修改人警号</th>
        <th>原权限等级</th>
        <th>修改权限等级</th>
        <th>申请状态</th>
        <th>申请原因</th>
      </tr>
      </thead>
      <!-- 表格内容 -->
      <tbody>
      <tr v-for="request in approvedRequests" :key="request.h_number + '-' + request.s_number">
        <td>{{ request.h_number }}</td>
        <td>{{ request.s_number }}</td>
        <td>{{ request.f_level }}</td>
        <td>{{ request.l_level }}</td>
        <td>{{ request.status }}</td>
        <td>{{ request.reason }}</td>
      </tr>
      </tbody>
    </table>

    <!-- 已拒绝的申请 -->
    <h2>已拒绝的申请</h2>
    <table>
      <!-- 表头 -->
      <thead>
      <tr>
        <th>申请人警号</th>
        <th>被修改人警号</th>
        <th>原权限等级</th>
        <th>修改权限等级</th>
        <th>申请状态</th>
        <th>申请原因</th>
      </tr>
      </thead>
      <!-- 表格内容 -->
      <tbody>
      <tr v-for="request in rejectedRequests" :key="request.h_number + '-' + request.s_number">
        <td>{{ request.h_number }}</td>
        <td>{{ request.s_number }}</td>
        <td>{{ request.f_level }}</td>
        <td>{{ request.l_level }}</td>
        <td>{{ request.status }}</td>
        <td>{{ request.reason }}</td>
      </tr>
      </tbody>
    </table>

    </div>
  </template>
<script>
import axios from 'axios'

export default {
  data() {
    return {
      pendingRequests: [], // 将requests改为pendingRequests
      approvedRequests: [],
      rejectedRequests: [],
      selectAll: false
    }
  },
  mounted() {
    this.fetchRequests()
  },
  methods: {
    toggleSelectAll() {
      this.pendingRequests.forEach(request => {
        request.selected = this.selectAll
      })
    },
    fetchRequests() {
      axios.get('http://localhost:7078/api/manage')
        .then(response => {
          const pendingRequests = [] // 存储待处理的申请
          const approvedRequests = [] // 存储已同意的申请
          const rejectedRequests = [] // 存储已拒绝的申请
          response.data.forEach(request => {
            if (request.status === '待处理') {
              pendingRequests.push(request)
            } else if (request.status === '同意') {
              approvedRequests.push(request)
            } else if (request.status === '拒绝') {
              rejectedRequests.push(request)
            }
          })
          this.pendingRequests = pendingRequests
          this.approvedRequests = approvedRequests
          this.rejectedRequests = rejectedRequests
        })
        .catch(error => {
          console.error(error)
        })
    },
    approveRequest(request) {
      axios.post('http://localhost:7078/api/manage', { s_number: request.s_number, h_number: request.h_number, F_level: request.f_level, L_level: request.l_level, reason: request.reason, status: '同意' })
        .then(response => {
          this.fetchRequests()
        })
        .catch(error => {
          console.error(error)
        })
    },
    rejectRequest(request) {
      axios.post('http://localhost:7078/api/manage', { s_number: request.s_number, h_number: request.h_number, F_level: request.f_level, L_level: request.l_level, reason: request.reason, status: '拒绝' })
        .then(response => {
          this.fetchRequests()
        })
        .catch(error => {
          console.error(error)
        })
    },
    batchApprove() {
      const selectedRequests = this.pendingRequests.filter(request => request.selected)
      selectedRequests.forEach(request => {
        this.approveRequest(request)
      })
    },
    batchReject() {
      const selectedRequests = this.pendingRequests.filter(request => request.selected)
      selectedRequests.forEach(request => {
        this.rejectRequest(request)
      })
    }
  }
}
</script>
