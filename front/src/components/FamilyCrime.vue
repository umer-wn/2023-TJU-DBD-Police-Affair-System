<template>
    <div class="container">
        <p>Family Crime</p>
	<CriminalInfo :imgUrl="imageUrl" class="imgControl"/>

    </div>
</template>



<script>
import CriminalInfo from './CriminalInfo.vue'
export default{
    components:{
        CriminalInfo
    },
    data(){
        return{
            imageUrl:require('@/assets/criminal.png')
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
    margin-top: 100px;
    margin-left: 300px;
    width: 100%;
    height: 100%;
}


</style>
