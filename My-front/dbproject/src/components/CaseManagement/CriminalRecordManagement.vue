<template>
    <main class="main">
        <section>
            <div class="inputcontainer">
                <lable style="position: relative; display: block">
                    <div class="ssqinputtext">输入案件编号</div>
                    <input class="ssqinputinfobox" type="text" v-model="caseID" placeholder="案件编号" />
                </lable>
                <lable style="position: relative; display: block">
                    <div class="ssqinputtext">输入案发地点</div>
                    <input class="ssqinputinfobox" type="text" v-model="address" placeholder="案发地点" />
                </lable>
                <lable style="position: relative; display: block">
                    <div class="ssqinputtext">输入涉案人员身份证号</div>
                    <input class="ssqinputinfobox" type="text" v-model="IDNum" placeholder="涉案人员身份证号" />
                </lable>
            </div>
            <div class="inputcontainer">
                <div class="selectcontainer">
                    <div class="ssqinputtext">选择案件类型</div>
                    <select class="zyhselect" v-model="caseType">
                        <option selected value="全部">全部案件类型</option>
                        <option value="强奸">强奸</option>
                        <option value="抢劫">抢劫</option>
                        <option value="故意伤害">故意伤害</option>
                        <option value="盗窃">盗窃</option>
                        <option value="诈骗">诈骗</option>
                        <option value="谋杀">谋杀</option>
                    </select>
                </div>

                <div class="selectcontainer">
                    <div class="ssqinputtext">选择案件状态</div>
                    <select class="zyhselect" v-model="status">
                        <option selected value="全部">全部案件状态</option>
                        <option value="立案">立案</option>
                        <option value="结案">结案</option>
                        <option value="调查">调查</option>
                    </select>
                </div>
            </div>
            <div class="inputcontainer">
                <div class="selectcontainer">
                    <div class="ssqinputtext">选择案件等级</div>
                    <select class="zyhselect" v-model="ranking">
                        <option selected value="全部">全部等级</option>
                        <option value="0">0</option>
                        <option value="1">1</option>
                        <option value="2">2</option>
                        <option value="3">3</option>
                    </select>
                </div>

                <div class="selectcontainer">
                    <div class="ssqinputtext">选择涉案人类别</div>
                    <select class="zyhselect" v-model="relatedType">
                        <option selected value="全部">全部涉案人</option>
                        <option value="受害人">受害人</option>
                        <option value="嫌疑人">嫌疑人</option>
                        <option value="犯人">犯人</option>
                        <option value="证人">证人</option>
                    </select>
                </div>

            </div>
            <div class="btncontainer">
                <button class="ssqbutton1" @click="fetchCaseInfo" @mousemove="handleMouseMove">
                    <span>查询</span>
                </button>
            </div>

            <!-- 表格显示获取的警员信息 -->
            <el-table v-if="caseInfo.length > 0" :data="caseInfo" stripe @wheel.passive.stop height="450">
                <el-table-column prop="caseID" label="案件编号" width="95px" />
                <el-table-column prop="caseType" label="案件类型" width="60px" />
                <el-table-column prop="status" label="案件状态" width="60px" />
                <el-table-column prop="registerTime" label="登记时间" />
                <el-table-column prop="address" label="案发地点" />
                <el-table-column prop="ranking" label="案件等级" width="60px" />
                <el-table-column prop="idNum" label="涉案人身份证号" />
                <el-table-column prop="relatedType" label="涉案类型" width="80px" />
                <el-table-column prop="citizenName" label="涉案人姓名" width="95px" />
                <el-table-column prop="gender" label="涉案人性别" width="95px" />
            </el-table>
            <!-- 错误提示 -->
            <div v-else>{{ boxContent }}</div>
        </section>
    </main>
</template>

<script>
import axios from "axios";

export default {
    data() {
        return {
            caseID: "",
            caseType: "全部",
            status: "全部",
            address: "",
            ranking: "全部",
            IDNum: "",
            relatedType: "全部",
            caseInfo: [],
            err: "录像不存在！",
        };
    },
    methods: {
        fetchCaseInfo() {
            axios
                .post("http://localhost:7078/api/citizenInCaseInfo", {
                    caseID: this.caseID,
                    caseType: this.caseType,
                    status: this.status,
                    address: this.address,
                    ranking: this.ranking,
                    IDNum: this.IDNum,
                    relatedType: this.relatedType,
                })
                .then((res) => {
                    this.caseInfo = res.data;
                    for (var i = 0; i < this.caseInfo.length; i++) {
                        if (this.caseInfo[i].gender === "F") {
                            this.caseInfo[i].gender = "女";
                        } else if (this.caseInfo[i].gender === "M") {
                            this.caseInfo[i].gender = "男";
                        }
                    }
                    // console.log(res.data);
                })
                .catch((err) => {
                    this.boxContent = this.err;
                    console.log(err);
                });
        },
    },
};
</script>

<style lang="postcss" scoped>
main {
    display: flex;
    justify-content: center;
    align-content: center;
    width: 100%;
    height: 120vh;
    min-width: 800px;
}

.ssqinputinfobox {
    position: relative;
    width: 10vw;
    display: inline-block;
}

.ssqinputtext {
    text-align: center;
    margin-top: 7vh;
    margin-left: 5vw;
    margin-right: 2vw;
    width: auto;
    display: inline-block;
}

input {
    margin-top: 5vh;
    display: block;
    width: 10vw;
    padding: 0.8em;
    outline: none;
    border: 1px solid #e3e3e3;
    border-radius: 2px;
}

.zyhselect {
    margin-top: 5vh;
    display: block;
    width: 10vw;
    padding: 0.8em;
    outline: none;
    border: 1px solid #e3e3e3;
    border-radius: 2px;
    margin-left: 5vw;
    margin-right: 2vw;
    margin-top: 5vh;
}

.inputcontainer,
.selectcontainer {
    display: flex;
    align-content: center;
}

.btncontainer {
    display: flex;
    justify-content: center;
}

.leftbtn {
    margin-right: 5vw;
}

.maintable {
    flex-direction: column;
    align-content: center;
    justify-content: center;
    text-align: center;
    margin: 20px auto;
    width: 80vw;
    height: 75vh;
}

table {
    position: relative;
    width: 100%;
    border: 1px solid #ccc;
    text-align: center;

    tbody {
        border-collapse: separate;
        height: 100%;
    }

    td,
    th {
        padding: 5px;
        border: 1px solid #ccc;
    }
}

.rolltable {
    top: -1px;
    position: relative;
    width: 100%;
    height: 100%;
    overflow-y: scroll;
    overflow-x: auto;
    background: linear-gradient(#fff, transparent) top / 100% 100px,
        radial-gradient(at 50% -15px, rgba(0, 0, 0, 0.8), transparent 70%) top / 100000% 12px;
    background-repeat: no-repeat;
    background-attachment: local, scroll;
}

.ssqbutton1 {
    margin-top: 3vh;
    margin-bottom: 5vh;
    width: 150px;
    height: 40px;
    padding: 0 30px;
    line-height: 60px;
    text-align: center;
    position: relative;
    appearance: none;
    background: #dfd8c8;
    border: none;
    color: white;
    font-size: 1.2em;
    cursor: pointer;
    outline: none;
    overflow: hidden;
    border-radius: 100px;
}

.ssqbutton1 span {
    position: relative;
    top: -20%;
}

.ssqbutton1::before {
    --size: 0;
    content: "";
    position: absolute;
    left: var(--x);
    top: var(--y);
    width: var(--size);
    height: var(--size);
    background: radial-gradient(circle closest-side, #0b72bbb2, transparent);
    transform: translate(-50%, -50%);
    transition: width 0.2s ease, height 0.2s ease;
}

.ssqbutton1:hover::before {
    --size: 400px;
}

.ssqbutton1-1 {
    margin: 10px auto;
    width: 150px;
    height: 40px;
    padding: 0 30px;
    line-height: 60px;
    text-align: center;
    position: relative;
    appearance: none;
    background: #0b71bb;
    border: none;
    color: white;
    font-size: 1.2em;
    cursor: pointer;
    outline: none;
    overflow: hidden;
    border-radius: 0px;
}

.ssqbutton1-1 span {
    position: relative;
    top: -20%;
}

.ssqbutton1-1::before {
    --size: 0;
    content: "";
    position: absolute;
    left: var(--x);
    top: var(--y);
    width: var(--size);
    height: var(--size);
    background: radial-gradient(circle closest-side, #abc9de, transparent);
    transform: translate(-50%, -50%);
    transition: width 0.2s ease, height 0.2s ease;
}

.ssqbutton1-1:hover::before {
    --size: 400px;
}

.ssqbutton1-2 {
    margin: 10px auto;
    width: 150px;
    height: 40px;
    padding: 0 30px;
    line-height: 60px;
    text-align: center;
    position: relative;
    appearance: none;
    background: #dfd8c8;
    border: none;
    color: white;
    font-size: 1.2em;
    cursor: pointer;
    outline: none;
    overflow: hidden;
    border-radius: 0px;
}

.ssqbutton1-2 span {
    position: relative;
    top: -20%;
}

.ssqbutton1-2::before {
    --size: 0;
    content: "";
    position: absolute;
    left: var(--x);
    top: var(--y);
    width: var(--size);
    height: var(--size);
    background: radial-gradient(circle closest-side, #99c4e5, transparent);
    transform: translate(-50%, -50%);
    transition: width 0.2s ease, height 0.2s ease;
}

.ssqbutton1-2:hover::before {
    --size: 400px;
}
</style>
