<script setup>
import { onMounted, ref } from 'vue';
import { loadState, saveState } from '../utils/Store.js';
import Login from './Login.vue';

const theme = ref('light')

onMounted(() => {
  document.documentElement.setAttribute('data-bs-theme', theme.value)
})

</script>

<template>
  <nav class="navbar navbar-expand-sm navbar-dark bg-white px-3 d-flex justify-content-between">
    <div></div>
    <router-link class="navbar-brand d-flex flex-grow-1 justify-content-center me-0" :to="{ name: 'Home' }">
      <div class="d-flex flex-column align-items-center">
        <div class="keeprLogo marko">
            the<br>keepr<br>co.
        </div>
      </div>
    </router-link>
    <div class="position-relative d-sm-none">
        <div class="position-absolute end-0" style="bottom: -1em;">
            <button class="navbar-toggler text-dark p-1 m-0 me-1" type="button" data-bs-toggle="collapse" data-bs-target="#navbarText"
                aria-controls="navbarText" aria-expanded="false" aria-label="Toggle navigation">
                <i class="mdi mdi-menu"></i>
            </button>
        </div>
    </div>
    <div class="collapse navbar-collapse flex-grow-0 justify-content-end position-relative IHateCSS" id="navbarText">
        <div class="position-absolute end-0" style="bottom: -1.25em;">
            <Login />
        </div>
    </div>
  </nav>
</template>

<style scoped>
.keeprLogo {
    aspect-ratio: 1.1/1;
    width: fit-content;
    padding: 0.27em;
    border-radius: 0.35em;
    color: var(--bs-secondary);
    border: 2px solid var(--bs-secondary);
    line-height: 1em;
    font-size: 0.89em;
}

a:hover {
    text-decoration: none;
}

.nav-link {
    text-transform: uppercase;
}

.navbar-nav .router-link-exact-active {
    border-bottom: 2px solid var(--bs-success);
    border-bottom-left-radius: 0;
    border-bottom-right-radius: 0;
}

@media screen and (min-width: 576px) {
    nav {
        height: 84px;
    }
}

@media screen and (max-width: 577px) {
    .IHateCSS {
        position: unset !important;

        * {
            position: unset !important;
            bottom: unset !important;
        }
    }
}
</style>
