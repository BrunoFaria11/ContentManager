import * as React from "react";
import Projects from "./views/Projects";
import { BrowserRouter, Routes, Route } from "react-router-dom";
import "./style.css";
import ProjectDetails from "./views/ProjectDetails";

export default function App() {
  return (
    <>
      <header className="bg-gray-900 text-white flex items-center h-12 w-full">
        <div className="container mx-auto">
          <a className="navbar-brand" href="/">
            Timelogger
          </a>
        </div>
      </header>

      <main>
        <div className="container mx-auto">
          <BrowserRouter>
            <Routes>
              <Route path="/" element={<Projects />} />
              <Route path="/projectDetails" element={<ProjectDetails />} />
            </Routes>
          </BrowserRouter>
        </div>
      </main>
    </>
  );
}
