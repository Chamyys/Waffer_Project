import { createStore } from 'vuex';
import crc32 from 'crc/crc32';

const store = createStore({
  state: {
    myError: Object,
    myErrorsArray: [],
    maxErrorsArraySize: 7,
    lastErrorId: '',
    title: 'Vuex Store',
    arrayofWeather: [],
    //graphArray: [],
    elementGraphsArray: [],
    myTempGraphArray: [],
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
            errorText: 'Пожалуйста, выберете измеряемые элементы',
            name: 'monitorElementsEmpty',
            color: 'error',
            snackbar: true
          },
          {
            component: 'app.vue',
            method: 'returnDef',
            errorCode: '500',
            errorText: 'Ошибка загрузки с сервера',
            name: 'serverLoadingError',
            color: 'error',
            snackbar: true
          },
          {
            component: 'app.vue',
            method: 'returnDef',
            errorCode: '500',
            errorText: 'Ошибка загрузки на сервер',
            name: 'serverPostError',
            color: 'error',
            snackbar: true
          },
          {
            component: 'app.vue',
            method: 'returnDef',
            errorCode: '500',
            errorText: 'Конфиг с такими измерениями уже существует!',
            name: 'configAlreadyExists',
            color: 'error',
            snackbar: true
          },
          {
            component: 'app.vue',
            method: 'returnDef',
            errorCode: '500',
            errorText: 'Не выбран конфиг для удаления',
            name: 'NoConfigToDeleteChosen',
            color: 'error',
            snackbar: true
          },
          {
            component: 'app.vue',
            method: 'returnDef',
            errorCode: '500',
            errorText: 'Такое имя уже занято',
            name: 'nameIsBuissy',
            color: 'error',
            snackbar: true
          },
          {
            component: 'app.vue',
            method: 'returnDef',
            errorCode: '500',
            errorText: 'Пожалуйста, дайте имя вашему конфигу',
            name: 'configNameEmpty',
            color: 'error',
            snackbar: true
          },
          {
            component: 'app.vue',
            method: 'returnDef',
            errorCode: '500',
            errorText: 'Список конфигов пуст!',
            name: 'NoConfigsToDelete',
            color: 'error',
            snackbar: true
          },
          {
            component: 'app.vue',
            method: 'returnDef',
            errorCode: '500',
            errorText: 'Что-то пошло не так',
            name: 'error',
            color: 'error',
            snackbar: true
          },
          {
            component: '',
            method: '',
            errorCode: '500',
            errorText:
              'Ошибка при подключению к серверу, пожалуйста попробуйте позже',
            name: 'noServerConnection',
            color: 'error',
            snackbar: true
          },
          {
            component: '',
            method: '',
            errorCode: '401',
            errorText: 'Неверный логин и/или пароль',
            name: 'notCorrectLogin',
            color: 'error',
            snackbar: true
          },
          {
            component: '',
            method: '',
            errorCode: '401',
            errorText: 'Пожалуйста, закончите заполнение формы',
            name: 'FormNotComplited',
            color: 'error',
            snackbar: true
          },
          {
            component: 'app.vue',
            method: 'returnDef',
            errorCode: '404',
            errorText: 'Такого пользователя не существует',
            name: 'userDoesNotExist',
            color: 'error',
            snackbar: true
          }
        ]
      },

      {
        messageType: 'warning',
        messageList: [
          {
            component: 'app.vue',
            method: 'returnDef',
            errorCode: '500',
            errorText:
              'Внимание! Выбранный конфиг не подходит для текущего монитора, измеряемые величины будут изменены!',
            name: 'configDoesntMatch',
            color: 'warning',
            snackbar: true
          }
        ]
      },

      {
        messageType: 'success',
        messageList: [
          {
            component: 'app.vue',
            method: 'returnDef',
            errorCode: '200',
            errorText: 'Новый конфиг успешно удален!',
            name: 'configSuccsessRemoved',
            color: 'success',
            snackbar: true
          },
          {
            component: 'app.vue',
            method: 'returnDef',
            errorCode: '200',
            errorText: 'Новый конфиг успешно сохранен!',
            name: 'configSuccsess',
            color: 'success',
            snackbar: true
          },
          {
            component: 'app.vue',
            method: 'returnDef',
            errorCode: '200',
            errorText: 'Все прошло успешно',
            name: 'success',
            color: 'success',
            snackbar: true
          },
          {
            component: 'app.vue',
            method: 'returnDef',
            errorCode: '200',
            errorText: 'Данные успешно загружены на сервер',
            name: 'deliverySuccsess',
            color: 'success',
            snackbar: true
          },
          {
            component: 'app.vue',
            method: 'returnDef',
            errorCode: '200',
            errorText: 'Пользователь успешно добавлен',
            name: 'userAddedSuccessesfully',
            color: 'success',
            snackbar: true
          },
          {
            component: 'app.vue',
            method: 'returnDef',
            errorCode: '200',
            errorText: 'Пользователь успешно изменен',
            name: 'userEditedSuccessesfully',
            color: 'success',
            snackbar: true
          },
          {
            component: 'app.vue',
            method: 'returnDef',
            errorCode: '200',
            errorText: 'Пользователь успешно удален',
            name: 'userDeletedSuccessesfully',
            color: 'success',
            snackbar: true
          }
        ]
      }
    ]
  },
  getters: {
    getError (state) {
      return state.myError;
    },
    getweatherarray (state) {
      return state.arrayofWeather;
    },
    getGraphArray (state) {
      return state.graphArray;
    },
    getErrorsArray (state) {
      return state.myErrorsArray;
    },
    getFirstName (state) {
      return state.firstname;
    },
    getElementGraphsArray (state) {
      return state.elementGraphsArray;
    },
    getSecondName (state) {
      return state.secondname;
    },
    getMyMessageCollection (state, type) {
      return state.type;
    },
    getMyGraph (state) {
      return state.myTempGraphArray;
    }
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
    FORGET_CURRENT_ERROR (state) {
      state.lastErrorId = null;
    },
    SAVE_WEATHER_ARRAY (state, title) {
      state.arrayofWeather.push(title);
    },
    CLEAN_WEATHER_ARRAY (state) {
      state.arrayofWeather.length = 0;
    },
    CLEAN_ELEMENT_GRAPH_ARRAY(state) {
      state.elementGraphsArray.length = 0;
    },
    /*SAVE_ONE_ELEMENT (state, { id, name }) {
      if (!state.graphArray.find((el) => el.id === id)) {
        state.graphArray.push({ id, name });
      } else {
        state.graphArray.forEach(function (el, i) {
          if (el.id === id) state.graphArray.splice(i, 1);
        });
      }
    },*/
    /*
    CLEAN_GRAPH_ARRAY (state) {
      state.graphArray.length = 0;
    },*/
    THROW_NEW_MESSAGE (state, { type, name }) {
      if (state.myErrorsArray.length < state.maxErrorsArraySize) {
        const id = crc32(new Date().toString()).toString(16) + Math.random(10);
        const errorConfig = state.messages
          .find((obj) => obj.messageType === type)
          .messageList.find((obj) => obj.name === name);
        const currentError = { ...errorConfig, id };
        state.lastErrorId = currentError.id;
        state.myError = currentError;
        state.myErrorsArray.push(currentError);
      }
    },
    SAVE_GRAPHS_FOR_ELEMENT (state, { element, graphs }) {
      if (
        !state.elementGraphsArray.find((el) => el.element.id === element.id)
      ) {
        state.elementGraphsArray.push({ element, graphs });
      } else {
        state.elementGraphsArray.forEach(function (el, i) {
          if (el.element.id === element.id) {
            state.elementGraphsArray.splice(i, 1);
          }
        });
        state.elementGraphsArray.push({ element, graphs });
      }
    },
    DELETE_ERROR_IN_ARRAY_BY_ID (state, id) {
      state.myErrorsArray = state.myErrorsArray.filter(
        (value) => value.id !== id
      );
    },
    GET_GRAPHS_BY_ELEMENT (state, id) {
      const myElement = state.elementGraphsArray.find(
        (el) => el.element.id === id
      );
      if (myElement) {
        state.myTempGraphArray = myElement.graphs;
      }
    },
    DELETE_LAST_ERROR_IN_ARRAY (state) {
      if (state.myErrorsArray.length > 0) {
        state.myErrorsArray = state.myErrorsArray.slice(
          0,
          state.myErrorsArray.length - 1
        );
      }
    },
    CREATE_LOCAL_USER (state, { firstname, secondname }) {
      state.firstname = firstname;
      state.secondname = secondname;
    }
  },
  actions: {
    /* throwError({ commit }, myError) {
      commit('THROW_NEW_ERROR', myError)
    },*/
    throwMessage ({ commit }, { type, name }) {
      commit('THROW_NEW_MESSAGE', { type, name });
    },
    deleteErrorInArrayById ({ commit }, id) {
      commit('DELETE_ERROR_IN_ARRAY_BY_ID', id);
    },
    deleteLastErrorInArray ({ commit }) {
      commit('DELETE_LAST_ERROR_IN_ARRAY');
    },
    forgetError ({ commit }) {
      commit('FORGET_CURRENT_ERROR');
    },
    createUser ({ commit }, { firstname, secondname }) {
      commit('CREATE_LOCAL_USER', { firstname, secondname });
    },
    createWeatherArray ({ commit }, title) {
      commit('SAVE_WEATHER_ARRAY', title);
    },
    getGraphsByElementId ({ commit }, id) {
      commit('GET_GRAPHS_BY_ELEMENT', id);
    },
    saveGraph ({ commit }, { element, graphs }) {
      commit('SAVE_GRAPHS_FOR_ELEMENT', { element, graphs });
    },
    cleanWeatherArray ({ commit }) {
      commit('CLEAN_WEATHER_ARRAY');
    },
    cleanElementGraphsArray({ commit }) {
      commit('CLEAN_ELEMENT_GRAPH_ARRAY');
    }
  }
});

export default store;
