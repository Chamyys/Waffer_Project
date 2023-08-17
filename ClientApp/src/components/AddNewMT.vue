<script>
import { ref, watch } from 'vue'
import { useRouter } from 'vue-router'
import axios from 'axios'
import crc32 from 'crc/crc32'
import { useStore } from 'vuex'
import ProgressCircular from './ProgressCircular.vue'
export default {
  components: {
    ProgressCircular
  },
  setup () {
    const configName = ref()
    const createNewElementsConfigFlag = ref(false)
    const waferNumbers = ref([]) //GenerateNewWafelString
    const myCounter = ref(0)
    const monitors = ref(['Первый монитор', 'Второй монитор'])
    const monitorElementsConfigCollection = ref([])
    const menu = ref(false)
    const monitorElementsselected = ref([])
    const monitorElements = ref([
      'TS1',
      'TS2',
      'TS3',
      'TS4',
      'TS5',
      'TS6',
      'TS7',
      'TS8',
      'TS9',
      'TS10',
      'TS11',
      'TS12',
      'TS13',
      'TS14',
      'TS15',
      'TS16',
      'TS17',
      'TS18',
      'TS19',
      'TS20',
      'TS21',
      'TS22',
      'TS23',
      'TS24',
      'TS25',
      'TS26',
      'TS27',
      'TS28',
      'TS29',
      'TS30',
      'TS31',
      'TS32',
      'TS33',
      'TS34',
      'TS35',
      'TS36',
      'TS37',
      'TS38',
      'TS39',
      'TS40',
      'TS41',
      'TS42',
      'TS43',
      'TS44',
      'TS45',
      'TS46',
      'TS47',
      'TS48',
      'TS49',
      'TS50',
      'TS51',
      'TS52',
      'TS53',
      'TS54',
      'TS55',
      'TS56',
      'TS57',
      'TS58',
      'TS59',
      'TS60',
      'TS61',
      'TS62',
      'TS63',
      'TS64'
    ])
    const comment = ref()
    const timeoutId = ref(null)
    const showCancelBtn = ref(false)
    const snackbar = ref(false)
    const store = useStore()
    const startupNumbers = ref(['Первый запуск', 'Второй запуск'])
    const textArea = ref()
    const dynamicServerPushButtonText = ref('Отправить данные')
    const statusDialog = ref(false)
    const textfieldhashconfirmcode = ref('')
    const generatedHashCode = ref()
    const createNewMonitor = ref('')
    const createNewElementsConfig = ref()
    const createNewWafel = ref()
    const addNewElementsConfiguration = ref(false)
    const selectedMonitor = ref()
    const measurementRecording = ref()
    const startupNumber = ref()
    const router = useRouter()
    const AllWaffelsInLabArray = ref([])
    const AllConfigObjects = ref([])
    const waferChoosedFlag = ref(false)
    const currentMissionType = ref()
    const textareatransition = ref(false)
    const isNewConfigInProgress = ref(false)

    const goBack = () => {
      router.push('/Technologist/WelcomeBack')
    }
    const isAllFieldsComplitedCheck = () => {
      if (
        !createNewMonitor.value ||
        !createNewWafel.value ||
        !measurementRecording.value ||
        !createNewElementsConfig.value ||
        !monitorElementsselected.value
      ) {
        return false
      } else return true
    }
    const fillWafelPole = () => {
      const wafer = {
        id: 'id', ////  APISVR2.0
        waferId: createNewWafel.value,
        codeProductId: 12, //(ID шаблона) // APISVR2.0
        parcelId: 12 //(ID запуска) //  APISVR2.0
      }
      return wafer
    }
    const getPreviousMeasurementRecording = () => {
      // APISVR2.0
      const returnedResult = 1000
      if (measurementRecording.value >= returnedResult) {
        return measurementRecording.value
      }
      store.dispatch('throwMessage', {
        type: 'error',
        name: 'FormNotComplited'
      })
    }

    const fillMeasurementRecording = () => {
      const measurementRecordingObj = {
        id: 12, // // APISVR2.0
        name: 'name', // APISVR2.0
        stageId: 12, //// APISVR2.0
        measurementDate: new Date()
      }
      return measurementRecordingObj
    }
    const fillMonitorConfig = () => {
      const config = {
        id: 'someId',
        name: createNewElementsConfig.value,
        elements: monitorElementsselected.value
      }
      return config
    }
    const fillStage = () => {
      const stage = {
        stageId: getPreviousMeasurementRecording(), // APISVR2.0
        stageName: 'name', // APISVR2.0
        codeProductId: 12 // APISVR2.0
      }
      return stage
    }
    const fillParcel = () => {
      const parcel = {
        id: 12, // APISVR2.0
        name: startupNumber.value // APISVR2.0
      }
      return parcel
    }
    const fillDieType = () => {
      const dieType = {
        dieTypeId: 12, // APISVR2.0
        name: createNewMonitor.value
      }
      return dieType
    }

    const createNewMtObject = () => {
      if (!comment.value) {
        comment.value = ''
      }
      const newMtObject = {
        id: generatedHashCode.value,
        status: 'NEW',
        wafer: fillWafelPole(),
        technologist: technologistName,
        measurementRecording: fillMeasurementRecording(),
        stage: fillStage(),
        parcel: fillParcel(),
        dieType: fillDieType(),
        monitorConfig: fillMonitorConfig(),
        comment: comment.value
      }
      return newMtObject
    }
    const sendToServer = () => {
      generatedHashCode.value = crc32(new Date().toString()).toString(16)

      if (isAllFieldsComplitedCheck()) {
        postMT()
      } else {
        store.dispatch('throwMessage', {
          type: 'error',
          name: 'FormNotComplited'
        })
      }
    }
    const chunksMaker = () => {
      const chunkSize = 8
      const chunks = []

      for (let i = 0; i < monitorElements.value.length; i += chunkSize) {
        chunks.push(monitorElements.value.slice(i, i + chunkSize))
      }

      return chunks
    }

    const chunks = chunksMaker()
    const lastChunkSize = chunks.at(-1).length
    const columnWidth = (100 / chunks[0].length).toString().concat('%')

    const technologistName = `${window.localStorage.getItem(
      'firstName'
    )} ${window.localStorage.getItem('lastName')}`

    const getWaferNumbers = async () => {
      try {
        const promise = await axios.get(
          'https://localhost:3000/api/WaferInLab/Get'
        )
        console.log(promise)
        const dataPromise = await promise
        const wafels = dataPromise.data
        AllWaffelsInLabArray.value = dataPromise.data
        waferNumbers.value = wafels.map(function (item) {
          return item.id
        })
      } catch (error) {
        console.error(error)
      }
    }
    const makeNewConfig = () => {
      isNewConfigInProgress.value = true
      textareatransition.value = true
      addNewElementsConfiguration.value = true
      cleanCheckbox()
    }

    const showCurrentConfig = () => {
      if (createNewElementsConfig.value !== null) {
        addNewElementsConfiguration.value = true
        monitorElementsselected.value = AllConfigObjects.value.find(
          (el) => createNewElementsConfig.value === el.name
        ).elements
      } else {
        monitorElementsselected.value = ['']
      }
    }

    const cleanCheckbox = () => {
      monitorElementsselected.value = []
    }

    const postMT = async () => {
      try {
        const response = await axios.post(
          'https://localhost:3000/api/MT/Post',
          createNewMtObject()
        )

        console.log(response)
        store.dispatch('throwMessage', {
          type: 'success',
          name: 'deliverySuccsess'
        })
      } catch (error) {
        console.error(error)
      }
    }
    const getConfigObjects = async () => {
      try {
        const response = await axios.get(
          'https://localhost:3000/api/MonitorConfig/Get'
        )

        console.log(response)
        AllConfigObjects.value = response.data
        monitorElementsConfigCollection.value = AllConfigObjects.value.map(
          function (item) {
            return item.name //Проверка на законченность измерений
          }
        )
      } catch (error) {
        console.error(error)
      }
    }
    const createNewConfigObject = () => {
      textareatransition.value = true
      isNewConfigInProgress.value = false
      return {
        id: crc32(new Date().toString()).toString(16),
        name: configName.value,
        elements: monitorElementsselected.value
      }
    }

    const continueWithoutSavingConfig = async () => {
      textareatransition.value = !textareatransition.value
    }
    const postNewConfig = async () => {
      try {
        const response = await axios.post(
          'https://localhost:3000/api/MonitorConfig/Post',
          createNewConfigObject()
        )

        console.log(response)
        AllConfigObjects.value.push(createNewConfigObject())
        monitorElementsConfigCollection.value.push(createNewConfigObject().name)
        monitorElementsselected.value = []
        createNewElementsConfig.value = configName.value
        addNewElementsConfiguration.value = false
        configName.value = ''
        store.dispatch('throwMessage', {
          type: 'success',
          name: 'configSuccsess'
        })
      } catch (error) {
        console.error(error)
      }
    }

    const isCheckboxesNotEmpty = () => {
      if (monitorElementsselected.value.length > 0) return true
      else {
        store.dispatch('throwMessage', {
          type: 'error',
          name: 'monitorElementsEmpty'
        })
        return false
      }
    }

    const nameIsBuissy = () => {
      if (!monitorElementsConfigCollection.value.includes(configName.value)) {
        return true
      } else {
        store.dispatch('throwMessage', {
          type: 'error',
          name: 'nameIsBuissy'
        })
        return false
      }
    }

    const isConfigElementsAlreadyExists = () => {
      if (
        !AllConfigObjects.value.find(
          (element) =>
            element.elements.sort().toString() ===
            monitorElementsselected.value.sort().toString()
        )
      ) {
        return true
      } else {
        store.dispatch('throwMessage', {
          type: 'error',
          name: 'configAlreadyExists'
        })
        return false
      }
    }

    const isConfigNotEmpty = () => {
      if (configName.value) return true
      else {
        store.dispatch('throwMessage', {
          type: 'error',
          name: 'configNameEmpty'
        })
        return false
      }
    }
    const checkIsConfigOk = () => {
      if (
        isCheckboxesNotEmpty() &&
        isConfigNotEmpty() &&
        nameIsBuissy() &&
        isConfigElementsAlreadyExists()
      ) {
        postNewConfig()
      }
    }
    const deleteConfig = () => {
      if (
        monitorElementsConfigCollection.value.length > 0 
      ) {
        snackbar.value = true

        timeoutId.value = setTimeout(async () => {
          try {
            const response = await axios.post(
              'https://localhost:3000/api/MonitorConfig/Delete',
              findConfigForRemoving()
            )

            console.log(response)
            store.dispatch('throwMessage', {
              type: 'success',
              name: 'configSuccsessRemoved'
            })
            removeConfigFromArray(findConfigForRemoving())
          } catch (error) {
            console.error(error)
          }

          cancelSend()
        }, 5000)
      } else {
        store.dispatch('throwMessage', {
          type: 'error',
          name: 'NoConfigsToDelete'
        })
      }
    }

    const cancelSend = () => {
      console.log('Отправка отменена')
      clearTimeout(timeoutId.value)
      timeoutId.value = null
    }

    const removeConfigFromArray = (objectToRemove) => {
      AllConfigObjects.value = AllConfigObjects.value.filter(
        (e) => e !== objectToRemove
      )
      monitorElementsConfigCollection.value =
        monitorElementsConfigCollection.value.filter(
          (e) => e !== objectToRemove.name
        )
      createNewElementsConfig.value = monitorElementsConfigCollection.value[0]
    }
    const findConfigForRemoving = () => {
      //monitorElementsConfigCollection // имена
      //AllConfigObjects//объекты
      //createNewElementsConfig//текущее имя
      return AllConfigObjects.value.find(
        (element) => element.name === createNewElementsConfig.value
      )
    }

    const stopCreatingNewConfig = () => {
      textareatransition.value = !textareatransition.value
      isNewConfigInProgress.value = false
      showCurrentConfig()
    }

    watch(createNewElementsConfig, (newVal, oldVal) => {
      if (newVal !== oldVal && textareatransition) {
        textareatransition.value = false
        showCurrentConfig()
      }
    })
    watch(createNewWafel, (newVal, oldVal) => {
      waferChoosedFlag.value = true
    })

    getWaferNumbers()
    getConfigObjects()
    return {
      deleteConfig,
      chunks,
      textareatransition,
      stopCreatingNewConfig,
      postNewConfig,
      continueWithoutSavingConfig,
      lastChunkSize,
      myCounter,
      menu,
      checkIsConfigOk,
      columnWidth,
      createNewElementsConfig,
      monitorElementsConfigCollection,
      addNewElementsConfiguration,
      monitorElementsselected,
      monitorElements,
      statusDialog,
      waferChoosedFlag,
      startupNumbers,
      configName,
      createNewElementsConfigFlag,
      startupNumber,
      currentMissionType,
      timeoutId,
      showCancelBtn,
      waferNumbers,
      snackbar,
      makeNewConfig,
      cancelSend,
      createNewMonitor,
      createNewWafel,
      selectedMonitor,
      measurementRecording,
      technologistName,
      monitors,
      cleanCheckbox,
      sendToServer,
      textfieldhashconfirmcode,
      dynamicServerPushButtonText,
      generatedHashCode,
      goBack,
      comment,
      textArea,
      isNewConfigInProgress,
      TechnoStageRules: [
        (value) => {
          if (value?.length <= 4 && /^\d+$/.test(value)) return true
          else if (!/^\d+$/.test(value)) {
            return 'Технологический этап должен состоять только из цифр'
          } else if (value?.length > 4) {
            return 'Введенное значение слишком длинное'
          }
        }
      ]
    }
  }
}
</script>
<template>
  <div style="height: 3em" />
  <v-sheet
    width="80%"
    class="mx-auto"
  >
    <v-form disabled>
      <v-row> <v-text-field v-model="technologistName" /> </v-row>
    </v-form>
    <v-row>
      <v-autocomplete
        v-model="createNewWafel"
        :disabled="isNewConfigInProgress"
        label="Номер пластины"
        :items="waferNumbers"
      />
    </v-row>
    <v-row>
      <v-autocomplete
        v-model="startupNumber"
        :disabled="!waferChoosedFlag || isNewConfigInProgress"
        label="Номер запуска"
        :items="startupNumbers"
      />
    </v-row>
    <v-row>
      <v-autocomplete
        v-model="createNewMonitor"
        :disabled="!waferChoosedFlag || isNewConfigInProgress"
        label="Монитор для измерения"
        :items="monitors"
      />
    </v-row>
    <v-row v-if="!textareatransition">
      <div style="width: 70%">
        <v-autocomplete
          v-model="createNewElementsConfig"
          label="Готовые наборы элементов для мониторов"
          :disabled="!waferChoosedFlag"
          :items="monitorElementsConfigCollection"
          no-data-text="К сожалению готовых конфигов сейчас нет, но вы можете создать их"
        />
      </div>
      <div style="width: 15%">
        <v-btn
          :disabled="!waferChoosedFlag"
          elevation="2"
          size="x-large"
          style="font-size: medium"
          @click="makeNewConfig()"
        >
          Добавить новый конфиг
        </v-btn>
      </div>
      <div style="width: 15%">
        <v-btn
          :disabled="!waferChoosedFlag"
          elevation="2"
          size="x-large"
          style="font-size: medium"
          @click="deleteConfig()"
        >
          Удалить текущий конфиг
        </v-btn>
      </div>
    </v-row>
    <v-row v-else>
      <v-text-field
        v-model="configName"
        label="Введите название нового конфига..."
      />
      <v-btn
        :disabled="!waferChoosedFlag"
        elevation="2"
        size="x-large"
        style="font-size: medium"
        @click="stopCreatingNewConfig"
      >
        Вернуться к готовым
      </v-btn>
      <v-btn
        :disabled="!waferChoosedFlag"
        elevation="2"
        size="x-large"
        style="font-size: medium"
        @click="checkIsConfigOk()"
      >
        Сохранить
      </v-btn>
    </v-row>
    <v-form>
      <v-row>
        <v-text-field
          v-model="measurementRecording"
          label="Технологический этап"
          :disabled="!waferChoosedFlag || isNewConfigInProgress"
          :rules="TechnoStageRules"
        />
      </v-row>
    </v-form>
  </v-sheet>
  <div
    v-if="addNewElementsConfiguration"
    width="80%"
  >
    <div style="align-items: center; width: 100%">
      <div
        v-for="(chunk, index) in chunks"
        :key="index"
        style="width: max-content; float: none; padding-left: 10%"
      >
        <div
          v-for="(item, key) in chunk"
          :key="key"
          class="column"
        >
          <v-checkbox
            v-model="monitorElementsselected"
            :disabled="!textareatransition"
            style="float: none; width: 14em; height: 2em"
            :value="item"
            :label="item"
          />
        </div>
      </div>
    </div>
  </div>
  <div style="height: 20em" />
  <v-sheet
    v-if="!textareatransition"
    width="400"
    class="mx-auto"
  >
    <v-textarea
      :disabled="!waferChoosedFlag || isNewConfigInProgress"
      clearable
      label="Вы можете оставить комментарий"
      variant="outlined"
      v-model="comment"
    />
    <v-btn
      :disabled="!waferChoosedFlag || isNewConfigInProgress"
      type="submit"
      block
      class="mt-2"
      @click="sendToServer"
    >
      {{ dynamicServerPushButtonText }}
    </v-btn>

    <v-btn
      type="submit"
      block
      class="mt-10"
      @click="goBack"
    >
      Назад
    </v-btn>
  </v-sheet>
  <v-snackbar
    v-model="snackbar"
    location="bottom right"
    :timeout="4000"
    color="warning"
    width="36em"
    height="6em"
  >
    <p style="font-size: large">
      Отменить удаление конфига?
    </p>
    <template #actions>
      <v-btn
        color="black"
        @click=";(snackbar = false), cancelSend()"
      >
        Отменить
      </v-btn>
      <ProgressCircular />
    </template>
  </v-snackbar>
</template>
<style>
.column {
  float: left;
}
.slide-fade-enter-active {
  transition: all 12s ease;
}

.v-selection-control--disabled {
  opacity: var(--v-disabled-opacity);
  color: rgb(30, 7, 135) !important;
}
.v-selection-control {
  color: rgb(30, 7, 135) !important;
}
</style>
<!-- сценарий, если монитор новый, а конфиг старый
     элементы монитора - есть 
     Debounce - удаление (блокировать)
     No-data-available-переделать
     у каждого чекбокса есть свой id
     тех этап не должен реагировать на удаление 
-->