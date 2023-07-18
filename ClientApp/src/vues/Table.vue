@@ -0,0 +1,125 @@
<script>
import axios from 'axios'
import Counter from './Counter.vue'
import CreationData from './CreationData.vue'
import { Forecast } from './Forecast'
import App from '@/App.vue'
import { ref, onBeforeMount, toRefs, watch, reactive, computed } from 'vue'
import { useStore } from 'vuex'
import store from '@/store/index'

const arr = ref([])
const counter = ref(0)
const mainArray = []

const exportnumber = ref(0)

export default {
  components: { Counter, CreationData },
  props: {
    localprop: Number,
  },

  setup(props) {
    // Now you can access params like:

    // let  myPropValue = ref(props.localprop)
    const store = useStore()

    let currentrows = 5
    let myPropValue = ref(props.localprop)
    currentrows = myPropValue.value.btnnumber
    exportnumber.value = currentrows
    // const { localprop: myPropValue } = toRefs(props)

    // let myPropValue = ref(10)
    /*
    const state = reactive({
      myPropValue: props.localprop,
      // другие переменные состояния
    })
*/
    /*
    watch(
      () => props.localprop,
      (newValue) => {
        state.myPropValue = newValue
      }
    )
    */

    const axiosTest = async () => {
      const promise = axios.get('api/WeatherForecast/get')
      console.log(promise)

      const dataPromise = await promise.then((response) => response.data)
      arr.value.length = 0
      store.dispatch('createWeatherArray', dataPromise) //заполнение vuex

      for (let i = 0; i < 5; i++) {
        arr.value.push(dataPromise[i])
      }

      arr.value.length = currentrows
      console.log(arr)

      //const totalNotes = store.state.arrayofWeather //получение из vuex

      // console.log(totalNotes[0][0].date) //вызов поля объекта

      //store.dispatch('createWeatherArray', mainArray) //заполнение vuex

      return dataPromise
    }

    const vote = () => {
      axiosTest()
      arr.value.length = currentrows
      console.log(myPropValue)
    }
    const createrows = (rowsToCreate) => {
      //for (let i = 0; i < rowsToCreate; i++) {
      // arr.value.push(mainArray[5 - rowsToCreate + i])
      //}
      arr.value.length = 0
      for (let i = 0; i < currentrows; i++) {
        //  arr.value.push(store.state.arrayofWeather[0][i])
        arr.value.push(store.getters.getweatherarray[0][i])
      }
    }

    const onUpdateColor = (data) => {
      currentrows = data.selectedColor
      if (data.selectedColor >= currentrows) {
        createrows(data.selectedColor - currentrows)
      } else {
        // console.log('child component said login', data)
        arr.value.length = data.selectedColor
      }
    }

    const votes = ref([])
    arr.value.push(axiosTest())
    return { arr, vote, onUpdateColor, myPropValue, exportnumber } //labels
  },
}
</script>
<template>

  <!-- <router-link to="/">Home</router-link> |-->
  <!--  <router-link to="/bugaga">Table</router-link>
-->
  <p>{{ myPropValue.btnnumber }}</p>
  <CreationData></CreationData>

  <v-data-table v-model:items-per-page="itemsPerPage">
    <thead>
      <tr>
        <th class="text-left">Номер</th>
        <th class="text-left">Градусы Цельсия</th>
        <th class="text-left">Градусы Фаренгейта</th>
        <th class="text-left">Ощущение</th>
        <th class="text-left">Дата</th>
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
  <div style="height: 50px; width: 100%"></div>
  <div>
    <div>
      <Counter
        style="
          width: 50%;
          height: 140px;
          float: left;
          padding-left: 17em;
          padding-right: 17em;
        "
        :defoultnumber="exportnumber"
        @updateColor="onUpdateColor"
      ></Counter>
    </div>
    <div>
      <v-btn
        variant="outlined"
        style="
          width: 15%;
          height: 90px;
          padding-left: 30em;
          padding-right: 30em;
        "
        @click="vote"
      >
        Update Table
      </v-btn>
    </div>
  </div>


</template>
<style>
th {
  text-align: left; /* Выравнивание по левому краю */
  background: #ccc; /* Цвет фона ячеек */
  padding: 5px; /* Поля вокруг содержимого ячеек */
  border: 1px solid black; /* Граница вокруг ячеек */
}
td {
  padding: 5px; /* Поля вокруг содержимого ячеек */
  border: 1px solid black; /* Граница вокруг ячеек */
}
</style>
