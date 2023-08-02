<template>
  <div class="text-center">
    <v-snackbar v-model="snackbar" :timeout="timeout">
      {{ text }}
      <template #actions>
        <v-btn color="blue" variant="text" @click="snackbar = false">
          Close
        </v-btn>
      </template>
    </v-snackbar>
  </div>
</template>
<script>
import { useStore } from 'vuex'

import { ref, toRefs, watch } from 'vue'
export default {

  emits: ['handleError'],
  setup(  emit ) {
    const store = useStore()
    store.watch(
      (state) => {
        // Check for the specific data in the store that you want to trigger the function on
        return state.myError // Replace 'data' with the property name in your store
      },
      (newValue, oldValue) => {
        // Call your function here
        if (newValue !== oldValue) {
          // Execute your function logic here
          nyfunc()
        }
      }
    )
    const text = ref()
    const snackbar = ref(false)
    // Создаем реактивную переменную для отслеживания пропсов

    // const myPropValue = ref(props.what)
    const nyfunc = () => {
      let currentError = store.getters.getError
      snackbar.value = true
      text.value = currentError.errorText
      store.dispatch('forgetError')
      emit('handleError', {
        //доделать эмит
        component: currentError.component,
        method: currentError.method,
        errorCode: currentError.errorCode,
        errorText: currentError.errorText,
      })
    }
    // Используем хук watch для вызова функции при изменении пропсов

    /*
    watch(
      () => {
        return props.what
      },
      (newVal) => {
        // Выполняем нужную функцию при изменении пропсов
        myFunction(newVal)
        // Обновляем значение реактивной переменной
        myPropValue.value = newVal
      }
    )*/

    // Функция, которая будет вызываться при изменении пропсов
    function myFunction(value) {
      snackbar.value = true
      text.value = value.errorMessage
      store.dispatch('forgetError')
    }

    return {
      timeout: 10000,
      snackbar,
      text,
    }
  },
}
</script>
