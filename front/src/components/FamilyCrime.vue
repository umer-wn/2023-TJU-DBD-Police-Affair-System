<template>
  <div class="container">
    <!-- main window -->
    <!-- element:query inputbox and confirm button-->
    <div class="queryBox" :class="{ upBox: isGraphContainerVisible }">
      <input
        type="text"
        v-model="inputID"
        :placeholder="placeholder"
        @focus="changeInputStatus"
        @blur="changeInputStatus"
        @keydown.enter="query"
        class="inputBox"
      />
      <button @click="query" class="button">查询</button>
    </div>
    <!-- element:family graph container -->
    <div v-if="readyToRenderGraph" class="graphContainer">
      <!-- render only when content isn't null -->
      <!-- highest floor, parents , render secondly-->
      <div
        class="level"
        :style="{ visibility: curLevel >= 1 ? 'visible' : 'hidden' }"
      >
        <transition name="imgAnime">
          <criminalInfo
            v-if="curLevel >= 1"
            :imgUrl="imageUrl"
            :content="content.relatePeople[0]"
            class="imgCtrl"
          />
        </transition>
        <transition name="imgAnime">
          <criminalInfo
            v-if="curLevel >= 1"
            :imgUrl="imageUrl"
            :content="content.relatePeople[1]"
            class="imgCtrl"
          />
        </transition>
      </div>
      <!-- middle floor, center person, render firstly -->
      <div
        class="level"
        :style="{ visibility: curLevel >= 0 ? 'visible' : 'hidden' }"
      >
        <transition name="imgAnime">
          <criminalInfo
            v-if="curLevel >= 0"
            :imgUrl="imageUrl"
            :content="content.centerMan"
            class="imgCtrl"
          />
        </transition>
      </div>
      <!-- bottom floor, children, render at last -->
      <div
        class="level"
        :style="{ visibility: curLevel >= 2 ? 'visible' : 'hidden' }"
      >
        <criminalInfo
          v-if="curLevel >= 2"
          :imgUrl="imageUrl"
          :content="content.relatePeople[3]"
          class="imgCtrl"
        />
      </div>
      <svg class="line">
        <line :x1="element1X" :y1="element1Y" :x2="element2X" :y2="element2Y"></line>
      </svg>
    </div>
  </div>
</template>

<script>
import CriminalInfo from "./CriminalInfo.vue";
import axios from "axios";
export default {
  components: {
    CriminalInfo,
  },
  data() {
    return {
      inputID: "",
      content: [],
      curLevel: -1,
      imageUrl: require("@/assets/criminal.png"),
      placeholder: "请输入居民身份证号",
      isGraphContainerVisible: false,
      readyToRenderGraph: false,
      element1X: 0,
    element1Y: 0,
    element2X: 100,
    element2Y: 100
    };
  },
  watch: {
    content: {
      handler() {
        setTimeout(() => {
          this.readyToRenderGraph = true;
          setInterval(() => {
            if (this.curLevel < 2) this.curLevel++;
          }, 500);
        }, 1000);
      },
    },
  },
  methods: {
    query() {
      this.curLevel = -1;
      axios
        .post("http://localhost:7078/api/queryFamily", {
          InputText: this.inputID,
        })
        .then((response) => {
          this.content = [];
          this.content = response.data;
          console.log(response.data);

          if (
            this.content.length !== 0 &&
            this.content.centerMan.name.length !== 0
          ) {
            // 有数据
            this.isGraphContainerVisible = true;
          }
        })
        .catch((error) => {
          console.error(error);
        });
    },
    changeInputStatus() {
      if (this.placeholder === "") {
        this.placeholder = "请输入居民身份证号";
      } else {
        this.placeholder = "";
      }
    },
  },
  mounted() {},
};
</script>

<style scoped>
.line{
  position: absolute;
  top: 0;
  left: 0;
  width: 100%;
  height: 100%;
}
.queryBox {
  width: 100%;
  height: 100px;
  position: relative;
  top: 40vh;
  overflow: hidden;
  display: flex;
  justify-content: center;
  align-items: center;
  transition: transform 0.5s ease;
  border: #00aeff 1px solid;
}

.upBox {
  transform: translateY(-40vh);
}
.container {
  background: #ffffff;
  width: 100%;
  height: 100vh;
  position: relative;
  overflow: hidden;

  display: flex;
  flex-direction: column;
  justify-content: flex-start;
  align-items: center;

  border: #00aeff 1px solid;
}
.inputBox {
  width: 250px;
  height: 50px;
  text-align: center;
  border-radius: 16px;
  font-size: 16px;
  background: #ffffff;
  padding: 3px;
}
.inputBox:focus {
  outline: none;
  border: 3px rgba(25, 167, 255, 0.644) solid !important;
  box-shadow: 0 0 0px rgba(25, 167, 255, 0.644);
}
.inputBox:not(:focus) {
  outline: none;
  border: 1px rgba(158, 158, 158, 0.943) solid;
  box-shadow: 0 0 0px rgba(158, 158, 158, 0.943);
}
.inputBox:hover {
  outline: none;
  border: 1px rgba(158, 158, 158, 0.943) solid;
  box-shadow: 0 0 5px rgba(214, 214, 214, 0.943);
}
.button {
  width: 70px;
  height: 55px;
  text-align: center;
  margin-left: 9px;
  border-radius: 16px;
  font-size: 16px;
  color: #ffffff;
  background: #00aeff;
}
.imgCtrl {
  width: 150px;
  height: 150px;
  text-align: center;
  display: flex;
  justify-content: center;
  flex: 1;
}
.graphContainer {
  width: 100%;
  /* 高度：填满剩余屏幕 */
  height: calc(100% - 100px);
  position: relative;
  top: 9px;
  overflow: hidden;

  display: flex;
  justify-content: center;
  flex-direction: column;
  align-items: center;
  border: #00aeff 1px solid;
}
.level {
  width: 100vw;
  height: 200px;
  position: relative;
  overflow: hidden;

  display: flex;
  justify-content: center;
  align-items: center;
}

.imgAnime-enter-active {
  transition: opacity 0.5s ease, transform 0.5s ease, width 0.5s ease,
    height 0.5s ease; /* 添加宽度和高度的过渡效果 */
}
.imgAnime-leave-active {
  transition: opacity 0.5s ease, transform 0.5s ease, width 0.5s ease,
    height 0.5s ease; /* 添加宽度和高度的过渡效果 */
}
.imgAnime-enter {
  opacity: 0;
  transform: translateY(100%) scale(0); /* 添加初始的缩放效果 */
}
.imgAnime-leave-to {
  opacity: 0;
  transform: translateY(100%) scale(0); /* 添加最终的缩放效果 */
}
</style>
