<script>
import { ref, watch } from 'vue'
import { useRouter } from 'vue-router'
import axios from 'axios'
import crc32 from 'crc/crc32'

export default {
  setup() {
    const GenerateNewWafelString = 'Создать новую пластину'
    const GenerateNewMonitorString = 'Создать новый монитор'
    const waferNumbers = ref([
      '1',
      '2',
      '3',
      '4',
      '5',
      '6',
      GenerateNewWafelString,
    ])
    const monitors = ref([
      'Первый монитор',
      'Второй монитор',
      GenerateNewMonitorString,
    ])
    const createNewMonitor = ref()
    const createNewWafel = ref()
    const dialogText = ref()
    const delegateDialogText = ref()
    const selectedMonitor = ref()
    const measurementRecording = ref()
    const dialog = ref(false)
    const delegateDialog = ref(false)
    const router = useRouter()
    const delegateData = ref()
    const currentMissionType = ref()
    const technologistName = `${window.localStorage.getItem(
      'firstName'
    )} ${window.localStorage.getItem('lastName')}`

    const createNewMonitorBtnAction = () => {
      delegateDialog.value = true
      delegateDialogText.value = 'Задайте данные монитора'
      createNewMonitor.value = ''
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

    return {
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
    }
  },
}
</script>

<template>
  <v-dialog v-model="dialog" width="50em">
    <v-card>
      <v-card-text>
        {{ dialogText }}
      </v-card-text>
      <v-card-actions>
        <v-btn color="red" block @click="createNewWafer()">Создать лично</v-btn>
      </v-card-actions>
      <v-card-actions>
        <v-btn color="green" block @click="delegateCreateNewWafer()"
          >Делегировать</v-btn
        >
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
      v-model="createNewMonitor"
      label="Монитор для измерения"
      :items="monitors"
    ></v-autocomplete>
    <v-form>
      <v-text-field
        v-model="measurementRecording"
        label="Технологический этап"
      ></v-text-field>
    </v-form>
    <router-link :to="'/Technologist/WelcomeBack'">Назад</router-link>
  </v-sheet>
</template>
