<template>
  <!--
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
            title="Создать измерение"
            value="home"
            @click="createMT"
          ></v-list-item>
          <v-list-item
            prepend-icon="mdi-download"
            title="Прием пластины"
            value="account"
            @click="createWaffer"
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
        <v-col cols="12">
          <v-btn-toggle v-model="text" rounded="0" group> </v-btn-toggle>
        </v-col>
      </v-main>
    </v-layout>
  </v-card>
  -->
  <v-alert color="green" theme="dark" icon="mdi-pencil" prominent>
    Добро пожаловать, {{ username }}!
  </v-alert>
  <v-tabs fixed-tabs bg-color="green">
    <v-tab @click="createMT"> Создать Измерение </v-tab>
    <v-tab @click="createWaffer"> Прием пластины </v-tab>
  </v-tabs>
  <router-view></router-view>
</template>

<script>
import { ref } from 'vue'
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
    const createMT = () => {
      router.push('/Technologist/AddNewMT')
    }
    const createWaffer = () => {
      router.push('/Technologist/WelcomeBack')
    }
    router.push('/Technologist/WelcomeBack')
    return {
      drawer,
      rail,
      username,
      createMT,
      createWaffer,
    }
  },
}
</script>
