<template>
  <b-card
    class="video-card mx-2 my-2"
    @click="openVideo(videoId)"
    :title="title | crop"
    title-tag="h5"
    :img-src="imgSrc"
    :img-alt="imgAlt"
    img-top
    bg-variant="warning"
    text-variant="black"
  >
    <b-card-text>
      <div class="d-flex justify-content-between">
        <div class="video-card-text text-black text-sm-center">
          {{ author }}
        </div>
      </div>
    </b-card-text>
  </b-card>
</template>

<script>
export default {
  name: "VideoCard",
  props: {
    title: String,
    imgSrc: String,
    imgAlt: String,
    author: String,
    videoId: Number
  },
  methods: {
    openVideo: function(id) {
      this.$router.push({ name: "Video", params: { index: id } });
    }
  },
  filters: {
    crop: function(value) {
      if (!value) return "";
      value = value.toString();
      if (value.length > 42) return value.slice(0, 40) + "...";
      else return value;
    }
  }
};
</script>

<style scoped></style>
