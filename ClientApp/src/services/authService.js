import http from "./httpService";
import jwtDecode from "jwt-decode";
import config from "../config.json";

const apiEndpoint = `${config.apiEndpoint}/user`;
const tokenKey = "token";

http.setJwt(getJwt());

export async function login(email, password) {
  const { data: data } = await http.post(`${apiEndpoint}/token`, {
    email,
    password,
  });
  if (data.isAuthenticated) setToken(data.token);

  return data;
}

function setToken(token) {
  localStorage.setItem(tokenKey, token);
}

export function loginWithJwt(jwt) {
  localStorage.setItem(tokenKey, jwt);
}

export function logout() {
  localStorage.removeItem(tokenKey);
}

export function getCurrentUser() {
  try {
    const jwt = localStorage.getItem(tokenKey);
    //ToDo: Check if token is expired and refresh
    return jwtDecode(jwt);
  } catch (ex) {
    return null;
  }
}

export async function refreshToken() {
  await http.post(`${apiEndpoint}/token`).then((data) => {
    setToken(data.token);
  });
}

export function getJwt() {
  return localStorage.getItem(tokenKey);
}

export default {
  login,
  loginWithJwt,
  logout,
  getCurrentUser,
  getJwt,
};
