<script setup>
import { Vault } from '../models/Vault';


defineProps({ vault: { type: Vault }})

</script>


<template>
    <router-link v-if="vault" :to="{ name: 'Vault', params: { vaultId: vault.id }}" class="col-lg-4 col-xxl-3 col-sm-6 col-12 my-lg-2 px-lg-2 my-1 px-1 vaultcard">
        <img :src="vault.img" class="rounded vaultImg" onerror="this.src = 'https://user-images.githubusercontent.com/106156/51716184-21e08680-203c-11e9-971c-9b7a384a8f21.jpg'" :alt="`Cover Image of '${vault.name}' Vault`" :title="vault.name">
        <div class="position-relative">
            <div class="position-absolute title text-white fw-bold w-100 p-2 rounded-bottom">
                <div class="d-flex justify-content-between align-items-end">
                    <h5 class="ps-1 quando m-0">{{ vault.name }}</h5>
                    <i v-if="vault.isPrivate" class="pe-1 fs-3 mdi mdi-shield-lock" title="Private"></i>
                </div>
            </div>
        </div>
    </router-link>
    <button v-else class="col-lg-4 col-xxl-3 col-sm-6 col-12 my-lg-2 px-lg-2 my-1 px-1 vaultcard btn border-0 px-0 pt-0" title="Add Your Vault" data-bs-toggle="modal" data-bs-target="#createVaultModal">
        <img src="/src/assets/img/create-2.png" class="rounded vaultImg" onerror="this.src = 'https://user-images.githubusercontent.com/106156/51716184-21e08680-203c-11e9-971c-9b7a384a8f21.jpg'" alt="Graphic of a + sign" title="Add Your Vault">
    </button>
</template>


<style lang="scss" scoped>

.vaultcard {
    user-select: none;
    cursor: pointer;
    transition: transform 0.4s ease-in-out;
}

.vaultcard:hover {
    transform: scale(1.03);
}

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
    height: 185px;
    width: 100%;
    object-fit: cover;
    object-position: center;
    box-shadow: 3px 7px 10px rgba(31, 14, 2, 0.3);
    outline: 1px solid rgba(0, 0, 0, 0.1);
}

</style>