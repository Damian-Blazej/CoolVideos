import Vue from "vue";
import VueResource from "vue-resource";
import Vuelidate from "vuelidate";
import App from "./App.vue";
import { BootstrapVue, IconsPlugin } from "bootstrap-vue";
import router from "./router";
import "./sass/main.scss";
import store from "./store/index";
import VueSlideUpDown from "vue-slide-up-down";

Vue.component("vue-slide-up-down", VueSlideUpDown);
Vue.use(BootstrapVue);
Vue.use(IconsPlugin);
Vue.use(VueResource);
Vue.use(Vuelidate);
Vue.config.productionTip = false;
Vue.prototype.$serverUrl = "http://localhost:44338/";

//Ten middleware dodaje header z tokenem
Vue.http.interceptors.push((request, next) => {
  if (store.getters.isLogged) {
    request.headers.set("Authorization", "Bearer " + store.getters.token);
  }

  request.url = Vue.prototype.$serverUrl + "api/" + request.url;

  next(response => {
    const statusCode = response.status;
    if (statusCode === 401) {
      router.push({ name: "Login" });
    }
  });
});

new Vue({
  router,
  store,
  render: h => h(App)
}).$mount("#app");
