<script setup>
import { ref } from 'vue';
import Pop from '../utils/Pop';
import { keepsService } from '../services/KeepsService';
import { Modal } from 'bootstrap';

const formData = ref({
    name: '',
    description: '',
    img: ''
})

async function createKeep() {
    try {
        await keepsService.createKeep(formData.value)
        formData.value = {
    name: '',
    description: '',
    img: ''
}
        Modal.getInstance('#createKeepModal')?.hide()
    }
    catch (error){
      Pop.error("A problem occurred while creating keep.");
    }
}

</script>


<template>
    <div class="modal fade" id="createKeepModal" tabindex="-1"  aria-hidden="true">
        <div class="modal-dialog">
            <div class="modal-content p-5">
                <h3 class="m-0 p-0 mb-2 fw-bold text-grey inter">Add your keep</h3>
                <form @submit.prevent="createKeep()">
                    <input v-model="formData.name" type="text" class="form-control" placeholder="Title..." required minlength="1" maxlength="28">
                    <input v-model="formData.img" type="url" class="form-control" placeholder="Image URL..." required minlength="1" maxlength="1000">
                    <textarea v-model="formData.description" class="form-control" name="" id="" cols="30" rows="10" placeholder="Description..." required minlength="1" maxlength="1000"></textarea>
                    <div class="text-center">
                        <button class="btn btn-grey px-5">Create</button>
                    </div>
                </form>
            </div>
        </div>
    </div>
</template>


<style lang="scss" scoped>
    .form-control,.form-control:focus {
        margin: 1.7em 0em;
        border: 0;
        outline: none !important;
        border-bottom: 1px solid grey;
        border-radius: 0;
        box-shadow: none;
    }
</style>