@@ -0,0 +1,90 @@
<template>
  <div style="height: 20em"></div>
  <h1>Личный кабинет {{ role }}</h1>
  <div style="height: 2em"></div>
  <v-sheet width="400" class="mx-auto">
    <v-form fast-fail @submit.prevent>
      <v-text-field
        v-model="firstName"
        label="Имя"
        :rules="firstNameRules"
      ></v-text-field>

      <v-text-field
        v-model="lastName"
        label="Фамилия"
        :rules="lastNameRules"
      ></v-text-field>

      <v-btn type="submit" block class="mt-2" @click="enterInAccount"
        >Войти</v-btn
      >

      <v-btn block class="mt-10" @click="returnHome">Назад</v-btn>
    </v-form>
  </v-sheet>
</template>

<script>
import { ref, computed } from 'vue'
import { useRoute, useRouter } from 'vue-router'
import { useStore } from 'vuex'
export default {
  setup() {
    const store = useStore()
    const firstName = ref('')
    const lastName = ref('')
    const route = useRoute() //router
    const router = useRouter()
    const path = computed(() => route.path)

    const enterInAccount = () => {
      window.localStorage.setItem('firstName', firstName.value)
      window.localStorage.setItem('lastName', lastName.value)
      router.push('/' + path.value.split('/')[2])
    }

    const returnHome = () => {
      router.push('/')
    }
    const getRole = (path) => {
      switch (path.value) {
        case '/Login/Measurer':
          return 'Измерителя'

        case '/Login/Admin':
          return 'Администратора'

        case '/Login/Technologist':
          return 'Технолога'

        default:
          alert('Ошибка')
      }
    }
    let role = getRole(path)

    return {
      returnHome,
      enterInAccount,
      path,
      role,
      firstName,
      firstNameRules: [
        (value) => {
          if (value?.length > 3) return true

          return 'Имя не может быть короче трех символов.'
        },
      ],
      lastName,
      lastNameRules: [
        (value) => {
          if (/[^0-9]/.test(value)) return true

          return 'Фамилия не должна содержать цифр.'
        },
      ],
    }
  },
}
</script>
