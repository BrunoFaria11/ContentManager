import React from "react";
import { Alert, Container } from "react-bootstrap";

export default function ReponseAlert(props: any) {
  let data = null;
  try {
    data = JSON.parse(props.response).data;
  } catch (error) {
    data = null;
  }

  return (
    <div className="App">
      <Container className="p-4">
        {data == null ? (
          <Alert variant="danger" dismissible>
            <pre>{props.response}</pre>
          </Alert>
        ) : (
          <Alert variant="success" dismissible>
            <pre>{props.response}</pre>
          </Alert>
        )}
      </Container>
    </div>
  );
}
