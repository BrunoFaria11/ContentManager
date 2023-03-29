import React, { useState } from "react";
import { editProject, postProject } from "../../api/projects";

export default function FormProjects(props: any) {
  const [timerPerWeek, setTimerPerWeek] = useState(
    props.isEdit ? props.project.timePerWeek : ""
  );
  const [deadLine, setDeadLine] = useState(
    props.isEdit ? props.project.deadLine : ""
  );
  const [projectName, setProjectName] = useState(
    props.isEdit ? props.project.name : ""
  );
  const [isCompleted, setIsCompleted] = useState(
    props.isEdit ? props.project.isCompleted : ""
  );

  const createProject = async () => {
    props.initModal(false);
    await postProject(
      projectName,
      deadLine == "" ? new Date() : new Date(deadLine),
      Number(timerPerWeek)
    ).then((res) => {
      props.response(JSON.stringify(res));
    });
    props.fetchProjects();
  };

  const updateProject = async () => {
    props.initModal(false);
    await editProject(
      props.isEdit ? props.project.id : "",
      projectName,
      new Date(deadLine),
      Number(timerPerWeek),
      isCompleted
    ).then((res) => {
      props.response(JSON.stringify(res));
    });
    props.fetchProjects();
  };

  return (
    <>
      <form>
        <div className="row">
          <div className="col-6">
            <label>Name</label>
            <input
              className="form-control"
              placeholder="Enter Project Name"
              onChange={(e) => setProjectName(e.target.value)}
              value={projectName}
            />
          </div>
          <div className="col-6">
            <label>Time per Week </label>
            <span>
              ({timerPerWeek == null || timerPerWeek == "" ? 0 : timerPerWeek})
            </span>
            <input
              type="range"
              className="custom-range"
              min="0"
              max="8"
              id="rangeInput"
              onChange={(e) => setTimerPerWeek(e.target.value)}
              value={
                timerPerWeek == null || timerPerWeek == "" ? 0 : timerPerWeek
              }
            />
          </div>
        </div>
        <br></br>
        <div className="row">
          <div className="col-6">
            <label>Deadline Date</label>
            <input
              type="date"
              className="form-control"
              min={new Date().toLocaleDateString("en-CA")}
              onChange={(e) => setDeadLine(e.target.value)}
              defaultValue={new Date(deadLine).toLocaleDateString("en-CA")}
            />
          </div>
          {props.isEdit ? (
            <div className="col-6">
              <label>Mark as Completed</label>
              <div className="form-check">
                <input
                  className="form-check-input"
                  type="checkbox"
                  onChange={(e) => setIsCompleted(e.target.checked)}
                  value={isCompleted}
                />
              </div>
            </div>
          ) : null}
        </div>
        <br></br>
        <div className="row">
          <div className="form-group">
            {props.isEdit ? (
              <button
                className="btn btn-primary"
                onClick={() => updateProject()}
                type="button"
              >
                Update
              </button>
            ) : (
              <button
                className="btn btn-primary"
                type="button"
                onClick={() => createProject()}
              >
                Create
              </button>
            )}
          </div>
        </div>
      </form>
    </>
  );
}
