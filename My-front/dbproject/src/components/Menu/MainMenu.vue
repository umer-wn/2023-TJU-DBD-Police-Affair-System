<template>
  <el-container
    class="home-container"
    :style="{
      height: containerHeight + 'px',
      backgroundSize: '100% ' + containerHeight + 'px',
    }"
    @wheel="handleScroll"
  >
    <!-- 头部 -->
    <el-header
      style="height: 70px; padding: 0; text-align: justify; hyphens: auto"
    >
      <div class="headpic">
        <div class="header1">
          <img class="logo" src="../../assets/image-2.png" />
          <div class="title">&nbsp;&nbsp;警务处理系统</div>

          <!-- <div class="footer1">返回首页</div> -->
        </div>
      </div>
    </el-header>

    <!-- 页面主体 -->
    <el-container>
      <!-- 导航栏 -->
      <el-aside width="200px">
        <el-menu
          router
          class="el-menu-vertical-demo"
          default-active="2"
          background-color="transparent"
          text-color="#ffffff"
          active-text-color="#ffd04b"
        >
          <el-sub-menu index="1-1">
            <!-- 警员管理 -->
            <template #title>
              <span>警员管理</span>
            </template>
            <el-menu-item index="/mainMenu/Register">注册 </el-menu-item>
            <el-menu-item index="/mainMenu/ChangePermission">
              警员权限修改
            </el-menu-item>
            <el-menu-item index="/mainMenu/PoliceAssessment">
              警员考核管理
            </el-menu-item>
            <el-menu-item index="/mainMenu/PoliceStationInfoManagement">
              警局信息管理
            </el-menu-item>
            <el-menu-item index="/mainMenu/PoliceOfficerInfoManagement">
              警员信息管理
            </el-menu-item>
            <el-menu-item index="/mainMenu/AttendanceManagement">
              警员出勤管理
            </el-menu-item>
            <el-menu-item index="/mainMenu/VideoManagement">
              执法录像管理
            </el-menu-item>
            <el-menu-item index="/mainMenu/AlarmResponseRecordManagement">
              接警记录管理
            </el-menu-item>
            <el-menu-item index="/mainMenu/PayrollManagement">
              薪水管理
            </el-menu-item>
          </el-sub-menu>

          <!-- 案件管理 -->
          <el-sub-menu index="1-2">
            <template #title>
              <span>案件管理</span>
            </template>
            <el-menu-item index="/mainMenu/EvidenceManagement">
              证据收纳
            </el-menu-item>
            <el-menu-item index="/mainMenu/SuspectInfoManagement">
              嫌疑人信息管理
            </el-menu-item>
            <el-menu-item index="/mainMenu/CriminalRecordManagement">
              犯罪记录管理
            </el-menu-item>
            <el-menu-item index="/mainMenu/VictimInfoManagement">
              受害人信息管理
            </el-menu-item>
            <el-menu-item index="/mainMenu/CaseClassificationManagement">
              案件分类与管理
            </el-menu-item>
            <el-menu-item index="/mainMenu/CrimeDataStatistics">
              犯罪数据统计
            </el-menu-item>
            <el-menu-item index="/mainMenu/KeyIndividualsManagement">
              重点人员统计
            </el-menu-item>
          </el-sub-menu>

          <!-- 装备管理 -->
          <el-sub-menu index="1-3">
            <template #title>
              <span>装备管理</span>
            </template>
            <el-menu-item index="/mainMenu/PoliceVehicleManagement">
              警车管理
            </el-menu-item>
            <el-menu-item index="/mainMenu/PoliceEquipmentManagement">
              警械管理
            </el-menu-item>
          </el-sub-menu>

          <!-- 城区和居民管理 -->

          <el-sub-menu index="1-4">
            <template #title>
              <span>城区和居民管理</span>
            </template>

            <el-menu-item index="/mainMenu/CitizenInfoManagement">
              公民户籍管理
            </el-menu-item>
            <el-menu-item index="/mainMenu/UrbanZoningManagement">
              城市地区管理
            </el-menu-item>
            <el-menu-item index="/mainMenu/RegionalDispatch">
              城市调度管理
            </el-menu-item>
            <el-menu-item index="/mainMenu/FamilybgCheck">
              家族背景调查
            </el-menu-item>
          </el-sub-menu>

          <el-menu-item index="/mainMenu/DataQualityManagement">
            数据质量管理
          </el-menu-item>
          <el-menu-item index="/mainMenu/CaseInvestigation">
            案件办理
          </el-menu-item>

          <!-- 系统日志 -->

          <el-menu-item index="/mainMenu/SystemLogManagement">
            系统日志管理
          </el-menu-item>
        </el-menu>
      </el-aside>

      <!-- 页面主体 -->
      <el-main class="background">
        <el-button
          type="primary"
          style="margin-left: 16px"
          @click="drawer2 = true"
        >
          with footer
        </el-button>
        <router-view></router-view>
      </el-main>
    </el-container>
  </el-container>

  账户信息展示
  <el-drawer v-model="drawer2" :direction="direction">
    <template #header>
      <h4>set title by slot</h4>
    </template>
    <template #default>
      <span>账号：{{ username }}</span>
      <span>密码：{{ password }}</span>
    </template>
    <template #footer> </template>
  </el-drawer>
</template>

<script lang="js" setup>
import { ref } from "vue";

let containerHeight= 1500 // 初始化容器高度
let minHeight= 1500 // 最小高度
let maxScrollHeight= 3000 // 最大滚动高度，控制界面的延伸
// const mytitle= "注册"

const drawer2 = ref(false);
const direction = ref("rtl");
const username = ref("1234567");
const password = ref("44554");


function handleScroll(event) {
      if (event.deltaY > 0) {
        // 向下滚动
        if (containerHeight < maxScrollHeight) {
          containerHeight += 50;
        }
      } else {
        // 向上滚动
        if (containerHeight > minHeight) {
          containerHeight -= 50;
        }
      }
    }
</script>

<style scoped>
.home-container {
  width: 100%;
  overflow: hidden;
  background-color: #f0f0f0;
  transition: height 0.3s ease; /* 添加过渡效果 */
}

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
  background-attachment: fixed;
  background-position: center;
  background-repeat: no-repeat;
  /* 其他背景相关样式，例如背景颜色、背景重复等，可根据需要添加 */
}

.el-aside {
  background-color: #1620a9;
  background-image: url("../../assets/sdtest.jpg"); /* 替换为你的背景图路径 */

  background-attachment: fixed;
  background-repeat: no-repeat;
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
</style>
