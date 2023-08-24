import axios from 'axios';

const api = axios.create({
  baseURL: 'https://ваш-базовый-URL-api.com',
});

export default api;

export const getUsers = async () => {
  try {
    const response = await api.get('/users');
    return response.data;
  } catch (error) {
    console.error(error);
  }
};

export const getUser = async (userId) => {
  try {
    const response = await api.get(`/users/${userId}`);
    return response.data;
  } catch (error) {
    console.error(error);
  }
};

export const createUser = async (user) => {
  try {
    const response = await api.post('/users', user);
    return response.data;
  } catch (error) {
    console.error(error);
  }
};
