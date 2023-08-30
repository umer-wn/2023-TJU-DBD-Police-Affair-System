<template>
  <el-header class="sub-header" @mousemove="handleMouseMove">
    <div>&nbsp;&nbsp;警务处理系统&nbsp;>&nbsp;案件办理</div>
  </el-header>

  <el-table v-if="process === '待调查案件'" :data="cases" height="450" @wheel.passive.stop stripe
    style="width: 100%; margin-top: 10vh">
    <el-table-column label="案件编号" prop="案件编号" sortable width="105px" />
    <el-table-column label="案件类型" prop="案件类型" sortable width="105px" />
    <el-table-column label="案件状态" prop="案件状态" sortable width="105px" />
    <el-table-column label="登记时间" prop="登记时间" width="155px" sortable />
    <el-table-column label="案发地点" prop="案发地点" sortable />
    <el-table-column label="案件等级" prop="案件等级" sortable width="105px" />
    <el-table-column align="right" width="260px">
      <template #header>
        <table>
          <tr>
            <td><el-text type="primary" style="display: inline-block;">筛选案件编号：</el-text></td>
            <td><el-input v-model="caseID" size="small" placeholder="请输入案件编号"
                style="display: inline-block; width: 130px; height: 25px" /></td>
          </tr>
        </table>
      </template>
      <template #default="scope">
        <el-button size="small" type="primary" @click="setTeam(scope.$index, scope.row)">进行调查</el-button>
      </template>
    </el-table-column>
  </el-table>

  <div v-if="process === '选择办案人员'" style="text-align: center">
    <el-table :data="policemen" height="400" @selection-change="selectPolicemen" @wheel.passive.stop stripe
      style="width: 100%; margin-top: 10vh">
      <el-table-column type="selection" />
      <el-table-column label="警员编号" prop="警员编号" sortable />
      <el-table-column label="姓名" prop="姓名" sortable />
      <el-table-column label="性别" prop="性别" sortable />
      <el-table-column label="民族" prop="民族" sortable />
      <el-table-column label="状态" prop="状态" sortable />
      <el-table-column label="职位" prop="职位" sortable />
      <el-table-column align="left" width="260px">
        <template #header>
          <table>
            <tr>
              <td><el-text type="primary" style="display: inline-block; width: 130px">当前选择{{ selectCaseID }}</el-text>
              </td>
              <td><el-button size="small" type="primary" @click="goBack">重选案件</el-button></td>
            </tr>
            <tr>
              <td><el-text type="primary" style="display: inline-block;">筛选警员编号：</el-text></td>
              <td><el-input v-model="policemenID" size="small" placeholder="请输入警员编号"
                  style="display: inline-block; width: 100px; height: 25px" /></td>
            </tr>
          </table>
        </template>
      </el-table-column>
    </el-table>

    <el-button type="primary" @click="finish" :disabled="isSet" style="margin-top: 10px">调查此案件</el-button>
  </div>

  <div v-if="process === '完成'" style="text-align: center">
    <el-result icon="success" title="案件已设为调查" sub-title="点击按钮返回立案列表">
      <template #extra>
        <el-button type="primary" @click="backToH">返回</el-button>
      </template>
    </el-result>
  </div>
</template>

<script>
import axios from "axios";
export default {
  data() {
    return {
      caseID: "",
      process: "待调查案件",
      cases: "",
      policemen: "",
      policemenID: "",
      selectCaseID: "",
      team: "",
      isSet: true,
    };
  },
  methods: {
    handleMouseMove(event) {
      const x = event.pageX - event.target.offsetLeft;
      const y = event.pageY - event.target.offsetTop;

      event.target.style.setProperty("--x", `${x}px`);
      event.target.style.setProperty("--y", `${y}px`);
    },
    async setTeam(index, Case) {
      this.process = "选择办案人员";
      await this.getPolicemen();
      this.selectCaseID = Case["案件编号"];
    },
    async getCases() {
      try {
        const response = await axios.get(`http://localhost:7078/api/caseInvestigation/queryCase?caseID=${encodeURIComponent(this.caseID)}`);
        this.cases = response.data;
        console.log(response.data);
      } catch (error) {
        // 请求失败时的处理逻辑
        alert(error);
        console.log("getCases函数出错：" + error);
      }
    },
    async getPolicemen() {
      try {
        const response = await axios.get(`http://localhost:7078/api/caseInvestigation/queryPolicemen?policemenID=${encodeURIComponent(this.policemenID)}`);
        this.policemen = response.data;
        console.log(response.data);
      } catch (error) {
        // 请求失败时的处理逻辑
        alert(error);
        console.log("getPolicemen函数出错：" + error);
      }
    },
    goBack() {
      this.process = "待调查案件";
      this.selectCaseID = "";
    },
    selectPolicemen(selection) {
      // console.log(selection);
      // console.log(selection[0]["警员编号"]);
      // console.log(selection.length)
      this.team = selection;
      if (this.team.length > 0)
        this.isSet = false;
      else
        this.isSet = true;
    },
    async finish() {
      try {
        await axios.put(`http://localhost:7078/api/caseInvestigation/modifyCaseStatus?caseID=${encodeURIComponent(this.selectCaseID)}`);
      } catch (error) {
        // 请求失败时的处理逻辑
        alert(error);
        console.log("finish函数出错：" + error);
      };

      this.process = "完成";
      this.selectCaseID = "";
      this.selectPolicemen = "";
      await this.getCases();
    },
    backToH() {
      this.process = "待调查案件";
    }
  },
  async created() {
    await this.getCases();
  },
  watch: {
    async caseID() {
      await this.getCases();
    },
    async policemenID() {
      await this.getPolicemen();
    }
  }
};
</script>

<style>
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
}

.caseContainer {
  width: 200pt;
  background-color: rgb(20, 0, 98);
  color: white;
  position: relative;
}

.policeContainer {
  width: 200pt;
  background-color: rgb(98, 0, 0);
  color: white;
  position: relative;
}

.title {
  text-align: center;
}
</style>

<!-- ?? 一组测试数据：H5605121 9123765 7398123-->
