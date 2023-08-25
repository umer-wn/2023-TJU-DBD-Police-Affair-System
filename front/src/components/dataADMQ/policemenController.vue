<template>
    <div>
        <div>
            <div>警务处理系统</div>
        </div>

        <div>
            <div>警员信息</div>
        </div>
        <!-- 此处为返回首页按钮，还没加链接 -->
        <div>返回首页</div>

        <div>
            <!-- 输入框中默认提示为警局id -->
            <input type="text" v-model="policemenNumber" placeholder="警员ID" />
            <input type="text" v-model="policemenName" placeholder="警员姓名" />
            <select v-model="policemenStatus">
                <option selected value="全部">全部</option>
                <option value="在职">在职</option>
                <option value="离职">离职</option>
            </select>
            <select v-model="policemenPosition">
                <option selected value="全部">全部</option>
                <option value="学员">学员</option>
                <option value="警员">警员</option>
                <option value="警司">警司</option>
                <option value="警督">警督</option>
                <option value="警监">警监</option>
                <option value="总警监">总警监</option>
            </select>
            <button @click="fetchpolicemenInfo">查询</button>
            <button @click="info">查看信息</button>
        </div>
        <!-- 表格显示获取的警员信息 -->
        <table v-if="policemenInfo.length > 0">
            <thead>
                <tr>
                    <th>警号</th>
                    <th>姓名</th>
                    <th>身份证号</th>
                    <th>出生日期</th>
                    <th>性别</th>
                    <th>民族</th>
                    <th>联系电话</th>
                    <th>状态</th>
                    <th>职务</th>
                    <th>薪资</th>
                </tr>
            </thead>
            <tbody>
                <tr v-for="policemen of policemenInfo" :key="policemen.policemenNumber">
                    <td>{{ policemen.policemenNumber }}</td>
                    <td>{{ policemen.policemenName }}</td>
                    <td>{{ policemen.idNumber }}</td>
                    <td>{{ policemen.birthday }}</td>
                    <td>{{ policemen.gender }}</td>
                    <td>{{ policemen.nation }}</td>
                    <td>{{ policemen.phoneNumber }}</td>
                    <td>{{ policemen.policemenStatus }}</td>
                    <td>{{ policemen.policemenPosition }}</td>
                    <td>{{ policemen.salary }}</td>
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
            policemenNumber: '',
            policemenName: '',
            policemenStatus: '全部',
            policemenPosition: '全部',
            policemenInfo: [],
            err: '警员不存在！'
        }
    },
    methods: {
        info() {
            console.log('警号：' + this.policemenNumber)
            console.log('姓名：' + this.policemenName)
            console.log('状态：' + this.policemenStatus)
            console.log('职务：' + this.policemenPosition)
        },
        fetchpolicemenInfo() {
            axios.post('http://localhost:7078/api/policemenInfo', {
                policemenNumber: this.policemenNumber,
                policemenName: this.policemenName,
                policemenStatus: this.policemenStatus,
                policemenPosition: this.policemenPosition
            })
                .then((res) => {
                    this.policemenInfo = res.data
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
