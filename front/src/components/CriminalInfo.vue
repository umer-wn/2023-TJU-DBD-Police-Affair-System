<template>
<div>
<<<<<<< HEAD
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
=======
  <transition name="all">
    <div class="container">
      <div
        class="textCtrlL"
      >
        <transition name="textAnime">
          <div v-show="isShow" :key="content.id">
            身份证号:<br />
            {{ content.id }}<br /><br />
            犯罪类型:<br />
            <li v-for="item in content.crimeType" :key="item.id">{{ item }}</li>
          </div>
        </transition>
      </div>

      <div class="circleBack">
        <img
          :src="imgUrl"
          class="circleCtrl"
          @mouseenter="changeTextStatus"
          @mouseleave="changeTextStatus"
        />
      </div>

      <div
        class="textCtrlR"
      >
        <transition name="textAnime">
          <div v-show="isShow" :key="content.name">
            {{ content.name }}<br /><br /><br />
            {{ content.gender == "M" ? "男" : "女" }}<br />
          </div>
        </transition>
>>>>>>> group4
      </div>
    </div>
  </transition>
</div>
</template>

<script>
export default {
<<<<<<< HEAD
  components: {
    FamilyCrime
  },
  data () {
=======
  data() {
>>>>>>> group4
    return {
      isShow: false,
    };
  },
  props: ["imgUrl", "content"],
  methods: {
<<<<<<< HEAD
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
=======
    changeTextStatus() {
      this.isShow = !this.isShow;
    },
  },
};
>>>>>>> group4
</script>

<style scoped>
.container {
  width: 400px;
  height: 160px;
  position: relative;
  overflow: hidden;

  display: flex;
  justify-content: flex-start;
  align-items: center;
}
.all-enter-active,
.all-leave-active {
  transition: opacity 1s ease;
}
.all-enter-from,
.all-leave-to {
  opacity: 0;
}
.all-enter-to,
.all-leave-from {
  opacity: 1;
}
.circleBack {
  width: 150px;
  height: 150px;
  position: relative;
  border-radius: 50%;
  overflow: hidden;
  background: linear-gradient(315deg, #42bdd3 25%, #64aaff);

  display: flex;
  justify-content: center;
  align-items: center;
}
.circleCtrl {
  width: 90%;
  height: 90%;
  position: relative;
  border-radius: 50%;
  overflow: hidden;
  background: #eceff1;

  display: flex;
  justify-content: center;
  align-items: center;
}
.textCtrlL {
  width: 170px;
  height: 100%;
  font-size: 16px;

  display: flex;
  justify-content: center;
  align-items: center;
}
.textCtrlR {
  width: 70px;
  height: 100%;
  font-size: 16px;

  display: flex;
  justify-content: center;
  align-items: center;
}

.textAnime-enter-active {
  transition: opacity 0.5s ease, transform 0.5s ease;
}
.textAnime-leave-active {
  transition: opacity 0.5s ease, transform 0.5s ease;
}
.textAnime-enter-from {
  opacity: 0;
  transform: translateY(100%);
}
.textAnime-enter-to {
  opacity: 1;
  transform: translateY(0%);
}
.textAnime-leave-to {
  opacity: 0;
  transform: translateY(100%);
}
.textAnime-leave-from {
  opacity: 1;
  transform: translateY(0%);
}

</style>
