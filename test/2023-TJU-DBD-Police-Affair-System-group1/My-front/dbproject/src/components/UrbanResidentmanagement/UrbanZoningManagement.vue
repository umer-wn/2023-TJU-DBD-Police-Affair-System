<template>
  <el-header class="sub-header" @mousemove="handleMouseMove">
    <div>&nbsp;&nbsp;城区和居民管理&nbsp;>&nbsp;城市地区管理</div>
  </el-header>

  <!--LHM编写用于呈现区域犯罪信息与人口情况-->

  <div class="main">
    <div class="content">
      <input type="text" v-model="CityName" placeholder="请输入全称如“上海市”" />
      <el-button type="primary" @click="fetchDistrictInfo">查询</el-button>

      <ul class="result-list">


        <li v-for="result in distictResults" :key="result.DistrictName">
          <div class="district-box">
            <div class="district-name">{{ result.districtName }}</div>
            <div>人口:{{ result.population }}</div>
            <div>犯罪数:{{ result.crimeNum }}</div>
            <div>犯罪率:{{ crimeRate(result) }}</div>
          </div>

          <div class="charts">
            <div class="top-content">
              <h2 style="text-align: center">案件类型统计饼图</h2>
              <!-- 使用v-for迭代distictResults列表，并为每个result调用drawStatusPieChart方法 -->
              <div :id="'typePieChart_' + result.DistrictName" style="width: 600px; height: 400px;"></div>
            </div>

          </div>

        </li>
      </ul>



    </div>
  </div>
</template>

<script>
import axios from 'axios';
import * as echarts from 'echarts';


export default {
  data() {
    return {
      CityName: '',
      distictResults: [],
      crimeTypeStatistics: [],
    }
  },
  methods: {
    handleMouseMove(event) {
      const x = event.pageX - event.target.offsetLeft;
      const y = event.pageY - event.target.offsetTop;
      event.target.style.setProperty("--x", `${x}px`);
      event.target.style.setProperty("--y", `${y}px`);
    },

    // 计算犯罪率
    crimeRate(result) {
      return result.crimeNum / result.population;
    },

    drawStatusPieChart() {
      //对distictResults列表中的每个result进行遍历
      this.distictResults.forEach(result => {
        // 生成每个饼状图容器的唯一id
        const chartId = 'typePieChart_' + result.DistrictName;
        // 初始化echarts实例，并传入对应的id
        const pieChart = echarts.init(document.getElementById(chartId));

        const option = {
          tooltip: {
            trigger: 'item'
          },
          legend: {
            top: '5%',
            left: 'center'
          },
          series: [
            {
              name: 'Access From',
              type: 'pie',
              radius: ['40%', '70%'],
              avoidLabelOverlap: false,
              itemStyle: {
                borderRadius: 10,
                borderColor: '#fff',
                borderWidth: 2
              },
              label: {
                show: false,
                position: 'center'
              },
              emphasis: {
                label: {
                  show: true,
                  fontSize: 20,
                  fontWeight: 'bold'
                }
              },
              labelLine: {
                show: false
              },
              data: [
                { value: result.crimeTypeStatistic[0], name: '强奸' },
                { value: result.crimeTypeStatistic[1], name: '抢劫' },
                { value: result.crimeTypeStatistic[2], name: '故意伤害' },
                { value: result.crimeTypeStatistic[3], name: '盗窃' },
                { value: result.crimeTypeStatistic[4], name: '诈骗' },
                { value: result.crimeTypeStatistic[5], name: '谋杀' }
              ]
            }
          ]
        };
        option && pieChart.setOption(option);
        console.log("正常执行");
      });
    },

    // 生成折线图数据
    //lineData(result) {
    // return result.districtCrimeTimeStatistic; 
    // },


    async fetchDistrictInfo() {
      try {

        const response = await axios.get(`http://localhost:7078/api/CityCrimeInfo?CityName=${encodeURIComponent(this.CityName)}`);
        console.log("fetchDistrictInfo读取内容" + response.data);
        this.distictResults = response.data;
        await new Promise((resolve) => setTimeout(resolve, 500));
        this.drawStatusPieChart();
        console.log("调用结束");
      } catch (error) {
        console.log("fetchDistrictInfo出错！" + error);
      }
    },


  }
}
</script>


<style scoped>
.top-content {
  margin-top: 15px;
  display: block;
}

.bottom-content {
  display: block;
}

.cds-select {
  width: 25%;
  height: 35px;
  display: inline-block;
  margin-left: 40px;
  background-color: #f0e6cf;
  border: #c0b7a2;
}

.cdsamount {
  margin-top: 10px;
  margin-left: 40px
}

.content {
  min-width: 1000px;
  min-height: 800px;
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

/* 导航条下方边框阴影*/
.main ::v-deep .el-tabs__nav-wrap {
  background-color: #bca77690;
  min-width: 900px;
  box-shadow: 0px 10px 10px 0px #e6dbc190;
}

.main ::v-deep .el-tabs__item.is-active {
  color: #0051ff !important;
  font-size: 20px;
}

.main ::v-deep .el-tabs__item {
  margin-left: 20px;
  color: #ffffff;
  font-size: 20px;
}

/*下方条 */
.main ::v-deep .el-tabs__active-bar {
  background-color: #0051ff !important;
}</style>