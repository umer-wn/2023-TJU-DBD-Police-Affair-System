<template>
    <div>
        <div>
            <div>警务处理系统</div>
        </div>

        <div>
            <div>人员信息</div>
        </div>
        <!-- 此处为返回首页按钮，还没加链接 -->
        <div>返回首页</div>

        <div>
            <!-- 输入框中默认提示为警局id -->
            <input type="text" v-model="IDNum" placeholder="身份证号码" />
            <input type="text" v-model="citizenName" placeholder="姓名" />
            <select v-model="gender">
                <option selected value="全部">全部性别</option>
                <option value="M">男</option>
                <option value="F">女</option>
            </select>
            <button @click="fetchCitizenInfo">查询</button>
        </div>
        <!-- 表格显示获取的警员信息 -->
        <table v-if="citizenInfo.length > 0">
            <thead>
                <tr>
                    <th>身份证号码</th>
                    <th>姓名</th>
                    <th>性别</th>
                    <th>父亲身份证号</th>
                    <th>母亲身份证号</th>
                </tr>
            </thead>
            <tbody>
                <tr v-for="item of citizenInfo" :key="item.IDNum">
                    <td>{{ item.idNum }}</td>
                    <td>{{ item.citizenName }}</td>
                    <td>{{ item.gender }}</td>
                    <td>{{ item.fatherID }}</td>
                    <td>{{ item.motherID }}</td>
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
            IDNum: '',
            citizenName: '',
            gender: '全部',
            citizenInfo: [],
            err: '人员不存在！'
        }
    },
    methods: {
        fetchCitizenInfo() {
            axios.post('http://localhost:7078/api/citizenInfo', {
                IDNum: this.IDNum,
                citizenName: this.citizenName,
                gender: this.gender
            })
                .then((res) => {
                    this.citizenInfo = res.data
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
