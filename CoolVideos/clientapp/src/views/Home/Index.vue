<template>
  <div>
    <div class="bg-light text-primary py-5">
      <b-container>
        <b-row>
          <b-col md="12">
            <div class="text-left">
              <h1 class="font-weight-bold">Siema na CoolVideos! 💪</h1>
              <h2>Obejrzysz tutaj HIT filmy 🎬</h2>
              <h2>Dołącz do nas całkowicie za darmo! 🤑</h2>
              <b-button
                id="loginButton"
                v-if="!this.$store.getters.isLogged"
                pill
                variant="primary"
                size="lg"
                :to="{ name: 'Login' }"
                class="mt-4 px-5"
              >
                Dołącz!
                <i class="bi bi-door-open"></i>
              </b-button>
            </div>
          </b-col>
        </b-row>
      </b-container>
    </div>
    <div class="mt-5">
      <b-container>
        <div class="mt-3 text-center">
          <h2 class="font-weight-bold">Najnowsze filmy</h2>
          <div
            class="d-flex justify-content-center align-items-center flex-wrap mt-4"
          >
            <b-spinner
              v-if="isLoading"
              variant="warning"
              label="Ładowanie..."
            ></b-spinner>
            <VideoCard
              v-for="video in videos"
              :key="video.videoId"
              :video-id="video.id"
              :title="video.title"
              :img-src="`${$serverUrl}Resources/Images/${video.image}`"
              :img-alt="video.title + ' image'"
              :author="video.user.firstName + ' ' + video.user.lastName"
            >
            </VideoCard>
          </div>
        </div>
      </b-container>
    </div>
  </div>
</template>

<script>
import VideoCard from "../../components/VideoCard";

export default {
  name: "Home",
  components: {
    VideoCard
  },
  data() {
    return {
      videos: null,
      isLoading: true
    };
  },
  methods: {
    fetchLatestVideos() {
      this.$http.get("video/latest?n=3").then(result => {
        this.isLoading = false;
        this.videos = result.data;
      });
    }
  },
  mounted() {
    this.fetchLatestVideos();
  }
};
</script>
