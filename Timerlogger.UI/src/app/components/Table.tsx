import React from "react";

export default function Table(props: any) {
  return (
    <table className="table-fixed w-full">
      <thead className="bg-gray-200">
        <tr>
          {props.headers.map((header: string) => {
            return <th>{header}</th>;
          })}
        </tr>
      </thead>
      {props.body}
    </table>
  );
}
