<template>
  <el-header class="sub-header" @mousemove="handleMouseMove">
    <div>&nbsp;&nbsp;警员管理&nbsp;>&nbsp;薪水管理</div>
  </el-header>
  <div class="main">
    <el-tabs type="border-card">
      <el-tab-pane label="查询" class="father">
        <div class="childElement">
          <div>
            <el-form
              :label-position="top"
              label-width="100px"
              :model="searchData"
              style="width: 450px; height: 100%"
            >
              <el-form-item label="警号">
                <el-input v-model="searchData.police_number" />
              </el-form-item>
              <el-form-item label="姓名">
                <el-input v-model="searchData.name" />
              </el-form-item>
              <el-form-item size="large" label="年份">
                <el-select
                  v-model="searchData.year"
                  placeholder="please select year"
                >
                  <el-option value="">全部</el-option>
                  <el-option v-for="year in years" :value="year">{{
                    year
                  }}</el-option>
                </el-select>
              </el-form-item>

              <el-form-item size="large" label="月份">
                <el-select
                  v-model="searchData.month"
                  placeholder="please select month"
                >
                  <el-option value="">全部</el-option>
                  <el-option v-for="month in months" :value="month.value">{{
                    month.label
                  }}</el-option>
                </el-select>
              </el-form-item>

              <el-form-item size="large" label="警局">
                <el-select
                  v-model="searchData.station"
                  placeholder="please select station"
                >
                  <el-option value="">全部</el-option>

                  <el-option value="320103014">徐州市公安局鼓楼分局</el-option>
                </el-select>
              </el-form-item>
              <el-row>
                <el-col :span="20"></el-col>
                <el-col :span="2">
                  <el-button type="submit" size="large" @click="search"
                    >搜索</el-button
                  >
                </el-col>
              </el-row>
              <!-- <button type="submit">搜索</button> -->
            </el-form>
          </div>
          <div>
            <div class="spacer"></div>
            <el-table
              :data="searchData.searchResults"
              style="width: 100%"
              height="300px"
            >
              <el-table-column prop="POLICE_NAME" label="姓名" width="120" />

              <el-table-column
                prop="PAYROLL_NUMBER"
                label="流水号"
                width="100"
              />
              <el-table-column prop="POLICE_NUMBER" label="警号" width="100" />
              <el-table-column prop="STATION_ID" label="警局号" width="120" />
              <el-table-column prop="PAY_DAY" label="薪水日期" width="100" />
              <el-table-column prop="SALARY" label="薪水" width="120" />
              <el-table-column prop="SUBSIDY" label="补贴" width="120" />
              <el-table-column prop="DESCRIPTION" label="描述" width="120" />
            </el-table>
          </div>
        </div>
      </el-tab-pane>
      <el-tab-pane label="新建" class="father">
        <div class="childElement">
          <el-form
            :label-position="top"
            label-width="100px"
            :model="searchData"
            style="width: 450px; height: 100%"
          >
            <el-form-item label="ID:">
              <el-input v-model="employeeData.police_number_receive" required />
            </el-form-item>
            <el-form-item label="基本工资:">
              <el-input
                type="number"
                v-model="employeeData.basic_amount"
                required
              />
            </el-form-item>
            <el-form-item label="奖金:">
              <el-input
                type="number"
                v-model="employeeData.reward_amount"
                required
              />
            </el-form-item>
            <el-form-item label="描述">
              <el-input
                type="textarea"
                :rows="4"
                placeholder="请输入内容"
                v-model="employeeData.description"
                required
              ></el-input>
            </el-form-item>

            <el-row>
              <el-col :span="20"></el-col>
              <el-col :span="2">
                <el-button type="submit" size="large" @click="submitForm"
                  >确认</el-button
                >
              </el-col>
            </el-row>
          </el-form>
        </div>
      </el-tab-pane>
    </el-tabs>
  </div>
</template>

<script>
import axios from "../../api/request";

export default {
  data() {
    return {
      searchData: {
        police_number: "",
        name: "",
        year: "", // 存储年份
        month: "", // 存储月份
        station: "",
        showResults: false, // 控制是否显示搜索结果
        searchResults: [
          // {
          //   PAYROLL_NUMBER: "001",
          //   POLICE_NUMBER: "P001",
          //   POLICE_NAME: "张三",
          //   STATION_ID: "S001",
          //   PAY_DAY: "2023-08-01",
          //   SALARY: 5000,
          //   SUBSIDY: 1000,
          //   DESCRIPTION: "工资发放",
          //   ISSUE_ID: "ISSUE001",
          //   ISSUE_NAME: "ISSUE001",
          // },
          // {
          //   PAYROLL_NUMBER: "002",
          //   POLICE_NUMBER: "P002",
          //   POLICE_NAME: "李四",
          //   STATION_ID: "S002",
          //   PAY_DAY: "2023-08-01",
          //   SALARY: 6000,
          //   SUBSIDY: 1200,
          //   DESCRIPTION: "工资发放",
          //   ISSUE_ID: "ISSUE002",
          //   ISSUE_NAME: "ISSUE001",
          // },
          // {
          //   PAYROLL_NUMBER: "001",
          //   POLICE_NUMBER: "P001",
          //   POLICE_NAME: "张三",
          //   STATION_ID: "S001",
          //   PAY_DAY: "2023-08-01",
          //   SALARY: 5000,
          //   SUBSIDY: 1000,
          //   DESCRIPTION: "工资发放",
          //   ISSUE_ID: "ISSUE001",
          //   ISSUE_NAME: "ISSUE001",
          // },
          // {
          //   PAYROLL_NUMBER: "002",
          //   POLICE_NUMBER: "P002",
          //   POLICE_NAME: "李四",
          //   STATION_ID: "S002",
          //   PAY_DAY: "2023-08-01",
          //   SALARY: 6000,
          //   SUBSIDY: 1200,
          //   DESCRIPTION: "工资发放",
          //   ISSUE_ID: "ISSUE002",
          //   ISSUE_NAME: "ISSUE001",
          // },
          // {
          //   PAYROLL_NUMBER: "001",
          //   POLICE_NUMBER: "P001",
          //   POLICE_NAME: "张三",
          //   STATION_ID: "S001",
          //   PAY_DAY: "2023-08-01",
          //   SALARY: 5000,
          //   SUBSIDY: 1000,
          //   DESCRIPTION: "工资发放",
          //   ISSUE_ID: "ISSUE001",
          //   ISSUE_NAME: "ISSUE001",
          // },
          // {
          //   PAYROLL_NUMBER: "002",
          //   POLICE_NUMBER: "P002",
          //   POLICE_NAME: "李四",
          //   STATION_ID: "S002",
          //   PAY_DAY: "2023-08-01",
          //   SALARY: 6000,
          //   SUBSIDY: 1200,
          //   DESCRIPTION: "工资发放",
          //   ISSUE_ID: "ISSUE002",
          //   ISSUE_NAME: "ISSUE001",
          // },
        ], // 存储搜索结果
      },
      years: ["2023", "2022", "2021"], // 年份选项
      months: [
        { value: "01", label: "一月" },
        { value: "02", label: "二月" },
        { value: "03", label: "三月" },
        { value: "04", label: "四月" },
        { value: "05", label: "五月" },
        { value: "06", label: "六月" },
        { value: "07", label: "七月" },
        { value: "08", label: "八月" },
        { value: "09", label: "九月" },
        { value: "10", label: "十月" },
        { value: "11", label: "十一月" },
        { value: "12", label: "十二月" },
        // ... 还可以继续添加其他月份
      ],
      employeeData: {
        police_number_receive: "",
        basic_amount: "",
        reward_amount: "",
        description: "",
      },
    };
  },
  methods: {
    handleMouseMove(event) {
      const x = event.pageX - event.target.offsetLeft;
      const y = event.pageY - event.target.offsetTop;

      event.target.style.setProperty("--x", `${x}px`);
      event.target.style.setProperty("--y", `${y}px`);
    },
    search() {
      // 在这里执行搜索操作
      // 检查 searchData 中至少有一个字段非空
      if (
        !this.searchData.police_number &&
        !this.searchData.name &&
        !this.searchData.year &&
        !this.searchData.month &&
        !this.searchData.station
      ) {
        alert("搜索条件不足！");
        return;
      }

      // 使用axios发送数据到后端，并在headers中包含Token
      axios
        .post("/searchWagesRecord", this.searchData)
        .then((response) => {
          // 处理后端返回的数据

          this.searchResults = response.data.map((result) => {
            let newResult = {};
            for (let key in result) {
              newResult[key.toUpperCase()] = result[key];
            }
            return newResult;
          });

          // 显示搜索结果区域
          this.showResults = true;
        })
        .catch((error) => {
          // 处理请求错误
          console.error("请求错误:", error); // 隐藏搜索结果区域
          this.showResults = false;
          // 显示错误消息
          alert("请求错误，请稍后重试");
        });
    },
    submitForm() {
      // 使用axios发送数据到后端，并在headers中包含Token
      axios
        .post("/salary_newRecord", this.employeeData)
        .then((response) => {
          // 处理响应
          console.log(response.data);
        })
        .catch((error) => {
          // 处理错误
          console.error(error);
        });
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
  content: "";
  position: absolute;
  left: var(--x);
  top: var(--y);
  width: var(--size);
  height: var(--size);
  background: radial-gradient(circle closest-side, #5a65ff, transparent);
  transform: translate(-50%, -50%);
  transition: width 0.2s ease, height 0.2s ease;
}
.sub-header:hover::before {
  --size: 400px;
}

.main {
  margin-top: 10vh;
  /* display: flex; */
  /* justify-content: center; */
}
.father {
  display: flex;
  justify-content: center;
}

.spacer {
  height: 30px; /* 设置间距高度为 10px */
}
</style>
