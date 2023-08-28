<template>
  <el-header class="sub-header"  @mousemove="handleMouseMove">
    <div>&nbsp;&nbsp;警员管理&nbsp;>&nbsp;警员出勤管理</div>
  </el-header>
</template>

<script>
export default {
  methods: {
    handleMouseMove(event) {
      const x = event.pageX - event.target.offsetLeft;
      const y = event.pageY - event.target.offsetTop;

      event.target.style.setProperty("--x", `${x}px`);
      event.target.style.setProperty("--y", `${y}px`);
    },
  },
};
</script>

<style scoped>
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
</style>
