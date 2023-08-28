<template>
<el-header class="sub-header"  @mousemove="handleMouseMove">
    <div>&nbsp;&nbsp;警员管理&nbsp;>&nbsp;警员信息管理</div>
  </el-header>
  <main class="main">
    <section>
      <div class="inputcontainer">
        <!-- 这里开始输入 -->

        <label style="position: relative; display: block">
          <div class="ssqinputtext">输入警员ID</div>
          <input class="ssqinputinfobox" type="text" v-model="policemenNumber" placeholder="警员ID" />
        </label>

        <label style="position: relative; display: block">
          <div class="ssqinputtext">输入警员姓名</div>
          <input class="ssqinputinfobox" type="text" v-model="policemenName" placeholder="警员姓名" />
        </label>
        <div class="selectcontainer">
          <div class="ssqinputtext">选择工作状态</div>
          <select class="zyhselect" v-model="policemenStatus">
            <option selected value="全部">全部</option>
            <option value="在职">在职</option>
            <option value="离职">离职</option>
          </select>
        </div>
        <div class="selectcontainer">
          <div class="ssqinputtext">选择职务</div>
          <select class="zyhselect" v-model="policemenPosition">
            <option selected value="全部">全部</option>
            <option value="学员">学员</option>
            <option value="警员">警员</option>
            <option value="警司">警司</option>
            <option value="警督">警督</option>
            <option value="警监">警监</option>
            <option value="总警监">总警监</option>
          </select>
        </div>
      </div>
      <!-- 输入结束，下面是按钮 -->
      <div class="btncontainer">
        <div class="leftbtn">
          <button class="ssqbutton1" @click="fetchpolicemenInfo" @mousemove="handleMouseMove">
            <span>查询</span>
          </button>
        </div>
        <div class="rightbtn">
          <button class="ssqbutton1" @click="info" @mousemove="handleMouseMove">
            <span>查看信息</span>
          </button>
        </div>
      </div>
      <!-- 按钮结束，表格显示获取的警员信息 -->
      <el-table v-if="policemenInfo.length > 0" :data="policemenInfo" stripe height="450" @wheel.passive.stop>
        <el-table-column prop="policemenNumber" label="警号" />
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
  </main>
</template>

<script>
import axios from "axios";

export default {
  data() {
    return {
      policemenNumber: "",
      policemenName: "",
      policemenStatus: "全部",
      policemenPosition: "全部",
      policemenInfo: [],
      err: "警员不存在！",
      boxContent:"",
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
          // console.log(res.data);
        })
        .catch((err) => {
          this.boxContent = this.err;
          console.log(err);
        });
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
    align-items: center; /* 文字竖直方向居中对齐 */
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
.main {
margin-top: 10vh;
  justify-content: center;
  align-content: center;
  width: 100%;
  height: 120vh;
  min-width: 800px;
}

.ssqinputinfobox {
  position: relative;
  width: 10vw;
  display: inline-block;
}

.ssqinputtext {
  text-align: center;
  margin-top: 7vh;
  margin-left: 5vw;
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
  background: linear-gradient(#fff, transparent) top / 100% 100px,
    radial-gradient(at 50% -15px, rgba(0, 0, 0, 0.8), transparent 70%) top / 100000% 12px;
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
</style>
