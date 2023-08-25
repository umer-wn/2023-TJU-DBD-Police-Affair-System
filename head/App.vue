<!-- 页面头图，请将image-2加到assets文件夹中 -->
<template>
  <div class="container">
    <div class="header">
      <div class="title">警务处理系统</div>
    </div>

    <div class="content">
      <div class="subtitle">警局信息</div>
    </div>
    <!-- 此处为返回首页按钮，还没加链接 -->
    <div class="footer">返回首页</div>
    <!-- 此处为表示退回上一级的箭头，还没加链接 -->
    <svg class="arrow" width="20" height="22" viewBox="0 0 20 22" fill="none" xmlns="http://www.w3.org/2000/svg">
      <line y1="-2.5" x2="20.4742" y2="-2.5" transform="matrix(-0.814031 0.580822 -0.814031 -0.580822 16.6667 0)" stroke="white" stroke-width="5" />
      <line y1="-2.5" x2="20.4742" y2="-2.5" transform="matrix(-0.814031 -0.580822 0.814031 -0.580822 20 22)" stroke="white" stroke-width="5" />
    </svg>

    <img class="logo" src="./assets/image-2.png" />
  </div>
</template>

<style scoped>
.container {
  background: #ffffff;
  width: calc(100vw - 20px);
  height: 600px;
  position: relative;
  overflow: hidden;
}

.header {
  background: #0b71bb;
  width: calc(100vw - 20px);
  height: 70px;
  position: absolute;
  top: 0;
  left: 0;
  display: flex;
  align-items: center;
  padding-left: 75px;
}

.title {
  color: #ffffff;
  text-align: left;
  font: 400 36px "Inter", sans-serif;
}

.content {
  width: calc(100vw - 20px);
  height: 38px;
  position: absolute;
  top: 70px;
  left: 0;
  display: flex;
  align-items: center;
  padding-left: 75px;
  background: #b7b3b3;
  box-shadow: inset 200px 0px 50px 0px rgba(0, 0, 0, 0.25);
}

.subtitle {
  color: #ffffff;
  text-align: left;
  font: 400 28px "Inter", sans-serif;
}

.footer {
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
}

.logo {
  width: 70px;
  height: 70px;
  position: absolute;
  top: 0px;
  left: 0px;
}
</style>