<script setup>
import { computed, onMounted } from 'vue';
import { useRoute } from 'vue-router';
import Pop from '../utils/Pop';
import { vaultsService } from '../services/VaultsService';
import { AppState } from '../AppState';
import { router } from '../router';
import { keepsService } from '../services/KeepsService';

const route = useRoute()
const vaultId = route.params.vaultId

const vault = computed(() => AppState.activeVault)
const keeps = computed(() => AppState.keeps)
const account = computed(() => AppState.account)

async function getVault() {
    try {
      await vaultsService.getVaultById(vaultId)
    }
    catch (error) {
        if (error.response.data.includes('FORBIDDEN')) {
            Pop.error("Cannot access private vault.")
            await router.push({ name: 'Home' })
        } else {
            Pop.error("A problem occurred while fetching vault data.");
        }
    }
}

async function destroyVault() {
    try {
        const conf = await Pop.confirm('Are you sure you want to delete this vault?')
        if (!conf) return
        await vaultsService.destroyVault(vault.value?.id)
    }
    catch (error){
      Pop.error(error);
    }
}

async function getData() {
    try {
      await keepsService.getKeepsFromVault(vaultId)
    }
    catch (error){
      Pop.error("A problem occurred while fetching keeps from vault.");
    }
}

onMounted(() => {
    getVault()
    getData()
})

</script>


<template>
    <div v-if="vault && keeps" class="container-fluid mt-md-4 mt-3">
        <div class="row justify-content-center">
            <div class="col-xxl-8 col-sm-11 col-12">
                <div class="row justify-content-center">
                    <div class="col-md-11 col-12 text-center">
                        <img :src="vault.img" onerror="this.src = 'https://user-images.githubusercontent.com/106156/51716184-21e08680-203c-11e9-971c-9b7a384a8f21.jpg'" class="vaultImg rounded" alt="">
                        <div class="mx-auto position-relative h-100 vaultImg rounded us-none">
                            <div class="position-absolute w-100 h-100 overlay rounded d-flex justify-content-end align-items-center flex-column text-white">
                                <h3 class="mb-0 quando stretched text-shadow">{{ vault.name }}</h3>
                                <p class="mb-3 quando fs-5 text-shadow">by {{ vault.creator.name }}</p>
                            </div>
                        </div>
                    </div>
                </div>
                <div class="mt-3 row justify-content-center">
                    <div class="col-md-11 col-12 text-center">
                        <span class="inter fw-bold fs-5">
                            Description
                            <div class="dropdown d-inline" v-if="vault.creatorId == account?.id">
                                <button class="d-inline btn border-0 m-0 p-0" type="button" data-bs-toggle="dropdown" aria-expanded="false" aria-label="Vault Settings">
                                    <i class="mdi mdi-dots-horizontal"></i>
                                </button>
                                <ul class="dropdown-menu">
                                    <li><button @click="destroyVault" class="btn text-danger border-0"><i class="mdi mdi-delete pe-1"></i>Delete Vault</button></li>
                                </ul>
                            </div>
                            <br>
                            <span class="text-grey fs-6 fw-normal fst-italic">{{ vault.description }}</span>
                        </span>
                        <p class="mt-3">{{ keeps.length }} Keeps</p>
                    </div>
                </div>

                <div class="row">
                    <div class="col-12 masonry">
                        <KeepCard v-for="keep in keeps" :keep="keep" :key="keep.id"/>
                    </div>
                </div>
            </div>
        </div>
    </div>
</template>


<style lang="scss" scoped>

.stretched {
    text-transform: uppercase;
    letter-spacing: 0.2em;
}

.text-shadow {
    text-shadow: 0px 0px 7px rgba(31, 14, 2, 0.5);
}

.vaultImg:not(.position-relative) {
    object-fit: cover;
    object-position: center;
    box-shadow: 3px 7px 10px rgba(31, 14, 2, 0.3);
    outline: 1px solid rgba(0, 0, 0, 0.1);
}

.vaultImg {
    width: 100%;
    max-width: 400px;
    max-height: 20vh;
}

.overlay {
    background: linear-gradient(90deg, transparent, rgba(0, 0, 0, 0.1) 30%, rgba(0, 0, 0, 0.1) 70%, transparent);
    bottom: 20vh;
}

</style>