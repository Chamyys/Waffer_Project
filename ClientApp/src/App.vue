<template>
  <div class="wrapper">
    <div class="wrapper-content">
      <div class="view">
        <div class="container">
          <router-view />
          <ErrorComponent />
          <SeeChatComponent />
        </div>
      </div>
    </div>
  </div>
</template>
<script>
import { useStore } from 'vuex';
import ErrorComponent from '@/components/ErrorComponent.vue';
import { ref, computed } from 'vue';
import { useRoute } from 'vue-router';
import SeeChatComponent from '@/components/SeeChatComponent.vue';

export default {
  components: { ErrorComponent, SeeChatComponent },
  setup () {
    const componentKey = ref(0);
    const store = useStore();
    const changed = ref(false);
    const route = useRoute();
    const path = computed(() => route.path);
    const handleErrorType = () => {};

    return {
      changed,
      path,
      handleErrorType,
      componentKey
    };
  }
};
</script>
<!---->
<!--<component :is="currentComponent" :key="tab" @change="onChange" />-->
