<template>
  <div class="container">
    <p>家族背景调查</p>
    <input type="text" v-model="inputID_num" placeholder="请输入居民身份证号">
    <button @click="query">查询</button>
    <p>{{x}}</p>
    <ul v-if="content.items != '{}' && x == 1">
        <li v-for="(item) in content.items" :key="item.ID">
            <div v-if="item==content.items[0]">
                <CriminalInfo :imgUrl="imageUrl" class="imgControl"/>
            </div>
            <div v-else-if="item==content.items[1]">
                <ul>
                    <li v-for="(item, i) in content.items[1]" :key="item.ID">
                        <CriminalInfo :imgUrl="imageUrl" class="imgControl"/>
                        {{ i }}
                    </li>
                </ul>
            </div>
        </li>
    </ul>
    <ul v-else-if="content.items == '{}' && x == 0">
      无结果！
    </ul>
  </div>
</template>

<script>
import CriminalInfo from './CriminalInfo.vue'
import axios from 'axios'
export default{
  components: {
    CriminalInfo
  },
  data () {
    return {
      inputID_num: '',
      content: [],
      x: -1,
      imageUrl: require('@/assets/criminal.png')
    }
  },
  methods: {
    query () {
      axios.post('http://localhost:7078/api/queryFamily', { InputText: this.inputID_num })
        .then(response => {
          this.content = []
          this.content = { items: response.data }
          console.log(this.content)
          if (this.content.items) {
            this.x = 1
          } else {
            this.x = 0
          }
        })
        .catch(error => {
          console.error(error)
        })
    }
  }
}
</script>

<style scoped>
.container{
    background: #ffffff;
    width: calc(100vw - 20px);
    height: 600px;
    position: relative;
    overflow: hidden;
}
.imgControl{
    margin-top: 10px;
    /* margin-left: 300px; */
    width: 100%;
    height: 40%;
}

</style>
