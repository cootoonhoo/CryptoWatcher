<template>
    <div class="crypto-profiles-modal">
      <button @click="openProfilesModal" class="open-profiles-btn">
        Profiles
      </button>
  
      <div v-if="isModalOpen" class="modal-overlay">
        <div class="modal-content">
          <div class="modal-header">
            <h2>Profiles</h2>
            <button @click="closeModal" class="close-btn">&times;</button>
          </div>
  
          <div v-if="loading" class="loading-indicator">
            Loading profiles...
          </div>
  
          <div v-else-if="error" class="error-message">
            {{ error }}
          </div>
  
          <div v-else class="profiles-list">
            <div 
              v-for="profile in profiles" 
              :key="profile.id" 
              class="profile-card"
              @click="selectProfile(profile)"
              :class="{ 'selected-profile': selectedProfile?.id === profile.id }"
            >
              <h3>{{ profile.profileName }}</h3>
            </div>
          </div>
  
          <div class="modal-actions">
            <button @click="openCreateProfileModal" class="create-profile-btn">
              New Profile
            </button>
          </div>
        </div>
      </div>
  
      <div v-if="isCreateModalOpen" class="modal-overlay">
        <div class="modal-content create-profile-modal">
          <div class="modal-header">
            <h2>New Profile</h2>
            <button @click="closeCreateProfileModal" class="close-btn">&times;</button>
          </div>
  
          <form @submit.prevent="createProfile">
            <div class="form-group">
              <label>Profile Name</label>
              <input 
                v-model="newProfileName" 
                type="text" 
                required 
                placeholder="Profile Name"
              />
            </div>
  
            <div class="form-group crypto-input">
              <label>Add Cryptocoin</label>
              <div class="crypto-input-row">
                <input 
                  v-model="newCryptoSymbol" 
                  type="text" 
                  placeholder="Trade Symbol"
                />
                <button 
                  @click.prevent="addCryptoToNewProfile" 
                  class="add-crypto-btn"
                >
                  +
                </button>
              </div>
            </div>
  
            <div class="added-cryptos">
              <div 
                v-for="(crypto, index) in newProfileCryptos" 
                :key="index" 
                class="added-crypto-item"
              >
                <span>{{ crypto.symbol }} - {{ crypto.price }}</span>
                <button 
                  @click.prevent="removeCryptoFromNewProfile(index)"
                  class="remove-crypto-btn"
                >
                  Remover
                </button>
              </div>
            </div>
  
            <div class="modal-actions">
              <button 
                type="button"
                @click="closeCreateProfileModal" 
                class="cancel-btn"
              >
                Cancelar
              </button>
              <button 
                type="submit" 
                class="submit-btn"
              >
                Criar Perfil
              </button>
            </div>
          </form>
        </div>
      </div>
    </div>
  </template>
  
  <script lang="ts">
  import { defineComponent, ref, onMounted, defineProps, defineEmits } from 'vue';
  import axios from 'axios';
  
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
  
  export default defineComponent({
    name: 'ProfileMannager',
    props: {
      profiles: {
        type: Array,
        required: true
      }
    },
    emits: ['profile-selected'],
    setup(props, { emit }) {
      const loading = ref(false);
      const error = ref<string | null>(null);
      const isModalOpen = ref(false);
      const isCreateModalOpen = ref(false);
      const newProfileName = ref('');
      const newCryptoSymbol = ref('');
      const newCryptoPrice = ref('');
      const newProfileCryptos = ref<Crypto[]>([]);
      const selectedProfile = ref<Profile | null>(null);
  
      const fetchProfiles = async () => {
        loading.value = true;
        try {
          const response = await axios.get('https://localhost:7094/CryptoWatcher/GetAllProfiles');
          props.profiles.splice(0, props.profiles.length, ...response.data);
          loading.value = false;
        } catch (err) {
          error.value = 'Erro ao carregar perfis';
          loading.value = false;
        }
      };
  
      const selectProfile = (profile: Profile) => {
        selectedProfile.value = profile;
        emit('profile-selected', profile);
        closeModal();
      };
  
      const openProfilesModal = () => {
        isModalOpen.value = true;
        fetchProfiles();
      };
  
      const closeModal = () => {
        isModalOpen.value = false;
      };
  
      const openCreateProfileModal = () => {
        isCreateModalOpen.value = true;
      };
  
      const closeCreateProfileModal = () => {
        isCreateModalOpen.value = false;
        resetNewProfileForm();
      };
  
      const resetNewProfileForm = () => {
        newProfileName.value = '';
        newCryptoSymbol.value = '';
        newCryptoPrice.value = '';
        newProfileCryptos.value = [];
      };
  
      const addCryptoToNewProfile = () => {
        if (newCryptoSymbol.value && newCryptoPrice.value) {
          newProfileCryptos.value.push({
            symbol: newCryptoSymbol.value,
            price: newCryptoPrice.value
          });
          newCryptoSymbol.value = '';
          newCryptoPrice.value = '';
        }
      };
  
      const removeCryptoFromNewProfile = (index: number) => {
        newProfileCryptos.value.splice(index, 1);
      };
  
      const createProfile = async () => {
        try {
          const newProfile: Profile = {
            profileName: newProfileName.value,
            cryptos: newProfileCryptos.value
          };
  
          await axios.post('https://localhost:5248/api/Job/UpsertProfile', newProfile);
          
          // Recarrega os perfis após a criação
          await fetchProfiles();
          
          closeCreateProfileModal();
        } catch (err) {
          error.value = 'Erro ao criar perfil';
        }
      };
  
      return {
        loading,
        error,
        isModalOpen,
        isCreateModalOpen,
        newProfileName,
        newCryptoSymbol,
        newCryptoPrice,
        newProfileCryptos,
        selectedProfile,
        openProfilesModal,
        closeModal,
        openCreateProfileModal,
        closeCreateProfileModal,
        addCryptoToNewProfile,
        removeCryptoFromNewProfile,
        createProfile,
        fetchProfiles,
        selectProfile
      };
    }
  });
  </script>
  
  <style scoped>
  .crypto-profiles-modal {
    font-family: Arial, sans-serif;
  }
  
  .open-profiles-btn {
    padding: 10px 15px;
    background-color: #007bff;
    color: white;
    border: none;
    border-radius: 4px;
    cursor: pointer;
  }
  
  .modal-overlay {
    position: fixed;
    top: 0;
    left: 0;
    width: 100%;
    height: 100%;
    background-color: rgba(0, 0, 0, 0.5);
    display: flex;
    justify-content: center;
    align-items: center;
    z-index: 1000;
  }
  
  .modal-content {
    background-color: var(--vt-c-black-soft);;
    padding: 20px;
    border-radius: 8px;
    width: 500px;
    max-height: 80vh;
    overflow-y: auto;
  }
  
  .modal-header {
    display: flex;
    justify-content: space-between;
    align-items: center;
    border-bottom: 1px solid #eee;
    padding-bottom: 10px;
    margin-bottom: 15px;
  }
  
  .close-btn {
    background: none;
    border: none;
    font-size: 24px;
    cursor: pointer;
    color: white;
  }
  
  .profiles-list {
    display: grid;
    gap: 15px;
  }
  
  .profile-card {
    border: 1px solid #ddd;
    padding: 15px;
    border-radius: 4px;
    width: auto;
  }
  
  
  .crypto-list {
    margin-top: 10px;
  }
  
  .crypto-item {
    display: flex;
    justify-content: space-between;
    padding: 5px 0;
    border-bottom: 1px solid #f1f1f1;
  }
  
  .modal-actions {
    margin-top: 15px;
    display: flex;
    justify-content: flex-end;
  }
  
  .create-profile-btn, .submit-btn {
    background-color: #28a745;
    color: white;
    border: none;
    padding: 10px 15px;
    border-radius: 4px;
    cursor: pointer;
  }
  
  .cancel-btn {
    background-color: #6c757d;
    color: white;
    border: none;
    padding: 10px 15px;
    border-radius: 4px;
    margin-right: 10px;
    cursor: pointer;
  }
  
  .form-group {
    margin-bottom: 15px;
  }
  
  .form-group label {
    display: block;
    margin-bottom: 5px;
  }
  
  .form-group input {
    width: 100%;
    padding: 8px;
    border: 1px solid #ddd;
    border-radius: 4px;
  }
  
  .crypto-input-row {
    display: flex;
    gap: 10px;
  }
  
  .crypto-input-row input {
    flex-grow: 1;
  }
  
  .add-crypto-btn {
    background-color: #17a2b8;
    color: white;
    border: none;
    padding: 8px 12px;
    border-radius: 4px;
    cursor: pointer;
  }
  
  .added-cryptos {
    margin-top: 10px;
  }
  
  .added-crypto-item {
    display: flex;
    justify-content: space-between;
    background-color: #f1f1f1;
    padding: 8px;
    margin-bottom: 5px;
    border-radius: 4px;
  }
  
  .remove-crypto-btn {
    background-color: #dc3545;
    color: white;
    border: none;
    padding: 4px 8px;
    border-radius: 4px;
    cursor: pointer;
  }
  
  .loading-indicator, .error-message {
    text-align: center;
    padding: 20px;
  }

  .profile-card {
  border: 1px solid #ddd;
  padding: 15px;
  border-radius: 4px;
  cursor: pointer;
  transition: background-color 0.3s;
}

.profile-card:hover {
  background-color: #f0f0f02a;
}

.selected-profile {
  background-color: #e0e0e044;
  border-color: #007bff;
}
  </style>