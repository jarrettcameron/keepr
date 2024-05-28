<script setup>
import { ref } from 'vue';
import Pop from '../utils/Pop';
import { Modal } from 'bootstrap';
import { vaultsService } from '../services/VaultsService';

const formData = ref({
    name: '',
    description: '',
    img: '',
    isPrivate: false
})

async function createVault() {
    try {
        await vaultsService.createVault(formData.value)
        formData.value = {
            name: '',
            description: '',
            img: '',
            isPrivate: false
        }
        Modal.getInstance('#createVaultModal')?.hide()
    }
    catch (error){
      Pop.error("A problem occurred while creating keep.");
    }
}

</script>


<template>
    <div class="modal fade" id="createVaultModal" tabindex="-1"  aria-hidden="true">
        <div class="modal-dialog">
            <div class="modal-content p-5">
                <h3 class="m-0 p-0 mb-2 fw-bold text-grey inter">Add your vault</h3>
                <form @submit.prevent="createVault()">
                    <input v-model="formData.name" type="text" class="form-control" placeholder="Title..." required minlength="1" maxlength="28">
                    <input v-model="formData.img" type="url" class="form-control" placeholder="Image URL..." required minlength="1" maxlength="1000">
                    <textarea v-model="formData.description" class="form-control" name="" id="" cols="30" rows="10" placeholder="Description..." required minlength="1" maxlength="1000"></textarea>
                    <div class="form-check mb-3">
                        <input v-model="formData.isPrivate" class="form-check-input" type="checkbox" id="private">
                        <label class="form-check-label fw-bold" for="private">
                            Make Vault Private?
                        </label>
                        <small class="d-block text-grey">Private vaults can only be seen by you.</small>
                    </div>
                    <div class="text-center">
                        <button class="btn btn-grey px-5">Create Vault</button>
                    </div>
                </form>
            </div>
        </div>
    </div>
</template>


<style lang="scss" scoped>
    .form-control:not(input[type=checkbox]),.form-control:focus {
        margin: 1.7em 0em;
        border: 0;
        outline: none !important;
        border-bottom: 1px solid grey;
        border-radius: 0;
        box-shadow: none;
    }

    textarea {
        max-height: 150px;
    }
</style>