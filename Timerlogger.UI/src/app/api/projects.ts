const BASE_URL = "https://localhost:3001/api";

export async function getAll() {
    const response = await fetch(`${BASE_URL}/Projects/GetAllProjects`);
    return response.json();
}
