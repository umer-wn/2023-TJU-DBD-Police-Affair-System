<template>
    <div>
        <h4>案件总数：{{ numCases }}</h4>

        <h2 style="text-align: center">案件状态统计</h2>
        <select v-model="selectedCity1">
            <option value="全部" selected>全部城市</option>
            <option v-for="city in cityName" :value="city">{{ city }}</option>
        </select>
        <select v-model="selectedYear1">
            <option selected value="全部">全部年份</option>
            <option v-for="i in years" :value="i">{{ i }}</option>
        </select>
        <select v-model="selectedMonth1">
            <option selected value="全部">全部月份</option>
            <option v-for="i in ['1', '2', '3', '4', '5', '6', '7', '8', '9', '10', '11', '12']" :value="i">{{ i }}</option>
        </select>
        <div id="statusPieChart" style="width: 400px; height: 400px; margin-left: auto; margin-right: auto"></div>

        <h2 style="text-align: center">案件类型统计</h2>
        <select v-model="selectedCity2">
            <option value="全部" selected>全部城市</option>
            <option v-for="city in cityName" :value="city">{{ city }}</option>
        </select>
        <select v-model="selectedYear2">
            <option selected value="全部">全部年份</option>
            <option v-for="i in years" :value="i">{{ i }}</option>
        </select>
        <select v-model="selectedMonth2">
            <option selected value="全部">全部月份</option>
            <option v-for="i in ['1', '2', '3', '4', '5', '6', '7', '8', '9', '10', '11', '12']" :value="i">{{ i }}</option>
        </select>
        <div id="typePieChart" style="width: 600px; height: 400px; margin-left: auto; margin-right: auto"></div>

        <h2 style="text-align: center">城市案件统计</h2>
        <table border="1px solid black" style="margin-left: auto; margin-right: auto;text-align: center">
            <thead>
                <tr>
                    <th>城市</th>
                    <th>数量</th>
                </tr>
            </thead>
            <tbody>
                <tr v-for="(count_, city_) in cityStatistics" :key="city_">
                    <td>{{ city_ }}</td>
                    <td>{{ count_ }}</td>
                </tr>
            </tbody>
        </table>

        <h2 style="text-align: center">分时间和城市的案件统计</h2>
        <select v-model="selectedCity3">
            <option value="全部" selected>全部城市</option>
            <option v-for="city in cityName" :value="city">{{ city }}</option>
        </select>
        <select v-model="selectedMethod">
            <option selected value="年份">年份统计</option>
            <option selected value="月份">月份统计</option>
        </select>
        <select v-if="selectedMethod === '月份'" v-model="selectedYear3">
            <option selected value="全部">全部年份</option>
            <option v-for="i in years" :value="i">{{ i }}</option>
        </select>
        <div id="zheChart" style="width: 600px; height: 400px; margin-left: auto; margin-right: auto"></div>


        <!-- <button @click="getCityDateStatistics">基于城市的时间统计</button> -->
        <!-- <div>{{ cityDateStatistics }}</div> -->
    </div>
</template>

<script>
import axios from 'axios';
import * as echarts from 'echarts';

export default {
    data() {
        return {
            numCases: 0,
            statusStatistics: "默认",
            typeStatistics: "默认",
            cityStatistics: "默认",
            cityName: "默认",
            selectedCity1: "全部",
            selectedYear1: "全部",
            selectedMonth1: "全部",
            selectedCity2: "全部",
            selectedYear2: "全部",
            selectedMonth2: "全部",
            selectedCity3: "全部",
            selectedYear3: "全部",
            cityDateStatistics: "默认",
            years: [],
            selectedMethod: "年份",
        };
    },
    methods: {
        async getCityStatistics() {
            try {
                const response = await axios.get("http://localhost:7078/api/caseStatistics/caseCityStatistics");
                this.cityStatistics = response.data;
                console.log(response.data);
            } catch (error) {
                // 请求失败时的处理逻辑
                alert(error);
                console.log("getCityStatistics出错！");
            }
        },
        async getCityDateStatistics() {
            try {
                const response = await axios.get("http://localhost:7078/api/caseStatistics/cityDateStatistics", { params: { city: this.selectedCity3 } });
                this.cityDateStatistics = response.data;
                console.log(response.data);
            } catch (error) {
                // 请求失败时的处理逻辑
                alert(error);
                console.log("getCityDateStatistics出错！");
            }
        },
        async getStatusCityDateStatistics() {
            try {
                const response = await axios.get(`http://localhost:7078/api/caseStatistics/statusCityDateStatistics?city=${encodeURIComponent(this.selectedCity1)}&year=${encodeURIComponent(this.selectedYear1)}&month=${encodeURIComponent(this.selectedMonth1)}`);
                // this.cityStatistics = response.data;
                console.log("123::");
                console.log(response.data);
                this.statusStatistics = response.data;
            } catch (error) {
                // 请求失败时的处理逻辑
                alert(error);
                console.log("getStatusCityDateStatistics出错！");
            }
        },
        async getTypeCityDateStatistics() {
            try {
                const response = await axios.get(`http://localhost:7078/api/caseStatistics/typeCityDateStatistics?city=${encodeURIComponent(this.selectedCity2)}&year=${encodeURIComponent(this.selectedYear2)}&month=${encodeURIComponent(this.selectedMonth2)}`);
                this.typeStatistics = response.data;
            } catch (error) {
                // 请求失败时的处理逻辑
                alert(error);
                console.log("getTypeCityDateStatistics出错！");
            }
        },
        drawStatusPieChart() {
            const pieChart = echarts.init(document.getElementById('statusPieChart'));

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
                            { value: this.statusStatistics['立案'], name: '立案' },
                            { value: this.statusStatistics['调查'], name: '调查' },
                            { value: this.statusStatistics['结案'], name: '结案' }
                        ]
                    }
                ]
            };

            option && pieChart.setOption(option);
        },
        drawTypePieChart() {
            const pieChart = echarts.init(document.getElementById('typePieChart'));

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
                            { value: this.typeStatistics['强奸'], name: '强奸' },
                            { value: this.typeStatistics['抢劫'], name: '抢劫' },
                            { value: this.typeStatistics['故意伤害'], name: '故意伤害' },
                            { value: this.typeStatistics['盗窃'], name: '盗窃' },
                            { value: this.typeStatistics['诈骗'], name: '诈骗' },
                            { value: this.typeStatistics['谋杀'], name: '谋杀' }
                        ]
                    }
                ]
            };

            option && pieChart.setOption(option);
        },
        drawYearZheChart() {
            var result = {}; // 保存结果的对象
            console.log(123);
            for (const [year, yearData] of Object.entries(this.cityDateStatistics)) {
                let sum = 0;
                for (const value of Object.values(yearData)) {
                    sum += value;
                }
                result[year] = sum;
            }
            result = Object.values(result); // result: [6, 6, 7, 4, 5, 4, 78]

            const zheChart = echarts.init(document.getElementById('zheChart'));

            const option = {
                xAxis: {
                    type: 'category',
                    data: this.years
                },
                yAxis: {
                    type: 'value'
                },
                series: [
                    {
                        data: result,
                        type: 'line'
                    }
                ]
            };

            option && zheChart.setOption(option);
        },
        drawMonthZheChart() {
            const zheChart = echarts.init(document.getElementById('zheChart'));
            var option;

            if (this.selectedYear3 !== "全部") {

                option = {
                    xAxis: {
                        type: 'category',
                        data: ['Jan', 'Feb', 'Mar', 'Apr', 'May', 'Jun', 'Jul', 'Aug', 'Sep', 'Oct', 'Nov', 'Dec']
                    },
                    yAxis: {
                        type: 'value'
                    },
                    series: [
                        {
                            data: [this.cityDateStatistics[this.selectedYear3]["1"], this.cityDateStatistics[this.selectedYear3]["2"],
                            this.cityDateStatistics[this.selectedYear3]["3"], this.cityDateStatistics[this.selectedYear3]["4"],
                            this.cityDateStatistics[this.selectedYear3]["5"], this.cityDateStatistics[this.selectedYear3]["6"],
                            this.cityDateStatistics[this.selectedYear3]["7"], this.cityDateStatistics[this.selectedYear3]["8"],
                            this.cityDateStatistics[this.selectedYear3]["9"], this.cityDateStatistics[this.selectedYear3]["10"],
                            this.cityDateStatistics[this.selectedYear3]["11"], this.cityDateStatistics[this.selectedYear3]["12"]],
                            type: 'line'
                        }
                    ]
                };
            }
            else {
                var result = {}; // 保存结果的对象

                for (const [year, yearData] of Object.entries(this.cityDateStatistics)) {
                    for (const [month, value] of Object.entries(yearData)) {
                        if (!result[month]) {
                            result[month] = value;
                        } else {
                            result[month] += value;
                        }
                    }
                }
                result = Object.values(result);

                console.log(2222222);
                console.log(result);

                option = {
                    xAxis: {
                        type: 'category',
                        data: ['Jan', 'Feb', 'Mar', 'Apr', 'May', 'Jun', 'Jul', 'Aug', 'Sep', 'Oct', 'Nov', 'Dec']
                    },
                    yAxis: {
                        type: 'value'
                    },
                    series: [
                        {
                            data: [result["1"], result["2"],
                            result["3"], result["4"],
                            result["5"], result["6"],
                            result["7"], result["8"],
                            result["9"], result["10"],
                            result["11"], result["12"]],
                            type: 'line'
                        }
                    ]
                };
            }

            option && zheChart.setOption(option);
        },
        drawZheChart() {
            if (this.selectedMethod === "年份") {
                this.drawYearZheChart();
            }
            else {
                this.drawMonthZheChart();
            }
        },
    },
    watch: {
        async selectedCity1() {
            await this.getStatusCityDateStatistics();
            this.drawStatusPieChart();
        },
        async selectedYear1() {
            await this.getStatusCityDateStatistics();
            this.drawStatusPieChart();
        },
        async selectedMonth1() {
            await this.getStatusCityDateStatistics();
            this.drawStatusPieChart();
        },
        async selectedCity2() {
            await this.getTypeCityDateStatistics();
            this.drawTypePieChart();
            console.log("测试");
        },
        async selectedYear2() {
            await this.getTypeCityDateStatistics();
            this.drawTypePieChart();
            console.log("测试");
        },
        async selectedMonth2() {
            await this.getTypeCityDateStatistics();
            this.drawTypePieChart();
            console.log("测试");
        },
        selectedYear3() {
            this.drawZheChart();
        },
        async selectedCity3() {
            await this.getCityDateStatistics();
            this.drawZheChart();
        },
        async selectedMethod() {
            if (this.selectedMethod === "年份") {
                this.drawZheChart();
            }
            else {
                await this.getCityDateStatistics();
                this.drawZheChart();
            }
        }
    },
    async created() {
        await this.getCityStatistics();           // 获取城市及其总的案件数，其内接口返回数据形如{"西安":10,"上海":20,...}
        await this.getStatusCityDateStatistics(); // 获取指定城市指定年月的案件状态，其内接口返回数据形如{"立案":10,"状态":20,...}
        await this.getTypeCityDateStatistics();   // 获取指定城市指定年月的案件类型，其内接口返回数据形如{"抢劫":10,"谋杀":20,...}
        await this.getCityDateStatistics();       // 获取指定城市指定年月的案件数目，其内接口返回数据形如{"2017":{"1":3,"2":5,...},...}
        this.years = Object.keys(this.cityDateStatistics); // 获取年份范围,形如["2017","2018",...]
        this.cityName = Object.keys(this.cityStatistics);  // 获取年份范围,形如["西安","上海",...]

        // 获取案件总数
        this.numCases = this.statusStatistics["立案"] + this.statusStatistics["调查"] + this.statusStatistics["结案"];

        this.drawStatusPieChart(); // 画总案件状态圆饼图
        this.drawTypePieChart(); // 画总案件类型圆饼图
        this.drawZheChart(); // 画折线图

    },
};
</script>

