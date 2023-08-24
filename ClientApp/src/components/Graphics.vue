<style>
.v-selection-control {
  color: white;
}
</style>
<script>
import { ref, watch } from 'vue';
import axios from 'axios';
import crc32 from 'crc/crc32';
import { useStore } from 'vuex';
import { toRefs } from 'vue';
import { reactive } from 'vue';

export default {
  props: {
    choosedConfigElements: Object
  },

  setup (props) {
    const localComponents = ref(props.choosedConfigElements);
    const firstGraphicsCollection = ref([
      { name: 'Первый_1', id: '121lc' },
      { name: 'Первый_2', id: '2321d' },
      { name: 'Первый_3', id: '3casd' },
      { name: 'Первый_4', id: '3c2s1' }
    ]);
    const secondGraphicsCollection = ref([
      { name: 'Второй_1', id: '1231lc' },
      { name: 'Второй_2', id: '232dda1d' },
      { name: 'Второй_3', id: '31ssasd' },
      { name: 'Второй_4', id: '1631lsc' },
      { name: 'Второй_5', id: '237aasa5d' },
      { name: 'Второй_6', id: '3cs1saa12d' },
      { name: 'Второй_11', id: '12314lc' },
      { name: 'Второй_21', id: '232d5da1d' },
      { name: 'Второй_31', id: '31s7sasd' },
      { name: 'Второй_41', id: '16381lsc' },
      { name: 'Второй_51', id: '2397aavsa5d' },
      { name: 'Второй_61', id: '3c99ssaa12d' },
      { name: 'Второй_12', id: '1wv231776lc' },
      { name: 'Второй_22', id: '232dda451vd' },
      { name: 'Второй_32', id: '3121ssavs1d' },
      { name: 'Второй_42', id: '163v31qwelsc' },
      { name: 'Второй_52', id: '237awasaqwe5d' },
      { name: 'Второй_41', id: '16381qwlsc' },
      { name: 'Второй_51', id: '239eq7aasa5d' },
      { name: 'Второй_61', id: '3c99ssqwaa12d' }
    ]);

    let currentElementIndex = 0;
    const checkboxes = ref([]);
    const store = useStore();
    const graphicsCollection = ref([]);
    graphicsCollection.value = secondGraphicsCollection.value;
    const currentElement = ref();
    currentElement.value = localComponents.value[currentElementIndex];
    const saveGrapForCurElem = () => {
      store.dispatch('saveGraph', {
        element: currentElement.value,
        graphs: checkboxes.value
      });
    };

    const loadTempGraphConfig = () => {
      store.dispatch('getGraphsByElementId', currentElement.value.id);
      checkboxes.value = store.getters.getMyGraph;
    };
    const nextElement = () => {
      saveGrapForCurElem();
      cleanChekboxes();
      if (localComponents.value.length - 1 > currentElementIndex) {
        currentElementIndex++;
        currentElement.value = localComponents.value[currentElementIndex];
      }
      loadTempGraphConfig();
    };
    const prevElement = () => {
      saveGrapForCurElem();
      cleanChekboxes();
      if (currentElementIndex > 0) {
        currentElementIndex--;
        currentElement.value = localComponents.value[currentElementIndex];
      }
      loadTempGraphConfig();
    };
    const cleanChekboxes = () => {
      checkboxes.value = [];
    };
    const chunksMaker = () => {
      const chunkSize = 6;
      const chunks = [];

      for (let i = 0; i < graphicsCollection.value.length; i += chunkSize) {
        chunks.push(graphicsCollection.value.slice(i, i + chunkSize));
      }
      return chunks;
    };
    const chunks = ref([]);
    chunks.value = chunksMaker();

    watch(
      () => props.choosedConfigElements,
      (newValue) => {
        cleanChekboxes();
        localComponents.value = newValue;
        currentElement.value = localComponents.value[0];
      }
    );
    watch(
      () => checkboxes.value,
      (newValue) => {
        saveGrapForCurElem();
      }
    );

    return {
      graphicsCollection,
      chunks,
      nextElement,
      prevElement,
      checkboxes,
      saveGrapForCurElem,
      currentElement
    };
  }
};
</script>

<template>
  <v-card style="height: 4em; background-color: rgb(229, 239, 241)">
    <div
      class="text-h4"
      style="padding-top: 0.25em"
    >
      {{ currentElement.name }}
    </div>
    <v-row style="height: max-content" />
  </v-card>

  <v-container>
    <div class="d-flex white mb-6 flex-col">
      <div
        v-for="(chunk, index) in chunks"
        :key="index"
        style="width: 100%"
      >
        <div
          v-for="(item, key) in chunk"
          :key="item.id"
          style="width: 20em; padding: 4px; float: left"
        >
          <v-card style="height: 4em; background-color: rgb(229, 239, 241)">
            <v-row style="height: max-content">
              <v-checkbox
                v-model="checkboxes"
                :value="item"
                :label="item.name"
                style="padding: 1em; color: white"
              />
            </v-row>
          </v-card>
        </div>
      </div>
    </div>
  </v-container>
  <v-btn
    style="float: left"
    width="15em"
    @click="prevElement()"
  >
    Назад
  </v-btn>
  <v-btn
    style="float: right"
    width="15em"
    @click="nextElement()"
  >
    Следующий элемент
  </v-btn>
</template>
