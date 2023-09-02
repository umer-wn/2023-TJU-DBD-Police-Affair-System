<template>
  <el-header class="sub-header" @mousemove="handleMouseMove">
    <div>&nbsp;&nbsp;警员管理&nbsp;>&nbsp;警员信息管理</div>
  </el-header>
  <el-tabs v-model="anv" type="border-card" style="margin-top: 10vh;">
    <el-tab-pane label="查询警员信息" name="1">
      <div style="text-align: center;">
        <section>
          <div class="inputcontainer">
            <el-text class="noteText" type="primary">警员ID:</el-text>
            <el-input class="inputBox" type="text" v-model="policemenNumber" placeholder="警员ID" clearable maxlength="7"
            oninput="value=value.replace(/[^\d.]/g,'')" show-word-limit />
            <el-text class="noteText" type="primary">警员姓名:</el-text>
            <el-input class="inputBox" type="text" v-model="policemenName" placeholder="警员姓名" />
          </div>
          <div class="inputcontainer">
            <el-text class="noteText" type="primary">工作状态:</el-text>
            <el-select class="inputBox" v-model="policemenStatus">
              <el-option selected value="全部">全部</el-option>
              <el-option value="在职">在职</el-option>
              <el-option value="离职">离职</el-option>
            </el-select>
            <el-text class="noteText" type="primary">选择职务:</el-text>
            <el-select class="inputBox" v-model="policemenPosition">
              <el-option selected value="全部">全部</el-option>
              <el-option value="学员">学员</el-option>
              <el-option value="警员">警员</el-option>
              <el-option value="警司">警司</el-option>
              <el-option value="警督">警督</el-option>
              <el-option value="警监">警监</el-option>
              <el-option value="总警监">总警监</el-option>
            </el-select>
          </div>
          <div class="seaButton">
            <el-button type="primary" @click="fetchpolicemenInfo">
              <span>查询</span>
            </el-button>
          </div>
          <el-table v-if="true" :data="policemenInfo" stripe height="450" @wheel.passive.stop>
            <el-table-column prop="policemenNumber" label="警号" sortable/>
            <el-table-column prop="policemenName" label="姓名" />
            <el-table-column prop="idNumber" label="身份证号" width="200px" />
            <el-table-column prop="birthday" label="出生日期" />
            <el-table-column prop="gender" label="性别" />
            <el-table-column prop="nation" label="民族" />
            <el-table-column prop="phoneNumber" label="联系电话" />
            <el-table-column prop="policemenStatus" label="状态" />
            <el-table-column prop="policemenPosition" label="职务" />
          </el-table>
          <!-- 错误提示 -->
          <div v-else>{{ boxContent }}</div>
        </section>
      </div>
    </el-tab-pane>
    <el-tab-pane label="删除警员信息" name="2">
      <div style="text-align: center;">
        <section>
          <div class="inputcontainer">
            <el-text class="noteTexta" type="primary">输入警员ID</el-text>
            <el-input class="ssqinputinfobox" type="text" v-model="policemenNumber" placeholder="警员ID" clearable maxlength="7"
            oninput="value=value.replace(/[^\d.]/g,'')" show-word-limit />
          </div>
          <div class="seaButton">
            <el-button type="primary" @click="delpolicemenInfo($event, this.policemenNumber)">
              <span>删除</span>
            </el-button>
          </div>
        </section>
      </div>
    </el-tab-pane>
  </el-tabs>
</template>

<script>
  import axios from "axios";
  export default {
    data() {
      return {
        anv: "1",
        show: true,
        policemenNumber: "",
        policemenName: "",
        policemenStatus: "全部",
        policemenPosition: "全部",
        policemenInfo: [],
        err: "警员不存在！",
        boxContent: "",
      };
    },
    methods: {
      handleMouseMove(event) {
        const x = event.pageX - event.target.offsetLeft;
        const y = event.pageY - event.target.offsetTop;
        event.target.style.setProperty('--x', `${x}px`);
        event.target.style.setProperty('--y', `${y}px`);
      },
      info() {
        console.log("警号：" + this.policemenNumber);
        console.log("姓名：" + this.policemenName);
        console.log("状态：" + this.policemenStatus);
        console.log("职务：" + this.policemenPosition);
      },
      fetchpolicemenInfo() {
        axios
          .post("http://localhost:7078/api/policemenInfo", {
            policemenNumber: this.policemenNumber,
            policemenName: this.policemenName,
            policemenStatus: this.policemenStatus,
            policemenPosition: this.policemenPosition,
          })
          .then((res) => {
            this.policemenInfo = res.data;
            for (var i = 0; i < this.policemenInfo.length; i++) {
              if (this.policemenInfo[i].gender === "F") {
                this.policemenInfo[i].gender = "女";
              } else if (this.policemenInfo[i].gender === "M") {
                this.policemenInfo[i].gender = "男";
              }
            }
          })
          .catch((err) => {
            this.boxContent = this.err;
            console.log(err);
          });
      },
      delpolicemenInfo(event, apolicemenNumber) {
        axios.delete("http://localhost:7078/api/delpolicemenInfo", {
            policemenNumber: apolicemenNumber,
          })
          .then((res) => {
            this.policemenInfo = res.data;
            console.log(res.data);
            location.reload();
          })
          .catch((err) => {
            this.boxContent = this.err;
            console.log(err);
          })
      },
    }
  }
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
  .noteTexta {
    font-size: 18px;
    text-align: center;
    width: 10vw;
    display: inline-block;
  }
  input {
    margin-top: 4.5vh;
    margin-bottom: 1vh;
    display: block;
    width: 10vw;
    padding: 0.8em;
    outline: none;
    border: 1px solid #e3e3e3;
    border-radius: 2px;
  }
  .main {
    margin-top: 10vh;
    justify-content: center;
    align-content: center;
    width: 100%;
    height: 120vh;
    min-width: 800px;
  }
  .ssqinputinfobox {
    display: inline-block;
    width: 200px;
    height: 32px;
    margin: 20px 10px;
  }
  .ssqinputtext {
    text-align: center;
    margin-top: 7vh;
    margin-right: 2vw;
    width: 15vw;
    display: inline-block;
  }
  input {
    margin-top: 5vh;
    display: block;
    width: 10vw;
    padding: 0.8em;
    outline: none;
    border: 1px solid #e3e3e3;
    border-radius: 2px;
  }
  .zyhselect {
    margin-top: 5vh;
    display: block;
    width: 10vw;
    padding: 0.8em;
    outline: none;
    border: 1px solid #e3e3e3;
    border-radius: 2px;
    margin-right: 2vw;
    margin-top: 5vh;
  }
  .selectcontainer {
    display: flex;
    margin-right: 5vw;
  }
  .btncontainer {
    margin-top: 5vh;
    display: flex;
    justify-content: center;
  }
  .leftbtn {
    margin-right: 5vw;
  }
  .maintable {
    flex-direction: column;
    align-content: center;
    justify-content: center;
    text-align: center;
    margin: 20px auto;
    width: 75vw;
    height: 75vh;
  }
  table {
    position: relative;
    width: 100%;
    border: 1px solid #ccc;
    text-align: center;
    tbody {
      border-collapse: separate;
      height: 100%;
    }
    td,
    th {
      padding: 5px;
      border: 1px solid #ccc;
    }
  }
  .rolltable {
    top: -1px;
    position: relative;
    width: 100%;
    height: 100%;
    overflow-y: scroll;
    overflow-x: hidden;
    background: linear-gradient(#fff, transparent) top / 100% 100px, radial-gradient(at 50% -15px, rgba(0, 0, 0, 0.8), transparent 70%) top / 100000% 12px;
    background-repeat: no-repeat;
    background-attachment: local, scroll;
  }
  .ssqbutton1 {
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
  .ssqbutton1 span {
    position: relative;
    top: -20%;
  }
  .ssqbutton1::before {
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
  .ssqbutton1:hover::before {
    --size: 400px;
  }
  .ssqbutton1-1 {
    margin: 10px auto;
    width: 150px;
    height: 40px;
    padding: 0 30px;
    line-height: 60px;
    text-align: center;
    position: relative;
    appearance: none;
    background: #0b71bb;
    border: none;
    color: white;
    font-size: 1.2em;
    cursor: pointer;
    outline: none;
    overflow: hidden;
    border-radius: 0px;
  }
  .ssqbutton1-1 span {
    position: relative;
    top: -20%;
  }
  .ssqbutton1-1::before {
    --size: 0;
    content: "";
    position: absolute;
    left: var(--x);
    top: var(--y);
    width: var(--size);
    height: var(--size);
    background: radial-gradient(circle closest-side, #abc9de, transparent);
    transform: translate(-50%, -50%);
    transition: width 0.2s ease, height 0.2s ease;
  }
  .ssqbutton1-1:hover::before {
    --size: 400px;
  }
  .ssqbutton1-2 {
    margin: 10px auto;
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
    border-radius: 0px;
  }
  .ssqbutton1-2 span {
    position: relative;
    top: -20%;
  }
  .ssqbutton1-2::before {
    --size: 0;
    content: "";
    position: absolute;
    left: var(--x);
    top: var(--y);
    width: var(--size);
    height: var(--size);
    background: radial-gradient(circle closest-side, #99c4e5, transparent);
    transform: translate(-50%, -50%);
    transition: width 0.2s ease, height 0.2s ease;
  }
  .ssqbutton1-2:hover::before {
    --size: 400px;
  }
  .inputBox {
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
