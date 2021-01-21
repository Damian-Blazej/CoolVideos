import Vue from 'vue'
import Vuex from 'vuex'
import router from './router'

Vue.use(Vuex);

const types = {
    LOGIN: 'LOGIN',
    LOGUOT: 'LOGOUT'
};

const state = {
    logged: localStorage.getItem('token'),
    user: localStorage.getItem('userId')
};

const getters = {
    isLogged: state => state.logged,
    userId: state => state.user
};

const actions = {
    login({ commit }, userId) {
        router.push({ path: '/' });
        commit(types.LOGIN, userId);
/*        Vue.http.post('/api/token', credential)
            .then((result) => {
                if (result.status == 200) {
                    localStorage.setItem('token', result.body);
                    router.push({ path: '/' });
                    commit(types.LOGIN);
                }
            });*/
    },

    logout({ commit }) {
        localStorage.removeItem('token');
        localStorage.removeItem('userId');
        commit(types.LOGUOT);
    }
};

const mutations = {
    [types.LOGIN](state, userId) {
        state.logged = 1;
        state.user = userId;
    },
    [types.LOGUOT](state) {
        state.logged = 0;
        state.user = null;
    }
};

export default new Vuex.Store({
    state,
    getters,
    actions,
    mutations
})
