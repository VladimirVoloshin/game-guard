import axios, { type AxiosResponse } from 'axios'; 
 
axios.defaults.baseURL = process.env.REACT_APP_GAME_GUARD_API_URL; 

const responseBody = <T>(response: AxiosResponse<T>): T => response.data; 
 
const requests = { 
  get: async <T>(url: string) => 
    await axios.get<T>(url).then(responseBody), 
  
  put: async <T>(url: string, body: unknown) => 
    await axios.put<T>(url, body).then(responseBody), 
}; 
 
export default requests;