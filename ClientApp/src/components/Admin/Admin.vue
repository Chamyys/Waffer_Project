<template>
  <v-card>
    <v-layout>
      <v-navigation-drawer
        v-model="drawer"
        :rail="rail"
        permanent
        @click="rail = false"
      >
        <v-list-item
          prepend-avatar="https://katalog-rek.ru/upload/iblock/dbc/SVR-LOGO.png"
          title=""
          nav
        >
          <template #append>
            {{ username }}
            <v-btn
              variant="text"
              icon="mdi-chevron-left"
              @click.stop="rail = !rail"
            ></v-btn>
          </template>
        </v-list-item>

        <v-divider></v-divider>

        <v-list density="compact" nav>
          <v-list-item
            prepend-icon="mdi-magnify"
            title="Поиск пластины"
            value="home"
          ></v-list-item>
          <v-list-item
            prepend-icon="mdi-download"
            title="Загрузка измерений"
            value="account"
          ></v-list-item>
          <v-list-item
            prepend-icon="mdi-help"
            title="Этот пункт в разработке"
            value="users"
          ></v-list-item>
        </v-list>
      </v-navigation-drawer>
      <v-main style="height: 80em">
        <router-view></router-view>
      </v-main>
    </v-layout>
  </v-card>
</template>

<script>
import { ref } from 'vue'
import axios from 'axios'
import { useRouter } from 'vue-router'

export default {
  setup() {
    const rail = ref(true)

    const drawer = ref(true)

    const router = useRouter()

    const username = (
      window.localStorage.getItem('firstName') +
      ' ' +
      window.localStorage.getItem('lastName')
    ).toString()
    const goBack = () => {
      router.push('/')
    }
    router.push('/Admin/AdminChooseConfig')
    return {
      drawer,
      rail,
      username,
      goBack,
    }
  },
}
</script>
