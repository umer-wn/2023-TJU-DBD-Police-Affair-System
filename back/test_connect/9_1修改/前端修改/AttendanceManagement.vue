<template>
  <div>
    <el-header class="sub-header" @mousemove="handleMouseMove">
      <div>&nbsp;&nbsp;警员管理&nbsp;>&nbsp;警员出勤管理</div>
    </el-header>
    <el-tabs v-model="anv" type="border-card" style="margin-top: 10vh;">
      <el-tab-pane label="警员出勤" name="1">
          <div class="form-container">
            <div class="floating-input">
              <el-text class="note-text" type="primary">输入警员ID:</el-text>
              <input class="ssq-input" type="text" v-model="policemanNumber" placeholder="警员ID" clearable maxlength="7" @input="onInputNumber" show-word-limit />
            </div>
            <div class="floating-input">
              <el-text class="note-text" type="primary">输入警局ID:</el-text>
              <input class="ssq-input" type="text" v-model="stationID" placeholder="警局ID" clearable maxlength="9" @input="onInputStation" show-word-limit />
            </div>
          </div>
          <div class="btn-container">
            <button class="ssq-button" @click="fetchAttendanceInfo">
              <span>查询</span>
            </button>
            <button class="ssq-button" @click="clearInfo">
              <span>清除信息</span>
            </button>
          </div>
          <el-table v-if="true" :data="attendinfo" stripe height="450" @wheel.passive.stop>
            <!-- Table columns here -->
            <el-table-column prop="policemanNumber" label="警号" sortable />
            <el-table-column prop="stationID" label="警局ID" sortable />
            <el-table-column prop="intime" label="开始时间" sortable />
            <el-table-column prop="outtime" label="结束时间" sortable />
          </el-table>
          <div v-else>{{ boxContent }}</div>
      </el-tab-pane>
      <el-tab-pane label="警员出勤删除" name="2">
          <div class="form-container">
            <div class="floating-input">
              <el-text class="note-text" type="primary">输入警员ID:</el-text>
              <input class="ssq-input" type="text" v-model="policemanNumber" placeholder="警员ID" clearable maxlength="7" @input="onInputNumber" show-word-limit />
            </div>
            <div class="floating-input">
              <el-text class="note-text" type="primary">输入警局ID:</el-text>
              <input class="ssq-input" type="text" v-model="stationID" placeholder="警局ID" clearable maxlength="9" @input="onInputStation" show-word-limit />
            </div>
          </div>
          <div class="btn-container">
            <button class="ssq-button" @click="delAttendanceInfo">
              <span>删除</span>
            </button>
            <button class="ssq-button" @click="clearInfo">
              <span>清除信息</span>
            </button>
          </div>
          <el-table v-if="true" :data="attendinfo" stripe height="450" @wheel.passive.stop>
            <!-- Table columns here -->
            <el-table-column prop="policemanNumber" label="警号" sortable />
            <el-table-column prop="stationID" label="警局ID" sortable />
            <el-table-column prop="intime" label="开始时间" sortable />
            <el-table-column prop="outtime" label="结束时间" sortable />
          </el-table>
          <div v-else>{{ boxContent }}</div>
      </el-tab-pane>
      <el-tab-pane label="警员出勤添加" name="3">
          <div class="form-container">
            <div class="floating-input">
              <el-text class="note-text" type="primary">输入警员ID:</el-text>
              <input class="ssq-input" type="text" v-model="policemanNumber" placeholder="警员ID" clearable maxlength="7" @input="onInputNumber" show-word-limit />
            </div>
            <div class="floating-input">
              <el-text class="note-text" type="primary">输入警局ID:</el-text>
              <input class="ssq-input" type="text" v-model="stationID" placeholder="警局ID" clearable maxlength="9" @input="onInputStation" show-word-limit />
            </div>
            <div class="floating-input">
              <el-text class="note-text" type="primary">输入进入时间:</el-text>
              <input class="ssq-input" type="text" v-model="intime" placeholder="进入时间" />
            </div>
            <div class="floating-input">
              <el-text class="note-text" type="primary">输入离开时间:</el-text>
              <input class="ssq-input" type="text" v-model="outtime" placeholder="离开时间" />
            </div>
          </div>
          <div class="btn-container">
            <button class="ssq-button" @click="addAttendanceInfo">
              <span>添加</span>
            </button>
            <button class="ssq-button" @click="clearInfo">
              <span>清除信息</span>
            </button>
          </div>
          <el-table v-if="true" :data="attendinfo" stripe height="450" @wheel.passive.stop>
            <!-- Table columns here -->
            <el-table-column prop="policemanNumber" label="警号" sortable />
            <el-table-column prop="stationID" label="警局ID" sortable />
            <el-table-column prop="intime" label="开始时间" sortable />
            <el-table-column prop="outtime" label="结束时间" sortable />
          </el-table>
          <div v-else>{{ boxContent }}</div>
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
        policemanNumber: "",
        stationID: "",
        intime: "",
        outtime: "",
        attendinfo: [],
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
      fetchAttendanceInfo() {
        axios
          .post("http://localhost:7078/api/attendInfo", {
            policemanNumber: this.policemanNumber,
            stationID: this.stationID,
          })
          .then((res) => {
            this.attendinfo = res.data;
            console.log(res.data);
          })
          .catch((err) => {
            this.boxContent = this.err;
            console.log(err);
          });
      },
      delAttendanceInfo() {
        axios
          .post("http://localhost:7078/api/attendInfo", {
            policemanNumber: this.policemanNumber,
            stationID: this.stationID,
          })
          .then((res) => {
            this.attendinfo = res.data;
            console.log(res.data);
          })
          .catch((err) => {
            this.boxContent = this.err;
            console.log(err);
          });
      },
      addAttendanceInfo() {
        axios
          .post("http://localhost:7078/api/attendInfo", {
            policemanNumber: this.policemanNumber,
            stationID: this.stationID,
            intime:this.intime,
            outtime:this.outtime,
          })
          .then((res) => {
            this.attendinfo = res.data;
            console.log(res.data);
          })
          .catch((err) => {
            this.boxContent = this.err;
            console.log(err);
          });
      },
      clearInfo() {
        // Handle clearInfo button click to clear information
        this.policemanNumber = "";
        this.stationID = "";
        this.intime = "";
        this.outtime = "";
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
