<template>
  <v-progress-circular
    v-if="loading"
    indeterminate
    :size="100"
  />
  <div v-if="isStageAvailable">
    <p>Текст</p>
    <div>
      <v-btn
        style="float: left"
        @click="goBack"
      >
        Предыдущий этап
      </v-btn>
      <v-btn style="float: right">
        Следующий этап
      </v-btn>
      <v-sheet
        width="700"
        class="mx-auto"
      >
        <v-autocomplete
          label="Autocomplete"
          :items="[
            'California',
            'Colorado',
            'Florida',
            'Georgia',
            'Texas',
            'Wyoming',
          ]"
        />
        <v-autocomplete
          label="Autocomplete"
          :items="[
            'California',
            'Colorado',
            'Florida',
            'Georgia',
            'Texas',
            'Wyoming',
          ]"
        />
        <v-autocomplete
          label="Autocomplete"
          :items="[
            'California',
            'Colorado',
            'Florida',
            'Georgia',
            'Texas',
            'Wyoming',
          ]"
        />
        <v-text-field label="Label" />
        <v-text-field label="Label" />
        <v-text-field label="Label" />
        <v-text-field label="Label" />
        <v-textarea label="Label" />
      </v-sheet>
    </div>
  </div>

  <div v-else>
    Извините, данный этап пока не доступен
    <v-btn style="float: left">
      Предыдущий этап
    </v-btn>
  </div>
</template>
<script>
import { useRouter, useRoute } from 'vue-router';
import axios from 'axios';
import { ref } from 'vue';
import { useStore } from 'vuex';

export default {
  setup () {
    const loading = ref(true);
    const isStageAvailable = ref(true);
    const router = useRouter();
    const response = ref();
    const store = useStore();
    const getData = async () => {
      try {
        response.value = await axios.get(
          'https://localhost:3000/api/RabbitMq/GetMessage'
        );
      } catch (error) {
        console.error(error);
        store.dispatch('throwMessage', {
          type: 'error',
          name: 'noServerConnection'
        });
      }
      loading.value = false;
    };
    const goBack = () => {
      router.back();
    };
    // getData()
    // setInterval(getData, 2000)
    // throwError()

    return {
      response,
      goBack,
      isStageAvailable
    };
  }
};
</script>
