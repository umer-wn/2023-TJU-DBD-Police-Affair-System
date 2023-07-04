<!-- TodoItem.vue -->
<template>
    <li>{{ todo.text }}</li>
  </template>
  
  <script>
  export default {
    name: 'TodoItem',
    props: {
      todo: {
        type: Object,
        required: true
      }
    }
  };
  </script>
  
  <style scoped>
  /* 样式代码 */
  </style>
  