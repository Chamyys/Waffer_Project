<template>
  <div class="wrapper">
    <div class="wrapper-content">
      <div class="view">
        <div class="container">
          <ErrorComponent />
          <router-view></router-view>
        </div>
      </div>
    </div>
  </div>
</template>
<script>
import { useStore } from 'vuex'
import ErrorComponent from '@/components/ErrorComponent.vue'
import { ref, computed } from 'vue'
import { useRoute } from 'vue-router'
export default {
  components: { ErrorComponent },
  setup() {
    const componentKey = ref(0)
    const store = useStore()
    const changed = ref(false)
    const route = useRoute()
    const path = computed(() => route.path)
    const handleErrorType = () => {
      //handlesomething
    }
    //Вызов ошибки
    //const throwError = () => {
    //эта хрень в конфиге ()нормальные имена
    // store.dispatch('throwError', 'error')
    //}
    //setTimeout(throwError, 5000)
    return {
      changed,
      path,
      handleErrorType,
      componentKey,
    }
  },
}
</script>
<!---->
<!--<component :is="currentComponent" :key="tab" @change="onChange" />-->
