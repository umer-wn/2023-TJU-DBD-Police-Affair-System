<template>
  <el-header class="sub-header" @mousemove="handleMouseMove">
      <div>&nbsp;&nbsp;警员管理&nbsp;>&nbsp;注册</div>
    </el-header>
  
    <div class="container" @wheel.passive.stop>
      <div class="ssqtitletest"><span>添&nbsp;加&nbsp;新&nbsp;用&nbsp;户</span></div>
      <!-- 以下为添加新用户的表单 -->
      <div class="content">
        <!-- 每个输入栏由inputContainer组合，内含标题inputTitle、输入框input、可能含错误提示message -->
        <!-- police_number -->
        <div class="inputContainer">
          <div class="inputTitle">警号</div>
          <input class="input" v-model="signinInfo.police_number" />
          <div
            class="message"
            v-show="this.police_number_message !== ''"
            :style="{ color: this.police_number_color }"
          >
            {{ this.police_number_message }}
          </div>
        </div>
        <!-- police_name -->
        <div class="inputContainer">
          <div class="inputTitle">姓名</div>
          <input class="input" v-model="signinInfo.police_name" />
        </div>
        <!-- ID_number -->
        <div class="inputContainer">
          <div class="inputTitle">身份证号</div>
          <input class="input" v-model="signinInfo.ID_number" />
          <div
            class="message"
            v-show="this.ID_number_message !== ''"
            :style="{ color: this.ID_number_color }"
          >
            {{ this.ID_number_message }}
          </div>
        </div>
        <!-- birthday -->
        <div class="inputContainer">
          <div class="inputTitle">出生日期</div>
          <el-date-picker v-model="signinInfo.birthday"
            >this.signinInfo.birthday</el-date-picker
          >
        </div>
        <!-- gender -->
        <div class="inputContainer">
          <div class="inputTitle">性别</div>
          <input class="input"  v-model="signinInfo.gender" />
        </div>
        <!-- nation -->
        <div class="inputContainer">
          <div class="inputTitle">民族</div>
          <input class="input" v-model="signinInfo.nation" />
          <div
            class="message"
            v-show="this.nation_message !== ''"
            :style="{ color: this.nation_color }"
          >
            {{ this.nation_message }}
          </div>
        </div>
        <!-- phone_number -->
        <div class="inputContainer">
          <div class="inputTitle">联系方式</div>
          <input class="input" v-model="signinInfo.phone_number" />
        </div>
        <!-- email -->
        <div class="inputContainer">
          <div class="inputTitle">邮箱</div>
          <input class="input" v-model="signinInfo.email" />
          <div
            class="message"
            v-show="this.email_message !== ''"
            :style="{ color: this.email_color }"
          >
            {{ this.email_message }}
          </div>
        </div>
        <!-- status -->
        <div class="inputContainer">
          <div class="inputTitle">状态</div>
          <select class="select" v-model="signinInfo.status">
            <option>在职</option>
            <option>停职</option>
            <option>离职</option>
          </select>
        </div>
        <!-- position -->
        <div class="inputContainer">
          <div class="inputTitle">职务</div>
          <select class="select" v-model="signinInfo.position">
            <option>学员</option>
            <option>警员</option>
            <option>警司</option>
            <option>警督</option>
            <option>警监</option>
            <option>总警监</option>
          </select>
        </div>
        <!-- 确定按钮 -->
        <button class="button" @click="addNewUser">确定</button>
      </div>
    </div>
  </template>
  
  <script>
  import axios from "axios";
  
  export default {
    data() {
      return {
        // 初始密码由后端生成
        signinInfo: {
          police_number: "",
          police_name: "",
          ID_number: "",
          birthday: "",
          gender: "",
          nation: "",
          phone_number: "",
          email: "",
          status: "",
          position: "",
        },
        // 以下为提高用户体验的变量
        police_number_message: "",
        police_number_color: "",
        ID_number_message: "",
        ID_number_color: "",
        nation_message: "",
        nation_color: "",
        email_message: "",
        email_color: "",
      };
    },
    methods: {
      handleMouseMove(event) {
        const x = event.pageX - event.target.offsetLeft;
        const y = event.pageY - event.target.offsetTop;

        event.target.style.setProperty('--x', `${x}px`);
        event.target.style.setProperty('--y', `${y}px`);
      },
      // 添加新用户
      addNewUser() {
        for (let key in this.signinInfo) {
          if (this.signinInfo[key] === "") {
            alert("请填写完整信息");
            return;
          }
        }
        if (
          this.police_number_color !== "green" ||
          this.ID_number_color !== "green" ||
          this.nation_color !== "green" ||
          this.email_color !== "green"
        ) {
          alert("请填写正确信息");
          return;
        }
        if (this.signinInfo.gender === "男") this.signinInfo.gender = "M";
        else this.signinInfo.gender = "F";
        console.log(this.signinInfo);
        axios
          .put("http://localhost:7078/api/Register", this.signinInfo)
          .then((response) => {
            if (response.data == "success") {
              alert("添加成功");
            } else {
              alert("网络错误，请重试");
            }
          })
          .catch((error) => {
            console.error(error);
          });
      },
      // 根据身份证号计算出生日期
      calcBirthday(birthInfo) {
        let year = birthInfo.slice(0, 4);
        let month = birthInfo.slice(4, 6);
        let day = birthInfo.slice(6, 8);
        this.signinInfo.birthday = year + "-" + month + "-" + day;
      },
      checkLegal(queryType, queryContent) {
        axios
          .post("http://localhost:7078/api/Register", {
            queryType,
            queryContent,
          })
          .then((response) => {
            if (queryType == "police_number") {
              if (response.data === "ok") {
                this.police_number_message = "✔";
                this.police_number_color = "green";
              } else {
                this.police_number_message = "该警号已存在";
                this.police_number_color = "red";
              }
            } else if (queryType == "ID_number") {
              if (response.data === "ok") {
                this.ID_number_message = "✔";
                this.ID_number_color = "green";
                this.signinInfo.gender =
                  Number(this.signinInfo.ID_number[16]) % 2 == 1 ? "男" : "女";
                this.calcBirthday(this.signinInfo.ID_number.slice(6, 14));
              } else {
                this.ID_number_message = "该身份证号已注册过";
                this.ID_number_color = "red";
              }
            }
          })
          .catch((error) => {
            console.error(error);
          });
      },
    },
    watch: {
      // 监听警号输入框
      "signinInfo.police_number": function (val) {
        if (val.length != 7) {
          this.police_number_message = "✖";
          this.police_number_color = "red";
        } else {
          this.police_number_message = "校验...";
          this.police_number_color = "blue";
          this.checkLegal("police_number", val);
        }
      },
      "signinInfo.ID_number": function (val) {
        if (val.length != 18) {
          this.ID_number_message = "✖";
          this.ID_number_color = "red";
        } else {
          this.ID_number_message = "校验...";
          this.ID_number_color = "blue";
          this.checkLegal("ID_number", val);
        }
      },
      "signinInfo.nation": function (val) {
        if (!val.endsWith("族")) {
          this.nation_message = "请以“族”结尾";
          this.nation_color = "red";
        } else {
          this.nation_message = "✔";
          this.nation_color = "green";
        }
      },
      "signinInfo.email": function (val) {
        if (
          val.includes("@") &&
          val.includes(".") &&
          !val.endsWith(".") &&
          !val.endsWith("@")
        ) {
          this.email_message = "✔";
          this.email_color = "green";
        } else {
          this.email_message = "邮箱地址不正确";
          this.email_color = "red";
        }
      },
    },
  };
  </script>
  
  <style scoped>
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
  .ssqtitletest {
    margin:10px auto;
    width:700px;
    height:40px;
    padding:0 30px;
    line-height: 60px;
    text-align: center;
    position: relative;
    appearance: none;
    background: #e6dbc1;
    box-shadow: inset -500px 0px 100px 0px rgb(204, 186, 144);
    border: none;
    color: white;
    font-size: 25px;
    outline: none;
    overflow: hidden;
    border-radius: 0px;
  }
  .ssqtitletest span {
    position: relative;
    top: -20%;
  }
  .container {
    /* width: 80vw;
    height: 130vh;  /* width: 80vw;
    height: 130vh; */
    /* background-color: #f4f6f9; */
    margin-top:10vh;
     min-width: 700px;
    display: flex;
    flex-direction: column;
    justify-content: center;
    align-items: center;
  }
  .title {
    font-size: 30px;
    font-weight: bold;
    color: #9c7614;
    margin: 16px;
  
    text-shadow: #9e37ff 0px 0px 3px;
    text-shadow: #9a9a9a 0px 0px 6px;
  }
  .content {
    width: 38%;
    min-width: 700px;
    height: 850px;
    position: relative;
    box-shadow: 0px 0px 10px 2px rgba(123, 103, 75, 0.427);
    background-color: rgba(255, 255, 255, 0.616); 
    margin-bottom: 30px;
    display: flex;
    flex-direction: column;
    justify-content: flex-start;
    align-items: center;
    margin: 16px;
    padding: 16px;
  
    border-radius: 5px;
    box-shadow: #9a9a9a 0px 0px 6px;
    box-shadow: #777777 0px 0px 3px;
    border-top: #0051ff 3px solid;
    border-top: solid 3px transparent;
  }
  
  .content:hover {
    border-top-color: #c1a04d;
  }
  
  .content {
    transition: border-top-color 0.3s;
  }
  
  .content:hover {
    transition: border-top-color 0.3s;
  }
  .inputContainer {
    width: 61%;
    height: 60px;
    display: flex;
    flex-direction: column;
    justify-content: flex-start;
    margin: 6px;
  }
  .inputTitle {
    width: 100%;
    font-size: 18px;
    margin-bottom: 4px;
    margin-left: 0px;
    color: #b3a37c;
    text-shadow: #ba9c51 0px 0px 1px;
  }
  .input {
    width: 100%;
    height: 30px;
  
    outline: none;
    border: 1px solid #e3e3e3;
    border-radius: 2px;
    box-shadow: #b4a078 0px 0px 2px;
    font-size: 14px;
    padding: 6px;
  
    transition: border-color 0.2s ease-in-out;
    transition: box-shadow 0.2s ease-in-out;
  }
  .input:focus {
    outline: none;
    border-color: #6777ef;
    box-shadow: #3d4cc1 0px 0px 2px;
  }

  .select {
    width: 100%;
    height: 30px;
  
    background: #fdfdff;
    border: #ebecfd 1px solid;
    box-shadow: #b4a078 0px 0px 2px;
    border-radius: 5px;
  
    font-size: 14px;
    padding: 6px;
    transition: border-color 0.2s ease-in-out;
    transition: box-shadow 0.2s ease-in-out;
  }
  .select:focus {
    outline: none;
    border: #6777ef 1px solid;
    box-shadow: #6777ef 0px 0px 2px;
  }
  .select:not(:focus) {
    outline: none;
    box-shadow: #9a9a9a 0px 0px 1px;
  }
  .button {
    width: 61%;
    height: 44px;
    border-radius: 5px;
    background-color: #6777ef;
    box-shadow: #777777 0px 0px 1px;
    border: none;
    padding: 6px;
    color: #ffffff;
    font-weight: 600;
  
    transition: background-color 0.2s ease-in-out, box-shadow 0.2s ease-in-out;
  }
  .button:hover {
    background-color: #394eea;
    box-shadow: #9a9a9a 0px 0px 3px;
  }
  .button:not(:disabled):not(.disabled) {
    cursor: pointer;
  }
  .message {
    width: 100%;
    height: 8px;
    font-size: 4px;
    margin-left: 6px;
  }
  </style>