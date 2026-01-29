import {defineStore} from 'pinia';
import { ref,computed } from 'vue'
import type { LoginRequest,RegisterRequest,User } from '../types/auth';
import { logginApi, registerApi } from '../api/auth.api';
import { useToast } from 'vue-toastification';

const toast = useToast();
export const useAuthStore = defineStore('auth', () => {
    const token = ref<string|null>(null)
    const user = ref<User|null>(null)

    const isLoggedIn = computed(()=>!!user.value)

    function init(){
        const t = localStorage.getItem('token')
        const u = localStorage.getItem('user')
        if (t && u){
            token.value = t
            user.value = JSON.parse(u)
        }
    }

    async function login(data:LoginRequest){
        try{
            const res = await logginApi(data)
            token.value = res.data.token
            user.value = {
                username:res.data.username,
                email:res.data.email
            }
            localStorage.setItem('token',token.value);
            localStorage.setItem('user',JSON.stringify(user.value));
            toast.success('Welcome back')
        }catch (err) {
            toast.error('Invalid username or password')
            throw err
        }
    }

    async function register(data:RegisterRequest){
        try{
            const res = await registerApi(data)
            token.value = res.data.token
            user.value = {
                username:res.data.username,
                email:res.data.email
            }
            localStorage.setItem('token',token.value);
            localStorage.setItem('user',JSON.stringify(user.value));
            toast.success('Welcome')
        }catch (err) {
            toast.error('Invalid username, password or email')
            throw err
        }
    }

    function logout(){
        token.value = null
        user.value = null
        localStorage.clear()
        toast.info('Logged out')
    }

    return {token, user, isLoggedIn, login, logout,register, init}
})