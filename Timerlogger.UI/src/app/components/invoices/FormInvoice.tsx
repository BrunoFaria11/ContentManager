import React, { useState } from "react";
import { postInvoice } from "../../api/invoice";

export default function FormInvoice(props: any) {
  const [devName, setDevName] = useState("");
  const [devDocNumber, setDevDocNumber] = useState("");
  const [invoiceDate, setInvoiceDate] = useState("");
  const [customerName, setCustomerName] = useState("");
  const [customerDocNumber, setCustomerDocNumber] = useState("");

  const createInvoice = async () => {
    props.initModal(false);
    await postInvoice(
      props.projectId,
      devName,
      devDocNumber,
      new Date(invoiceDate),
      customerName,
      customerDocNumber
    ).then((res) => {
      props.response(JSON.stringify(res));
    });
    props.fetchInvoices();
  };

  return (
    <>
      <form>
        <div className="row">
          <div className="col-6">
            <label>Dev Name</label>
            <input
              className="form-control"
              placeholder="Enter Dev Name"
              onChange={(e) => setDevName(e.target.value)}
            />
          </div>
          <div className="col-6">
            <label>Dev Doc Number</label>
            <input
              className="form-control"
              placeholder="Dev Doc Number"
              onChange={(e) => setDevDocNumber(e.target.value)}
            />
          </div>
        </div>
        <br></br>

        <div className="row">
          <div className="col-6">
            <label>Invoice Date</label>
            <input
              type="date"
              className="form-control"
              min={new Date().toLocaleDateString("en-CA")}
              onChange={(e) => setInvoiceDate(e.target.value)}
            />
          </div>
          <div className="col-6">
            <label>Customer Name</label>
            <input
              className="form-control"
              placeholder="Enter Customer Name"
              onChange={(e) => setCustomerName(e.target.value)}
            />
          </div>
        </div>
        <br></br>
        <div className="row">
          <div className="col-6">
            <label>Customer Doc Number</label>
            <input
              className="form-control"
              placeholder="Enter Customer Doc Number"
              onChange={(e) => setCustomerDocNumber(e.target.value)}
            />
          </div>
        </div>
        
        <br></br>
        <div className="row">
          <div className="form-group">
            <button
              className="btn btn-primary"
              type="button"
              onClick={() => createInvoice()}
            >
              Create
            </button>
          </div>
        </div>
      </form>
    </>
  );
}
