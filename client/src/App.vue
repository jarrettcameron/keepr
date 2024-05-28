<script setup>
import { AppState } from './AppState'
import Navbar from './components/Navbar.vue'


</script>

<template>
  <header class="sticky-top">
    <Navbar />
  </header>
  <main>
    <router-view />
  </main>
  <KeepModal/>
  <CreateKeepModal/>
  <CreateVaultModal/>
</template>

<style lang="scss">
@import "./assets/scss/main.scss";

:root {
  --main-height: calc(100vh - 32px - 64px);
}
</style>
