<template>
  <div class="wrapper">
    <div class="wrapper-content">
      <div class="view">
        <div class="container">
          <router-view></router-view>
          <ErrorComponent />
          <!-- <ErrorComponent  :what="error" />-->
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
  //components: { MyTable, Hello },
  components: { ErrorComponent },
  setup() {
    const componentKey = ref(0)
    const store = useStore()

    let nmberofpushedbtn = ref('12')
    const changed = ref(false)
    //let currentComponent = ref();
    const route = useRoute() //router
    const path = computed(() => route.path)

    const returnDef = () => {
      return {
        component: 'app.vue',
        method: 'returnDef',
        errorCode: '500',
        errorText: 'FINALLY',
      }
    }
    const returnDefTwo = () => {
      return {
        component: '',
        method: '',
        errorCode: '500',
        errorText: '',
      }
    }

    //Вызов ошибки
    const throwError = () => {
      store.dispatch('throwError', returnDef())
    }

    const throwErrorTwo = () => {
      store.dispatch('throwError', returnDefTwo())
    }

    setTimeout(throwError, 10000)
    setTimeout(throwErrorTwo, 14000)
    const onChange = (btnnumber) => {
      changed.value = !changed.value
      // alert(btnnumber.btnnumber)
      nmberofpushedbtn.value = btnnumber //выбранное количество строчек
      console.log(nmberofpushedbtn)
    }
    function getResult() {
      let a = changed.value
      return a
    }
    const error = ref()
    error.value = returnDef()
    return {
      onChange,
      error,
      getResult,
      changed,
      nmberofpushedbtn,
      path,

      componentKey,
    }
  },
}
</script>

<!--<component :is="currentComponent" :key="tab" @change="onChange" />-->
