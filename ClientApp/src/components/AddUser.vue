<template>
  <div style="height: 5em"></div>
  <v-alert
    v-if="DeliverySuccsess"
    closable
    text="Данные успешно загружены на сервер"
    type="success"
    style="margin-left: 5em; float: left"
  ></v-alert>

  <v-sheet width="300" class="mx-auto">
    <form @submit.prevent="submit">
      <v-text-field
        v-model="name.value.value"
        :counter="10"
        :error-messages="name.errorMessage.value"
        label="Имя"
      ></v-text-field>

      <v-text-field
        v-model="surname.value.value"
        :counter="7"
        :error-messages="surname.errorMessage.value"
        label="Фамилия"
      ></v-text-field>

      <v-text-field
        v-model="login.value.value"
        :error-messages="login.errorMessage.value"
        label="Логин"
      ></v-text-field>

      <v-text-field
        v-model="password.value.value"
        :error-messages="password.errorMessage.value"
        label="Пароль"
      ></v-text-field>

      <v-select
        v-model="select.value.value"
        :items="items"
        :error-messages="select.errorMessage.value"
        label="Роль"
      ></v-select>

      <v-btn class="me-4" type="Submit"> Создать </v-btn>

      <v-btn @click="handleReset"> Очистить </v-btn>
    </form>
    <v-btn block class="mt-10" @click="returnHome">Назад</v-btn>
  </v-sheet>
</template>

<script setup>
import crc32 from 'crc/crc32'
import axios from 'axios'
import { ref } from 'vue'
import { useField, useForm } from 'vee-validate'
import router from '@/router/index'
const { handleSubmit, handleReset } = useForm({
  validationSchema: {
    name(value) {
      DeliverySuccsess.value = false
      if (value?.length >= 2) return true

      return 'Имя должно состоять минимум из двух символов'
    },
    surname(value) {
      if (value?.length >= 2) return true

      return 'Фамилия должна состоять минимум из двух символов'
    },
    login(value) {
      if (value?.length > 12) return 'Логин слишком длинный'
      if (value?.length < 5) return 'Логин слишком короткий'
      if (value?.match(/[^A-Za-z0-9_@-]/))
        return 'В логине возможны только латинские буквы, цифры и знаки: @,-,_ '

      return true
    },
    password(value) {
      if (value?.length > 12) return 'Пароль слишком длинный'
      if (value?.length < 5) return 'Пароль слишком короткий'
      if (value?.match(/[^A-Za-z0-9_@-]/))
        return 'В пароле возможны только латинские буквы, цифры и знаки: @,-,_ '

      return true
    },
    select(value) {
      if (value) return true

      return 'Выберете роль пользователя'
    },
  },
})
const DeliverySuccsess = ref(false)
const name = useField('name')
const surname = useField('surname')
const login = useField('login')
const password = useField('password')
const select = useField('select')

const items = ref(['Администратор', 'Измеритель', 'Технолог'])
const upload = (values) => {
  let worker = {
    firstName: values.name,
    secondName: values.surname,
    login: values.login,
    role: values.select,
    password: values.password,
    id: crc32(new Date().toString()).toString(16),
  }
  return worker
}

const returnHome = () => {
  router.push('/Admin/AdminChooseConfig')
}
const submit = handleSubmit(async (values) => {
  try {
    const response = await axios.post(
      'https://localhost:3000/api/WorkerData/Post',
      upload(values)
    )
    DeliverySuccsess.value = true
    console.log(response)
  } catch (error) {
    console.error(error)
  }
})
</script>
