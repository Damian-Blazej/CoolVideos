<template>
  <div
    v-if="!deleted"
    class="video-info w-75 py-1 pl-1 pr-3 mt-2 d-flex justify-content-between align-items-center"
  >
    <img
      class="img-video-link"
      @click="openVideo"
      :src="`${$serverUrl}/Resources/Images/${imgSrc}`"
      style="height: 100px;"
    />
    <h5 class="mb-0">{{ title }}</h5>
    <div
      class="video-info-buttons d-flex justify-content-between align-items-center"
    >
      <b-button pill variant="warning">😎🤙 {{ likes }}</b-button>
      <b-button @click="showConfirmBox" variant="danger" pill
        ><b-icon-trash></b-icon-trash> Delete</b-button
      >
      <b-button variant="primary" pill :to="'/video/my/edit/' + videoId"
        ><b-icon-file-text></b-icon-file-text> Edit</b-button
      >
    </div>
  </div>
</template>
.
<script>
export default {
  name: "VideoInfo",
  data() {
    return {
      dialog: false,
      deleted: false
    };
  },
  props: {
    title: String,
    imgSrc: String,
    description: String,
    likes: Number,
    videoId: Number
  },
  methods: {
    openVideo: function() {
      this.$router.push({ path: `/video/${this.videoId}` });
    },
    deleteVideo() {
      this.$http.delete(this.$serverUrl + `/api/video/${this.videoId}`).then(
        response => {
          if (response.ok) this.deleted = true;
        },
        error => {
          console.log("Error deleteting video", error.toString());
        }
      );
    },
    showConfirmBox() {
      this.$bvModal
        .msgBoxConfirm(`Czy na pewno chcesz usunąć wideo "${this.title}"?`, {
          size: "sm",
          buttonSize: "sm",
          okVariant: "danger",
          okTitle: "YES",
          cancelTitle: "NO",
          footerClass: "p-2",
          centered: true
        })
        .then(value => {
          if (value) {
            this.deleteVideo();
          }
        });
    }
  }
};
</script>
