<template>
    <div>
        <div>
            <div>警务处理系统</div>
        </div>

        <div>
            <div>录像信息</div>
        </div>
        <!-- 此处为返回首页按钮，还没加链接 -->
        <div>返回首页</div>

        <div>
            <!-- 输入框中默认提示为警局id -->
            <input type="text" v-model="videoID" placeholder="录像ID" />
            <select v-model="videoType">
                <option selected value="全部">全部录像</option>
                <option value="审讯">审讯</option>
                <option value="监控">监控</option>
                <option value="调查">调查</option>
            </select>
            <input type="text" v-model="principleID" placeholder="涉及警员的警号" />
            <button @click="fetchvideoInfo">查询</button>
        </div>
        <!-- 表格显示获取的警员信息 -->
        <table v-if="videoInfo.length > 0">
            <thead>
                <tr>
                    <th>视频ID</th>
                    <th>记录时间</th>
                    <th>上传时间</th>
                    <th>录像类型</th>
                    <th>涉及警员的警号</th>
                </tr>
            </thead>
            <tbody>
                <tr v-for="video of videoInfo" :key="video.videoNumber">
                    <td>{{ video.videoID }}</td>
                    <td>{{ video.recordTime }}</td>
                    <td>{{ video.uploadTime }}</td>
                    <td>{{ video.videoType }}</td>
                    <td>{{ video.principleID }}</td>
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
            videoID: '',
            videoType: '全部',
            principleID: '',
            videoInfo: [],
            err: '警员不存在！'
        }
    },
    methods: {
        fetchvideoInfo() {
            axios.post('http://localhost:7078/api/videoInfo', {
                videoID: this.videoID,
                videoType: this.videoType,
                principleID: this.principleID
            })
                .then((res) => {
                    this.videoInfo = res.data
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
