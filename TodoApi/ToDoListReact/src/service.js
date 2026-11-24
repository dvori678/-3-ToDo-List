import axios from 'axios';

axios.defaults.baseURL = process.env.REACT_APP_API_URL;

axios.interceptors.response.use(
  (response) => response,
  (error) => {
    console.error("API Error Interceptor:", error);
    return Promise.reject(error);
  }
);

const service= {
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

export default service;