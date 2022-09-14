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
  let response = null;
  response = await http
    .post(`${apiEndpoint}/new`, course)
    .catch((error) => (response = error));

  if (response.status === undefined) {
    return { success: false, data: null, message: response.response.data };
  }

  if (response.status === 200) {
    return { success: true, data: response.data, message: "" };
  }
}

export async function updateCourse(id, course) {
  let response = null;
  response = await http
    .put(`${apiEndpoint}/update/${id}`, course)
    .catch((error) => (response = error));

  if (response.status === undefined) {
    return { success: false, data: null, message: response.response.data };
  }

  if (response.status === 204) {
    return { success: true, data: response.data, message: "" };
  }
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
