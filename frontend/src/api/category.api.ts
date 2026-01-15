import axios from "axios";
import { useToast } from "vue-toastification"; 
import api from './axios'
import type { Category } from "../types/category";
import type { List } from "postcss/lib/list";


export function getCategories(){
    return api.get<Category[]>('/Category')
}

export function getCategoryById(id:number){
    return api.get<Category>(`/Category/${id}`)
}

export function deleteCategory(id:number){
    return api.delete(`/Category/${id}`)
}

export function createCategory(name:string){
    return api.post('/Category',{name})
}

export function updateCategory(id:number,name:string){
    return api.put(`/Category/${id}`,{name})
}