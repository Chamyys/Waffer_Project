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
  <v-alert
    color="indigo-darken-1"
    theme="dark"
    icon="mdi-pencil"
    prominent
  >
    Добро пожаловать, {{ username }}!
  </v-alert>
  <v-tabs
    v-model="selected"
    slider-color="rgb(30, 7, 135)"
    active-class="choosed"
    fixed-tabs
    bg-color="indigo-lighten-1"
  >
    <v-tab
      :id="0"
      :class="checkActivityClass(0)"
      @click="createWaffer"
    >
      Прием пластины
    </v-tab>
    <v-tab
      :id="1"
      :class="checkActivityClass(1)"
      @click="createMT"
    >
      Создать Измерение
    </v-tab>
    <v-tab
      :id="2"
      :class="checkActivityClass(2)"
      @click="seeLivePath"
    >
      Просмотреть статус пластины
    </v-tab>
    <v-tab
      :id="3"
      :class="checkActivityClass(3)"
      @click="checkMissions"
    >
      Таблица задач
    </v-tab>
  </v-tabs>
  <router-view />
</template>

<script>
import { ref } from 'vue';
import { useRouter } from 'vue-router';

export default {
  setup () {
    const rail = ref(true);
    const drawer = ref(true);
    const router = useRouter();
    const selected = ref(0);
    const username = (
      window.localStorage.getItem('firstName') +
      ' ' +
      window.localStorage.getItem('lastName')
    ).toString();
    const createMT = () => {
      router.push('/Technologist/AddNewMT');
    };
    const createWaffer = () => {
      router.push('/Technologist/WelcomeBack');
    };
    const checkMissions = () => {
      router.push('/Technologist/Missions');
    };
    const seeLivePath = () => {
      router.push('/Technologist/WaferTimeLine');
    };
    const checkActivityClass = (id) => {
      return id === selected.value ? 'choosed' : '';
    };

    router.push('/Technologist/WelcomeBack');
    return {
      drawer,
      seeLivePath,
      rail,
      username,
      createMT,
      createWaffer,
      checkActivityClass,
      checkMissions,
      selected
    };
  }
};
</script>

<style>
.choosed {
  background-color: rgb(30, 7, 135);
  color: rgb(30, 7, 135);
}
</style>
