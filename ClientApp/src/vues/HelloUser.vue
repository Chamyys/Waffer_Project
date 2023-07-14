@@ -0,0 +1,60 @@
<template>
  <h1>Добрый День!</h1>

  <v-col cols="12" class="py-2">
    <p>Выберете количество строчек:</p>

    <v-btn-toggle v-model="text" rounded="0" color="deep-purple-accent-3" group>
      <v-btn v-model="btnnumber" name="1" value="left" @click="change($event)">
        1
      </v-btn>

      <v-btn
        v-model="btnnumber"
        name="2"
        value="center"
        @click="change($event)"
      >
        2
      </v-btn>

      <v-btn v-model="btnnumber" name="3" value="right" @click="change($event)">
        3
      </v-btn>

      <v-btn
        v-model="btnnumber"
        name="4"
        value="justify"
        @click="change($event)"
      >
        4
      </v-btn>

      <v-btn
        v-model="btnnumber"
        name="5"
        value="justify"
        @click="change($event)"
      >
        5
      </v-btn>
    </v-btn-toggle>
  </v-col>

  <v-btn icon="$vuetify" variant="plain"> Вернуться назад </v-btn>
</template>
<script>
export default {
  emits: ['change'],
  setup(_, { emit }) {
    const change = (data) => {
      emit('change', {
        btnnumber: data.rangeParent.data,
      })
    }

    return { btnnumber: 0, change }
  },
}
</script>
