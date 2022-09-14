import React, { Component, useEffect, useState } from "react";
import {
  Container,
  Form,
  Label,
  CustomInput,
  Button,
  FormGroup,
  Input,
} from "reactstrap";
import classroomService from "../../services/classroomService";
import teachersService from "../../services/teachersService";
import coursesService from "../../services/coursesService";

const CourseDetailsForm = ({ courseId }) => {
  const [teachersList, setTeachersList] = useState([]);
  const [classroomList, setClassroomList] = useState([]);

  useEffect(() => {
    //get list of classrooms
    const fetchCourseData = async () => {
      const classroom = await classroomService.list();
      setClassroomList(classroom);
    };

    //get list of teachers
    const fetchTeacherData = async () => {
      const teachers = await teachersService.list();
      setTeachersList(teachers);
    };

    fetchCourseData().catch(console.error);
    fetchTeacherData().catch(console.error);
  }, []);

  useEffect(() => {
    console.log("courseId", courseId);
    if (courseId) {
      const fetchCourseDetails = async () => {
        const details = await coursesService.details(courseId);
        console.log(details);
      };

      fetchCourseDetails().catch(console.error);
    }
  }, [courseId]);
  return (
    <Container>
      <h5>Course details</h5>
      <Form>
        <FormGroup className="mb-2 mr-sm-2 mb-sm-0 pb-2">
          <Label className="mr-sm-2">Course Name</Label>
          <Input name="name" />
        </FormGroup>
        <FormGroup className="mb-2 mr-sm-2 mb-sm-0 pb-2">
          <Label className="mr-sm-2">Course Description</Label>
          <Input type="textarea" name="description" />
        </FormGroup>
        <FormGroup className="mb-2 mr-sm-2 mb-sm-0 pb-2">
          <Label className="mr-sm-2">Classroom</Label>
          <CustomInput id="classroom" name="classroom" type="select">
            <option value="">Select Classroom</option>
            {classroomList.map((x, i) => {
              return (
                <option key={x.id} value={x.id}>
                  {x.name}
                </option>
              );
            })}
          </CustomInput>
        </FormGroup>
        <FormGroup check>
          <Label check />
          <Input name="active" type="checkbox" /> Course is Active
        </FormGroup>
        <Button
          color="primary"
          className="mt-3 sm-4 right"
          //disabled={isDisabled}
        >
          Save Details
        </Button>
      </Form>
    </Container>
  );
};

export default CourseDetailsForm;
