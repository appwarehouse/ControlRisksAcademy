import http from "./httpService";
import config from "./../config.json";

const apiEndpoint = `${config.apiEndpoint}/User/register`;

export function register(user) {
  return http.post(apiEndpoint, {
    firstName: user.name,
    lastName: user.surname,
    username: user.email,
    email: user.email,
    password: user.password,
  });
}
