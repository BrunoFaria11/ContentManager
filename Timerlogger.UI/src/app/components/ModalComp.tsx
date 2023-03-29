import React from "react";
import {
  ModalBody,
  ModalHeader,
  ModalTitle,
} from "react-bootstrap";

export default function ModalComp(props: any) {
  return (
    <div>
      <ModalHeader closeButton onClick={() => props.initModal(false)}>
        <ModalTitle>{props.title ?? ""}</ModalTitle>
      </ModalHeader>
      <ModalBody>{props.body}</ModalBody>
    </div>
  );
}
