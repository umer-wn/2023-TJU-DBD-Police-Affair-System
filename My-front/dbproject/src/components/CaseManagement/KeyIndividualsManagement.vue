<template>
<el-header class="sub-header"  @mousemove="handleMouseMove">
    <div>&nbsp;&nbsp;案件管理&nbsp;>&nbsp;重点人员统计</div>
  </el-header>

<div class="main">
  <el-page-header class="headText" :icon="ArrowLeft" title="返回" @back="goBack">
    <template #content>
      <el-text type="primary" class="headText">{{ headText }}</el-text>
    </template>
  </el-page-header>

  <!-- 表格 -->
  <el-table v-if="!isDetail" :data="repeatOffenderInfo" height="450" @wheel.passive.stop stripe style="width: 100%">
    <el-table-column label="身份证号码" prop="身份证号" />
    <el-table-column label="姓名" prop="姓名" />
    <el-table-column label="性别" prop="性别" />
    <el-table-column align="right" width="300">
      <template #header>
        <table>
          <tr>
            <td><el-text type="primary" style="display: inline-block;">筛选身份证号码：</el-text></td>
            <td><el-input v-model="roID" size="small" placeholder="请输入身份证号码"
                style="display: inline-block; width: 130px; height: 25px" /></td>
          </tr>
          <tr>
            <td><el-text type="primary" style="display: inline-block;">筛选姓名：</el-text></td>
            <td><el-input v-model="roName" size="small" placeholder="请输入姓名"
                style="display: inline-block; width: 130px; height: 25px" /></td>
          </tr>
          <tr>
            <td><el-text type="primary" style="display: inline-block;">筛选性别：</el-text></td>
            <td>
              <el-select v-model="roSex" placeholder="请选择" size="small"
                style="display: inline-block; width: 130px; height: 25px">
                <el-option label="请选择" value="全部" />
                <el-option label="男" value="M" />
                <el-option label="女" value="F" />
              </el-select>
            </td>
          </tr>
        </table>
      </template>
      <template #default="scope">
        <el-button size="small" type="primary" @click="getDetail(scope.$index, scope.row)">详情</el-button>
      </template>
    </el-table-column>
  </el-table>

  <el-table v-if="isDetail" :data="caseRecord" stripe style="width: 100%">
    <el-table-column label="案件编号" prop="案件编号" />
    <el-table-column label="案件类型" prop="案件类型" />
    <el-table-column label="案件状态" prop="案件状态" />
    <el-table-column label="登记时间" prop="登记时间" />
    <el-table-column label="案发地点" prop="案发地点" />
    <el-table-column label="案件等级" prop="案件等级" />
  </el-table>
</div>
</template>

<script>
import axios from 'axios';

export default {
  data() {
    return {
      headText: "重犯名单",
      repeatOffenderInfo: "",
      roName: "",
      isDetail: false,
      roID: "",
      roSex: "",
      caseRecord: "",
    };
  },
  methods: {
     handleMouseMove(event) {
      const x = event.pageX - event.target.offsetLeft;
      const y = event.pageY - event.target.offsetTop;

      event.target.style.setProperty("--x", `${x}px`);
      event.target.style.setProperty("--y", `${y}px`);
    },
    async getRepeatOffenderInfo() {
      try {
        const response = await axios.get("http://localhost:7078/api/keyIndividualsStatistics/repeatOffenderInfoStatistics");
        this.repeatOffenderInfo = response.data;
        console.log(response.data);
      } catch (error) {
        // 请求失败时的处理逻辑
        alert(error);
        console.log("getRepeatOffenderInfo出错！");
      }
    },
    goBack() {
      if (this.isDetail) {
        this.isDetail = false;
        this.headText = "重犯名单";
      }
    },
    async getDetail(index, man) {
      window.scrollTo({
        top: 0,
        behavior: 'smooth' // 可选的平滑滚动效果
      });
      this.isDetail = true;
      this.headText = "犯罪记录";
      try {
        const response = await axios.get("http://localhost:7078/api/keyIndividualsStatistics/repeatOffenderCaseStatistics", { params: { id: man["身份证号"] } });
        this.caseRecord = response.data;
        console.log(this.caseRecord);
      } catch (error) {
        // 请求失败时的处理逻辑
        alert(error);
        console.log("getDetail函数出错：" + error);
      }
    },
    async filter() {
      try {
        const response = await axios.get(`http://localhost:7078/api/keyIndividualsStatistics/repeatOffenderFilterStatistics?ID=${encodeURIComponent(this.roID)}&name=${encodeURIComponent(this.roName)}&sex=${encodeURIComponent(this.roSex)}`);
        this.repeatOffenderInfo = response.data;
        console.log(response.data);
      } catch (error) {
        // 请求失败时的处理逻辑
        alert(error);
        console.log("filter函数出错：" + error);
      }
    }
  },
  async created() {
    await this.getRepeatOffenderInfo();
  },
  watch: {
    async roID() {
      await this.filter();
    },
    async roName() {
      await this.filter();
    },
    async roSex() {
      await this.filter();
    },
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
.main
{
  margin-top:10vh;
}
.headText {
  display: block;
  font-size: 20px;
  margin: 10px;
}
</style>