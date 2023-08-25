<template>
    <div class="RangeSearch">
      <el-select
        v-model="employeesData.transshipmentdepot"
        multiple
        filterable
        remote
        reserve-keyword
        :placeholder="$t(placeholder)"
        :remote-method="transshipmentdepotremoteMethod"
        :focus="transshipmentdepotremoteMethod"
        :loading="loading">
        <el-option
          v-for="item in options"
          :key="item.value"
          :label="item.label"
          :value="item.value">
        </el-option>
      </el-select>
    </div>
  </template>
  
  <script>
  export default {
    data(){
      return{
        options: [],
        value: [],
        list: [],
        loading: false,
        employeesData:{
          transshipmentdepot:'',
        },
        data:[]
      }
    },
    props:{
      placeholder:{
        default(){
          return [];
        }
      },
      SelectData:{
        default(){
          return [];
        }
      }
    },
    methods:{
      transshipmentdepotremoteMethod(query) {
        if (query !== '') {
          this.loading = true;
          setTimeout(() => {
            this.loading = false;
            this.options = this.list.filter(item => {
              return item.label.toLowerCase()
                .indexOf(query.toLowerCase()) > -1;
            });
          }, 200);
        } else {
          this.options = []
        }
      }
    },
    watch:{
      SelectData:function(newVal){
        this.options = newVal
        this.list = newVal
      }
    },
  }
  </script>
  
  <style>
  
  </style>