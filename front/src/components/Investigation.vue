<template>
  <div>
    <h1 class="title">组成办案组</h1>
    <div>
      <div v-if="finalConfirmFlag == 0">
        <input type="text" v-model="inputCaseID" placeholder="请输入案件编号">
        <p v-if="caseChooseFlag == 0">
          <button @click="queryCase">查询</button>
        </p>
        <p v-else>
          <button :disabled="true">查询</button>
        </p>
      </div>
      <div v-if="caseContent[2] == '立案' && caseQueryAgainFlag == 0">
        <div class="caseContainer">
          <p><br></p>
          <p>&nbsp;&nbsp;&nbsp;&nbsp;案件编号：{{ caseContent[0] }}</p>
          <p>&nbsp;&nbsp;&nbsp;&nbsp;案件类型：{{ caseContent[1] }}</p>
          <p>&nbsp;&nbsp;&nbsp;&nbsp;案件状态：{{ caseContent[2] }}</p>
          <p>&nbsp;&nbsp;&nbsp;&nbsp;案件等级：{{ caseContent[3] }}</p>
          <div v-if="caseChooseFlag == 0">
            &nbsp;&nbsp;&nbsp;&nbsp;
            <button @click="confirmCase">确认选择</button>
          </div>
          <div v-else>
            <p v-if="finalConfirmFlag == 0">
              &nbsp;&nbsp;&nbsp;&nbsp;
              <button @click="confirmCaseAgain">重新选择案件</button>
            </p>
        </div>
          <p><br></p>
        </div>
        <div v-if="caseChooseFlag == 1">
          <p v-if="finalConfirmFlag == 0">
            <input type="text" v-model="inputPoliceID" placeholder="请输入警员编号">
          </p>
          <p v-if="finalConfirmFlag == 0"><button @click="queryPolice">查询</button></p>
          <div v-if="policeContent[3] == '在职'">
            <div v-if="policeCount !== 0 && policeInfo[0][0] !== null">
              <div v-for="(field, i) in policeInfo" :key="field[0]">
                <div class="policeContainer">
                  <p><br>&nbsp;&nbsp;&nbsp;&nbsp;办案组成员 {{ i+1 }}</p>
                  <p>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;警员编号：{{ field[0] }}</p>
                  <p>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;姓名：{{ field[1] }}</p>
                  <p>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;电话号码：{{ field[2] }}</p>
                  <p>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;职务：{{ field[4] }}</p>
                  <p v-if="finalConfirmFlag == 0">
                    &nbsp;&nbsp;&nbsp;&nbsp;
                    <button @click="deletePolice">取消选择</button>
                  </p>
                  <p><br></p>
                </div>
              </div>
            </div>
            <div v-if="(((policeCount !== 0 && policeContent[0] !== policeInfo[policeCount - 1][0]) || policeCount == 0) && policeQueryFlag !== -1)">
              <div class="policeContainer">
                <p><br></p>
                <p>&nbsp;&nbsp;&nbsp;&nbsp;警员编号：{{ policeContent[0] }}</p>
                <p>&nbsp;&nbsp;&nbsp;&nbsp;姓名：{{ policeContent[1] }}</p>
                <p>&nbsp;&nbsp;&nbsp;&nbsp;电话号码：{{ policeContent[2] }}</p>
                <p>&nbsp;&nbsp;&nbsp;&nbsp;职务：{{ policeContent[4] }}</p>
                <p v-if="policeChooseFlag == 0">
                  &nbsp;&nbsp;&nbsp;&nbsp;
                  <button @click="confirmPolice">确认选择</button>
                </p>
                <p v-else>
                  &nbsp;&nbsp;&nbsp;&nbsp;
                  <button @click="deletePolice">取消选择</button>
                </p>
                <p><br></p>
              </div>
            </div>
          </div>
          <div v-else-if="policeContent[3] == '离职'">
            此警员已离职，请重新选择警员！
          </div>
        </div>
      </div>
      <div v-else-if="caseContent[2] == '调查'">
        此案件已在调查中，请重新选择案件！
      </div>
      <div v-else-if="caseContent[2] == '结案'">
        此案件已结案，请重新选择案件！
      </div>
      <div v-else-if="caseQueryFlag == 0">
        此案件编号不存在，请重新选择案件！
      </div>
      <!-- <p>&nbsp;&nbsp;</p> -->
    </div>
    <p v-if="policeCount !== 0 && finalConfirmFlag == 0"><button @click="finalConfirm">确认成立办案组</button></p>
    <div v-if="finalConfirmFlag == 1">
      <br>
      <button @click="next">继续成立办案组</button>
    </div>
  </div>
</template>

<script>
import axios from 'axios'
export default({
  data () {
    return {
      inputCaseID: '',
      inputPoliceID: '',
      caseContent: [],
      policeContent: [],
      policeInfo: [[]],
      caseQueryFlag: -1,
      caseChooseFlag: 0,
      caseQueryAgainFlag: 0,
      policeQueryFlag: -1,
      policeChooseFlag: 0,
      policeChooseFlagArray: [],
      policeCount: 0,
      finalConfirmFlag: 0,
      x: 0,
      i: 0
    }
  },
  methods: {
    queryCase () {
      axios.post('http://localhost:7078/api/queryCase', { InputTextCase: this.inputCaseID })
        .then(response => {
          this.caseContent = response.data
          if (this.caseContent[0] !== null) {
            this.caseQueryFlag = 1
            this.policeChooseFlag = 0
          } else {
            this.caseQueryFlag = 0
          }
        })
        .catch(error => {
          console.error(error)
        })
    },
    queryPolice () {
      axios.post('http://localhost:7078/api/queryPolice', { InputTextPolice: this.inputPoliceID })
        .then(response => {
          this.policeContent = response.data
          if (this.policeContent[0] !== null) {
            this.policeQueryFlag = 1
            this.policeChooseFlag = 0
          } else {
            this.policeQueryFlag = 0
          }
        })
        .catch(error => {
          console.error(error)
        })
    },
    confirmCase () {
      this.inputCaseID = ''
      this.caseChooseFlag = 1
    },
    confirmCaseAgain () {
      this.inputCaseID = ''
      this.inputPoliceID = ''
      this.caseContent = []
      this.policeContent = []
      this.policeInfo = [[]]
      this.caseQueryFlag = -1
      this.caseChooseFlag = 0
      this.caseQueryAgainFlag = 0
      this.policeQueryFlag = -1
      this.policeChooseFlag = 0
      this.policeChooseFlagArray = []
      this.policeCount = 0
      this.finalConfirmFlag = 0
      this.x = 0
      this.i = 0
    },
    confirmPolice () {
      this.inputPoliceID = ''
      this.policeChooseFlag = 1
      this.policeChooseFlagArray[this.policeCount] = 1
      this.policeInfo[this.policeCount] = this.policeContent
      this.policeCount += 1
    },
    deletePolice () {
      this.policeChooseFlagArray[this.i] = 0
      this.policeCount -= 1
      for (this.x = this.i + 1; this.x <= this.policeInfo.length; this.x++) {
        this.policeInfo[this.i] = this.policeInfo[this.i + 1]
      }
      this.policeInfo.length -= 1
      this.policeQueryFlag = -1
    },
    finalConfirm () {
      this.finalConfirmFlag = 1
      axios.put('http://localhost:7078/api/modifyCaseStatus', { InputCase: this.caseContent[0] })
    },
    next () {
      this.inputCaseID = ''
      this.inputPoliceID = ''
      this.caseContent = []
      this.policeContent = []
      this.policeInfo = [[]]
      this.caseQueryFlag = -1
      this.caseChooseFlag = 0
      this.caseQueryAgainFlag = 0
      this.policeQueryFlag = -1
      this.policeChooseFlag = 0
      this.policeChooseFlagArray = []
      this.policeCount = 0
      this.finalConfirmFlag = 0
      this.x = 0
      this.i = 0
    }
  }
})
</script>

<style>
.caseContainer{
    width: 200pt;
    background-color: rgb(20, 0, 98);
    color: white;
    position: relative;
}
.policeContainer{
    width: 200pt;
    background-color: rgb(98, 0, 0);
    color: white;
    position: relative;
}
.title{
    text-align: center;
}
</style>

<!-- 一组测试数据：H5605121 9123765 7398123-->
