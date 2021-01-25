<template>
  <div>
    <div class="bg-light text-primary py-5">
      <b-container>
        <div class="text-left">
          <h1 class="text-center font-weight-bold">Dodaj film</h1>
        </div>
      </b-container>
    </div>
    <b-container>
      <b-form>
        <b-row class="mt-5">
          <b-col md="6">
            <b-form-group label="Tytuł" label-for="title">
              <b-form-input
                id="title"
                v-model="video.title"
                type="text"
                :state="!$v.video.title.$invalid"
              >
              </b-form-input>
              <b-form-invalid-feedback id="title-live-feedback">
                Tytuł jest wymagany i może mieć maksymalnie 255 znaków.
              </b-form-invalid-feedback>
            </b-form-group>
            <b-form-group label="Opis" label-for="description">
              <b-form-textarea
                id="description"
                v-model="video.description"
                rows="3"
                max-rows="10"
                :state="!$v.video.description.$invalid"
              ></b-form-textarea>
              <b-form-invalid-feedback id="description-live-feedback">
                Opis jest wymagany i może mieć maksymalnie 5000 znaków.
              </b-form-invalid-feedback>
            </b-form-group>
            <b-form-group label="Kategoria" label-for="category">
              <b-form-select
                id="category"
                class="mb-2 mr-sm-2 mb-sm-0"
                v-model="video.categoryId"
              >
                <b-form-select-option
                  v-for="category in categories"
                  :key="category.id"
                  :value="category.id"
                  >{{ category.name }}</b-form-select-option
                >
              </b-form-select>
            </b-form-group>
            <img
              :src="
                newImageSrc != null
                  ? newImageSrc
                  : `${$serverUrl}Resources/Images/${video.image}`
              "
              style="width: 300px;"
            />
            <b-form-file
              class="mt-2 w-75"
              id="imageFile"
              accept="image/jpeg"
              v-model="newImageFile"
              placeholder="Wybierz nowe zdjęcie albo upuść je tutaj..."
              drop-placeholder="Upuść zdjęcie tutaj..."
              browse-text="Przeglądaj"
            ></b-form-file>
          </b-col>
          <b-col md="6" class="d-flex flex-column align-items-end">
            <video
              :src="
                newVideoSrc != null
                  ? newVideoSrc
                  : `${$serverUrl}Resources/Videos/${video.uri}`
              "
              controls
              type="video/mp4"
              width="100%"
              height="auto"
            ></video>
            <b-form-file
              class="mt-2"
              id="videoFile"
              accept="video/mp4"
              v-model="newVideoFile"
              placeholder="Wybierz nowe wideo albo upuść je tutaj..."
              drop-placeholder="Upuść wideo tutaj..."
              browse-text="Przeglądaj"
            ></b-form-file>
          </b-col>
        </b-row>
        <div class="text-center">
          <h4 class="text-danger">{{ error }}</h4>
          <b-button
            id="submitButton"
            @click="saveNewVideo"
            class="w-50 mt-5"
            squared
            variant="primary"
            >Opublikuj</b-button
          >
        </div>
      </b-form>
    </b-container>
  </div>
</template>

<script>
import { required, maxLength } from "vuelidate/lib/validators";

export default {
  name: "NewVideo",
  data() {
    return {
      video: {
        userId: 0,
        uri: "",
        categoryId: 0,
        title: "",
        description: "",
        likes: 0,
        image: ""
      },
      categories: [],
      newImageFile: null,
      newVideoFile: null,
      error: ""
    };
  },
  created() {
    this.video.userId = this.$store.getters.userId;
    this.$http.get("api/category").then(res => {
      this.categories = res.data;
    });
  },
  computed: {
    newImageSrc: function() {
      if (this.newImageFile != null)
        return URL.createObjectURL(this.newImageFile);
      else return null;
    },
    newVideoSrc: function() {
      if (this.newVideoFile != null)
        return URL.createObjectURL(this.newVideoFile);
      else return null;
    }
  },
  methods: {
    saveNewVideo: function() {
      if (this.$v.$invalid || this.video.categoryId < 1) {
        this.error = "Niepoprawne dane!";
        return;
      }
      if (this.newImageFile) {
        this.video.image = this.video.id + ".jpg";
      }

      if (this.newVideoFile) {
        this.video.uri = this.video.id + ".mp4";
      }

      this.$http.post("video", this.video).then(
        response => {
          const id = response.data.id;

          if (this.newImageFile) {
            this.uploadFile(this.newImageFile, "image", id + ".jpg");
          }

          if (this.newVideoFile)
            this.uploadFile(this.newVideoFile, "video", id + ".mp4");

          this.$router.push({ name: "MyVideos" });
        },
        error => {
          console.log(error);
        }
      );
    },
    uploadFile: function(file, fileType, filename) {
      if (file.length === 0) {
        return;
      }

      const formData = new FormData();
      formData.append("file", file, filename);

      this.$http
        .post(`file/${fileType}`, formData)
        .then(res => {
          return res;
        })
        .catch(err => {
          console.log(err);
        });
    }
  },
  validations: {
    video: {
      title: {
        required,
        maxLength: maxLength(255)
      },
      description: {
        required,
        maxLength: maxLength(5000)
      }
    }
  }
};
</script>
