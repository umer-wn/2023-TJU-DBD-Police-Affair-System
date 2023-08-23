<template>
  <div class="body">
    <div class="headpic">
      <div class="header1">
        <img class="logo" src="@/assets/police-logo.png" />
        <div class="title">&nbsp;&nbsp;警务处理系统</div>
      </div>
    </div>
    <p class="welcome-text">
      &nbsp;欢&nbsp;迎&nbsp;进&nbsp;入&nbsp;警&nbsp;务&nbsp;处&nbsp;理&nbsp;系&nbsp;统&nbsp;
    </p>
    <button class="loginbutton" @click="login">登&nbsp;&nbsp;&nbsp;录</button>
  </div>
</template>

<script>
export default {
  methods: {
    login() {
      this.$router.push("/mainMenu");
    },
  },
};
</script>

<style scoped>
.body {
  overflow: auto;
  flex-direction: column;
  display: flex;
  height: 800px;
  align-items: center;
  background-image: url("./assets/hellopolice.jpg");
  background-attachment: fixed;
  background-position: center;
  background-repeat: no-repeat;
  background-size: cover;
  height: 100vh;
}

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

.welcome-text {
  background-color: #1890ff;
  color: #ffffff;
  margin-top: 150px;
  text-align: center;
  font-size: 100px; /* 字号大小 */
  font-weight: bold; /* 字体加粗 */
  border-radius: 5px;
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
