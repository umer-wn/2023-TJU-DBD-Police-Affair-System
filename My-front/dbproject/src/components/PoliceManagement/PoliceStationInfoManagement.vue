<template>
<el-header class="sub-header"  @mousemove="handleMouseMove">
    <div>&nbsp;&nbsp;警员管理&nbsp;>&nbsp;警局信息管理</div>
  </el-header>
  <main class="main">
    <section>
      <div class="inputcontainer">
        <label style="position: relative; display: block">
          <div class="ssqinputtext">输入警局ID</div>
          <input class="ssqinputinfobox" type="text" v-model="stationID" placeholder="警局ID" />
        </label>
        <label style="position: relative; display: block">
          <div class="ssqinputtext">输入警局名称</div>
          <input class="ssqinputinfobox" type="text" v-model="stationName" placeholder="警局名称" />
        </label>
        <label style="position: relative; display: block">
          <div class="ssqinputtext">输入警局城市</div>
          <input class="ssqinputinfobox" type="text" v-model="city" placeholder="警局城市" />
        </label>
        <label style="position: relative; display: block">
          <div class="ssqinputtext">输入警局地址</div>
          <input class="ssqinputinfobox" type="text" v-model="address" placeholder="警局地址" />
        </label>
        <label style="position: relative; display: block">
          <div class="ssqinputtext">输入警局预算</div>
          <input class="ssqinputinfobox" input type="number" v-model="budget" placeholder="警局预算" />
        </label>
        <div class="btncontainer">
          <div class="leftbtn">
            <button class="ssqbutton1" @click="fetchStationInfo" @mousemove="handleMouseMove">
              <span>查询</span>
            </button>
          </div>
          <div class="rightbtn">
            <button class="ssqbutton1" @click="goToNewRecord" @mousemove="handleMouseMove">
              <span>新建记录</span>
            </button>
          </div>
        </div>
      </div>
      <div class="table">
        <!-- 表格显示获取的警局信息 -->
        <el-table v-if="stationInfo.length > 0" :data="stationInfo" stripe height="450" @wheel.passive.stop>
          <el-table-column prop="stationID" label="警局编号" />
          <el-table-column prop="stationName" label="警局名称" />
          <el-table-column prop="city" label="所在城市" />
          <el-table-column prop="address" label="警局地址" />
          <el-table-column prop="budget" label="警局预算" />
        </el-table>
        <!-- 错误提示 -->
        <div v-else>{{ boxContent }}</div>
      </div>


    </section>
  </main>
</template>

<script>
import axios from "axios";

export default {
  data() {
    return {
      stationID: "",
      stationName: "",
      city: "",
      address: "",
      budget: null, // 修改为null，确保初始值为null
      stationInfo: [],
      err: "警局不存在！",
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
    fetchStationInfo() {
      axios
        .post("http://localhost:7078/api/stationInfo", {
          stationID: this.stationID,
          stationName: this.stationName,
          city: this.city,
          address: this.address,
          budget: this.budget === "" ? null : this.budget,
        })
        .then((res) => {
          this.stationInfo = res.data;
          console.log(res.data);
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
  width: 10vw;
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

.btncontainer {
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
  margin-top: 5vh;
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

.table {
  display: flex;
  justify-content: center;
}
</style>
