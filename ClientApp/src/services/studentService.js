import http from "./httpService";
import config from "../config.json";

const apiEndpoint = `${config.apiEndpoint}/Student`;

export async function list() {
  const { data } = await http.get(`${apiEndpoint}/list`);
  return data;
}

export async function details(id) {
  return await http.get(`${apiEndpoint}/details/${id}`);
}

export async function createStudent(student) {
  let response = null;
  response = await await http
    .post(`${apiEndpoint}/new`, student)
    .catch((error) => (response = error));

  if (response.status === undefined) {
    return { success: false, data: null, message: response.response.data };
  }

  if (response.status === 200) {
    return { success: true, data: response.data, message: "" };
  }
}

export async function updateStudent(id, classroom) {
  return await http.put(`${apiEndpoint}/update/${id}`, classroom);
}

export async function deactivateStudent(id) {
  const { status } = await http.delete(`${apiEndpoint}/deactivate/${id}`);

  if (status === 200) {
    return true;
  }
  return false;
}

export default {
  list,
  details,
  createStudent,
  updateStudent,
  deactivateStudent,
};
