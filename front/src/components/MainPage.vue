<template>
  <div class="main-container">
    <div class="headpic">
      <div class="header1">
        <img class="logo" src="@/assets/police-logo.png" />
        <div class="title">&nbsp;&nbsp;警务处理系统</div>
      </div>
    </div>
    <p class="welcome-text">欢迎进入警务处理系统</p>
    <button class="loginbutton" @click="login">登&nbsp;&nbsp;&nbsp;录</button>
  </div>
</template>

<script>
export default {
  methods: {
    login() {
      this.$router.push("/login");
    }
  }
};
</script>

<style scope>
.headpic {
  background: #fff;
  width: 100%;
  position: relative;
  overflow: hidden;
}

.header1 {
  background: #0b71bb;
  width: 100%;
  height: 70px;
  position: relative;
  left: 0;
  display: flex;
  align-items: center;
  padding-left: 20px;
}

.logo {
  width: 70px;
  height: 70px;
  position: relative;
  top: 2px;
  left: 0px;
}

.title {
  color: #ffffff;
  text-align: left;
  font: 400 36px "Inter", sans-serif;
  display: inline-block;
}

.main-container {
  overflow: auto;
  flex-direction: column;
  display: flex;
  background-image: url("../assets/hello-bg.jpg");
  background-size: cover;
  height: 800px;
  align-items: center;
}

.welcome-text {
  margin-top: 150px;
  text-align: center;
  font-size: 100px; /* 字号大小 */
  font-weight: bold; /* 字体加粗 */
}

.loginbutton {
  margin-top: 20px;
  background-color: #1890ff;
  padding: 10px 20px;
  color: #fff;
  border: none;
  border-radius: 5px;
  cursor: pointer;
  font-size: 30px;
  font-weight: bold;
}
</style>
