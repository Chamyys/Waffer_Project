import { createStore } from 'vuex'

const store = createStore({
  state: {
    myError: {
      component: '',
      method: '',
      errorCode: '',
      errorText: '',
    },
    title: 'Vuex Store',
    arrayofWeather: [],
    firstname: '',
    secondname: '',
  },
  getters: {
    getError(state) {
      return state.myError
    },
    getweatherarray(state) {
      return state.arrayofWeather
    },
    getFirstName(state) {
      return state.firstname
    },
    getSecondName(state) {
      return state.secondname
    },
  },

  mutations: {
    THROW_NEW_ERROR(state, title) {
      state.myError.component = title.component
      state.myError.method = title.method
      state.myError.errorCode = title.errorCode
      state.myError.errorText = title.errorText
    },
    FORGET_CURRENT_ERROR(state) {
      state.myError = ''
    },
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
    throwError({ commit }, myError) {
      commit('THROW_NEW_ERROR', myError)
    },
    forgetError({ commit }) {
      commit('FORGET_CURRENT_ERROR')
    },
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
