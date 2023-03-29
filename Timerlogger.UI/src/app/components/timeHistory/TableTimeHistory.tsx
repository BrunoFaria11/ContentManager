import React from "react";

export default function TableTimeHistory(props: any) {
  return (
    <tbody>
      {props.data.map((item: any, index: number) => {
        return (
          <tr>
            <td style={{ textAlign: "center" }}>{index + 1}</td>
            <td style={{ textAlign: "center" }}>{new Date(item.startDate).toLocaleString()}</td>
            <td style={{ textAlign: "center" }}>{new Date(item.endDate).toLocaleString()}</td>
            <td style={{ textAlign: "center" }}>{(Math.round(item.totalHours * 100) / 100).toFixed(2)}</td>
          </tr>
        );
      })}
    </tbody>
  );
}

