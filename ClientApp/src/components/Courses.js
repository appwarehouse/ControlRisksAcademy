import React, { useState, useEffect } from "react";
import { toast } from "react-toastify";
import {
  Badge,
  Button,
  ButtonGroup,
  Col,
  Container,
  Input,
  ListGroup,
  ListGroupItem,
  Row,
} from "reactstrap";
import coursesService from "../services/coursesService";
import CourseDetailsForm from "./common/CourseDetailsForm";

function Courses() {
  const [selectedCourse, setSelectedCourse] = useState("");
  const [courseList, setCourseList] = useState([
    { id: "", name: "", classroom: "", description: "", active: true },
  ]);

  useEffect(() => {
    const fetchCourseData = async () => {
      const courses = await coursesService.list();
      setCourseList(courses);
    };

    fetchCourseData().catch(console.error);
  }, []);

  // handle click event of the Add button
  const handleSelectionClick = () => {
    setCourseList([...courseList, { id: "", name: "", active: true }]);
  };

  return (
    <Container>
      <h1>Course Manager</h1>
      <Row>
        <Col xs="3" className="mb-2">
          <h5>Course List</h5>
          <p>Select a course </p>
          <ListGroup>
            {courseList.map((x, i) => (
              <ListGroupItem
                {...(selectedCourse === x.id ? "active" : "")}
                tag="a"
                onClick={() => setSelectedCourse(x.id)}
                action
              >
                {x.name}
              </ListGroupItem>
            ))}
          </ListGroup>
        </Col>
        <Col xs="9">
          <CourseDetailsForm courseId={selectedCourse} />
        </Col>
      </Row>
    </Container>
  );
}

export default Courses;
