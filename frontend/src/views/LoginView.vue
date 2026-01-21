<script setup lang="ts">
import { ref } from 'vue';
import { useRouter } from 'vue-router';
import { useAuthStore } from '../stores/auth.store';


const Username = ref('')
const Password = ref('')
const loading = ref(false)
const router = useRouter()
const authStore = useAuthStore()

const handleLogin = async() =>{
    if(!Username.value||!Password.value){
        alert('Please enter username and password')
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
    <div class="login">
        <h1>Login</h1>

        <input v-model="Username" placeholder="Username"/>
        <input v-model="Password" type="Password" placeholder="Password"/>

        <button :disabled = "loading" @click ="handleLogin" >
            {{loading?'Loggin in ...':'Login'}}
        </button>

    </div>
</template>