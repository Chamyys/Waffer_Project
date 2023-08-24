<script>
import { ref } from 'vue';
import { useRouter } from 'vue-router';
import axios from 'axios';
export default {
  setup () {
    const stagesNames = ref([
      'Пластина зарегистрирована',
      'Пластина убыла на измерения',
      'Пластина измеряется',
      'Измерения окончены',
      'Пластина готова'
    ]);
    const loading = ref(false);
    const wafelNumber = ref();
    const router = useRouter();
    const dialog = ref(true);
    const stages = ref(['']);
    const click2 = ref(false);
    const stageOne = ref([]);
    const choosedIndex = ref(0);
    const getNumberOfStages = (stages) => {
      return stages.length;
    };
    const dialogText = ref(
      'Введите номер пластины для отслеживания истории ее состояний'
    );
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
    const goBack = () => {
      router.push(router.options.history.state.back);
    };
    const getStage = (key) => {
      choosedIndex.value = key;
    };
    const getCurentWaferInfo = async () => {
      try {
        const promise = await axios.get(
          'https://localhost:3000/api/WaferInLab/GetById',
          {
            params: {
              id: wafelNumber.value
            }
          }
        );
        console.log(promise);
        const dataPromise = await promise;
        stages.value = dataPromise.data;

        if (dataPromise.status === 204) {
          dialogText.value =
            'Такой пластины не существует, пожалуйста проверьте номер';
        } else {
          stagesNames.value = [
            ...stagesNames.value.slice(
              0,
              getNumberOfStages(stages.value.stages)
            )
          ];
          dialog.value = false;
          stageOne.value = stages.value.stages[0];
          loading.value = true;
        }
      } catch (error) {
        console.error(error);
      }
    };
    return {
      choosedIndex,
      dialog,
      loading,
      goBack,
      getCurentWaferInfo,
      stages,
      click2,
      wafelNumber,
      stageOne,
      dialogText,
      parseData,
      stagesNames,
      getStage
    };
  }
};
</script>
<template>
  <v-progress-circular
    v-if="!loading"
    indeterminate
    :size="100"
  />

  <v-card v-if="loading">
    <v-tabs
      fixed-tabs
      bg-color="green-darken-1"
    >
      <v-tab
        v-for="(name, index) in stagesNames"
        :key="index"
        @click="getStage(index)"
      >
        {{ name }}
      </v-tab>
    </v-tabs>
  </v-card>
  <v-row justify="center">
    <v-dialog
      v-model="dialog"
      persistent
      width="auto"
    >
      <template #activator="{ props }" />
      <v-card>
        <v-card-title class="text-h8">
          {{ dialogText }}
        </v-card-title>

        <v-text-field
          v-model="wafelNumber"
          label="Номер пластины"
        />
        <v-card-actions>
          <v-spacer />
          <v-btn
            color="green-darken-1"
            variant="text"
            @click=";(dialog = false), goBack()"
          >
            Назад
          </v-btn>
          <v-btn
            color="green-darken-1"
            variant="text"
            @click="getCurentWaferInfo()"
          >
            Готово
          </v-btn>
        </v-card-actions>
      </v-card>
    </v-dialog>
  </v-row>
  <div style="height: 10em" />
  <v-row justify="center">
    <v-timeline v-if="loading">
      <v-timeline-item>
        <template #opposite>
          <div style="width: 28em; height: auto" />
          <!--Костыль убрать по возможности-->
        </template>
        <v-card>
          <v-card-title :class="['text-h6', `bg-green-lighten-3`]">
            Пластина зарегистрирована
          </v-card-title>
          <v-card-text class="text--primary bg-white">
            <div class="text-h6">
              Информация по этапу:
            </div>
            <p>Этап начат - {{ parseData(stageOne.returnedTime) }}</p>
            <p>Технолог - {{ stageOne.technologist }}</p>
            <p>Измеритель - {{ stageOne.measurer }}</p>
          </v-card-text>
        </v-card>
      </v-timeline-item>
      <v-timeline-item v-if="choosedIndex > 0">
        <v-card>
          <v-card-title :class="['text-h6', `bg-green-lighten-3`]">
            Пластина убыла на измерения
          </v-card-title>
          <v-card-text class="text--primary bg-white">
            <div class="text-h6">
              Информация по этапу:
            </div>
            <p>Этап начат - {{ parseData(stageOne.returnedTime) }}</p>
            <p>Технолог - {{ stageOne.technologist }}</p>
            <p>Измеритель - {{ stageOne.measurer }}</p>
          </v-card-text>
        </v-card>
      </v-timeline-item>
      <v-timeline-item v-if="choosedIndex > 1">
        <v-card>
          <v-card-title :class="['text-h6', `bg-green-lighten-3`]">
            Пластина измеряется
          </v-card-title>
          <v-card-text class="text--primary bg-white">
            <div class="text-h6">
              Информация по этапу:
            </div>
            <p>Этап начат - {{ parseData(stageOne.returnedTime) }}</p>
            <p>Технолог - {{ stageOne.technologist }}</p>
            <p>Измеритель - {{ stageOne.measurer }}</p>
          </v-card-text>
        </v-card>
      </v-timeline-item>
      <v-timeline-item v-if="choosedIndex > 2">
        <v-card>
          <v-card-title :class="['text-h6', `bg-green-lighten-3`]">
            Измерения окончены
          </v-card-title>
          <v-card-text class="text--primary bg-white">
            <div class="text-h6">
              Информация по этапу:
            </div>
            <p>Этап начат - {{ parseData(stageOne.returnedTime) }}</p>
            <p>Технолог - {{ stageOne.technologist }}</p>
            <p>Измеритель - {{ stageOne.measurer }}</p>
          </v-card-text>
        </v-card>
      </v-timeline-item>
      <v-timeline-item v-if="choosedIndex > 3">
        <v-card>
          <v-card-title :class="['text-h6', `bg-green-lighten-3`]">
            Пластина готова
          </v-card-title>
          <v-card-text class="text--primary bg-white">
            <div class="text-h6">
              Информация по этапу:
            </div>
            <p>Этап начат - {{ parseData(stageOne.returnedTime) }}</p>
            <p>Технолог - {{ stageOne.technologist }}</p>
            <p>Измеритель - {{ stageOne.measurer }}</p>
          </v-card-text>
        </v-card>
      </v-timeline-item>
    </v-timeline>
  </v-row>
</template>
