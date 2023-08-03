import { createStore } from 'vuex'
import crc32 from 'crc/crc32'

const store = createStore({
  state: {
    myError: Object,
    title: 'Vuex Store',
    arrayofWeather: [],
    firstname: '',
    secondname: '',
    errors: [
      {
        id: '',
        component: 'app.vue',
        method: 'returnDef',
        errorCode: '500',
        errorText: 'Что-то пошло не так',
        name: 'error',
        color: 'error',
      },
      {
        id: '',
        component: 'app.vue',
        method: 'returnDef',
        errorCode: '200',
        errorText: 'Все прошло успешно',
        name: 'success',
        color: 'success',
      },
      {
        id: '',
        component: '',
        method: '',
        errorCode: '500',
        errorText:
          'Ошибка при подключению к серверу, пожалуйста попробуйте позже',
        name: 'noServerConnection',
        color: 'error',
      },
      {
        id: '',
        component: '',
        method: '',
        errorCode: '401',
        errorText: 'Неверный логин и/или пароль',
        name: 'notCorrectLogin',
        color: 'error',
      },
      {
        id: '',
        component: '',
        method: '',
        errorCode: '401',
        errorText: 'Пожалуйста, закончите заполнение формы',
        name: 'FormNotComplited',
        color: 'error',
      },
      {
        id: '',
        component: 'app.vue',
        method: 'returnDef',
        errorCode: '200',
        errorText: 'Данные успешно загружены на сервер',
        name: 'deliverySuccsess',
        color: 'success',
      },
      {
        id: '',
        component: 'app.vue',
        method: 'returnDef',
        errorCode: '200',
        errorText: 'Пользователь успешно добавлен',
        name: 'userAddedSuccessesfully',
        color: 'success',
      },
      {
        id: '',
        component: 'app.vue',
        method: 'returnDef',
        errorCode: '200',
        errorText: 'Пользователь успешно изменен',
        name: 'userEditedSuccessesfully',
        color: 'success',
      },
      {
        id: '',
        component: 'app.vue',
        method: 'returnDef',
        errorCode: '404',
        errorText: 'Такого пользователя не существует',
        name: 'userDoesNotExist',
        color: 'error',
      },
      {
        id: '',
        component: 'app.vue',
        method: 'returnDef',
        errorCode: '200',
        errorText: 'Пользователь успешно удален',
        name: 'userDeletedSuccessesfully',
        color: 'success',
      },
    ],
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
      let error = state.errors.find((obj) => obj.name === title)
      error.id = crc32(new Date().toString()).toString(16)

      state.myError = error
    },
    FORGET_CURRENT_ERROR(state) {
      state.myError = null
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
