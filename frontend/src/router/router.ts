import { createRouter,createWebHistory } from "vue-router";
import { useAuthStore } from "../stores/auth.store";
import LoginView from '../views/LoginView.vue'
import RegisterView from '../views/RegisterView.vue'


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
    //{
        //path:'/',
        //redirect:'/transactions',
        //meta:{requireAuth:true},
        //children:[
            //{
            //path:'transactions',
            //name:'transactions',
            //component:TransactionsView,
            //meta:{requireAuth:true},
            //},
            //{
            //path:'categories',
            //name:'categories',
            //component:CategoriesView,
            //meta:{requireAuth:true},
            //},
        //]
    //}
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