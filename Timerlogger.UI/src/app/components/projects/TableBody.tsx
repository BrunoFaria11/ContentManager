import React from "react";

export default function TableBody(props: any) {
  return (
    <tbody>
      {props.data.map((item: any) => {
        return (
          <tr>
            <td>{item.id}</td>
            <td>{item.name}</td>
            <td>{item.deadLine}</td>
            <td>{item.timePerWeek}</td>
            <td>{item.isCompleted}</td>
          </tr>
        );
      })}
    </tbody>
  );
}
