<script>
import { ref } from 'vue'
import { useRouter } from 'vue-router'
import axios from 'axios'
export default {
  setup() {
    const loading = ref(false)

    const router = useRouter()
    const dialog = ref(true)
    const stages = ref([''])
    const goBack = () => {
      router.push(router.options.history.state.back)
    }

    const getCurentWafferInfo = async () => {
      try {
        const promise = await axios.get(
          'https://localhost:3000/api/WaferInLab/Get'
        )

        console.log(promise)
        const dataPromise = await promise
        stages.value = [...dataPromise.data]
        loading.value = true
      } catch (error) {
        console.error(error)
      }
    }
    getCurentWafferInfo()
    return {
      dialog,
      loading,
      goBack,
      getCurentWafferInfo,
      stages,
    }
  },
}
</script>
<template>
  <v-row justify="center">
    <v-dialog v-model="dialog" persistent width="auto">
      <template #activator="{ props }"> </template>
      <v-card>
        <v-card-title class="text-h8">
          Введите номер пластины для отслеживания истории ее состояний
        </v-card-title>

        <v-text-field label="Номер пластины"></v-text-field>
        <v-card-actions>
          <v-spacer></v-spacer>
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
            @click=";(dialog = false), getCurentWafferInfo()"
          >
            Готово
          </v-btn>
        </v-card-actions>
      </v-card>
    </v-dialog>
  </v-row>

    <v-timeline v-if="loading">
      <v-timeline-item>
        <template #opposite>
          Пластина создана - {{ stages[0].stages[0].returnedTime }}
        </template>
        <div>
          <div class="text-h6">Content title</div>
          <p>
            Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do
            eiusmod tempor incididunt ut labore et dolore magna aliqua.
          </p>
        </div>
      </v-timeline-item>

      <v-timeline-item>
        <template #opposite> Opposite content </template>
        <div>
          <div class="text-h6">Content title</div>
          <p>
            Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do
            eiusmod tempor incididunt ut labore et dolore magna aliqua.
          </p>
        </div>
      </v-timeline-item>

      <v-timeline-item>
        <template #opposite> Opposite content </template>
        <div>
          <div class="text-h6">Content title</div>
          <p>
            Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do
            eiusmod tempor incididunt ut labore et dolore magna aliqua.
          </p>
        </div>
      </v-timeline-item>
      <v-timeline-item>
        <template #opposite> Opposite content </template>
        <div>
          <div class="text-h6">Content title</div>
          <p>
            Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do
            eiusmod tempor incididunt ut labore et dolore magna aliqua.
          </p>
        </div>
      </v-timeline-item>
      <v-timeline-item>
        <template #opposite> Opposite content </template>
        <div>
          <div class="text-h6">Content title</div>
          <p>
            Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do
            eiusmod tempor incididunt ut labore et dolore magna aliqua.
          </p>
        </div>
      </v-timeline-item>
    </v-timeline>
</template>
