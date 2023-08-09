<template>
<div>
  <!-- 显示图片 -->
  <div class="contrl">
    <!-- @mousemove="onMouseMove" :style="{backgroundColor:`hsl(${x}, 80%, 50%)`}" -->
    <div class="overlay">
      <img v-bind:src="imgUrl" class="imgctrl" @mouseover="showText" @mouseleave="hideText">
      <div class="text" v-if="show">
        姓名：{{item.Name}}<br>身份证号：{{item.ID}}<br>性别：{{item.gender}}<br>
        <div v-if="item == content.items[1][i]">
          关系：{{content.items[2][i]}}<br>
        </div>
        罪名:
        <ul>
          <li v-for="(type) in item.crimeType" :key="type">
            {{type}}
          </li>
        </ul>
      </div>
    </div>
  </div>
</div>
</template>

<script>
import FamilyCrime from './FamilyCrime.vue'
export default {
  components: {
    FamilyCrime
  },
  data () {
    return {
      show: false,
      hide: true
      // x: 0
    }
  },
  props: ['imgUrl', 'content', 'items', 'item', 'i'],
  methods: {
    showText () {
      this.show = true
      this.hide = false
    },
    hideText () {
      this.show = false
      this.hide = true
    }
    // onMouseMove(e) {
    //   this.x=e.clientX;
    // }
  }
}
</script>

<style>
.overlay {
    position: relative;
}
.contrl{
    width:250px;
    height:200px;
    background-color: grey;
    display: flex;
    align-items: center;
    justify-content: center;
    padding: 10px;
    color: white;
    position: relative;
    transition: 0.3s background-color ease;
}
.text{
    position: absolute;
    width: 50%;
    top: 50%;
    left: 80%; /* 将文字初始位置设置在图片右侧，可以根据需要调整 */
    transform: translate(-50%, -50%);
    opacity: 1;
    transition: opacity 1s ease, left 1s ease;
}
.imgctrl{
    width: 50%; /* 根据需要调整图片宽度 */
    height: auto; /* 根据需要调整图片高度 */
    transition: transform 0.3s ease; /* 添加过渡效果 */
}
</style>
