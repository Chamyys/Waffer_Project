import { createStore } from 'vuex'

const store = createStore({
  state: {
    title: 'Vuex Store',
    arrayofWeather: [],
    firstname: '',
    secondname: '',
  },
  getters: {
    getweatherarray(state) {
      return state.arrayofWeather
    },
  },
  mutations: {
    SAVE_WEATHER_ARRAY(state, title) {
      state.arrayofWeather.push(title)
    },
    CLEAN_WEATHER_ARRAY(state) {
      state.arrayofWeather.length = 0
    },
    CREATE_LOCAL_USER_ARRAY(state, firstname, secondname) {
      state.firstname = firstname
      state.secondname = secondname
    },
  },
  actions: {
    createUser({ commit }, firstname, secondname) {
      commit('CREATE_LOCAL_USER', firstname, secondname)
    },
    createWeatherArray({ commit }, title) {
      commit('SAVE_WEATHER_ARRAY', title)
    },
    cleanWeatherArray({ commit }) {
      commit('CLEAN_WEATHER_ARRAY')
    },
  },
})

export default store
