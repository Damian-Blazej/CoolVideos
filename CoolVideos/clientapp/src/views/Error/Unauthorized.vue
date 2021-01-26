<template>
  <Error :code="403" />
</template>

<script>
import Error from "../../components/Error";
export default {
  name: "Unauthorized",
  components: { Error }
};
</script>
