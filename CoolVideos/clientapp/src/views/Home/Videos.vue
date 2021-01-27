<template>
  <div>
    <div class="bg-light text-primary py-5">
      <b-container>
        <b-row>
          <b-col md="12">
            <div class="text-center">
              <h1 class="font-weight-bold">Odkryj nasze cool filmy</h1>
              <form
                v-on:submit.prevent="searchVideosByQuery"
                v-on:keyup.enter="submit"
              >
                <b-input-group class="mt-3 w-50 mx-auto" size="lg">
                  <b-form-input v-model="searchQuery"></b-form-input>
                  <b-input-group-append>
                    <b-button type="submit" variant="primary"
                      ><b-icon icon="search"></b-icon
                    ></b-button>
                  </b-input-group-append>
                </b-input-group>
              </form>
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
            class="d-flex justify-content-center align-items-center flex-wrap my-4"
          >
            <b-spinner
              v-if="areNewestVideosLoading"
              variant="warning"
              label="Ładowanie..."
            ></b-spinner>
            <template v-else>
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
            </template>
          </div>
          <div class="d-flex justify-content-center">
            <b-button
              id="moreVideos"
              @click="searchVideosAll"
              variant="warning"
              pill
              class="px-5"
              >Więcej filmów...</b-button
            >
          </div>
        </div>
        <div class="mt-5">
          <div class="mt-3 text-center">
            <h2 class="font-weight-bold">Kategorie wideo</h2>
            <b-nav pills align="center" variant="secondary">
              <b-nav-item
                class="category-pill"
                :active="category.id == selectedCategory"
                @click="changeCategory(category.id)"
                v-for="category in categories"
                :key="category.id"
                >{{ category.name }}</b-nav-item
              >
            </b-nav>
          </div>
          <div
            class="d-flex justify-content-center align-items-center flex-wrap my-4"
          >
            <b-spinner
              v-if="areCategoriesVideosLoading"
              variant="warning"
              label="Ładowanie..."
            ></b-spinner>
            <template v-else>
              <VideoCard
                v-for="video in videosByCategory"
                :key="video.id"
                :video-id="video.id"
                :title="video.title"
                :img-src="`${$serverUrl}Resources/Images/${video.image}`"
                :img-alt="video.title + ' image'"
                :author="video.user.firstName + ' ' + video.user.lastName"
              >
              </VideoCard>
            </template>
          </div>
          <div class="d-flex justify-content-center">
            <b-button
              id="moreVideosOfCategory"
              @click="searchVideosByCategory"
              variant="warning"
              pill
              class="px-5"
              >Więcej z tej kategorii...</b-button
            >
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
  created() {
    this.$http.get("video/latest?n=6").then(res => {
      this.areNewestVideosLoading = false;
      this.videos = res.data;
    });
    this.changeCategory(1);
  },
  beforeCreate() {
    this.$http.get("category").then(res => {
      this.categories = res.data;
    });
  },
  data() {
    return {
      categories: [],
      videos: [],
      selectedCategory: 1,
      numFetchByCategory: 3,
      videosByCategory: [],
      searchQuery: "",
      areNewestVideosLoading: true,
      areCategoriesVideosLoading: true
    };
  },
  methods: {
    changeCategory: function(id) {
      this.areCategoriesVideosLoading = true;
      this.selectedCategory = id;
      this.$http.get(`video/category/${id}?n=3`).then(res => {
        this.areCategoriesVideosLoading = false;
        this.videosByCategory = res.data;
      });
    },
    searchVideosByQuery() {
      this.$router.push({
        name: "ListVideos",
        query: { query: this.searchQuery }
      });
    },
    searchVideosByCategory() {
      this.$router.push({
        name: "ListVideos",
        query: { categoryID: this.selectedCategory }
      });
    },
    searchVideosAll() {
      this.$router.push({ name: "ListVideos" });
    }
  }
};
</script>
