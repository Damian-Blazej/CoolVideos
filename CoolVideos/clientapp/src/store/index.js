import Vue from "vue";
import Vuex from "vuex";
import router from "../router";

Vue.use(Vuex);

const types = {
  LOGIN: "LOGIN",
  LOGOUT: "LOGOUT"
};

const state = {
  logged: !!localStorage.getItem("token"),
  token: localStorage.getItem("token"),
  user: localStorage.getItem("userId")
};

const getters = {
  isLogged: state => state.logged,
  userId: state => state.user,
  token: state => state.token
};

const actions = {
  login({ commit }, { token, userId }) {
    localStorage.setItem("token", token);
    localStorage.setItem("userId", userId);
    commit(types.LOGIN);
    router.push({ path: "/" });
  },

  logout({ commit }) {
    localStorage.removeItem("token");
    localStorage.removeItem("userId");
    commit(types.LOGOUT);
  }
};

const mutations = {
  [types.LOGIN](state) {
    state.logged = 1;
    state.token = localStorage.getItem("token");
    state.user = localStorage.getItem("userId");
  },
  [types.LOGOUT](state) {
    state.logged = 0;
    state.user = null;
    state.token = null;
  }
};

export default new Vuex.Store({
  state,
  getters,
  actions,
  mutations
});
