import http from "./httpService";
import config from "../config.json";

const apiEndpoint = `${config.apiEndpoint}/Classroom`;

export async function list() {
  const { data } = await http.get(`${apiEndpoint}/list`);
  return data;
}

export async function details(id) {
  return await http.get(`${apiEndpoint}/details/${id}`);
}

export async function createClassroom(classroom) {
  let response = null;
  response = await http
    .post(`${apiEndpoint}/new`, classroom)
    .catch((error) => (response = error));

  if (response.status === undefined) {
    return { success: false, data: null, message: response.response.data };
  }

  if (response.status === 200) {
    return { success: true, data: response.data, message: "" };
  }
}

export async function updateClassroom(id, classroom) {
  const { status } = await http.put(`${apiEndpoint}/update/${id}`, classroom);
  if (status === 204) {
    return true;
  }
  return false;
}

export async function deleteClassroom(id) {
  const { status } = await http.delete(`${apiEndpoint}/delete/${id}`);

  if (status === 200) {
    return true;
  }
  return false;
}

export default {
  list,
  details,
  createClassroom,
  updateClassroom,
  deleteClassroom,
};
