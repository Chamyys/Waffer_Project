import { createStore } from 'vuex'

const store = createStore({
  state: {
    title: 'Vuex Store',
    arrayofWeather: [],
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
  },
  actions: {
    createWeatherArray({ commit }, title) {
      commit('SAVE_WEATHER_ARRAY', title)
    },
  },
})

export default store
