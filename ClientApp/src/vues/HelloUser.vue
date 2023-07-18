@@ -0,0 +1,60 @@

<script>
import { useRouter, useRoute } from 'vue-router'
import { ref } from 'vue'
import router from '@/router'

// Используем метод push для перехода на новый маршрут

export default {
  /* использование без routinga с применением emits
  emits: ['change'],
  setup(_, { emit }) {
    const change = (data) => {
      emit('change', {
        btnnumber: data.target.innerText, // data.rangeParent.data
      })
      // router.push('/Table')
    }

    return { btnnumber: 0, change }
  },*/

  setup() {
    const router = useRouter()
    const name = ref('')

    const change = (data) => {
      let a = data.target.innerText
      // Переходим на маршрут "/results" с параметром "numberOfPushedBtn"
      router.push({ name: 'Table', params: { btnnumber: a } })
    }

    return {
      name,
      change,
    }
  },
}
</script>

<template>
  <v-icon size="large" color="green-darken-2" icon="mdi-domain"></v-icon>

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

  <router-link :to="'Home'">Информация</router-link>
</template>
