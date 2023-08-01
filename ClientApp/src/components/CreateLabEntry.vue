<template>
  <v-progress-circular
    v-if="loading"
    indeterminate
    :size="100"
  ></v-progress-circular>
  <div v-if="isStageAvailable">
    <p>{{ response.data }}</p>
    <div>
      <v-btn style="float: left" @click="goBack">Предыдущий этап</v-btn>
      <v-btn style="float: right">Следующий этап</v-btn>
      <v-sheet width="700" class="mx-auto">
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
        ></v-autocomplete>
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
        ></v-autocomplete>
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
        ></v-autocomplete>
        <v-text-field label="Label"></v-text-field>
        <v-text-field label="Label"></v-text-field>
        <v-text-field label="Label"></v-text-field>
        <v-text-field label="Label"></v-text-field>
        <v-textarea label="Label"></v-textarea>
      </v-sheet>
    </div>
  </div>

  <div v-else>
    Извините, данный этап пока не доступен
    <v-btn style="float: left">Предыдущий этап</v-btn>
  </div>
</template>
<script>
import { useRouter, useRoute } from 'vue-router'
import axios from 'axios'
import { ref } from 'vue'
export default {
  setup() {
    const loading = ref(true)
    const isStageAvailable = ref(true)
    const router = useRouter()
    const response = ref()
    const getData = async () => {
      try {
        response.value = await axios.get(
          'https://localhost:3000/api/RabbitMq/GetMessage'
        )
      } catch (error) {
        console.error(error)
      }
      loading.value = false
    }
    const goBack = () => {
      router.back()
    }
   // getData()
    setInterval(getData, 2000)
    return {
      response,
      goBack,
      isStageAvailable,
    }
  },
}
</script>
