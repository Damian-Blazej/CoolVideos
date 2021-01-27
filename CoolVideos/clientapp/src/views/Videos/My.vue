<template>
  <div>
    <div class="bg-light text-primary py-5">
      <b-container>
        <div class="text-center">
          <h1 class="font-weight-bold">Moje filmy</h1>
          <b-button
            pill
            variant="warning"
            size="lg"
            :to="{ name: 'NewVideo' }"
            class="mt-4 px-5"
            >Dodaj nowy</b-button
          >
        </div>
      </b-container>
    </div>
    <div class="mt-5 container d-flex flex-column align-items-center">
      <b-spinner
        v-if="isLoading"
        variant="warning"
        label="Ładowanie..."
      ></b-spinner>
      <template v-else>
        <b-list-group class="w-100">
          <VideoInfo
            v-for="video in videos"
            :key="video.videoId"
            @deleted="fetchVideos"
            :title="video.title"
            :img-src="video.image"
            :description="video.description"
            :likes="video.likes"
            :video-id="video.id"
          ></VideoInfo>
        </b-list-group>
      </template>
    </div>
  </div>
</template>

<script>
import VideoInfo from "../../components/VideoInfo";

export default {
  name: "Index.vue",
  components: {
    VideoInfo
  },
  data() {
    return {
      videos: [],
      userID: 1,
      isLoading: true
    };
  },
  created() {
    this.fetchVideos();
  },
  methods: {
    fetchVideos() {
      this.isLoading = true;
      this.$http
        .get(
          `video/search?pageNumber=1&pageCount=15&userID=${this.$store.getters.userId}`
        )
        .then(res => {
          this.isLoading = false;
          this.videos = res.data.videos;
        });
    }
  }
};
</script>

<style scoped></style>
