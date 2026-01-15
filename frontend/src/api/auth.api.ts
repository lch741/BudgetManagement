import axios from "axios";
import { useToast } from "vue-toastification"; 
import api from './axios'
import type { AuthResponse, LoginRequest, RegisterRequest } from "../types/auth";

export function logginApi(data:LoginRequest){
    return api.post<AuthResponse>('/account/login',{
        data
    })
}

export function registerApi(data:RegisterRequest){
    return api.post<AuthResponse>('/account/register',{
        data
    })
}