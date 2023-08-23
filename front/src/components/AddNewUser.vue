<template>
  <div class="container">
    <div class="title">添加新用户</div>
    <div class="content">
      <div class="inputContainer">
        <div class="inputTitle">警号</div>
        <input
          class="input"
          v-model="signinInfo.police_number"
        />
        <div
          class="message"
          v-show="this.police_number_message!==''"
          :style="{color:this.police_number_color}"
        >{{this.police_number_message}}</div>
      </div>
      <div class="inputContainer">
        <div class="inputTitle">姓名</div>
        <input
          class="input"
          v-model="signinInfo.police_name"
        />
      </div>
      <div class="inputContainer">
        <div class="inputTitle">身份证号</div>
        <input
          class="input"
          v-model="signinInfo.ID_number"
        />
        <div
          class="message"
          v-show="this.ID_number_message!==''"
          :style="{color:this.ID_number_color}"
        >
          {{this.ID_number_message}}</div>
      </div>
      <div class="inputContainer">
        <div class="inputTitle">出生日期</div>
        <el-date-picker v-model="signinInfo.birthday">this.signinInfo.birthday</el-date-picker>
      </div>
      <div class="inputContainer">
        <div class="inputTitle">性别</div>
        <input
          class="input"
          :disabled="true"
          v-model="signinInfo.gender"
        />
      </div>
      <div class="inputContainer">
        <div class="inputTitle">民族</div>
        <input
          class="input"
          v-model="signinInfo.nation"
        />
        <div
          class="message"
          v-show="this.nation_message!==''"
          :style="{color:this.nation_color}"
        >{{this.nation_message}}</div>
      </div>
      <div class="inputContainer">
        <div class="inputTitle">联系方式</div>
        <input
          class="input"
          v-model="signinInfo.phone_number"
        />
      </div>
      <div class="inputContainer">
        <div class="inputTitle">邮箱</div>
        <input
          class="input"
          v-model="signinInfo.email"
        />
        <div
          class="message"
          v-show="this.email_message!==''"
          :style="{color:this.email_color}"
        >
          {{this.email_message}}
        </div>
      </div>
      <div class="inputContainer">
        <div class="inputTitle">状态</div>
        <select
          class="select"
          v-model="signinInfo.status"
        >
          <option>在职</option>
          <option>停职</option>
          <option>离职</option>
        </select>
      </div>
      <div class="inputContainer">
        <div class="inputTitle">职务</div>
        <select
          class="select"
          v-model="signinInfo.position"
        >
          <option>学员</option>
          <option>警员</option>
          <option>警司</option>
          <option>警督</option>
          <option>警监</option>
          <option>总警监</option>
        </select>
      </div>
      <button
        class="button"
        @click="addNewUser"
      >确定</button>

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
    // 添加新用户
    addNewUser() {
      axios
        .put("http://localhost:7078/api/addNewUser", { Input: this.signinInfo })
        .then((response) => {
          if (response.data == "success") {
            alert("添加成功");
          } else {
            alert("添加失败");
          }
        })
        .catch((error) => {
          console.error(error);
        });
    },
    checkLegal(queryType, queryContent) {
      axios
        .get("http://localhost:7078/api/addNewUser", {
          LegalJudgeMessage: {
            queryType,
            queryContent,
          },
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
                this.signinInfo.ID_number[16] % 2 == 0 ? "F" : "M";
            } else {
              this.ID_number_message = "该身份证号已注册过";
              this.ID_number_color = "red";
            }
          }
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
    "signinInfo.gender": function (val) {
      if (val === "男") this.signinInfo.gender = "M";
      else this.signinInfo.gender = "F";
    },
    "signinInfo.birthday": function (val) {
      console.log(val);
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
.container {
  width: 100%;
  height: 100%;
  background-color: #f4f6f9;

  display: flex;
  flex-direction: column;
  justify-content: center;
  align-items: center;
}
.title {
  font-size: 30px;
  font-weight: bold;
  color: #9e37ff;
  margin: 16px;

  text-shadow: #9e37ff 0px 0px 3px;
  text-shadow: #9a9a9a 0px 0px 6px;
}
.content {
  width: 38%;
  height: 850px;
  position: relative;
  box-shadow: 0 4px 8px rgba(0, 0, 0, 0.03);
  background-color: #ffffff;
  position: relative;
  margin-bottom: 30px;
  display: flex;
  flex-direction: column;
  justify-content: flex-start;
  align-items: center;
  margin: 16px;

  border-radius: 5px;
  box-shadow: #9a9a9a 0px 0px 6px;
  box-shadow: #777777 0px 0px 3px;
  border-top: #0051ff 3px solid;
  border-top: solid 3px transparent;
}

.content:hover {
  border-top-color: #0051ff;
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
  font-size: 12px;
  margin-bottom: 4px;
  margin-left: 0px;
  font-weight: bold;
  color: #6777ef;
  text-shadow: #9a9a9a 0px 0px 1px;
}
.input {
  width: 100%;
  height: 18px;

  background: #fdfdff;
  border: #ebecfd 1px solid;
  border-radius: 5px;

  font-size: 14px;
  padding: 6px;

  transition: border-color 0.2s ease-in-out;
  transition: box-shadow 0.2s ease-in-out;
}
.input:focus {
  outline: none;
  border: #6777ef 1px solid;
  box-shadow: #6777ef 0px 0px 2px;
}
.input:not(:focus) {
  outline: none;
  box-shadow: #9a9a9a 0px 0px 1px;
}
.select {
  width: 100%;
  height: 30px;

  background: #fdfdff;
  border: #ebecfd 1px solid;
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