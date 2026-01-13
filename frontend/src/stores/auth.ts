import {defineStore} from 'pinia';
import { ref,computed } from 'vue'

export const useAuthStore = defineStore('auth', () => {
    const token = ref<string|null>(null)
    const user = ref<string|null>(null)

    const isLoggedIn = computed(()=>!!user.value)

    function init(){
        const t = localStorage.getItem('token')
        const u = localStorage.getItem('user')
        if (t && u){
            token.value = t
            user.value = u
        }
    }

    function login(tokenValue:string,userValue:string){
        token.value = tokenValue
        user.value = userValue
        localStorage.setItem('token',tokenValue)
        localStorage.setItem('user',tokenValue)
    }

    function logout(){
        token.value = null
        user.value = null
        localStorage.clear()
    }

    return {token, user, isLoggedIn, login, logout, init}
})