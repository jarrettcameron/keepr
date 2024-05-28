<script setup>
import { computed, ref, watch } from 'vue';
import { AppState } from '../AppState.js';
import Pop from '../utils/Pop.js';
import { accountService } from '../services/AccountService.js';

const account = computed(() => AppState.account)
const formData = ref({
    email: account.value?.email,
    name: account.value?.name,
    picture: account.value?.picture,
    coverImg: account.value?.coverImg
})

async function editAccount() {
    try {
        await accountService.editAccount(formData.value)
        Pop.success("Updated profile.")
    }
    catch (error){
      Pop.error("A problem occurred while updating account info.");
    }
}

watch(account, (nv) => {
    if (nv) {
        formData.value.email = account.value?.email
        formData.value.name = account.value?.name
        formData.value.picture = account.value?.picture
        formData.value.coverImg = account.value?.coverImg
    }
})

</script>

<template>
  <div class="container-fluid mt-4">
        <div class="row justify-content-center">
            <div class="col-xxl-8 col-sm-11 col-12">
                <div class="row justify-content-center">
                    <div class="col-xl-6 col-md-7 col-11 text-center">
                        <img :src="account?.picture" class="pfp mb-3" height="100" alt="">
                        <form @submit.prevent="editAccount()" class="text-start mt-2">
                            <label for="name">Name</label>
                            <input id="name" v-model="formData.name" class="form-control" type="text" required maxlength="255">
                            <label for="email">Email</label>
                            <input id="email" v-model="formData.email" class="form-control" type="text" required maxlength="255">
                            <label for="picture">Profile Picture</label>
                            <input id="picture" v-model="formData.picture" class="form-control" type="url" required maxlength="255">
                            <label for="coverimg">Cover Image</label>
                            <input id="coverimg" v-model="formData.coverImg" class="form-control" type="url" required maxlength="1000">
                            <div class="text-center">
                                <button class="btn btn-grey px-5 mt-4">Save Changes</button>
                            </div>
                        </form>
                    </div>
                </div>
            </div>
        </div>
    </div>
</template>

<style scoped lang="scss">
label {
    padding: 0em 0.5em;
    margin-top: 1em;
}

.form-control:not(input[type=checkbox]),.form-control:focus {
        margin: 0em 0em;
        border: 0;
        outline: none !important;
        border-bottom: 1px solid grey;
        border-radius: 0;
        box-shadow: none;
    }
</style>
