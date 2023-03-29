export class InvoiceModel {
  id: String;
  projectId: String;
  devName: String;
  devDocNumber: String;
  invoiceNumber: String;
  invoiceDate: Date;
  customerName: String;
  customerDocNumber: String;

  constructor(
    id: String,
    projectId: String,
    devName: String,
    devDocNumber: String,
    invoiceNumber: String,
    invoiceDate: Date,
    customerName: String,
    customerDocNumber: String
  ) {
    this.id = id;
    this.projectId = projectId;
    this.devName = devName;
    this.devDocNumber = devDocNumber;
    this.invoiceNumber = invoiceNumber;
    this.invoiceDate = invoiceDate;
    this.customerName = customerName;
    this.customerDocNumber = customerDocNumber;
  }
}
