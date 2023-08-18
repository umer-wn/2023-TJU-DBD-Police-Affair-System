<template>
    <div>
        <div>
            <div>警务处理系统</div>
        </div>

        <div>
            <div>案件信息</div>
        </div>
        <!-- 此处为返回首页按钮，还没加链接 -->
        <div>返回首页</div>

        <div>
            <!-- 输入框中默认提示为警局id -->
            <input type="text" v-model="caseID" placeholder="案件ID" />
            <select v-model="caseType">
                <option selected value="全部">全部案件类型</option>
                <option value="强奸">强奸</option>
                <option value="抢劫">抢劫</option>
                <option value="故意伤害">故意伤害</option>
                <option value="盗窃">盗窃</option>
                <option value="诈骗">诈骗</option>
                <option value="谋杀">谋杀</option>
            </select>
            <select v-model="status">
                <option selected value="全部">全部案件状态</option>
                <option value="立案">立案</option>
                <option value="结案">结案</option>
                <option value="调查">调查</option>
            </select>
            <input type="text" v-model="address" placeholder="案发地点" />
            <select v-model="ranking">
                <option selected value="全部">全部等级</option>
                <option value="0">0</option>
                <option value="1">1</option>
                <option value="2">2</option>
                <option value="3">3</option>
            </select>
            <button @click="fetchCaseInfo">查询</button>
        </div>
        <!-- 表格显示获取的警员信息 -->
        <table v-if="caseInfo.length > 0">
            <thead>
                <tr>
                    <th>案件ID</th>
                    <th>案件类型</th>
                    <th>案件状态</th>
                    <th>登记时间</th>
                    <th>案发地点</th>
                    <th>案件等级</th>
                </tr>
            </thead>
            <tbody>
                <tr v-for="item of caseInfo" :key="item.caseID">
                    <td>{{ item.caseID }}</td>
                    <td>{{ item.caseType }}</td>
                    <td>{{ item.status }}</td>
                    <td>{{ item.registerTime }}</td>
                    <td>{{ item.address }}</td>
                    <td>{{ item.ranking }}</td>
                    <td>
                      <button @click="goToDetails(item)">详情</button>
                    </td>
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
  data () {
    return {
      caseID: '',
      caseType: '全部',
      status: '全部',
      address: '',
      ranking: '全部',
      caseInfo: [],
      err: '录像不存在！'
    }
  },
  methods: {
    fetchCaseInfo () {
      axios.post('http://localhost:7078/api/caseInfo', {
        caseID: this.caseID,
        caseType: this.caseType,
        status: this.status,
        address: this.address,
        ranking: this.ranking
      })
        .then((res) => {
          this.caseInfo = res.data
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
