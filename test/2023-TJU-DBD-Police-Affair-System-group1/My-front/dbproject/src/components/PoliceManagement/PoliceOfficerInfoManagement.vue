<template>
  <el-header class="sub-header" @mousemove="handleMouseMove">
    <div>&nbsp;&nbsp;警员管理&nbsp;>&nbsp;警员信息管理</div>
  </el-header>

  <el-tabs v-model="anv" type="border-card" style="margin-top: 10vh;">
    <!-- 查询栏 -->
    <el-tab-pane label="查询警员信息" name="1">
      <div style="text-align: center;">
        <table style="display: inline-block;text-align: left;">
          <tr>
            <td>
              <el-text class="noteText" type="primary">警员编号:</el-text>
              <el-input class="inputBox" v-model="policemenID" placeholder="请输入警员编号" clearable maxlength="7"
                oninput="value=value.replace(/[^\d.]/g,'')" style="width: 231px;" show-word-limit />
            </td>
            <td>
              <el-text class="noteText" type="primary">姓名:</el-text>
              <el-input class="inputBox" v-model="policemenName" style="width: 250px;" placeholder="请输入姓名" clearable />
            </td>
          </tr>
          <tr>
            <td>
              <el-text class="noteText" type="primary">身份证号:</el-text>
              <el-input class="inputBox" v-model="policemenIDNum" placeholder="请输入身份证号" clearable maxlength="18"
                oninput="value=value.replace(/[^\d.]/g,'')" style="width: 231px;" show-word-limit />
            </td>

            <td>
              <el-text class="noteText" type="primary">出生年份:</el-text>
              <el-input class="inputBox" v-model="policemenYear" style="width: 200px;" placeholder="请输入年份" clearable
                maxlength="4" oninput="value=value.replace(/[^\d.]/g,'')" show-word-limit @keyup.enter="checkYear"
                @blur="checkYear" />
            </td>
          </tr>
          <tr>
            <td>
              <el-text class="noteText" type="primary">性别:</el-text>
              <el-select class="inputBox" v-model="policemenSex" style="width: 231px;" placeholder="请选择">
                <el-option label="全部性别" selected value=""></el-option>
                <el-option label="男" value="M"></el-option>
                <el-option label="女" value="F"></el-option>
              </el-select>
            </td>
            <td>
              <el-text class="noteText" type="primary">民族:</el-text>
              <el-input class="inputBox" v-model="policemenNation" style="width: 250px;" placeholder="请输入民族" clearable />
            </td>
          </tr>
          <tr>
            <td>
              <el-text class="noteText" type="primary">状态:</el-text>
              <el-select class="inputBox" v-model="policemenStatus" style="width: 231px;" placeholder="请选择">
                <el-option label="全部状态" selected value=""></el-option>
                <el-option label="在职" value="在职"></el-option>
                <el-option label="离职" value="离职"></el-option>
              </el-select>
            </td>
            <td>
              <el-text class="noteText" type="primary">职务:</el-text>
              <el-select class="inputBox" v-model="policemenPosition" style="width: 200px;" placeholder="请选择">
                <el-option label="全部职务" selected value=""></el-option>
                <el-option label="学员" value="学员"></el-option>
                <el-option label="警员" value="警员"></el-option>
                <el-option label="警司" value="警司"></el-option>
                <el-option label="警督" value="警督"></el-option>
                <el-option label="警监" value="警监"></el-option>
                <el-option label="总警监" value="总警监"></el-option>
              </el-select>
            </td>
          </tr>
        </table>

        <div class="seaButton">
          <el-button type="primary" @click="fetchPolicemenInfo">查询</el-button>
        </div>
      </div>

      <el-divider />

      <div>
        <!-- 表格显示获取的警员信息 -->
        <el-table :data="policemenInfo" stripe height="450" @wheel.passive.stop>
          <el-table-column prop="pid" label="警员编号" sortable width="105px" />
          <el-table-column prop="name" label="姓名" sortable width="105px" />
          <el-table-column prop="idn" label="身份证号" sortable />
          <el-table-column prop="birthday" label="出生日期" sortable width="110px" />
          <el-table-column prop="gender" label="性别" sortable width="80px" />
          <el-table-column prop="nation" label="民族" sortable width="80px" />
          <el-table-column prop="phone" label="电话" sortable width="120px" />
          <el-table-column prop="email" label="邮箱" sortable />
          <el-table-column prop="status" label="状态" sortable width="80px" />
          <el-table-column prop="position" label="职务" sortable width="80px" />
        </el-table>
      </div>
    </el-tab-pane>

    <!-- 离职登记栏 -->
    <el-tab-pane label="警员离职登记" name="2">
      <div style="text-align: center;">
        <table style="display: inline-block;text-align: left;">
          <tr>
            <td>
              <el-text class="noteText" type="primary">警员编号：</el-text>
              <el-input class="inputBox" v-model="depPolicemenID" placeholder="请输入警员编号" clearable maxlength="7"
                oninput="value=value.replace(/[^\d.]/g,'')" show-word-limit />
            </td>
            <td>
              <el-button type="primary" @click="depart" style="margin-left: 20px">登记离职</el-button>
            </td>
          </tr>
        </table>


      </div>
    </el-tab-pane>

    <!-- 删除栏 -->
    <el-tab-pane label="删除警员信息" name="3">
      <div style="text-align: center;">
        <table style="display: inline-block;text-align: left;">
          <tr>
            <td>
              <el-text class="noteText" type="primary">警员编号：</el-text>
              <el-input class="inputBox" v-model="delPolicemenID" placeholder="请输入警员编号" clearable maxlength="7"
                oninput="value=value.replace(/[^\d.]/g,'')" show-word-limit />
            </td>
            <td>
              <el-button type="primary" @click="delPolicemenInfo" style="margin-left: 20px">删除</el-button>
            </td>
          </tr>
        </table>
      </div>
    </el-tab-pane>

    <!-- 修改警员信息栏 -->
    <el-tab-pane label="修改警员信息" name="4">
      <div style="text-align: center;">
        <table style="display: inline-block;text-align: left;">
          <tr>
            <td>
              <el-text class="noteText" type="primary">警员编号:</el-text>
              <el-input class="inputBox" v-model="updPolicemenID" placeholder="请输入警员编号" clearable maxlength="7"
                oninput="value=value.replace(/[^\d.]/g,'')" style="width: 231px;" show-word-limit />
            </td>
            <td>
              <el-text class="noteText" type="primary">更新姓名:</el-text>
              <el-input class="inputBox" v-model="updPolicemenName" style="width: 250px;" placeholder="请输入姓名" clearable />
            </td>
          </tr>
          <tr>
            <td>
              <el-text class="noteText" type="primary">更新身份证号:</el-text>
              <el-input class="inputBox" v-model="updPolicemenIDNum" placeholder="请输入身份证号" clearable maxlength="18"
                oninput="value=value.replace(/[^\d.]/g,'')" style="width: 231px;" show-word-limit />
            </td>

            <td>
              <el-text class="noteText" type="primary">更新出生日期:</el-text>
              <el-input class="inputBox" v-model="updPolicemenYear" style="width: 60px;margin-right:6px;" placeholder="年"
                maxlength="4" oninput="value=value.replace(/[^\d.]/g,'')" @keyup.enter="checkYear2" @blur="checkYear2" />
              -<el-input class="inputBox" v-model="updPolicemenMonth" style="width: 40px;margin-right:6px;"
                placeholder="月" maxlength="2" oninput="value=value.replace(/[^\d.]/g,'')" @keyup.enter="checkMonth"
                @blur="checkMonth" />
              -<el-input class="inputBox" v-model="updPolicemenDay" style="width: 40px;" placeholder="日" maxlength="2"
                oninput="value=value.replace(/[^\d.]/g,'')" @keyup.enter="checkDay" @blur="checkDay" />
            </td>
          </tr>
          <tr>
            <td>
              <el-text class="noteText" type="primary">更新性别:</el-text>
              <el-select class="inputBox" v-model="updPolicemenSex" style="width: 231px;" placeholder="请选择">
                <el-option label="男" value="M"></el-option>
                <el-option label="女" value="F"></el-option>
              </el-select>
            </td>
            <td>
              <el-text class="noteText" type="primary">更新民族:</el-text>
              <el-input class="inputBox" v-model="updPolicemenNation" style="width: 250px;" placeholder="请输入民族"
                clearable />
            </td>
          </tr>
          <tr>
            <td>
              <el-text class="noteText" type="primary">更新电话:</el-text>
              <el-input class="inputBox" v-model="updPolicemenPhone" style="width: 231px;" placeholder="请输入电话" clearable
                maxlength="11" oninput="value=value.replace(/[^\d.]/g,'')" show-word-limit />
            </td>
            <td>
              <el-text class="noteText" type="primary">更新邮件:</el-text>
              <el-input class="inputBox" v-model="updPolicemenEmail" style="width: 250px;" placeholder="请输入邮件"
                clearable />
            </td>
          </tr>
        </table>

        <div class="seaButton">
          <el-button type="primary" @click="updPolicemenInfo">修改信息</el-button>
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
      policemenID: "",
      policemenName: "",
      policemenIDNum: "",
      policemenYear: "",
      policemenSex: "",
      policemenNation: "",
      policemenStatus: "",
      policemenPosition: "",
      policemenInfo: [],
      delPolicemenID: "",
      depPolicemenID: "",
      updPolicemenID: "",
      updPolicemenName: "",
      updPolicemenIDNum: "",
      updPolicemenYear: "",
      updPolicemenMonth: "",
      updPolicemenDay: "",
      updPolicemenSex: "",
      updPolicemenNation: "",
      updPolicemenPhone: "",
      updPolicemenEmail: "",
    }
  },
  methods: {
    handleMouseMove(event) {
      const x = event.pageX - event.target.offsetLeft;
      const y = event.pageY - event.target.offsetTop;

      event.target.style.setProperty("--x", `${x}px`);
      event.target.style.setProperty("--y", `${y}px`);
    },
    checkYear() {
      if (Number(this.policemenYear) < 1900)
        this.policemenYear = "1900";
      if (Number(this.policemenYear) > 2100)
        this.policemenYear = "2100";
    },
    async fetchPolicemenInfo() {
      try {
        const response = await axios.get(`http://localhost:7078/api/policemenInfo?policemenID=${encodeURIComponent(this.policemenID)}&policemenName=${encodeURIComponent(this.policemenName)}&policemenIDNum=${encodeURIComponent(this.policemenIDNum)}&policemenYear=${encodeURIComponent(this.policemenYear)}&policemenSex=${encodeURIComponent(this.policemenSex)}&policemenNation=${encodeURIComponent(this.policemenNation)}&policemenStatus=${encodeURIComponent(this.policemenStatus)}&policemenPosition=${encodeURIComponent(this.policemenPosition)}`);
        this.policemenInfo = response.data.map(item => {
          if (item.gender === 'M') {
            return { ...item, gender: '男' };
          } else if (item.gender === 'F') {
            return { ...item, gender: '女' };
          } else {
            return item;
          }
        });
      } catch (error) {
        // 请求失败时的处理逻辑
        ElMessage({
          showClose: true,
          message: "获取警员列表失败！",
          type: 'error',
          duration: 5000,
        });
      }
    },
    async delPolicemenInfo() {
      try {
        const myPolicemenID = localStorage.getItem("policeNumber");
        const response = await axios.delete(`http://localhost:7078/api/delPolicemen?policemenID=${encodeURIComponent(this.delPolicemenID)}&myPolicemenID=${encodeURIComponent(myPolicemenID)}`);
        if (typeof response.data == "string") {
          ElMessage({
            showClose: true,
            message: response.data,
            type: 'warning',
            duration: 5000,
          });
        }
        else {
          await this.fetchPolicemenInfo();
          ElMessage({
            showClose: true,
            message: '删除警员成功! ',
            type: 'success',
            duration: 5000,
          });
        }
      } catch (error) {
        // 请求失败时的处理逻辑
        ElMessage({
          showClose: true,
          message: "删除警员失败！",
          type: 'error',
          duration: 5000,
        });
      }
    },
    async depart() {
      try {
        const myPolicemenID = localStorage.getItem("policeNumber");
        const response = await axios.put(`http://localhost:7078/api/depart?policemenID=${encodeURIComponent(this.depPolicemenID)}&myPolicemenID=${encodeURIComponent(myPolicemenID)}`);
        if (typeof response.data == "string") {
          ElMessage({
            showClose: true,
            message: response.data,
            type: 'warning',
            duration: 5000,
          });
        }
        else {
          await this.fetchPolicemenInfo();
          ElMessage({
            showClose: true,
            message: '登记成功! ',
            type: 'success',
            duration: 5000,
          });
        }
      } catch (error) {
        // 请求失败时的处理逻辑
        ElMessage({
          showClose: true,
          message: "登记失败！",
          type: 'error',
          duration: 5000,
        });
      }
    },
    async updPolicemenInfo() {
      try {
        const myPolicemenID = localStorage.getItem("policeNumber");
        const response = await axios.put(`http://localhost:7078/api/updPolicemen?policemenID=${encodeURIComponent(this.updPolicemenID)}&policemenName=${encodeURIComponent(this.updPolicemenName)}&policemenIDNum=${encodeURIComponent(this.updPolicemenIDNum)}&policemenYear=${encodeURIComponent(this.updPolicemenYear)}&policemenMonth=${encodeURIComponent(this.updPolicemenMonth)}&policemenDay=${encodeURIComponent(this.updPolicemenDay)}&policemenSex=${encodeURIComponent(this.updPolicemenSex)}&policemenNation=${encodeURIComponent(this.updPolicemenNation)}&myPolicemenID=${encodeURIComponent(myPolicemenID)}&policemenPhone=${encodeURIComponent(this.updPolicemenPhone)}&policemenEmail=${encodeURIComponent(this.updPolicemenEmail)}`);
        if (typeof response.data == "string") {
          ElMessage({
            showClose: true,
            message: response.data,
            type: 'warning',
            duration: 5000,
          });
        }
        else {
          await this.fetchPolicemenInfo();
          ElMessage({
            showClose: true,
            message: '修改成功! ',
            type: 'success',
            duration: 5000,
          });
        }
      } catch (error) {
        console.log(error)
        // 请求失败时的处理逻辑
        ElMessage({
          showClose: true,
          message: "修改失败！",
          type: 'error',
          duration: 5000,
        });
      }
    },
    checkYear2() {
      const year = Number(this.updPolicemenYear);
      var tag = 0;
      if (year < 1900) {
        this.updPolicemenYear = "1900";
        tag = 1;
      }
      if (year > 2100) {
        this.updPolicemenYear = "2100";
        tag = 1;
      }

      if (tag == 1) {
        this.updPolicemenMonth = "";
        this.updPolicemenDay = "";
      }
    },
    checkMonth() {
      const month = Number(this.updPolicemenMonth);
      var tag = 0;
      if (month < 1) {
        this.updPolicemenMonth = "1";
        tag = 1;
      }
      if (month > 12) {
        this.updPolicemenMonth = "12";
        tag = 1;
      }

      if (tag == 1)
        this.updPolicemenDay = "";
    },
    checkDay() {
      const year = Number(this.updPolicemenYear);
      const month = Number(this.updPolicemenMonth);
      const day = Number(this.updPolicemenDay);

      if (day < 1) {
        this.updPolicemenDay = "1";
      }

      if (month === 2) { // 2月份的处理逻辑
        if ((year % 4 === 0 && year % 100 !== 0) || (year % 400 === 0)) { // 闰年的情况
          if (day > 29) {
            this.updPolicemenDay = "29";
          }
        } else { // 非闰年的情况
          if (day > 28) {
            this.updPolicemenDay = "28";
          }
        }
      } else if (month === 4 || month === 6 || month === 9 || month === 11) { // 4、6、9、11月份的处理逻辑
        if (day > 30) {
          this.updPolicemenDay = "30";
        }
      } else { // 其他月份的处理逻辑（1、3、5、7、8、10、12月份）
        if (day > 31) {
          this.updPolicemenDay = "31";
        }
      }
    },
  },
  watch: {
    updPolicemenYear() {
      this.updPolicemenMonth = "";
      this.updPolicemenDay = "";
    },
    updPolicemenMonth() {
      this.updPolicemenDay = "";
    },
  }
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

.noteText {
  font-size: 18px;
  text-align: center;
  margin: 20px 0px;
  width: 10vw;
  display: inline-block;
}

.inputBox {
  display: inline-block;
  width: 250px;
  height: 32px;
  margin: 20px 10px;
}

.seaButton {
  display: block;
  text-align: center;
  margin-top: 15px;
  margin-bottom: 10px;
}
</style>