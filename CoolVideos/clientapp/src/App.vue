<template>
  <div id="app">
    <Navbar />
    <!--<button v-on:click="logout" class="btn btn-outline-success my-2 my-sm-0">Wyloguj</button>-->
    <router-view class="router-view mb-5" />
    <Footer />
  </div>
</template>

<script>
import Navbar from "./components/Navbar.vue";
import Footer from "./components/Footer.vue";

export default {
  name: "app",
  components: {
    Navbar,
    Footer
  }
};
</script>
