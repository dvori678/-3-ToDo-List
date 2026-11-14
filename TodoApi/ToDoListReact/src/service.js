import axios from 'axios';

axios.defaults.baseURL = "http://localhost:5299"; // או https://localhost:5299 בהתאם למה שהשרת שלך מציג

axios.interceptors.response.use(
  (response) => response,
  (error) => {
    console.error("API Error Interceptor:", error);
    return Promise.reject(error);
  }
);

export default {
  getTasks: async () => {
    const result = await axios.get('/tasks');
    return result.data;
  },

  addTask: async (name) => {
    const result = await axios.post('/new', { name: name, isComplete: false });
    return result.data;
  },

setCompleted: async (id, isComplete) => {
    console.log('setCompleted', { id, isComplete });
    const result = await axios.put(`/items/${id}`, { isComplete: isComplete });
    return result.data;
  },

  deleteTask: async (id) => {
    const result = await axios.delete(`/delete/${id}`);
    return result.data;
  }
};