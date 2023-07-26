<template>
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
    <v-btn block class="mt-10" @click="returnHome()">Назад</v-btn>
  </v-sheet>
</template>

<script>
import crc32 from 'crc/crc32'
import axios from 'axios'
import { ref } from 'vue'
import { useField, useForm } from 'vee-validate'
import router from '@/router/index'
export default {
  props: {
    measurersDelete: Array,
    technologistsDelete: Array,
  },
  emits: ['add'],
  setup(props, { emit }) {
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
          if (logins.includes(value))
            return 'Пользователь с таким логином уже существует'
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
    const name = ref(useField('name'))
    const surname = ref(useField('surname'))
    const login = ref(useField('login'))
    const password = ref(useField('password'))
    const select = ref(useField('select'))
    const workers = props.measurersDelete.concat(props.technologistsDelete)
    const logins = workers.map((obj) => {
      return obj.login
    })

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
        emit('add', {
          //доделать эмит
          role: select.value,
          newUser: upload(values),
          message: 'Пользователь успешно создан',
        })
        console.log(response)
        returnHome()
      } catch (error) {
        console.error(error)
      }
    })
    return {
      handleSubmit,
      handleReset,
      name,
      surname,
      login,
      password,
      select,
      items,
      returnHome,
      submit,
    }
  },
}
</script>
