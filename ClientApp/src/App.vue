<template>
  <div class="wrapper">
    <div class="wrapper-content">
      <div class="view">
        <div class="container">

          <Hello v-if="!getResult()" @change="onChange" />

          <MyTable v-if="getResult()" :localprop="nmberofpushedbtn" />
        </div>
      </div>
    </div>
  </div>
</template>

<script>
import { ref, onBeforeMount, onUpdated,computed } from 'vue'
import MyTable from './vues/Table.vue'
import {useRoute} from 'vue-router'
import Hello from './vues/HelloUser.vue'

export default {
  components: { MyTable, Hello },

  setup() {
    let nmberofpushedbtn = ref('12')
    const changed = ref(false)
    //let currentComponent = ref();


    const route=useRoute();                //router
    const path = computed(() =>route.path)



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
      MyTable,
      Hello,
      onChange,
      getResult,
      changed,
      nmberofpushedbtn,
    }
  },
}
</script>

<!--<component :is="currentComponent" :key="tab" @change="onChange" />-->
