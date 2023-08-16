<script>
import { ref, watch } from 'vue'
import { useRouter } from 'vue-router'
import axios from 'axios'
import crc32 from 'crc/crc32'
import { useStore } from 'vuex'
export default {
  setup() {
    const testref = ref()
    const GenerateNewWafelString = 'Создать новую пластину'
    const GenerateNewMonitorString = 'Создать новый монитор'
    const GenerateNewElementsConfigString = 'Создать новый набор элементов'
    const configName = ref()
    const waferNumbers = ref([]) //GenerateNewWafelString
    const myCounter = ref(0)
    const monitors = ref([
      'Первый монитор',
      'Второй монитор',
      GenerateNewMonitorString,
    ])
    const monitorElementsConfigCollection = ref([
      'Первый набор',
      'Второй набор',
      'Третий набор',
      'Четвертый набор',
      GenerateNewElementsConfigString,
    ])
    const menu = ref(false)
    const monitorElementsselected = ref([])
    const monitorElements = ref([
      'Programming1',
      'Programming2',
      'Programming3',
      'Programming4',
      'Programming5',
      'Programming6',
      'Programming7',
      'Programming8',
      'Programming9',
      'Programming0',
      'Programming10',
      'Programming11',
      'Programming12',
      'Programming13',
      'Programming14',
      'Programming15',
      'Design',
      'Vue',
      'Vuetify',
      'эиополевой',
      'быковатый',
      'гиппология',
      'отциклеваться',
      'отволаживаться',
      'котировочный',
      'соблазниться',
      'разгерметизация',
      'эмерджентный',
      'цопать',
      'тропление',
      'забивка',
      'зазнобить',
      'геноцид',
      'затрепыхаться',
      'запарник',
      'дублинский',
      'кунктаторский',
      'ено',
      'оклеить',
      'пляжик',
      'ржанкообразные',
      'фуганок',
      'умирать',
      'межкомнатный',
      'домбра',
      'перебарщивание',
      'несообразный',
      'подчалиться',
      'скрытохвост',
      'белобровый',
      'усложнение',
      'лямочка',
      'перхота',
      'доплетать',
      'известись',
      'смачивание',
      'щитонаборный',
      'трудноплавкость',
      'обставиться',
      'умненек',
      'непоэт',
      'витиеватый',
      'гигрометрический',
      'нарастать',
    ])
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
    const dialogText = ref()
    const delegateDialogText = ref()
    const selectedMonitor = ref()
    const measurementRecording = ref()
    const startupNumber = ref()
    const dialog = ref(false)
    const delegateDialog = ref(false)
    const router = useRouter()
    const delegateData = ref()
    const AllWaffelsInLabArray = ref([])
    const AllConfigObjects = ref([])
    const waferChoosedFlag = ref(false)
    const currentMissionType = ref()
    const textareatransition = ref(false)

    const goBack = () => {
      router.push('/Technologist/WelcomeBack')
    }
    const isAllFieldsComplitedCheck = () => {
      if (
        !createNewMonitor.value ||
        !createNewWafel.value ||
        !measurementRecording.value
      )
        return false
      else return true
    }
    const fillWafelPole = () => {
      let wafer = {
        id: 'id', ////  APISVR2.0
        waferId: createNewWafel.value,
        codeProductId: 12, //(ID шаблона) // APISVR2.0
        parcelId: 12, //(ID запуска) //  APISVR2.0
      }
      return wafer
    }
    const getPreviousMeasurementRecording = () => {
      // APISVR2.0
      let returnedResult = 1000
      if (measurementRecording.value >= returnedResult)
        return measurementRecording.value
      store.dispatch('throwMessage', {
        type: 'error',
        name: 'FormNotComplited',
      })
    }
    const fillMeasurementRecording = () => {
      let measurementRecordingObj = {
        id: 12, // // APISVR2.0
        name: 'name', // APISVR2.0
        stageId: 12, //// APISVR2.0
        measurementDate: new Date(),
      }
      return measurementRecordingObj
    }
    const fillMonitorConfig = () => {
      let config = {
        id: 'someId',
        name: createNewElementsConfig.value,
        elements: monitorElementsselected.value,
      }
      return config
    }
    const fillStage = () => {
      let stage = {
        stageId: getPreviousMeasurementRecording(), // APISVR2.0
        stageName: 'name', // APISVR2.0
        codeProductId: 12, // APISVR2.0
      }
      return stage
    }
    const fillParcel = () => {
      let parcel = {
        id: 12, // APISVR2.0
        name: startupNumber.value, // APISVR2.0
      }
      return parcel
    }
    const fillDieType = () => {
      let dieType = {
        dieTypeId: 12, // APISVR2.0
        name: createNewMonitor.value,
      }
      return dieType
    }



    const createNewMtObject = () => {
      let newMtObject = {
        id: generatedHashCode.value,
        status: 'NEW',
        wafer: fillWafelPole(),
        technologist: technologistName,
        measurementRecording: fillMeasurementRecording(),
        stage: fillStage(),
        parcel: fillParcel(),
        dieType: fillDieType(),
        monitorConfig: fillMonitorConfig(),
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
          name: 'FormNotComplited',
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
    //const columnWidth =    "100em"

    const technologistName = `${window.localStorage.getItem(
      'firstName'
    )} ${window.localStorage.getItem('lastName')}`


    const getWaferNumbers = async () => {
      //use removeWafersWithInappropriateType() here insted when API ready
      try {
        const promise = await axios.get(
          'https://localhost:3000/api/WaferInLab/Get'
        )
        console.log(promise)
        const dataPromise = await promise
        let wafels = dataPromise.data
        AllWaffelsInLabArray.value = dataPromise.data
        waferNumbers.value = wafels.map(function (item) {
          return item.id //Проверка на законченность измерений
        })

      } catch (error) {
        console.error(error)
      }
    }
 
    const createNewElementsConfigAction = () => {
      addNewElementsConfiguration.value = true
    }
    const showCurrentConfig = () => {
      if (createNewElementsConfig.value != null) {
        addNewElementsConfiguration.value = true
        monitorElementsselected.value = AllConfigObjects.value.find(
          (el) => createNewElementsConfig.value === el.name
        ).elements
      } else {
        monitorElementsselected.value = ['']
      }
      //СДЕЛАТЬ ПОИСК ПО КОНФИГУ!!!
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
          name: 'deliverySuccsess',
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
      monitorElementsConfigCollection.value.push(
        GenerateNewElementsConfigString
      )
    }
    const createNewConfigObject = () => {
      textareatransition.value = true
      return {
        id: crc32(new Date().toString()).toString(16),
        name: configName.value,
        elements: monitorElementsselected.value,
      }
    }

    const continueWithoutSavingConfig = async () => {
      textareatransition.value = !textareatransition.value
    }
    //в разработке----------------------------------------------------------------------------------------------очистка чекбоксов, новый id,
    /*
      let newMtObject = {
        id: crc32(new Date().toString()).toString(16),
        status: 'NEW',
        wafer: fillWafelPole(),
        technologist: technologistName,
        measurementRecording: fillMeasurementRecording(),
        stage: fillStage(),
        parcel: fillParcel(),
        dieType: fillDieType(),
        monitorConfig: fillMonitorConfig(),
      }
      try {
        let localConfig = createNewConfigObject()
        localConfig.name = 'temp'
        let localObject = createNewMtObject()
        localObject.monitorConfig = localConfig
        newMtObject.monitorConfig = localConfig
        cleanCheckbox()
        const response = await axios.post(
          'https://localhost:3000/api/MT/Post',
          newMtObject
        )

        console.log(response)
        store.dispatch('throwMessage', {
          type: 'success',
          name: 'deliverySuccsess',
        })
      } catch (error) {
        console.error(error)
      }
    }
*/
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
        store.dispatch('throwMessage', {
          type: 'success',
          name: 'configSuccsess',
        })
      } catch (error) {
        console.error(error)
      }
    }
    const checkboxeschanged = () => {
      if (
        monitorElementsselected.value !==
        AllConfigObjects.value.map(function (item) {
          return item.elements //Проверка на законченность измерений
        })
      ) {
        textareatransition.value = true
        createNewElementsConfig.value = GenerateNewElementsConfigString
      }
    }
    // Вызов функции при определенном значении createNew
    watch(createNewMonitor, (newVal, oldVal) => {
      if (newVal === GenerateNewMonitorString) {
        currentMissionType.value = newVal
        createNewMonitorBtnAction()
      }
      currentMissionType.value = newVal
    })

    watch(createNewElementsConfig, (newVal, oldVal) => {
      if (newVal === GenerateNewElementsConfigString) {
        textareatransition.value = true
        cleanCheckbox()
        createNewElementsConfigAction()
      } else if (newVal !== oldVal) {
        textareatransition.value = false
        showCurrentConfig()
      } else addNewElementsConfiguration.value = false
    })
    watch(createNewWafel, (newVal, oldVal) => {
      waferChoosedFlag.value = true
     
    })
   
    getWaferNumbers()
    getConfigObjects()
    return {
      chunks,
      textareatransition,
      checkboxeschanged,
      postNewConfig,
      continueWithoutSavingConfig,
      lastChunkSize,
      myCounter,
      menu,
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
      startupNumber,
      currentMissionType,
      waferNumbers,
      createNewMonitor,
      createNewWafel,
      selectedMonitor,
      measurementRecording,
      technologistName,
      monitors,
      dialog,
      delegateDialog,
      delegateData,
      dialogText,
      delegateDialogText,
      cleanCheckbox,
      sendToServer,
      textfieldhashconfirmcode,
      dynamicServerPushButtonText,
      generatedHashCode,
      goBack,
      textArea,
      TechnoStageRules: [
        (value) => {
          if (value?.length === 4 && /^\d+$/.test(value)) return true
          return 'Технологический этап должен состоять из 4 цифр'
        },
      ],
    }
  },
}
</script>

<template>
   
  <div style="height: 3em"></div>
  <v-sheet width="80%" class="mx-auto">
    <v-form disabled>
      <v-text-field v-model="technologistName"></v-text-field>
    </v-form>
    <v-autocomplete
      v-model="createNewWafel"
      label="Номер пластины"
      :items="waferNumbers"
    ></v-autocomplete>
    <v-autocomplete
      v-model="startupNumber"
      :disabled="!waferChoosedFlag"
      label="Номер запуска"
      :items="startupNumbers"
    ></v-autocomplete>
    <v-autocomplete
      v-model="createNewMonitor"
      :disabled="!waferChoosedFlag"
      label="Монитор для измерения"
      :items="monitors"
    ></v-autocomplete>
    <v-autocomplete
      v-model="createNewElementsConfig"
      label="Готовые наборы элементов для мониторов"
      :disabled="!waferChoosedFlag"
      :items="monitorElementsConfigCollection"
    ></v-autocomplete>
    <v-form>
      <v-text-field
        v-model="measurementRecording"
        label="Технологический этап"
        :disabled="!waferChoosedFlag"
        :rules="TechnoStageRules"
      ></v-text-field>
    </v-form>
  </v-sheet>
  <div v-if="addNewElementsConfiguration" width="80%">
    <div style="align-items: center; width: 100%">
      <div
        v-for="(chunk, index) in chunks"
        :key="index"
        style="width: max-content; float: none; padding-left: 10%"
      >
        <div v-for="(item, key) in chunk" :key="key" class="column">
          <v-checkbox
            v-model="monitorElementsselected"
            style="float: none; width: 14em; height: 2em"
            :value="item"
            :label="item"
            @click="checkboxeschanged"
          ></v-checkbox>
        </div>
      </div>
    </div>

    <div style="height: 20em"></div>
    <v-sheet width="800" class="mx-auto">
      <v-expand-transition>
        <div v-show="textareatransition" style="padding-bottom: 3em">
          <v-expand-x-transition>
            <v-textarea
              v-model="configName"
              rows="3"
              bg-color="indigo-lighten-4"
              color="indigo-darken-2"
              label="Введите имя нового конфига"
              maxlength="128"
              style="float: none; width: 35em"
            ></v-textarea>
          </v-expand-x-transition>
          <v-btn
            color="green"
            style="float: none; width: 300px; margin: 6em; height: 5em"
            @click="postNewConfig"
            >Сохранить текущую конфигурацию</v-btn
          >
          <v-btn
            color="red"
            style="float: none; width: 300px; margin: 6em; height: 5em"
            @click="continueWithoutSavingConfig"
            >Продолжить без сохранения</v-btn
          >
        </div>
      </v-expand-transition>
    </v-sheet>
  </div>
  <v-sheet v-if="!textareatransition" width="400" class="mx-auto">
    <v-btn type="submit" block class="mt-2" @click="sendToServer">{{
      dynamicServerPushButtonText
    }}</v-btn>

    <v-btn type="submit" block class="mt-10" @click="goBack">Назад</v-btn>
  </v-sheet>
</template>
<style>
.column {
  float: left;
}
.slide-fade-enter-active {
  transition: all 12s ease;
}
</style>
