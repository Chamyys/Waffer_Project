import { createApp } from 'vue'
import App from './App.vue'
import axios from 'axios'
import store from './store'
import router from './router'
import VueAxios from 'vue-axios'
import { Vue3Mq } from 'vue3-mq'
// import styling
import './assets/scss/style.scss'
import './assets/css/style.css'
import 'vuetify/styles'
import { createVuetify } from 'vuetify'
import * as components from 'vuetify/components'
import * as directives from 'vuetify/directives'

const vuetify = createVuetify({
  components,
  directives,
})
const app = createApp(App)
app.use(store)
app.use(router)
app.use(vuetify)
app.use(Vue3Mq)
app.use(VueAxios, axios)
app.provide('axios', app.config.globalProperties.axios)

app.mount('#app')
