import React from "react";
import {
  ModalBody,
  ModalFooter,
  ModalHeader,
  ModalTitle,
} from "react-bootstrap";

export default function ModalComp(props: any) {
  return (
    <div>
      <ModalHeader closeButton onClick={props.initModal}>
        <ModalTitle>React Modal Popover Example</ModalTitle>
      </ModalHeader>
      <ModalBody>{props.body}</ModalBody>
      <ModalFooter>
        <button onClick={props.initModal}>Close</button>
        <button onClick={props.initModal}>Store</button>
      </ModalFooter>
    </div>
  );
}
