<template>
  <v-snackbar
    v-for="(item, index) in errorArray"
    :key="item"
    v-model="item.snackbar"
    :color="item.color"
    :timeout="100000"
    :style="getSnackbarStyle(index)"
    location="top right"
    width="30em"
  >
    {{ item.errorText }}
    {{ item.id }}
    <template #actions>
      <v-btn variant="text" @click="deleteOnewSnackBar(item.id)">
        Закрыть
      </v-btn>
    </template>
  </v-snackbar>
  <div style="height: 2em"></div>
</template>
<script>
import { useStore } from 'vuex'
import crc32 from 'crc/crc32'

import { ref } from 'vue'
export default {
  setup() {
    const errorArray = ref([])
    const text = ref()
    const snackbar = ref(false)
    const myColor = ref('error')
    const store = useStore()
    const lastIndex = ref(0)
    store.watch(
      (state) => state.lastErrorId,
      (newValue, oldValue) => {
        if (newValue !== oldValue) {
          handleError()
        }
      }
    )
    const deleteOnewSnackBar = (id) => {
      store.dispatch('deleteErrorInArrayById', id)
      errorArray.value = errorArray.value.filter((value) => value.id !== id)
      lastIndex.value--
    }
    const handleError = () => {
      errorArray.value = store.getters.getErrorsArray
      lastIndex.value++
    }
    const deleteLastError = () => {
      if (errorArray.value.length > 0) {
        store.dispatch('deleteLastErrorInArray')
        errorArray.value = errorArray.value.slice(
          0,
          errorArray.value.length - 1
        )
        lastIndex.value--
      }
    }
    const getSnackbarStyle = (index) => {
      const topOffset = index * 60
      return {
        position: 'absolute',
        top: `${topOffset}px`,
      }
    }
    setInterval(deleteLastError, 5000)

    return {
      getSnackbarStyle,
      snackbar,
      text,
      lastIndex,
      myColor,
      errorArray,
      deleteOnewSnackBar,
    }
  },
}
</script>
