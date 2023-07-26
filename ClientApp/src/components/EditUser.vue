<template>
  <v-alert
    v-if="DeliverySuccsess"
    closable
    text="Данные успешно загружены на сервер"
    type="success"
    style="margin-left: 5em; float: left"
  ></v-alert>

  <v-sheet v-if="!isIdSubmited" width="300" class="mx-auto">
    <v-form @submit.prevent>
      <v-text-field
        v-model="Id"
        :rules="rules"
        label="Введите id для поиска"
      ></v-text-field>
      <v-btn type="remove" block class="mt-2" @click="submitId(id)"
        >Поиск</v-btn
      >
    </v-form>
  </v-sheet>

  <v-sheet v-if="isIdSubmited" width="300" class="mx-auto">
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

      <v-btn class="me-4" type="Submit"> Изменить </v-btn>

      <v-btn @click="submitId(Id)"> Отменить </v-btn>
    </form>
  </v-sheet>
</template>

<script>
import { useRouter } from 'vue-router'
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
  emits: ['handleSubmit'],
  setup(props, { emit }) {
    const Id = ref()
    const isIdSubmited = ref(false)
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
    const router = useRouter()
    const DeliverySuccsess = ref(false)
    let deletedRole
    let startRole = ''
    let indexEdited = ''
    let isUserExists = true
    const name = useField('name')
    const surname = useField('surname')
    const login = useField('login')
    const password = useField('password')
    const select = useField('select')
    const editedUser = ref('')
    const items = ref(['Админ', 'Измеритель', 'Технолог'])
    const upload = (values) => {
      let worker = {
        firstName: values.name,
        secondName: values.surname,
        login: values.login,
        role: values.select,
        password: values.password,
        id: editedUser.value.id, //не забыть поменять + сделать axios
      }
      return worker
    }

    const roleChanged = async (values) => {
      let workerToDelete = upload(values)
      try {
        workerToDelete.role = startRole
        await axios.post(
          'https://localhost:3000/api/WorkerData/Delete',
          workerToDelete
        )
        workerToDelete.role = values.select
        await axios.post(
          'https://localhost:3000/api/WorkerData/Post',
          workerToDelete
        )
      } catch (error) {
        console.error(error)
      }
    }
    const returnHome = () => {
      router.push('/Admin/AdminChooseConfig')
    }
    const closeComponent = () => {
      router.push('/Admin/AdminChooseConfig')
    }
    const makeFullEmits = (values) => {
      emit('handleSubmit', {
        //доделать эмит
        role: values.select,
        baseRole: startRole,
        worker: upload(values),
        index: indexEdited,
        isUserExists: isUserExists,
        message: 'Пользователь успешно изменен',
      })
    }
    const makeErrorEmit = () => {
      emit('handleSubmit', {
        isUserExists: isUserExists,
      })
    }
    const submit = handleSubmit(async (values) => {
      makeFullEmits(values)
      if (isUserExists) {
        if (startRole === values.role) {
          try {
            const response = await axios.post(
              'https://localhost:3000/api/WorkerData/Update',
              upload(values)
            )
            DeliverySuccsess.value = true
            console.log(response)
          } catch (error) {
            console.error(error)
          }
        } else {
          roleChanged(values)
        }

        closeComponent()
      }
    })

    const submitId = () => {
      if (findWorkerById(Id.value)) {
        isIdSubmited.value = true
        editedUser.value = findWorkerById(Id.value)
        name.value.value = editedUser.value.firstName
        surname.value.value = editedUser.value.secondName
        login.value.value = editedUser.value.login
        password.value.value = editedUser.value.password
        select.value.value = editedUser.value.role
        startRole = editedUser.value.role
        indexEdited = findIndex(editedUser.value)
      } else {
        makeErrorEmit()
      }
    }

    const findIndex = (workerToDelete) => {
      let currentRoleArray
      if (deletedRole === 'Измеритель') {
        currentRoleArray = props.measurersDelete
      } else {
        currentRoleArray = props.technologistsDelete
      }
      return currentRoleArray.indexOf(workerToDelete)
    }

    const findWorkerById = (Id) => {
      const workers = props.measurersDelete.concat(props.technologistsDelete)
      const workerToDelete = workers.filter((worker) => worker.id === Id)[0]
      if (workerToDelete) {
        deletedRole = workerToDelete.role
        isUserExists = true
        return workerToDelete
      } else {
        isUserExists = false
        return false
      }
    }

    return {
      submit,
      returnHome,
      name,
      surname,
      login,
      password,
      select,
      items,
      handleReset,
      handleSubmit,
      submitId,
      isIdSubmited,
      Id,
    }
  },
}
</script>
