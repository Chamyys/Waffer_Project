import axios from 'axios'
//import config from './svrApiKey.json'

const api = axios.create({
  baseURL: 'https://localhost:3000/api/proxy/',
})

export default api

export const getWafers = async () => {
  try {
    const response = await api.get('wafer/all', {
      // headers: {
      //   'x-api-key': config.key,
      //},
    })
    return response.data
  } catch (error) {
    console.error(error)
  }
}
/*
export const getUser = async (userId) => {
  try {
    const response = await api.get(`/users/${userId}`)
    return response.data
  } catch (error) {
    console.error(error)
  }
}

export const createUser = async (user) => {
  try {
    const response = await api.post('/users', user)
    return response.data
  } catch (error) {
    console.error(error)
  }
}
*/
