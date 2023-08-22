<template>
  <div>
    <div>
      <div>警务处理系统</div>
    </div>

    <div>
      <div>警局信息</div>
    </div>
    <!-- 此处为返回首页按钮，还没加链接 -->
    <div>返回首页</div>

    <div>
      <!-- 输入框中默认提示为警局id -->
      <input type="text" v-model="stationID" placeholder="警局ID" />
      <input type="text" v-model="stationName" placeholder="警局名称" />
      <input type="text" v-model="city" placeholder="警局城市" />
      <input type="text" v-model="address" placeholder="警局地址" />
      <input type="number" v-model="budget" placeholder="警局预算" />
      <button @click="fetchStationInfo">查询</button>
    </div>
    <!-- 表格显示获取的警局信息 -->
    <table v-if="stationInfo.length > 0">
      <thead>
        <tr>
          <th>警局ID</th>
          <th>警局名称</th>
          <th>警局城市</th>
          <th>警局地址</th>
          <th>警局预算</th>
        </tr>
      </thead>
      <tbody>
        <tr v-for="station in stationInfo" :key="station.stationID">
          <td>{{ station.stationID }}</td>
          <td>{{ station.stationName }}</td>
          <td>{{ station.city }}</td>
          <td>{{ station.address }}</td>
          <td>{{ station.budget }}</td>
        </tr>
      </tbody>
    </table>
    <!-- 错误提示 -->
    <div v-else>{{ boxContent }}</div>
  </div>
</template>

<script>
import axios from 'axios'

export default {
  data() {
    return {
      stationID: '',
      stationName: '',
      city: '',
      address: '',
      budget: null, // 修改为null，确保初始值为null
      stationInfo: [],
      err: '警局不存在！'
    }
  },
  methods: {
    fetchStationInfo() {
      axios.post('http://localhost:7078/api/stationInfo', {
        stationID: this.stationID,
        stationName: this.stationName,
        city: this.city,
        address: this.address,
        budget: this.budget === '' ? null : this.budget
      })
        .then((res) => {
          this.stationInfo = res.data
          console.log(res.data)
        })
        .catch((err) => {
          this.boxContent = this.err
          console.log(err)
        })
    }
  }
}
</script>
