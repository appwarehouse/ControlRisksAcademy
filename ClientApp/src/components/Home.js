import React, { useState, useEffect } from "react";
import {
  Container,
  Col,
  Row,
  Table,
  Badge,
  Button,
  ButtonGroup,
  ModalHeader,
  ModalBody,
  Modal,
  ModalFooter,
} from "reactstrap";
import studentService from "./../services/studentService";
import Moment from "react-moment";
import { toast } from "react-toastify";
import { useHistory } from "react-router";
import StudentDetails from "./common/StudentDetails";

const Home = () => {
  const history = useHistory();
  const [studentData, setStudentData] = useState([]);
  const [selectedStudent, setSelectedStudent] = useState(null);
  const [modelOpen, setModelOpen] = useState(false);

  //on load
  useEffect(() => {
    fetchStudentData().catch(console.error);
  }, []);
  const fetchStudentData = async () => {
    const students = await studentService.list();
    setStudentData(students);
  };
  //edit a student details
  const handleStudentEdit = (id) => {
    //history.push(`/student-details/${id}`);
    setSelectedStudent(id);
    setModelOpen(true);
  };

  const modalToggle = () => {
    fetchStudentData();
    setModelOpen(false);
  };

  //update the active status of a student
  const handleActivation = async (x) => {
    const status = await studentService.deactivateStudent(x.id);
    if (!status) {
      toast.error(`Failed to update active status for ${x.name} ${x.surname}.`);
      return;
    }

    if (status) {
      const updated = studentData.map((p) =>
        p === x ? { ...p, active: !x.active } : p
      );

      setStudentData(updated);
      toast.success(`Active status updated for ${x.name} ${x.surname}`);
    }
  };

  //ToDo: Handle the Enrollment of a student
  const handleEnrollment = (id) => {
    toast.info("Future feature to enroll student in a set of courses.");
  };
  return (
    <Container>
      <Row>
        <h1>Welcome to The Academy</h1>
      </Row>
      <Row></Row>
      <Row>
        <Table striped bordered>
          <thead>
            <tr>
              <th>Student #</th>
              <th>Name</th>
              <th>Surame</th>
              <th>Gender</th>
              <th>DoB</th>
              <th>Active</th>
              <th></th>
            </tr>
          </thead>
          <tbody>
            {studentData.length === 0 && (
              <tr>
                <td colSpan={7}>No student data to display</td>
              </tr>
            )}
            {studentData.length > 0 &&
              studentData.map((x) => {
                return (
                  <tr key={x.id}>
                    <td>{x.studentNumber}</td>
                    <td>{x.name}</td>
                    <td>{x.surname}</td>
                    <td>{x.gender}</td>
                    <td>
                      <Moment format="DD-MMM-YYYY">{x.dob}</Moment> (
                      <Moment fromNow ago>
                        {x.dob}
                      </Moment>
                      )
                    </td>
                    <td>
                      {x.active ? (
                        <Badge color="success" pill>
                          Active
                        </Badge>
                      ) : (
                        <Badge color="danger" pill>
                          Inactive
                        </Badge>
                      )}
                    </td>
                    <td>
                      <ButtonGroup>
                        <Button
                          size="sm"
                          color="primary"
                          onClick={() => handleStudentEdit(x.id)}
                        >
                          Edit
                        </Button>
                        <Button
                          size="sm"
                          color={x.active ? "danger" : "success"}
                          onClick={() => handleActivation(x)}
                        >
                          {x.active ? "Deactivate" : "Activate"}
                        </Button>
                        <Button
                          size="sm"
                          color="secondary"
                          onClick={() => handleEnrollment(x.id)}
                        >
                          Enrollment
                        </Button>
                      </ButtonGroup>
                    </td>
                  </tr>
                );
              })}
          </tbody>
        </Table>
      </Row>

      <Modal isOpen={modelOpen} toggle={modalToggle}>
        <ModalBody>
          <StudentDetails studentId={selectedStudent}></StudentDetails>
        </ModalBody>
        <ModalFooter>
          <Button size="sm" color="danger" onClick={modalToggle}>
            Close
          </Button>
        </ModalFooter>
      </Modal>
    </Container>
  );
};

export default Home;
