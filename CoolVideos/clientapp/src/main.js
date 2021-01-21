// import Vue from "vue";
// import App from "./App.vue";
// import router from "./router";
// import store from "./store";

// Vue.config.productionTip = false;

// new Vue({
//   router,
//   store,
//   render: h => h(App)
// }).$mount("#app");
import Vue from 'vue'
import vueResorce from 'vue-resource'
import Vuelidate from 'vuelidate'
import App from './App.vue'
import { BootstrapVue, IconsPlugin } from 'bootstrap-vue'
import router from './router'
import './sass/main.scss'
import store from './store'
import VueSlideUpDown from 'vue-slide-up-down'
//import Vuetify from 'vuetify'



Vue.component('vue-slide-up-down', VueSlideUpDown)
Vue.use(BootstrapVue);
Vue.use(IconsPlugin);
Vue.use(vueResorce);
Vue.use(Vuelidate);
//Vue.use(Vuetify);
Vue.config.productionTip = false;
Vue.prototype.$serverUrl = 'http://localhost:44338';

//Ten middleware dodaje header z tokenem
Vue.http.interceptors.push((request, next) => {

    if (localStorage.getItem('token')) {
        request.headers.set('Authorization', 'Bearer ' + localStorage.getItem('token'));
    }

    next(response => {
        const statusCode = response.status;
        if (statusCode === 401) {
            router.push({ name: "Login" });
            return;
        }
        const statusCodeGroup = Math.floor(statusCode / 100);
        if (statusCodeGroup === 4 || statusCodeGroup === 5) {
            router.push({ path: "/error/" + statusCode });
        }
    })
});

new Vue({
    router,
    store,
    render: h => h(App),
}).$mount('#app');
