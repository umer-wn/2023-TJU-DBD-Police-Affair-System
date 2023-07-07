<template>
<div>
  <!-- 显示图片 -->
  <div class="contrl" @mousemove="onMouseMove" :style="{backgroundColor:`hsl(${x}, 80%, 50%)`}">
    <div class="overlay">
    <img v-bind:src="imgUrl" class="imgctrl" @mouseover="showText" @mouseout="hideText">
    <div class="text" v-if="showText">姓名：葛蕾<br>年龄：25<br>罪名：杀人<br>刑期：无期徒刑</div>
    </div>
  </div>
  
</div>
</template>




<script>
export default {
  data() {
    return {
      showText: false,
      x:0
    }
  },
  props:['imgUrl'],
  methods: {
    showText() {
    this.showText = true
    },
    hideText() {
    this.showText = false
    },
    onMouseMove(e){
        this.x=e.clientX;
    }
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
    background-color: rgba(66, 164, 255, 0.447);
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
    top: 50%;
    left: 120%; /* 将文字初始位置设置在图片右侧，可以根据需要调整 */
    transform: translate(-50%, -50%);
    opacity: 1;
    transition: opacity 0.3s ease, left 0.3s ease;
}
.imgctrl{
    width: 50%; /* 根据需要调整图片宽度 */
    height: auto; /* 根据需要调整图片高度 */
    transition: transform 0.3s ease; /* 添加过渡效果 */
}
</style>
