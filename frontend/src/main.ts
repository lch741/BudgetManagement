import Toast, { POSITION } from "vue-toastification";
import { createApp } from 'vue'
import { createPinia } from 'pinia'
import App from './App.vue'
import router from "./router/router";
import './assets/test.css'
import "vue-toastification/dist/index.css"

const app = createApp(App);
app.use(Toast, {
  position: POSITION.TOP_RIGHT,
  timeout: 3000,          
  closeOnClick: true,     
  pauseOnFocusLoss: true, 
  pauseOnHover: true,     
  draggable: true,
  draggablePercent: 0.6,
  showCloseButtonOnHover: false,
  hideProgressBar: false,
  newestOnTop: true,
  maxToasts: 5,
  toastClassName: "rounded-lg !text-white !bg-blue-600 shadow-md px-4 py-2",
});
const pinia = createPinia();
app.use(router)
app.use(pinia);
app.mount('#app');

