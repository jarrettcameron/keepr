<script setup>
import { computed, onMounted } from 'vue';
import { useRoute } from 'vue-router';
import { AppState } from '../AppState';
import Pop from '../utils/Pop';
import { profilesService } from '../services/ProfilesService';
import { Modal } from 'bootstrap';
import { vaultsService } from '../services/VaultsService';
import { keepsService } from '../services/KeepsService';

const route = useRoute()
const profileId = route.params.profileId

const account = computed(() => AppState.activeAccount)
const vaults = computed(() => AppState.activeVaults)
const keeps = computed(() => AppState.keeps)

async function getData() {
    try {
        await vaultsService.getVaultsOfProfile(profileId)
        await keepsService.getKeepsOfProfile(profileId)
        await profilesService.getProfile(profileId)
    }
    catch (error){
        Pop.error(error);
    }
}

onMounted(() => {
    AppState.keeps = null
    AppState.activeAccount = null
    Modal.getInstance('#keepModal')?.hide()
    getData()
})

</script>


<template>
    <div v-if="account" class="container-fluid mt-md-4 mt-3">
        <div class="row justify-content-center">
            <div class="col-xxl-10 col-lg-9 col-sm-11 col-12">
                <div class="row justify-content-center">
                    <div class="col-md-11 col-12">
                        <img :src="account.coverImg" class="coverImg rounded">
                        <div class="position-relative w-100">
                            <div class="position-absolute w-100 text-center pfpContainer">
                                <img :src="account.picture" class="pfp mx-auto" height="120" alt="">
                            </div>
                        </div>
                        <div class="text-center mt-5 mb-4 pt-3">
                            <span class="fs-3 fw-bold d-block">{{ account.name }}</span>
                            <span class="fs-6">{{ vaults.length }} Vaults | {{ keeps.length }} Keeps</span>
                        </div>
                    </div>
                </div>
                <div class="row justify-content-center">
                    <div class="col-md-11 col-12">
                        <div class="row justify-content-center justify-content-lg-start g-0">
                            <h2 class="fw-bold">Vaults</h2>
                            <div class="col-lg-4 col-xxl-3 col-6 my-lg-2 px-lg-2 my-1 px-1" v-for="vault in vaults" :key="vault.id">
                                <img :src="vault.img" class="rounded vaultImg" onerror="this.src = 'https://user-images.githubusercontent.com/106156/51716184-21e08680-203c-11e9-971c-9b7a384a8f21.jpg'" :alt="vault.name" :title="vault.name">
                                <div class="position-relative">
                                    <div class="position-absolute title text-white fw-bold w-100 p-2 rounded-bottom">
                                        <div class="d-flex justify-content-between align-items-end">
                                            <h5 class="ps-1 quando m-0">{{ vault.name }}</h5>
                                            <i v-if="vault.isPrivate" class="pe-1 fs-3 mdi mdi-shield-lock" title="Private"></i>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
                <div class="mt-4 row justify-content-center">
                    <div class="col-md-11 col-12">
                        <h2 class="fw-bold">Vaults</h2>
                    </div>
                    <div class="col-md-11 col-12 masonry">
                        <KeepCard v-for="keep in keeps" :key="keep.id" :keep="keep"/>
                    </div>
                </div>
            </div>
        </div>
    </div>
</template>


<style lang="scss" scoped>

i {
    line-height: 0.9em;
}

.title {
    bottom: -0em;
    background: linear-gradient(transparent, rgba(0, 0, 0, 0.5));
    text-transform: uppercase;
    letter-spacing: 0.2em;
}

.vaultImg {
    max-height: 190px;
    width: 100%;
    object-fit: cover;
    object-position: center;
    box-shadow: 3px 7px 10px rgba(31, 14, 2, 0.3);
    outline: 1px solid rgba(0, 0, 0, 0.1);
}

.coverImg {
    width: 100%;
    max-height: 30vh;
    object-fit: cover;
    object-position: center;
    box-shadow: 0px 0px 25px rgba(0, 0, 0, 0.3);
}

.pfp {
    border: 0;
}

.pfpContainer {
    bottom: -60px;
}

</style>