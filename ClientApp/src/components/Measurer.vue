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
          title=""
          nav
        >
          <template #append>
            {{ username }}
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
        <div>
          <div>
            <h1 style="float: none">Прием пластины</h1>
           
            <div style="float: left">
              
              <v-alert
                v-if="FormNotComplitedAlert"
                text="Данные внесены некорректно"
                type="error"
                style="margin-left: 5em ; margin-bottom: 2em;"
              ></v-alert>
              <v-alert
                v-if="DeliverySuccsess"
                closable
                text="Данные успешно загружены на сервер"
                type="success"
                style="margin-left: 5em"
              ></v-alert>
            
              <v-alert
                v-if="renderHashAlert"
                
                style="margin-left: 5em"
                title="Ваш код для подтверждения операции:"
              >
                {{ hashCode }}
              </v-alert>
            
              <v-text-field
                v-if="renderHashAlert"
                v-model="textfieldhashconfirmcode"
                style="text-align: center; margin-top: 2em; margin-left: 5em"
                label="Введите код поддтверждения"
                :rules="rules"
                hide-details="auto"
              ></v-text-field>
          
            </div>
          
          </div>
          <div style="height: 5em"></div>
          <!-- <div class="right">-->

          <v-sheet width="400" class="mx-auto">
            <div>
              <Datepicker
                v-model="date"
                style="
                  text-align: center;
                  border-style: solid;
                  border-color: black;
                  border-radius: 0.3em;
                "
                input-format="dd.MM.yyyy"
              />
            </div>

            <!-- </div> -->

            <!--  <div class="left">-->

            <!-- </div>-->
            <!-- 

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
            -->
            <div style="height: 5em"></div>

            <v-select
              id="select-id"
              v-model="currentTechnologistName"
              label="Ответственный технолог"
              :items="['А.С. Иванов', 'Б.А. Пушкин', 'В.И.Ленин', 'К.С.Имя']"
              @change="getCurrentTechnologistName($event)"
            >
              {{ item.Text }}
            </v-select>
          </v-sheet>

          <div>
            <div>
              <div></div>

              <div></div>
            </div>
          </div>
          <div style="height: 5em"></div>

          <v-sheet width="400" class="mx-auto">
            <v-btn type="submit" block class="mt-2" @click="createHashCode">{{
              dynamicServerPushButtonText
            }}</v-btn>
            <v-btn type="submit" block class="mt-2" @click="createWELCOMEBACK"
              >Получить что-то с сервера</v-btn
            >
            <v-btn type="submit" block class="mt-10" @click="goBack"
              >Выход</v-btn
            >
          </v-sheet>
        </div>
      </v-main>
    </v-layout>
  </v-card>
</template>

<script>
import Datepicker from 'vue3-datepicker'
import { ref } from 'vue'
import axios from 'axios'
import { useStore } from 'vuex'
import { useRouter } from 'vue-router'
import crc32 from 'crc/crc32'
export default {
  components: { Datepicker },
  setup() {
    let currentTechnologistName = ref('')

    const getCurrentTechnologistName = (name) => {
      currentTechnologistName.value = name
    }
    const dynamicServerPushButtonText = ref('Получить код')
    const renderHashAlert = ref(false)
    const DeliverySuccsess = ref(false)
    const FormNotComplitedAlert = ref(false)
    const date = ref(new Date())
    const rail = ref(true)
    const textfieldhashconfirmcode = ref('')
    let isHashGot = false
    const drawer = ref(true)
    const store = useStore()
    const arr = ref([])
    const hashCode = ref('')
    const router = useRouter()
    let entity = {
      id: '',
      Technologist: '',
      returnedTime: '',
      Measurer: '',
    }
    const username = (
      window.localStorage.getItem('firstName') +
      ' ' +
      window.localStorage.getItem('lastName')
    ).toString()

    let currentrows = 5
    const isAllFieldsComplitedCheck = (entity) => {
      if (
        !entity.id ||
        !entity.Technologist ||
         entity.Measurer.length < 2 ||
        hashCode.value != textfieldhashconfirmcode.value
      )
        return false
      else return true
    }
    const createHashCode = () => {
      if (!isHashGot) {
        DeliverySuccsess.value = false
        isHashGot = true
        hashCode.value = crc32(new Date().toString()).toString(16)
        renderHashAlert.value = true
        dynamicServerPushButtonText.value = 'Отправить данные на сервер'
      } else {
        if (isAllFieldsComplitedCheck(createWELCOMEBACK())) {
          postInfo(entity)
      } else {
          FormNotComplitedAlert.value = true
      }
    }
  }
    const createWELCOMEBACK = () => {
      const Time = new Date()
      date.value.setHours(Time.getHours() + 3)
      date.value.setMinutes(Time.getMinutes())
      date.value.setSeconds(Time.getSeconds())
      entity = {
        id: hashCode.value,
        Technologist: currentTechnologistName.value,
        returnedTime: date.value,
        Measurer: username,
      }
      return entity
    }

    const postInfo = async () => {
      try {
        const response = await axios.post(
          'https://localhost:3000/api/WeatherForecast/Post',
          createWELCOMEBACK()
        )
        FormNotComplitedAlert.value = false
        renderHashAlert.value = false
        console.log(response)
      } catch (error) {
        console.error(error)
      }
      restartPageLife()
    }

    const restartPageLife = () => {
      DeliverySuccsess.value = true
      dynamicServerPushButtonText.value = 'Получить код'
      isHashGot = false
      textfieldhashconfirmcode.value = ""
       FormNotComplitedAlert.value = false
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
      renderHashAlert,
      postInfo,
      goBack,
      getInfo,
      arr,
      drawer,
      rail,
      date,
      Datepicker,
      username,
      FormNotComplitedAlert,
      currentTechnologistName,
      getCurrentTechnologistName,
      createHashCode,
      hashCode,
      dynamicServerPushButtonText,
      textfieldhashconfirmcode,
      DeliverySuccsess,
      rules: [
        (value) => !!value || 'Поле не может быть пустым.',
        (value) =>
          (value && value.length == hashCode.value.length) ||
          'Длины кодов подтверждения не совпадают',
        (value) =>
          (value && textfieldhashconfirmcode.value == hashCode.value) ||
          'Код подтверждения не совпадает',
      ],
    }
  },
  
}
</script>

<style>
div.left {
  float: right;
}
div.right {
  float: right;
  margin-right: 25%;
  padding: 2em;
}

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
