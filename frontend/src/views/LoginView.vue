<script setup lang="ts">
import { ref } from 'vue';
import { useRouter } from 'vue-router';
import { useAuthStore } from '../stores/auth.store';
import { useToast } from 'vue-toastification';


const Username = ref('')
const Password = ref('')
const loading = ref(false)
const router = useRouter()
const authStore = useAuthStore()
const toast = useToast();

const handleLogin = async() =>{
    if(!Username.value||!Password.value){
        toast.warning('Please enter username and password')
        return
    }
    try{
        loading.value = true

        await authStore.login({
            username:Username.value,
            password:Password.value
        })

        router.push('/')
    } catch(err){
        console.error(err)
    } finally{
        loading.value = false
    }
}
</script>

<template>
    <div class="min-h-screen flex items-center justify-center bg-gray-400">
        <div class="w-full max-w-md bg-white p-8 rounded-lg shadow">  
            <h1 class="text-2xl font-bold text-center mb-6">
                Login
            </h1>

            <div class="mb-4">
                <label class="block text-sm font-medium mb-1">Username
                    <input v-model="Username" 
                    type="text"
                    class="w-full border border-gray-300 rounded px-3 py-2
                           focus:outline-none focus:ring-2 focus:ring-blue-500"   
                    placeholder="Enter username"/>
                </label>
            </div>

            <div class="mb-6">
                <label class="block text-sm font-medium mb-1">Password
                    <input v-model="Password" 
                    type="Password"
                    class="w-full border border-gray-300 rounded px-3 py-2
                           focus:outline-none focus:ring-2 focus:ring-blue-500"   
                    placeholder="Enter Password"/>
                </label>
            </div>

            <button 
            :disabled = "loading" 
            @click ="handleLogin" 
            class="block mt-4 w-full text-center border border-red-600
                   text-red-600 py-2 rounded hover:bg-red-50 transition
                   disabled:opacity-50 disabled:cursor-not-allowed">
                {{loading?'Loggin in ...':'Login'}}
            </button>
            
            <router-link 
                to="/register"
                class="block mt-4 w-full text-center border border-blue-600
                       text-blue-600 py-2 rounded hover:bg-blue-50 transition"
            >
                Register
            </router-link>
        </div>
    </div>
</template>