<template>
  <el-container class="home-container">
    <!-- 页面主体 -->
    <el-container class="maindiv">
      <el-main class="background">
        <div style="width: 1000px; padding-left: 200px">
          <!-- 顶部两个按钮 -->
          <div style="width: 100%">
            <button
              class="ssqbutton1-1"
              @mousemove="handleMouseMove"
              style="width: 49%; display: inline-block"
            >
              <span>记录搜索</span>
            </button>
            <button
              class="ssqbutton1-2"
              @mousemove="handleMouseMove"
              style="width: 49%; display: inline-block"
              @click="goToDataStatistics"
            >
              <span>数据统计</span>
            </button>
          </div>

          <!-- 筛选部分，页面中间的大div -->
          <div
            style="
              background-color: rgba(255, 255, 255, 0.733);
              box-shadow: 0px 0px 10px 2px rgba(123, 103, 75, 0.427);
            "
          >
            <div>&nbsp;</div>
            <label style="position: relative; display: block; margin-left: 25%"
              ><div class="ssqinputtext">输入警号</div>
              <input
                class="ssqinputinfobox"
                type="text"
                v-model="inputId"
                placeholder="输入警号"
              /><span class="ssqpoptip">仅支持字母、数字组合!</span></label
            >
            <div>&nbsp;</div>
            <label style="position: relative; display: block; margin-left: 25%"
              ><div class="ssqinputtext">输入姓名</div>
              <input
                class="ssqinputinfobox"
                type="text"
                v-model="inputName"
                placeholder="输入姓名"
            /></label>
            <div>&nbsp;</div>
            <label style="position: relative; display: block; margin-left: 25%"
              ><div class="ssqinputtext">选择时间</div>
              <input
                class="ssqinputinfobox"
                type="text"
                v-model="inputTime"
                placeholder="选择时间"
            /></label>
            <div>&nbsp;</div>
            <label style="position: relative; display: block; margin-left: 25%"
              ><div class="ssqinputtext">选择警局</div>
              <input
                class="ssqinputinfobox"
                type="text"
                v-model="inputPoliceStation"
                placeholder="选择警局"
            /></label>

            <div>
              <button
                class="ssqbutton1"
                style="margin-left: 25%"
                @click="submitSearch"
                @mousemove="handleMouseMove"
              >
                <span>筛选</span>
              </button>
            </div>
            <div>
              <button
                class="ssqbutton1"
                style="margin-left: 25%"
                @click="goToNewRecord"
                @mousemove="handleMouseMove"
              >
                <span>新建记录</span>
              </button>
            </div>
            <div class="g-table">
              <table style="width: 100%">
                <thead>
                  <tr>
                    <th>编号</th>
                    <th>姓名</th>
                    <th>地址</th>
                    <th>查看</th>
                  </tr>
                </thead>
              </table>
              <div class="g-scroll" @wheel.passive.stop>
                <table style="width: 100%">
                  <tbody>
                    <tr v-for="rrr in 50" :key="rrr">
                      <td>123456</td>
                      <td>kusoyaro</td>
                      <td>aaaaaa</td>
                      <td>
                        <button @click="goToDetailPage(rrr)">查看</button>
                      </td>
                    </tr>
                  </tbody>
                </table>
              </div>
            </div>
          </div>
        </div>
      </el-main>
    </el-container>
  </el-container>
</template>

<script>
import axios from "axios";
export default {
  data() {
    return {
      inputId: "",
      inputName: "",
      inputTime: "",
      inputPoliceStation: "",
      boxContent: "",
    };
  },

  methods: {
    submitSearch() {
      console.log(this.inputId, this.inputRelated, this.inputAmount);
      axios
        .post("http://localhost:7078/api/query", {
          inputId: this.inputId,
          inputRelated: this.inputRelated,
          inputAmount: this.inputAmount,
        })
        .then((response) => {
          // 请求成功的处理逻辑
          this.boxContent = response.data.ssqinputtext2;
          console.log(response.data);
        })
        .catch((error) => {
          // 请求失败的处理逻辑
          console.error(error);
        });
    },
    handleMouseMove(event) {
      const x = event.pageX - event.target.offsetLeft;
      const y = event.pageY - event.target.offsetTop;

      event.target.style.setProperty("--x", `${x}px`);
      event.target.style.setProperty("--y", `${y}px`);
    },
    goToNewRecord() {
      this.$router.push("/newRecord");
    },
    goToDetailPage(id) {
      this.$router.push({ name: "detailPage", params: { id: id } });
    },
    goToDataStatistics() {
      this.$router.push("/dataStatistics");
    },
  },
};
</script>

<style lang="postcss" scoped>
.el-menu-item.is-active {
  background-color: #fff !important;
}

.home-container {
  overflow: auto;
  height: 160vh;
}
.maindiv {
  display: flex;
  background-color: red;
  min-width: 500px;
}
.title {
  color: #ffffff;
  text-align: left;
  font: 400 36px "Inter", sans-serif;
  display: inline-block;
}

.content1 {
  width: 100%;
  height: 38px;
  position: relative;
  overflow: hidden;
  display: flex;
  align-items: center;
  padding-left: 20px;
  background: #1f2cdf;
  box-shadow: inset -500px 0px 200px 0px rgba(4, 0, 113, 0.856);
}

.subtitle {
  color: #ffffff;
  text-align: left;
  display: relative;
  left: 80px;
  font: 400 28px "Inter", sans-serif;
}

.footer1 {
  color: #ffffff;
  text-align: left;
  font: 400 16px "Inter", sans-serif;
  position: absolute;
  top: 42px;
  right: 20px;
}

.arrow {
  position: absolute;
  top: 77px;
  left: 16px;
  overflow: visible;
  animation: stroke 0.5s infinite alternate;
  padding-left: 190px;
}
.arrow:hover {
  stroke: $pink; /* 设置悬停时的颜色，这里以红色为例 */
}

.logo {
  width: 70px;
  height: 70px;
  position: relative;
  top: 2px;
  left: 0px;
}
.headbar {
  color: #ffffff;
  text-align: left;
  display: relative;
  left: 80px;
  width: 100%;
  font: 400 28px "Inter", sans-serif;
  padding-left: 250px;
}

.headbar span {
  position: relative;
}

.headbar::before {
  --size: 0;
  content: "";
  position: absolute;
  left: var(--x);
  top: var(--y);
  width: var(--size);
  height: var(--size);
  background: radial-gradient(circle closest-side, #e1f2ff, transparent);
  transform: translate(-50%, -50%);
  transition: width 0.2s ease, height 0.2s ease;
}

.headbar:hover::before {
  --size: 400px;
}

main {
  width: 100vw;
  min-width: 800px;
}
h2.title {
  color: white;
  margin-top: 1em;
  margin-bottom: 1em;
}
.titletext1 {
  ssqinputinfoboxwidth: 100px;
}
.titletext2 {
  width: 100px;
  background: #b7b3b3;
}
.box {
  width: 700px;
  height: 200px;
  border: 1px solid #000;
  padding: 10px;
}
.ssqinputinfobox {
  position: relative;
  width: 160px;
  display: inline-block;
}
.ssqinputtext {
  width: 100px;
  display: inline-block;
}

input {
  display: block;
  width: 229px;
  padding: 0.8em;
  outline: none;
  border: 1px solid #e3e3e3;
  border-radius: 2px;
}
input:focus,
input:hover {
  border-color: #b4a078;
}
input:not(:placeholder-shown) {
  border-color: #be4141;
  box-shadow: 0 0 0 2px rgba(255, 100, 97, 0.2);
}
input:not(:placeholder-shown) + .ssqpoptip {
  color: #be4141;
}
input:valid {
  border-color: #b4a078;
  box-shadow: 0 0 0 2px rgba(180, 160, 120, 0.2);
}
input:valid + .ssqpoptip {
  color: unset;
}
input:not(:focus) + .ssqpoptip {
  transform: scale(0);
  animation: elastic-dec 0.25s;
}

input:focus + .ssqpoptip {
  transform: scale(1);
  animation: elastic-grow 0.45s;
}
.ssqpoptip {
  display: inline-block;
  width: 200px;
  font-size: 13px;
  padding: 0.6em;
  background: #fafafa;
  position: relative;
  margin-left: -3px;
  margin-top: 3px;
  border-radius: 2px;
  filter: drop-shadow(0 0 1px rgba(0, 0, 0, 0.23456));
  transform-origin: 15px -6px;
}
.ssqpoptip::before {
  content: "";
  position: absolute;
  top: 50%;
  left: -10px;
  border: 9px solid transparent;
  border-bottom-color: #fafafa;
  border-top-width: 0;
  padding: 3px;
}
@keyframes elastic-grow {
  from {
    transform: scale(0);
  }
  70% {
    transform: scale(1.1);
    animation-timing-function: cubic-bezier(0.1, 0.25, 0.1, 0.25);
  }
}
@keyframes elastic-dec {
  from {
    transform: scale(1);
  }
  to {
    transform: scale(0);
    animation-timing-function: cubic-bezier(0.25, 0.1, 0.25, 0.1);
  }
}

* {
  box-sizing: border-box;
}

.g-table {
  margin: 20px auto;
  width: 600px;
  height: 400px;
}

table {
  position: relative;
  width: 450px;
  border: 1px solid #ccc;
  text-align: center;

  tbody {
    border-collapse: separate;
    height: 200px;
  }

  td,
  th {
    width: 150px;
    padding: 5px;
    border: 1px solid #ccc;
  }
}
.sec {
  min-width: 800px;
}
.g-scroll {
  top: -1px;
  position: relative;
  max-height: 300px;
  width: 100%;
  overflow-y: scroll;
  overflow-x: hidden;
  background: linear-gradient(#fff, transparent) top / 100% 100px,
    radial-gradient(at 50% -15px, rgba(0, 0, 0, 0.8), transparent 70%) top /
      100000% 12px;
  background-repeat: no-repeat;
  background-attachment: local, scroll;
}

.ssqbutton1 {
  margin: 10px auto;
  width: 150px;
  height: 40px;
  padding: 0 30px;
  line-height: 60px;
  text-align: center;
  position: relative;
  appearance: none;
  background: #d7caaa;
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
  background: radial-gradient(circle closest-side, #7286f9, transparent);
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
  background: #1f2cdf;
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
  background: #d7caaa;
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
  background: radial-gradient(circle closest-side, #7286f9, transparent);
  transform: translate(-50%, -50%);
  transition: width 0.2s ease, height 0.2s ease;
}

.ssqbutton1-2:hover::before {
  --size: 400px;
}

/* 消除小三角 */
.el-popper[x-placement^="bottom"] .popper__arrow {
  border: none;
}
.el-popper[x-placement^="bottom"] .popper__arrow::after {
  border: none;
}

.el-dropdown-link:hover {
  transform: scale(
    1.2,
    1.2
  ); /*实现头像的放大处理，x和y都放大1.2就相当于整体放大*/
  animation: elhd-grow 0.25s;
}
.el-dropdown-link:not(:hover) {
  animation: elhd-dec 0.25s;
}
@keyframes elhd-grow {
  from {
    transform: scale(1);
  }
  70% {
    transform: scale(1.2);
    animation-timing-function: cubic-bezier(0.1, 0.25, 0.1, 0.25);
  }
}
@keyframes elhd-dec {
  from {
    transform: scale(1.2);
  }
  to {
    transform: scale(1);
    animation-timing-function: cubic-bezier(0.25, 0.1, 0.25, 0.1);
  }
}
</style>
