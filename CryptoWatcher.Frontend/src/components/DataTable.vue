<script setup lang="ts">
import { ref, computed, defineProps } from 'vue'

interface Crypto {
  id?: number;
  symbol: string;
  price: string;
}

const props = defineProps<{
  items: Crypto[]
}>()

const searchQuery = ref('')
const filteredItems = computed(() => {
  if (!searchQuery.value) return props.items
  
  return props.items.filter(item => 
    item.symbol.toLowerCase().includes(searchQuery.value.toLowerCase())
  )
})
</script>

<template>
  <div class="data-table-container">
    <div class="search-container">
      <input 
        v-model="searchQuery" 
        type="text" 
        placeholder="Search crypto" 
        class="search-input"
      >
    </div>
    <table class="crypto-table">
      <thead>
        <tr>
          <th>Id</th>
          <th>Crypto</th>
          <th>Current Value</th>
        </tr>
      </thead>
      <tbody>
        <tr 
          v-for="item in filteredItems" 
          :key="item.id" 
          class="table-row"
        >
          <td>{{ item.id }}</td>
          <td>{{ item.symbol }}</td>
          <td>{{ item.price }}</td>
        </tr>
        <tr v-if="filteredItems.length === 0" class="no-results">
          <td colspan="3">No crypto was found</td>
        </tr>
      </tbody>
    </table>
  </div>
</template>

<style scoped>
.data-table-container {
  background-color: #1e1e1e;
  color: #ffffff;
  padding: 20px;
  border-radius: 8px;
}

.search-container {
  margin-bottom: 15px;
}

.search-input {
  width: 100%;
  padding: 10px;
  background-color: #2c2c2c;
  border: 1px solid #3c3c3c;
  border-radius: 4px;
  color: #ffffff;
}

.search-input::placeholder {
  color: #888;
}

.crypto-table {
  width: 100%;
  border-collapse: collapse;
}

.crypto-table thead {
  background-color: #2c2c2c;
}

.crypto-table th, 
.crypto-table td {
  padding: 12px;
  text-align: left;
  border-bottom: 1px solid #3c3c3c;
}

.table-row:hover {
  background-color: #2c2c2c;
}

.no-results {
  text-align: center;
  color: #888;
  padding: 20px;
}
</style>