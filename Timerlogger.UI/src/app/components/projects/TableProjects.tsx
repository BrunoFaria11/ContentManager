import React from "react";
import { useNavigate } from "react-router-dom";
import {
  Eye,
  Pencil,
  Play,
  Check,
  PersonWorkspace,
  Clock,
} from "react-bootstrap-icons";

export default function TableProjects(props: any) {
  const navigate = useNavigate();
  return (
    <tbody>
      {props.data.map((item: any, index: number) => {
        return (
          <tr>
            <td style={{ textAlign: "center" }}>{index + 1}</td>
            <td style={{ textAlign: "center" }}>{item.name}</td>
            <td style={{ textAlign: "center" }}>
              {new Date(item.deadLine).toLocaleDateString("en-CA")}
            </td>
            <td style={{ textAlign: "center" }}>{item.timePerWeek}</td>
            <td style={{ textAlign: "center" }}>{item.totalTimeSpent  == null || item.totalTimeSpent  == 0 ? 0 : (Math.round(item.totalTimeSpent * 100) / 100).toFixed(2)}</td>
            <td style={{ textAlign: "center" }}>
              {item.isCompleted ? (
                <label>
                  <Check color="green" size={15} />
                </label>
              ) : (
                <label>
                  <PersonWorkspace color="gray" size={15} />
                </label>
              )}
            </td>
            <button
              type="button"
              className="btn btn-primary mb-2 mt-2"
              onClick={() => navigate(`/projectDetails?ProjectId=${item.id}`)}
            >
              <Eye color="white" size={15} />
            </button>
            {!item.isCompleted ? (
                            <><button
                type="button"
                className="btn btn-success ml-1"
                onClick={() => props.editModal(item)}
              >
                <Pencil color="white" size={15} />
              </button><button
                type="button"
                className="btn btn-danger ml-1"
                onClick={() => props.createTimeHistory(item.id)}
              >
                  <Play color="white" size={15} />
                </button><button type="button" className="btn btn-info ml-1">
                  <Clock color="white" size={15} />
                </button></>
              ) : null}

          </tr>
        );
      })}
    </tbody>
  );
}
