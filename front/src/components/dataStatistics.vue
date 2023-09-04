<template>
    <main class="main">
      <div class="headpic">
        <div class="header1">
          <img class="logo" src="@/assets/image-2.png" />
          <div class="title">&nbsp;&nbsp;警务处理系统</div>
        </div>
        <div class="content1">
          <div class="headbar" @mousemove="handleMouseMove">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;数据统计</div>
        </div>  
        <div class="footer1">返回首页</div>  
        <svg class="arrow" width="20" height="22" viewBox="0 0 20 22" fill="none" xmlns="http://www.w3.org/2000/svg"  @click="goToSearchWagesRecord">
          <line y1="-2.5" x2="20.4742" y2="-2.5" transform="matrix(-0.814031 0.580822 -0.814031 -0.580822 16.6667 0)" stroke="white" stroke-width="5" />
          <line y1="-2.5" x2="20.4742" y2="-2.5" transform="matrix(-0.814031 -0.580822 0.814031 -0.580822 20 22)" stroke="white" stroke-width="5" />
        </svg>
      </div>

      <section>     
        <div style="width:100%">
          <button class="ssqbutton1-2" @mousemove="handleMouseMove" style="width:49%;display:inline-block" @click="goToSearchWagesRecord"><span>记录搜索</span></button>
          <button class="ssqbutton1-1" @mousemove="handleMouseMove" style="width:49%;display:inline-block"><span>数据统计</span></button>
        </div>
        <lable>请输入信息以统计数据</lable>
        
    </section>
    <footer>
      <p>&copy; 2018 LHammer</p>
      <p>CSS Tricks need to know for web developer.</p>
    </footer>
  </main>
</template>
</template>

<script>
  import axios from 'axios';
  export default {
    data() {
      return {
        inputId: '',
        inputRelated: '',
        inputAmount: '',
        boxContent: ''
      };
    },
    methods: {
      submitSearch() {
        console.log(this.inputId, this.inputRelated, this.inputAmount);
        axios.post('http://localhost:7078/api/query', { inputId: this.inputId, inputRelated: this.inputRelated, inputAmount: this.inputAmount })
          .then(response => {
            // 请求成功的处理逻辑
            this.boxContent = response.data.inputText2;
            console.log(response.data);
          })
          .catch(error => {
            // 请求失败的处理逻辑
            console.error(error);
          });
      },
      handleMouseMove(event) {
        const x = event.pageX - event.target.offsetLeft;
        const y = event.pageY - event.target.offsetTop;
  
        event.target.style.setProperty('--x', `${x}px`);
        event.target.style.setProperty('--y', `${y}px`);
      },
      goToSearchWagesRecord() {
        this.$router.push('/');
      },
    }
  };
  </script>
  
  
  
<style>
  main{
      width: 100%;
      min-width: 800px;
    }
    h2.title {
      color: white;
      margin-top: 1em;
      margin-bottom: 1em;
    }
    header{
      background: #fd018b;
      color: white;
      text-align: left;
    }
    footer{
      background: rgba(180,160,120,.05);
    }
    .main > header,
    .main > section,
    .main > footer{
        padding: .1em calc(50% - 329px);
        text-align: justify;
        hyphens: auto;
    }
  .titletext1{inputinfobox
    width: 100px;
  }
  .titletext2{
    width: 100px;
    background: #b7b3b3;
  }
  .box {
    width: 700px;
    height: 200px;
    border: 1px solid #000;
    padding: 10px;
  }
  .ssqinputinfobox{
    position: relative;
    width: 160px;
    display: inline-block;
  }
  .ssqinputtext{
    width: 100px;
    display: inline-block;
  }
  
  input {
      display: block;
      width: 229px;
      padding: .8em;
      outline: none;
      border: 1px solid #e3e3e3;
      border-radius: 2px;
    }
    input:focus,
    input:hover {
      border-color: #b4a078;
    }
    input:not(:placeholder-shown) {
      border-color: #be4141;
      box-shadow: 0 0 0 2px rgba(255, 100, 97, 0.2);
    }
    input:not(:placeholder-shown) + .poptip {
      color: #be4141;
    }
    input:valid {
      border-color: #b4a078;
      box-shadow: 0 0 0 2px rgba(180, 160, 120, 0.2);
    }
    input:valid + .poptip {
      color: unset;
    }
    input:not(:focus) + .poptip {
      transform: scale(0);
      animation: elastic-dec .25s;
    }
  
    input:focus + .poptip {
      transform: scale(1);
      animation: elastic-grow .45s;
    }
    .ssqpoptip {
      display: inline-block;
      width: 200px;
      font-size: 13px;
      padding: .6em;
      background: #fafafa;
      position: relative;
      margin-left: -3px;
      margin-top: 3px;
      border-radius: 2px;
      filter: drop-shadow(0 0 1px rgba(0, 0, 0, .23456));
      transform-origin: 15px -6px;
    }
    .ssqpoptip::before {
      content: "";
      position: absolute;
      top: 50%;
      left: -10px;
      border: 9px solid transparent;
      border-bottom-color: #fafafa;
      border-top-width: 0;
      padding: 3px;
    }
    @keyframes elastic-grow {
      from {
          transform: scale(0);
      }
      70% {
          transform: scale(1.1);
          animation-timing-function: cubic-bezier(.1, .25, .1, .25);
      }
    }
    @keyframes elastic-dec {
      from {
          transform: scale(1);
      }
      to {
          transform: scale(0);
          animation-timing-function: cubic-bezier(.25, .1, .25, .1);
      }
    }
  
  * {
      box-sizing: border-box;
  }
  
  .g-table {
      margin: 20px auto;
      width: 450px;
  }
  
  table {
      position: relative;
      width: 450px;
      border: 1px solid #ccc;
      text-align: center;
      
      tbody {
          border-collapse: separate;
          height: 200px;
      }
      
      td, th {
          width: 150px;
          padding: 5px;
          border: 1px solid #ccc;
      }
  }
  .sec{
      min-width: 800px;
    }
  .g-scroll {
      top: -1px;
      position: relative;
      height: 200px;
      overflow-y: scroll;
      overflow-x: hidden;
      background: 
          linear-gradient(#fff, transparent) top / 100% 100px,
          radial-gradient(at 50% -15px, rgba(0, 0, 0, .8), transparent 70%) top / 100000% 12px;
      background-repeat: no-repeat;
      background-attachment: local, scroll;
  }
  
    
  .ssqbutton1 {
      margin:10px auto;
      width:150px;
      height:40px;
      padding:0 30px;
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
  
    .button1::before {
      --size: 0;
      content: '';
      position: absolute;
      left: var(--x);
      top: var(--y);
      width: var(--size);
      height: var(--size);
      background: radial-gradient(circle closest-side, #0b72bbb2, transparent);
      transform: translate(-50%, -50%);
      transition: width .2s ease, height .2s ease;
    }
  
    .ssqbutton1:hover::before {
      --size: 400px;
    }

    .headpic {
    background: #ffffff;
    width: 100%;
    position: relative;
    overflow: hidden;
  }
  
  .header1 {
    background: #0b71bb;
    width: 100%;
    height: 70px;
    position: relative;

    left: 0;
    display: flex;
    align-items: center;
    padding-left: 20px;
  }
  
  .title {
    color: #ffffff;
    text-align: left;
    font: 400 36px "Inter", sans-serif;
    display: inline-block;
  }
  
  .content1 {
    width: 100%;
    height: 38px;
    position: relative;

    display: flex;
    align-items: center;
    padding-left: 20px;
    background: #dfd8c8;
    box-shadow: inset 200px 0px 50px 0px rgba(211, 194, 177, 0.25);
  }
  
  .subtitle {
    color: #ffffff;
    text-align: left;
    display:relative;
    left:80px;
    font: 400 28px "Inter", sans-serif;
  }
  
  .footer1 {
    color: #ffffff;
    text-align: left;
    font: 400 16px "Inter", sans-serif;
    position: absolute;
    top: 42px;
    right: 20px;
  }
  
  .arrow {
    position: absolute;
    top: 77px;
    left: 16px;
    overflow: visible;
    animation: stroke 0.5s infinite alternate;
  }
  .arrow:hover {
    stroke: $pink; /* 设置悬停时的颜色，这里以红色为例 */
  }
  
  .logo {
    width: 70px;
    height: 70px;
    position: relative;
    top: 2px;
    left: 0px;
  }
    
  .headbar {
    color: #ffffff;
    text-align: left;
    display:relative;
    left:80px;
    width: 100%;
    font: 400 28px "Inter", sans-serif;
  }

  .headbar span {
    position: relative;
  }

  .headbar::before {
    --size: 0;
    content: '';
    position: absolute;
    left: var(--x);
    top: var(--y);
    width: var(--size);
    height: var(--size);
    background: radial-gradient(circle closest-side, #e1f2ff, transparent);
    transform: translate(-50%, -50%);
    transition: width .2s ease, height .2s ease;
  }

  .headbar:hover::before {
    --size: 400px;
  }




  </style>