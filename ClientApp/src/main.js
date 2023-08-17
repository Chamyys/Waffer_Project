import { createApp } from 'vue'
import App from './App.vue'
import axios from 'axios'

import store from './store'
import router from './router'
import VueAxios from 'vue-axios'
import { Vue3Mq } from 'vue3-mq'
import { VDatePicker } from 'vuetify/labs/VDatePicker'
import { VDataTable } from 'vuetify/labs/VDataTable'
//import { createStore } from 'vuex'
// import styling
import * as signalR from '@microsoft/signalr'

import style from './assets/scss/style.scss'
//import styletwo from './assets/css/style.css'
import '@mdi/font/css/materialdesignicons.css' //иконки
import 'vuetify/styles'
import { createVuetify } from 'vuetify'
import * as components from 'vuetify/components'
import * as directives from 'vuetify/directives'
import { aliases, mdi } from 'vuetify/iconsets/mdi'

////import { createStore } from './store/index'

// Create a new store instance.
/*
const vuex = createStore({
  state() {
    return {
      count: 0,
    }
  },
  mutations: {
    increment(state) {
      state.count++
    },
  },
})
*/
const vuetify = createVuetify({
  components,
  directives,
  VDataTable,
  VDatePicker,
  style,
  ssr: true
})
const app = createApp(App)
app.use(store)
app.config.globalProperties.$signalR = signalR

app.use(router)
app.use(vuetify)
app.use(Vue3Mq)
app.use(VueAxios, axios)
//app.use(vuex)
app.provide('axios', app.config.globalProperties.axios)
app.mount('#app')
