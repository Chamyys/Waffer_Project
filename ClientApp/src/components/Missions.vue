<script>
import { ref } from 'vue'
import axios from 'axios'
import { useRouter } from 'vue-router'
export default {
  setup() {
    const router = useRouter()
    const createWafelMissions = ref([])
    const creeateMeasursMissions = ref([])
    const createMonitorMissions = ref([])
    const missions = ref([])
    const offsetTop = ref(0)
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
    const createMeasur = () => {
      router.push('/Measurer/CreateMeasur')
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
    const getMeasurMissions = async () => {
      try {
        const promise = await axios.get('https://localhost:3000/api/MT/Get')

        console.log(promise)
        const dataPromise = await promise
        creeateMeasursMissions.value = [...dataPromise.data]
      } catch (error) {
        console.error(error)
      }
    }
    const onScroll = (e) => {
      offsetTop.value = e.target.scrollTop
    }

    getMissions()
    getMeasurMissions()
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
      creeateMeasursMissions,
      createWafel,
      parseData,
      createMonitor,
      createMeasur,
      onScroll,
      offsetTop,
    }
  },
}
</script>

<template>
  <v-container
    id="scroll-target"
    style="max-height: 100%"
    class="overflow-y-auto"
  >
    <div style="width: 100%">
      <h2 style="text-align: center">Создать новое измерение</h2>
    </div>

    <div class="d-table">
      <div class="d-tr">
        <div class="d-td"></div>
        <div class="d-td">Данные технолога</div>
        <div class="d-td">Номер пластины</div>
        <div class="d-td">Технологический этап</div>
        <div class="d-td">Этап</div>
        <div class="d-td">Номер запуска</div>
        <div class="d-td">Схема/монитор</div>
        <div class="d-td">Действие</div>
      </div>

      <div
        v-for="(item, index) in creeateMeasursMissions"
        :key="item.creationTime"
        style="height: 4em"
        class="d-tr"
      >
        <div class="d-td">{{ index + 1 }}</div>

        <div class="d-td">{{ item.technologist }}</div>
        <div class="d-td">{{ item.id }}</div>
        <div class="d-td">{{ item.measurementRecording.name }}</div>
        <div class="d-td">{{ item.stage.stageName }}</div>
        <div class="d-td">{{ item.parcel.name }}</div>
        <div class="d-td">{{ item.dieType.name }}</div>
        <div class="d-td">
          <v-btn
            size="small"
            density="compact"
            variant="plain"
            icon="mdi-plus"
            @click="createMeasur"
            >Измерить</v-btn
          >
        </div>
      </div>
    </div>

    <div style="height: 5em"></div>
    <div style="width: 100%">
      <h2 style="text-align: center">Создать новую пластину</h2>
    </div>

    <div class="d-table">
      <div class="d-tr">
        <div class="d-td"></div>
        <div class="d-td">Номер пластины</div>
        <div class="d-td">Дата выставления задачи</div>
        <div class="d-td">Действие</div>
      </div>

      <div
        v-for="(item, index) in createWafelMissions"
        :key="item.creationTime"
        style="height: 4em"
        class="d-tr"
      >
        <div class="d-td">{{ index + 1 }}</div>
        <div class="d-td">{{ item.number }}</div>
        <div class="d-td">{{ parseData(item.creationTime) }}</div>
        <div class="d-td">
          <v-btn
            size="small"
            density="compact"
            variant="plain"
            icon="mdi-plus"
            @click="createWafel"
            >Создать</v-btn
          >
        </div>
      </div>
    </div>

    <div style="height: 5em"></div>
    <div style="width: 100%">
      <h2 style="text-align: center">Создать новый монитор</h2>
    </div>

    <div class="d-table">
      <div class="d-tr">
        <div class="d-td"></div>
        <div class="d-td">Номер монитора</div>
        <div class="d-td">Дата выставления задачи</div>
        <div class="d-td">Действие</div>
      </div>

      <div
        v-for="(item, index) in createMonitorMissions"
        :key="item.creationTime"
        style="height: 4em"
        class="d-tr"
      >
        <div class="d-td">{{ index + 1 }}</div>
        <div class="d-td">{{ item.number }}</div>
        <div class="d-td">{{ parseData(item.creationTime) }}</div>
        <div class="d-td">
          <v-btn
            size="small"
            density="compact"
            variant="plain"
            icon="mdi-plus"
            @click="createMonitor"
            >Создать</v-btn
          >
        </div>
      </div>
    </div>
    <div style="height: 5em"></div>
  </v-container>
</template>
<style>
.d-td {
  display: table-cell;
  text-align: center;
  border: none;
  border: 1px solid #ccc;
  vertical-align: middle;
  padding: 4px;
}
.d-tr {
  display: table-row;
  padding: 4px;
}
.d-td:not(.no-p) {
  padding: 4px;
}
.d-table {
  display: table;
  width: 100%;
  border-collapse: collapse;
}
</style>
