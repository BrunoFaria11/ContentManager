import React, { useEffect, useState } from "react";
import { Modal } from "react-bootstrap";
import { getAll } from "../api/projects";
import ModalComp from "../components/ModalComp";
import FormProjects from "../components/projects/FormProjects";
import TableBody from "../components/projects/TableBody";
import Table from "../components/Table";

export default function Projects() {
  const [isShow, invokeModal] = React.useState(false);
  const initModal = () => {
    return invokeModal(!false);
  };

  const [data, setData] = useState([]);

  const fetchProjects = () => {
    return getAll()
      .then((res) => res.data)
      .then((d) => setData(d));
  };

  useEffect(() => {
    fetchProjects();
  }, []);

  const tableHeaders = [
    "#",
    "Name",
    "DeadLine",
    "TimePerWeek",
    "TotalTimeSpent",
    "IsCompleted",
  ];

  const tableBody = <TableBody data={data} />;
  const formProjects = <FormProjects />;
  return (
    <>
      <div className="flex items-center my-6">
        <div className="w-1/2">
          <button
            className="bg-blue-500 hover:bg-blue-700 text-white font-bold py-2 px-4 rounded"
            onClick={initModal}
          >
            Add Project
          </button>
        </div>
        <Modal show={isShow}>
          <ModalComp initModal={invokeModal} body={formProjects} />
        </Modal>
        <div className="w-1/2 flex justify-end">
          <form>
            <input
              className="border rounded-full py-2 px-4"
              type="search"
              placeholder="Search"
              aria-label="Search"
            />
            <button
              className="bg-blue-500 hover:bg-blue-700 text-white rounded-full py-2 px-4 ml-2"
              type="submit"
            >
              Search
            </button>
          </form>
        </div>
      </div>

      <Table headers={tableHeaders} body={tableBody} />
    </>
  );
}
