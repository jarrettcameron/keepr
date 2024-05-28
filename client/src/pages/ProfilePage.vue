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

const account = computed(() => AppState.account)
const profile = computed(() => AppState.activeAccount)
const vaults = computed(() => AppState.activeVaults)
const keeps = computed(() => AppState.keeps)

async function getData() {
    try {
        await vaultsService.getVaultsOfProfile(profileId)
        await keepsService.getKeepsOfProfile(profileId)
        await profilesService.getProfile(profileId)
    }
    catch (error){
        Pop.error("A problem occurred: " + error);
    }
}

onMounted(() => {
    AppState.keeps = null
    AppState.activeAccount = null
    AppState.activeVault = null
    Modal.getInstance('#keepModal')?.hide()
    getData()
})

</script>


<template>
    <div v-if="profile" class="container-fluid mt-md-4 mt-3">
        <div class="row justify-content-center">
            <div class="col-xxl-10 col-sm-11 col-12">
                <div class="row justify-content-center">
                    <div class="col-md-11 col-12">
                        <img :src="profile.coverImg" class="coverImg rounded" :alt="`${profile.name}'s Cover Image'`">
                        <div class="position-relative w-100">
                            <div class="position-absolute w-100 text-center pfpContainer">
                                <img :src="profile.picture" class="pfp mx-auto" height="120" alt="">
                            </div>
                        </div>
                        <div class="text-center mt-5 mb-4 pt-3">
                            <span class="fs-3 fw-bold d-block">{{ profile.name }}</span>
                            <span class="fs-6">{{ vaults?.length }} Vaults | {{ keeps?.length }} Keeps</span>
                        </div>
                    </div>
                </div>
                <div class="row justify-content-center">
                    <div class="col-xl-11 col-12">
                        <div class="row justify-content-center justify-content-lg-start g-0">
                            <h2 class="fw-bold">Vaults</h2>
                            <VaultCard v-if="account?.id == profile.id" />
                            <VaultCard :vault="vault" v-for="vault in vaults" :key="vault.id"/>
                        </div>
                    </div>
                </div>
                <div class="mt-4 mb-5 pb-5 row justify-content-center">
                    <div class="col-xl-11 col-12">
                        <h2 class="fw-bold">Keeps</h2>
                    </div>
                    <div class="col-xl-11 col-12 masonry">
                        <KeepCard v-if="account?.id == profile.id" />
                        <KeepCard v-for="keep in keeps" :key="keep.id" :keep="keep"/>
                    </div>
                </div>
            </div>
        </div>
    </div>
</template>


<style lang="scss" scoped>

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