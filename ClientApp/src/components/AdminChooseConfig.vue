<template>
  <v-alert
    v-if="ActionSuccessesfullyFlag"
    :text="`${actionText}`"
    type="success"
    style="margin-left: 5em; float: left"
  ></v-alert>
  <v-alert
    v-if="!isUserExists"
    text="Такого пользователя не существует"
    type="error"
    style="margin-left: 5em; float: left"
  ></v-alert>
  <div style="height: 5em"></div>
  <v-sheet width="400" class="mx-auto">
    <v-switch
      v-model="switchmodel"
      hide-details
      inset
      :label="`${switchmodel ? 'Измерители' : 'Технологи'}`"
    ></v-switch>
  </v-sheet>

  <v-container
    id="scroll-target"
    style="max-height: 1000px"
    class="overflow-y-auto"
  >
    <div>
      <v-data-table v-if="switchmodel">
        <h2>Измерители</h2>
        <thead>
          <tr>
            <th class="text-left">Номер</th>
            <th class="text-left">Имя</th>
            <th class="text-left">Фамилия</th>
            <th class="text-left">Логин</th>
            <th class="text-left">Пароль</th>
            <th class="text-left">id</th>
          </tr>
        </thead>
        <tbody>
          <tr v-for="(item, index) in measurers" :key="item.date">
            <td>{{ index + 1 }}</td>
            <td>{{ item.firstName }}</td>
            <td>{{ item.secondName }}</td>
            <td>{{ item.login }}</td>
            <td>{{ item.password }}</td>
            <td>{{ item.id }}</td>
          </tr>
        </tbody>
      </v-data-table>

      <v-data-table v-if="!switchmodel">
        <h2>Технологи</h2>
        <thead>
          <tr>
            <th class="text-left">Номер</th>
            <th class="text-left">Имя</th>
            <th class="text-left">Фамилия</th>
            <th class="text-left">Логин</th>
            <th class="text-left">Пароль</th>
            <th class="text-left">id</th>
          </tr>
        </thead>
        <tbody>
          <tr v-for="(item, index) in technologists" :key="item.date">
            <td>{{ index + 1 }}</td>
            <td>{{ item.firstName }}</td>
            <td>{{ item.secondName }}</td>
            <td>{{ item.login }}</td>
            <td>{{ item.password }}</td>
            <td>{{ item.id }}</td>
          </tr>
        </tbody>
      </v-data-table>
      <div style="height: 3em"></div>
      <div class="container">
        <router-view
          :measurers-delete="measurers"
          :technologists-delete="technologists"
          @remove="userWasRemoved"
          @handleSubmit="userWasEdited"
        ></router-view>
      </div>

      <div style="height: 2em"></div>

      <v-col cols="12">
        <v-btn-toggle v-model="text" rounded="0" group>
          <v-btn value="add" @click="addUser"> Добавить пользователя </v-btn>

          <v-btn value="edit" @click="editUser"> Изменить пользователя </v-btn>

          <v-btn value="remove" @click="deleteUser">
            Удалить пользователя
          </v-btn>
        </v-btn-toggle>
      </v-col>

      <v-col cols="12">
        <v-btn-toggle v-model="text" rounded="0" group>
          <v-btn value="add" @click="goBack"> В меню </v-btn>
        </v-btn-toggle>
      </v-col>
    </div>
  </v-container>
</template>
<script>
import { useRouter } from 'vue-router'

import { ref } from 'vue'
import axios from 'axios'
export default {
  setup() {
    /*
  const getInfo = async () => {
      const promise = await axios.get(
        'https://localhost:3000/api/WorkerData/get',
        {
          params: {
            curentRole: 'Технолог',
          },
        }
      )
      .then(function (response) {
       return(response.data)
    })
    .catch(function (error) {
        console.error(error);
    });   
    }

    

    const fillTechnologistsArray = (a) => {
    for(let i = 0; i < response.data.length; i++){
          technologists.value.push(response.data[i])
        }
      }
    */
    const actionText = ref('')
    const router = useRouter()
    const offsetTop = ref(0)
    const switchmodel = ref(true)
    const technologists = ref([])
    const measurers = ref([])
    const ActionSuccessesfullyFlag = ref(false)
    const isUserExists = ref(true)
    const deleteUser = () => {
      router.push('/Admin/AdminChooseConfig/Delete')
    }
    const editUser = () => {
      router.push('/Admin/AdminChooseConfig/Edit')
    }
    const getInfoTechnologists = async () => {
      const promise = await axios
        .get('https://localhost:3000/api/WorkerData/get', {
          params: {
            curentRole: 'Технолог',
          },
        })
        .then(function (response) {
          // technologists.value.push(response.data[0])

          for (let i = 0; i < response.data.length; i++) {
            technologists.value.push(response.data[i])
          }
          // return response.data;
        })
        .catch(function (error) {
          console.error(error)
        })
    }
    const goBack = () => {
      router.push('/')
    }
    const getInfoMeasurers = async () => {
      const promise = await axios
        .get('https://localhost:3000/api/WorkerData/get', {
          params: {
            curentRole: 'Измеритель',
          },
        })
        .then(function (response) {
          for (let i = 0; i < response.data.length; i++) {
            measurers.value.push(response.data[i])
          }
          // return response.data;
        })
        .catch(function (error) {
          console.error(error)
        })
    }
    const addUser = () => {
      router.push('/Admin/AddUser')
    }
    /*
    const userWasRemoved = (data) => {
      if (data.isUserExists) {
        isUserExists.value = true
        if (data.role === 'Измеритель') {
          measurers.value.splice(data.currentIndex, 1)
        } else technologists.value.splice(data.currentIndex, 1)
        userWasSuccessesfullyRemovedFlag.value = true
      } else isUserExists.value = false
    }
    */
    const userWasRemoved = (data) => {
      if (data.isUserExists) {
        actionText.value = data.message
        isUserExists.value = true
        chooseNewEditCollection(data.role).splice(data.currentIndex, 1)
        ActionSuccessesfullyFlag.value = true
      } else isUserExists.value = false
    }
    const chooseNewEditCollection = (role) => {
      if (role === 'Измеритель') return measurers.value
      else return technologists.value
    }
    const userWasEdited = (data) => {
      if (data.isUserExists) {
        actionText.value = data.message
        ActionSuccessesfullyFlag.value = true
        if (data.role === data.baseRole) {
          //если роль не менялась
          chooseNewEditCollection(data.role)[data.index] = data.worker
        } else {
          //если роль поменялась
          chooseNewEditCollection(data.baseRole).splice(data.index, 1)
          chooseNewEditCollection(data.role).push(data.worker)
        }
        isUserExists.value = true
      } else isUserExists.value = false
    }
    let dasdasd = {
      id: 'ddaaadddd',
      firstName: 'dsada',
      secondName: 'wdasdaasdasd',
      login: 'dasdasdsada',
      password: ' sad13sadasd',
      role: 'Измеритель',
    }
    const onScroll = (e) => {
      offsetTop.value = e.target.scrollTop
    }
    const postInfo = async () => {
      try {
        const response = await axios.post(
          'https://localhost:3000/api/WorkerData/Post',
          dasdasd
        )
        console.log(response)
      } catch (error) {
        console.error(error)
      }
    }
    postInfo()
    getInfoTechnologists()
    getInfoMeasurers()
    // getInfo()

    return {
      technologists,
      measurers,
      switchmodel,
      addUser,
      goBack,
      deleteUser,
      editUser,
      onScroll,
      userWasRemoved,
      userWasEdited,
      isUserExists,
      offsetTop,
      ActionSuccessesfullyFlag,
      actionText,
    }
  },
}
</script>
<style>
v-switch {
  margin: 0 auto;
  display: block;
}
</style>
