import http from "./httpService";
import config from "../config.json";

const apiEndpoint = `${config.apiEndpoint}/Teacher`;

export async function list() {
  const { data } = await http.get(`${apiEndpoint}/list`);
  return data;
}

export async function details(id) {
  return await http.get(`${apiEndpoint}/details/${id}`);
}
export default {
  list,
  details,
};
