<script setup>
import { computed, onMounted } from 'vue';
import { AppState } from '../AppState';
import Pop from '../utils/Pop';
import { keepsService } from '../services/KeepsService';
import { Modal } from 'bootstrap';

const keeps = computed(() => AppState.keeps)

async function getKeeps() {
    try {
      await keepsService.getKeeps()
    }
    catch (error){
      Pop.error(error);
    }
}

onMounted(() => {
    getKeeps()
})

</script>

<template>
    <div class="container-fluid mt-3">
        <div class="row justify-content-center">
            <div class="col-xxl-8 col-lg-10 col-md-10 col-sm-11 col-12">
                <div class="row">
                    <div class="col-12 masonry">
                        <KeepCard v-for="keep in keeps" :key="keep.id" :keep="keep"/>
                    </div>
                </div>
            </div>
        </div>
    </div>
</template>

<style scoped lang="scss">

.masonry {
    column-count: 4;
    column-gap: 1em;
    width: 100%;
}

@media screen and (max-width: 768px) {
    .masonry {
        column-count: 2;
        column-gap: 0.5em;
        >* {
            padding-bottom: 0.5em;
        }
    }
}

</style>
