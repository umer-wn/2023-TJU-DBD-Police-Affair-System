<template>
  <div class="body">
    <div class="headpic">
      <img class="logo" src="../../assets/image-2.png" />
      <div class="title">&nbsp;&nbsp;警务处理系统</div>
    </div>

    <div class="change-password-page">
      <div class="change-password-container">
        <p class="forget-text">忘记密码</p>
        <p class="setnew-text">设置新密码</p>
        <form @submit.prevent="submitForm">
          <div class="input-container">
            <input
              id="phoneNumber"
              type="text"
              v-model="phoneNumber"
              autocomplete="off"
              @focus="handleFocus1"
              @blur="handleBlur1"
              required
            />
            <div class="Tip" :class="{ TipA: isFocused1 }">手机号:</div>
          </div>
          <div class="input-container">
            <input
              id="code-input"
              type="text"
              v-model="verificationCode"
              autocomplete="off"
              @focus="handleFocus2"
              @blur="handleBlur2"
              required
            />
            <div class="Tip" :class="{ TipA: isFocused2 }">手机验证码:</div>
          </div>
          <div>
            <button
              type="button"
              @click="sendVerificationCode"
              :disabled="isSendingCode"
            >
              {{ isSendingCode ? "发送中..." : "发送验证码" }}
            </button>
          </div>
          <div class="input-container">
            <input
              id="newPassword"
              type="password"
              v-model="newPassword"
              autocomplete="off"
              @focus="handleFocus3"
              @blur="handleBlur3"
              required
            />
            <div class="Tip" :class="{ TipA: isFocused3 }">新密码:</div>
          </div>
          <div class="input-container">
            <input
              id="confirmPassword"
              type="password"
              v-model="confirmPassword"
              autocomplete="off"
              @focus="handleFocus4"
              @blur="handleBlur4"
              required
            />
            <div class="Tip" :class="{ TipA: isFocused4 }">确认密码:</div>
          </div>
          <button type="submit" class="confirm-button">确认</button>
        </form>
      </div>
    </div>
  </div>
</template>

<script>
export default {
  data() {
    return {
      phoneNumber: "",
      verificationCode: "",
      newPassword: "",
      confirmPassword: "",
      isSendingCode: false,
      isFocused1: false,
      isFocused2: false,
      isFocused3: false,
      isFocused4: false,
    };
  },
  methods: {
    handleFocus1() {
      this.isFocused1 = true;
      console.log("点击了");
    },
    handleBlur1() {
      let val = document.getElementById("phoneNumber").value;
      if (!val) {
        this.isFocused1 = false;
      }
    },
    handleFocus2() {
      this.isFocused2 = true;
      console.log("点击了");
    },
    handleBlur2() {
      let val = document.getElementById("code-input").value;
      if (!val) {
        this.isFocused2 = false;
      }
    },
    handleFocus3() {
      this.isFocused3 = true;
      console.log("点击了");
    },
    handleBlur3() {
      let val = document.getElementById("newPassword").value;
      if (!val) {
        this.isFocused3 = false;
      }
    },
    handleFocus4() {
      this.isFocused4 = true;
      console.log("点击了");
    },
    handleBlur4() {
      let val = document.getElementById("confirmPassword").value;
      if (!val) {
        this.isFocused4 = false;
      }
    },
    submitForm() {
      // 提交表单逻辑
    },
    sendVerificationCode() {
      // 发送验证码逻辑
      // 这里可以调用后端API发送验证码
      // 设置 isSendingCode 为 true 来禁用发送按钮
      this.isSendingCode = true;

      // 模拟发送验证码成功，延时2秒后恢复按钮状态
      setTimeout(() => {
        this.isSendingCode = false;
      }, 2000);
    },
  },
  // mounted() {
  //   // 从 sessionStorage 中获取令牌
  //   const token = sessionStorage.getItem("token");

  //   if (!token) {
  //     // 令牌不存在，说明未经过身份验证，跳转回登录页面或其他处理
  //     this.$router.push("/fail");
  //     return;
  //   }
  // },
};
</script>

<style lang="postcss" scoped>
.body {
  background-image: url("../../assets/hellopolice.jpg");
  background-attachment: fixed;
  background-position: center;
  background-repeat: no-repeat;
  background-size: cover;
  height: 120vh;
  overflow: auto;
}

.headpic {
  background: #1f2cdf;
  background-image: url("../../assets/hdtest.jpg"); /* 替换为你的背景图路径 */
  background-size: contain;
  background-position: right top; /* 背景图靠左上角 */
  background-repeat: no-repeat;
  width: 100%;
  height: 70px;
  overflow: hidden;
  position: relative;
  left: 0;
  display: flex;
  align-items: center;
  padding-left: 20px;
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
}

.change-password-container {
  flex-direction: column;
  display: flex;
  margin: 0 auto;
  margin-top: 50px;
  padding: 20px;
  border-radius: 4px;
  height: 80%;
  width: 30%;
  max-width: 35%;
  min-width: 400px;
  justify-content: center;
  align-items: center;
  border-radius: 0.3em;
  backdrop-filter: blur(2px);
  background-color: #2b2828ba;
  box-shadow: 0 0 0 10px hsla(0, 2%, 73%, 0.711) inset,
    0 0.3em 1em rgba(0, 0, 0, 0.12);
}
.forget-text {
  color: #fff;
  font-size: 40px; /* 字号大小 */
  font-weight: bold; /* 字体加粗 */
  margin-top: 1px;
}
.setnew-text {
  color: #fff;
  margin-top: 1px;
  font-size: 20px; /* 字号大小 */
  font-weight: bold; /* 字体加粗 */
}
.input-container {
  display: flex;
  position: relative;
  width: 400px;
  height: 80px;
  justify-content: center;
  align-items: center;
  margin-top: 20px;
  font-size: 20px;
}

#phoneNumber {
  display: inline-block;
  width: 300px;
  height: 30px;
  color: #0ff;
  font-size: 20px;
  border: 0px transparent;
  border-bottom: 3px solid #fff;
  background-color: transparent;
}

#phoneNumber:focus {
  outline: none;
  border-bottom: 3px solid #0ff;
}

#code-input {
  display: inline-block;
  width: 300px;
  height: 30px;
  color: #0ff;
  font-size: 20px;
  border: 0px transparent;
  border-bottom: 3px solid #fff;
  background-color: transparent;
}

#code-input:focus {
  outline: none;
  border-bottom: 3px solid #0ff;
}

#newPassword {
  display: inline-block;
  width: 300px;
  height: 30px;
  color: #0ff;
  font-size: 20px;
  border: 0px transparent;
  border-bottom: 3px solid #fff;
  background-color: transparent;
}

#newPassword:focus {
  outline: none;
  border-bottom: 3px solid #0ff;
}

#confirmPassword {
  display: inline-block;
  width: 300px;
  height: 30px;
  color: #0ff;
  font-size: 20px;
  border: 0px transparent;
  border-bottom: 3px solid #fff;
  background-color: transparent;
}

#confirmPassword:focus {
  outline: none;
  border-bottom: 3px solid #0ff;
}

.Tip {
  position: absolute;
  top: 5px;
  left: 20px;
  font-size: 20px;
  font-weight: bold;
  color: #fff;
}
@keyframes user {
  from {
    top: 0px;
    font-size: 20px;
  }
  to {
    top: -20px;
    font-size: 17px;
    color: rgb(12, 235, 179);
  }
}

.TipA {
  animation: user 0.3s linear normal forwards;
  animation-iteration-count: 1;
}

button {
  margin-left: 50px;
  margin-bottom: 5px;
  background-color: #1890ff;
  padding: 10px 20px;
  color: #fff;
  border: none;
  border-radius: 4px;
  cursor: pointer;
  font-size: 18px;
  font-weight: bold;
}

button:disabled {
  background-color: #ccc;
  cursor: not-allowed;
}
.confirm-button {
  margin-bottom: 10px;
}
.confirm-button:hover {
  background-color: #40a9ff;
}
</style>
