<template>
  <Error :code="404" />
</template>

<script>
import Error from "./Error";
export default {
  name: "NotFound",
  components: { Error }
};
</script>
