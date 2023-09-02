<template>
  <div>
    <el-header class="sub-header" @mousemove="handleMouseMove">
      <div>&nbsp;&nbsp;警员管理&nbsp;>&nbsp;警员出勤管理</div>
    </el-header>
    <el-tabs v-model="anv" type="border-card" style="margin-top: 10vh;">
      <el-tab-pane label="警员出勤" name="1">
        <div class="form-container">
          <div class="floating-input">
            <el-text class="note-text" type="primary">输入视频ID:</el-text>
            <input class="ssq-input" type="text" v-model="audioid" placeholder="视频ID" clearable maxlength="7" @input="onInputNumber" show-word-limit />
          </div>
          <div class="floating-input">
            <el-text class="note-text" type="primary">输入电话号码:</el-text>
            <input class="ssq-input" type="text" v-model="phonenumber" placeholder="电话号码" clearable maxlength="9" @input="onInputStation" show-word-limit />
          </div>
          <div class="floating-input">
            <el-text class="note-text" type="primary">输入接警时间:</el-text>
            <input class="ssq-input" type="text" v-model="calltime" placeholder="接警时间" clearable maxlength="9" @input="onInputStation" show-word-limit />
          </div> 
          <div class="floating-input">
            <el-text class="note-text" type="primary">输入案件号码:</el-text>
            <input class="ssq-input" type="text" v-model="caseid" placeholder="案件号码" />
          </div>
          <div class="floating-input">
            <el-text class="note-text" type="primary">输入接警号码:</el-text>
            <input class="ssq-input" type="text" v-model="answernumber" placeholder="接警号码" />
          </div>
        </div>
        <div class="btn-container">
          <button class="ssq-button" @click="fetchCallInfo">
                  <span>查询</span>
                </button>
          <button class="ssq-button" @click="clearInfo">
                  <span>清除信息</span>
                </button>
        </div>
        <el-table v-if="true" :data="callinfo" stripe height="450" @wheel.passive.stop>
          <!-- Table columns here -->
          <el-table-column prop="audioid" label="视频ID" sortable />
          <el-table-column prop="phonenumber" label="电话号码" sortable />
          <el-table-column prop="calltime" label="接警时间" sortable />
          <el-table-column prop="caseid" label="案件号码" sortable />
          <el-table-column prop="answernumber" label="接警号码" sortable />
        </el-table>
        <div v-else>{{ boxContent }}</div>
      </el-tab-pane>
      <el-tab-pane label="警员出勤删除" name="2">
        <div class="floating-input">
            <el-text class="note-text" type="primary">输入视频ID:</el-text>
            <input class="ssq-input" type="text" v-model="audioid" placeholder="视频ID" clearable maxlength="7" @input="onInputNumber" show-word-limit />
          </div>
        <div class="btn-container">
          <button class="ssq-button" @click="delCallInfo">
                  <span>删除</span>
                </button>
          <button class="ssq-button" @click="clearInfo">
                  <span>清除信息</span>
                </button>
        </div>
      </el-tab-pane>
      <el-tab-pane label="警员出勤添加" name="3">
        <div class="form-container">
          <div class="floating-input">
            <el-text class="note-text" type="primary">输入视频ID:</el-text>
            <input class="ssq-input" type="text" v-model="audioid" placeholder="视频ID" clearable maxlength="7" @input="onInputNumber" show-word-limit />
          </div>
          <div class="floating-input">
            <el-text class="note-text" type="primary">输入电话号码:</el-text>
            <input class="ssq-input" type="text" v-model="phonenumber" placeholder="电话号码" clearable maxlength="9" @input="onInputStation" show-word-limit />
          </div>
          <div class="floating-input">
            <el-text class="note-text" type="primary">输入接警时间:</el-text>
            <input class="ssq-input" type="text" v-model="calltime" placeholder="接警时间" clearable maxlength="9" @input="onInputStation" show-word-limit />
          </div> 
          <div class="floating-input">
            <el-text class="note-text" type="primary">输入案件号码:</el-text>
            <input class="ssq-input" type="text" v-model="caseid" placeholder="案件号码" />
          </div>
          <div class="floating-input">
            <el-text class="note-text" type="primary">输入接警号码:</el-text>
            <input class="ssq-input" type="text" v-model="answernumber" placeholder="接警号码" />
          </div>
        </div>
        <div class="btn-container">
          <button class="ssq-button" @click="addCallInfo">
                  <span>添加</span>
                </button>
          <button class="ssq-button" @click="clearInfo">
                  <span>清除信息</span>
                </button>
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
      onInputNumber(event) {
        // Input validation logic here
        event.target.value = event.target.value.replace(/[^\d.]/g, "");
      },
      onInputStation(event) {
        // Input validation logic here
        event.target.value = event.target.value.replace(/[^\d.]/g, "");
      },
      fetchCallInfo() {
        axios
          .post("http://localhost:7078/api/callInfo", {
            audioid: this.audioid,
            calltime: this. calltime,
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
            this.callinfo = res.data;
            console.log(res.data);
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
            calltime: this. calltime,
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
    flex-direction: row;
    justify-content: center;
    margin-bottom: 20px;
  }
  .floating-input {
    display: flex;
    flex-direction: column;
    align-items: center;
    margin: 0 20px;
    position: relative;
  }
  .note-text {
    margin-top: 6vh;
    font-size: 18px;
    text-align: left;
    width: 10vw;
    display: inline-block;
  }
  .ssq-input {
    margin-top: 4.5vh;
    margin-bottom: 1vh;
    display: block;
    width: 10vw;
    padding: 0.8em;
    outline: none;
    border: 1px solid #e3e3e3;
    border-radius: 2px;
  }
  .btn-container {
    display: flex;
    justify-content: center;
  }
  .ssq-button {
    margin-top: 3vh;
    margin-bottom: 5vh;
    width: 150px;
    height: 40px;
    padding: 0 30px;
    line-height: 60px;
    text-align: center;
    position: relative;
    appearance: none;
    background: #dfd8c8;
    border: none;
    color: white;
    font-size: 1.2em;
    cursor: pointer;
    outline: none;
    overflow: hidden;
    border-radius: 100px;
  }
  .ssq-button span {
    position: relative;
    top: -20%;
  }
  .ssq-button::before {
    --size: 0;
    content: "";
    position: absolute;
    left: var(--x);
    top: var(--y);
    width: var(--size);
    height: var(--size);
    background: radial-gradient(circle closest-side, #0b72bbb2, transparent);
    transform: translate(-50%, -50%);
    transition: width 0.2s ease, height 0.2s ease;
  }
  .ssq-button:hover::before {
    --size: 400px;
  }
</style>
