<template>
    <div>
      <button id="bb" class="both">筛选</button>
    </div>
</template>

<script>
</script>

<style>
  .both {
      position: relative;
      width: 200px;
      height: 64px;
      line-height: 64px;
      box-shadow: inset 0 0 0 3px #000000;
      margin: 50px auto;
      text-align: center;
      color: #000000;
      font-size: 32px;
      cursor: pointer;
      transition: color 1s;
  }

  .both::before,
  .both::after {
      content: "";
      position: absolute;
      width: 0;
      height: 0;
      top: 0;
      left: 0;
      box-sizing: border-box;
      transition: width .5s, height .5s;
      transition-delay: .5s, 0s;
      border: 3px solid transparent;
  }

  .both:hover {
      color: #00e2ff;
      &::before {
          transition: width .5s, height .5s, border-bottom-color 0s;
          transition-delay: .5s, 0s, .5s;
          width: 200px;
          height: 64px;
          border-left: 3px solid #00e2ff;
          border-bottom: 3px solid #00e2ff;
      }
      &::after {
          transition: width .5s, height .5s, border-right-color .5s;
          transition-delay: 0s, .5s, .5s;
          width: 200px;
          height: 64px;
          border-top: 3px solid #00e2ff;
          border-right: 3px solid #00e2ff;
      }
  }
</style>
