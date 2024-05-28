<script setup>
import { computed, onMounted } from 'vue';
import { AppState } from '../AppState';
import Pop from '../utils/Pop';
import { keepsService } from '../services/KeepsService';

const keeps = computed(() => AppState.keeps)
const account = computed(() => AppState.account)

async function getKeeps() {
    try {
      await keepsService.getKeeps()
    }
    catch (error){
      Pop.error("A problem occurred while fetching feed.");
    }
}

onMounted(() => {
    AppState.activeVault = null
    getKeeps()
})

</script>

<template>
    <div class="container-fluid mt-4">
        <div class="row justify-content-center">
            <div class="col-xxl-8 col-sm-11 col-12">
                <div class="row">
                    <div class="col-12 masonry">
                        <KeepCard v-if="account"/>
                        <KeepCard v-for="keep in keeps" :key="keep.id" :keep="keep"/>
                    </div>
                </div>
            </div>
        </div>
    </div>
</template>

<style scoped lang="scss">
</style>
