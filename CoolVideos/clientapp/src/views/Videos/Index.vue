<template>
  <b-container>
    <div class="mt-5 d-flex flex-column align-items-center">
      <h2 class="mb-4">{{ video.title }}</h2>
      <video
        :src="`${$serverUrl}/Resources/Videos/${video.uri}`"
        controls
        width="100%"
        height="auto"
      ></video>
      <div class="d-flex justify-content-between align-items-center my-2 w-100">
        <div class="d-flex align-items-center">
          <b-button
            @click="toggleLike"
            :variant="didUserLikeVideo ? 'warning' : 'outline-warning'"
            pill
            size="sm"
            squared
            class="mr-2"
          >
            😎🤙
          </b-button>
          <span>{{ video.likes }} szefów myśli, że ten film jest super!</span>
        </div>
        <div class="sharethis-inline-share-buttons"></div>
      </div>
      <h5 class="mt-4">{{ video.description }}</h5>
    </div>
  </b-container>
</template>

<script>
import { fetchData } from "@/functions/fetch.js";

export default {
  data() {
    return {
      video: {},
      didUserLikeVideo: true
    };
  },
  created() {
    fetchData(this.$serverUrl + "/api/video/" + this.$route.params.index).then(
      data => {
        if (data.status === 404) {
          this.$router.push({ name: "PageNotFound" });
        } else {
          this.video = data;
          this.checkIfUserLikedVideo();
        }
      }
    );
  },
  methods: {
    toggleLike: function() {
      if (this.didUserLikeVideo) this.deleteLike();
      else this.giveLike();
    },
    giveLike: function() {
      this.$http
        .post(this.$serverUrl + "/api/like", {
          userID: this.$store.getters.userId,
          videoID: this.video.id
        })
        .then(res => res.body)
        .then(
          res => {
            this.didUserLikeVideo = true;
            this.video.likes = res.likes;
          },
          error => {
            console.log(error);
          }
        );
    },
    deleteLike: function() {
      this.$http
        .delete(this.$serverUrl + "/api/like", {
          body: {
            userID: this.$store.getters.userId,
            videoID: this.video.id
          }
        })
        .then(res => res.body)
        .then(
          res => {
            this.didUserLikeVideo = false;
            this.video.likes = res.likes;
          },
          error => {
            console.log(error);
          }
        );
    },
    checkIfUserLikedVideo: function() {
      fetchData(
        this.$serverUrl +
          `/api/like/video/${this.video.id}/user/${this.$store.getters.userId}`
      ).then(res => (this.didUserLikeVideo = res.liked));
    }
  }
};
</script>
