
import axios, { AxiosResponse } from 'axios';
import { SectorOption } from '../models/sector-option';
import { User } from '../models/user';

const sleep = (delay: number) => {
    return new Promise((resolve) => {
      setTimeout(resolve, delay);
    });
  };

axios.defaults.baseURL = 'http://localhost:5000/api';

axios.interceptors.response.use(async response => {
    try {
        await sleep(3000);
        return response;
    } catch (error) {
        console.log(error);
        return await Promise.reject(error);
    }
});

const headers = {
  headers: {
    "Content-Type": "application/json",
  },
};

const responseBody = <T>(response: AxiosResponse<T>) => response.data;

const requests = {
  users: {
    put: <T> (url: string, body: {}) => axios.put<T>(url, body).then(responseBody),
  },

  sectorOptions: {
    get: <T>(url: string) => axios.get<T>(url).then(responseBody),
  },
};

const users = {
  edit: (body: User) => requests.users.put<User>(`/users/${body.id}`, body),
};

const sectorOptions = {
  list: () => requests.sectorOptions.get<SectorOption[]>(`/sectorOptions`),
};

const agent = {
  users: users,
  sectorOptions: sectorOptions,
};

export default agent;
