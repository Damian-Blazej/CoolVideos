<template>
  <div>
    <b-navbar
      toggleable="lg"
      type="light"
      variant="warning"
      class="shadow-sm py-3"
      fixed="top"
    >
      <b-navbar-brand id="homePageLink" to="/" class="nav-logo"
        >😎🤙CoolVideos😎🤙</b-navbar-brand
      >

      <b-navbar-toggle target="nav-collapse"></b-navbar-toggle>

      <b-collapse id="nav-collapse" is-nav>
        <b-navbar-nav>
          <b-nav-item id="videosLink" class="nav-item" :to="{ name: 'Videos' }"
            >Filmy</b-nav-item
          >
          <b-nav-item v-if="isLogged" :to="{ name: 'MyVideos' }">
            Moje filmy
          </b-nav-item>
        </b-navbar-nav>
        <b-navbar-nav class="ml-auto">
          <b-nav-item id="loginLink" v-if="!isLogged" :to="{ name: 'Login' }">
            Zaloguj się
          </b-nav-item>
          <b-nav-item id="logoutLink" v-if="isLogged" @click="logout">
            Wyloguj się
          </b-nav-item>
          <b-nav-item
            id="registerLink"
            v-if="!isLogged"
            :to="{ name: 'Register' }"
          >
            Zarejestruj się
          </b-nav-item>
        </b-navbar-nav>
      </b-collapse>
    </b-navbar>
  </div>
</template>

<script>
import { mapGetters } from "vuex";

export default {
  name: "Navbar",
  computed: mapGetters(["isLogged"]),
  methods: {
    logout() {
      this.$store.dispatch("logout");
      this.$router.push({ name: "Login" });
    }
  }
};
</script>
