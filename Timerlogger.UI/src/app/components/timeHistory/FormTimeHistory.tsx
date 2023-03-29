import React, { useState } from "react";
import { postTimeHistory } from "../../api/timeHistory";

export default function FormTimeHistory(props: any) {

  const [startDate, setStartDate] = useState("");
  const [endDate, setEndDate] = useState("");

  const createTimeHistory = async () => {
    props.initModal(false);
    await postTimeHistory(
      props.projectId,
      new Date(startDate),
      new Date(endDate)
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
            <label>Strat Date</label>
            <input
              type="datetime-local"
              className="form-control"
              min={new Date()
                .toISOString()
                .slice(0, new Date().toISOString().lastIndexOf(":"))}
              onChange={(e) => setStartDate(e.target.value)}
            />
          </div>
          <div className="col-6">
            <label>End Date </label>
            <input
              type="datetime-local"
              className="form-control"
              min={new Date()
                .toISOString()
                .slice(0, new Date().toISOString().lastIndexOf(":"))}
              onChange={(e) => setEndDate(e.target.value)}
            />
          </div>
        </div>
        <br></br>
        <div className="row">
          <div className="form-group">
            <button
              className="btn btn-primary"
              type="button"
              onClick={() => createTimeHistory()}
            >
              Create
            </button>
          </div>
        </div>
      </form>
    </>
  );
}
