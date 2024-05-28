<script setup>
import { Modal } from 'bootstrap';
import { Keep } from '../models/Keep';
import { keepsService } from '../services/KeepsService';
import Pop from '../utils/Pop';


defineProps({keep: {type: Keep}})

async function setActive(keep) {
    try {
        await keepsService.setActive(keep)
        Modal.getOrCreateInstance('#keepModal').show()
    } catch (error) {
        Pop.error(error)
    }
    
}

</script>


<template>
    <div v-if="keep" class="keepcard">
        <img @click="setActive(keep)" class="imgshadow rounded" :src="keep.img" onerror="this.src = 'https://user-images.githubusercontent.com/106156/51716184-21e08680-203c-11e9-971c-9b7a384a8f21.jpg'" :alt="keep.name" :title="keep.name">
        <div class="position-relative">
            <div class="position-absolute title text-white fw-bold w-100 p-2 rounded-bottom">
                <div class="d-flex justify-content-between align-items-end">
                    <h5 @click="setActive(keep)" class="marko m-0">{{ keep.name }}</h5>
                    <router-link :to="{ name: 'Profile', params: { profileId: keep.creatorId }}">
                        <img :src="keep.creator.picture" height="35" class="pfp" :alt="keep.creator.name" :title="keep.creator.name">
                    </router-link>
                </div>
            </div>
        </div>
    </div>
    <button v-else class="btn border-0 px-0 pt-0 keepcard vt" title="Add Your Keep" data-bs-toggle="modal" data-bs-target="#createKeepModal">
        <img class="imgshadow rounded" src="/src/assets/img/create.png" onerror="this.src = 'https://user-images.githubusercontent.com/106156/51716184-21e08680-203c-11e9-971c-9b7a384a8f21.jpg'" alt="">
    </button>
</template>


<style lang="scss" scoped>
.imgshadow {
    box-shadow: 3px 7px 10px rgba(31, 14, 2, 0.3);
    outline: 1px solid rgba(0, 0, 0, 0.1);
}

.title2 {
    bottom: -0em;
    padding-left: 0.5em;
}

.title {
    bottom: -0em;
    padding-left: 0.5em;
    background: linear-gradient(transparent, rgba(0, 0, 0, 0.5));
}

.keepcard {
    user-select: none;
    cursor: pointer;
    transition: transform 0.4s ease-in-out;
    padding-bottom: 1em;

    >img {
        width: 100%;
        min-height: 80px !important;
    }
}

.keepcard:hover {
    transform: scale(1.03);
}

@media screen and (max-width: 768px) {
    .keepcard {
        padding-bottom: 0.5em;
    }
}
</style>