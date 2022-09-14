import http from "./httpService";
import config from "../config.json";

const apiEndpoint = `${config.apiEndpoint}/Course`;

export async function list() {
  const { data } = await http.get(`${apiEndpoint}/list`);
  return data;
}

export async function details(id) {
  const { data } = await http.get(`${apiEndpoint}/details/${id}`);
  return data;
}

export async function createCourse(course) {
  return await http.post(`${apiEndpoint}/new`, course);
}

export async function updateCourse(id, course) {
  return await http.put(`${apiEndpoint}/update/${id}`, course);
}

export async function deactivateCourse(id) {
  const { status } = await http.delete(`${apiEndpoint}/deactivate/${id}`);

  if (status === 200) {
    return true;
  }
  return false;
}

export default {
  list,
  details,
  createCourse,
  updateCourse,
  deactivateCourse,
};
