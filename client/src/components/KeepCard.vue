<script setup>
import { Modal } from 'bootstrap';
import { Keep } from '../models/Keep';
import { keepsService } from '../services/KeepsService';


defineProps({keep: {type: Keep, required: true}})

async function setActive(keep) {
    await keepsService.setActive(keep)
    Modal.getOrCreateInstance('#keepModal').show()
}

</script>


<template>
    <div class="keepcard" @click="setActive(keep)">
        <img class="imgshadow rounded" :src="keep.img" onerror="this.src = 'https://user-images.githubusercontent.com/106156/51716184-21e08680-203c-11e9-971c-9b7a384a8f21.jpg'" alt="">
        <div class="position-relative">
            <div class="position-absolute title text-white fw-bold w-100 p-2 rounded-bottom">
                <div class="d-flex justify-content-between align-items-end">
                    <h5 class="marko m-0">{{ keep.name }}</h5>
                    <img :src="keep.creator.picture" height="35" class="pfp" alt="">
                </div>
            </div>
        </div>
    </div>
</template>


<style lang="scss" scoped>
.imgshadow {
    box-shadow: 3px 7px 10px rgba(31, 14, 2, 0.3);
    outline: 1px solid rgba(0, 0, 0, 0.1);
}

.title {
    bottom: -0em;
    padding-left: 0.5em;
    background: linear-gradient(transparent, rgba(0, 0, 0, 0.5));
}

.keepcard {
    cursor: pointer;
    transition: transform 0.4s ease-in-out;
    padding-bottom: 1em;

    >img {
        width: 100%;
        object-fit: container;
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