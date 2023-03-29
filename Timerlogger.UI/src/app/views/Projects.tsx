import React, { useEffect, useState } from "react";
import { Modal } from "react-bootstrap";
import { getAll } from "../api/projects";
import ModalComp from "../components/ModalComp";
import FormProjects from "../components/projects/FormProjects";
import TableProjects from "../components/projects/TableProjects";
import Table from "../components/Table";
import FormTimeHistory from "../components/timeHistory/FormTimeHistory";
import { ArrowUp, ArrowDown } from "react-bootstrap-icons";
import ReponseAlert from "../components/ReponseAlert";

export default function Projects() {
  const tableHeaders = [
    "#",
    "Name",
    "Dead Line",
    "Time Per Week",
    "Total Time Spent",
    "Completed",
    "Actions",
  ];

  const [isShow, invokeModal] = useState(false);
  const [isShowTimeHistory, invokeModalToTimeHistory] = useState(false);
  const [isEdit, setIsEdit] = useState(false);
  const [project, setProject] = useState();
  const [projectId, setProjectId] = useState("");
  const [data, setData] = useState([]);
  const [dataCompleted, setDataCompleted] = useState([]);
  const [modalTitle, setModalTitle] = useState("Add Project");
  const [isToOrder, setIsToOrder] = useState(false);
  const [showAlert, setShowAlert] = useState(false);
  const [response, setResponse] = useState("");

  const initModal = (istToOpen: boolean) => {
    return invokeModal(istToOpen);
  };

  const initModalToTimeHistory = (istToOpen: boolean) => {
    return invokeModalToTimeHistory(istToOpen);
  };

  const fetchProjects = () => {
    getAll(false, false)
      .then((res) => res.data)
      .then((d) => setData(d));
    getAll(false, true)
      .then((res) => res.data)
      .then((d) => setDataCompleted(d));
  };

  const editModal = (project: any) => {
    setIsEdit(true);
    setProject(project);
    setModalTitle("Edit Project");
    initModal(true);
  };

  const createProject = () => {
    setIsEdit(false);
    initModal(true);
  };

  const createTimeHistory = (projectId: string) => {
    setProjectId(projectId);
    initModalToTimeHistory(true);
  };

  const orderProjects = () => {
    setIsToOrder(!isToOrder);
    return getAll(isToOrder, false)
      .then((res) => res.data)
      .then((d) => setData(d));
  };

  const openResponse = (response: any) => {
    setShowAlert(true);
    setResponse(response);
  };

  useEffect(() => {
    fetchProjects();
  }, []);

  const formProjects = (
    <FormProjects
      isEdit={isEdit}
      project={project}
      response={openResponse}
      initModal={initModal}
      fetchProjects={fetchProjects}
    />
  );

  const formTimeHistory = (
    <FormTimeHistory
      projectId={projectId}
      response={openResponse}
      initModal={initModalToTimeHistory}
      fetchProjects={fetchProjects}
    />
  );

  const tableBody = (
    <TableProjects
      data={data}
      editModal={editModal}
      createTimeHistory={createTimeHistory}
    />
  );

  const tableBodyToProjectCompleted = (
    <TableProjects
      data={dataCompleted}
      editModal={editModal}
      createTimeHistory={createTimeHistory}
    />
  );

  return (
    <>
      <div className="items-center my-6">
        <div className="card">
          <div className="card-header">Prjects Not Completed</div>
          <div className="card-body">
            {showAlert ? <ReponseAlert response={response} /> : null}
            <div className="flex items-center my-6">
              <div className="w-1/2">
                <button
                  className="bg-blue-500 hover:bg-blue-700 text-white font-bold py-2 px-4 rounded"
                  onClick={() => createProject()}
                >
                  Add Project
                </button>
              </div>
              <Modal show={isShow}>
                <ModalComp
                  initModal={invokeModal}
                  title={modalTitle}
                  body={formProjects}
                />
              </Modal>
              <Modal show={isShowTimeHistory}>
                <ModalComp
                  initModal={invokeModalToTimeHistory}
                  title="Add Timer"
                  body={formTimeHistory}
                />
              </Modal>
              <div className="w-1/2 flex justify-end">
                <form>
                  <button
                    className="bg-blue-500 hover:bg-blue-700 text-white rounded-full py-2 px-4 ml-2"
                    onClick={() => orderProjects()}
                    type="button"
                  >
                    <label>
                      {isToOrder ? (
                        <ArrowDown color="white" size={20} />
                      ) : (
                        <ArrowUp color="white" size={20} />
                      )}
                    </label>
                  </button>
                </form>
              </div>
            </div>
            <Table headers={tableHeaders} body={tableBody} />{" "}
          </div>
        </div>
      </div>
      <div className="items-center my-6">
        <div className="card">
          <div className="card-header">Projects Completed</div>
          <div className="card-body">
            <Table headers={tableHeaders} body={tableBodyToProjectCompleted} />
          </div>
        </div>
      </div>
    </>
  );
}
