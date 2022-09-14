import React, { useState, useEffect } from "react";
import { Link, Redirect, useHistory } from "react-router-dom";
import { toast, ToastContainer } from "react-toastify";
import { Button, Form, FormGroup, Label, Input, Col } from "reactstrap";
import logo from "../assests/control-risks-logo-dark.png";
import auth from "../services/authService";

const LoginForm = (props) => {
  const [email, setEmail] = useState("");
  const [password, setPassword] = useState("");
  const [isDisabled, setIsDisabled] = useState(true);

  const handleSubmit = async (e) => {
    e.preventDefault();
    const result = await auth.login(email, password);
    console.log("Login Result", result);
    if (result !== undefined) {
      if (result.isAuthenticated) {
        window.location.reload(false);
        return;
      }

      toast.error(`Login failed. ${result.message}`);
      return;
    }
    toast.error(`Login failed`);
  };

  useEffect(() => {
    setIsDisabled(!email || !password);
  }, [email, password]);

  return (
    <>
      <ToastContainer />
      <Col sm="12" className="mt-5" style={{ textAlign: "center" }}>
        <img
          src={logo}
          alt="Control Risks Logo"
          className="img-fluid logo justify-content-md-center"
        />
        <h1>Academy</h1>
      </Col>
      <Col sm="12" md={{ size: 8, offset: 2 }}>
        <Col sm="12" className="left">
          <Form onSubmit={handleSubmit}>
            <FormGroup className="mb-2 mr-sm-2 mb-sm-0 pb-2">
              <Label className="mr-sm-2">Email/Username</Label>
              <Input
                type="email"
                value={email}
                onChange={({ target }) => setEmail(target.value)}
                placeholder="email@example.com"
              />
            </FormGroup>
            <FormGroup className="mb-2 mr-sm-2 mb-sm-0 pb-2">
              <Label className="mr-sm-2">Password</Label>
              <Input
                type="password"
                value={password}
                onChange={({ target }) => setPassword(target.value)}
                placeholder="Password"
              />
            </FormGroup>
            <Button
              color="primary"
              className="mt-3 sm-4 right"
              disabled={isDisabled}
            >
              Login
            </Button>
          </Form>
        </Col>
      </Col>
    </>
  );
};

export default LoginForm;
