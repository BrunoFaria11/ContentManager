import { ProjectModel } from "../models/ProjectModel";

const BASE_URL = "https://localhost:3001/api";

export async function getAll(isToOrder: boolean, isCompleted: boolean) {
  const response = await fetch(
    `${BASE_URL}/Projects/GetAllProjects?IsCompleted=${isCompleted}&IsToSortDesc=${isToOrder}`
  );
  return response.json();
}

export async function postProject(
  name: String,
  deadLine: Date,
  timePerWeek: Number
) {
  const project = new ProjectModel("", name, deadLine, timePerWeek, 0, false);
  const requestOptions = {
    method: "POST",
    headers: { "Content-Type": "application/json" },
    body: JSON.stringify(project),
  };
  const response = await fetch(`${BASE_URL}/Projects`, requestOptions);
  const data = await response.json();
  return data;
}

export async function editProject(
  id: String,
  name: String,
  deadLine: Date,
  timePerWeek: Number,
  isCompleted: boolean
) {
  const project = new ProjectModel(
    id,
    name,
    deadLine,
    timePerWeek,
    0,
    isCompleted
  );
  const requestOptions = {
    method: "PATCH",
    headers: { "Content-Type": "application/json" },
    body: JSON.stringify(project),
  };
  const response = await fetch(`${BASE_URL}/Projects`, requestOptions);
  const data = await response.json();
  return data;
}
