<template>
  <router-view />
</template>
  
<script setup>
import { Options, Vue } from 'vue-class-component';
import { ref, onMounted } from 'vue';
import { useRouter, useRoute } from 'vue-router';
import axios from 'axios';
const router = useRouter();
const route = useRoute();

//修改路由位置
// router.push('/caseStatistics');
</script>