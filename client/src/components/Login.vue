<script setup>
import { computed } from 'vue';
import { AppState } from '../AppState';
import { AuthService } from '../services/AuthService';

const identity = computed(() => AppState.identity)
const account = computed(() => AppState.account)
async function login() {
  AuthService.loginWithPopup()
}
async function logout() {
  AuthService.logout()
}

</script>

<template>
  <span class="navbar-text">
    <button class="btn selectable text-dark lighten-30 text-uppercase fw-bold" @click="login" v-if="!identity">
      Login
    </button>
    <div v-else>
      <div class="dropdown my-2 my-lg-0">
        <div type="button" class="border-0 selectable no-select" data-bs-toggle="dropdown"
          aria-expanded="false">
          <div v-if="account?.picture || identity?.picture">
            <img :src="account?.picture || identity?.picture" alt="account photo" height="40" class="pfp" />
          </div>
        </div>
        <div class="dropdown-menu dropdown-menu-sm-end dropdown-menu-start p-0" aria-labelledby="authDropdown">
          <div v-if="account" class="list-group">
            <router-link :to="{ name: 'Profile', params: { profileId: account.id } }">
              <div class="list-group-item dropdown-item list-group-item-action selectable">
                My Profile
              </div>
            </router-link>
            <router-link :to="{ name: 'Account' }">
              <div class="list-group-item dropdown-item list-group-item-action selectable">
                Edit Account
              </div>
            </router-link>
            <div class="list-group-item dropdown-item list-group-item-action selectable" @click="logout">
              Logout
            </div>
          </div>
        </div>
      </div>
    </div>
  </span>
</template>

<style lang="scss" scoped>

.navbar-text {
    user-select: none;
}

.dropdown-menu,.dropdown-menu>.list-group, .dropdown-menu>.list-group *,.list-group-item {
    border-radius: 0 !important;
}

</style>
