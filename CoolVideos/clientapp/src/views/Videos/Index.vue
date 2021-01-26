<template>
  <b-container>
    <div class="mt-5 d-flex flex-column align-items-center">
      <h2 class="mb-4">{{ video.title }}</h2>
      <video
        :src="`${$serverUrl}Resources/Videos/${video.uri}`"
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
export default {
  data() {
    return {
      video: {},
      didUserLikeVideo: true
    };
  },
  beforeCreate() {
    this.$http.get("video/" + this.$route.params.index).then(res => {
      if (res.status === 404) {
        this.$router.push({ name: "PageNotFound" });
      } else {
        this.video = res.data;
        if (this.$store.getters.userId) {
          this.checkIfUserLikedVideo();
        }
      }
    });
  },
  methods: {
    toggleLike: function() {
      if (!this.$store.getters.userId) {
        this.$router.push({ name: "Login" });
        return;
      }
      if (this.didUserLikeVideo) this.deleteLike();
      else this.giveLike();
    },
    giveLike: function() {
      this.$http
        .post("like", {
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
        .delete("like", {
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
      this.$http
        .get(`like/video/${this.video.id}/user/${this.$store.getters.userId}`)
        .then(res => (this.didUserLikeVideo = res.data.liked));
    }
  }
};
</script>
