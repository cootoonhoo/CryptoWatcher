<script setup lang="ts">
import { ref } from 'vue'
import DataTable from "./components/DataTable.vue"
import ProfileMannager from "./components/ProfileMannager.vue"

interface Crypto {
  id?: number;
  symbol: string;
  price: string;
}

interface Profile {
  id?: number;
  profileName: string;
  cryptos: Crypto[];
}

const items = ref<Crypto[]>([])
const profiles = ref<Profile[]>([])

const handleProfileSelected = (profile: Profile) => {
  items.value = profile.cryptos
}
</script>

<template>
  <header>
    <ProfileMannager 
      :profiles="profiles" 
      @profile-selected="handleProfileSelected"
    ></ProfileMannager>
  </header>
  <DataTable :items="items">
  </DataTable>
</template>