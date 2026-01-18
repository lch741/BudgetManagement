import Toast, { POSITION } from "vue-toastification";
import { createApp } from 'vue'
import { createPinia } from 'pinia'
import './style.css'
import App from './App.vue'
import router from "./router/router";

const app = createApp(App);
app.use(Toast,{position:POSITION.TOP_RIGHT});
const pinia = createPinia();
app.use(router)
app.use(pinia);
app.mount('#app');

