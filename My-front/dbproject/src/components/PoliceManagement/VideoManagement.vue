<template>
<el-header class="sub-header"  @mousemove="handleMouseMove">
    <div>&nbsp;&nbsp;警员管理&nbsp;>&nbsp;执法录像管理</div>
  </el-header>
  <main class="main">
    <section>
      <div class="inputcontainer">
        <label style="position: relative; display: block">
          <div class="ssqinputtext">输入录像ID</div>
          <input class="ssqinputinfobox" type="text" v-model="videoID" placeholder="录像ID" />
        </label>

        <label style="position: relative; display: block">
          <div class="ssqinputtext">输入涉及警员的警号</div>
          <input class="ssqinputinfobox" type="text" v-model="principleID" placeholder="涉及警员的警号" />
        </label>

        <div class="ssqinputtext">选择录像类别</div>
        <select class="zyhselect" v-model="videoType">
          <option selected value="全部">全部录像</option>
          <option value="审讯">审讯</option>
          <option value="监控">监控</option>
          <option value="调查">调查</option>
        </select>
      </div>
      <div class="btncontainer">
        <button class="ssqbutton1" @click="fetchvideoInfo" @mousemove="handleMouseMove">
          <span>查询</span>
        </button>
      </div>

      <!-- 表格显示获取的警员信息 -->
      <el-table v-if="videoInfo.length > 0" :data="videoInfo" stripe height="450" @wheel.passive.stop>
        <el-table-column prop="videoID" label="录像编号" />
        <el-table-column prop="recordTime" label="记录时间" />
        <el-table-column prop="uploadTime" label="上传时间" />
        <el-table-column prop="videoType" label="录像类型" />
        <el-table-column prop="principleID" label="涉及警员的警号" />
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
      videoID: "",
      videoType: "全部",
      principleID: "",
      videoInfo: [],
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
    fetchvideoInfo() {
      axios
        .post("http://localhost:7078/api/videoInfo", {
          videoID: this.videoID,
          videoType: this.videoType,
          principleID: this.principleID,
        })
        .then((res) => {
          this.videoInfo = res.data;
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
  display: flex;
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
  width: auto;
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
  margin-left: 5vw;
  margin-right: 2vw;
  margin-top: 5vh;
  margin-bottom: 8vh;
}

.inputcontainer,
.selectcontainer {
  display: flex;
  align-content: center;
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
