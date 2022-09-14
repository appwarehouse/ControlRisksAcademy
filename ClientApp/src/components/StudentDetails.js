import React, { useState, useEffect } from "react";
import { Link, Redirect, useHistory, useParams } from "react-router-dom";
import { toast, ToastContainer } from "react-toastify";
import Joi from "joi-browser";
import {
  Button,
  Form,
  FormGroup,
  Label,
  Input,
  Col,
  CustomInput,
} from "reactstrap";
import studentService from "../services/studentService";

const StudentDetails = () => {
  let { id } = useParams();
  const history = useHistory();
  const [studentDetails, setStudentDetails] = useState({
    name: "",
    surname: "",
    emailAddress: "",
    phoneNumber: "",
    dob: "",
    gender: "",
    active: true,
    studentNumber: "0",
  });
  const [errors, setErrors] = useState({});
  const schema = {
    name: Joi.string().required().label("Name"),
    surname: Joi.string().required().label("Surname"),
    emailAddress: Joi.string().email().required().label("Email"),
    gender: Joi.string().required().label("Gender"),
    dob: Joi.date().required().label("Date of Birth"),
    phoneNumber: Joi.string().min(10).label("Phone"),
    active: Joi.boolean(),
    studentNumber: Joi.string(),
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
    let studentData = { ...studentDetails };
    studentData[name] = value;
    setStudentDetails(studentData);
    setErrors(errorData);
  };

  useEffect(() => {
    console.log("ships");
  }, []);

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
    const result = Joi.validate(studentDetails, schema, { abortEarly: false });

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

  const handleSubmit = async (e) => {
    e.preventDefault();

    if (!validateForm(e)) {
      console.log("details", studentDetails);
      const created = await studentService.createStudent(studentDetails);

      if (created.success) {
        toast.success("User created successfully");
        history.push("/");
      } else {
        toast.error(`There was an error creating user. ${created.message}`);
      }
    }
  };
  return (
    <>
      <Col sm="12" className="mt-5" style={{ textAlign: "center" }}>
        <h1>Student Details</h1>
      </Col>
      <Col sm="12" md={{ size: 8, offset: 2 }}>
        <Col sm="12" className="left">
          <Form onSubmit={handleSubmit}>
            <FormGroup className="sm-6 mb-2 mr-sm-2 mb-sm-0 pb-2">
              <Label className="mr-sm-2">Name</Label>
              <Input
                name="name"
                value={studentDetails.name}
                onChange={handleSave}
                placeholder="First Name"
              />
              {errors.name && <i style={{ color: "red" }}>{errors.name}</i>}
            </FormGroup>
            <FormGroup className="sm-6 mb-2 mr-sm-2 mb-sm-0 pb-2">
              <Label className="mr-sm-2">Surame</Label>
              <Input
                name="surname"
                value={studentDetails.surname}
                onChange={handleSave}
                placeholder="Surname"
              />
              {errors.surname && (
                <i style={{ color: "red" }}>{errors.surname}</i>
              )}
            </FormGroup>
            <FormGroup className="sm-6 mb-2 mr-sm-2 mb-sm-0 pb-2">
              <Label className="mr-sm-2">Email Address</Label>
              <Input
                name="emailAddress"
                value={studentDetails.emailAddress}
                onChange={handleSave}
                placeholder="email@example.com"
              />
              {errors.emailAddress && (
                <i style={{ color: "red" }}>{errors.emailAddress}</i>
              )}
            </FormGroup>
            <FormGroup className="sm-6 mb-2 mr-sm-2 mb-sm-0 pb-2">
              <Label className="mr-sm-2">Phone/Mobile Number</Label>
              <Input
                name="phoneNumber"
                value={studentDetails.phoneNumber}
                onChange={handleSave}
                placeholder="0710000000"
              />
              {errors.phoneNumber && (
                <i style={{ color: "red" }}>{errors.phoneNumber}</i>
              )}
            </FormGroup>
            <FormGroup className="mb-2 mr-sm-2 mb-sm-0 pb-2">
              <Label className="mr-sm-2">Date of Birth</Label>
              <Input
                name="dob"
                value={studentDetails.dob}
                onChange={handleSave}
                placeholder="1 Jan 2022"
                type="date"
              />
              {errors.dob && <i style={{ color: "red" }}>{errors.dob}</i>}
            </FormGroup>
            <FormGroup className="mb-2 mr-sm-2 mb-sm-0 pb-2">
              <Label className="mr-sm-2">Gender</Label>
              <CustomInput
                id="gender"
                name="gender"
                value={studentDetails.gender}
                onChange={handleSave}
                type="select"
              >
                <option value="">Select Gender</option>
                <option value="M">Male</option>
                <option value="F">Female</option>
              </CustomInput>
              {errors.gender && <i style={{ color: "red" }}>{errors.gender}</i>}
            </FormGroup>

            <Button color="primary" className="mt-3 sm-4 right">
              Save Details
            </Button>
          </Form>
        </Col>
      </Col>
    </>
  );
};

export default StudentDetails;
