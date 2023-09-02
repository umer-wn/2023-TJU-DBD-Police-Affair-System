<template>
  <el-header class="sub-header" @mousemove="handleMouseMove">
    <div>&nbsp;&nbsp;警员管理&nbsp;>&nbsp;执法录像管理</div>
  </el-header>
  <el-tabs v-model="anv" type="border-card" style="margin-top: 10vh;">
    <el-tab-pane label="查询录像" name="1">
      <div style="text-align: center;">
        <section>
          <div>
            <table style="display: inline-block; text-align: left;">
              <tr>
                <td>
                  <el-text class="noteText" type="primary">录像ID:</el-text>
                  <el-input class="ssqinputinfobox" type="text" v-model="videoID" placeholder="录像ID" clearable maxlength="5"  oninput="value=value.replace(/[^\d.]/g,'')" show-word-limit />
                </td>
                <td>
                  <el-text class="noteText" type="primary">警员警号:</el-text>
                  <el-input class="ssqinputinfobox" type="text" v-model="principleID" placeholder="涉及警员的警号" clearable maxlength="7"  oninput="value=value.replace(/[^\d.]/g,'')" show-word-limit />
                </td>
              </tr>
              <div>
                <el-text class="noteText" type="primary">录像类别:</el-text>
                <el-select class="ssqinputinfobox" v-model="videoType">
                  <el-option value="全部">全部录像</el-option>
                  <el-option value="审讯">审讯</el-option>
                  <el-option value="监控">监控</el-option>
                  <el-option value="调查">调查</el-option>
                </el-select>
              </div>
            </table>
            <div class="seaButton">
              <el-button type="primary" @click="fetchvideoInfo">
                <span>查询</span>
              </el-button>
            </div>
          </div>
          <!-- 表格显示获取的录像信息 -->
          <el-table :data="videoInfo" v-if="true" stripe height="450" @wheel.passive.stop>
            <el-table-column prop="videoID" label="录像编号" sortable />
            <el-table-column prop="recordTime" label="记录时间" sortable />
            <el-table-column prop="uploadTime" label="上传时间" sortable />
            <el-table-column prop="videoType" label="录像类型" sortable />
            <el-table-column prop="principleID" label="涉及警员的警号" sortable />
            <el-table-column align="right" width="100px"></el-table-column>
          </el-table>
          <!-- 错误提示 -->
          <div v-else>
            {{ boxContent }}
          </div>
        </section>
      </div>
    </el-tab-pane>
    <el-tab-pane label="删除录像" name="2">
      <div style="text-align: center;">
        <table style="display: inline-block;text-align: left;">
          <section>
            <div class="inputcontainer">
              <label style="position: relative; display: block">
                                      <el-text class="noteText" type="primary">输入录像ID</el-text>
                                      <el-input class="ssqinputinfobox" type="text" v-model="videoID" placeholder="录像ID" clearable maxlength="5"
                                      oninput="value=value.replace(/[^\d.]/g,'')" show-word-limit />
                                      </label>
            </div>
            <div class="seaButton">
              <el-button type="primary" @click="delvideoInfo($event, videoID)" @mousemove="handleMouseMove">
                <span>删除</span>
              </el-button>
            </div>
          </section>
        </table>
      </div>
    </el-tab-pane>
    <el-tab-pane label="增加录像" name="3">
      <div style="text-align: center;">
        <table style="display: inline-block;text-align: left;">
          <section>
            <table style="display: inline-block; text-align: left;">
              <tr>
                <td>
                  <el-text class="noteText" type="primary">录像ID:</el-text>
                  <el-input class="ssqinputinfobox" type="text" v-model="videoID" placeholder="录像ID" clearable maxlength="5"  oninput="value=value.replace(/[^\d.]/g,'')" show-word-limit />
                </td>
                <td>
                  <el-text class="noteText" type="primary">警员警号:</el-text>
                  <el-input class="ssqinputinfobox" type="text" v-model="principleID" placeholder="涉及警员的警号" clearable maxlength="7"  oninput="value=value.replace(/[^\d.]/g,'')" show-word-limit />
                </td>
              </tr>
              <div>
                <el-text class="noteText" type="primary">录像类别:</el-text>
                <el-select class="ssqinputinfobox" v-model="videoType">
                  <el-option value="全部">全部录像</el-option>
                  <el-option value="审讯">审讯</el-option>
                  <el-option value="监控">监控</el-option>
                  <el-option value="调查">调查</el-option>
                </el-select>
              </div>
            </table>
            <div class="seaButton">
              <el-button type="primary" @click="addvideoInfo" @mousemove="handleMouseMove">
                <span>增加</span>
              </el-button>
            </div>
          </section>
        </table>
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
        videoID: "",
        videoType: "全部",
        principleID: "",
        videoInfo: [],
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
      delvideoInfo(event, avideoID) {
        axios
          .post('http://localhost:7078/api/delvideoInfo', {
            videoID: avideoID,
          })
          .then((res) => {
            this.videoInfo = res.data;
            console.log(res.data);
            location.reload();
          })
          .catch((err) => {
            this.boxContent = this.err;
            console.log(err);
          });
      },
      addvideoInfo(event, avideoID) {
        axios
          .post('http://localhost:7078/api/addvideoInfo', {
            videoID: this.videoID,
            videoType: this.videoType,
            principleID: this.principleID,
          })
          .then((res) => {
            this.videoInfo = res.data;
            console.log(res.data);
            location.reload();
          })
          .catch((err) => {
            this.boxContent = this.err;
            console.log(err);
          });
      }
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
    position: left;
    display: inline-block;
    width: 200px;
    height: 32px;
    margin: 20px 10px;
  }
  .ssqinputtext {
    text-align: center;
    margin-top: 7vh;
    margin-left: 5vw;
    margin-right: 2vw;
    width: auto;
    display: inline-block;
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
    margin-bottom: 3.5vh;
    width: 10vw;
    display: inline-block;
  }
  input {
    margin-top: 5vh;
    margin-right: 8vh;
    text-align: left;
    display: block;
    width: 10vw;
    padding: 0.8em;
    outline: none;
    border: 1px solid #e3e3e3;
    border-radius: 2px;
  }
  .seaButton {
    display: block;
    text-align: center;
    margin-top: 15px;
    margin-bottom: 10px;
  }
</style>
