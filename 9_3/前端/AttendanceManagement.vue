<template>
  <div>
    <el-header class="sub-header" @mousemove="handleMouseMove">
      <div>&nbsp;&nbsp;警员管理&nbsp;>&nbsp;警员出勤管理</div>
    </el-header>
    <el-tabs v-model="anv" type="border-card" style="margin-top: 10vh;">
      <el-tab-pane label="警员出勤" name="1">
        <div class="form-container">
          <tr>
            <div>
              <td>
                <el-text class="note-text" type="primary">输入警员:</el-text>
                <el-input class="ssq-input" type="text" v-model="policemanNumber" placeholder="警员ID" clearable maxlength="7" show-word-limit />
                <el-text class="note-text" type="primary">输入地点:</el-text>
                <el-input class="ssq-input" type="text" v-model="area" placeholder="地点" show-word-limit />
              </td>
            </div>
            <div>
              <td>
                <div>
              <el-text class="note-text" type="primary">输入日期：</el-text>
              <div class="ssq-inputa">
                <el-input type="number" id="year" v-model="inputDate.year" placeholder="年" min="1900" max="2099" />
              </div>
              <div class="ssq-inputa">
                <el-input type="number" id="month" v-model="inputDate.month" placeholder="月" min="1" max="12" />
              </div>
              <div class="ssq-inputa">
                <el-input type="number" id="day" v-model="inputDate.day" min="1" placeholder="日" max="31" />
              </div>
              <div class="ssq-inputa">
                <el-input type="number" id="hour" v-model="inputDate.hour" min="0" placeholder="小时" max="23" />
              </div>
              <div class="ssq-inputa">
                <el-input type="number" id="minute" v-model="inputDate.minute" min="0" placeholder="分钟" max="59" />
              </div>
              <div class="ssq-inputa">
                <el-input type="number" id="second" v-model="inputDate.second" min="0" placeholder="秒钟" max="59" />
              </div>
              <el-button type="primary" @click="createDateTime">创建日期：</el-button>
              <div v-if="dateTimea">
                <el-text class="ssq-inputb" type="primary" position="center">{{ dateTimea }}</el-text>
              </div>
            </div>
              </td>
            </div>
          </tr>
        </div>
        <div class="seaButton">
          <el-button type="primary" @click="fetchAttendanceInfo">
            <span>查询</span>
          </el-button>
        </div>
        <el-table v-if="true" :data="attendinfo" stripe height="450" @wheel.passive.stop>
          <!-- Table columns here -->
          <el-table-column prop="policemanNumber" label="警号" sortable />
          <el-table-column prop="area" label="地点" sortable />
          <el-table-column prop="time" label="时间" sortable />
        </el-table>
        <div v-else>{{ boxContent }}</div>
      </el-tab-pane>
      <el-tab-pane label="警员出勤删除" name="2">
        <div class="form-container">
          <tr>
            <div>
              <td>
                <el-text class="note-text" type="primary">输入警员:</el-text>
                <el-input class="ssq-input" type="text" v-model="policemanNumber" placeholder="警员ID" clearable maxlength="7" oninput="value=value.replace(/[^\d.]/g,'')" show-word-limit />
                <el-text class="note-text" type="primary">输入地点:</el-text>
                <el-input class="ssq-input" type="text" v-model="area" placeholder="地点" oninput="value=value.replace(/[^\d.]/g,'')" show-word-limit />
              </td>
            </div>
            <div>
              <td>
                <div>
              <el-text class="note-text" type="primary">输入日期：</el-text>
              <div class="ssq-inputa">
                <el-input type="number" id="year" v-model="inputDate.year" placeholder="年" min="1900" max="2099" />
              </div>
              <div class="ssq-inputa">
                <el-input type="number" id="month" v-model="inputDate.month" placeholder="月" min="1" max="12" />
              </div>
              <div class="ssq-inputa">
                <el-input type="number" id="day" v-model="inputDate.day" min="1" placeholder="日" max="31" />
              </div>
              <div class="ssq-inputa">
                <el-input type="number" id="hour" v-model="inputDate.hour" min="0" placeholder="小时" max="23" />
              </div>
              <div class="ssq-inputa">
                <el-input type="number" id="minute" v-model="inputDate.minute" min="0" placeholder="分钟" max="59" />
              </div>
              <div class="ssq-inputa">
                <el-input type="number" id="second" v-model="inputDate.second" min="0" placeholder="秒钟" max="59" />
              </div>
              <el-button type="primary" @click="createDateTime">创建日期：</el-button>
              <div v-if="dateTimea">
                <el-text class="ssq-inputb" type="primary" position="center">{{ dateTimea }}</el-text>
              </div>
            </div>
              </td>
            </div>
          </tr>
        </div>
        <div class="seaButton">
          <el-button type="primary" @click="delAttendanceInfo">
            <span>删除</span>
          </el-button>
        </div>
      </el-tab-pane>
      <el-tab-pane label="警员出勤添加" name="3">
        <div class="form-container">
          <tr>
            <div>
              <td>
                <el-text class="note-text" type="primary">输入警员:</el-text>
                <el-input class="ssq-input" type="text" v-model="policemanNumber" placeholder="警员ID" clearable maxlength="7" oninput="value=value.replace(/[^\d.]/g,'')" show-word-limit />
                <el-text class="note-text" type="primary">输入地点:</el-text>
                <el-input class="ssq-input" type="text" v-model="area" placeholder="地点" oninput="value=value.replace(/[^\d.]/g,'')" show-word-limit />
              </td>
            </div>
            <div>
              <el-text class="note-text" type="primary">输入日期：</el-text>
              <div class="ssq-inputa">
                <el-input type="number" id="year" v-model="inputDate.year" placeholder="年" min="1900" max="2099" />
              </div>
              <div class="ssq-inputa">
                <el-input type="number" id="month" v-model="inputDate.month" placeholder="月" min="1" max="12" />
              </div>
              <div class="ssq-inputa">
                <el-input type="number" id="day" v-model="inputDate.day" min="1" placeholder="日" max="31" />
              </div>
              <div class="ssq-inputa">
                <el-input type="number" id="hour" v-model="inputDate.hour" min="0" placeholder="小时" max="23" />
              </div>
              <div class="ssq-inputa">
                <el-input type="number" id="minute" v-model="inputDate.minute" min="0" placeholder="分钟" max="59" />
              </div>
              <div class="ssq-inputa">
                <el-input type="number" id="second" v-model="inputDate.second" min="0" placeholder="秒钟" max="59" />
              </div>
              <el-button type="primary" @click="createDateTime">创建日期：</el-button>
              <div v-if="dateTimea">
                <el-text class="ssq-inputb" type="primary" position="center">{{ dateTimea }}</el-text>
              </div>
            </div>
          </tr>
        </div>
        <div class="seaButton">
          <el-button type="primary" @click="addAttendanceInfo">
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
        inputDate: {
          year: null,
          month: null,
          day: null,
          hour: null,
          minute: null,
          second: null,
        },
        dateTime: "1899-12-31T16:55:18.000Z",
        dateTimea: null,
        policemanNumber: "",
        area: '',
        attendinfo: [],
        err: "警员不存在！",
        boxContent: "",
      };
    },
    methods: {
      createDateTime() {
        const {
          year,
          month,
          day,
          hour,
          minute,
          second
        } = this.inputDate;
        if (year && month && day && hour !== null && minute !== null && second !== null) {
          const dateTime = new Date(year, month-1, day, hour, minute, second);
          if (!isNaN(dateTime)) {
            this.dateTime = dateTime.toISOString(); // 格式化日期时间
            this.dateTimea = dateTime;
          } else {
            alert('无效的日期时间');
          }
        } else {
          alert('请填写所有字段');
        }
      },
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
            area: this.area,
            time: this.dateTime,
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
          .post("http://localhost:7078/api/delattendInfo", {
            policemanNumber: this.policemanNumber,
            area: this.area,
            time: this.dateTime,
          })
          .then((res) => {
            console.log(res.data);
          })
          .catch((err) => {
            this.boxContent = this.err;
            console.log(err);
          });
      },
      addAttendanceInfo() {
        axios
          .post("http://localhost:7078/api/addattendInfo", {
            policemanNumber: this.policemanNumber,
            area: this.area,
            time: this.dateTime,
          })
          .then((res) => {
            location.reload();
          })
          .catch((err) => {
            this.boxContent = this.err;
            console.log(err);
            location.reload();
          });
      },
      clearInfo() {
        // Handle clearInfo button click to clear information
        this.policemanNumber = "";
        this.area = "";
        this.time = "";
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
  .floating-input {
    display: flex;
    align-items: center;
    margin: 0 20px;
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
  .ssq-inputa {
    position: left;
    display: inline-block;
    width: 100px;
    height: 32px;
  }
  .ssq-inputb {
    position: center;
    height: 32px;
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
  .seaButton {
    display: block;
    text-align: center;
    margin-top: 15px;
    margin-bottom: 10px;
  }
</style>
