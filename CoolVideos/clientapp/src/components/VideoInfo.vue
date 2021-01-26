<template>
  <b-list-group-item
    v-if="!deleted"
    class="d-flex justify-content-between align-items-center"
  >
    <b-img
      rounded="true"
      @click="openVideo"
      :src="`${$serverUrl}Resources/Images/${imgSrc}`"
      height="100"
      width="100"
      style="object-fit: contain;"
    />
    <div class="d-flex align-items-center">
      <h5 class="mb-0">{{ title }}</h5>
      <b-badge class="ml-3 p-1" variant="warning" pill>😎🤙 {{ likes }}</b-badge>
    </div>
    <div>
      <b-button @click="showConfirmBox" variant="danger" pill class="mr-3" size="sm"
        ><b-icon-trash></b-icon-trash> Delete</b-button
      >
      <b-button
        variant="primary"
        size="sm"
        pill
        :to="{ name: 'EditVideo', params: { id: videoId.toString() } }"
        ><b-icon-file-text></b-icon-file-text> Edit</b-button
      >
    </div>
  </b-list-group-item>
</template>

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
      this.$router.push({
        name: "Video",
        params: { index: this.videoId.toString() }
      });
    },
    deleteVideo() {
      this.$http.delete(`video/${this.videoId}`).then(
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
          cancelVariant: "primary",
          okTitle: "Tak",
          cancelTitle: "Nie",
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
