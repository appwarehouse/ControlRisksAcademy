import React, { Component, useEffect, useState } from "react";
import Joi from "joi-browser";
import { toast, ToastContainer } from "react-toastify";
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
  const [courseDetails, setCourseDetails] = useState({
    name: "",
    description: "",
    classroom: { id: "", name: "", active: "" },
    active: true,
  });
  const [errors, setErrors] = useState({});
  const schema = {
    name: Joi.string().required().label("Name"),
    description: Joi.string().required().label("Description"),
    active: Joi.boolean(),
    classroom: Joi.object({
      id: Joi.number().label("Classroom Id"),
      name: Joi.string().label("Classroom Name"),
    }),
  };

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
    if (courseId) {
      const fetchCourseDetails = async () => {
        const details = await coursesService.details(courseId);
        try {
          setCourseDetails({
            name: details.name,
            description: details.description,
            active: details.active,
          });
        } catch (error) {
          toast.error("Error retrieving records.");
        }
      };

      fetchCourseDetails().catch(console.error);
    } else {
      setCourseDetails({
        name: "",
        description: "",
        active: true,
      });
    }
  }, [courseId]);

  const validateProperty = (event) => {
    const { name: variable, value } = event.target;
    const obj = { [variable]: value };
    const subSchema = { [variable]: schema[variable] };
    const result = Joi.validate(obj, subSchema);
    const { error } = result;
    return error ? error.details[0].message : null;
  };

  const validateForm = (event) => {
    event.preventDefault();
    const result = Joi.validate(courseDetails, schema, { abortEarly: false });

    const { error } = result;
    if (!error) {
      return null;
    } else {
      const errorData = {};
      for (let item of error.details) {
        const name = item.path[0];
        const message = item.message;
        errorData[name] = message;
      }
      setErrors(errorData);
      return errorData;
    }
  };

  const handleSave = (event) => {
    const { name, value } = event.target;
    let errorData = { ...errors };
    const errorMessage = validateProperty(event);

    if (errorMessage) {
      errorData[name] = errorMessage;
    } else {
      delete errorData[name];
    }
    let courseData = { ...courseDetails };
    courseData[name] = value;
    setCourseDetails(courseData);
    setErrors(errorData);
  };

  const handleSubmit = async (e) => {
    e.preventDefault();

    if (!validateForm(e)) {
      if (courseId != null) {
        //update
        const updated = await coursesService.updateCourse(
          courseId,
          courseDetails
        );

        if (updated.success) toast.success("Course updated successfully");
        else
          toast.error(
            `There was an error updating the course details. ${updated.message}`
          );
      } else {
        //create
        const created = await coursesService.createCourse(courseDetails);
        if (created.success) {
          toast.success("Course created successfully");
        } else {
          toast.error(`There was an error creating course. ${created.message}`);
        }
      }
    }
  };

  return (
    <Container>
      <h5>Course details</h5>
      <Form onSubmit={handleSubmit}>
        <FormGroup className="mb-2 mr-sm-2 mb-sm-0 pb-2">
          <Label className="mr-sm-2">Course Name</Label>
          <Input
            name="name"
            value={courseDetails.name}
            onChange={handleSave}
            placeholder="Name"
          />
          {errors.name && <i style={{ color: "red" }}>{errors.name}</i>}
        </FormGroup>
        <FormGroup className="mb-2 mr-sm-2 mb-sm-0 pb-2">
          <Label className="mr-sm-2">Course Description</Label>
          <Input
            type="textarea"
            value={courseDetails.description}
            onChange={handleSave}
            name="description"
          />
          {errors.description && (
            <i style={{ color: "red" }}>{errors.description}</i>
          )}
        </FormGroup>
        {/* <FormGroup className="mb-2 mr-sm-2 mb-sm-0 pb-2">
          <Label className="mr-sm-2">Classroom</Label>
          <CustomInput
            id="classroom"
            name="classroom"
            value={courseDetails.classroom.id}
            onChange={handleSave}
            type="select"
          >
            <option value="">Select Classroom</option>
            {classroomList.map((x, i) => {
              return (
                <option key={x.id} value={x.id}>
                  {x.name}
                </option>
              );
            })}
          </CustomInput>
          {errors.classroom.id && (
            <i style={{ color: "red" }}>{errors.classroom.id}</i>
          )}
        </FormGroup> */}
        <FormGroup check>
          <Label check />
          <Input
            name="active"
            value={courseDetails.active}
            onChange={handleSave}
            type="checkbox"
          />{" "}
          Course is Active
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
