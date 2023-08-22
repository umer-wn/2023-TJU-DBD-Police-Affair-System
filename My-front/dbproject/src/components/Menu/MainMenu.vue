<template>
  <el-container class="home-container">
    <!-- 头部 -->
    <el-header
      style="height: 108px; padding: 0; text-align: justify; hyphens: auto"
    >
      <div class="headpic">
        <div class="header1">
          <img class="logo" src="../../assets/image-2.png" />
          <div class="title">&nbsp;&nbsp;警务处理系统</div>

          <!-- <div class="footer1">返回首页</div> -->
        </div>
        <div class="content1" @mousemove="handleMouseMove">
          <div class="headbar">工资补贴管理</div>
        </div>
        <svg
          class="arrow"
          width="20"
          height="22"
          viewBox="0 0 20 22"
          fill="none"
          xmlns="http://www.w3.org/2000/svg"
        >
          <line
            y1="-2.5"
            x2="20.4742"
            y2="-2.5"
            transform="matrix(-0.814031 0.580822 -0.814031 -0.580822 16.6667 0)"
            stroke="white"
            stroke-width="5"
          />
          <line
            y1="-2.5"
            x2="20.4742"
            y2="-2.5"
            transform="matrix(-0.814031 -0.580822 0.814031 -0.580822 20 22)"
            stroke="white"
            stroke-width="5"
          />
        </svg>
      </div>
    </el-header>

    <!-- 页面主体 -->
    <el-container>
      <!-- 导航栏 -->
      <el-aside width="200px">
        <el-menu
          class="el-menu-vertical-demo"
          default-active="2"
          background-color="#1f2cdf"
          text-color="#ffffff"
          active-text-color="#ffd04b"
        >
          <el-sub-menu index="1-4">
            <!-- 警员管理 -->
            <template #title>
              <span>警员管理</span>
            </template>
            <el-menu-item index="1-4-1">
              <router-link to="/mainMenu/Register" class="menu-link"
                >&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;注册&nbsp;&nbsp;&nbsp;</router-link
              >
            </el-menu-item>
            <el-menu-item index="1-4-2">
              <router-link to="/mainMenu/ChangePermission" class="menu-link"
                >警员权限修改</router-link
              >
            </el-menu-item>
            <el-menu-item index="1-4-3">
              <router-link
                to="/mainMenu/PoliceStationInfoManagement"
                class="menu-link"
                >警员信息管理
              </router-link>
            </el-menu-item>
          </el-sub-menu>

          <!-- 案件管理 -->
          <el-sub-menu>
            <template #title>
              <span>案件管理</span>
            </template>
          </el-sub-menu>

          <!-- 装备管理 -->
          <el-sub-menu>
            <template #title>
              <span>装备管理</span>
            </template>
          </el-sub-menu>

          <!-- 城区和居民管理 -->

          <el-sub-menu>
            <template #title>
              <span>城区和居民管理</span>
            </template>
          </el-sub-menu>

          <!-- 系统日志 -->
        </el-menu>
      </el-aside>

      <!-- 页面主体 -->
      <el-main class="background">
        <router-view></router-view>
      </el-main>
    </el-container>
  </el-container>
</template>

<script></script>

<style scoped>
.el-menu-item.is-active {
  background-color: #1c80de !important;
  /* color: red; 设置选中时的字体颜色 */
}

.menu-link {
  text-decoration: none;
  color: #ffffff;
}

.background {
  background-image: url("../../assets/bg.png"); /* 替换为你的背景图路径 */
  background-size: cover; /* 根据需要调整背景图的尺寸适应方式 */
  /* 其他背景相关样式，例如背景颜色、背景重复等，可根据需要添加 */
}
.home-container {
  height: 100%;
}
.el-aside {
  background-color: #1620a9;
  background-image: url("../../assets/sdtest.jpg"); /* 替换为你的背景图路径 */
  background-size: contain;
  background-position: right bottom;
}

.headpic {
  background: #ffffff;
  width: 100%;
  position: relative;
  overflow: hidden;
}

.header1 {
  background: #1f2cdf;
  background-image: url("../../assets/hdtest.jpg"); /* 替换为你的背景图路径 */
  background-size: contain;
  background-position: right top; /* 背景图靠左上角 */
  background-repeat: no-repeat;
  width: 100%;
  height: 70px;
  position: relative;

  left: 0;
  display: flex;
  align-items: center;
  padding-left: 20px;
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
