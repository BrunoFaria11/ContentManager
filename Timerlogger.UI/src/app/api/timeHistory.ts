import { ProjectModel } from "../models/ProjectModel";
import { TimeHistoryModel } from "../models/TimeHistoryModel";

const BASE_URL = "https://localhost:3001/api";

export async function getAll(projectId: string) {
  const response = await fetch(
    `${BASE_URL}/TimerHistory/GetAllTimerHistories?ProjectId=${projectId}`
  );
  return response.json();
}

export async function postTimeHistory(
  projectId: String,
  startDate: Date,
  endDate: Date
) {
  const timeHistory = new TimeHistoryModel(projectId, startDate, endDate);
  const requestOptions = {
    method: "POST",
    headers: { "Content-Type": "application/json" },
    body: JSON.stringify(timeHistory),
  };
  const response = await fetch(`${BASE_URL}/TimerHistory`, requestOptions);
  const data = await response.json();
  return data;
}

export async function editTimeHistory(
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
  const response = await fetch(`${BASE_URL}/TimerHistory`, requestOptions);
  const data = await response.json();
  return data;
}
