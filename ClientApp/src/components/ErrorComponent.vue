<template>
  <div class="text-center">
    <v-snackbar v-model="snackbar" :color="myColor" :timeout="timeout">
      {{ text }}
      <template #actions>
        <v-btn variant="text" @click="snackbar = false"> Закрыть </v-btn>
      </template>
    </v-snackbar>
  </div>
</template>
<script>
import { useStore } from 'vuex'

import { ref } from 'vue'
export default {
  setup() {
    const text = ref()
    const snackbar = ref(false)
    const myColor = ref('error')
    const store = useStore()
    store.watch(
      (state) => {
        return state.myError
      },
      (newValue, oldValue) => {
        if (newValue !== oldValue && newValue !== null) {
          handleError()
        }
      }
    )

    const handleError = () => {
      let currentError = store.getters.getError
      snackbar.value = true
      text.value = currentError.errorText
      myColor.value = currentError.color
      store.dispatch('forgetError')
    }

    return {
      timeout: 5000,
      snackbar,
      text,
      myColor,
    }
  },
}
</script>
