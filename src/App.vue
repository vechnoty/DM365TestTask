<template>
  <div id="app">
    <h1>Задачи</h1>
    <TaskGrid />
  </div>
</template>

<script setup lang="ts">
import TaskGrid from './components/TaskGrid.vue';
</script>

<style>
#app {
  padding: 20px;
  font-family: 'Segoe UI', sans-serif;
}
</style>
