import { createStore } from 'vuex' //общее хранилище

const debug = import.meta.env.DEV

export default createStore({
  modules: {},
  strict: debug,
})
