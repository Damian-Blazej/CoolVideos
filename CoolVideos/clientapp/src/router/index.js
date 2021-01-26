import Vue from "vue";
import VueRouter from "vue-router";
import Home from "../views/Home/Index.vue";
import Videos from "../views/Home/Videos.vue";
import Video from "../views/Videos/Index.vue";
import ListVideos from "../views/Videos/List.vue";
import Login from "../views/Auth/Login.vue";
import Register from "../views/Auth/Register.vue";
import MyVideos from "../views/Videos/My.vue";
import EditVideo from "../views/Videos/Edit.vue";
import NewVideo from "../views/Videos/New.vue";
import store from "../store/index";
import NotFound from "../views/Error/NotFound";
import Unauthorized from "../views/Error/Unauthorized";

Vue.use(VueRouter);

function ifAuthenticated(to, from, next) {
  if (store.getters.isLogged) {
    next();
  } else {
    next("/login");
  }
}

const routes = [
  {
    path: "/",
    name: "Home",
    component: Home
  },
  {
    path: "/videos/:index",
    name: "Video",
    component: Video,
    beforeEnter: (to, from, next) => {
      if (!isNaN(to.params.index)) {
        next();
      } else {
        next({ name: "PageNotFound" });
      }
    }
  },
  {
    path: "/home",
    name: "Videos",
    component: Videos
  },
  {
    path: "/videos",
    name: "ListVideos",
    component: ListVideos
  },
  {
    path: "/login",
    name: "Login",
    component: Login
  },
  {
    path: "/register",
    name: "Register",
    component: Register
  },
  {
    path: "/videos/my",
    name: "MyVideos",
    component: MyVideos,
    beforeEnter: ifAuthenticated
  },
  {
    path: "/videos/create",
    name: "NewVideo",
    component: NewVideo,
    beforeEnter: ifAuthenticated
  },
  {
    path: "/videos/:id/edit",
    name: "EditVideo",
    component: EditVideo,
    beforeEnter: (to, from, next) => {
      Vue.http.get(`video/${to.params.id}`).then(res => {
        if (res.data.userId.toString() === store.getters.userId) {
          next();
        } else {
          next({ name: "Unauthorized" });
        }
      });
    }
  },
  {
    path: "/unauthorized",
    name: "Unauthorized",
    component: Unauthorized
  },
  {
    path: "*",
    name: "PageNotFound",
    component: NotFound
  }
];

const router = new VueRouter({
  mode: "history",
  routes
});

export default router;
