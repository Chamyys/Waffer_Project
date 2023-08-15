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
    const addChipsBtnText = ref([])
    const missions = ref([])
    const offsetTop = ref(0)
    const chunks = ref([])
    const showableIndexes = ref([])
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
    const fillChipBtnArray = () => {
      for(let i = 0; i < creeateMeasursMissions.value.length;i++) {
        addChipsBtnText.value[i] = "Показать больше..."
      }
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
        fillChipBtnArray()
      } catch (error) {
        console.error(error)
      }
    }
    const onScroll = (e) => {
      offsetTop.value = e.target.scrollTop
    }
    const getMore = (index) => {
      if (!showableIndexes.value.includes(index)) {
        showableIndexes.value.push(index)
        addChipsBtnText.value[index]="Скрыть..."
      } else {
        showableIndexes.value = showableIndexes.value.filter((number) => number !== index);
        addChipsBtnText.value[index]="Показать больше..."
      }
    }
    const isCurrentIndexShowable = (index) => {
      return showableIndexes.value.includes(index)
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

    const afterItemRendered = (item) => {
      chunks.value = chunksMaker(item)
    }
    const chunksMaker = (item) => {
      const chunkSize = 3
      const chunks = []

      for (let i = 0; i < item.monitorConfig.elements.length; i += chunkSize) {
        chunks.push(item.monitorConfig.elements.slice(i, i + chunkSize))
      }

      return chunks
    }
  
    return {
      fillChipBtnArray,
      createWafelMissions,
      createMonitorMissions,
      creeateMeasursMissions,
      createWafel,
      parseData,
      createMonitor,
      createMeasur,
      onScroll,
      offsetTop,
      afterItemRendered,
      chunks,
      getMore,
      chunksMaker,
      showableIndexes,
      isCurrentIndexShowable,
      addChipsBtnText
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
    <div v-for="(item, index) in creeateMeasursMissions" :key="item.index">
      <v-card class="mx-auto" style="width: 100%">
        <v-row style="width: auto">
          <v-col class="col">
            <v-card-title
              class="text-h3 text--primary"
              style="padding-top: 1em"
              >{{ item.id }}</v-card-title
            >
            <v-card-subtitle class="text-h4 text--primary" style="padding: 1em"
              >Тип измерения</v-card-subtitle
            >
          </v-col>
          <v-divider
            :thickness="8"
            class="border-opacity-25"
            color="indigo-darken-1"
            vertical
          ></v-divider>
          <v-col class="col">
            <v-card-title
              class="text-h4 text--primary"
              style="padding-top: 1em"
              >{{ item.technologist }}</v-card-title
            >
            <v-card-subtitle class="text-h5 text--primary" style="padding: 1em"
              >Данные технолога</v-card-subtitle
            >
          </v-col>
          <v-divider
            :thickness="8"
            class="border-opacity-25"
            color="indigo-darken-1"
            vertical
          ></v-divider>
          <v-col class="col">
            <v-card-title class="text-h4 text--primary" style="padding-top: 1em"
              >{{item.stage.stageName }}</v-card-title
            >
            <v-card-subtitle class="text-h5 text--primary" style="padding: 1em"
              >Данные об этапе</v-card-subtitle
            >
          </v-col>
          <v-divider
            :thickness="8"
            class="border-opacity-25"
            color="indigo-darken-1"
            vertical
          ></v-divider>
          <v-col class="col">
            <v-card-title class="text-h5 text--primary" style="padding-top: 1em"
              >Технологический этап - {{item.measurementRecording.name}}
            </v-card-title>
            <v-card-subtitle class="text-h5 text--primary" style="padding: 1em"
              >Номер запуска - {{ item.parcel.name }}</v-card-subtitle
            >
          </v-col>
          <v-divider
            :thickness="8"
            class="border-opacity-25"
            color="indigo-darken-1"
            vertical
          ></v-divider>
          <v-col class="col">
            <v-card-text>
              <p class="text-h6 text--primary" style="text-align: center">
                Элементы монитора:
              </p>
            </v-card-text>
            <v-row style="padding-left: 1em">
              <v-chip-group>
                <v-chip
                  v-for="(obj, index) in item.monitorConfig.elements.slice(
                    0,
                    10
                  )"
                  :key="index"
                  style="
                    color: white;
                    background-color: rgb(30, 7, 135);
                    padding-top: 0px;
                  "
                >
                  {{ obj }}
                </v-chip>
              </v-chip-group>
              <v-chip-group v-if="isCurrentIndexShowable(index)">
                <v-chip
                  v-for="(obj, key) in item.monitorConfig.elements.slice(
                    10,
                    999
                  )"
                  :key="key"
                  style="color: white; background-color: rgb(30, 7, 210)"
                  >{{ obj }}</v-chip
                >
              </v-chip-group>
              <v-chip-group style="padding-bottom: 1em;">
                <v-chip
                  v-if="item.monitorConfig.elements.length >= 9"
                  style="color: white; background-color: rgb(48, 79, 254); "
                  @click="getMore(index)"
                >
                 {{ addChipsBtnText[index] }}
                </v-chip>
              </v-chip-group>
            </v-row>
          </v-col>
        </v-row>
        <div style="height: 3em"></div>
      </v-card>
      </div>
            <!--
        <v-row style="max-height: min-content; padding-left: 1em;">
               <div
                v-for="(obj, index) in (item.monitorConfig.elements.slice(3,6))"
                :key="index"
                class="column"
              >
              <v-chip  class="ma-2"   style="color: white; background-color: rgb(30, 7, 135); padding-top: 0px; padding-bottom: 0px;"> 
                {{ obj }}
              </v-chip>
            </div>
        </v-row>
        <v-row style="max-height: max-content; padding-left: 1em;">
               <div
                v-for="(obj, index) in (item.monitorConfig.elements.slice(6,9))"
                :key="index"
                class="column"
              >
              <v-chip  class="ma-2"   style="color: white; background-color: rgb(30, 7, 135); padding-top: 0px; padding-bottom: 0px;"> 
                {{ obj }}
              </v-chip>
            </div>
            <div style="padding-bottom: 1em;">
            <v-chip v-if="item.monitorConfig.elements.length>=9" @click="getMore()"  class="ma-2"   style="color: white; background-color: rgb(48, 79, 254); padding-top: 0px;"> 
                 Увидеть больше... 
              </v-chip>
              <v-chip-group  v-if="showAllChipsFlag" style="padding-bottom: 1em; padding-left: 0.5em;">
            <v-chip>Chip 1</v-chip>
            <v-chip>Chip 2</v-chip> 
            <v-chip>Chip 3</v-chip>
            <v-chip>Chip 3</v-chip>
            <v-chip>Chip 3</v-chip>
            <v-chip>Chip 3</v-chip>
            <v-chip>Chip 3</v-chip>
            <v-chip>Chip 3</v-chip>
          </v-chip-group>
            </div>
        </v-row>
  

       
      <div style="height: 2em"></div>
    </div>

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
   
      -->
   
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
.col {
  background-color: #7986cb;
}
.column {
  float: left;
  max-width: min-content;
}
</style>
