import { createStore } from 'vuex'
import crc32 from 'crc/crc32'

const store = createStore({
  state: {
    myError: Object,
    myErrorsArray: [],
    maxErrorsArraySize: 7,
    lastErrorId: '',
    title: 'Vuex Store',
    arrayofWeather: [],
    firstname: '',
    secondname: '',
    messages: [
      {
        messageType: 'error',
        messageList: [
          {
            component: 'app.vue',
            method: 'returnDef',
            errorCode: '500',
            errorText: 'Что-то пошло не так',
            name: 'error',
            color: 'error',
            snackbar: true,
          },
          {
            component: '',
            method: '',
            errorCode: '500',
            errorText:
              'Ошибка при подключению к серверу, пожалуйста попробуйте позже',
            name: 'noServerConnection',
            color: 'error',
            snackbar: true,
          },
          {
            component: '',
            method: '',
            errorCode: '401',
            errorText: 'Неверный логин и/или пароль',
            name: 'notCorrectLogin',
            color: 'error',
            snackbar: true,
          },
          {
            component: '',
            method: '',
            errorCode: '401',
            errorText: 'Пожалуйста, закончите заполнение формы',
            name: 'FormNotComplited',
            color: 'error',
            snackbar: true,
          },
          {
            component: 'app.vue',
            method: 'returnDef',
            errorCode: '404',
            errorText: 'Такого пользователя не существует',
            name: 'userDoesNotExist',
            color: 'error',
            snackbar: true,
          },
        ],
      },
      {
        messageType: 'success',
        messageList: [
          {
            component: 'app.vue',
            method: 'returnDef',
            errorCode: '200',
            errorText: 'Новый конфиг успешно сохранен!',
            name: 'configSuccsess',
            color: 'success',
            snackbar: true,
          },
          {
            component: 'app.vue',
            method: 'returnDef',
            errorCode: '200',
            errorText: 'Все прошло успешно',
            name: 'success',
            color: 'success',
            snackbar: true,
          },
          {
            component: 'app.vue',
            method: 'returnDef',
            errorCode: '200',
            errorText: 'Данные успешно загружены на сервер',
            name: 'deliverySuccsess',
            color: 'success',
            snackbar: true,
          },
          {
            component: 'app.vue',
            method: 'returnDef',
            errorCode: '200',
            errorText: 'Пользователь успешно добавлен',
            name: 'userAddedSuccessesfully',
            color: 'success',
            snackbar: true,
          },
          {
            component: 'app.vue',
            method: 'returnDef',
            errorCode: '200',
            errorText: 'Пользователь успешно изменен',
            name: 'userEditedSuccessesfully',
            color: 'success',
            snackbar: true,
          },
          {
            component: 'app.vue',
            method: 'returnDef',
            errorCode: '200',
            errorText: 'Пользователь успешно удален',
            name: 'userDeletedSuccessesfully',
            color: 'success',
            snackbar: true,
          },
        ],
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
    getErrorsArray(state) {
      return state.myErrorsArray
    },
    getFirstName(state) {
      return state.firstname
    },
    getSecondName(state) {
      return state.secondname
    },
    getMyMessageCollection(state, type) {
      return state.type
    },
  },

  mutations: {
    /*
    THROW_NEW_ERROR(state, title) {
      if (state.myErrorsArray.length < state.maxErrorsArraySize) {
        let id = crc32(new Date().toString()).toString(16) + Math.random(10)
        let errorConfig = state.errors.find((obj) => obj.name === title)
        let currentError = { ...errorConfig, id }
        state.lastErrorId = currentError.id
        state.myError = currentError
        state.myErrorsArray.push(currentError)
      }
    },*/
    FORGET_CURRENT_ERROR(state) {
      state.lastErrorId = null
    },
    SAVE_WEATHER_ARRAY(state, title) {
      state.arrayofWeather.push(title)
    },
    CLEAN_WEATHER_ARRAY(state) {
      state.arrayofWeather.length = 0
    },
    THROW_NEW_MESSAGE(state, { type, name }) {
      if (state.myErrorsArray.length < state.maxErrorsArraySize) {
        let id = crc32(new Date().toString()).toString(16) + Math.random(10)
        let errorConfig = state.messages
          .find((obj) => obj.messageType === type)
          .messageList.find((obj) => obj.name === name)
        let currentError = { ...errorConfig, id }
        state.lastErrorId = currentError.id
        state.myError = currentError
        state.myErrorsArray.push(currentError)
      }
    },

    DELETE_ERROR_IN_ARRAY_BY_ID(state, id) {
      state.myErrorsArray = state.myErrorsArray.filter(
        (value) => value.id !== id
      )
    },
    DELETE_LAST_ERROR_IN_ARRAY(state) {
      if (state.myErrorsArray.length > 0) {
        state.myErrorsArray = state.myErrorsArray.slice(
          0,
          state.myErrorsArray.length - 1
        )
      }
    },
    CREATE_LOCAL_USER(state, { firstname, secondname }) {
      state.firstname = firstname
      state.secondname = secondname
    },
  },
  actions: {
    /* throwError({ commit }, myError) {
      commit('THROW_NEW_ERROR', myError)
    },*/
    throwMessage({ commit }, { type, name }) {
      commit('THROW_NEW_MESSAGE', { type, name })
    },
    deleteErrorInArrayById({ commit }, id) {
      commit('DELETE_ERROR_IN_ARRAY_BY_ID', id)
    },
    deleteLastErrorInArray({ commit }) {
      commit('DELETE_LAST_ERROR_IN_ARRAY')
    },
    forgetError({ commit }) {
      commit('FORGET_CURRENT_ERROR')
    },
    createUser({ commit }, { firstname, secondname }) {
      commit('CREATE_LOCAL_USER', { firstname, secondname })
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
