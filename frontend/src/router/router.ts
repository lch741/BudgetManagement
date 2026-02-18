import { createRouter,createWebHistory } from "vue-router";
import { useAuthStore } from "../stores/auth.store";
import LoginView from '../views/LoginView.vue'
import RegisterView from '../views/RegisterView.vue'
import CategoriesView from '../views/CategoriesView.vue'
import TransactionsView from '../views/TransactionsView.vue'


const routes = [
    {
        path:'/login',
        name:'login',
        component:LoginView,
    },
    {
        path:'/register',
        name:'register',
        component:RegisterView,
    },
    {
        path:'/categories',
        name:'categories',
        component:CategoriesView,
        meta:{requireAuth:true},
    },
    {
        path:'/transactions',
        name:'transactions',
        component:TransactionsView,
        meta:{requireAuth:true},
    },
    {
        path:'/',
        redirect:'/transactions'
    }
]

const router = createRouter({
  history: createWebHistory(),
  routes
})

router.beforeEach((to)=>{
    const auth = useAuthStore();
    if(!auth.user&&localStorage.getItem('token')){
        auth.init()
    }

    if(to.meta.requireAuth&&auth.isLoggedIn===false){
        return {name:'login'}
    }

    if(to.name==='login'&&auth.isLoggedIn===true){
        return {name:'transactions'}
    }
})

export default router