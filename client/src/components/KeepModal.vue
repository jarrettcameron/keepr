<script setup>
import { computed, ref, watch } from 'vue';
import { AppState } from '../AppState';
import { FastAverageColor } from 'fast-average-color';
import Pop from '../utils/Pop';
import { vaultKeepsService } from '../services/VaultKeepsService';
import { keepsService } from '../services/KeepsService';

const fac = new FastAverageColor()
const keep = computed(() => AppState.activeKeep);
const myVaults = computed(() => AppState.myVaults);
const account = computed(() => AppState.account);
const activeVault = computed(() => AppState.activeVault)
let avgColor = ref('#000')

const formData = ref({
    vaultId: '',
    keepId: ''
})

async function destroyVaultKeep() {
    try {
        const conf = await Pop.confirm("Are you sure you want to remove this keep from your vault?")
        if (!conf) return
        await vaultKeepsService.destroyVaultKeep(keep.value)
    }
    catch (error){
      Pop.error("A problem occurred while removing keep from vault.")
    }
}

async function destroyKeep() {
    try {
        const conf = await Pop.confirm("Are you sure you want to delete this keep?")
        if (!conf) return
        await keepsService.destroyKeep(keep.value.id)
    }
    catch (error){
      Pop.error("A problem occurred while deleting keep.");
    }
}

async function createVaultKeep() {
    try {
        if (formData.value.vaultId == '') {
            Pop.error("You must select a vault to contribute to.")
            return
        }
      await vaultKeepsService.createVaultKeep(formData.value)
      Pop.success("Added keep to vault.")
    }
    catch (error){
      Pop.error("A problem occurred while adding keep to vault.");
    }
}

watch(keep, async(nv) => {
    if (nv != null && nv?.img != null) {
        try {
            formData.value.vaultId = ""
            formData.value.keepId = nv.id
            const color = await fac.getColorAsync(nv.img);
            avgColor.value = color.hex
        } catch (error) {
            //
        }
        
    }
})

</script>


<template>
<div v-if="keep" class="modal fade" id="keepModal" aria-hidden="true" aria-labelledby="exampleModalToggleLabel" tabindex="-1">
  <div class="modal-dialog modal-dialog-centered">
    <div class="modal-content d-flex w-100 p-0 flex-lg-row flex-column">
        <img id="keepImg" :src="keep.img" onerror="this.src = 'https://user-images.githubusercontent.com/106156/51716184-21e08680-203c-11e9-971c-9b7a384a8f21.jpg'" class="keepImg p-0" :alt="keep.name" :title="keep.name">
        <div class="bg-white special-round p-md-4 py-3 w-100">
            <div class="container-fluid h-100">
                <div class="row align-content-between h-100">
                    <div class="col-12 d-flex justify-content-center gap-5 inter pb-4 us-none">
                        <div class="d-flex align-items-center gap-2 text-grey">
                            <i class="mdi mdi-eye-outline"></i>
                            <span>{{ keep.views }}</span>
                        </div>
                        <div class="d-flex align-items-center gap-2 text-grey">
                            <span class="marko small-logo text-center">k</span>
                            <span>{{ keep.kept }}</span>
                        </div>
                    </div>
                    <div class="col-12 text-center pb-4">
                        <h4 class="marko">{{ keep.name }}</h4>
                        <p class="text-grey inter text-md-start">{{ keep.description }}</p>
                    </div>
                    <div class="col-12 d-flex justify-content-between align-items-center">
                        <div>
                            <form @submit.prevent="createVaultKeep()" v-if="account && keep.vaultKeepId == ''" class="d-flex gap-1 align-items-center">
                                <select v-model="formData.vaultId" class="form-control inter text-truncate" required>
                                    <option v-for="vault in myVaults" :key="vault.id" :value="vault.id">{{ vault.name }}</option>
                                    <option disabled selected value="">Add to Vault</option>
                                </select>
                                <button class="btn p-0 m-0 border-0"><i class="fs-4 text-avgColor mdi mdi-plus"></i></button>
                            </form>
                            <div v-if="account && keep.vaultKeepId != '' && activeVault?.creatorId == account?.id">
                                <button @click="destroyVaultKeep()" class="btn text-avgColor"><i class="mdi mdi-delete"></i> Remove</button>
                            </div>
                        </div>
                        <div v-if="account?.id == keep.creatorId && !activeVault">
                            <button @click="destroyKeep()" class="btn border-0 m-0 p-0 fs-5 text-avgColor" title="Delete Keep"><i class="mdi mdi-delete"></i></button>
                        </div>
                        <router-link :to="{ name: 'Profile', params: { profileId: keep.creatorId ?? 'none' }}" class="d-flex align-items-center gap-2 us-none">
                            <img :src="keep.creator.picture" class="pfp" height="35" :alt="keep.creator.name" :title="keep.creator.name">
                            <span class="text-black fw-bold">{{ keep.creator.name }}</span>
                        </router-link>
                    </div>
                </div>
            </div>
        </div>
    </div>
  </div>
</div>
</template>


<style lang="scss" scoped>

i {
    line-height: 0em;
    padding: 0;
    margin: 0;
}

.pfp {
    border: 2px solid;
    border-color: v-bind(avgColor);
}

.text-avgColor {
    color: v-bind(avgColor);
}

select,select:focus {
    outline: none !important;
    border: none;
    border-radius: 0;
    box-shadow: none;
    font-weight: 600;
    letter-spacing: 0.15em;
    border-bottom: 3px solid v-bind(avgColor);
    padding: 0.2em 0.7em;
    max-width: 160px;
}

.modal-content {
    background-color: unset;
}

.small-logo {
    aspect-ratio: 2.7/1;
    padding: 0.2em;
    border-radius: 0.35em;
    color: var(--bs-grey);
    border: 1px solid var(--bs-grey);
    line-height: 1em;
}

.special-round {
    border-top-right-radius: 0.375rem;
    border-bottom-right-radius: 0.375rem;
}

.modal-dialog {
    max-width: 950px;
}

.keepImg {
    border-top-left-radius: 0.375rem;
    border-bottom-left-radius: 0.375rem;
    object-fit: cover;
    min-width: 460px;
    height: 80vh;
    max-height: 500px;
}

@media screen and (max-width: 992px) {
    .special-round {
        border-top-right-radius: 0rem;
        border-bottom-right-radius: 0.375rem;
        border-bottom-left-radius: 0.375rem;
    }

    .modal-dialog {
        max-width: 85vw;
    }

    .keepImg {
        border-top-left-radius: 0.375rem;
        border-top-right-radius: 0.375rem;
        border-bottom-left-radius: 0rem;
        width: 100%;
        min-width: 100%;
        max-width: 100%;
    }
}

@media screen and (max-width: 576px) {
    .modal-dialog {
        max-width: 100vw;
    }
}

</style>