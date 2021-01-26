<template>
  <div
    class="d-flex w-100 h-100 justify-content-center align-items-center text-danger text-center"
  >
    <h1>{{ message }}</h1>
  </div>
</template>

<script>
export default {
  name: "Error",
  data() {
    return {
      message: ""
    };
  },
  props: {
    code: Number
  },
  created() {
    this.message = `Błąd ${this.code}: `;
    switch (this.code) {
      case 404:
        this.message += "Nie znaleziono strony";
        break;
      case 403:
        this.message += "Brak odpowiednich uprawnień";
        break;
      case 500:
        this.message += "Błąd po stronie serwera";
        break;
      case 400:
        this.message += "Złe żądanie";
        break;
      case 503:
        this.message += "Błąd po stronie serwera";
        break;
      default:
        this.message += "Nieoczekiwany błąd";
        break;
    }
  }
};
</script>
