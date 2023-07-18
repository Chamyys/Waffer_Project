<template>
  <div class="wrapper">
    <div class="wrapper-content">
      <div class="view">
        <div class="container">
          <router-view></router-view>
        </div>
      </div>
    </div>
  </div>
</template>

<script>
import { ref, onBeforeMount, onUpdated, computed } from 'vue'
import { useRoute } from 'vue-router'
export default {
  //components: { MyTable, Hello },

  setup() {
    let nmberofpushedbtn = ref('12')
    const changed = ref(false)
    //let currentComponent = ref();
    const route = useRoute() //router
    const path = computed(() => route.path)

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
    return {
      onChange,
      getResult,
      changed,
      nmberofpushedbtn,
      path,
    }
  },
}
</script>

<!--<component :is="currentComponent" :key="tab" @change="onChange" />-->
