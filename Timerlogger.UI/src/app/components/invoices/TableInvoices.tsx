import React from "react";

export default function TableInvoices(props: any) {
  return (
    <tbody>
      {props.data.map((item: any, index: number) => {
        return (
          <tr>
            <td style={{ textAlign: "center" }}>{index + 1}</td>
            <td style={{ textAlign: "center" }}>{item.devName}</td>
            <td style={{ textAlign: "center" }}>{item.devDocNumber}</td>
            <td style={{ textAlign: "center" }}>{item.invoiceNumber}</td>
            <td style={{ textAlign: "center" }}>
              {new Date(item.invoiceDate).toLocaleDateString("en-CA")}
            </td>
            <td style={{ textAlign: "center" }}>{item.customerName}</td>
            <td style={{ textAlign: "center" }}>{item.customerDocNumber}</td>
          </tr>
        );
      })}
    </tbody>
  );
}
