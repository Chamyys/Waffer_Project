@@ -0,0 +1,181 @@
<template>
  <v-card>
    <v-layout>
      <v-navigation-drawer
        v-model="drawer"
        :rail="rail"
        permanent
        @click="rail = false"
      >
        <v-list-item
          prepend-avatar="https://katalog-rek.ru/upload/iblock/dbc/SVR-LOGO.png"
          title="Имя сотрудника"
          nav
        >
          <template #append>
            <v-btn
              variant="text"
              icon="mdi-chevron-left"
              @click.stop="rail = !rail"
            ></v-btn>
          </template>
        </v-list-item>

        <v-divider></v-divider>

        <v-list density="compact" nav>
          <v-list-item
            prepend-icon="mdi-magnify"
            title="Поиск пластины"
            value="home"
          ></v-list-item>
          <v-list-item
            prepend-icon="mdi-download"
            title="Загрузка измерений"
            value="account"
          ></v-list-item>
          <v-list-item
            prepend-icon="mdi-help"
            title="Этот пункт в разработке"
            value="users"
          ></v-list-item>
        </v-list>
      </v-navigation-drawer>
      <v-main style="height: 80em">
        <h1>Номер пластины</h1>
        <v-data-table>
          <thead>
            <tr>
              <th class="text-left">Значение</th>
              <th class="text-left">Этап 1</th>
              <th class="text-left">Этап 2</th>
              <th class="text-left">Этап 3</th>
              <th class="text-left">Этап 4</th>
            </tr>
          </thead>
          <tbody>
            <tr v-for="(item, index) in arr" :key="item.date">
              <td>{{ index + 1 }}</td>
              <td>{{ item.temperatureC }}</td>
              <td>{{ item.temperatureF }}</td>
              <td>{{ item.summary }}</td>
              <td>{{ item.date }}</td>
            </tr>
          </tbody>
        </v-data-table>

        <div>
          <div>
            <div></div>

            <div></div>
          </div>
        </div>
        <div style="height: 5em"></div>
        <v-sheet width="400" class="mx-auto">
          <v-btn type="submit" block class="mt-2" @click="postInfo"
            >Отправить что-то на сервер</v-btn
          >
          <v-btn type="submit" block class="mt-2" @click="getInfo"
            >Получить что-то с сервера</v-btn
          >
          <v-btn type="submit" block class="mt-10" @click="goBack">Выход</v-btn>
        </v-sheet>
      </v-main>
    </v-layout>
  </v-card>
</template>

<!--

  
-->
<script>
import { ref } from 'vue'
import axios from 'axios'
import { useStore } from 'vuex'
import { useRouter } from 'vue-router'

export default {
  setup() {
    const rail = ref(true)
    const drawer = ref(true)
    const store = useStore()
    const arr = ref([])
    const router = useRouter()

    let currentrows = 5

    const entity = {
      id: '12',
      name: 'finally',

      temperature: 15,

      location: 'Moskow',

      isRainyToday: true,
    }
    const postInfo = async () => {
      try {
        const response = await axios.post(
          'https://localhost:3000/api/WeatherForecast/Post',
          entity
        )
        console.log(response)
      } catch (error) {
        console.error(error)
      }
    }

    const goBack = () => {
      router.push('/')
    }
    const getInfo = async () => {
      const promise = axios.get(
        'https://localhost:3000/api/WeatherForecast/get'
      )
      console.log(promise)

      const dataPromise = await promise.then((response) => response.data)

      arr.value.length = 0
      store.dispatch('cleanWeatherArray')
      store.dispatch('createWeatherArray', dataPromise) //заполнение vuex

      arr.value = [...dataPromise.slice(0, currentrows)]

      console.log(arr)

      //const totalNotes = store.state.arrayofWeather //получение из vuex

      // console.log(totalNotes[0][0].date) //вызов поля объекта

      //store.dispatch('createWeatherArray', mainArray) //заполнение vuex

      ref.value = dataPromise
    }

    getInfo()

    return {
      postInfo,
      goBack,
      getInfo,
      arr,
      drawer,
      rail,
    }
  },
}
</script>

<style>
table {
  width: 100%;
  border-collapse: collapse;
  font-size: 14px;
  font-weight: 400;
  line-height: 20px;
}

th {
  text-align: left;
  padding: 16px;
  font-weight: 500;
  color: #212121;
  background-color: #f5f5f5;
  border-bottom: 1px solid #e0e0e0;
}

td {
  text-align: left;
  padding: 16px;
  color: #424242;
  border-bottom: 1px solid #e0e0e0;
}
</style>
