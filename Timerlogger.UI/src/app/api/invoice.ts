import { InvoiceModel } from "../models/InvoiceModel";

const BASE_URL = "https://localhost:3001/api";

export async function getAllInvoices(projectId: string) {
    const response = await fetch(
      `${BASE_URL}/Invoice/GetAllInvoices?ProjectId=${projectId}`
    );
    return response.json();
  }

export async function postInvoice(
    projectId: String,
    devName: String,
    devDocNumber: String,
    invoiceDate: Date,
    customerName: String,
    customerDocNumber: String
  ) {
    const invoice = new InvoiceModel("",
        projectId,
        devName,
        devDocNumber,
        "",
        invoiceDate,
        customerName,
        customerDocNumber);

    const requestOptions = {
      method: "POST",
      headers: { "Content-Type": "application/json" },
      body: JSON.stringify(invoice),
    };
    const response = await fetch(`${BASE_URL}/Invoice`, requestOptions);
    const data = await response.json();
    return data;
  }