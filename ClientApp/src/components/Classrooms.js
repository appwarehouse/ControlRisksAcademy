import React, { useState, useEffect } from "react";
import { toast } from "react-toastify";
import {
  Badge,
  Button,
  ButtonGroup,
  Col,
  Container,
  Input,
  Row,
} from "reactstrap";
import classroomService from "../services/classroomService";

function Classrooms() {
  const [classroomList, setClassroomList] = useState([
    { id: "", name: "", active: true },
  ]);

  useEffect(() => {
    const fetchClassroomData = async () => {
      const classrooms = await classroomService.list();
      setClassroomList(classrooms);
    };

    fetchClassroomData().catch(console.error);
  }, []);
  // handle input change
  const handleInputChange = (e, index) => {
    const { name, value } = e.target;
    const list = [...classroomList];
    list[index][name] = value;
    setClassroomList(list);
  };

  // handle click event of the Remove button
  const handleRemoveClick = async (id, index) => {
    if (id === "") {
      const list = [...classroomList];
      list.splice(index, 1);
      setClassroomList(list);
      return;
    }
    const result = await classroomService.deleteClassroom(id);
    if (result) {
      const list = [...classroomList];
      list.splice(index, 1);
      setClassroomList(list);
      toast.success("Classroom deleted.");
      return;
    }
    toast.error("Failed to delete classroom.");
  };

  // handle click event of the Save button
  const handleSaveClick = async (index) => {
    const { name, active } = classroomList[index];

    const result = await classroomService.createClassroom({
      name: name,
      active: active,
    });

    if (result.success) {
      const list = [...classroomList];
      list[index].id = result.data.id;
      setClassroomList(list);
      toast.success("Classroom added successfully.");
    } else {
      const list = [...classroomList];
      list.splice(index, 1);
      setClassroomList(list);
      toast.error(result.message);
    }
  };

  // handle click event of the Remove button
  const handleUpdateClick = async (index) => {
    const { id, name, active } = classroomList[index];
    const result = await classroomService.updateClassroom(id, {
      name: name,
      active: active,
    });

    if (result) {
      toast.success("Classroom name updated.");
      return;
    }
    toast.error("Failed to update classroom.");
  };

  // handle click event of the Add button
  const handleAddClick = () => {
    setClassroomList([...classroomList, { id: "", name: "", active: true }]);
  };

  return (
    <Container>
      <h1>Classroom List</h1>
      <div className="container">
        {classroomList.map((x, i) => {
          return (
            <Row key={i}>
              <Col xs="6" className="mb-2">
                <input
                  className="form-control"
                  name="name"
                  placeholder="Enter a classroom name"
                  value={x.name}
                  onChange={(e) => handleInputChange(e, i)}
                />
              </Col>
              <Col xs="3">
                {x.active ? (
                  <Badge color="success" pill>
                    Active
                  </Badge>
                ) : (
                  <Badge color="danger" pill>
                    Inactive
                  </Badge>
                )}
              </Col>
              <Col xs="3">
                <ButtonGroup>
                  {classroomList.length !== 1 && (
                    <>
                      <Button
                        size="sm"
                        color="primary"
                        onClick={() => handleUpdateClick(i)}
                      >
                        Update
                      </Button>
                      <Button
                        size="sm"
                        color="danger"
                        onClick={() => handleRemoveClick(x.id, i)}
                      >
                        {x.id === "" ? "Remove" : "Delete"}
                      </Button>
                    </>
                  )}
                  {classroomList.length - 1 === i && x.id === "" && (
                    <Button
                      size="sm"
                      color="info"
                      onClick={() => handleSaveClick(i)}
                      disabled={x.name.length <= 0}
                    >
                      Save
                    </Button>
                  )}
                  {classroomList.length - 1 === i && (
                    <Button size="sm" color="warning" onClick={handleAddClick}>
                      Add
                    </Button>
                  )}
                </ButtonGroup>
              </Col>
            </Row>
          );
        })}
      </div>
    </Container>
  );
}

export default Classrooms;
