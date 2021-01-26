<template>
  <div
    class="login-body d-flex justify-content-center align-items-center"
    style="min-width: 400px"
  >
    <div
      class="bg-light shadow px-5 py-5 text-primary d-flex justify-content-center align-items-center flex-column"
    >
      <div class="form-group">
        <label for="email">Email</label>
        <input
          v-model.trim="$v.credential.email.$model"
          :class="{ 'border-danger': $v.credential.email.$error }"
          id="email"
          class="form-control"
          type="email"
        />
        <div v-if="$v.credential.email.$error">
          <div class="text-danger" v-if="!$v.credential.email.required">
            Email jest wymagany.
          </div>
          <div class="text-danger" v-if="!$v.credential.email.email">
            Wpisz poprawny email.
          </div>
          <div class="text-danger" v-if="!$v.credential.email.minLength">
            Email musi zawierać conajmniej 5 znaków.
          </div>
          <div class="text-danger" v-if="!$v.credential.email.maxLength">
            Email może zawierać conajwyżej 35 znaków.
          </div>
        </div>
      </div>
      <div class="form-group">
        <label for="firstName">Imię</label>
        <input
          v-model.trim="$v.credential.firstName.$model"
          :class="{ 'border-danger': $v.credential.firstName.$error }"
          class="form-control"
          id="firstName"
          type="text"
        />
        <div v-if="$v.credential.firstName.$error">
          <div class="text-danger" v-if="!$v.credential.firstName.required">
            Imię jest wymagane.
          </div>
          <div class="text-danger" v-if="!$v.credential.firstName.minLength">
            Imię musi zawierać conajmniej 3 litery.
          </div>
          <div class="text-danger" v-if="!$v.credential.firstName.maxLength">
            Imię może zawierać conajwyżej 15 liter.
          </div>
        </div>
      </div>
      <div class="form-group">
        <label for="lastName">Nazwisko</label>
        <input
          v-model.trim="$v.credential.lastName.$model"
          :class="{ 'border-danger': $v.credential.lastName.$error }"
          id="lastName"
          class="form-control"
          type="text"
        />
        <div v-if="$v.credential.lastName.$error">
          <div class="text-danger" v-if="!$v.credential.lastName.required">
            Nazwisko jest wymagane.
          </div>
          <div class="text-danger" v-if="!$v.credential.lastName.minLength">
            Nazwisko musi zawierać conajmniej 3 litery.
          </div>
          <div class="text-danger" v-if="!$v.credential.lastName.maxLength">
            Nazwisko może zawierać conajwyżej 20 liter.
          </div>
        </div>
      </div>
      <div class="form-group">
        <label for="password">Hasło</label>
        <input
          v-model.trim="$v.credential.password.$model"
          :class="{ 'border-danger': $v.credential.password.$error }"
          id="password"
          class="form-control"
          type="password"
        />
        <div v-if="$v.credential.password.$error">
          <div class="text-danger" v-if="!$v.credential.password.required">
            Hasło jest wymagane.
          </div>
          <div class="text-danger" v-if="!$v.credential.password.minLength">
            Nazwisko musi zawierać conajmniej 6 znaków.
          </div>
        </div>
      </div>
      <div class="mt-3 w-75">
        <b-button @click="register" variant="outline-warning" block pill
          >Zarejestruj się</b-button
        >
        <p class="text-danger text-center mt-4" v-if="submitError">
          Proszę wypełnić poprawnie.
        </p>
      </div>
    </div>
  </div>
</template>

<script>
import {
  required,
  minLength,
  maxLength,
  email
} from "vuelidate/lib/validators";
export default {
  name: "Register",
  data() {
    return {
      credential: {
        email: "",
        firstName: "",
        lastName: "",
        password: ""
      },
      submitError: false
    };
  },
  validations: {
    credential: {
      email: {
        required,
        email,
        minLength: minLength(5),
        maxLength: maxLength(35)
      },
      firstName: {
        required,
        minLength: minLength(3),
        maxLength: maxLength(15)
      },
      lastName: {
        required,
        minLength: minLength(3),
        maxLength: maxLength(20)
      },
      password: {
        required,
        minLength: minLength(6)
      }
    }
  },
  methods: {
    register() {
      this.$v.$touch();
      if (this.$v.$invalid) {
        this.submitError = true;
      } else {
        this.submitError = false;
        this.$http
          .post("user", this.credential)
          .then(() => {
            this.$router.push({ name: "Login" });
          })
          .catch(() => {
            this.submitError = true;
          });
      }
    }
  }
};
</script>

<style scoped>
input {
  min-width: 350px;
}
</style>
