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
    <div class="mt-5 d-flex flex-column align-items-center">
      <VideoInfo
        v-for="video in videos"
        :key="video.videoId"
        :title="video.title"
        :img-src="video.image"
        :description="video.description"
        :likes="video.likes"
        :video-id="video.id"
      ></VideoInfo>
    </div>
  </div>
</template>

<script>
import { fetchData } from "../../functions/fetch";
import VideoInfo from "@/components/VideoInfo";

export default {
  name: "Index.vue",
  components: {
    VideoInfo
  },
  data() {
    return {
      videos: [],
      userID: 1
    };
  },
  created() {
    //this.$http.get('/api/video/search?pageNumber=1&pageCount=15&userID=51').then(res => { this.videos = res.videos });
    this.userID = this.$store.getters.userId;
    fetchData(
        `video/search?pageNumber=1&pageCount=15&userID=${this.userID}`
    ).then(res => {
      this.videos = res.videos;
    });
  }
};
</script>

<style scoped></style>
