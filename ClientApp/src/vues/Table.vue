@@ -0,0 +1,125 @@
<script>
import axios from 'axios'
import Counter from './Counter.vue'
import CreationData from './CreationData.vue'
import { Forecast } from './Forecast'
import App from '@/App.vue'
import { ref, onBeforeMount, toRefs, watch, reactive } from 'vue'
const arr = ref([])
const counter = ref(0)
const mainArray = []
let votes = axiosTest()

let currentrows = 5

async function axiosTest() {
  const promise = axios.get('api/WeatherForecast/get')
  console.log(promise)

  const dataPromise = await promise.then((response) => response.data)
  arr.value.length = 0

  for (let i = 0; i < 5; i++) {
    arr.value.push(dataPromise[i])
    mainArray[i] = dataPromise[i]
  }

  console.log(arr)

  return dataPromise
}

export default {
  components: { Counter, CreationData },
  props: {
    localprop: String,
  },

  setup(props) {
    // let  myPropValue = ref(props.localprop)
     const myPropValue = ref(props.localprop)
    // const { localprop: myPropValue } = toRefs(props)

      watch(
      () => props.localprop,
      (newValue) => {
        myPropValue.value = newValue
      }
    ) 
    
   // let myPropValue = ref(10)
    /*
    const state = reactive({
      myPropValue: props.localprop,
      // другие переменные состояния
    })
*/
    watch(
      () => props.localprop,
      (newValue) => {
        state.myPropValue = newValue
      }
    )
    const change = () => {}
    const vote = () => {
      axiosTest()
      arr.value.length = currentrows
      console.log(myPropValue)
    }
    const createrows = (rowsToCreate) => {
      for (let i = 0; i < rowsToCreate; i++) {
        arr.value.push(mainArray[5 - rowsToCreate + i])
      }
    }
    const onUpdateColor = (data) => {
      if (data.selectedColor > currentrows && data.length < createrows) {
        createrows(data.selectedColor - currentrows)
      } else {
        console.log('child component said login', data)
        arr.value.length = data.selectedColor
      }
      currentrows = data.selectedColor
    }

    const votes = ref([])
    arr.value.push(axiosTest())
    return { arr, vote, onUpdateColor, myPropValue } //labels
  },
}
</script>
<template>
  <p>{{ myPropValue }}</p>
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
