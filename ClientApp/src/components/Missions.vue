<script>
import { ref } from 'vue';
import axios from 'axios';
import { useRouter } from 'vue-router';
export default {
  setup () {
    const router = useRouter();
    const createWafelMissions = ref([]);
    const creeateMeasursMissions = ref([]);
    const createMonitorMissions = ref([]);
    const addChipsBtnText = ref([]);
    const missions = ref([]);
    const offsetTop = ref(0);
    const chunks = ref([]);
    const showableIndexes = ref([]);
    const parseData = (data) => {
      const dateString = data;
      const dateObj = new Date(dateString);
      const day = dateObj.getUTCDate().toString().padStart(2, '0');
      const month = (dateObj.getUTCMonth() + 1).toString().padStart(2, '0');
      const year = dateObj.getUTCFullYear().toString();
      const hours = dateObj.getUTCHours().toString().padStart(2, '0');
      const minutes = dateObj.getUTCMinutes().toString().padStart(2, '0');
      return `${day}-${month}-${year} ${hours}:${minutes}`;
    };
    const fillChipBtnArray = () => {
      for (let i = 0; i < creeateMeasursMissions.value.length; i++) {
        addChipsBtnText.value[i] = 'Показать больше...';
      }
    };
    const createWafel = () => {
      router.push('/Measurer/WelcomeBack');
    };
    const createMonitor = () => {
      router.push('/Measurer/CreateMonitor');
    };
    const createMeasur = () => {
      router.push('/Measurer/CreateMeasur');
    };

    const getMissions = async () => {
      try {
        const promise = await axios.get(
          'https://localhost:3000/api/WaferCreateMission/Get'
        );

        console.log(promise);
        const dataPromise = await promise;
        missions.value = [...dataPromise.data];
        createArreys();
      } catch (error) {
        console.error(error);
      }
    };
    const getMeasurMissions = async () => {
      try {
        const promise = await axios.get('https://localhost:3000/api/MT/Get');

        console.log(promise);
        const dataPromise = await promise;
        creeateMeasursMissions.value = [...dataPromise.data];
        fillChipBtnArray();
      } catch (error) {
        console.error(error);
      }
    };
    const onScroll = (e) => {
      offsetTop.value = e.target.scrollTop;
    };
    const getMore = (index) => {
      if (!showableIndexes.value.includes(index)) {
        showableIndexes.value.push(index);
        addChipsBtnText.value[index] = 'Скрыть...';
      } else {
        showableIndexes.value = showableIndexes.value.filter(
          (number) => number !== index
        );
        addChipsBtnText.value[index] = 'Показать больше...';
      }
    };
    const isCurrentIndexShowable = (index) => {
      return showableIndexes.value.includes(index);
    };
    getMissions();
    getMeasurMissions();
    const createArreys = () => {
      createWafelMissions.value = missions.value.filter(
        (obj) => obj.type === 'generateNewWafel'
      );
      createMonitorMissions.value = missions.value.filter(
        (obj) => obj.type === 'generateNewMonitor'
      );
    };

    const afterItemRendered = (item) => {
      chunks.value = chunksMaker(item);
    };
    const chunksMaker = (item) => {
      const chunkSize = 3;
      const chunks = [];

      for (let i = 0; i < item.monitorConfig.elements.length; i += chunkSize) {
        chunks.push(item.monitorConfig.elements.slice(i, i + chunkSize));
      }

      return chunks;
    };

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
    };
  }
};
</script>

<template>
  <v-container
    id="scroll-target"
    style="max-height: 100%"
    class="overflow-y-auto"
  >
    <div
      v-for="(item, index) in creeateMeasursMissions"
      :key="item.index"
    >
      <v-card
        class="mx-auto"
        style="width: 100%"
      >
        <v-row style="width: auto">
          <v-col class="col">
            <v-card-title
              class="text-h3 text--primary"
              style="padding-top: 1em"
            >
              {{ item.wafer.waferId }}
            </v-card-title>
            <v-card-subtitle
              class="text-h4 text--primary"
              style="padding: 1em"
            >
              Номер пластины
            </v-card-subtitle>
          </v-col>
          <v-divider
            :thickness="8"
            class="border-opacity-25"
            color="indigo-darken-1"
            vertical
          />
          <v-col class="col">
            <v-card-title
              class="text-h4 text--primary"
              style="padding-top: 1em"
            >
              {{ item.technologist }}
            </v-card-title>
            <v-card-subtitle
              class="text-h5 text--primary"
              style="padding: 1em"
            >
              Данные технолога
            </v-card-subtitle>
          </v-col>
          <v-divider
            :thickness="8"
            class="border-opacity-25"
            color="indigo-darken-1"
            vertical
          />
          <v-col class="col">
            <v-card-title
              class="text-h4 text--primary"
              style="padding-top: 1em"
            >
              {{ item.stage.stageName }}
            </v-card-title>
            <v-card-subtitle
              class="text-h5 text--primary"
              style="padding: 1em"
            >
              Данные об этапе
            </v-card-subtitle>
          </v-col>
          <v-divider
            :thickness="8"
            class="border-opacity-25"
            color="indigo-darken-1"
            vertical
          />
          <v-col class="col">
            <v-card-title
              class="text-h5 text--primary"
              style="padding-top: 1em"
            >
              Технологический этап - {{ item.measurementRecording.name }}
            </v-card-title>
            <v-card-subtitle
              class="text-h5 text--primary"
              style="padding: 1em"
            >
              Номер запуска - {{ item.parcel.name }}
            </v-card-subtitle>
          </v-col>
          <v-divider
            :thickness="8"
            class="border-opacity-25"
            color="indigo-darken-1"
            vertical
          />
          <v-col class="col">
            <v-card-subtitle
              class="text-h5 text--primary"
              style="padding: 1em"
            >
              <p>Комментарий технолога:</p>
            </v-card-subtitle>
            <v-row>
              <div style="padding: 1em">
                <p class="text-h10 text--primary">
                  {{ item.comment }}
                </p>
              </div>
            </v-row>
          </v-col>
          <v-divider
            :thickness="8"
            class="border-opacity-25"
            color="indigo-darken-1"
            vertical
          />
          <v-col class="col">
            <v-card-text>
              <p
                class="text-h6 text--primary"
                style="text-align: center"
              >
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
                  {{ obj.name }}
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
                >
                  {{ obj.name }}
                </v-chip>
              </v-chip-group>
              <v-chip-group style="padding-bottom: 1em">
                <v-chip
                  v-if="item.monitorConfig.elements.length > 10"
                  style="color: white; background-color: rgb(48, 79, 254)"
                  @click="getMore(index)"
                >
                  {{ addChipsBtnText[index] }}
                </v-chip>
              </v-chip-group>
            </v-row>
          </v-col>
        </v-row>
        <div style="height: 3em" />
      </v-card>
    </div>
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
