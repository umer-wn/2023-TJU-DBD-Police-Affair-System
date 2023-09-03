<template>
  <div>
    <el-header class="sub-header" @mousemove="handleMouseMove">
      <div>&nbsp;&nbsp;警员管理&nbsp;>&nbsp;接警记录管理</div>
    </el-header>
    <el-tabs v-model="anv" type="border-card" style="margin-top: 10vh;">
      <el-tab-pane label="记录查询" name="1">
        <div style="text-align: center;">
          <table style="display: inline-block;text-align: left;">
            <tr>
              <td>
                <div>
                  <el-text class="note-text" type="primary">视频ID:</el-text>
                  <el-input class="ssq-input" v-model="audioid" placeholder="视频ID" clearable maxlength="7"  oninput="value=value.replace(/[^\d.]/g,'')" show-word-limit />
                </div>
                <div>
                  <el-text class="note-text" type="primary">电话号码:</el-text>
                  <el-input class="ssq-input" v-model="phonenumber" placeholder="电话号码" clearable maxlength="9"  oninput="value=value.replace(/[^\d.]/g,'')" show-word-limit />
                </div>
              </td>
              <td>
                <div>
                  <el-text class="note-text" type="primary">接警时间:</el-text>
                  <el-input class="ssq-input" type="text" v-model="calltime" placeholder="接警时间" clearable maxlength="9"  oninput="value=value.replace(/[^\d.]/g,'')" show-word-limit />
                </div>
                <div>
                  <el-text class="note-text" type="primary">案件号码:</el-text>
                  <el-input class="ssq-input" type="text" v-model="caseid" placeholder="案件号码" />
                </div>
              </td>
            </tr>
            <div>
              <el-text class="note-text" type="primary">接警号码:</el-text>
              <el-input class="ssq-input" type="text" v-model="answernumber" placeholder="接警号码" />
            </div>
            <div class="seaButton">
            <el-button type="primary" @click="fetchCallInfo">
              <span>查询</span>
            </el-button></div>
          </table>
          <el-table v-if="true" :data="callinfo" stripe height="450" @wheel.passive.stop>
            <!-- Table columns here -->
            <el-table-column prop="audioid" label="视频ID" sortable />
            <el-table-column prop="phonenumber" label="电话号码" sortable />
            <el-table-column prop="calltime" label="接警时间" sortable />
            <el-table-column prop="caseid" label="案件号码" sortable />
            <el-table-column prop="answernumber" label="接警号码" sortable />
          </el-table>
          <div v-else>{{ boxContent }}</div>
        </div>
      </el-tab-pane>
      <el-tab-pane label="记录删除" name="2">
        <div style="text-align: center;">
          <el-text class="note-text" type="primary">输入ID:</el-text>
          <el-input class="ssq-input" type="text" v-model="audioid" placeholder="视频ID" clearable maxlength="7"  oninput="value=value.replace(/[^\d.]/g,'')" show-word-limit />
        </div>
        <div class="seaButton">
        <el-button type="primary" @click="delCallInfo">
          <span>删除</span>
        </el-button></div>
      </el-tab-pane>
      <el-tab-pane label="记录添加" name="3">
        <div style="text-align: center;">
          <table style="display: inline-block;text-align: left;">
            <tr>
              <td>
                <div>
                  <el-text class="note-text" type="primary">视频ID:</el-text>
                  <el-input class="ssq-input" v-model="audioid" placeholder="视频ID" clearable maxlength="7"  oninput="value=value.replace(/[^\d.]/g,'')" show-word-limit />
                </div>
                <div>
                  <el-text class="note-text" type="primary">电话号码:</el-text>
                  <el-input class="ssq-input" v-model="phonenumber" placeholder="电话号码" clearable maxlength="9"  oninput="value=value.replace(/[^\d.]/g,'')" show-word-limit />
                </div>
              </td>
              <td>
                <div>
                  <el-text class="note-text" type="primary">接警时间:</el-text>
                  <el-input class="ssq-input" type="text" v-model="calltime" placeholder="接警时间" clearable maxlength="9"  oninput="value=value.replace(/[^\d.]/g,'')" show-word-limit />
                </div>
                <div>
                  <el-text class="note-text" type="primary">案件号码:</el-text>
                  <el-input class="ssq-input" type="text" v-model="caseid" placeholder="案件号码" />
                </div>
              </td>
            </tr>
            <div>
              <el-text class="note-text" type="primary">接警号码:</el-text>
              <el-input class="ssq-input" type="text" v-model="answernumber" placeholder="接警号码" />
            </div>
          </table>
        </div>
        <div class="seaButton">
          <el-button type="primary" @click="addCallInfo">
            <span>添加</span>
          </el-button>
        </div>
      </el-tab-pane>
    </el-tabs>
  </div>
</template>

<script>
  import axios from "axios";
  export default {
    data() {
      return {
        anv: "1",
        audioid: "",
        calltime: "",
        caseid: "",
        phonenumber: "",
        answernumber: "",
        callinfo: [],
        err: "警员不存在！",
        boxContent: "",
      };
    },
    methods: {
      handleMouseMove(event) {
        const x = event.pageX - event.target.offsetLeft;
        const y = event.pageY - event.target.offsetTop;
        event.target.style.setProperty("--x", `${x}px`);
        event.target.style.setProperty("--y", `${y}px`);
      },
      fetchCallInfo() {
        axios
          .post("http://localhost:7078/api/callInfo", {
            audioid: this.audioid,
            calltime: this.calltime,
            caseid: this.caseid,
            phonenumber: this.phonenumber,
            answernumber: this.answernumber,
          })
          .then((res) => {
            this.callinfo = res.data;
            console.log(res.data);
          })
          .catch((err) => {
            this.boxContent = this.err;
            console.log(err);
          });
      },
      delCallInfo() {
        axios
          .post("http://localhost:7078/api/delcallInfo", {
            audioid: this.audioid,
          })
          .then((res) => {
            console.log(res.data);
            location.reload();
          })
          .catch((err) => {
            this.boxContent = this.err;
            console.log(err);
          });
      },
      addCallInfo() {
        axios
          .post("http://localhost:7078/api/addcallInfo", {
            audioid: this.audioid,
            calltime: this.calltime,
            caseid: this.caseid,
            phonenumber: this.phonenumber,
            answernumber: this.answernumber,
          })
          .then((res) => {
            location.reload();
            console.log(res.data);
          })
          .catch((err) => {
            this.boxContent = this.err;
            console.log(err);
          });
      },
      clearInfo() {
        // Handle clearInfo button click to clear information
        this.audioid = "";
        this.calltime = "";
        this.caseid = "";
        this.phonenumber = "";
        this.answernumber = "";
        this.attendinfo = [];
        this.boxContent = "";
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
  .content {
    /* Content styles */
    display: flex;
    flex-direction: column;
    align-items: center;
  }
  .form-container {
    display: flex;
    flex-direction: line;
    justify-content: center;
    margin-bottom: 20px;
  }
  .note-text {
    font-size: 18px;
    text-align: center;
    margin: 20px 0px;
    width: 10vw;
    display: inline-block;
  }
  .ssq-input {
    position: left;
    display: inline-block;
    width: 200px;
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
