<script>
import { ref } from 'vue'
import axios from 'axios'
import { useRouter } from 'vue-router'
export default {
  setup() {
    const router = useRouter()
    const createWafelMissions = ref([])
    const createMonitorMissions = ref([])
    const missions = ref([])
    const parseData = (data) => {
      let dateString = data
      let dateObj = new Date(dateString)
      let day = dateObj.getUTCDate().toString().padStart(2, '0')
      let month = (dateObj.getUTCMonth() + 1).toString().padStart(2, '0')
      let year = dateObj.getUTCFullYear().toString()
      let hours = dateObj.getUTCHours().toString().padStart(2, '0')
      let minutes = dateObj.getUTCMinutes().toString().padStart(2, '0')
      return `${day}-${month}-${year} ${hours}:${minutes}`
    }
    const createWafel = () => {
      router.push('/Measurer/WelcomeBack')
    }
    const createMonitor = () => {
      router.push('/Measurer/CreateMonitor')
    }
    const getMissions = async () => {
      try {
        const promise = await axios.get(
          'https://localhost:3000/api/WaferCreateMission/Get'
        )

        console.log(promise)
        const dataPromise = await promise
        missions.value = [...dataPromise.data]
        createArreys()
      } catch (error) {
        console.error(error)
      }
    }

    getMissions()
    const createArreys = () => {
      createWafelMissions.value = missions.value.filter(
        (obj) => obj.type === 'generateNewWafel'
      )
      createMonitorMissions.value = missions.value.filter(
        (obj) => obj.type === 'generateNewMonitor'
      )
    }
    return {
      createWafelMissions,
      createMonitorMissions,
      createWafel,
      parseData,
      createMonitor,
    }
  },
}
</script>

<template>
  <div style="height: 5em"></div>
  <v-data-table>
    <h2>Создать новую пластину</h2>
    <thead>
      <tr>
        <th class="text-left"></th>
        <th class="text-left">Номер пластины</th>
        <th class="text-left">Дата выставления задачи</th>
        <th class="text-left">Действие</th>
      </tr>
    </thead>
    <tbody>
      <tr v-for="(item, index) in createWafelMissions" :key="item.creationTime">
        <td>{{ index + 1 }}</td>
        <td>{{ item.number }}</td>
        <td>{{ parseData(item.creationTime) }}</td>
        <td class="text-center">
          <v-btn
            size="small"
            density="compact"
            variant="plain"
            icon="mdi-plus"
            @click="createWafel"
            >Создать</v-btn
          >
        </td>
      </tr>
    </tbody>
  </v-data-table>
  <v-data-table>
    <div style="height: 5em"></div>
    <h2>Создать новый монитор</h2>
    <thead>
      <tr>
        <th class="text-left"></th>
        <th class="text-left">Номер монитора</th>
        <th class="text-left">Дата выставления задачи</th>
        <th class="text-left">Действие</th>
      </tr>
    </thead>
    <tbody>
      <tr
        v-for="(item, index) in createMonitorMissions"
        :key="item.creationTime"
      >
        <td>{{ index + 1 }}</td>
        <td>{{ item.number }}</td>
        <td>{{ parseData(item.creationTime) }}</td>
        <td class="text-center">
          <v-btn
            size="small"
            density="compact"
            variant="plain"
            icon="mdi-plus"
            @click="createMonitor"
            >Создать</v-btn
          >
        </td>
      </tr>
    </tbody>
  </v-data-table>
</template>
