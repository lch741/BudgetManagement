import axios from "axios";
import { useToast } from "vue-toastification"; 
import { useAuthStore } from "../stores/auth.store";
import router from "../router/router";

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

api.interceptors.response.use(
  res => res,
  error => {
    const auth = useAuthStore();
    const err = error.response;

    if (!err) {
      toast.error('Network error');
    } else if (err.status === 401) {
      toast.warning('Session expired, please login again');
      auth.logout();
      router.push('/login');
    } else if (err.data?.message) {
      toast.warning(err.data.message);
    }

    return Promise.reject(error);
  }
);

export default api;