<script>
import { ref, watch } from 'vue'
import { useRouter } from 'vue-router'
import axios from 'axios'
import crc32 from 'crc/crc32'

export default {
  setup() {
    const GenerateNewWafelString = 'Создать новую пластину'
    const GenerateNewMonitorString = 'Создать новый монитор'
    const waferNumbers = ref([]) //GenerateNewWafelString
    const monitors = ref([
      'Первый монитор',
      'Второй монитор',
      GenerateNewMonitorString,
    ])
    const startupNumbers = ref(['Первый запуск', 'Второй запуск'])
    const textArea = ref()
    const dynamicServerPushButtonText = ref('Получить код')
    const statusDialog = ref(false)
    const renderHashAlert = ref(false)
    const DeliverySuccsess = ref(false)
    const FormNotComplitedAlert = ref(false)
    const textfieldhashconfirmcode = ref('')
    const generatedHashCode = ref()
    const isHashGot = ref(false)
    const createNewMonitor = ref('')
    const createNewWafel = ref()
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
    const currentMissionType = ref()
    const goBack = () => {
      router.push('/Technologist/WelcomeBack')
    }
    const isAllFieldsComplitedCheck = () => {
      if (
        !createNewMonitor.value ||
        !createNewWafel.value ||
        !measurementRecording.value ||
        generatedHashCode.value != textfieldhashconfirmcode.value
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
      else FormNotComplitedAlert.value = true
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

    const checkStatus = () => {
      //4 этап лк технолога
      // APISVR2.0
      //if(someFunc) return "NEW"; else return "ReWork"
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
      }
      return newMtObject
    }
    const createHashCode = () => {
      if (!isHashGot.value) {
        DeliverySuccsess.value = false
        isHashGot.value = true
        generatedHashCode.value = crc32(new Date().toString()).toString(16)
        renderHashAlert.value = true
        dynamicServerPushButtonText.value = 'Отправить данные на сервер'
      } else {
        if (isAllFieldsComplitedCheck()) {
          FormNotComplitedAlert.value = false
          DeliverySuccsess.value = true
          postMT()
        } else {
          FormNotComplitedAlert.value = true
        }
      }
    }

    const technologistName = `${window.localStorage.getItem(
      'firstName'
    )} ${window.localStorage.getItem('lastName')}`

    const createNewMonitorBtnAction = () => {
      delegateDialog.value = true
      delegateDialogText.value = 'Задайте данные монитора'
      createNewMonitor.value = ''
    }
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
          if (checkIfWaferInLabCycleEnded(item.id)) return item.id //Проверка на законченность измерений
        })

        waferNumbers.value.push(GenerateNewWafelString)
      } catch (error) {
        console.error(error)
      }
    }
    const createNewWaferBtnAction = () => {
      dialog.value = true
      dialogText.value =
        'Вы можете делегировать создание пластины измерителям или создать ее сами'
      createNewWafel.value = ''
    }
    const createNewWafer = () => {
      dialog.value = false
      router.push('/Technologist/WelcomeBack')
    }
    const delegateCreateNewWafer = () => {
      dialog.value = false
      delegateDialogText.value = 'Введите номер пластины'
      delegateDialog.value = true
    }
    const delegateConfirm = () => {
      delegateDialog.value = false
      postMission()
    }
    const translateType = () => {
      if (currentMissionType.value === GenerateNewWafelString)
        return 'generateNewWafel'
      else return 'generateNewMonitor'
    }
    const removeWafersWithInappropriateType = () => {
      //get array of  WaferCycle with InWork and Stored status, name of array is cycle
      waferNumbers.value = cycle.map(function (item) {
        if (item.cycleStatus === 'InWork' || item.cycleStatus === 'Stored')
          return item.waferId
      })
    }
    const checkIfWaferInLabCycleEnded = (waferId) => {
      let numberOfEndedStages = AllWaffelsInLabArray.value.find(
        (obj) => obj.id === waferId
      ).stages.length
      if (numberOfEndedStages < 3) return true
      else return false
    }
    const cleanCheckbox = () => {
      createNewMonitor.value = ''
    }
    const createMission = () => {
      let curentTime = new Date()
      curentTime.setHours(new Date().getHours() + 3)
      curentTime.setMinutes(new Date().getMinutes())
      curentTime.setSeconds(new Date().getSeconds())

      let mission = {
        id: crc32(new Date().toString()).toString(16),
        type: translateType(),
        number: delegateData.value,
        creationTime: curentTime,
      }
      return mission
    }
    const appendChangeStstatusInfo = () => {
      statusDialog.value = false
      //send reason of changed status
    }
    const postMission = async () => {
      try {
        const response = await axios.post(
          'https://localhost:3000/api/WaferCreateMission/Post',
          createMission()
        )

        console.log(response)
      } catch (error) {
        console.error(error)
      }
    }
    const postMT = async () => {
      try {
        const response = await axios.post(
          'https://localhost:3000/api/MT/Post',
          createNewMtObject()
        )

        console.log(response)
      } catch (error) {
        console.error(error)
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
    watch(createNewWafel, (newVal, oldVal) => {
      if (newVal === GenerateNewWafelString) {
        currentMissionType.value = newVal

        createNewWaferBtnAction()
      }
    })
    getWaferNumbers()
    return {
      statusDialog,
      startupNumbers,
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
      createNewWafer,
      delegateCreateNewWafer,
      delegateConfirm,
      createMission,
      delegateDialog,
      delegateData,
      dialogText,
      delegateDialogText,
      cleanCheckbox,
      createHashCode,
      textfieldhashconfirmcode,
      dynamicServerPushButtonText,
      renderHashAlert,
      generatedHashCode,
      FormNotComplitedAlert,
      goBack,
      appendChangeStstatusInfo,
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
  <div style="float: left">
    <v-alert
      v-if="FormNotComplitedAlert"
      text="Данные внесены некорректно"
      type="error"
      style="margin-left: 5em; margin-bottom: 2em"
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
      {{ generatedHashCode }}
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
  <div>
    <v-dialog v-model="dialog" width="50em">
      <v-card>
        <v-card-text>
          {{ dialogText }}
        </v-card-text>
        <v-card-actions>
          <v-btn color="red" block @click="createNewWafer()"
            >Создать лично</v-btn
          >
        </v-card-actions>
        <v-card-actions>
          <v-btn color="green" block @click="delegateCreateNewWafer()"
            >Делегировать</v-btn
          >
        </v-card-actions>
      </v-card>
    </v-dialog>

    <v-dialog v-model="statusDialog" persistent width="35em" height="35em">
      <template #activator="{ props }"> </template>
      <v-card>
        <v-card-title class="text-h8">
          {{ dialogText }}
        </v-card-title>

        <v-textarea
          v-model="textArea"
          label="Причина проведения повторного измерения"
        ></v-textarea>

        <v-card-actions>
          <v-spacer></v-spacer>
          <v-btn
            color="green-darken-1"
            variant="text"
            @click="statusDialog = false"
          >
            Назад
          </v-btn>
          <v-btn
            color="green-darken-1"
            variant="text"
            @click="appendChangeStstatusInfo()"
          >
            Готово
          </v-btn>
        </v-card-actions>
      </v-card>
    </v-dialog>
    <v-dialog v-model="delegateDialog" width="50em">
      <v-card>
        <v-card-text> {{ delegateDialogText }} </v-card-text>
        <v-card-actions>
          <v-text-field v-model="delegateData"></v-text-field>
        </v-card-actions>
        <v-card-actions>
          <v-btn color="green" block @click="delegateConfirm()"
            >Подтвердить</v-btn
          >
        </v-card-actions>
      </v-card>
    </v-dialog>
    <div style="height: 10em"></div>
    <v-sheet width="500" class="mx-auto">
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
        label="Номер запуска"
        :items="startupNumbers"
      ></v-autocomplete>
      <v-autocomplete
        v-model="createNewMonitor"
        label="Монитор для измерения"
        :items="monitors"
      ></v-autocomplete>

      <v-sheet v-if="createNewMonitor !== ''" width="500" class="mx-auto">
        <v-row width="10em">
          <v-checkbox label="Checkbox"></v-checkbox>
          <v-checkbox label="Checkbox"></v-checkbox>
          <v-checkbox label="Checkbox"></v-checkbox>
        </v-row>
        <v-row>
          <v-checkbox label="Checkbox"></v-checkbox>
          <v-checkbox label="Checkbox"></v-checkbox>
          <v-checkbox label="Checkbox"></v-checkbox>
        </v-row>
        <v-btn variant="plain" @click="cleanCheckbox"> Очистить </v-btn>
      </v-sheet>
      <v-form>
        <v-text-field
          v-model="measurementRecording"
          label="Технологический этап"
          :rules="TechnoStageRules"
        ></v-text-field>
      </v-form>
    </v-sheet>
  </div>
  <v-sheet width="400" class="mx-auto">
    <v-btn type="submit" block class="mt-2" @click="createHashCode">{{
      dynamicServerPushButtonText
    }}</v-btn>

    <v-btn type="submit" block class="mt-10" @click="goBack">Назад</v-btn>
  </v-sheet>
</template>
