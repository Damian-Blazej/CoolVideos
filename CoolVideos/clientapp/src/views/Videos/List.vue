<template>
  <div>
    <div class="bg-light text-primary py-5">
      <b-container>
        <div class="text-center">
          <h1 class="font-weight-bold">Czego szukasz?</h1>
          <form
            v-on:submit.prevent="
              searchVideos(searchQuery, selectedCategory, 1, selectedPageSize)
            "
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
        <div class="mt-3 text-center">
          <b-button
            @click="showSearchOptions = !showSearchOptions"
            variant="outline-primary"
            class="font-weight-bold"
          >
            Pokaż opcje wyszukiwania
          </b-button>
          <vue-slide-up-down :active="showSearchOptions" :duration="1000">
            <div class="mt-3">
              <b-nav pills align="center" variant="secondary">
                <b-nav-item
                  :active="category.id === selectedCategory"
                  @click="changeCategory(category.id)"
                  v-for="category in categories"
                  :key="category.id"
                  >{{ category.name }}</b-nav-item
                >
              </b-nav>
              <div
                class="d-flex justify-content-center align-items-center mt-3"
              >
                <h5 class="font-weight-bold mr-3">Liczba pozycji na stronę</h5>
                <b-form-select
                  id="selectPageSize"
                  v-model="selectedPageSize"
                  :options="pageSizeOptions"
                  class="text-center"
                  style="width: 75px;"
                ></b-form-select>
              </div>
            </div>
          </vue-slide-up-down>
        </div>
      </b-container>
    </div>

    <b-container>
      <div class="mt-3 text-center py-5">
        <h2 class="font-weight-bold">Wyniki wyszukiwania</h2>
        <div
          class="d-flex justify-content-center align-items-center flex-wrap mt-4"
        >
          <b-spinner
            v-if="isLoading"
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
      </div>
      <div class="mt-3 d-flex justify-content-around align-items-center">
        <b-button
          @click="prevPage"
          :disabled="currentPage <= 1"
          variant="outline-primary"
          size="sm"
        >
          <b-icon icon="chevron-double-left"></b-icon>
        </b-button>
        <b-button
          @click="nextPage"
          :disabled="currentNumOfPages <= currentPage"
          variant="outline-primary"
          size="sm"
        >
          <b-icon icon="chevron-double-right"></b-icon>
        </b-button>
      </div>
    </b-container>
  </div>
</template>

<script>
import VideoCard from "../../components/VideoCard";

export default {
  name: "Searchvideos",
  components: {
    VideoCard
  },
  created() {
    this.$http.get("category").then(res => {
      this.categories = this.categories.concat(res.data);
    });

    if (this.$route.query.query) this.searchQuery = this.$route.query.query;

    if (this.$route.query.categoryID)
      this.selectedCategory = this.$route.query.categoryID;

    this.searchVideos(
      this.searchQuery,
      this.selectedCategory,
      1,
      this.currentPageSize
    );
  },
  data() {
    return {
      videos: [],
      showSearchOptions: false,
      selectedCategory: 0,
      categories: [
        {
          id: 0,
          name: "Wszystkie"
        }
      ],
      searchQuery: "",
      selectedPageSize: 15,
      pageSizeOptions: [
        { value: 15, text: "15" },
        { value: 30, text: "30" },
        { value: 45, text: "45" }
      ],
      currentPage: 1,
      currentPageSize: 15,
      currentTotalVideoCount: 1,
      currentCategory: 0,
      currentQuery: "",
      isLoading: true
    };
  },
  methods: {
    changeCategory: function(id) {
      this.selectedCategory = id;
    },
    searchVideos(query, category, pageNum, pageSize) {
      this.isLoading = true;
      let searchQuery = query.trim();
      this.currentPage = pageNum;
      this.currentPageSize = pageSize;
      this.currentCategory = category;
      this.currentQuery = query;
      let url = `video/search?categoryID=${category}&pageSize=${pageSize}&pageNumber=${pageNum}`;
      if (searchQuery) url += "&query=" + searchQuery;

      this.$http.get(url).then(res => {
        this.isLoading = false;
        this.videos = res.data.videos;
        this.currentTotalVideoCount = res.data.videoListCount;
      });
    },
    prevPage() {
      if (this.currentPage > 1)
        this.searchVideos(
          this.currentQuery,
          this.currentCategory,
          --this.currentPage,
          this.currentPageSize
        );
    },
    nextPage() {
      if (
        this.currentPage <
        Math.ceil(this.currentTotalVideoCount / this.currentNumOfPages)
      )
        this.searchVideos(
          this.currentQuery,
          this.currentCategory,
          ++this.currentPage,
          this.currentPageSize
        );
    }
  },
  computed: {
    currentNumOfPages: function() {
      return Math.ceil(this.currentTotalVideoCount / this.currentPageSize);
    }
  }
};
</script>
