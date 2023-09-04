<template>
  <el-header class="sub-header" @mousemove="handleMouseMove">
    <div>&nbsp;&nbsp;警员管理&nbsp;>&nbsp;执法录像管理</div>
  </el-header>

  <el-tabs v-model="anv" type="border-card" style="margin-top: 10vh;">
    <!-- 查询录像栏 -->
    <el-tab-pane label="查询录像" name="1">
      <div style="text-align: center;">
        <table style="display: inline-block;text-align: left;">
          <tr>
            <td>
              <el-text class="noteText" type="primary" style="padding-left:6px">录像编号：</el-text>
              <el-input class="inputBox" v-model="videoID" placeholder="请输入录像编号"
                oninput="value=value.replace(/[^\d.]/g,'')" clearable maxlength="5" show-word-limit />
            </td>
            <td>
              <el-text class="noteText" type="primary" style="padding-left:6px">录像类型：</el-text>
              <el-select class="inputBox" v-model="videoType" placeholder="请选择">
                <el-option selected label="全部类型" value=""></el-option>
                <el-option label="审讯" value="审讯"></el-option>
                <el-option label="监控" value="监控"></el-option>
                <el-option label="调查" value="调查"></el-option>
              </el-select>
            </td>
          </tr>
          <tr>
            <td>
              <el-text class="noteText" type="primary">记录日期:</el-text>
              <el-date-picker class="inputBox" v-model="recordTime" type="date" placeholder="请选择出勤日期"
                style="margin-left: 16px;width:200px" />
            </td>
            <td>
              <el-text class="noteText" type="primary">是否筛选时间:</el-text>
              <el-select class="inputBox" v-model="isT" style="margin-left: 16px;width: 200px;" placeholder="请选择">
                <el-option label="不筛选" value="F"></el-option>
                <el-option label="筛选" value="T"></el-option>
              </el-select>
            </td>
          </tr>
          <tr>
            <td>
              <el-text class="noteText" type="primary" style="padding-left:6px">记录警员编号：</el-text>
              <el-input class="inputBox" v-model="policemenID" placeholder="请输入警员编号"
                oninput="value=value.replace(/[^\d.]/g,'')" clearable maxlength="7" show-word-limit />
            </td>
            <td></td>
          </tr>
        </table>
        <div class="seaButton">
          <el-button type="primary" @click="fetchVideoInfo">查询</el-button>
        </div>
      </div>

      <el-divider />
      <div>
        <!-- 表格显示获取的录像信息 -->
        <el-table :data="videoInfo" stripe height="450" @wheel.passive.stop>
          <el-table-column prop="videoID" label="录像编号" sortable width="130px" />
          <el-table-column prop="videoType" label="录像类型" sortable width="130px" />
          <el-table-column prop="recordTime" label="记录时间" sortable />
          <el-table-column prop="uploadTime" label="上传时间" sortable />
          <el-table-column prop="policemenID" label="进行记录的警员" sortable width="160px" />
        </el-table>
      </div>
    </el-tab-pane>

    <!-- 新增录像栏 -->
    <el-tab-pane label="新增录像" name="2">
      <div style="text-align: center;">
        <table style="display: inline-block;text-align: left;">
          <tr>
            <td>
              <el-text class="noteText" type="primary" style="padding-left:6px">录像编号：</el-text>
              <el-input class="inputBox" v-model="addVideoID" placeholder="请输入录像编号"
                oninput="value=value.replace(/[^\d.]/g,'')" clearable maxlength="5" show-word-limit />
            </td>
            <td>
              <el-text class="noteText" type="primary" style="padding-left:6px">录像类型：</el-text>
              <el-select class="inputBox" v-model="addVideoType" placeholder="请选择">
                <el-option label="审讯" value="审讯"></el-option>
                <el-option label="监控" value="监控"></el-option>
                <el-option label="调查" value="调查"></el-option>
              </el-select>
            </td>
          </tr>
          <tr>
            <td>
              <el-text class="noteText" type="primary">记录时间:</el-text>
              <el-date-picker class="inputBox" v-model="addRecordTime" type="datetime" placeholder="请选择出勤时间"
                style="margin-left: 16px;width:200px" />
            </td>
            <td> <el-text class="noteText" type="primary" style="padding-left:6px">记录警员编号：</el-text>
              <el-input class="inputBox" v-model="addPolicemenID" placeholder="请输入警员编号"
                oninput="value=value.replace(/[^\d.]/g,'')" clearable maxlength="7" show-word-limit />
            </td>
          </tr>
        </table>

        <div class="seaButton">
          <el-button type="primary" @click="addVideoInfo">上传录像</el-button>
        </div>
      </div>
    </el-tab-pane>

    <!-- 删除录像栏 -->
    <el-tab-pane label="删除录像" name="3">
      <div style="text-align: center;">
        <table style="display: inline-block;text-align: left;">
          <tr>
            <td>
              <el-text class="noteText" type="primary">录像编号：</el-text>
              <el-input class="inputBox" v-model="delVideoID" placeholder="请输入录像编号"
                oninput="value=value.replace(/[^\d.]/g,'')" clearable maxlength="5" show-word-limit />
            </td>
            <td>
              <el-button type="primary" @click="delVideoInfo" style="margin-left: 20px">删除录像</el-button>
            </td>
          </tr>
        </table>
      </div>
    </el-tab-pane>

    <!-- 修改录像栏 -->
    <el-tab-pane label="修改信息" name="4">
      <div style="text-align: center;">
        <table style="display: inline-block;text-align: left;">
          <tr>
            <td>
              <el-text class="noteText" type="primary" style="padding-left:6px">录像编号：</el-text>
              <el-input class="inputBox" v-model="updVideoID" placeholder="请输入录像编号"
                oninput="value=value.replace(/[^\d.]/g,'')" clearable maxlength="5" show-word-limit />
            </td>
            <td>
              <el-text class="noteText" type="primary" style="padding-left:6px">更新录像类型：</el-text>
              <el-select class="inputBox" v-model="updVideoType" placeholder="请选择">
                <el-option label="审讯" value="审讯"></el-option>
                <el-option label="监控" value="监控"></el-option>
                <el-option label="调查" value="调查"></el-option>
              </el-select>
            </td>
          </tr>
          <tr>
            <td>
              <el-text class="noteText" type="primary">更新记录时间:</el-text>
              <el-date-picker class="inputBox" v-model="updRecordTime" type="date" placeholder="请选择出勤日期"
                style="margin-left: 16px;width:200px" />
            </td>
            <td>
              <el-text class="noteText" type="primary" style="padding-left:6px">更新警员编号：</el-text>
              <el-input class="inputBox" v-model="updPolicemenID" placeholder="请输入警员编号"
                oninput="value=value.replace(/[^\d.]/g,'')" clearable maxlength="7" show-word-limit />
            </td>
          </tr>
        </table>
        <div class="seaButton">
          <el-button type="primary" @click="updVideoInfo">上传修改信息</el-button>
        </div>
      </div>
    </el-tab-pane>
  </el-tabs>
</template>

<script>
import axios from 'axios';
import { ElMessage } from 'element-plus';

export default {
  data() {
    return {
      anv: "1",
      videoID: "",
      videoType: "",
      recordTime: new Date(),
      isT: "F",
      policemenID: "",
      videoInfo: [],
      addVideoID: "",
      addVideoType: "",
      addRecordTime: new Date(),
      addPolicemenID: "",
      updVideoID: "",
      updVideoType: "",
      updRecordTime: new Date(),
      updPolicemenID: "",
    }
  },
  methods: {
    handleMouseMove(event) {
      const x = event.pageX - event.target.offsetLeft;
      const y = event.pageY - event.target.offsetTop;

      event.target.style.setProperty("--x", `${x}px`);
      event.target.style.setProperty("--y", `${y}px`);
    },
    async fetchVideoInfo() {
      try {
        this.recordTime = new Date(this.recordTime).toISOString();
        const response = await axios.get(`http://localhost:7078/api/videoInfo?videoID=${encodeURIComponent(this.videoID)}&recordTime=${encodeURIComponent(this.recordTime)}&isT=${encodeURIComponent(this.isT)}&policemenID=${encodeURIComponent(this.policemenID)}`);
        this.videoInfo = response.data;
      } catch (error) {
        // 请求失败时的处理逻辑
        ElMessage({
          showClose: true,
          message: "获取录像列表失败！",
          type: 'error',
          duration: 5000,
        });
      }
    },
    async addVideoInfo() {
      try {
        this.addRecordTime = new Date(this.addRecordTime).toISOString();
        const response = await axios.post(`http://localhost:7078/api/addVideo?videoID=${encodeURIComponent(this.addVideoID)}&videoType=${encodeURIComponent(this.addVideoType)}&recordTime=${encodeURIComponent(this.addRecordTime)}&policemenID=${encodeURIComponent(this.addPolicemenID)}`);
        if (typeof response.data == "string") {
          ElMessage({
            showClose: true,
            message: response.data,
            type: 'warning',
            duration: 5000,
          });
        }
        else {
          await this.fetchVideoInfo();
          ElMessage({
            showClose: true,
            message: '新增录像成功! ',
            type: 'success',
            duration: 5000,
          });
        }
      } catch (error) {
        // 请求失败时的处理逻辑
        ElMessage({
          showClose: true,
          message: "新增录像失败！",
          type: 'error',
          duration: 5000,
        });
      }
    },
    async delVideoInfo() {
      try {
        const response = await axios.delete(`http://localhost:7078/api/delVideo?videoID=${encodeURIComponent(this.delVideoID)}`);
        if (typeof response.data == "string") {
          ElMessage({
            showClose: true,
            message: response.data,
            type: 'warning',
            duration: 5000,
          });
        }
        else {
          await this.fetchVideoInfo();
          ElMessage({
            showClose: true,
            message: '删除录像成功! ',
            type: 'success',
            duration: 5000,
          });
        }
      } catch (error) {
        // 请求失败时的处理逻辑
        ElMessage({
          showClose: true,
          message: "删除录像失败！",
          type: 'error',
          duration: 5000,
        });
      }
    },
    async updVideoInfo() {
      try {
        this.updRecordTime = new Date(this.updRecordTime).toISOString();
        const response = await axios.put(`http://localhost:7078/api/updVideo?videoID=${encodeURIComponent(this.updVideoID)}&videoType=${encodeURIComponent(this.updVideoType)}&recordTime=${encodeURIComponent(this.updRecordTime)}&policemenID=${encodeURIComponent(this.updPolicemenID)}`);
        if (typeof response.data == "string") {
          ElMessage({
            showClose: true,
            message: response.data,
            type: 'warning',
            duration: 5000,
          });
        }
        else {
          await this.fetchVideoInfo();
          ElMessage({
            showClose: true,
            message: '修改成功! ',
            type: 'success',
            duration: 5000,
          });
        }
      } catch (error) {
        // 请求失败时的处理逻辑
        ElMessage({
          showClose: true,
          message: "修改失败！",
          type: 'error',
          duration: 5000,
        });
      }
    },
  },
};
</script>

<style lang="postcss" scoped>
.sub-header {
  overflow: hidden;
  display: flex;
  position: absolute;
  top: 70px;
  left: 199px;
  width: calc(100% - 199px);
  height: 7vh;
  min-height: 40px;
  align-items: center;
  /* 文字竖直方向居中对齐 */
  background-color: #1f2cdf;
  box-shadow: inset -500px 0px 200px 0px rgba(4, 0, 113, 0.856);
  color: #ffffff;
  font-size: 28px;
}

.sub-header::before {
  --size: 0;
  content: '';
  position: absolute;
  left: var(--x);
  top: var(--y);
  width: var(--size);
  height: var(--size);
  background: radial-gradient(circle closest-side, #5a65ff, transparent);
  transform: translate(-50%, -50%);
  transition: width .2s ease, height .2s ease;
}

.sub-header:hover::before {
  --size: 400px;
}

.seaButton {
  display: block;
  text-align: center;
  margin-top: 15px;
  margin-bottom: 10px;
}

.noteText {
  font-size: 18px;
  text-align: center;
  margin: 20px 0px;
  width: 10vw;
  display: inline-block;
}

.inputBox {
  display: inline-block;
  width: 200px;
  height: 32px;
  margin: 20px 10px;
}

.qianru {
  width: 7px;
}
</style>