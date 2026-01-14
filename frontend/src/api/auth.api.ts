import axios from "axios";
import { useAuthStore } from "../stores/auth";
import { useToast } from "vue-toastification"; 
import api from './axios'

export interface LoginResponse{
    Username:string,
    Email:string,
    Token:string
}

export function logginApi(Username:string,Password:string){
    return api.post<LoginResponse>('/account/login',{
        Username,
        Password
    })
}

export function registerApi(Username:string,Password:string,Email:string){
    return api.post<LoginResponse>('/account/register',{
        Username,
        Email,
        Password
    })
}