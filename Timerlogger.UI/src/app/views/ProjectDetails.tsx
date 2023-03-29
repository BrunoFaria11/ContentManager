import React, { useEffect, useState } from "react";
import { Modal } from "react-bootstrap";
import { getAllInvoices } from "../api/invoice";
import { getAll } from "../api/timeHistory";
import FormInvoice from "../components/invoices/FormInvoice";
import TableInvoices from "../components/invoices/TableInvoices";
import ModalComp from "../components/ModalComp";
import ReponseAlert from "../components/ReponseAlert";
import Table from "../components/Table";
import TableTimeHistory from "../components/timeHistory/TableTimeHistory";

export default function ProjectDetails() {
  const queryParameters = new URLSearchParams(window.location.search);
  const projectId = queryParameters.get("ProjectId");

  const tableHeaders = ["#", "Start Date", "End Date", "Total Hours"];
  const invoiceTableHeaders = [
    "#",
    "Dev Name",
    "Dev Doc Number",
    "Invoice Number",
    "Invoice Date",
    "Customer Name",
    "Customer Doc Number",
  ];

  const [isShow, invokeModal] = useState(false);
  const [data, setData] = useState([]);
  const [dataInvoice, setDataInvoice] = useState([]);
  const [showAlert, setShowAlert] = useState(false);
  const [response, setResponse] = useState("");

  const initModal = (istToOpen: boolean) => {
    return invokeModal(istToOpen);
  };

  const fetchTimeHistory = () => {
    return getAll(projectId ?? "")
      .then((res) => res.data)
      .then((d) => setData(d));
  };

  const openResponse = (response: any) => {
    setShowAlert(true);
    setResponse(response);
  };

  const fetchInvoices = () => {
    return getAllInvoices(projectId ?? "")
      .then((res) => res.data)
      .then((d) => setDataInvoice(d));
  };

  useEffect(() => {
    fetchTimeHistory();
  }, []);

  useEffect(() => {
    fetchInvoices();
  }, []);

  const tableBody = <TableTimeHistory data={data} />;
  const tableInvoiceBody = <TableInvoices data={dataInvoice} />;
  const formInvoice = (
    <FormInvoice
      fetchInvoices={fetchInvoices}
      initModal={initModal}
      projectId={projectId}
      response={openResponse}
    />
  );

  return (
    <>
      {showAlert ? <ReponseAlert response={response} /> : null}
      <div className="items-center my-6">
        <div className="card">
          <div className="card-header">Time History</div>
          <div className="card-body">
            <Table headers={tableHeaders} body={tableBody} />
          </div>
        </div>
      </div>
      <div className="items-center my-6">
        <div className="card">
          <div className="card-header">Invoice</div>
          <div className="card-body">
            <div className="flex items-center my-6">
              <div className="w-1/2">
                <button
                  className="bg-blue-500 hover:bg-blue-700 text-white font-bold py-2 px-4 rounded"
                  onClick={() => initModal(true)}
                >
                  Add Invoice
                </button>
              </div>
            </div>
            <Modal show={isShow}>
              <ModalComp
                initModal={invokeModal}
                title="Add Invoice"
                body={formInvoice}
              />
            </Modal>
            <Table headers={invoiceTableHeaders} body={tableInvoiceBody} />
          </div>
        </div>
      </div>
    </>
  );
}
