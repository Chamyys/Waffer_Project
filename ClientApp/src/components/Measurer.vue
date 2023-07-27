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
            title="Прием пластины"
            value="home"
            @click="createWafel"
          ></v-list-item>
          <v-list-item
            prepend-icon="mdi-download"
            title="Создание монитора"
            value="account"
            @click="createMonitor"
          ></v-list-item>
          <v-list-item
            prepend-icon="mdi-help"
            title="Текущие задачи"
            value="users"
            @click="missions"
          ></v-list-item>
          <v-list-item
            prepend-icon="mdi-download"
            title="Поиск пластины"
            value="account"
            @click="searchWafer"
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
import { useRouter } from 'vue-router'

export default {
  setup() {
    const rail = ref(true)
    const drawer = ref(true)
    const router = useRouter()
    const createWafel = () => {
      router.push('/Measurer/WelcomeBack')
    }
    const createMonitor = () => {
      router.push('/Measurer/CreateMonitor')
    }

    const missions = () => {
      router.push('/Measurer/Missions')
    }
    const searchWafer = () => {
      router.push('/Measurer/WaferTimeLine')
    }
    const username = (
      window.localStorage.getItem('firstName') +
      ' ' +
      window.localStorage.getItem('lastName')
    ).toString()

    router.push('/Measurer/WelcomeBack')

    return {
      drawer,
      rail,
      username,
      missions,
      createWafel,
      createMonitor,
      searchWafer,
    }
  },
}
</script>
