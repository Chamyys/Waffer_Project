@@ -0,0 +1,90 @@
<template>
  <v-alert
    v-if="!isLoginCorrect"
    text="Неверный логин/пароль"
    type="error"
    style="margin-left: 5em; float: left"
  ></v-alert>
  <div style="height: 20em"></div>
  <h1>Личный кабинет - {{ role }}</h1>
  <div style="height: 2em"></div>
  <v-sheet width="400" class="mx-auto">
    <v-form fast-fail @submit.prevent>
      <v-text-field
        v-model="login"
        label="Логин"
        :rules="firstNameRules"
      ></v-text-field>

      <v-text-field
        v-model="password"
        label="Пароль"
        :rules="lastNameRules"
      ></v-text-field>

      <v-btn type="submit" block class="mt-2" @click="checkEnterData"
        >Войти</v-btn
      >

      <v-btn block class="mt-10" @click="returnHome">Назад</v-btn>
    </v-form>
  </v-sheet>
</template>

<script>
import { ref, computed } from 'vue'
import axios from 'axios'
import { useRoute, useRouter } from 'vue-router'
import { useStore } from 'vuex'
export default {
  setup() {
    const store = useStore()
    const login = ref('')
    const password = ref('')
    const route = useRoute() //router
    let succsessUserIndex = ''
    const router = useRouter()
    const path = computed(() => route.path)
    const currentUsersArray = ref([])
    const isLoginCorrect = ref(true)
    const getCurrentRoleAccounts = async () => {
      await axios
        .get('https://localhost:3000/api/WorkerData/get', {
          params: {
            curentRole: role,
          },
        })
        .then(function (response) {
          for (let i = 0; i < response.data.length; i++) {
            currentUsersArray.value.push(response.data[i])
          }
        })
        .catch(function (error) {
          console.error(error)
        })
    }

    const checkEnterData = () => {
      let currentUser = currentUsersArray.value.find(
        (obj) => obj.login === login.value
      )
      if (
        currentUser !== undefined &&
        currentUser.password === password.value
      ) {
        succsessUserIndex = currentUsersArray.value.indexOf(
          currentUsersArray.value.find((obj) => obj.login === login.value)
        )
        enterInAccount()
      } else isLoginCorrect.value = false
    }
    const enterInAccount = () => {
      window.localStorage.setItem(
        'firstName',
        currentUsersArray.value[succsessUserIndex].firstName
      )
      window.localStorage.setItem(
        'lastName',
        currentUsersArray.value[succsessUserIndex].secondName
      )
      router.push('/' + path.value.split('/')[2])
    }

    const returnHome = () => {
      router.push('/')
    }
    const getRole = (path) => {
      switch (path.value) {
        case '/Login/Measurer':
          return 'Измеритель'

        case '/Login/Admin':
          return 'Администратор'

        case '/Login/Technologist':
          return 'Технолог'

        default:
          alert('Ошибка')
      }
    }

    let role = getRole(path)
    getCurrentRoleAccounts()
    return {
      returnHome,
      checkEnterData,
      path,
      isLoginCorrect,

      role,
      login,
      firstNameRules: [
        (value) => {
          if (value?.length > 3) return true

          return 'Имя не может быть короче трех символов.'
        },
      ],
      password,
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
