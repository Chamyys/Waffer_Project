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
    getFirstName(state){
      return state.firstname
    },
    getSecondName(state){
      return state.secondname
    }
  },

  mutations: {
    SAVE_WEATHER_ARRAY(state, title) {
      state.arrayofWeather.push(title)
    },
    CLEAN_WEATHER_ARRAY(state) {
      state.arrayofWeather.length = 0
    },
    CREATE_LOCAL_USER(state, fullname) {
      state.firstname = fullname.split(' ')[0]
      state.secondname = fullname.split(' ')[1]
    },
  },
  actions: {
    createUser({ commit }, fullname) {
      commit('CREATE_LOCAL_USER', fullname)
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
