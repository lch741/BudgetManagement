import axios from "axios";
import { useToast } from "vue-toastification"; 
import { useAuthStore } from "../stores/auth.store";

const api = axios.create({
  baseURL: 'http://localhost:5067/api',
  headers: {
    "Content-Type":'application/json'
  }
})

const toast = useToast();

api.interceptors.request.use((config) => {
    const auth = useAuthStore();
    if(auth.token){
        config.headers.Authorization = `Bearer ${auth.token}`
    }
    return config
})

api.interceptors.request.use(
    res => res,
    error => {
      const err = error.response
      if(!err){
        toast.error('Network error')
      }else if(Array.isArray(err.data?.errors)){
        err.data.errors.forEach((e:any) => {
          toast.warning(e.description)
        })
      }else if(typeof err.data?.errors === 'object'){
        Object.values(err.data.errors).forEach((e:any) => {
          toast.warning(e[0])
        })
      }else if(err.status === 401){
        toast.warning('Please login')
        window.location.href = '/login'
      }else if(err.data?.message){
        toast.warning(err.data.message)
      }else if (err) {
        toast.warning(err?.data);
      }
      return Promise.reject(error);
  }
)

export default api;