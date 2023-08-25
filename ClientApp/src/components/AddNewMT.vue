<script>
import { ref, watch, onMounted } from 'vue';
import { useThrottleFn } from '@vueuse/core';
import { useRouter } from 'vue-router';
import axios from 'axios';
import crc32 from 'crc/crc32';
import { useStore } from 'vuex';
import ProgressCircular from './ProgressCircular.vue';
import GraphicsComponent from './Graphics.vue';
import MetaComp from './MetaComp.vue';
import {
  getWafers,
  getProcessByWafer
} from '/home/egor/WAFERPATROL/ClientApp/SvrApiClient/svrApi.js';
export default {
  components: {
    ProgressCircular,
    GraphicsComponent
  },
  setup () {
    const getProcessByWaferProxy = async () => { //Тест новой функции api - вставить в логику и удалить 
      console.log(await getProcessByWafer(createNewWafel.value));
    };
    const isDeleteButtonDisabled = ref(false);
    const configName = ref();
    const rules = ref([]);
    const createNewElementsConfigFlag = ref(false);
    const waferNumbers = ref([]);
    const myCounter = ref(0);
    const firstMonitorObject = { name: 'Первый монитор', id: 'firstMonitorId' };
    const secondMonitorObject = {
      name: 'Второй монитор',
      id: 'secondMonitorId'
    };
    const valid = ref(false);
    const monitors = ref([firstMonitorObject, secondMonitorObject]); // выгрузка данных с api
    const monitorElementsConfigCollection = ref([]);
    const menu = ref(false);
    const monitorElementsselected = ref([{ id: '', name: '' }]);
    let lastId = '';
    const firstMonitorElements = ref([
      // выгрузка данных с api
      { name: 'TS1', id: '1lc' },
      { name: 'TS2', id: '2d' },
      { name: 'TS3', id: '3a' },
      { name: 'TS4', id: 'sxc4' },
      { name: 'TS5', id: '5da' },
      { name: 'TS6', id: '6' },
      { name: 'TS7', id: '7' },
      { name: 'TS8', id: '8' },
      { name: 'TS9', id: '9' },
      { name: 'TS10', id: '10' },
      { name: 'TS11', id: '11' },
      { name: 'TS12', id: '12' },
      { name: 'TS13', id: '13' },
      { name: 'TS14', id: '14' },
      { name: 'TS15', id: '15' },
      { name: 'TS16', id: '16' },
      { name: 'TS17', id: '17' },
      { name: 'TS18', id: '18' },
      { name: 'TS19', id: '19' },
      { name: 'TS20', id: '20' },
      { name: 'TS21', id: '21' },
      { name: 'TS22', id: '22' },
      { name: 'TS23', id: '23' },
      { name: 'TS24', id: '24' },
      { name: 'TS25', id: '25' },
      { name: 'TS26', id: '26' },
      { name: 'TS27', id: '27' },
      { name: 'TS28', id: '28' },
      { name: 'TS29', id: '29' },
      { name: 'TS30', id: '30' },
      { name: 'TS31', id: '31' },
      { name: 'TS32', id: '32' },
      { name: 'TS33', id: '33' },
      { name: 'TS34', id: '34' },
      { name: 'TS35', id: '35' },
      { name: 'TS36', id: '36' },
      { name: 'TS37', id: '37' },
      { name: 'TS38', id: '38' },
      { name: 'TS39', id: '39' },
      { name: 'TS40', id: '40' }
    ]);
    const secondMonitorElements = ref([
      // выгрузка данных с api
      { name: 'TS41', id: '41' },
      { name: 'TS42', id: '42' },
      { name: 'TS43', id: '43' },
      { name: 'TS44', id: '44' },
      { name: 'TS45', id: '45' },
      { name: 'TS46', id: '46' },
      { name: 'TS47', id: '47' },
      { name: 'TS48', id: '48' },
      { name: 'TS49', id: '49' },
      { name: 'TS50', id: '50' },
      { name: 'TS51', id: '51' },
      { name: 'TS52', id: '52' },
      { name: 'TS53', id: '53' },
      { name: 'TS54', id: '54' },
      { name: 'TS55', id: '55' },
      { name: 'TS56', id: '56' },
      { name: 'TS57', id: '57' },
      { name: 'TS58', id: '58' },
      { name: 'TS59', id: '59' },
      { name: 'TS60', id: '60' },
      { name: 'TS61', id: '61' },
      { name: 'TS62', id: '62' },
      { name: 'TS63', id: '63' },
      { name: 'TS64', id: '64' }
    ]);
    const monitorElements = ref([
      { name: 'TS1', id: '1lc' },
      { name: 'TS2', id: '2d' },
      { name: 'TS3', id: '3a' },
      { name: 'TS4', id: 'sxc4' },
      { name: 'TS5', id: '5da' },
      { name: 'TS6', id: '6' },
      { name: 'TS7', id: '7' },
      { name: 'TS8', id: '8' },
      { name: 'TS9', id: '9' },
      { name: 'TS10', id: '10' },
      { name: 'TS11', id: '11' },
      { name: 'TS12', id: '12' },
      { name: 'TS13', id: '13' },
      { name: 'TS14', id: '14' },
      { name: 'TS15', id: '15' },
      { name: 'TS16', id: '16' },
      { name: 'TS17', id: '17' },
      { name: 'TS18', id: '18' },
      { name: 'TS19', id: '19' },
      { name: 'TS20', id: '20' },
      { name: 'TS21', id: '21' },
      { name: 'TS22', id: '22' },
      { name: 'TS23', id: '23' },
      { name: 'TS24', id: '24' },
      { name: 'TS25', id: '25' },
      { name: 'TS26', id: '26' },
      { name: 'TS27', id: '27' },
      { name: 'TS28', id: '28' },
      { name: 'TS29', id: '29' },
      { name: 'TS30', id: '30' },
      { name: 'TS31', id: '31' },
      { name: 'TS32', id: '32' },
      { name: 'TS33', id: '33' },
      { name: 'TS34', id: '34' },
      { name: 'TS35', id: '35' },
      { name: 'TS36', id: '36' },
      { name: 'TS37', id: '37' },
      { name: 'TS38', id: '38' },
      { name: 'TS39', id: '39' },
      { name: 'TS40', id: '40' },
      { name: 'TS41', id: '41' },
      { name: 'TS42', id: '42' },
      { name: 'TS43', id: '43' },
      { name: 'TS44', id: '44' },
      { name: 'TS45', id: '45' },
      { name: 'TS46', id: '46' },
      { name: 'TS47', id: '47' },
      { name: 'TS48', id: '48' },
      { name: 'TS49', id: '49' },
      { name: 'TS50', id: '50' },
      { name: 'TS51', id: '51' },
      { name: 'TS52', id: '52' },
      { name: 'TS53', id: '53' },
      { name: 'TS54', id: '54' },
      { name: 'TS55', id: '55' },
      { name: 'TS56', id: '56' },
      { name: 'TS57', id: '57' },
      { name: 'TS58', id: '58' },
      { name: 'TS59', id: '59' },
      { name: 'TS60', id: '60' },
      { name: 'TS61', id: '61' },
      { name: 'TS62', id: '62' },
      { name: 'TS63', id: '63' },
      { name: 'TS64', id: '64' }
    ]);
    const comment = ref();
    const timeoutId = ref(null);
    const showCancelBtn = ref(false);
    const snackbar = ref(false);
    const store = useStore();
    const startupNumbers = ref(['Первый запуск', 'Второй запуск']);
    const textArea = ref();
    const dynamicServerPushButtonText = ref('Отправить данные');
    const statusDialog = ref(false);
    const textfieldhashconfirmcode = ref('');
    const generatedHashCode = ref();
    const createNewMonitor = ref('');
    const createNewElementsConfig = ref({ id: '', name: '' });
    const createNewWafel = ref();
    const addNewElementsConfiguration = ref(false);
    const selectedMonitor = ref();
    const measurementRecording = ref();
    const startupNumber = ref();
    const router = useRouter();
    const allWaffelsInLabArray = ref([]);
    const currentConfigObjects = ref([]);
    const allConfigObjects = ref([]);
    const waferChoosedFlag = ref(false);
    const monitorChoosedFlag = ref(false);
    const currentMissionType = ref();
    const textareatransition = ref(false);
    const isNewConfigInProgress = ref(false);
    const securityBlock = ref(false);
    const sendBtnPushed = ref(false);
    const goBack = () => {
      router.push('/Technologist/WelcomeBack');
    };
    const isAllFieldsComplitedCheck = () => {
      if (
        !createNewMonitor.value ||
        !createNewWafel.value ||
        !measurementRecording.value ||
        !startupNumber.value ||
        !monitorElementsselected.value ||
        !createNewElementsConfig.value ||
        !valid.value ||
        createNewElementsConfig.value.id === '' ||
        createNewElementsConfig.value.name === ''
      ) {
        return false;
      } else return true;
    };
    const fillWafelPole = () => {
      const wafer = {
        id: 'id', // //  APISVR2.0
        waferId: createNewWafel.value,
        codeProductId: 12, // (ID шаблона) // APISVR2.0
        parcelId: 12 // (ID запуска) //  APISVR2.0
      };
      return wafer;
    };
    const getPreviousMeasurementRecording = () => {
      // APISVR2.0
      const returnedResult = 1000;
      if (measurementRecording.value >= returnedResult) {
        return measurementRecording.value;
      }
      store.dispatch('throwMessage', {
        type: 'error',
        name: 'FormNotComplited'
      });
    };

    const fillMeasurementRecording = () => {
      const measurementRecordingObj = {
        id: 12, // // APISVR2.0
        name: 'name', // APISVR2.0

        stageId: 12, // // APISVR2.0
        measurementDate: new Date()
      };
      return measurementRecordingObj;
    };
    const fillStage = () => {
      const stage = {
        stageId: getPreviousMeasurementRecording(), // APISVR2.0
        stageName: 'name', // APISVR2.0
        codeProductId: 12 // APISVR2.0
      };
      return stage;
    };
    const fillParcel = () => {
      const parcel = {
        id: 12, // APISVR2.0
        name: startupNumber.value // APISVR2.0
      };
      return parcel;
    };
    const fillDieType = () => {
      const dieType = {
        dieTypeId: 12, // APISVR2.0
        name: createNewMonitor.value.name
      };
      return dieType;
    };
    const createGraphArray = () => {
      const graphArray = store.getters.getElementGraphsArray;
      return {
        graphArray
      };
    };
    const createNewMtObject = () => {
      if (!comment.value) {
        comment.value = '';
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

        monitorConfig: createNewConfigObject(), // createNewElementsConfig.value,
        comment: comment.value,
        graphArray: createGraphArray().graphArray
      };
      return newMtObject;
    };
    const sendToServer = () => {
      sendBtnPushed.value = true;
      generatedHashCode.value = crc32(new Date().toString()).toString(16);

      if (isAllFieldsComplitedCheck()) {
        postMT();
        sendBtnPushed.value = false;
      } else {
        store.dispatch('throwMessage', {
          type: 'error',
          name: 'FormNotComplited'
        });
      }
    };
    const chunksMaker = () => {
      const chunkSize = 9;
      const chunks = [];

      for (let i = 0; i < monitorElements.value.length; i += chunkSize) {
        chunks.push(monitorElements.value.slice(i, i + chunkSize));
      }
      return chunks;
    };
    const chunks = ref([chunksMaker()]);
    const lastChunkSize = ref(chunks.value.at(-1).length);
    const columnWidth = ref(
      (100 / chunks.value[0].length).toString().concat('%')
    );
    const technologistName = `${window.localStorage.getItem(
      'firstName'
    )} ${window.localStorage.getItem('lastName')}`;

    const getWaferNumbers = async () => {
      //функция на получение апи
      try {
        allWaffelsInLabArray.value = await getWafers();
        waferNumbers.value = allWaffelsInLabArray.value.map(function (item) {
          return item.waferId;
        });
      } catch (error) {
        store.dispatch('throwMessage', {
          type: 'error',
          name: 'serverLoadingError'
        });
      }
    };
    const makeNewConfig = () => {
      isNewConfigInProgress.value = true;
      textareatransition.value = true;
      addNewElementsConfiguration.value = true;
      cleanCheckbox();
    };

    const showCurrentConfig = () => {
      if (createNewElementsConfig.value.id !== '') {
        addNewElementsConfiguration.value = true;
        monitorElementsselected.value = currentConfigObjects.value.find(
          (el) => createNewElementsConfig.value.id === el.id
        ).elements;
      } else {
        monitorElementsselected.value = [''];
      }
    };

    const cleanCheckbox = () => {
      monitorElementsselected.value = [];
    };

    const postMT = async () => {
      try {
        await axios.post(
          'https://localhost:3000/api/MT/Post',
          createNewMtObject()
        );
        store.dispatch('throwMessage', {
          type: 'success',
          name: 'deliverySuccsess'
        });
        store.dispatch('cleanElementGraphsArray');
      } catch (error) {
        store.dispatch('throwMessage', {
          type: 'error',
          name: 'serverPostError'
        });
      }
    };
    const getConfigObjects = async () => {
      try {
        const response = await axios.get(
          'https://localhost:3000/api/MonitorConfig/Get'
        );
        allConfigObjects.value = response.data;
      } catch (error) {
        store.dispatch('throwMessage', {
          type: 'error',
          name: 'serverLoadingError'
        });
      }
    };
    const createNewConfigObject = () => {
      // textareatransition.value = true
      isNewConfigInProgress.value = false;
      lastId = crc32(new Date().toString()).toString(16);
      return {
        id: lastId,
        matchableMonitors: [createNewMonitor.value.id],
        name: configName.value,
        elements: monitorElementsselected.value
      };
    };
    const postNewConfig = async () => {
      createNewElementsConfig.value = createNewConfigObject();
      currentConfigObjects.value.push(createNewElementsConfig.value);
      // createNewElementsConfig.value = createNewConfigObject()
      try {
        await axios.post(
          'https://localhost:3000/api/MonitorConfig/Post',
          createNewConfigObject()
        );
        createNewElementsConfig.value.elements = monitorElementsselected.value;
        createNewElementsConfig.value.id = lastId;
        addNewElementsConfiguration.value = false;

        configName.value = '';
        store.dispatch('throwMessage', {
          type: 'success',
          name: 'configSuccsess'
        });
      } catch (error) {
        store.dispatch('throwMessage', {
          type: 'error',
          name: 'serverPostError'
        });
      }
      stopCreatingNewConfig();
    };
    const isCheckboxesNotEmpty = () => {
      if (monitorElementsselected.value.length > 0) return true;
      else {
        store.dispatch('throwMessage', {
          type: 'error',
          name: 'monitorElementsEmpty'
        });
        return false;
      }
    };
    const nameIsBuissy = () => {
      if (
        !currentConfigObjects.value
          .map(function (item) {
            return item.name;
          })
          .includes(configName.value)
      ) {
        return true;
      } else {
        store.dispatch('throwMessage', {
          type: 'error',
          name: 'nameIsBuissy'
        });
        return false;
      }
    };

    function areArraysEqual (configs, elements) {
      for (let i = 0; i < configs.length; i++) {
        if (
          JSON.stringify(configs[i].elements.sort()) ===
          JSON.stringify(elements.sort())
        ) {
          return true;
        }
      }

      return false;
    }

    const isConfigElementsAlreadyExists = () => {
      if (
        areArraysEqual(
          currentConfigObjects.value,
          monitorElementsselected.value
        )
      ) {
        store.dispatch('throwMessage', {
          type: 'error',
          name: 'configAlreadyExists'
        });
        return true;
      } else {
        return false;
      }
    };
    const isConfigNotEmpty = () => {
      if (configName.value) return true;
      else {
        store.dispatch('throwMessage', {
          type: 'error',
          name: 'configNameEmpty'
        });
        return false;
      }
    };
    const checkIsConfigOk = () => {
      if (
        isConfigNotEmpty() &&
        nameIsBuissy() &&
        isCheckboxesNotEmpty() &&
        !isConfigElementsAlreadyExists()
      ) {
        postNewConfig();
        securityBlock.value = false;
      }
      //
    };
    const isAnyObjectExists = () => {
      if (currentConfigObjects.value.length > 0) return true;
      else {
        store.dispatch('throwMessage', {
          type: 'error',
          name: 'NoConfigsToDelete'
        });
        return false;
      }
    };
    const isAnyConfigChoosedToDelete = () => {
      if (
        !createNewElementsConfig.value ||
        createNewElementsConfig.value.id === '' ||
        createNewElementsConfig.value.name === ''
      ) {
        store.dispatch('throwMessage', {
          type: 'error',
          name: 'NoConfigToDeleteChosen'
        });
        return false;
      } else return true;
    };
    const deleteConfigWithThrottle = () => {
      if (isAnyObjectExists() && isAnyConfigChoosedToDelete()) {
        snackbar.value = true;
        timeoutId.value = setTimeout(async () => {
          try {
            await axios.post(
              'https://localhost:3000/api/MonitorConfig/Delete',
              findConfigForRemoving()
            );
            store.dispatch('throwMessage', {
              type: 'success',
              name: 'configSuccsessRemoved'
            });
            snackbar.value = false;
            removeConfigFromArray(findConfigForRemoving());
          } catch (error) {
            store.dispatch('throwMessage', {
              type: 'error',
              name: 'serverPostError'
            });
          }
          cancelSend();
        }, 5500);
      }
    };

    const deleteConfig = useThrottleFn(() => {
      console.log('Hello');
      deleteConfigWithThrottle();
    }, 5500);

    const chooseAutoMatchConfig = () => {
      try {
        const temp = currentConfigObjects.value.find((config) => {
          return config.elements
            .map((i) => i.id)
            .every((e) => monitorElements.value.map((i) => i.id).includes(e));
        });
        configName.value = temp.name;
        createNewElementsConfig.value = temp; // выбрать первый подходящий конфиг
        securityBlock.value = false;
      } catch {
        cleanCheckbox();
        createNewElementsConfig.value = { id: '', name: '' };
        createNewElementsConfig.value.elements = [];
        store.dispatch('throwMessage', {
          type: 'error',
          name: 'NoConfigsToDelete'
        });
        securityBlock.value = false;
      }
    };
    const isConfigMatchMonitor = (myConfig) => {
      if (
        myConfig.value.elements
          .map((i) => i.id)
          .every((e) => monitorElements.value.map((i) => i.id).includes(e))
      ) {
        // стар
      } else {
        store.dispatch('throwMessage', {
          type: 'warning',
          name: 'configDoesntMatch'
        });
        securityBlock.value = true;
        setTimeout(() => {
          chooseAutoMatchConfig();
        }, 1500);
      }
    };

    const cancelSend = () => {
      clearTimeout(timeoutId.value);
      timeoutId.value = null;
      snackbar.value = false;
    };

    const removeConfigFromArray = (objectToRemove) => {
      currentConfigObjects.value = currentConfigObjects.value.filter(
        (e) => e !== objectToRemove
      );
      createNewElementsConfig.value = currentConfigObjects.value[0];
    };
    const findConfigForRemoving = () => {
      return currentConfigObjects.value.find(
        (element) => element.id === createNewElementsConfig.value.id
      );
    };
    const stopCreatingNewConfig = () => {
      textareatransition.value = false;
      isNewConfigInProgress.value = false;
      showCurrentConfig(); // Отображение свежесозданного
    };
    watch(
      // сделать проверку конфига на пустоту перед отправкой
      () => createNewElementsConfig.value,
      (newVal, oldVal) => {
        if (
          newVal &&
          newVal !== oldVal &&
          textareatransition &&
          newVal.name !== '' &&
          newVal.id !== ''
        ) {
          textareatransition.value = false;
          showCurrentConfig();
          isConfigMatchMonitor(createNewElementsConfig);
          store.dispatch('cleanElementGraphsArray');
        }
      }
    );
    watch(createNewWafel, (newVal, oldVal) => {
      waferChoosedFlag.value = true;
    });
    watch(
      () => createNewMonitor.value,
      (newVal, oldVal) => {
        if (newVal && newVal !== oldVal) {
          if (newVal.id === secondMonitorObject.id) {
            monitorElements.value = secondMonitorElements.value;
          } else {
            monitorElements.value = firstMonitorElements.value;
          }
          currentConfigObjects.value = allConfigObjects.value.filter(function (
            el
          ) {
            return el.matchableMonitors.includes(newVal.id);
          });
          chunks.value = chunksMaker();
          lastChunkSize.value = chunks.value.at(-1).length;
          columnWidth.value = (100 / chunks.value[0].length)
            .toString()
            .concat('%');
          configName.value = '';
          createNewElementsConfig.value = { id: '', name: '' };
          cleanCheckbox();
          monitorChoosedFlag.value = true;
        }
      }
    );
    getWaferNumbers();
    getConfigObjects();
    return {
      getProcessByWaferProxy,
      deleteConfig,
      chunks,
      textareatransition,
      stopCreatingNewConfig,
      postNewConfig,
      lastChunkSize,
      myCounter,
      menu,
      sendBtnPushed,
      checkIsConfigOk,
      columnWidth,
      createNewElementsConfig,
      monitorElementsConfigCollection,
      addNewElementsConfiguration,
      monitorElementsselected,
      monitorElements,
      statusDialog,
      waferChoosedFlag,
      monitorChoosedFlag,
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
      AllConfigObjects: currentConfigObjects,
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
      securityBlock,
      generatedHashCode,
      goBack,
      comment,
      rules,
      textArea,
      valid,
      isDeleteButtonDisabled,
      isNewConfigInProgress,
      TechnoStageRules: [
        (value) => {
          if (value?.length <= 4 && /^\d+$/.test(value)) return true;
          else if (!/^\d+$/.test(value)) {
            return 'Технологический этап должен состоять только из цифр';
          } else if (value?.length > 4) {
            return 'Введенное значение слишком длинное';
          }
        }
      ]
    };
  }
};
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
        no-data-text="Пластин с таким номером нет в базе"
      />
    </v-row>
    <v-row>
      <v-autocomplete
        v-model="startupNumber"
        :disabled="!waferChoosedFlag || isNewConfigInProgress"
        label="Номер запуска"
        :items="startupNumbers"
        no-data-text="В базе нет запуска с таким номером"
      />
    </v-row>
    <v-row>
      <v-autocomplete
        v-model="createNewMonitor"
        :disabled="!waferChoosedFlag || isNewConfigInProgress"
        label="Монитор для измерения"
        no-data-text="К сожалению, такого монитора нет в базе"
        item-value="id"
        item-title="name"
        :items="monitors"
        return-object
      />
    </v-row>
    <v-row v-if="!textareatransition">
      <div style="width: 70%">
        <v-autocomplete
          v-model="createNewElementsConfig"
          :disabled="!monitorChoosedFlag"
          :items="AllConfigObjects"
          item-value="id"
          item-title="name"
          no-data-text="К сожалению, готовых конфигов сейчас нет, но вы можете создать их"
          return-object
        />
      </div>
      <div style="width: 15%">
        <v-btn
          :disabled="!monitorChoosedFlag"
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
          :disabled="!monitorChoosedFlag"
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
        text="Вернуться к готовым"
      />
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

    <v-form v-model="valid">
      <v-row>
        <v-text-field
          v-model="measurementRecording"
          label="Технологический этап"
          :disabled="!waferChoosedFlag || isNewConfigInProgress"
          :rules="TechnoStageRules"
          no-data-text="К сожалению, такого технологического этапа нет в базе"
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
            :label="item.name"
          />
        </div>
      </div>
    </div>
    <div style="height: 15em" />
    <div
      style="width: 90%; padding-left: 10%"
      v-if="
        !textareatransition &&
          createNewElementsConfig &&
          createNewElementsConfig.id !== '' &&
          waferChoosedFlag &&
          !isNewConfigInProgress &&
          !securityBlock
      "
    >
      <GraphicsComponent :choosed-config-elements="monitorElementsselected" />
    </div>
  </div>
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
      :disabled="!waferChoosedFlag || isNewConfigInProgress || securityBlock"
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
  <v-btn @click="getProcessByWaferProxy()">
    Тест функции api - удалить
  </v-btn>
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
